using System.Web.Mvc;

namespace MvcMusicStore.Mvc.ViewEngines
{
    public class FeaturesRazorViewEngine : RazorViewEngine
    {
        public FeaturesRazorViewEngine()
        {
            // {0} ActionName
            // {1} ControllerName
            // {2} AreaName

            var featureFolderAreaViewLocationFormats = new[]
            {
                "~/Areas/{2}/Features/{1}/{0}.cshtml",
                "~/Areas/{2}/Features/Shared/{0}.cshtml",
            };

            var featureFolderAreaPartialViewLocationFormats = new[]
            {
                "~/Areas/{2}/Features/{1}/PartialViews/{0}.cshtml",
                "~/Areas/{2}/Features/Shared/PartialViews/{0}.cshtml",
            };

            AreaViewLocationFormats = featureFolderAreaViewLocationFormats;
            AreaMasterLocationFormats = featureFolderAreaViewLocationFormats;
            AreaPartialViewLocationFormats = featureFolderAreaPartialViewLocationFormats;

            var featureFolderViewLocationFormats = new[]
            {
                "~/Features/{1}/{0}.cshtml",
                "~/Features/Shared/{0}.cshtml"
            };

            var featureFolderPartialViewLocationFormats = new[]
            {
                "~/Features/{1}/PartialViews/{0}.cshtml",
                "~/Features/Shared/PartialViews/{0}.cshtml"
            };

            ViewLocationFormats = featureFolderViewLocationFormats;
            MasterLocationFormats = featureFolderViewLocationFormats;
            PartialViewLocationFormats = featureFolderPartialViewLocationFormats;
        }
    }
}