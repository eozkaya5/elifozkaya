using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.CustomValidations
{

    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string UserName) => new IdentityError { Code = "DuplicateUserName", Description = $"\"{ UserName }\" kullanıcı adı kullanılmaktadır." };
        public override IdentityError InvalidUserName(string UserName) => new IdentityError { Code = "InvalidUserName", Description = "Geçersiz kullanıcı adı." };
        public override IdentityError DuplicateEmail(string Email) => new IdentityError { Code = "DuplicateEmail", Description = $"\"{ Email }\" başka bir kullanıcı tarafından kullanılmaktadır." };
        public override IdentityError InvalidEmail(string Email) => new IdentityError { Code = "InvalidEmail", Description = "Geçersiz email." };
    }
}

