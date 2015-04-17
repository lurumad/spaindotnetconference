using System;
using System.Web.Mvc;
using System.Web.Security;
using MvcMusicStore.Features.Account.ViewModels;
using Microsoft.Web.Mvc;
using MvcMusicStore.Features.Home;

namespace MvcMusicStore.Features.Account
{
    public class AccountController : Controller
    {
        private void MigrateShoppingCart(string userName)
        {
            var cart = Models.ShoppingCart.GetCart(HttpContext);

            cart.MigrateCart(userName);
            Session[Models.ShoppingCart.CartSessionKey] = userName;
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (Membership.ValidateUser(model.UserName, model.Password))
            {
                MigrateShoppingCart(model.UserName); 
                    
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }

                return this.RedirectToAction<HomeController>(c => c.Index());
            }
            
            ModelState.AddModelError("", "The user name or password provided is incorrect.");

            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return this.RedirectToAction<HomeController>(c => c.Index());
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            MembershipCreateStatus createStatus;

            Membership.CreateUser(
                model.UserName, 
                model.Password, 
                model.Email, 
                "question", 
                "answer", 
                true, 
                null, 
                out createStatus);

            if (createStatus == MembershipCreateStatus.Success)
            {
                MigrateShoppingCart(model.UserName); 
                    
                FormsAuthentication.SetAuthCookie(model.UserName, false);

                return this.RedirectToAction<HomeController>(c => c.Index());
            }

            ModelState.AddModelError("", ErrorCodeToString(createStatus));

            return View(model);
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var changePasswordSucceeded = false;

            try
            {
                var currentUser = Membership.GetUser(
                    User.Identity.Name, 
                    true);

                if (currentUser != null)
                {
                    changePasswordSucceeded = currentUser.ChangePassword(
                        model.OldPassword, 
                        model.NewPassword);
                }
            }
            catch (Exception)
            {
                changePasswordSucceeded = false;
            }

            if (changePasswordSucceeded)
            {
                return this.RedirectToAction(c => c.ChangePasswordSuccess());
            }
            
            ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");

            return View(model);
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}
