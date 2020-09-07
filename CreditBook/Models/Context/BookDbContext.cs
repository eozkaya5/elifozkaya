using CreditBook.Models.BookViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditBook.Models.Context
{
    public class BookDbContext:DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext>options):base(options)
        { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Shopping> Shoppings { get; set; }

    }
   
}
