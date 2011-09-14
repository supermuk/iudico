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
using IUDICO.CourseMgt.Models.Auth;

namespace IUDICO.CourseMgt.Controllers
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

            return Redirect("/Account/Index");
        }

        public ActionResult Login()
        {
            IAuthenticationResponse response = openid.GetResponse();

            if (response != null)
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:

                        string OpenID = response.FriendlyIdentifierForDisplay;
                        OpenIDMembershipProvider OpenIDProvider = (OpenIDMembershipProvider) Membership.Provider;
                        MembershipUser User = OpenIDProvider.GetUser(OpenID, true);

                        if (User == null)
                        {
                            ModelState.AddModelError("loginIdentifier", "Login failed using the provided OpenID identifier");

                            break;
                        }

                        if (Request.QueryString["ReturnUrl"] != null)
                        {
                            FormsAuthentication.RedirectFromLoginPage(User.UserName, false);
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(User.UserName, false);

                            return Redirect("/Account/Index");
                        }
                        
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
        public ActionResult Login(string loginIdentifier)
        {
            if (!Identifier.IsValid(loginIdentifier))
            {
                ModelState.AddModelError("loginIdentifier", "Invalid OpenID");
                
                return View("Login");
            }
            else
            {
                IAuthenticationRequest request = openid.CreateRequest(Identifier.Parse(loginIdentifier));

                return request.RedirectingResponse.AsActionResult();
            }
        }

        [HttpPost]
        public ActionResult LoginDefault(string loginUsername, string loginPassword)
        {
            if (Membership.ValidateUser(loginUsername, loginPassword))
            {
                if (Request.QueryString["ReturnUrl"] != null)
                {
                    FormsAuthentication.RedirectFromLoginPage(loginUsername, false);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(loginUsername, false);

                    return Redirect("/Account/Index");
                }
            }
            else
            {
                Response.Write("Invalid UserID and Password");
            }

            return View("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            OpenIDMembershipProvider provider = (OpenIDMembershipProvider) Membership.Provider;
            MembershipCreateStatus status;

            provider.CreateUser(registerModel.Username, registerModel.Password, registerModel.Email,
                                registerModel.OpenID, out status);

            if (status == MembershipCreateStatus.Success)
            {
                return View("Registered");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Edit()
        {
            EditModel editModel = new EditModel((OpenIDMembershipUser)Membership.GetUser());

            return View();
        }

        [HttpPost]
        public ActionResult Edit(EditModel editModel)
        {
            try
            {
                OpenIDMembershipProvider provider = (OpenIDMembershipProvider)Membership.Provider;

                OpenIDMembershipUser user = (OpenIDMembershipUser)Membership.GetUser();
                user.Update(editModel);

                provider.UpdateUser(user);

                return Redirect("/Account/Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
