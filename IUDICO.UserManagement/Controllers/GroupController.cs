using System.Linq;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.UserManagement.Models.Storage;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.UserManagement.Controllers
{
    public class GroupController : UserBaseController
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
            var groupUser = new GroupUser();

            groupUser.GroupList = _Storage.GetGroups().AsQueryable().Select(g => new SelectListItem { Text = g.Name, Value = g.Id.ToString(), Selected = false });
            //groupUser.UserList = _Storage.GetUsers().AsQueryable().Select(u => new SelectListItem { Text = u.Username, Value = u.Id.ToString(), Selected = false });

            return View(groupUser);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult AddUsers(int id, int userId)
        {
            return View(new GroupUser());
        }
    }
}
