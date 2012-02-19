using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.CurriculumManagement.Models;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.CurriculumManagement.Models.ViewDataClasses;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class CurriculumTimelineController : CurriculumBaseController
    {
        public CurriculumTimelineController(ICurriculumStorage disciplineStorage)
            : base(disciplineStorage)
        {

        }

        [Allow(Role = Role.Teacher)]
        public ActionResult Index(int curriculumId)
        {
            try
            {
                Curriculum curriculum = Storage.GetCurriculum(curriculumId);
                Discipline discipline = Storage.GetDiscipline(curriculum.DisciplineRef);
                Group group = Storage.GetGroup(curriculum.UserGroupRef);
                var timelines = Storage.GetCurriculumTimelines(curriculumId);

                ViewData["Group"] = group;
                ViewData["Discipline"] = discipline;
                return View(timelines);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(int curriculumId)
        {
            try
            {
                LoadValidationErrors();

                Curriculum curriculum = Storage.GetCurriculum(curriculumId);
                Discipline discipline = Storage.GetDiscipline(curriculum.DisciplineRef);
                Group group = Storage.GetGroup(curriculum.UserGroupRef);
                CreateCurriculumTimelineModel createTimelineModel = new CreateCurriculumTimelineModel(new Timeline());

                ViewData["GroupName"] = group.Name;
                ViewData["DisciplineName"] = discipline.Name;
                return View(createTimelineModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(int curriculumId, CreateCurriculumTimelineModel createTimelineModel)
        {
            try
            {
                Timeline timeline = createTimelineModel.Timeline;
                timeline.CurriculumRef = curriculumId;
                timeline.ChapterRef = null;

                AddValidationErrorsToModelState(Validator.ValidateCurriculumTimeline(timeline).Errors);

                if (ModelState.IsValid)
                {
                    Storage.AddTimeline(timeline);

                    return RedirectToAction("Index", new { CurriculumId = curriculumId });
                }
                else
                {
                    SaveValidationErrors();

                    return RedirectToAction("Create");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int timelineId)
        {
            try
            {
                LoadValidationErrors();

                Timeline timeline = Storage.GetTimeline(timelineId);
                Curriculum curriculum = Storage.GetCurriculum(timeline.CurriculumRef);
                Discipline discipline = Storage.GetDiscipline(curriculum.DisciplineRef);
                Group group = Storage.GetGroup(curriculum.UserGroupRef);
                CreateCurriculumTimelineModel editTimelineModel = new CreateCurriculumTimelineModel(timeline);

                ViewData["GroupName"] = group.Name;
                ViewData["DisciplineName"] = discipline.Name;
                Session["CurriculumId"] = timeline.CurriculumRef;
                return View(editTimelineModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int timelineId, CreateCurriculumTimelineModel editTimelineModel)
        {
            try
            {
                Timeline timeline = editTimelineModel.Timeline;
                timeline.Id = timelineId;

                AddValidationErrorsToModelState(Validator.ValidateCurriculumTimeline(timeline).Errors);

                if (ModelState.IsValid)
                {
                    Storage.UpdateTimeline(timeline);

                    return RedirectToRoute("CurriculumTimelines", new { action = "Index", CurriculumId = Session["CurriculumId"] });
                }
                else
                {
                    SaveValidationErrors();

                    return RedirectToAction("Edit");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult DeleteItem(int timelineId)
        {
            try
            {
                Storage.DeleteTimeline(timelineId);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult DeleteItems(int[] timelineIds)
        {
            try
            {
                Storage.DeleteTimelines(timelineIds);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }
    }
}
