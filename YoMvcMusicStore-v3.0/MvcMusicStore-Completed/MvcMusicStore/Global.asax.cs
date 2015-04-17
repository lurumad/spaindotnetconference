using System;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcMusicStore.Configuration;
using MvcMusicStore.Models;
using MvcMusicStore.Properties;
using StackExchange.Profiling;

namespace MvcMusicStore
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Mappers.Configure();
            Database.SetInitializer(new SampleData());
            AreaRegistration.RegisterAllAreas();
            Filters.RegisterGlobal(GlobalFilters.Filters);
            Routes.Register(RouteTable.Routes);
            Engines.Register(ViewEngines.Engines);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var request = ((HttpApplication) sender).Request;

            if (request.Cookies[Strings.MiniProfilerCookie] != null)
            {
                MiniProfiler.Start();
            }
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            MiniProfiler.Stop();
        }
    }
}