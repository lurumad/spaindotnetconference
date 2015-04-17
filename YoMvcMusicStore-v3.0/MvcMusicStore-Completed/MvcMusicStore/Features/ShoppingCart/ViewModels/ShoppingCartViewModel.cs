using System.Collections.Generic;
using MvcMusicStore.Models;

namespace MvcMusicStore.Features.ShoppingCart.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }

        public decimal CartTotal { get; set; }
    }
}