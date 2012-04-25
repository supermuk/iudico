using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;
using IUDICO.Common.Models.Shared;
using IUDICO.UserManagement.Models;
using IUDICO.UserManagement.Models.Storage;

namespace IUDICO.UserManagement.Controllers
{
    public class UserController : PluginController
    {
        private readonly IUserStorage storage;

        public UserController(IUserStorage userStorage)
        {
            this.storage = userStorage;
        }

        // GET: /User/

        [Allow(Role = Role.Admin)]
        public ActionResult Index()
        {
            return this.View(this.storage.GetUsers());
        }

        // GET: /User/Details/5

        [Allow(Role = Role.Admin)]
        public ActionResult Details(Guid id)
        {
            var user = this.storage.GetUser(u => u.Id == id);
            var roles = this.storage.GetUserRoles(user.Username);
            var groups = this.storage.GetGroupsByUser(user);

            return this.View(new AdminDetailsModel(user, roles, groups));
        }

        // GET: /User/Create

        [Allow(Role = Role.Admin)]
        public ActionResult Create()
        {
            var user = new User();

            return View(user);
        }

        // POST: /User/Create

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public ActionResult Create(User user)
        {
            if (user.OpenId == null)
            {
                user.OpenId = string.Empty;
            }

            if (this.ModelState.IsValid)
            {
                bool error = false;
                if (this.storage.UsernameExists(user.Username))
                {
                    error = true;
                    this.ModelState.AddModelError("Username", "User with such username already exists.");
                }
                if (!this.storage.UserUniqueIdAvailable(user.UserId, user.Id))
                {
                    error = true;
                    this.ModelState.AddModelError("UserID", Localization.GetMessage("Unique ID Error"));
                }

                if (!error)
                {
                    this.storage.CreateUser(user);

                    return this.RedirectToAction("Index");
                }
            }

            user.Password = null;

            return View(user);
        }

        // GET: /User/CreateMultiple

        [Allow(Role = Role.Admin)]
        public ActionResult CreateMultiple()
        {
            return this.View();
        }

        // POST: /User/CreateMultiple

        [Allow(Role = Role.Admin)]
        [HttpPost]
        public ActionResult CreateMultiple(HttpPostedFileBase fileUpload)
        {
            if (fileUpload == null)
            {
                this.ModelState.AddModelError(string.Empty, "Please select file to upload");

                return this.View();
            }
            
            var path = this.HttpContext.Request.PhysicalApplicationPath;

            path = Path.Combine(path, @"Data\CSV");
            path = Path.Combine(path, Guid.NewGuid().ToString());

            Directory.CreateDirectory(path);

            path = Path.Combine(path, fileUpload.FileName.Split('\\').Last());

            fileUpload.SaveAs(path);

            try
            {
                var users = this.storage.CreateUsersFromCSV(path);

                return View("CreatedMultiple", users);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex);

                return this.View();
            }
        }

        // GET: /User/Edit/5

        [Allow(Role = Role.Admin)]
        public ActionResult Edit(Guid id)
        {
            var user = this.storage.GetUser(u => u.Id == id);

            return this.View(new EditUserModel(user));
        }

        // POST: /User/Edit/5

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public ActionResult Edit(Guid id, EditUserModel user)
        {
            if (user.OpenId == null)
            {
                user.OpenId = string.Empty;
            }

            if (!this.ModelState.IsValid)
            {
                user.Password = null;

                return View(user);
            }

            if (!this.storage.UserUniqueIdAvailable(user.UserId, user.Id))
            {
                this.ModelState.AddModelError("UserID", Localization.GetMessage("Unique ID Error"));

                return View(user);
            }

            this.storage.EditUser(id, user);

            return this.RedirectToAction("Index");
        }

        // Delete: /User/Delete/5

        [HttpDelete]
        [Allow(Role = Role.Admin)]
        public JsonResult Delete(Guid id)
        {
            try
            {
                this.storage.DeleteUser(u => u.Id == id);

                return this.Json(new { success = true, Id = id });
            }
            catch
            {
                return this.Json(new { success = false });
            }
        }

        [Allow(Role = Role.Admin)]
        public ActionResult Activate(Guid id)
        {
            this.storage.ActivateUser(id);

            return this.RedirectToAction("Index");
        }

        [Allow(Role = Role.Admin)]
        public ActionResult Deactivate(Guid id)
        {
            this.storage.DeactivateUser(id);

            return this.RedirectToAction("Index");
        }

        [Allow(Role = Role.Admin)]
        public ActionResult RemoveFromGroup(Guid id, int groupRef)
        {
            var user = this.storage.GetUser(u => u.Id == id);
            var group = this.storage.GetGroup(groupRef);

            this.storage.RemoveUserFromGroup(group, user);

            return this.RedirectToAction("Details", new { id });
        }

        [Allow(Role = Role.Admin)]
        public ActionResult AddToGroup(Guid id)
        {
            var user = this.storage.GetUser(u => u.Id == id);

            var groupList =
                this.storage.GetGroupsAvailableToUser(user).Select(
                    g => new SelectListItem { Text = g.Name, Value = g.Id.ToString(), Selected = false });

            var userGroup = new UserGroupModel { GroupList = groupList };

            return View(userGroup);
        }

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public ActionResult AddToGroup(Guid id, int? groupRef)
        {
            var user = this.storage.GetUser(u => u.Id == id);

            if (groupRef == null)
            {
                var groupList =
                    this.storage.GetGroupsAvailableToUser(user).Select(
                        g => new SelectListItem { Text = g.Name, Value = g.Id.ToString(), Selected = false });

                var userGroup = new UserGroupModel { GroupList = groupList };

                this.ModelState.AddModelError("GroupRef", "Please select group from list");

                return View(userGroup);
            }

            var group = this.storage.GetGroup(groupRef.Value);

            this.storage.AddUserToGroup(group, user);

            return this.RedirectToAction("Details", new { Id = id });
        }

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public ActionResult UploadAvatar(Guid id, HttpPostedFileBase file)
        {
            this.storage.UploadAvatar(id, file);

            return this.RedirectToAction("Edit", new { id });
        }

        [Allow(Role = Role.Admin)]
        public ActionResult DeleteAvatar(Guid id)
        {
            this.storage.DeleteAvatar(id);

            return this.RedirectToAction("Edit", new { id });
        }

        [Allow(Role = Role.Admin)]
        public ActionResult RemoveFromRole(Guid id, int? roleRef)
        {
            var user = this.storage.GetUser(u => u.Id == id);

            if (roleRef == null)
            {
                return this.RedirectToAction("Details", new { id });
            }

            var role = UserRoles.GetRole(roleRef.Value);

            this.storage.RemoveUserFromRole(role, user);

            return this.RedirectToAction("Details", new { id });
        }

        [Allow(Role = Role.Admin)]
        public ActionResult AddToRole(Guid id)
        {
            var user = this.storage.GetUser(u => u.Id == id);

            var roleList =
                this.storage.GetRolesAvailableToUser(user).Select(
                    r =>
                    new SelectListItem
                        { Text = Localization.GetMessage(r.ToString()), Value = ((int)r).ToString(), Selected = false });

            var userRole = new UserRoleModel { RoleList = roleList };

            return View(userRole);
        }

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public ActionResult AddToRole(Guid id, int? roleRef)
        {
            var user = this.storage.GetUser(u => u.Id == id);

            if (roleRef == null)
            {
                var userList =
                    this.storage.GetRolesAvailableToUser(user).Select(
                        r =>
                        new SelectListItem
                            {
                                Text = Localization.GetMessage(r.ToString()),
                                Value = ((int)r).ToString(),
                                Selected = false
                            });

                var userRole = new UserRoleModel { RoleList = userList };

                this.ModelState.AddModelError("RoleRef", "Please select role from list");

                return View(userRole);
            }

            var role = UserRoles.GetRole(roleRef.Value);

            this.storage.AddUserToRole(role, user);

            return this.RedirectToAction("Details", new { Id = id });
        }

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public JsonResult DeleteItem(Guid userId, string role)
        {
            try
            {
                var newrole = UserRoles.GetRole(role);
                var user = this.storage.GetUser(u => u.Id == userId);
                this.storage.RemoveUserFromRole(newrole, user);

                return this.Json(new { success = true });
            }
            catch (Exception e)
            {
                return this.Json(new { success = false, message = e.Message });
            }
        }

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public JsonResult AddItem(Guid userId, string role)
        {
            try
            {
                var newrole = UserRoles.GetRole(role);
                var user = this.storage.GetUser(u => u.Id == userId);
                this.storage.AddUserToRole(newrole, user);

                return this.Json(new { success = true });
            }
            catch (Exception e)
            {
                return this.Json(new { success = false, message = e.Message });
            }
        }
    }
}