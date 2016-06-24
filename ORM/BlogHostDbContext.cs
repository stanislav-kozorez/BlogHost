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
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasMany(x => x.Users)
                .WithRequired(x => x.Role)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<User>()
                .HasMany(x => x.Articles)
                .WithRequired(x => x.Author)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Article>()
                .HasMany(x => x.Comments)
                .WithRequired(x => x.Article)
                .WillCascadeOnDelete(true);
                
        }
    }
}
