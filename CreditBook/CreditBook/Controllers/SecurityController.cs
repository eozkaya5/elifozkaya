using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using Identity.Models.Authentication;
using Identity.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class SecurityController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        public SecurityController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            ViewBag.UserName = User.Identity.Name;
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserModel user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    UserName = user.UserName,
                    Email = user.Email
                };
                IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Security");
                else
                    result.Errors.ToList().ForEach(x => ModelState.AddModelError(x.Code, x.Description));
            }
            return View("Index");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.FindByEmailAsync(login.Email);
                if (appUser != null)
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, login.Password, login.Persistent, login.Lock);
                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");
                    ModelState.AddModelError("NotUser2", "E-Posta veya şifre hatalı.");
                }
                else
                {
                    ModelState.AddModelError("NotUser", "Böyle bir kullanıcı bulunmamaktadır.");
                }
            }
            return View("Index");
        }


        public IActionResult PasswordReset()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>PasswordReset(ResetPasswordModel reset)
        {
            AppUser user = await _userManager.FindByEmailAsync(reset.Email);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.To.Add(user.Email);
                mail.From = new MailAddress("******@gmail.com", "Şifre Güncelleme", System.Text.Encoding.UTF8);
                mail.Subject = "Şifre Güncelleme Talebi";
                mail.Body = $"<a target=\"_blank\" href=\"https://localhost:5001{Url.Action("UpdatePassword", "Security", new { Id = user.Id, token = HttpUtility.UrlEncode(resetToken) })}\">Yeni şifre talebi için tıklayınız</a>";
                mail.IsBodyHtml = true;
                SmtpClient smp = new SmtpClient();
                smp.Credentials = new NetworkCredential("Email", "Password");
                smp.Port = 587;
                smp.Host = "smtp.gmail.com";
                smp.EnableSsl = true;
                smp.Send(mail);

                ViewBag.State = true;
            }
            else
                ViewBag.State = false;

            return View();
        }

        [HttpGet("[action]/{Id}/{token}")]
        public IActionResult UpdatePassword()
        {
            return View();
        }
        [HttpPost("[action]/{Id}/{token}")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordModel update, string id, string token)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            IdentityResult result = await _userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(token), update.Password);
            if (result.Succeeded)
            {
                ViewBag.State = true;
                await _userManager.UpdateSecurityStampAsync(user);
                return RedirectToAction("Index", "Security");
            }
            else
                ViewBag.State = false;
            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Security");
        }

    }
}

