using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreditBook.Models.BookViewModel;
using CreditBook.Models.Context;
using Identity.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CreditBook.Controllers
{
 [Authorize]
    public class ShoppingController : Controller
    {
        public readonly BookDbContext _context;

        public ShoppingController(BookDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id, decimal ucrett)
        {
            ViewBag.UserName = User.Identity.Name;
            List<Shopping> model = _context.Shoppings.Include(x => x.Customer).Where(x => x.CustomerId == id).ToList();
            ViewBag.CustomerId = id;     
            return View(model);
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            var todo = new Shopping { CustomerId = id };
            return View(todo);
        }
        [HttpPost]
        public IActionResult Create(Shopping shopping)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var payment = _context.Customers.Include(x => x.Payments).Include(x => x.Shoppings).FirstOrDefault( x=>x.Id ==shopping.CustomerId);
                    payment.TotalDept += shopping.TotalFee;
                    shopping.CreateDate = DateTime.Now;
                    _context.Shoppings.Add(shopping);
                    _context.SaveChanges();
                    return RedirectToAction("Index", new { id = shopping.CustomerId });
                }
            }
            catch (Exception)
            {

            }
            return View(shopping);
        }

        public IActionResult Delete(int id)
        {
            var delete = _context.Shoppings.Find(id);         
                _context.Shoppings.Remove(delete);
                _context.SaveChanges();                      
            return RedirectToAction("Index", new { id = delete.CustomerId });
        }
    }
}
