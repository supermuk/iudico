using System;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;
using IUDICO.UserManagement.Models;
using IUDICO.UserManagement.Models.Storage;
using System.Linq;

namespace IUDICO.UserManagement.Controllers
{
    public class UserController : UserBaseController
    {
        private readonly IUserStorage _Storage;

        public UserController(IUserStorage userStorage)
        {
            _Storage = userStorage;
        }

        //
        // GET: /User/

        [Allow(Role = Role.Teacher)]
        public ActionResult Index()
        {
            return View(_Storage.GetUsers());
        }

        //
        // GET: /User/Details/5

        [Allow(Role = Role.Teacher)]
        public ActionResult Details(Guid id)
        {
            var user = _Storage.GetUser(u => u.Id == id);
            var group = _Storage.GetGroupsByUser(user);

            return View(new AdminDetailsModel(user, group));
        }

        //
        // GET: /User/Create

        [Allow(Role = Role.Teacher)]
        public ActionResult Create()
        {
            var user = new User
                           {
                               RolesList =
                                   _Storage.GetRoles().AsQueryable().Select(
                                       r =>
                                       new SelectListItem
                                           {Text =IUDICO.UserManagement.Localization.getMessage(r.ToString()), Value = ((int) r).ToString(), Selected = false})
                           };

            return View(user);
        } 

        //
        // POST: /User/Create

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(User user)
        {
            if (user.OpenId == null)
                user.OpenId = string.Empty;
            if (ModelState.IsValid)
            {
                if (!_Storage.UsernameExists(user.Username))
                {
                    _Storage.CreateUser(user);

                    return RedirectToAction("Index");
                }
                
                ModelState.AddModelError("Username", "User with such username already exists.");
            }
            user.Password = null;
            user.RolesList = _Storage.GetRoles().AsQueryable().Select(r => new SelectListItem { Text = IUDICO.UserManagement.Localization.getMessage(r.ToString()), Value = ((int)r).ToString(), Selected = false });

            return View(user);
        }

        //
        // GET: /User/Edit/5

        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(Guid id)
        {
            var user = _Storage.GetUser(u => u.Id == id);

            user.RolesList = _Storage.GetRoles().AsQueryable().Select(r => new SelectListItem { Text = IUDICO.UserManagement.Localization.getMessage(r.ToString()), Value = ((int)r).ToString(), Selected = (user.Role == r) });

            return View(new EditUserModel(user));
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(Guid id,  EditUserModel user)
        {
            if (user.OpenId == null)
                user.OpenId = string.Empty;
            if (!ModelState.IsValid)
            {
                user.Password = null;
                user.RolesList = _Storage.GetRoles().AsQueryable().Select(r => new SelectListItem { Text = IUDICO.UserManagement.Localization.getMessage(r.ToString()), Value = ((int)r).ToString(), Selected = (user.Role == r) });
                return View(user);
            }

            _Storage.EditUser(id, user);
 
            return RedirectToAction("Index");
        }

        //
        // Delete: /User/Delete/5

        [HttpDelete]
        [Allow(Role = Role.Teacher)]
        public JsonResult Delete(Guid id)
        {
            try
            {
                _Storage.DeleteUser(u => u.Id == id);

                return Json(new { success = true, Id = id }); ;
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult Activate(Guid id)
        {
            _Storage.ActivateUser(id);

            return RedirectToAction("Index");
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult Deactivate(Guid id)
        {
            _Storage.DeactivateUser(id);

            return RedirectToAction("Index");
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult RemoveFromGroup(Guid id, int groupRef)
        {
            var user = _Storage.GetUser(u => u.Id == id);
            var group = _Storage.GetGroup(groupRef);

            _Storage.RemoveUserFromGroup(group, user);

            return RedirectToAction("Details", new { id = id });
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult AddToGroup(Guid id)
        {
            var user = _Storage.GetUser(u => u.Id == id);

            var groupList = _Storage.GetGroupsAvaliableForUser(user).Select(g => new SelectListItem { Text = g.Name, Value = g.Id.ToString(), Selected = false });

            var userGroup = new UserGroupModel { GroupList = groupList };

            return View(userGroup);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult AddToGroup(Guid id, int? groupRef)
        {
            var user = _Storage.GetUser(u => u.Id == id);

            if (groupRef == null)
            {
                var groupList = _Storage.GetGroupsAvaliableForUser(user).Select(g => new SelectListItem { Text = g.Name, Value = g.Id.ToString(), Selected = false });

                var userGroup = new UserGroupModel { GroupList = groupList };

                ModelState.AddModelError("GroupRef", "Please select group from list");

                return View(userGroup);
            }

            var group = _Storage.GetGroup(groupRef.Value);

            _Storage.AddUserToGroup(group, user);

            return RedirectToAction("Details", new { Id = id });
        }
    }
}
