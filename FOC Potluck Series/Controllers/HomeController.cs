using FOC_Potluck_Series.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace FOC_Potluck_Series.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult RSVP()
        {
            return View();
        }
        public IActionResult Registered(Teammember teammember)
        {
            bool isvalid = true;
            if(teammember.LastName == null || teammember.LastName.Length < 1)
            {
                TempData["LastNameError"] = "*Last name must contain at least one character.";
                isvalid = false;
            }
            if (teammember.Email == null || !teammember.Email.Contains("@"))
            {
                TempData["EmailError"] = "*Email must contain the @ symbol.";
                isvalid = false;
            }
            if(isvalid == true)
            {
                return View(teammember);
            }
            else
            {
                return Redirect("/Home/RSVP");
            }
        }
        public IActionResult BringDish()
        {
            return View();
        }
        
        public IActionResult RegisteredDish(DishInfo dish)
        {
            bool isvalid = true;

            Regex phone = new Regex("^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$");
            if (dish.PhoneNumber == null || !phone.IsMatch(dish.PhoneNumber))
            {
                TempData["PhoneError"] = "You have entered an invalid phone number. Please try again.";
                isvalid = false;
            }
            if(dish.DishName == null || dish.DishName.Length < 2)
            {
                TempData["DishNameError"] = "You have entered an invalid value for the dish. Please try again.";
                isvalid = false;
            }
            if(isvalid == true)
            {
                return View(dish);
            }
            else
            {
                return Redirect("/Home/BringDish");
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}