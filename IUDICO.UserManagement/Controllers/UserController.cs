﻿using System;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;
using IUDICO.UserManagement.Models;
using IUDICO.UserManagement.Models.Storage;
using System.Linq;

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
            var user = _Storage.GetUser(u => u.Id == id);
            var group = _Storage.GetGroupsByUser(user);

            return View(new AdminDetailsModel(user, group));
        }

        //
        // GET: /User/Create

        [Allow(Role = Role.Teacher)]
        public ActionResult Create()
        {
            var user = new User
                           {
                               RolesList =
                                   _Storage.GetRoles().AsQueryable().Select(
                                       r =>
                                       new SelectListItem
                                           {Text = r.ToString(), Value = ((int) r).ToString(), Selected = false})
                           };

            return View(user);
        } 

        //
        // POST: /User/Create

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                if (!_Storage.UsernameExists(user.Username))
                {
                    _Storage.CreateUser(user);

                    return RedirectToAction("Index");
                }
                
                ModelState.AddModelError("Username", "User with such username already exists.");
            }

            user.Password = null;
            user.RolesList = _Storage.GetRoles().AsQueryable().Select(r => new SelectListItem { Text = r.ToString(), Value = ((int)r).ToString(), Selected = false });

            return View(user);
        }

        //
        // GET: /User/Edit/5

        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(Guid id)
        {
            var user = _Storage.GetUser(u => u.Id == id);

            user.RolesList = _Storage.GetRoles().AsQueryable().Select(r => new SelectListItem { Text = r.ToString(), Value = ((int)r).ToString(), Selected = (user.Role == r) });

            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(Guid id,  User user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _Storage.EditUser(id, user);
 
            return RedirectToAction("Index");
        }

        //
        // GET: /User/Delete/5

        [Allow(Role = Role.Teacher)]
        public ActionResult Delete(Guid id)
        {
            return View(_Storage.GetUser(u => u.Id == id));
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Delete(Guid id, FormContext context)
        {
            try
            {
                _Storage.DeleteUser(u => u.Id == id);
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Allow(Role = Role.Admin)]
        public ActionResult Activate(Guid id)
        {
            _Storage.ActivateUser(id);

            return RedirectToAction("Index");
        }

        [Allow(Role = Role.Admin)]
        public ActionResult Deactivate(Guid id)
        {
            _Storage.DeactivateUser(id);

            return RedirectToAction("Index");
        }

        [Allow(Role = Role.Admin)]
        public ActionResult RemoveFromGroup(Guid id, int groupRef)
        {
            var user = _Storage.GetUser(u => u.Id == id);
            var group = _Storage.GetGroup(groupRef);

            _Storage.RemoveUserFromGroup(group, user);

            return RedirectToAction("Details", new { id = id });
        }
    }
}
