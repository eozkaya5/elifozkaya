using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CreditBook.Models;
using CreditBook.Models.Context;
using Microsoft.AspNetCore.Identity;
using Identity.Models.Authentication;
using Microsoft.EntityFrameworkCore;

namespace CreditBook.Controllers
{
    public class HomeController : Controller
    {


        private readonly BookDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public HomeController(BookDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index(int id, int ucrett)
        {
            ViewBag.UserName = User.Identity.Name;
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var customer = _context.Customers.Where(x => x.UserId == user.Id).Include(x => x.Payments).Include(x => x.Shoppings);

            var shoping = _context.Shoppings.FirstOrDefault(x => x.CustomerId == id);
            //if (shoping != null)
            //{
            //    ucrett = _context.Shoppings.Where(x => x.CustomerId == shoping.CustomerId).Sum(x => x.Tot);
            //    ViewBag.ucrett = +ucrett + "₺";
            //}

            ViewBag.CustomerId = id;

            return View(customer);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
