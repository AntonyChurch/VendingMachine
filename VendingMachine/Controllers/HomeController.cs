using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VendingMachine.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //int thing = int.MaxValue;

            //int cals = thing + 1;

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
