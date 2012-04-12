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
        private readonly IUserStorage _Storage;

        public UserController(IUserStorage userStorage)
        {
            _Storage = userStorage;
        }

        //
        // GET: /User/

        [Allow(Role = Role.Admin)]
        public ActionResult Index()
        {
            return View(_Storage.GetUsers());
        }

        //
        // GET: /User/Details/5

        [Allow(Role = Role.Admin)]
        public ActionResult Details(Guid id)
        {
            var user = _Storage.GetUser(u => u.Id == id);

            return View(new AdminDetailsModel(user));
        }

        //
        // GET: /User/Create

        [Allow(Role = Role.Admin)]
        public ActionResult Create()
        {
            var user = new User
                           {
                               /*RolesList =
                                   _Storage.GetRoles().AsQueryable().Select(
                                       r =>
                                       new SelectListItem
                                           {Text = IUDICO.UserManagement.Localization.getMessage(r.ToString()), Value = ((int) r).ToString(), Selected = false}
                                    )*/
                           };

            return View(user);
        }

        //
        // POST: /User/Create

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public ActionResult Create(User user)
        {
            if (user.OpenId == null)
            {
                user.OpenId = string.Empty;
            }

            if (ModelState.IsValid)
            {
                bool error = false;
                if (_Storage.UsernameExists(user.Username))
                {
                    error = true;
                    ModelState.AddModelError("Username", "User with such username already exists.");
                }
                if (!_Storage.UserUniqueIdAvailable(user.UserId, user.Id))
                {
                    error = true;
                    ModelState.AddModelError("UserID", Localization.getMessage("Unique ID Error"));
                }

                if (!error)
                {
                    _Storage.CreateUser(user);

                    return RedirectToAction("Index");
                }
            }

            user.Password = null;
            //user.RolesList = _Storage.GetRoles().AsQueryable().Select(r => new SelectListItem { Text = IUDICO.UserManagement.Localization.getMessage(r.ToString()), Value = ((int)r).ToString(), Selected = false });

            return View(user);
        }

        //
        // GET: /User/CreateMultiple

        [Allow(Role = Role.Admin)]
        public ActionResult CreateMultiple()
        {
            return View();
        }

        //
        // POST: /User/CreateMultiple

        [Allow(Role = Role.Admin)]
        [HttpPost]
        public ActionResult CreateMultiple(HttpPostedFileBase fileUpload)
        {
            var path = HttpContext.Request.PhysicalApplicationPath;

            path = Path.Combine(path, @"Data\CSV");
            path = Path.Combine(path, Guid.NewGuid().ToString());

            Directory.CreateDirectory(path);

            path = Path.Combine(path, fileUpload.FileName.Split('\\').Last());

            fileUpload.SaveAs(path);

            try
            {
                var users = _Storage.CreateUsersFromCSV(path);

                return View("CreatedMultiple", users);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);

                return View();
            }
        }

        //
        // GET: /User/Edit/5

        [Allow(Role = Role.Admin)]
        public ActionResult Edit(Guid id)
        {
            var user = _Storage.GetUser(u => u.Id == id);

            //user.RolesList = _Storage.GetRoles().AsQueryable().Select(r => new SelectListItem { Text = IUDICO.UserManagement.Localization.getMessage(r.ToString()), Value = ((int)r).ToString(), Selected = (user.Role == r) });

            return View(new EditUserModel(user));
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public ActionResult Edit(Guid id, EditUserModel user)
        {
            if (user.OpenId == null)
            {
                user.OpenId = string.Empty;
            }

            if (!ModelState.IsValid)
            {
                user.Password = null;
                //user.RolesList = _Storage.GetRoles().AsQueryable().Select(r => new SelectListItem { Text = IUDICO.UserManagement.Localization.getMessage(r.ToString()), Value = ((int)r).ToString(), Selected = (user.Role == r) });

                return View(user);
            }

            if (!_Storage.UserUniqueIdAvailable(user.UserId, user.Id))
            {
                ModelState.AddModelError("UserID", Localization.getMessage("Unique ID Error"));

                return View(user);
            }

            _Storage.EditUser(id, user);

            return RedirectToAction("Index");
        }

        //
        // Delete: /User/Delete/5

        [HttpDelete]
        [Allow(Role = Role.Admin)]
        public JsonResult Delete(Guid id)
        {
            try
            {
                _Storage.DeleteUser(u => u.Id == id);

                return Json(new {success = true, Id = id});
                ;
            }
            catch
            {
                return Json(new {success = false});
            }
        }

        [Allow(Role = Role.Admin)]
        public ActionResult Activate(Guid id)
        {
            _Storage.ActivateUser(id);

            return RedirectToAction("Index");
        }

        [Allow(Role = Role.Admin)]
        public ActionResult Deactivate(Guid id)
        {
            _Storage.DeactivateUser(id);

            return RedirectToAction("Index");
        }

        [Allow(Role = Role.Admin)]
        public ActionResult RemoveFromGroup(Guid id, int groupRef)
        {
            var user = _Storage.GetUser(u => u.Id == id);
            var group = _Storage.GetGroup(groupRef);

            _Storage.RemoveUserFromGroup(group, user);

            return RedirectToAction("Details", new {id});
        }

        [Allow(Role = Role.Admin)]
        public ActionResult AddToGroup(Guid id)
        {
            var user = _Storage.GetUser(u => u.Id == id);

            var groupList =
                _Storage.GetGroupsAvailableToUser(user).Select(
                    g => new SelectListItem {Text = g.Name, Value = g.Id.ToString(), Selected = false});

            var userGroup = new UserGroupModel {GroupList = groupList};

            return View(userGroup);
        }

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public ActionResult AddToGroup(Guid id, int? groupRef)
        {
            var user = _Storage.GetUser(u => u.Id == id);

            if (groupRef == null)
            {
                var groupList =
                    _Storage.GetGroupsAvailableToUser(user).Select(
                        g => new SelectListItem {Text = g.Name, Value = g.Id.ToString(), Selected = false});

                var userGroup = new UserGroupModel {GroupList = groupList};

                ModelState.AddModelError("GroupRef", "Please select group from list");

                return View(userGroup);
            }

            var group = _Storage.GetGroup(groupRef.Value);

            _Storage.AddUserToGroup(group, user);

            return RedirectToAction("Details", new {Id = id});
        }

        [HttpPost]
        public ActionResult UploadAvatar(Guid id, HttpPostedFileBase file)
        {
            _Storage.UploadAvatar(id, file);
            return RedirectToAction("Edit", new {id});
        }

        [Allow(Role = Role.Admin)]
        public ActionResult RemoveFromRole(Guid id, int? roleRef)
        {
            var user = _Storage.GetUser(u => u.Id == id);

            if (roleRef == null)
            {
                return RedirectToAction("Details", new {id});
            }

            var role = UserRoles.GetRole(roleRef.Value);

            _Storage.RemoveUserFromRole(role, user);

            return RedirectToAction("Details", new {id});
        }

        [Allow(Role = Role.Admin)]
        public ActionResult AddToRole(Guid id)
        {
            var user = _Storage.GetUser(u => u.Id == id);

            var roleList =
                _Storage.GetRolesAvailableToUser(user).Select(
                    r =>
                    new SelectListItem
                        {Text = Localization.getMessage(r.ToString()), Value = ((int) r).ToString(), Selected = false});

            var userRole = new UserRoleModel {RoleList = roleList};

            return View(userRole);
        }

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public ActionResult AddToRole(Guid id, int? roleRef)
        {
            var user = _Storage.GetUser(u => u.Id == id);

            if (roleRef == null)
            {
                var userList =
                    _Storage.GetRolesAvailableToUser(user).Select(
                        r =>
                        new SelectListItem
                            {
                                Text = Localization.getMessage(r.ToString()),
                                Value = ((int) r).ToString(),
                                Selected = false
                            });

                var userRole = new UserRoleModel {RoleList = userList};

                ModelState.AddModelError("RoleRef", "Please select role from list");

                return View(userRole);
            }

            var role = UserRoles.GetRole(roleRef.Value);

            _Storage.AddUserToRole(role, user);

            return RedirectToAction("Details", new {Id = id});
        }





        //=======================================================

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public JsonResult DeleteItem(Guid userId, string role)
        {
            try
            {
                var newrole = UserRoles.GetRole(role);
                var user = _Storage.GetUser(u => u.Id == userId);
                _Storage.RemoveUserFromRole(newrole, user);
                
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpPost]
        [Allow(Role = Role.Admin)]
        public JsonResult AddItem(Guid userId, string role)
        {
            try
            {
                var newrole = UserRoles.GetRole(role);
                var user = _Storage.GetUser(u => u.Id == userId);
                _Storage.AddUserToRole(newrole, user);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }
    }
}