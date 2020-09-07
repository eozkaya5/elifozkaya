using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models.Entites
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Description { get; set; }

        public List<TodoItem> TodoItems { get; set; }
    }
}
