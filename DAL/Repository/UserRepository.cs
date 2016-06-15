using DAL.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Entities;
using System.Linq.Expressions;
using System.Data.Entity;
using ORM.Entity;
using DAL.Mappers;

namespace DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext context;

        public UserRepository(DbContext context)
        {
            this.context = context;
        }
        public void Create(DalUser entity)
        {
            if (entity == null)
                throw new NullReferenceException(nameof(entity));
            context.Set<User>().Add(entity.ToOrmUser());
        }

        public void Delete(DalUser entity)
        {
            if (entity == null)
                throw new NullReferenceException(nameof(entity));
            var user = context.Set<User>().Find(entity.Id);
            if (user == null)
                throw new ArgumentException($"The entity with id = {entity.Id} doesn't exist");
            context.Set<User>().Remove(user);
        }

        public IEnumerable<DalUser> GetAll()
        {
            return context.Set<User>().ToList().Select(x => x.ToDalUser());
        }

        public DalUser GetById(int id)
        {
            return context.Set<User>().Find(id)?.ToDalUser();
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> predicate)
        {
            
            context.Set<User>().Where(predicate);
        }

        public void Update(DalUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
