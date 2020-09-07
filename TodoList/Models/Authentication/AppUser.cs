using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models.Authentication
{
    public class AppUser:IdentityUser<int>
    {       
        public string Memleket { get; set; }
        public bool Cinsiyet { get; set; }    
    }
}

