using DBase.Abstact;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBase.Entity
{
    public class Context : DbContext, IDbContext
    {
        public Context() : base("stringConnection")
        {
            Database.SetInitializer<Context>(null);
        }
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<Role> Roles { get; set; }


    }
    //public class MyTestContext : IDbContext
    //{
    //    public void Dispose()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public int SaveChanges()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IDbSet<TEntity> Set<TEntity>() where TEntity : class
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
