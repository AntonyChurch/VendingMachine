using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.Currencies;
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

        public IActionResult Index()
        {
            IndexViewModel viewModel = new IndexViewModel();
            viewModel.SessionId = Guid.NewGuid();
            viewModel.Items = _itemService.GetItems();
            viewModel.AvailableCoins = _currency.GetItems();

            _sessionService.StoreCurrentTally(viewModel.SessionId, 0);
            
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel model)
        {

            return View(model);
        }

        [HttpPost]
        public IActionResult GetCoinValue(double coinWeight, double coinSize, Guid sessionId)
        {
            var coinValue = _coinValueService.GetCoinValue(coinWeight, coinSize);

            if(coinValue.IsValidCoin)
            {
                double currentTally = _sessionService.GetStoredTally(sessionId);
                //Session does not exist, so create a new one with the coin value.
                if(currentTally > -1)
                {
                    _sessionService.StoreCurrentTally(sessionId, currentTally + coinValue.CoinValue.Value);
                }
                else
                {
                    _sessionService.StoreCurrentTally(sessionId, coinValue.CoinValue.Value);
                }
            }

            return Json(new { CoinValue = coinValue.IsValidCoin, NewTotal = _sessionService.GetStoredTally(sessionId), SessionId = sessionId});
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
