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

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult GetCurriculumChapters(int parentId)
        {
           try {
              var curriculum = Storage.GetCurriculum(parentId);
              var discipline = Storage.GetDiscipline(curriculum.DisciplineRef);
              var group = Storage.GetGroup(curriculum.UserGroupRef);
              var model = Storage.GetCurriculumChapters(item => item.CurriculumRef == parentId)
                                 .Select(item => new ViewCurriculumChapterModel {
                                    Id = item.Id,
                                    CurriculumRef = item.CurriculumRef,
                                    HaveTimelines = item.StartDate != (DateTime?)null,
                                    StartDate = Converter.ToString(item.StartDate),
                                    EndDate = Converter.ToString(item.EndDate),
                                    ChapterName = Storage.GetChapter((int) item.ChapterRef).Name
                                 });
              var partialViews = model.Select(item => PartialViewAsString("CurriculumChapterRow", item)).ToArray();

              //ViewData["GroupName"] = group != null ? group.Name : Localization.GetMessage("GroupNotExist");
              //ViewData["Discipline"] = discipline;
              //ViewData["DisciplineName"] = discipline.Name;

              return Json(new {
                 success = true,
                 items = partialViews
              });
           } catch (Exception ex) {
              return Json(new {
                 success = false
              });
           }
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
            
            return PartialView(model);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult Edit(int curriculumChapterId, CreateCurriculumChapterModel model)
        {
           try {
            var curriculumChapter = Storage.GetCurriculumChapter(curriculumChapterId);
            model.SetTimeline = true;
            curriculumChapter.StartDate = model.StartDate;
            curriculumChapter.EndDate = model.EndDate;

            AddValidationErrorsToModelState(Validator.ValidateCurriculumChapter(curriculumChapter).Errors);

            if (ModelState.IsValid)
            {
                Storage.UpdateCurriculumChapter(curriculumChapter);
                return Json(new { success = true, curriculumChapterId = curriculumChapterId, 
                   curriculumChapterRow = PartialViewAsString("CurriculumChapterRow", new ViewCurriculumChapterModel {
                      Id = curriculumChapterId,
                      CurriculumRef = curriculumChapter.CurriculumRef,
                      ChapterName = curriculumChapter.Chapter.Name,
                      HaveTimelines = model.SetTimeline,
                      StartDate = curriculumChapter.StartDate.ToString(),
                      EndDate = curriculumChapter.EndDate.ToString()
                   }),
                   curriculumInfo = new { Id = curriculumChapter.CurriculumRef, IsValid = Storage.GetCurriculum(curriculumChapter.CurriculumRef).IsValid }
                });
            }
              return Json(new {success = false, curriculumChapterId = curriculumChapterId, html = PartialViewAsString("Edit", model)});
           } catch (Exception ex) {
              return Json(new { success = false, html = ex.Message });
           }
        }

        [HttpPost]
        [Allow( Role = Role.Teacher )]
        public JsonResult RemoveChapterTimelines( int curriculumChapterId ) {
           try {
              var curriculumChapter = Storage.GetCurriculumChapter(curriculumChapterId);
              curriculumChapter.StartDate = null;
              curriculumChapter.EndDate = null;
              Storage.UpdateCurriculumChapter(curriculumChapter);
              return Json(new {success = true});
           } catch (Exception ex) {
              return Json(new {success = false, error = ex.Message});
           }
        }
    }
}
