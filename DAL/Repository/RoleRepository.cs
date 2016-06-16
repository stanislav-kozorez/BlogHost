using DAL.Interface.Entities;
using DAL.Interface.Mappers;
using DAL.Interface.Repository;
using ORM.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class RoleRepository : Repository<DalRole, Role>, IRoleRepository
    {
        public RoleRepository(DbContext context, IMapper<DalRole, Role> mapper)
            : base(context, mapper)
        { }
    }
}
