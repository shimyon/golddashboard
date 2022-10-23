using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GoldDashboard
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_BeginRequest()
        {
            if (HttpContext.Current.Request.IsLocal)
            {
                //if (Context.Request.IsSecureConnection)
                //    Response.Redirect(Context.Request.Url.ToString().Replace("https://localhost:44310/", "http://localhost:44310/"));
            }
            else
            {
                //Response.Redirect(Context.Request.Url.ToString().Replace("https://", "http://"));
            }
        }

    }
}
