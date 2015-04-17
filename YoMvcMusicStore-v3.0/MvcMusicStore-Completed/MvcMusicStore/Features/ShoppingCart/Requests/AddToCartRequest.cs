using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcMusicStore.Mediatr;

namespace MvcMusicStore.Features.ShoppingCart.Requests
{
    public class AddToCartRequest : IAsyncRequest<Unit>
    {
        public int Id { get; private set; }

        public HttpContextBase ContextBase { get; private set; }

        public AddToCartRequest(int id, HttpContextBase contextBase)
        {
            Id = id;
            ContextBase = contextBase;
        }
    }
}