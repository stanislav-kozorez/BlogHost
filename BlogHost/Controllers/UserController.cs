using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ORM.Entity;
using ORM;
using System.Collections.Generic;
using BlogHost.Models;
using System.Web.Helpers;

namespace BlogHost.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            using (var context = new BlogHostDbContext())
            {
                List<User> users = context.Users.Include("Role").ToList();
                return View(users);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            using (var context = new BlogHostDbContext())
            {
                var ormUser = context.Users.Find(id);
                if (ormUser != null)
                {
                    var editUser = new UserEditViewModel();
                    editUser.UserId = ormUser.UserId;
                    editUser.Name = ormUser.Name;
                    editUser.Email = ormUser.Email;
                    editUser.RoleId = ormUser.Role.RoleId;
                    editUser.Roles = new SelectList(context.Roles.ToList(), "RoleId", "Name");

                    //ViewBag.Roles = new SelectList(context.Roles.ToList(), "RoleId", "Name", ormUser.Role.RoleId);

                    //ViewBag.Roles = context.Roles.ToList().Select(x => new SelectListItem() {Text = x.Name, Value = x.RoleId.ToString(), Selected = x.RoleId == ormUser.Role.RoleId });
                    return View(editUser);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(UserEditViewModel editUser)
        {
            if (ModelState.IsValid)
            {
                using (var context = new BlogHostDbContext())
                {
                    var ormUser = context.Users.Find(editUser.UserId);
                    if (ormUser != null)
                    {
                        ormUser.Name = editUser.Name;
                        if (editUser.Password != null && !Crypto.VerifyHashedPassword(ormUser.Password, editUser.Password))
                            ormUser.Password = Crypto.HashPassword(editUser.Password);
                        ormUser.Role = context.Roles.Find(editUser.RoleId);
                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(editUser);
        }

        [ActionName("Delete")]
        public ActionResult ConfirmUserDelete(int id)
        {
            using (var context = new BlogHostDbContext())
            {
                var ormUser = context.Users.Find(id);
                if (ormUser != null)
                {
                    var deleteUser = new UserDeleteViewModel() { Email = ormUser.Email, UserId = ormUser.UserId };
                    return View(deleteUser);
                }

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int userId)
        {
            using (var context = new BlogHostDbContext())
            {
                var user = context.Users.Find(userId);
                if (user != null)
                    context.Users.Remove(user);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}