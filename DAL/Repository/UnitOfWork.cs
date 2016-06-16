using DAL.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        bool disposed = false;
        public DbContext Context { get; private set; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        public void Commit()
        {
            if (Context != null)
            {
                Context.SaveChanges();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (disposed)
                throw new ObjectDisposedException(nameof(Context));
            if (disposing)
                GC.SuppressFinalize(this);
            if (Context != null)
                Context.Dispose();
            disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
