using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models.Entites
{
    public class TodoItem
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public bool Status { get; set; }  
        public int TodoId { get; set; }
      
        public  Todo Todo { get; set; }
        //public IEnumerable<SelectListItem> ItemList { get; set; }
    }
}
