using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditBook.Models.BookViewModel
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal FeePaid { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
       
        public Customer Customer { get; set; }
    }
}
