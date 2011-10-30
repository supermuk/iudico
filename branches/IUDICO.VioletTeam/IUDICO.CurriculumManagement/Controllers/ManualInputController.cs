using System;
using System.Collections.Generic;
using IUDICO.Common.Models;
using IUDICO.CurriculumManagement.Models.Storage;
using System.Web.Mvc;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class ManualInputController : CurriculumBaseController
    {
        public ManualInputController(ICurriculumStorage curriculumStorage)
            : base(curriculumStorage)
        {

        }
     
        [Allow (Role = Role.Teacher)]
        public ActionResult Index(int themeId)
        {
            try
            {
            var groups = Storage.GetGroupsAssignedToTheme(themeId);
            return View(groups);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //
        // GET: /ManualInput/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ManualInput/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ManualInput/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /ManualInput/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ManualInput/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ManualInput/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ManualInput/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
