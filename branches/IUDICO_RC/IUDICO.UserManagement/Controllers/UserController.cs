using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;
using IUDICO.Common.Models.Notifications;
using IUDICO.Common.Models.Shared;
using IUDICO.UserManagement.Models;
using IUDICO.UserManagement.Models.Storage;

namespace IUDICO.UserManagement.Controllers
{
    using IUDICO.Common;

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
            return this.View(this.storage.GetUsers().Where(u=>!u.Deleted));
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
                if (!this.storage.UserOpenIdAvailable(user.OpenId, user.Id))
                {
                    this.ModelState.AddModelError("OpenId", Localization.GetMessage("OpenIdError"));

                    return this.View(user);
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

            if (fileUpload.ContentType != ".csv")
            {
                this.ModelState.AddModelError(string.Empty, "Incorrect file format. Please, upload csv file");

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
                this.ModelState.AddModelError(string.Empty, ex.Message);

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

                return this.View(user);
            }

            if (!this.storage.UserUniqueIdAvailable(user.UserId, user.Id))
            {
                this.ModelState.AddModelError("UserId", Localization.GetMessage("Unique ID Error"));

                return this.View(user);
            }

            if (!this.storage.UserOpenIdAvailable(user.OpenId, user.Id))
            {
                this.ModelState.AddModelError("OpenId", Localization.GetMessage("OpenIdError"));

                return this.View(user);
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
        public JsonResult DeleteSelected(IEnumerable<Guid> usersIds)
        {
            try
            {
                foreach (var id in usersIds)
                {
                    this.storage.DeleteUser(u => u.Id == id);
                }

                return this.Json(new { success = true });
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
        public JsonResult ActivateSelected(IEnumerable<Guid> usersIds)
        {
            foreach (var id in usersIds)
            {
                this.storage.ActivateUser(id);
            }

            return Json(new { success = true });
        }

        [Allow(Role = Role.Admin)]
        public ActionResult Deactivate(Guid id)
        {
            this.storage.DeactivateUser(id);

            return this.RedirectToAction("Index");
        }


        [Allow(Role = Role.Admin)]
        public JsonResult DeactivateSelected(IEnumerable<Guid> usersIds)
        {
            foreach (var id in usersIds)
            {
                this.storage.DeactivateUser(id);
            }

            return Json(new { success = true });
        }

        [Allow(Role = Role.Admin)]
        public ActionResult RemoveFromGroup(Guid id, int groupRef)
        {
            var user = this.storage.GetUser(u => u.Id == id);
            var group = this.storage.GetGroup(groupRef);

            if (user == null || group == null)
            {
                return this.RedirectToAction("Index");
            }

            this.storage.RemoveUserFromGroup(group, user);

            return this.RedirectToAction("Details", new { id });
        }

        [Allow(Role = Role.Admin)]
        public ActionResult AddToGroup(Guid id)
        {
            var user = this.storage.GetUser(u => u.Id == id);

            if (user == null)
            {
                return this.RedirectToAction("Index");
            }

            var groupList =
                this.storage.GetGroupsAvailableToUser(user).Select(
                    g => new SelectListItem { Text = g.Name, Value = g.Id.ToString(), Selected = false });

            var userGroup = new UserGroupModel { GroupList = groupList };

            return this.View(userGroup);
        }

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public ActionResult AddToGroup(Guid id, int? groupRef)
        {
            var user = this.storage.GetUser(u => u.Id == id);

            if (user == null)
            {
                return this.RedirectToAction("Index");
            }

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

            if (user == null)
            {
                return this.RedirectToAction("Index");
            }

            if (roleRef == null)
            {
                return this.RedirectToAction("Details", new { id });
            }

            var role = UserRoles.GetRole(roleRef.Value);

            this.storage.RemoveUserFromRole(role, user);
            LmsService.Inform(LMSNotifications.ActionsChanged);

            return this.RedirectToAction("Details", new { id });
        }

        [Allow(Role = Role.Admin)]
        public ActionResult AddToRole(Guid id)
        {
            var user = this.storage.GetUser(u => u.Id == id);

            if (user == null)
            {
                return this.RedirectToAction("Index");
            }

            var roleList =
                this.storage.GetRolesAvailableToUser(user).Select(
                    r =>
                    new SelectListItem { Text = Localization.GetMessage(r.ToString()), Value = ((int)r).ToString(), Selected = false });

            var userRole = new UserRoleModel { RoleList = roleList };

            LmsService.Inform(LMSNotifications.ActionsChanged);

            return this.View(userRole);
        }

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public ActionResult AddToRole(Guid id, int? roleRef)
        {
            var user = this.storage.GetUser(u => u.Id == id);

            if (user == null)
            {
                return this.RedirectToAction("Index");
            }

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

                return this.View(userRole);
            }

            var role = UserRoles.GetRole(roleRef.Value);

            this.storage.AddUserToRole(role, user);
            LmsService.Inform(LMSNotifications.ActionsChanged);

            return this.RedirectToAction("Details", new { Id = id });
        }

        [Allow(Role = Role.Admin)]
        public ActionResult AddUsersToRole(string usersId)
        {
            var ids = usersId.Split(',');

            List<Guid> usersIds = new List<Guid>();

            foreach (var id in ids)
            {
                try
                {
                    usersIds.Add(Guid.Parse(id));
                }
                catch (Exception)
                {

                }
            }

            var list = new List<Role> { Role.Student, Role.Teacher, Role.CourseCreator, Role.Admin }.Select(
                         r =>
                         new SelectListItem
                         {
                             Text = Localization.GetMessage(r.ToString()),
                             Value = ((int)r).ToString(),
                             Selected = false
                         });

            var userRole = new UserRoleModel { RoleList = list };

            return View(userRole);
        }

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public ActionResult AddUsersToRole(string usersId, int? roleRef)
        {

            var ids = usersId.Split(',');

            List<Guid> usersIds = new List<Guid>();

            foreach (var id in ids)
            {
                try
                {
                    usersIds.Add(Guid.Parse(id));
                }
                catch (Exception)
                {

                }
            }

            if (roleRef == null)
            {


                var list = new List<Role> { Role.Student, Role.Teacher, Role.CourseCreator, Role.Admin }.Select(r =>
                                                                                                                   new SelectListItem
                                                                                                                   {
                                                                                                                       Text = Localization.GetMessage(r.ToString()),
                                                                                                                       Value = ((int)r).ToString(),
                                                                                                                       Selected = false
                                                                                                                   });

                var userRole = new UserRoleModel { RoleList = list };

                this.ModelState.AddModelError("RoleRef", Localization.GetMessage("SelectRole"));

                return View(userRole);
            }


            var role = UserRoles.GetRole(roleRef.Value);

            foreach (var id in usersIds)
            {
                var user = this.storage.GetUser(u => u.Id == id);

                if (!this.storage.GetRolesAvailableToUser(user).Contains(role))
                {
                    continue;
                }

                this.storage.AddUserToRole(role, user);
            }

            LmsService.Inform(LMSNotifications.ActionsChanged);

            return RedirectToAction("Index");
        }


        [Allow(Role = Role.Admin)]
        public ActionResult AddUsersToGroup(string usersId)
        {
            var ids = usersId.Split(',');

            List<Guid> usersIds = new List<Guid>();

            foreach (var id in ids)
            {
                try
                {
                    usersIds.Add(Guid.Parse(id));
                }
                catch (Exception)
                {

                }

            }


            var groupList =
                this.storage.GetGroups().Select(
                    g => new SelectListItem { Text = g.Name, Value = g.Id.ToString(), Selected = false });

            var userGroup = new UserGroupModel { GroupList = groupList };

            return this.View(userGroup);
        }

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public ActionResult AddUsersToGroup(string usersId, int? groupRef)
        {

            var ids = usersId.Split(',');

            List<Guid> usersIds = new List<Guid>();

            foreach (var id in ids)
            {
                try
                {
                    usersIds.Add(Guid.Parse(id));
                }
                catch (Exception)
                {

                }
            }

            if (groupRef == null)
            {
                var groupList =
                    this.storage.GetGroups().Select(
                    g => new SelectListItem { Text = g.Name, Value = g.Id.ToString(), Selected = false });

                var userGroup = new UserGroupModel { GroupList = groupList };

                this.ModelState.AddModelError("GroupRef", Localization.GetMessage("SelectGroup"));

                return this.View(userGroup);
            }

            var group = this.storage.GetGroup(groupRef.Value);

            var users = this.storage.GetUsers().Where(u => usersIds.Contains(u.Id) && this.storage.GetGroupsAvailableToUser(u).Any(g => g.Id == group.Id)).ToList();

            foreach (var user in users)
            {
                this.storage.AddUserToGroup(group, user);
            }

            return RedirectToAction("Index");
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