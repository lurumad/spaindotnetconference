using System.Web;
using System.Web.Mvc;
using Humanizer;
using MvcMusicStore.Properties;
using Microsoft.Web.Mvc;
using MvcMusicStore.Features.Home;

namespace MvcMusicStore.Features.MiniProfiler
{
    public class MiniProfilerController : System.Web.Mvc.Controller
    {
        public virtual ActionResult On()
        {
            var cookie = new HttpCookie(Strings.MiniProfilerCookie, Strings.MiniProfilerCookieOn)
            {
                Expires = In.One.Day
            };

            Response.Cookies.Add(cookie);

            return this.RedirectToAction<HomeController>(c => c.Index());
        }

        public virtual ActionResult Off()
        {
            var cookie = new HttpCookie(Strings.MiniProfilerCookie)
            {
                Expires = In.One.Second
            };

            Response.Cookies.Add(cookie);

            return this.RedirectToAction<HomeController>(c => c.Index());
        }
    }
}