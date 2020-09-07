using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models.ViewModel
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage ="Lütfen E-posta adresini boş geçmeyiniz.")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Lütfen uygun formatta e-posta giriniz.")]
        [Display(Name ="E-posta adresiniz.")]
        public string Email { get; set; }
    }
}
