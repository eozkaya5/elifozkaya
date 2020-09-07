using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreditBook.Migrations;
using CreditBook.Models.BookViewModel;
using CreditBook.Models.Context;
using Identity.Models.Authentication;
using Identity.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CreditBook.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly BookDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public CustomerController(BookDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index(int id, decimal ucrett)
        {
            ViewBag.UserName = User.Identity.Name;
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var customer = _context.Customers.Where(x => x.UserId == user.Id).Include(x => x.Payments).Include(x => x.Shoppings);
            ViewBag.CustomerId = id;
            return View(customer);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            try
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                customer.UserId = user.Id;
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return RedirectToAction("Index", "Customer");
            }
            catch (Exception)
            {
                return View(customer);
            }
        }

        public IActionResult Delete(int? id, Customer customer)
        {
            try
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                var delete = _context.Customers.Include(x => x.Shoppings).FirstOrDefault(x => x.Id == id);
                delete.UserId = user.Id;
                var shopping = _context.Shoppings.FirstOrDefault(x => x.CustomerId == id);
                Customer dept = _context.Customers.First(k => k.TotalDept == 0);
                _context.Customers.Remove(dept);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                TempData["Message"] = "Ödenmesi gereken borç bulunmaktadır. Ödeme işlemi yapıldıktan sonra tekrar deneyiniz.";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _context.Customers.FirstOrDefault(x => x.Id == id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Customer customer, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = _context.Customers.Include(x => x.Payments).Include(x => x.Shoppings).FirstOrDefault(x => x.Id == id);
                    model.NameSurname = customer.NameSurname;
                    _context.SaveChanges();
                    return RedirectToAction("Index", new { id = model.UserId });
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View(customer);
        }





    }

}


