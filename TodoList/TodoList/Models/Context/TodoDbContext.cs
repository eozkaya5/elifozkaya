using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Models.Entites;


namespace TodoList.Models.Context
{
    public class TodoDbContext :DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        { }

        public DbSet<TodoItem> TodoItems { get; set; }
        //public DbSet<Status> Statuses { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public IEnumerable<SelectListItem> TodoId { get; set; }
    }
}
