using System.Web.Optimization;

namespace MvcMusicStore.Configuration
{
    public static class Bundles
    {
        public static void Register(BundleCollection bundles)
        {
            var vendorBundle = new ScriptBundle("~/bundles/vendor")
                .Include("~/bower_components/jquery/dist/jquery.js");

            bundles.Add(vendorBundle);
        }
    }
}