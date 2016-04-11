using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MVCHomeWork.Models;
using System.Web.Security;
using MVCHomeWork.Areas.HomeWork.Models;

namespace MVCHomeWork.Controllers
{
    // [AllowAnonymous]
    public class AccountController : Controller
    {
        protected BaseUserRepository userRepo = RepositoryHelper.GetBaseUserRepository();

        public ActionResult Login()
        {
            Session.RemoveAll();
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel data)
        {
            var role = ValidateLogin(data.Email, data.Password);
            if (!string.IsNullOrEmpty(role))
            {
                // 將管理者登入的 Cookie 設定成 Session Cookie
                bool isPersistent = false;

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                  data.Email,
                  DateTime.Now,
                  DateTime.Now.AddMinutes(30),
                  isPersistent,
                  role,
                  FormsAuthentication.FormsCookiePath);

                string encTicket = FormsAuthentication.Encrypt(ticket);

                // Create the cookie.
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                //Response.Redirect("~/Members/", true);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        private string ValidateLogin(string email, string password)
        {
            string strPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");

            return userRepo.ValidateLogin(email, strPassword);
        }

        private bool CheckLogin(LoginViewModel data)
        {
            return (data.Email == "doggy.huang@gmail.com" && data.Password == "123");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel data)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult EditProfile()
        {
            return View();
        }

        //[HttpPost]
        //[Authorize]
        //public ActionResult EditProfile(EditProfileViewModel data)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    return View();
        //}
    }
}