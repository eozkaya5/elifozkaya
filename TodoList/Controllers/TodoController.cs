using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration.UserSecrets;
using TodoList.Models.Authentication;
using TodoList.Models.Context;
using TodoList.Models.Entites;
using TodoList.Models.Viewmodels;

namespace TodoList.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly TodoDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public TodoController(UserManager<AppUser> userManager, TodoDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }       
        public IActionResult Index()
        {
            ViewBag.UserName = User.Identity.Name;
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var model =  Tuple.Create<Todo,List<Todo>>(new Todo(),_context.Todos.Where(x => x.UserId == user.Id).ToList());
            return View(model);
           
        }
        public IActionResult Detail()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var models = _context.Todos.Where(x => x.UserId == user.Id).FirstOrDefault();
            return View(models);
        }     
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Todo todo)
        {
            try
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                todo.DateTime = DateTime.Now;
                todo.UserId = user.Id;
                _context.Todos.Add(todo);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(todo);
            }
        }     
        public IActionResult Edit(int id)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var todo = _context.Todos.FirstOrDefault(x => x.Id == id && x.UserId == user.Id);
            return View(todo);
        }
        [HttpPost]
        public IActionResult Edit(Todo todo)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            todo.UserId = user.Id;
            _context.Entry(todo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.Todos.Update(todo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var todo = _context.Todos.FirstOrDefault(x => x.Id == id && x.UserId == user.Id);
            todo.UserId = user.Id;

            _context.Todos.Remove(todo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

