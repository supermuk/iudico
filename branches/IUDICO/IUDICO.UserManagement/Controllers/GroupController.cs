using System;
using System.Linq;
using System.Web.Mvc;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;
using IUDICO.Common.Models.Shared;
using IUDICO.UserManagement.Models.Storage;

namespace IUDICO.UserManagement.Controllers
{
    using IUDICO.Common;

    public class GroupController : PluginController
    {
        private readonly IUserStorage Storage;

        public GroupController(IUserStorage userStorage)
        {
            this.Storage = userStorage;
        }

        // GET: /Group/

        [Allow(Role = Role.Teacher)]
        public ActionResult Index()
        {
            return this.View(this.Storage.GetGroups());
        }

        // GET: /Group/Create

        [Allow(Role = Role.Teacher)]
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: /Group/Create

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(Group group)
        {
            if (this.ModelState.IsValid)
            {
                this.Storage.CreateGroup(group);

                return this.RedirectToAction("Index");
            }
            else
            {
                return View(group);
            }
        }

        // GET: /Group/Edit/5

        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int id)
        {
            var group = this.Storage.GetGroup(id);

            if (group == null)
            {
                return this.RedirectToAction("Error");
            }
            else
            {
                return View(group);
            }
        }

        // POST: /Group/Edit/5

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int id, Group group)
        {
            if (this.ModelState.IsValid)
            {
                this.Storage.EditGroup(id, group);

                return this.RedirectToAction("Index");
            }
            else
            {
                return View(group);
            }
        }

        // POST: /Role/Delete/5

        [HttpDelete]
        [Allow(Role = Role.Teacher)]
        public JsonResult Delete(int id)
        {
            try
            {
                this.Storage.DeleteGroup(id);

                return this.Json(new { status = true });
            }
            catch
            {
                return this.Json(new { status = false });
            }
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult AddUsers(int id)
        {
            var group = this.Storage.GetGroup(id);

            var userList =
                this.Storage.GetUsersNotInGroup(group).Select(
                    u => new SelectListItem { Text = u.Username, Value = u.Id.ToString(), Selected = false });

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
            return this.View(this.Storage.GetGroup(id));
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult AddUsers(int id, Guid? userRef)
        {
            var group = this.Storage.GetGroup(id);

            if (userRef == null)
            {
                var userList =
                    this.Storage.GetUsersNotInGroup(group).Select(
                        u => new SelectListItem { Text = u.Username, Value = u.Id.ToString(), Selected = false });

                var groupUser = new GroupUser
                                    {
                                        Group = group,
                                        UserList = userList
                                    };

                this.ModelState.AddModelError("UserRef", Localization.GetMessage("PleaseSelectUserFromList"));

                return View(groupUser);
            }

            var user = this.Storage.GetUser(u => u.Id == userRef.Value);

            this.Storage.AddUserToGroup(group, user);

            return this.RedirectToAction("Details", new { Id = id });
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult RemoveUser(int id, Guid userRef)
        {
            var group = this.Storage.GetGroup(id);
            var user = this.Storage.GetUser(u => u.Id == userRef);

            this.Storage.RemoveUserFromGroup(group, user);

            return this.RedirectToAction("Details", new { Id = id });
        }
    }
}