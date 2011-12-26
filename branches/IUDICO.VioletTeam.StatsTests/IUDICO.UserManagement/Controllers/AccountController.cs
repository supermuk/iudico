using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.IO;
using System.Collections.Generic;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using IUDICO.Common.Controllers;
using IUDICO.UserManagement.Models;
using IUDICO.UserManagement.Models.Storage;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;
using System.Globalization;
using IUDICO.Common;

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

            return View(new DetailsModel(user));
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();

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
                            log4net.ILog log = log4net.LogManager.GetLogger(typeof(AccountController));
                            log.Info("OpenID user " + user.Username + " logged in.");
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
                ModelState.AddModelError(string.Empty, Localization.getMessage("InvalidOpenID"));
                
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

                    log4net.ILog log = log4net.LogManager.GetLogger(typeof(AccountController));
                    log.Info(loginUsername + " logged in.");
                    return Redirect("/");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, Localization.getMessage("InvalidUsernameAndPassword"));
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

        public ActionResult Forgot()
        {
            return View(new RestorePasswordModel());
        }

        [HttpPost]
        public ActionResult Forgot(RestorePasswordModel restorePasswordModel)
        {
            var user = _Storage.GetUser(u => u.Email == restorePasswordModel.Email);

            if (user == null)
            {
                ModelState.AddModelError("Email", "No user with such email");
            }

            if (!ModelState.IsValid)
            {
                return View(restorePasswordModel);
            }

            _Storage.RestorePassword(restorePasswordModel);

            return View("ForgotSent");
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
                editModel.Id = _Storage.GetCurrentUser().Id;

                return View(editModel);
            }

            if (!_Storage.UserUniqueIdAvailable(editModel.UserId, _Storage.GetCurrentUser().Id))
            {
                ModelState.AddModelError("UserID", Localization.getMessage("Unique ID Error"));

                return View(editModel);
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
	    var oldPassword = _Storage.EncryptPassword(changePasswordModel.OldPassword);

            if (oldPassword != _Storage.GetCurrentUser().Password)
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

        [HttpPost]
        public ActionResult UploadAvatar(Guid id, HttpPostedFileBase file)
        {
            _Storage.UploadAvatar(id, file);
            
            return RedirectToAction("Edit");
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult TeacherToAdminUpgrade()
        {
            var allow = (bool)(Session["AllowAdmin"] ?? false);

            if (!allow && !Roles.IsUserInRole(Role.Admin.ToString()))
            {
                Session["AllowAdmin"] = true;
            }

            return RedirectToAction("Index");
        }

        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            Session["Culture"] = new CultureInfo(lang);
            
            return Redirect(returnUrl);
        }

    }
}
