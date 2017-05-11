using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.ViewModels;

namespace VendingMachine.Controllers
{
    public class HomeController : Controller
    {
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

        public IActionResult GetCoinValue(int coinWeight, int coinSize)
        {
            return Json(true);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
