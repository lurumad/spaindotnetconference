using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Features.StoreManager
{
    [Authorize(Roles = "Administrator")]
    public class StoreManagerController : System.Web.Mvc.Controller
    {
        private readonly MusicStoreDbContext _dbContext;

        public StoreManagerController(MusicStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ViewResult Index()
        {
            var albums = _dbContext.Albums.Include(a => a.Genre).Include(a => a.Artist);
            return View(albums.ToList());
        }

        public ViewResult Details(int id)
        {
            var album = _dbContext.Albums.Find(id);
            return View(album);
        }

        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(_dbContext.Genres, "GenreId", "Name");
            ViewBag.ArtistId = new SelectList(_dbContext.Artists, "ArtistId", "Name");
            return View();
        } 

        [HttpPost]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Albums.Add(album);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.GenreId = new SelectList(_dbContext.Genres, "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(_dbContext.Artists, "ArtistId", "Name", album.ArtistId);
            return View(album);
        }
        
        public ActionResult Edit(int id)
        {
            var album = _dbContext.Albums.Find(id);
            ViewBag.GenreId = new SelectList(_dbContext.Genres, "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(_dbContext.Artists, "ArtistId", "Name", album.ArtistId);
            return View(album);
        }

        [HttpPost]
        public ActionResult Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(album).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(_dbContext.Genres, "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(_dbContext.Artists, "ArtistId", "Name", album.ArtistId);
            return View(album);
        }

        public ActionResult Delete(int id)
        {
            var album = _dbContext.Albums.Find(id);
            return View(album);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var album = _dbContext.Albums.Find(id);

            _dbContext.Albums.Remove(album);
            _dbContext.SaveChanges();

            return this.RedirectToAction(c => c.Index());
        }
    }
}