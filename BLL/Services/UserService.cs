using BLL.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        public void CreateUser(BllUser user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(BllUser user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BllUser> GetAllUserEntities()
        {
            throw new NotImplementedException();
        }

        public BllUser GetUserEntity(string email)
        {
            throw new NotImplementedException();
        }

        public BllUser GetUserEntity(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(BllUser user)
        {
            throw new NotImplementedException();
        }
    }
}
