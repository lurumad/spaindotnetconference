using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using MvcMusicStore.Features.ShoppingCart.Requests;
using MvcMusicStore.Features.ShoppingCart.ViewModels;
using MvcMusicStore.Mediatr;
using MvcMusicStore.Models;

namespace MvcMusicStore.Features.ShoppingCart
{
    public class ShoppingCartController : Controller
    {
        private readonly IMediator _mediator;
        private readonly MusicStoreDbContext _dbContext;

        public ShoppingCartController(
            MusicStoreDbContext dbContext,
            IMediator mediator)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }

            _dbContext = dbContext;
            _mediator = mediator;
        }

        public ActionResult Index()
        {
            var cart = Models.ShoppingCart.GetCart(HttpContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(viewModel);
        }

        public async Task<ActionResult> AddToCart(int id)
        {
            var request = new AddToCartRequest(id, HttpContext);

            await _mediator.SendAsync(request);

            return this.RedirectToAction(c => c.Index());
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = Models.ShoppingCart.GetCart(HttpContext);

            var albumName = _dbContext.Carts
                .Single(item => item.RecordId == id).Album.Title;

            var itemCount = cart.RemoveFromCart(id);

            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(albumName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }


        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = Models.ShoppingCart.GetCart(HttpContext);

            ViewData["CartCount"] = cart.GetCount();

            return PartialView("CartSummary");
        }
    }
}