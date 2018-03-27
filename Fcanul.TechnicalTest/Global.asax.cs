using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Fcanul.TechnicalTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            System.Data.Entity.Database.SetInitializer(new
                MigrateDatabaseToLatestVersion<techical.test.Data.Contexts.TechnicalTestContext,
                    techical.test.Data.Migrations.Configuration>());

            using (var context = new techical.test.Data.Contexts.TechnicalTestContext())
            {
                context.Database.Initialize(false);
            }

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
