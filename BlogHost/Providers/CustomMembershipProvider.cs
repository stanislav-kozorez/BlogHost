using System;
using System.Web.Helpers;
using System.Web.Security;
using BlogHost.Models;
using BLL.Interface.Services;
using BLL.Interface.Entities;
using System.Web.Mvc;

namespace BlogHost.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {
        public IUserService UserService => System.Web.Mvc.DependencyResolver.Current.GetService<IUserService>();
        public IRoleService RoleService => System.Web.Mvc.DependencyResolver.Current.GetService<IRoleService>();
        public override bool ValidateUser(string username, string password)
        {
            var user = UserService.GetUserEntity(username);

            return (user != null && Crypto.VerifyHashedPassword(user.Password, password));
        }

        public MembershipUser CreateUser(RegistrationViewModel regViewModel)
        {
            MembershipUser membershipUser = GetUser(regViewModel.Email, false);

            if (membershipUser != null)
                return null;

            var user = new BllUser();

            user.Name = regViewModel.Name;
            user.Email = regViewModel.Email;
            user.Password = Crypto.HashPassword(regViewModel.Password);
            user.CreationDate = DateTime.Now;

            var role = RoleService.GetRole("User");

            user.Role = role;

            UserService.CreateUser(user);

            membershipUser = GetUser(user.Email, false);

            return membershipUser;
        }

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            var user = UserService.GetUserEntity(email);
            if (user == null)
                return null;

            return new MembershipUser("CustomMembershipProvider", user.Name,
                    null, user.Email, null, null,
                    false, false, user.CreationDate,
                    DateTime.MinValue, DateTime.MinValue,
                    DateTime.MinValue, DateTime.MinValue);
        }

        #region Stubs
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

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

        public override bool EnablePasswordReset
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}