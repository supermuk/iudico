using System;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using IUDICO.Common.Controllers;
using IUDICO.UserManagement.Models;
using IUDICO.UserManagement.Models.Storage;
using IUDICO.Common.Models.Attributes;

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

        [Allow]
        public ActionResult Index()
        {
            var user = _Storage.GetCurrentUser();
            var groups = _Storage.GetGroupsByUser(user);

            return View(new DetailsModel(user, groups));
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return Redirect("/");
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

                        var user = _Storage.GetUser(u => u.OpenId == openId);

                        if (user == null)
                        {
                            ModelState.AddModelError(string.Empty, "Login failed using the provided OpenID identifier");

                            break;
                        }

                        if (Request.QueryString["ReturnUrl"] != null)
                        {
                            FormsAuthentication.RedirectFromLoginPage(user.Username, false);
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(user.Username, false);

                            return Redirect("/");
                        }
                        
                        break;
                    case AuthenticationStatus.Canceled:
                        ModelState.AddModelError(string.Empty, "Login was cancelled at the provider");
                        
                        break;
                    case AuthenticationStatus.Failed:
                        ModelState.AddModelError(string.Empty, "Login failed using the provided OpenID identifier");
                        
                        break;
                }
            }           

            return View();
        }

        [HttpPost]
        public ActionResult Login(string loginIdentifier)
        {
            if (string.IsNullOrEmpty(loginIdentifier) || !Identifier.IsValid(loginIdentifier))
            {
                ModelState.AddModelError(string.Empty, "Invalid OpenID");
                
                return View("Login");
            }
            else
            {
                try
                {
                    var request = _OpenId.CreateRequest(Identifier.Parse(loginIdentifier));

                    return request.RedirectingResponse.AsActionResult();
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Login failed using the provided OpenID identifier");

                    return View("Login");
                }
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

                    return Redirect("/");
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
            if (_Storage.UsernameExists(registerModel.Username))
            {
                ModelState.AddModelError("Username", "User with such username already exists");
            }
            else if (registerModel.Password != registerModel.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords don't match");
            }
            
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            _Storage.RegisterUser(registerModel);
            
            return View("Registered");
        }

        [Allow]
        public ActionResult Edit()
        {
            var editModel = new EditModel(_Storage.GetCurrentUser());

            return View(editModel);
        }

        [HttpPost]
        [Allow]
        public ActionResult Edit(EditModel editModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _Storage.EditAccount(editModel);
                
            return RedirectToAction("Index");
        }

        [Allow]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Allow]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (changePasswordModel.OldPassword != _Storage.GetCurrentUser().Password)
            {
                ModelState.AddModelError("OldPassword", "Pasword is not correct");
            }
            else if (changePasswordModel.NewPassword != changePasswordModel.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Paswords don't match");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }
            
            _Storage.ChangePassword(changePasswordModel);
            
            return RedirectToAction("Index");
        }
    }
}
