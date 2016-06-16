using DAL.Interface.Entities;
using DAL.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using DAL.Interface.Mappers;
using DAL.Mappers;

namespace DAL.Repository
{
    public class Repository<TDal, TOrm> : IRepository<TDal> 
        where TDal : IEntity 
        where TOrm: class
    {
        protected DbContext Context { get; private set; }
        protected IMapper<TDal, TOrm> Mapper { get; private set; }

        public Repository(DbContext context, IMapper<TDal, TOrm> mapper)
        {
            this.Context = context;
            this.Mapper = mapper;
        }
        public void Create(TDal entity)
        {
            if (entity == null)
                throw new NullReferenceException(nameof(entity));
            Context.Set<TOrm>().Add(Mapper.ToOrm(entity));
        }

        public void Delete(TDal entity)
        {
            if (entity == null)
                throw new NullReferenceException(nameof(entity));
            var foundEntity = Context.Set<TOrm>().Find(entity.Id);
            if (foundEntity == null)
                throw new ArgumentException($"The entity with id = {entity.Id} doesn't exist");
            Context.Set<TOrm>().Remove(foundEntity);
        }

        public IEnumerable<TDal> GetAll()
        {
            return Context.Set<TOrm>().ToList().Select(x => Mapper.ToDal(x));
        }

        public TDal GetById(int id)
        {
            var ormEntity = Context.Set<TOrm>().Find(id);
            if (ormEntity == null)
                throw new ArgumentException($"The entity with id = {id} doesn't exist");

            return Mapper.ToDal(ormEntity);
        }

        public TDal GetByPredicate(Expression<Func<TDal, bool>> predicate)
        {
            var result = Context.Set<TOrm>().Where(predicate.Map<TDal, TOrm, bool>()).FirstOrDefault();
            if(result == null)
                throw new ArgumentException($"The entity wasn't found by predicate = {predicate.ToString()}");

            return Mapper.ToDal(result); 
        }

        public void Update(TDal entity)
        {
            var ormEntity = Context.Set<TOrm>().Find(entity.Id);
            if (ormEntity == null)
                throw new ArgumentException($"The entity with id = {entity.Id} doesn't exist");
            /// complete user update
        }
    }
}
