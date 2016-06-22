using DAL.Interface.Entities;
using System.Data.Entity;
using ORM.Entity;
using DAL.Interface.Mappers;
using DAL.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repository
{
    public class UserRepository: Repository<DalUser, User>, IUserRepository
    {
        public UserRepository(DbContext context, IMapper<DalUser, User> mapper)
            : base(context, mapper) {}

        public override void Create(DalUser entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var user = Mapper.ToOrm(entity);
            var role = Context.Set<Role>().Find(entity.Role.Id);
            user.Role = role;
            Context.Set<User>().Add(user);
        }

        public override void Update(DalUser entity)
        {
            var user = Context.Set<User>().Find(entity.Id);
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            user.Name = entity.Name;
            user.Password = entity.Password;
            var role = Context.Set<Role>().Find(entity.Role.Id);
            user.Role = role;
        }

        public IEnumerable<DalUser> GetPagedUsers(int page, int pageSize)
        {
            var ormUsers = Context.Set<User>().Include("Role").OrderBy(x => x.UserId).Skip((page - 1) * pageSize).Take(pageSize).ToArray();
            return ormUsers.Select(x => Mapper.ToDal(x));
        }
    }
}
