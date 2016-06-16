using DAL.Interface.Entities;
using System.Data.Entity;
using ORM.Entity;
using DAL.Interface.Mappers;
using DAL.Interface.Repository;
using System;

namespace DAL.Repository
{
    public class UserRepository: Repository<DalUser, User>, IUserRepository
    {
        public UserRepository(DbContext context, IMapper<DalUser, User> mapper)
            : base(context, mapper) {}

        public override void Create(DalUser entity)
        {
            if (entity == null)
                throw new NullReferenceException(nameof(entity));

            var user = Mapper.ToOrm(entity);
            var role = Context.Set<Role>().Find(entity.Role.Id);
            user.Role = role;
            Context.Set<User>().Add(user);
        }
    }
}
