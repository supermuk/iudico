using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;
using IUDICO.UserManagement.Models.Storage;

namespace IUDICO.UserManagement.Controllers
{
    public class RoleController : UserBaseController
    {
        private readonly IUserStorage _Storage;

        public RoleController(IUserStorage userStorage)
        {
            _Storage = userStorage;
        }

        //
        // GET: /Role/

        [Allow(Roles = "Teacher")]
        public ActionResult Index()
        {
            return View(_Storage.GetRoles());
        }

        //
        // GET: /Role/Create

        [Allow(Roles = "Teacher")]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Role/Create

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                _Storage.CreateRole(role);
                return RedirectToAction("Index");
            }
            else
            {
                return View(role);
            }
        }
        
        //
        // GET: /Role/Edit/5

        [Allow(Roles = "Teacher")]
        public ActionResult Edit(int id)
        {
            var role = _Storage.GetRole(id);

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
        [Allow(Roles = "Teacher")]
        public ActionResult Edit(int id, Role role)
        {
            if (ModelState.IsValid)
            {
                _Storage.EditRole(id, role);

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
        [Allow(Roles = "Teacher")]
        public JsonResult Delete(int id)
        {
            try
            {
                _Storage.DeleteRole(id);

                return Json(new { status = true });
            }
            catch
            {
                return Json(new { status = false }); // Don't know how to do this better.
            }
        }
    }
}
