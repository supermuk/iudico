using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using IUDICO.Common;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;
using IUDICO.Common.Models.Notifications;
using IUDICO.Common.Models.Services;
using IUDICO.UserManagement.Models;
using IUDICO.UserManagement.Models.Storage;

namespace IUDICO.UserManagement.Controllers
{
    public class AccountController : PluginController
    {
        private readonly OpenIdRelyingParty openId = new OpenIdRelyingParty();
        private readonly IUserStorage storage;

        public AccountController(IUserStorage userStorage)
        {
            this.storage = userStorage;
        }

        [Allow]
        public ActionResult Index()
        {
            var user = this.storage.GetCurrentUser();
            var roles = this.storage.GetUserRoles(user.Username);
            var groups = this.storage.GetGroupsByUser(user);

            return this.View(new DetailsModel(user, roles, groups));
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            this.Session.Clear();

            LmsService.Inform(UserNotifications.UserLogout);

            return this.Redirect("/");
        }

        public ActionResult Login()
        {
            var response = this.openId.GetResponse();

            if (response != null)
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:

                        var openId = response.FriendlyIdentifierForDisplay;

                        var user = this.storage.GetUser(u => u.OpenId == openId);

                        if (user == null)
                        {
                            this.ModelState.AddModelError(string.Empty, Localization.GetMessage("NoUserOpenID"));

                            break;
                        }

                        LmsService.Inform(UserNotifications.UserLogin, user);

                        if (!string.IsNullOrEmpty(this.Request.QueryString["ReturnUrl"]))
                        {
                            FormsAuthentication.RedirectFromLoginPage(user.Username, false);
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(user.Username, false);
                            Logger log = Logger.Instance;
                            log.Info(this, "OpenID user " + user.Username + " logged in.");

                            return this.Redirect("/");
                        }

                        break;
                    case AuthenticationStatus.Canceled:
                        this.ModelState.AddModelError(string.Empty, "Login was cancelled at the provider");

                        break;
                    case AuthenticationStatus.Failed:
                        this.ModelState.AddModelError(string.Empty, "Login failed using the provided OpenID identifier");

                        break;
                }
            }

            return this.View();
        }

        [HttpPost]
        public ActionResult Login(string loginIdentifier)
        {
            if (string.IsNullOrEmpty(loginIdentifier) || !Identifier.IsValid(loginIdentifier))
            {
                this.ModelState.AddModelError(string.Empty, Localization.GetMessage("InvalidOpenID"));

                return this.View();
            }
            else
            {
                try
                {
                    var user = this.storage.GetUser(u => u.OpenId == loginIdentifier);

                    if (user == null)
                    {
                        this.ModelState.AddModelError(string.Empty, Localization.GetMessage("NoUserOpenID"));

                        return this.View();
                    }

                    var request = this.openId.CreateRequest(Identifier.Parse(loginIdentifier));

                    if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                    {
                        request.AddCallbackArguments("ReturnUrl", Request.QueryString["ReturnUrl"]);
                    }

                    return request.RedirectingResponse.AsActionResult();
                }
                catch (Exception)
                {
                    this.ModelState.AddModelError(string.Empty, Localization.GetMessage("InvalidOpenID"));

                    return this.View();
                }
            }
        }

        [HttpPost]
        public ActionResult LoginDefault(string loginUsername, string loginPassword)
        {
            if (Membership.ValidateUser(loginUsername, loginPassword))
            {
                LmsService.Inform(UserNotifications.UserLogin, this.storage.GetCurrentUser());

                if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                {
                    FormsAuthentication.RedirectFromLoginPage(loginUsername, false);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(loginUsername, false);

                    Logger log = Logger.Instance;
                    log.Info(this, loginUsername + " logged in.");

                    return this.Redirect("/");
                }
            }
            else
            {
                this.ModelState.AddModelError(string.Empty, Localization.GetMessage("InvalidUsernameAndPassword"));
            }

            return this.View("Login");
        }

        public ActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (this.storage.UsernameExists(registerModel.Username))
            {
                this.ModelState.AddModelError("Username", "User with such username already exists");
            }
            else if (registerModel.Password != registerModel.ConfirmPassword)
            {
                this.ModelState.AddModelError("ConfirmPassword", "Passwords don't match");
            }
            
            if (!this.storage.UserOpenIdAvailable(registerModel.OpenId, Guid.Empty))
            {
                this.ModelState.AddModelError("OpenId", Localization.GetMessage("OpenIdError"));

                return this.View();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this.storage.RegisterUser(registerModel);

            return this.View("Registered");
        }

        public ActionResult Forgot()
        {
            return this.View(new RestorePasswordModel());
        }

        [HttpPost]
        public ActionResult Forgot(RestorePasswordModel restorePasswordModel)
        {
            var user = this.storage.GetUser(u => u.Username == restorePasswordModel.Username && u.Email == restorePasswordModel.Email);

            if (user == null)
            {
                this.ModelState.AddModelError("NotFound", "User with such username and email combination not found");
            }

            if (!this.ModelState.IsValid)
            {
                return View(restorePasswordModel);
            }

            this.storage.RestorePassword(restorePasswordModel);

            return this.View("ForgotSent");
        }

        [Allow]
        public ActionResult Edit()
        {
            var editModel = new EditModel(this.storage.GetCurrentUser());

            return this.View(editModel);
        }

        [HttpPost]
        [Allow]
        public ActionResult Edit(EditModel editModel)
        {
            if (!this.ModelState.IsValid)
            {
                editModel.Id = this.storage.GetCurrentUser().Id;

                return this.View(editModel);
            }

            if (!this.storage.UserUniqueIdAvailable(editModel.UserId, this.storage.GetCurrentUser().Id))
            {
                this.ModelState.AddModelError("UserId", Localization.GetMessage("Unique ID Error"));

                return this.View(editModel);
            }

            if (!this.storage.UserOpenIdAvailable(editModel.OpenId, this.storage.GetCurrentUser().Id))
            {
                this.ModelState.AddModelError("OpenId", Localization.GetMessage("OpenIdError"));

                return this.View(editModel);
            }

            this.storage.EditAccount(editModel);

            return this.RedirectToAction("Index");
        }

        [Allow]
        public ActionResult ChangePassword()
        {
            return this.View();
        }

        [Allow]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var oldPassword = this.storage.EncryptPassword(changePasswordModel.OldPassword);

            if (oldPassword != this.storage.GetCurrentUser().Password)
            {
                this.ModelState.AddModelError("OldPassword", "Pasword is not correct");
            }
            else if (changePasswordModel.NewPassword != changePasswordModel.ConfirmPassword)
            {
                this.ModelState.AddModelError("ConfirmPassword", "Paswords don't match");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this.storage.ChangePassword(changePasswordModel);

            return this.RedirectToAction("Index");
        }

        [Allow]
        [HttpPost]
        public ActionResult UploadAvatar(HttpPostedFileBase file)
        {
            var user = this.storage.GetCurrentUser();
            this.storage.UploadAvatar(user.Id, file);

            return this.RedirectToAction("Edit");
        }

        [Allow]
        public ActionResult DeleteAvatar()
        {
            var user = this.storage.GetCurrentUser();
            this.storage.DeleteAvatar(user.Id);

            return this.RedirectToAction("Edit");
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult TeacherToAdminUpgrade()
        {
            var allow = (bool)(this.Session["AllowAdmin"] ?? false);

            if (!allow && !Roles.IsUserInRole(Role.Admin.ToString()))
            {
                this.Session["AllowAdmin"] = true;
                LmsService.Inform(LMSNotifications.ActionsChanged);
            }

            return this.RedirectToAction("Index");
        }

        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            this.Session["Culture"] = new CultureInfo(lang);

            return this.Redirect(returnUrl);
        }

        [Allow]
        public JsonResult RateTopic(int topicId, int score)
        {
            score = Math.Min(0, score);
            score = Math.Max(score, 5);

            var topics =
                LmsService.FindService<ICurriculumService>().GetTopicDescriptions(this.storage.GetCurrentUser());

            if (topics.Select(t => t.Topic.Id).Contains(topicId))
            {
                this.storage.RateTopic(topicId, score);

                return this.Json("success");
            }

            return this.Json("fail");
        }
    }
}