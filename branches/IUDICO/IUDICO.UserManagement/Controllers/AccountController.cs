using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using IUDICO.Common.Controllers;
using IUDICO.UserManagement.Models;
using IUDICO.UserManagement.Models.Storage;
using OpenIdMembershipProvider = IUDICO.UserManagement.Models.Auth.OpenIdMembershipProvider;
using OpenIdMembershipUser = IUDICO.UserManagement.Models.Auth.OpenIdMembershipUser;

namespace IUDICO.UserManagement.Controllers
{
    public class AccountController : PluginController
    {
        private readonly OpenIdRelyingParty _OpenId = new OpenIdRelyingParty();
        private readonly IUserStorage _Storage;

        public AccountController(IUserStorage userStorage)
        {
            _Storage = userStorage;
        }

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
            var response = _OpenId.GetResponse();

            if (response != null)
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:

                        var openId = response.FriendlyIdentifierForDisplay;

                        var user = _Storage.GetUser(openId);

                        if (user == null)
                        {
                            ModelState.AddModelError("loginIdentifier", "Login failed using the provided OpenID identifier");

                            break;
                        }

                        if (Request.QueryString["ReturnUrl"] != null)
                        {
                            FormsAuthentication.RedirectFromLoginPage(user.Username, false);
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(user.Username, false);

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
                var request = _OpenId.CreateRequest(Identifier.Parse(loginIdentifier));

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
                ModelState.AddModelError(string.Empty, "Invalid Username and/or password");
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
            var provider = (OpenIdMembershipProvider) Membership.Provider;
            var status = MembershipCreateStatus.Success;

            //provider.CreateUser(registerModel.Username, registerModel.Password, registerModel.Email, registerModel.OpenID, out status);

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
            var editModel = new EditModel((OpenIdMembershipUser)Membership.GetUser());

            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(EditModel editModel)
        {
            try
            {
                var provider = (OpenIdMembershipProvider)Membership.Provider;

                var user = (OpenIdMembershipUser)Membership.GetUser();
                //user.Update(editModel);

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
