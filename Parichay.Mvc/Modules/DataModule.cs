using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
namespace Parichay.Mvc.Modules
{
    [System.Serializable]
    public class DataModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("Parichay.Data"))
                      .Where(t => t.Name.EndsWith("Data"))
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope();
        }
    }
}