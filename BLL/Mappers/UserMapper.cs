using BLL.Interface.Entities;
using DAL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class UserMapper
    {
        public static DalUser ToDalUser(this BllUser user)
        {

            return new DalUser()
            {
                Id = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                CreationDate = user.CreationDate,
                Role = user.Role?.ToDalRole()
            };
        }

        public static BllUser ToBllUser(this DalUser user)
        {
            return new BllUser()
            {
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                CreationDate = user.CreationDate,
                Role = user.Role?.ToBllRole()
            };
        }
    }
}
