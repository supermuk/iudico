using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace WebEditor.Controllers
{
    public class AccountController : Controller
    {
        private static OpenIdRelyingParty openid = new OpenIdRelyingParty();

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Account/Login");
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return Redirect("/Home");
        }

        public ActionResult LogOn()
        {
            IAuthenticationResponse response = openid.GetResponse();

            if (response != null)
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        FormsAuthentication.RedirectFromLoginPage(response.ClaimedIdentifier, false);
                        break;
                    case AuthenticationStatus.Canceled:
                        ModelState.AddModelError("loginIdentifier", "Login was cancelled at the provider");
                        break;
                    case AuthenticationStatus.Failed:
                        ModelState.AddModelError("loginIdentifier", "Login failed using the provided OpenID identifier");
                        break;
                }
            }           

            return View();
        }

        [HttpPost]
        public ActionResult LogOn(string loginIdentifier)
        {
            if (!Identifier.IsValid(loginIdentifier))
            {
                ModelState.AddModelError("loginIdentifier", "The specified login identifier is invalid");
                
                return View();
            }
            else
            {
                IAuthenticationRequest request = openid.CreateRequest(Identifier.Parse(loginIdentifier));

                request.AddExtension(new ClaimsRequest
                {
                    FullName = DemandLevel.Require,
                    Email = DemandLevel.Require
                });

                return request.RedirectingResponse.AsActionResult();
            }
        }
    }
}
