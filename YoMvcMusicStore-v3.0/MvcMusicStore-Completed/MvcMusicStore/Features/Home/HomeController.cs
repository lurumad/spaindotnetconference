using System.Threading.Tasks;
using System.Web.Mvc;
using MvcMusicStore.Features.Home.Requests;
using MvcMusicStore.Mediatr;

namespace MvcMusicStore.Features.Home
{
    public class HomeController : Controller
    {
        private const int MaximunResults = 5;
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> Index()
        {
            var request = new TopSellingAlbumsRequest(MaximunResults);

            var topSellingAlbums = await _mediator.SendAsync(request);

            return View(topSellingAlbums);
        }
    }
}