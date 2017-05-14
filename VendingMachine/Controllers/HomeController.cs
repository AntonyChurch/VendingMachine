using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.Currencies;
using VendingMachine.Models;
using VendingMachine.Services;
using VendingMachine.ViewModels;

namespace VendingMachine.Controllers
{
    public class HomeController : Controller
    {
        private ISessionService _sessionService;
        private ICoinValueService _coinValueService;
        private ICurrency _currency;
        private IItemService _itemService;
        private IPurchaseService _purchaseService;
        private IChangeService _changeService;

        public HomeController(
            ISessionService sessionService,
            ICoinValueService coinValueService,
            ICurrency currency,
            IItemService itemService,
            IPurchaseService purchaseService,
            IChangeService changeService)
        {
            _sessionService = sessionService;
            _coinValueService = coinValueService;
            _currency = currency;
            _itemService = itemService;
            _purchaseService = purchaseService;
            _changeService = changeService;
        }

        public IActionResult Index(double? change, int? itemId)
        {
            IndexViewModel viewModel = new IndexViewModel();
            viewModel.SessionId = Guid.NewGuid();
            viewModel.Items = _itemService.GetItems();
            viewModel.AvailableCoins = _currency.GetItems();

            if(change.HasValue)
            {
                viewModel.CoinsToCollect = _changeService.ChangeList(change.Value).Select(c => c.Value.ToString("C")).ToList();
            }

            if(itemId.HasValue)
            {
                viewModel.ItemsToCollect.Add(_itemService.GetItems()[itemId.Value].Name);
            }                

            _sessionService.StoreCurrentTally(viewModel.SessionId, 0);
            
            return View(viewModel);
        }

        public IActionResult ReturnCoins(Guid sessionId)
        {
            return RedirectToAction("Index", new { change = _sessionService.GetStoredTally(sessionId) });
        }

        [HttpPost]
        public IActionResult InsertCoin(double coinWeight, double coinSize, Guid sessionId)
        {
            var coinValue = _coinValueService.GetCoinValue(coinWeight, coinSize);

            if(coinValue.IsValidCoin)
            {
                double currentTally = _sessionService.GetStoredTally(sessionId);
                //Session does not exist, so create a new one with the coin value.
                if(currentTally > 0)
                {
                    _sessionService.StoreCurrentTally(sessionId, currentTally + coinValue.CoinValue.Value);
                }
                else
                {
                    _sessionService.StoreCurrentTally(sessionId, coinValue.CoinValue.Value);
                }
            }

            return Json(new { 
                CoinValue = coinValue.IsValidCoin, 
                NewTotal = _sessionService.GetStoredTally(sessionId),
                NewTotalString = _sessionService.GetStoredTally(sessionId).ToString("C")
                });
        }

        [HttpPost]
        public IActionResult BuyProduct(Guid sessionId, int itemId)
        {
            ItemModel item = _itemService.GetItems()[itemId];

            if(_purchaseService.CanAffordProduct(sessionId, itemId) < 0)
            {
                return Json(new { CanAfford = false, DisplayText = String.Format("PRICE {0}", item.Price.ToString("C"))});
            }           

            double newCredit = _purchaseService.BuyProduct(sessionId, itemId);

            return Json(new { CanAfford = true, Balance = newCredit});
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
