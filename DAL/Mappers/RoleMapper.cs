using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Mappers;
using DAL.Interface.Entities;
using ORM.Entity;

namespace DAL.Mappers
{
    public class RoleMapper : IMapper<DalRole, Role>
    {
        public DalRole ToDal(Role entity)
        {
            return new DalRole()
            {
                Id = entity.RoleId,
                Name = entity.Name
            };
        }

        public Role ToOrm(DalRole entity)
        {
            return new Role()
            {
                RoleId = entity.Id,
                Name = entity.Name
            };
        }
    }
}
