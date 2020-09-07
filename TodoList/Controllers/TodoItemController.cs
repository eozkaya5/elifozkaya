
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoList.Migrations.TodoDb;
using TodoList.Models.Context;
using TodoList.Models.Entites;

namespace TodoList.Controllers
{
    [Authorize]
    public class TodoItemController : Controller
    {
        public readonly TodoDbContext _context;
        public TodoItemController(TodoDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            List<TodoItem> item = _context.TodoItems.Include(x => x.Todo).Where(x => x.TodoId == id).ToList();
            ViewBag.TodoId = id;
            return View(item);
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            #region LinQ Sorgu
            //IEnumerable<SelectListItem> model = (from x in _context.Todos.ToList()
            //                                     select new SelectListItem
            //                                     {
            //                                         Text = x.Name,
            //                                         Value = x.Id.ToString()
            //                                     }).ToList();
            //ViewBag.value = model;

            //ViewData["TodoId"] = "TodoId";
            //var item = _context.TodoItems.Where(x => x.TodoId == id).FirstOrDefault();     
            //var item = (from todo in _context.Todos
            //            select new TodoItem
            //            {
            //                Id = todo.Id,
            //                TodoId = todo.Id,
            //                Name = todo.Name
            //            });
            #endregion
            var todo = new TodoItem { TodoId = id };
            return View(todo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Route("TodoItem/Create/{id?}")]
        //public ActionResult ItemEkle([FromForm]TodoItem todoItem, string abc)
        public IActionResult Create(TodoItem todoItem)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    var todo = _context.Todos.Find(todoItem.TodoId);
                    todoItem.Status = false;
                    todoItem.DateTime = DateTime.Now;
                    _context.TodoItems.Add(todoItem);
                    _context.SaveChanges();
                    return RedirectToAction("Index", new { id = todoItem.TodoId });
                }
            }
            catch (Exception)
            {


            }
            return View(todoItem);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = _context.TodoItems.FirstOrDefault(x => x.Id == id);
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(TodoItem todoItem, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var item = _context.TodoItems.FirstOrDefault(x => x.Id == id);
                    item.Name = todoItem.Name;
                    item.Description = todoItem.Description;
                    _context.SaveChanges();
                    return RedirectToAction("Index", new { id = item.TodoId });
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View(todoItem);
        }
        public IActionResult Delete(int id)
        {
            var item = _context.TodoItems.Find(id);
            _context.TodoItems.Remove(item);
            _context.SaveChanges();
            return RedirectToAction("Index", new { id = item.TodoId });
        }
        public IActionResult UpdateStatus(int id)
        {
            var todo = _context.TodoItems.Find(id);          
            todo.Status = !todo.Status;
            _context.SaveChanges();
            return RedirectToAction("Index", new { id = todo.TodoId });
        }
    }
}