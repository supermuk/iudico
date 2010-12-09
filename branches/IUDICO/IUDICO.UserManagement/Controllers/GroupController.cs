using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.UserManagement.Models;

namespace IUDICO.UserManagement.Controllers
{
    public class GroupController : UMBaseController
    {
        //
        // GET: /Group/

        public ActionResult Index()
        {
            return View(Storage.GetGroups());
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
            if (ModelState.IsValid && Storage.CreateGroup(group))
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
            Group group = Storage.GetGroup(id);
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
            if (ModelState.IsValid && Storage.EditGroup(id, group))
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
            return Json(new { status = Storage.DeleteGroup(id) });
        }
    }
}
