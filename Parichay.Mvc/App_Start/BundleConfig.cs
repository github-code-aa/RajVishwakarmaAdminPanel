using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Parichay.Mvc.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
           

            bundles.Add(new ScriptBundle("~/bundles/UserJs/JqueyValidate").Include(
                            "~/Scripts/jquery.validate.min.js",
                            "~/Scripts/jquery.validate.unobtrusive.min.js"

            ));

            bundles.Add(new ScriptBundle("~/bundles/UserJs/MainScript").Include(
                         "~/assets/scripts/main.87c0748b313a1dda75f5.js"

            ));

            bundles.Add(new ScriptBundle("~/bundles/UserJs/ValidationScripts").Include(
                 "~/Scripts/JqueryValidate/jquery.validate.js",
                "~/Scripts/JqueryValidate/jquery.validate.min.js",
                 "~/Scripts/JqueryValidate/additional-methods.js"

              ));   


            BundleTable.EnableOptimizations = true;
        }
    }
}