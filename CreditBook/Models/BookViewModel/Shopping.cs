using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace CreditBook.Models.BookViewModel
{
    public class Shopping
    {

        public int Id { get; set; }
        public decimal TotalFee { get; set; }
        public DateTime CreateDate { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
