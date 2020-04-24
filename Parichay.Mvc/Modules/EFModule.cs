using Autofac;
using Parichay.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
namespace Parichay.Mvc.Modules
{
    [System.Serializable]
    public class EFModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(ParichayContext)).As(typeof(IContext)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(ParichaySpContext)).As(typeof(ISPContext)).InstancePerLifetimeScope();
        }
    }
}