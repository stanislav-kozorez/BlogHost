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
        public ActionResult Index(int page = 1)
        {
            using (var context = new BlogHostDbContext())
            {
                int pageSize = 3;
                var model = new EntityListViewModel<User>();
                var ormUsers = context.Users.Include("Role").OrderBy(x => x.UserId).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                //List<User> users = context.Users.Include("Role").ToList();
                model.Items = ormUsers;
                model.PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = context.Users.Count()
                };
                return View(model);
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
                    if (ormUser.Email == User.Identity.Name)
                        return RedirectToAction("Index");
                    var editUser = new UserEditViewModel();
                    editUser.UserId = ormUser.UserId;
                    editUser.Name = ormUser.Name;
                    editUser.Email = ormUser.Email;
                    editUser.RoleId = ormUser.Role.RoleId;
                    editUser.Roles = new SelectList(context.Roles.ToList(), "RoleId", "Name");

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
                    if (ormUser.Email == User.Identity.Name)
                        return RedirectToAction("Index");
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