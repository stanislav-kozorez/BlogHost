using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface IRoleService
    {
        void CreateRole(BllRole role);
        BllRole GetRole(int id);
        BllRole GetRole(string name);
        IEnumerable<BllRole> GetAllRoles();
    }
}
