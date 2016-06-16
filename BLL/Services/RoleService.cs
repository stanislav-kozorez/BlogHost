using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using DAL.Interface.Repository;
using BLL.Mappers;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork uow;
        private readonly IRoleRepository roleRepository;

        public RoleService(IUnitOfWork uow, IRoleRepository repository)
        {
            this.uow = uow;
            this.roleRepository = repository;
        }

        public IEnumerable<BllRole> GetAllRoles()
        {
            return roleRepository.GetAll().Select(x => x.ToBllRole());
        }

        public BllRole GetRole(string name)
        {
            return roleRepository.GetByPredicate(x => x.Name == name)?.ToBllRole();
        }

        public BllRole GetRole(int id)
        {
            return roleRepository.GetById(id)?.ToBllRole();
        }
    }
}
