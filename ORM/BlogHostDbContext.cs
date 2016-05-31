using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.Entity;

namespace ORM
{
    public class BlogHostDbContext: DbContext
    {
        public BlogHostDbContext()
            : base("name=BlogHostDb")
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
