using BLL.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using DAL.Interface.Repository;
using BLL.Mappers;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;

        public UserService(IUnitOfWork uow, IUserRepository repository)
        {
            this.uow = uow;
            this.userRepository = repository;
        }
        public void CreateUser(BllUser user)
        {
            userRepository.Create(user.ToDalUser());
            uow.Commit();
        }

        public void DeleteUser(BllUser user)
        {
            userRepository.Delete(user.ToDalUser());
            uow.Commit();
        }

        public IEnumerable<BllUser> GetAllUserEntities()
        {
            return userRepository.GetAll().Select(x => x.ToBllUser());
        }

        public BllUser GetUserEntity(string email)
        {
            return userRepository.GetByPredicate(x => x.Email == email).ToBllUser();
        }

        public BllUser GetUserEntity(int id)
        {
            return userRepository.GetById(id).ToBllUser();
        }

        public void UpdateUser(BllUser user)
        {
            userRepository.Update(user.ToDalUser());
        }
    }
}
