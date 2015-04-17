using System;
using System.Linq;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Features.Checkout
{
    [Authorize]
    public class CheckoutController : System.Web.Mvc.Controller
    {
        private readonly MusicStoreDbContext _dbContext;
        const string PromoCode = "FREE";

        public CheckoutController(MusicStoreDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }

            _dbContext = dbContext;
        }

        public ActionResult AddressAndPayment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }

                order.Username = User.Identity.Name;
                order.OrderDate = DateTime.Now;

                _dbContext.Orders.Add(order);
                _dbContext.SaveChanges();

                var cart = Models.ShoppingCart.GetCart(HttpContext);
                cart.CreateOrder(order);

                return RedirectToAction("Complete",
                    new { id = order.OrderId });
            }
            catch
            {
                return View(order);
            }
        }

        public ActionResult Complete(int id)
        {
            var isValid = _dbContext.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            return isValid ? View(id) : View("Error");
        }
    }
}
