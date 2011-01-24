﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.CurriculumManagement.Controllers;
using IUDICO.CurriculumManagement.Models;
using IUDICO.CurriculumManagement.Models.ViewDataClasses;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class ThemeAssignmentController : CurriculumBaseController
    {
        public ThemeAssignmentController(ICurriculumStorage curriculumStorage)
            : base(curriculumStorage)
        {

        }

        public ActionResult Index(int curriculumAssignmentId)
        {
            try
            {
                CurriculumAssignment curriculumAssignment = Storage.GetCurriculumAssignment(curriculumAssignmentId);
                var themeAssignments = Storage.GetThemeAssignmentsByCurriculumAssignmentId(curriculumAssignmentId);

                ViewData["Curriculum"] = Storage.GetCurriculum(curriculumAssignment.CurriculumRef);
                ViewData["GroupName"] = Storage.GetGroup(curriculumAssignment.UserGroupRef).Name;
                return View
                (
                    from themeAssignment in themeAssignments
                    select new ViewThemeAssignmentModel
                    {
                        ThemeAssignment = themeAssignment,
                        Theme = Storage.GetTheme(themeAssignment.ThemeRef)
                    }
                );
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public ActionResult Edit(int themeAssignmentId)
        {
            try
            {
                ThemeAssignment themeAssignment = Storage.GetThemeAssignment(themeAssignmentId);
                Session["CurriculumAssignmentId"] = themeAssignment.CurriculumAssignmentRef;
                
                return View(themeAssignment);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult Edit(int themeAssignmentId, ThemeAssignment themeAssignment)
        {
            try
            {
                themeAssignment.Id = themeAssignmentId;
                Storage.UpdateThemeAssignment(themeAssignment);

                return RedirectToRoute("ThemeAssignments", new { action = "Index", CurriculumAssignmentId = Session["CurriculumAssignmentId"] });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
