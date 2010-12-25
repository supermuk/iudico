using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.UserManagement.Models;
using IUDICO.UserManagement.Models.Services;

namespace IUDICO.UserManagement.Controllers
{
    public class UserController : UserManagementBaseController
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View(Storage.GetUsers());
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(Guid id)
        {
            return View(Storage.GetUser(id));
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
                Storage.CreateUser(user);
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
            return View(new EditUserModel(Storage.GetUser(id)));
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid id,  EditUserModel editor)
        {
            if (ModelState.IsValid)
            {
                (Storage as DatabaseUserManagement).EditUser(id, editor);
 
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
            return View(Storage.GetUser(id));
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        public ActionResult Delete(Guid id, FormContext context)
        {
            try
            {
                Storage.DeleteUser(id);
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
