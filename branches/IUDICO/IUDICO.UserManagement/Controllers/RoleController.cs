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
    public class RoleController : UserManagementBaseController
    {
        private readonly IUserStorage _storage;

        public RoleController(IUserStorage userStorage)
        {
            _storage = userStorage;
        }

        //
        // GET: /Role/

        public ActionResult Index()
        {
            return View(_storage.GetRoles());
        }

        //
        // GET: /Role/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Role/Create

        [HttpPost]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid && _storage.CreateRole(role))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(role);
            }
        }
        
        //
        // GET: /Role/Edit/5
 
        public ActionResult Edit(int id)
        {
            Role role = _storage.GetRole(id);
            if (role == null)
            {
                return RedirectToAction("Error");
            }
            else
            {
                return View(role);
            }
        }

        //
        // POST: /Role/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Role role)
        {
            if (ModelState.IsValid && _storage.EditRole(id, role))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(role);
            }
        }

        //
        // POST: /Role/Delete/5

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            return Json(new { status = _storage.DeleteRole(id) });
        }
    }
}
