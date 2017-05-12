using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.Services;
using VendingMachine.ViewModels;

namespace VendingMachine.Controllers
{
    public class HomeController : Controller
    {
        private ICoinValueService _coinValueService;

        public HomeController(ICoinValueService coinValueService)
        {
            _coinValueService = coinValueService;
        }

        public IActionResult Index()
        {
            IndexViewModel viewModel = new IndexViewModel();
            
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult GetCoinValue(double coinWeight, double coinSize)
        {
            return Json(_coinValueService.GetCoinValue(coinWeight, coinSize));
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
