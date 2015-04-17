using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Features.Store
{
    public class StoreController : Controller
    {
        private readonly MusicStoreDbContext _dbContext;

        public StoreController(MusicStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Index()
        {
            var genres = _dbContext.Genres.ToList();

            return View(genres);
        }

        public ActionResult Browse(string genre)
        {
            var genreModel = _dbContext.Genres.Include("Albums")
                .Single(g => g.Name == genre);

            return View(genreModel);
        }

        public ActionResult Details(int id)
        {
            var album = _dbContext.Albums.Find(id);

            return View(album);
        }

        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            var genres = _dbContext.Genres.ToList();

            return PartialView(genres);
        }
    }
}