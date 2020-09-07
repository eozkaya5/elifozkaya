using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreditBook.Models.BookViewModel;
using CreditBook.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CreditBook.Controllers
{
 [Authorize]
    public class PaymentController : Controller
    {
        public readonly BookDbContext _context;

        public PaymentController(BookDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id,decimal ucrett)
        {
            ViewBag.UserName = User.Identity.Name;
            List<Payment> model = _context.Payments.Include(x => x.Customer).Where(x => x.CustomerId == id).ToList();
            ViewBag.CustomerId = id;           
            return View(model);
           
        }
        [HttpGet]
        public IActionResult PaymentDept(int id)
        {
            var model = new Payment { CustomerId = id };
            return View(model);
        }
        [HttpPost]
        public IActionResult PaymentDept(Payment model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var payment = _context.Customers.Include(x => x.Payments).Include(x => x.Shoppings).FirstOrDefault(x => x.Id == model.CustomerId);
                    payment.TotalDept -= model.FeePaid;
                    model.Date = DateTime.Now;
                    _context.Payments.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction("Index", new { id = model.CustomerId });
                }
            }
            catch (Exception)
            {

            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var model = _context.Payments.Find(id);
            _context.Payments.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index", new { id = model.CustomerId });
        }
    }
}
