using DAL.Interface.Entities;
using System.Data.Entity;
using ORM.Entity;
using DAL.Interface.Mappers;
using DAL.Interface.Repository;

namespace DAL.Repository
{
    public class UserRepository: Repository<DalUser, User>, IUserRepository
    {
        public UserRepository(DbContext context, IMapper<DalUser, User> mapper)
            : base(context, mapper) {}
    }
}
