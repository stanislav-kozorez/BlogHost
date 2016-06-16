using BLL.Interface.Entities;
using DAL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class RoleMapper
    {
        public static DalRole ToDalRole(this BllRole role)
        {
            return new DalRole()
            {
                Id = role.RoleId,
                Name = role.Name
            };
        }

        public static BllRole ToBllRole(this DalRole role)
        {
            return new BllRole()
            {
                RoleId = role.Id,
                Name = role.Name
            };
        }
    }
}
