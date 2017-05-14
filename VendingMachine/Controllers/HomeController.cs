using System;
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

        public HomeController(
            ISessionService sessionService,
            ICoinValueService coinValueService,
            ICurrency currency,
            IItemService itemService,
            IPurchaseService purchaseService)
        {
            _sessionService = sessionService;
            _coinValueService = coinValueService;
            _currency = currency;
            _itemService = itemService;
            _purchaseService = purchaseService;
        }

        public IActionResult Index(double? change, int? itemId)
        {
            IndexViewModel viewModel = new IndexViewModel();
            viewModel.SessionId = Guid.NewGuid();
            viewModel.Items = _itemService.GetItems();
            viewModel.AvailableCoins = _currency.GetItems();

            if(change.HasValue)
            {
                //Add change to view model
                int i = 0;
                i += 1;
            }

            _sessionService.StoreCurrentTally(viewModel.SessionId, 0);
            
            return View(viewModel);
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
