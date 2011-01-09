using System;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;
using IUDICO.UserManagement.Models;
using IUDICO.UserManagement.Models.Storage;

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
            return View(_Storage.GetUser(id));
        }

        //
        // GET: /User/Create

        [Allow(Role = Role.Teacher)]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /User/Create

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _Storage.CreateUser(user);

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        
        //
        // GET: /User/Edit/5

        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(Guid id)
        {
            return View(new EditUserModel(_Storage.GetUser(id)));
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(Guid id,  EditUserModel editor)
        {
            if (ModelState.IsValid)
            {
                _Storage.EditUser(id, editor);
 
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /User/Delete/5

        [Allow(Role = Role.Teacher)]
        public ActionResult Delete(Guid id)
        {
            return View(_Storage.GetUser(id));
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Delete(Guid id, FormContext context)
        {
            try
            {
                _Storage.DeleteUser(id);
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
