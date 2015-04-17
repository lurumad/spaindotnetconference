using System.Web.Mvc;
using StackExchange.Profiling.Mvc;

namespace MvcMusicStore.Configuration
{
    public static class Filters
    {
        public static void RegisterGlobal(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ProfilingActionFilter());
        }
    }
}