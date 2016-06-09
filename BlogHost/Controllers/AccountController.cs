using BlogHost.Models;
using BlogHost.Providers;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogHost.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            string email = null;
            if (User.Identity.IsAuthenticated)
            {
                email = User.Identity.Name;
                using (var context = new BlogHostDbContext())
                {
                    var user = context.Users.Include("Role").Where(x => x.Email == email).FirstOrDefault();
                    if (user != null)
                        return View(user);
                }
            }
            return RedirectToAction("Login");
        }

        public ActionResult ForUser(int id)
        {
            using (var context = new BlogHostDbContext())
            {
                var user = context.Users.Include("Role").Where(x => x.UserId == id).FirstOrDefault();
                if (user != null)
                    return View("Index", user);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(user.Email, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    var username = Membership.GetUser(user.Email).UserName;
                    Session.Add("username", username);
                    //if (Url.IsLocalUrl(returnUrl))
                    ///{
                    //     return Redirect(returnUrl);
                    //}
                   // else
                   // {
                        return RedirectToAction("Index", "Home");
                   // }
                }
                else
                {
                    ModelState.AddModelError("login", "Wrong password or email");
                }
            }
            return View(user);           
        }

        [HttpGet]
        public ActionResult Registration()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationViewModel user)
        {

            if (ModelState.IsValid)
            {
                MembershipUser membershipUser = ((CustomMembershipProvider)Membership.Provider).CreateUser(user);
                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    Session.Add("username", membershipUser.UserName);
                    return View("Success");
                }
                else
                {
                    ModelState.AddModelError("", "Something bad happened during your registration. Please, try later.");
                }
            }
            return View(user);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }
    }
}