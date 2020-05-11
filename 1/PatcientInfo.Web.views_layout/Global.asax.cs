using PatcientInfo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;

namespace PatcientInfo.Web.views_layout
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            StaticDataContext.Directory = HostingEnvironment.MapPath("~/files");
            //StaticDataContext.Load();
            StaticDataContext.CreateTestingData();
        }

    }
}
