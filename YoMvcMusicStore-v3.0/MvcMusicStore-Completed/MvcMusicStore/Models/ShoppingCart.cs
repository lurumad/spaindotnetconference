using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore.Models
{
    public class ShoppingCart
    {
        readonly MusicStoreDbContext _storeDb = new MusicStoreDbContext();

        string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Album album)
        {
            var cartItem = _storeDb.Carts.SingleOrDefault(
                    c => c.CartId == ShoppingCartId && 
                    c.AlbumId == album.AlbumId);

            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    AlbumId = album.AlbumId,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                _storeDb.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }

            _storeDb.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            var cartItem = _storeDb.Carts.Single(
                    cart => cart.CartId == ShoppingCartId && 
                            cart.RecordId == id);

            var itemCount = 0;

            if (cartItem == null)
            {
                return itemCount;
            }

            if (cartItem.Count > 1)
            {
                cartItem.Count--;
                itemCount = cartItem.Count;
            }
            else
            {
                _storeDb.Carts.Remove(cartItem);
            }

            _storeDb.SaveChanges();

            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = _storeDb.Carts.Where(cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                _storeDb.Carts.Remove(cartItem);
            }

            _storeDb.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return _storeDb.Carts.Where(cart => cart.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            var count = (from cartItems in _storeDb.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();

            return count ?? 0;
        }

        public decimal GetTotal()
        {
            var total = (from cartItems in _storeDb.Carts
                         where cartItems.CartId == ShoppingCartId
                         select (int?)cartItems.Count * cartItems.Album.Price).Sum();

            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    AlbumId = item.AlbumId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Album.Price,
                    Quantity = item.Count
                };

                orderTotal += (item.Count * item.Album.Price);

                _storeDb.OrderDetails.Add(orderDetail);

            }

            order.Total = orderTotal;

            _storeDb.SaveChanges();

            EmptyCart();

            return order.OrderId;
        }

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session != null && context.Session[CartSessionKey] != null)
            {
                return context.Session[CartSessionKey].ToString();
            }

            if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
            {
                if (context.Session != null)
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
            }
            else
            {
                var tempCartId = Guid.NewGuid();

                if (context.Session != null)
                {
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }

            return context.Session[CartSessionKey].ToString();
        }

        public void MigrateCart(string userName)
        {
            var shoppingCart = _storeDb.Carts.Where(c => c.CartId == ShoppingCartId);

            foreach (var item in shoppingCart)
            {
                item.CartId = userName;
            }

            _storeDb.SaveChanges();
        }
    }
}