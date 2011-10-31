using System;
using System.Linq;
using System.Web.Mvc;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.UserManagement.Models.Storage;
using IUDICO.Common.Models.Attributes;
using IUDICO.Common;

namespace IUDICO.UserManagement.Controllers
{
    public class GroupController : PluginController
    {
        private readonly IUserStorage _Storage;

        public GroupController(IUserStorage userStorage)
        {
            _Storage = userStorage;
        }

        //
        // GET: /Group/

        [Allow(Role = Role.Teacher)]
        public ActionResult Index()
        {
            return View(_Storage.GetGroups());
        }

        //
        // GET: /Group/Create

        [Allow(Role = Role.Teacher)]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Group/Create

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(Group group)
        {
            if (ModelState.IsValid)
            {
                _Storage.CreateGroup(group);

                return RedirectToAction("Index");
            }
            else
            {
                return View(group);
            }
        }

        //
        // GET: /Group/Edit/5

        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int id)
        {
            var group = _Storage.GetGroup(id);
            
            if (group == null)
            {
                return RedirectToAction("Error");
            }
            else
            {
                return View(group);
            }
        }

        //
        // POST: /Group/Edit/5

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int id, Group group)
        {
            if (ModelState.IsValid)
            {
                _Storage.EditGroup(id, group);

                return RedirectToAction("Index");
            }
            else
            {
                return View(group);
            }
        }

        //
        // POST: /Role/Delete/5

        [HttpDelete]
        [Allow(Role = Role.Teacher)]
        public JsonResult Delete(int id)
        {
            try
            {
                _Storage.DeleteGroup(id);

                return Json(new { status = true });
            }
            catch
            {
                return Json(new { status = false });
            }
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult AddUsers(int id)
        {
            var group = _Storage.GetGroup(id);

            var userList =
                _Storage.GetUsersNotInGroup(group).Select(
                    u => new SelectListItem {Text = u.Username, Value = u.Id.ToString(), Selected = false});

            var groupUser = new GroupUser
                                {
                                    Group = group,
                                    UserList = userList
                                };

            return View(groupUser);
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult Details(int id)
        {
            return View(_Storage.GetGroup(id));
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult AddUsers(int id, Guid? userRef)
        {
            var group = _Storage.GetGroup(id);

            if (userRef == null)
            {
                var userList =
                _Storage.GetUsersNotInGroup(group).Select(
                    u => new SelectListItem { Text = u.Username, Value = u.Id.ToString(), Selected = false });

                var groupUser = new GroupUser
                {
                    Group = group,
                    UserList = userList
                };

                ModelState.AddModelError("UserRef", Localization.getMessage("PleaseSelectUserFromList"));

                return View(groupUser);
            }

            var user = _Storage.GetUser(u => u.Id == userRef.Value);
            
            _Storage.AddUserToGroup(group, user);

            return RedirectToAction("Details", new { Id = id });
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult RemoveUser(int id, Guid userRef)
        {
            var group = _Storage.GetGroup(id);
            var user = _Storage.GetUser(u => u.Id == userRef);
            
            _Storage.RemoveUserFromGroup(group, user);

            return RedirectToAction("Details", new { Id = id });
        }
    }
}
