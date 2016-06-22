using System.Web.Mvc;
using BlogHost.Models;
using System.Web.Helpers;
using BLL.Interface.Services;
using BLL.Interface.Entities;
using System.Web;

namespace BlogHost.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;

        public UserController(IUserService userService, IRoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }

        public ActionResult Index(int page = 1)
        {
            int pageSize = 3;

            var model = new EntityListViewModel<BllUser>();
            var users = userService.GetPagedUsers(page, pageSize);
            
            model.Items = users;
            model.PagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = userService.GetUserCount()
            };
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                throw new HttpException(404, "Not found");

            var user = userService.GetUserEntity(id.Value);
            if (user != null)
            {
                if (user.Email == User.Identity.Name)
                    throw new HttpException(404, "Not found");
                var editUser = new UserEditViewModel();
                editUser.UserId = user.UserId;
                editUser.Name = user.Name;
                editUser.Email = user.Email;
                editUser.RoleId = user.Role.RoleId;
                editUser.Roles = new SelectList(roleService.GetAllRoles(), "RoleId", "Name");

                return View(editUser);
            }
            throw new HttpException(404, "Not found");
        }

        [HttpPost]
        public ActionResult Edit(UserEditViewModel editUser)
        {
            if (ModelState.IsValid)
            {
                var user = userService.GetUserEntity(editUser.UserId);
                if(user != null)
                {
                    user.Name = editUser.Name;
                    if (editUser.Password != null && !Crypto.VerifyHashedPassword(user.Password, editUser.Password))
                        user.Password = Crypto.HashPassword(editUser.Password);
                    user.Role = roleService.GetRole(editUser.RoleId);
                    userService.UpdateUser(user);

                    return RedirectToAction("Index");
                }
                throw new HttpException(404, "Not Found");
            }
            editUser.Roles = new SelectList(roleService.GetAllRoles(), "RoleId", "Name");
            return View(editUser);
        }

        [ActionName("Delete")]
        public ActionResult ConfirmUserDelete(int id)
        {
            var user = userService.GetUserEntity(id);

            if (user != null)
            {
                if (user.Email == User.Identity.Name)
                    throw new HttpException(404, "Not found");
                var deleteUser = new UserDeleteViewModel() { Email = user.Email, UserId = user.UserId };
                return View(deleteUser);
            }
            throw new HttpException(404, "Not foud");
        }

        [HttpPost]
        public ActionResult Delete(int userId)
        {
            var user = userService.GetUserEntity(userId);
            if (user != null)
            {
                userService.DeleteUser(user);
                return RedirectToAction("Index");
            }
            throw new HttpException(404, "Not found");
        }
    }
}