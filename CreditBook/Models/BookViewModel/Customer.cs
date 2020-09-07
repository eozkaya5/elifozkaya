using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CreditBook.Models.BookViewModel
{
    public class Customer
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }

        private decimal _totalDept;
        public decimal TotalDept
        {
            get
            {
                return Shoppings.Sum(x => x.TotalFee) - Payments.Sum(x => x.FeePaid);
            }
            set
            {
                _totalDept= value;
            }
        }
        private decimal _totalPayment;
        public decimal TotalPayment
        {
            get
            {
                return Payments.Sum(x => x.FeePaid);
            }
            set
            {
                _totalPayment = value;
            }
        }
        private decimal _totalShopping;
        public decimal TotalShopping
        {
            get
            {
                return Shoppings.Sum(x => x.TotalFee);
            }
            set
            {
                _totalShopping = value;
            }
        }
        private decimal _total;
        public decimal Total
        {
            get
            {
                return Payments.Sum(x => x.Customer.TotalPayment);
            }
            set
            {
                _total = value;
            }
        }


        public int UserId { get; set; }

        public List<Payment> Payments { get; set; }
        public List<Shopping> Shoppings { get; set; }
    }
}
