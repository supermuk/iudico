using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common;
using IUDICO.Common.Models.Shared;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.CurriculumManagement.Models;
using IUDICO.Common.Models;
using IUDICO.CurriculumManagement.Models.ViewDataClasses;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class CurriculumChapterController : CurriculumBaseController
    {
        public CurriculumChapterController(ICurriculumStorage disciplineStorage)
            : base(disciplineStorage)
        {

        }

        [Allow(Role = Role.Teacher)]
        public ActionResult Index(int curriculumId)
        {
            var curriculum = Storage.GetCurriculum(curriculumId);
            var discipline = Storage.GetDiscipline(curriculum.DisciplineRef);
            var group = Storage.GetGroup(curriculum.UserGroupRef);
            var model = Storage.GetCurriculumChapters(item => item.CurriculumRef == curriculumId)
                .Select(item => new ViewCurriculumChapterModel
                    {
                        Id = item.Id,
                        StartDate = Converter.ToString(item.StartDate),
                        EndDate = Converter.ToString(item.EndDate),
                        ChapterName = Storage.GetChapter((int)item.ChapterRef).Name
                    });

            ViewData["GroupName"] = group != null ? group.Name : Localization.GetMessage("GroupNotExist");
            ViewData["Discipline"] = discipline;
            
            return View(model);
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int curriculumChapterId)
        {
            var curriculumChapter = Storage.GetCurriculumChapter(curriculumChapterId);
            var curriculum = Storage.GetCurriculum(curriculumChapter.CurriculumRef);
            var discipline = Storage.GetDiscipline(curriculum.DisciplineRef);
            var group = Storage.GetGroup(curriculum.UserGroupRef);
            var model = new CreateCurriculumChapterModel(curriculumChapter.StartDate, curriculumChapter.EndDate);

            Session["CurriculumId"] = curriculumChapter.CurriculumRef;
            ViewData["GroupName"] = group != null ? group.Name : string.Empty;
            ViewData["DisciplineName"] = discipline.Name;
            
            return View(model);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int curriculumChapterId, CreateCurriculumChapterModel model)
        {
            var curriculumChapter = Storage.GetCurriculumChapter(curriculumChapterId);
            curriculumChapter.StartDate = model.SetTimeline ? model.StartDate : (DateTime?)null;
            curriculumChapter.EndDate = model.SetTimeline ? model.EndDate : (DateTime?)null;

            AddValidationErrorsToModelState(Validator.ValidateCurriculumChapter(curriculumChapter).Errors);

            if (ModelState.IsValid)
            {
                Storage.UpdateCurriculumChapter(curriculumChapter);
                return RedirectToRoute("CurriculumChapters", new { action = "Index", CurriculumId = Session["CurriculumId"] });
            }
            return View(model);
        }
    }
}
