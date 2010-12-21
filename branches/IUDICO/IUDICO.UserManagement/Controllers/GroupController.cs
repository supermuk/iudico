using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.UserManagement.Models;
using IUDICO.UserManagement.Models.Storage;

namespace IUDICO.UserManagement.Controllers
{
    public class GroupController : UserManagementBaseController
    {
        private readonly IUserStorage _storage;

        public GroupController(IUserStorage userStorage)
        {
            _storage = userStorage;
        }

        //
        // GET: /Group/

        public ActionResult Index()
        {
            return View(_storage.GetGroups());
        }

        //
        // GET: /Group/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Group/Create

        [HttpPost]
        public ActionResult Create(Group group)
        {
            if (ModelState.IsValid && _storage.CreateGroup(group))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(group);
            }
        }

        //
        // GET: /Group/Edit/5

        public ActionResult Edit(int id)
        {
            Group group = _storage.GetGroup(id);
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
        public ActionResult Edit(int id, Group group)
        {
            if (ModelState.IsValid && _storage.EditGroup(id, group))
            {
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
        public JsonResult Delete(int id)
        {
            return Json(new { status = _storage.DeleteGroup(id) });
        }
    }
}
