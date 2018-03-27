using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using technical.test.Model;

namespace techical.test.Data.Contexts
{
    public class TechnicalTestContext: DbContext
    {
        public TechnicalTestContext()
            : base("DefaultConnection")
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserData> UserData { get; set; }
        public DbSet<Session> Sessions { get; set; }

        public static TechnicalTestContext Create()
        {
            return new TechnicalTestContext();
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<UserData>()
                .HasRequired(u => u.User)
                .WithOptional(u => u.UserData);

            modelBuilder.Entity<Session>()
                .HasRequired(u => u.User)
                .WithOptional(u => u.Session);                        
        }
    }
}
