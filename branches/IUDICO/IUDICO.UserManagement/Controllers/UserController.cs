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
    public class UserController : UserBaseController
    {
        private readonly IUserStorage _storage;

        public UserController(IUserStorage userStorage)
        {
            _storage = userStorage;
        }

        //
        // GET: /User/

        public ActionResult Index()
        {
            return View(_storage.GetUsers());
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(Guid id)
        {
            return View(_storage.GetUser(id));
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _storage.CreateUser(user);

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        
        //
        // GET: /User/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            return View(new EditUserModel(_storage.GetUser(id)));
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid id,  EditUserModel editor)
        {
            if (ModelState.IsValid)
            {
                _storage.EditUser(id, editor);
 
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /User/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            return View(_storage.GetUser(id));
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        public ActionResult Delete(Guid id, FormContext context)
        {
            try
            {
                _storage.DeleteUser(id);
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
