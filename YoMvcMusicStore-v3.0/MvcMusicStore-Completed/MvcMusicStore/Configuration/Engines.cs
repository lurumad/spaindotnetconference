using System.Web.Mvc;
using MvcMusicStore.Mvc.ViewEngines;
using StackExchange.Profiling.Mvc;

namespace MvcMusicStore.Configuration
{
    public class Engines
    {
        public static void Register(ViewEngineCollection engines)
        {
            var featuresViewEngine = new FeaturesRazorViewEngine();
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ProfilingViewEngine(featuresViewEngine));
        }
    }
}