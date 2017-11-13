using Autofac;
using BLL.Abstarct;
using BLL.Concrete;
using DBase.Abstact;
using DBase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Core.DI
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new Context()).As<IDbContext>().InstancePerDependency();
            builder.RegisterType<UserProvider>().As<IUserProvider>().InstancePerDependency();
            base.Load(builder);
        }
    }
}