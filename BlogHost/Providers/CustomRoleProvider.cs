using System;
using System.Web.Security;
using BLL.Interface.Services;
using System.Web.Mvc;
using BLL.Interface.Entities;

namespace BlogHost.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public IUserService UserService => System.Web.Mvc.DependencyResolver.Current.GetService<IUserService>();
        public IRoleService RoleService => System.Web.Mvc.DependencyResolver.Current.GetService<IRoleService>();
        public override bool IsUserInRole(string email, string roleName)
        {
            var user = UserService.GetUserEntity(email);

            return (user != null && user.Role.Name == roleName);
        }

        public override string[] GetRolesForUser(string email)
        {
            var roles = new string[] { };
            var user = UserService.GetUserEntity(email);

            if (user == null)
                return roles;

            var userRole = user.Role;

            if (userRole != null)
            {
                roles = new string[] { userRole.Name };
            }

            return roles;
        }

        public override void CreateRole(string roleName)
        {
            var role = new BllRole() { Name = roleName };

            RoleService.CreateRole(role);
        }

        #region Stubs
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

#endregion
    }
}