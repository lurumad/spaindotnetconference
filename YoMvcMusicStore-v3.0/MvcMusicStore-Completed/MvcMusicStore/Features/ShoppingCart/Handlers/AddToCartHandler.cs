using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using MvcMusicStore.Features.ShoppingCart.Requests;
using MvcMusicStore.Mediatr;
using MvcMusicStore.Models;

namespace MvcMusicStore.Features.ShoppingCart.Handlers
{
    public class AddToCartHandler : IAsyncRequestHandler<AddToCartRequest, Unit>
    {
        private readonly MusicStoreDbContext _dbContext;

        public AddToCartHandler(MusicStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddToCartRequest message)
        {
            var addedAlbum = await _dbContext.Albums
                .SingleAsync(album => album.AlbumId == message.Id);

            var cart = Models.ShoppingCart.GetCart(message.ContextBase);
            
            cart.AddToCart(addedAlbum);

            return Unit.Value;
        }
    }
}