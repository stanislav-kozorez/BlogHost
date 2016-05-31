using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ORM;
using ORM.Entity;

namespace BlogHost.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override bool IsUserInRole(string email, string roleName)
        {
            using (var context = new BlogHostDbContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Email == email);

                return (user != null && user.Role != null && user.Role.Name == roleName);
            }
        }

        public override string[] GetRolesForUser(string email)
        {
            using (var context = new BlogHostDbContext())
            {
                var roles = new string[] { };
                var user = context.Users.FirstOrDefault(u => u.Email == email);

                if (user == null)
                    return roles;

                var userRole = user.Role;

                if (userRole != null)
                {
                    roles = new string[] { userRole.Name };
                }
                return roles;
            }
        }

        public override void CreateRole(string roleName)
        {
            var newRole = new Role() { Name = roleName };
            using (var context = new BlogHostDbContext())
            {
                context.Roles.Add(newRole);
                context.SaveChanges();
            }
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