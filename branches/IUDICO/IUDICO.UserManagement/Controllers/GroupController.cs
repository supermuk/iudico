using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using IUDICO.Common.Models;
using IUDICO.UserManagement.Models.Storage;

namespace IUDICO.UserManagement.Controllers
{
    public class GroupController : UserManagementBaseController
    {
        private readonly IUserStorage _Storage;

        public GroupController(IUserStorage userStorage)
        {
            _Storage = userStorage;
        }

        //
        // GET: /Group/

        public ActionResult Index()
        {
            return View(_Storage.GetGroups());
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
            if (ModelState.IsValid)
            {
                Storage.CreateGroup(group);
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
            Group group = _Storage.GetGroup(id);
            
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
            if (ModelState.IsValid)
            {
                Storage.EditGroup(id, group);
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
            try
            {
                Storage.DeleteGroup(id);
                return Json(new { status = true });
            }
            catch
            {
                return Json(new { status = false });
            }
        }

        public ActionResult AddUsers(int id)
        {
            var groupUser = new GroupUser();

            groupUser.GroupList = _Storage.GetGroups().AsQueryable().Select(g => new SelectListItem { Text = g.Name, Value = g.Id.ToString(), Selected = false });
            //groupUser.UserList = _Storage.GetUsers().AsQueryable().Select(u => new SelectListItem { Text = u.Username, Value = u.Id.ToString(), Selected = false });

            return View(groupUser);
        }

        [HttpPost]
        public ActionResult AddUsers(int id, int userId)
        {
            return View(new GroupUser());
        }
    }
}
