using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.CurriculumManagement.Controllers;
using IUDICO.CurriculumManagement.Models;
using IUDICO.CurriculumManagement.Models.ViewDataClasses;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class CurriculumChapterTopicController : CurriculumBaseController
    {
        public CurriculumChapterTopicController(ICurriculumStorage disciplineStorage)
            : base(disciplineStorage)
        {

        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult GetCurriculumChapterTopics(int parentId)
        {
           try {
                var curriculumChapter = Storage.GetCurriculumChapter(parentId);
                var curriculum = Storage.GetCurriculum(curriculumChapter.CurriculumRef);
                var curriculumChapterTopics = Storage.GetCurriculumChapterTopics(item => item.CurriculumChapterRef == parentId);
                var discipline = Storage.GetDiscipline(curriculum.DisciplineRef);
                ViewData["DisciplineId"] = discipline.Id;
                ViewData["DisciplineName"] = discipline.Name;
                ViewData["CurriculumId"] = curriculum.Id;
                ViewData["GroupName"] = Storage.GetGroup(curriculum.UserGroupRef) != null ? Storage.GetGroup(curriculum.UserGroupRef).Name : Localization.GetMessage("GroupNotExist");
                ViewData["ChapterName"] = Storage.GetChapter(curriculumChapter.ChapterRef).Name;
                var model = curriculumChapterTopics.Select(item => new ViewCurriculumChapterTopicModel {
                   Id = item.Id,
                   CurriculumChapterRef = item.CurriculumChapterRef,
                   BlockCurriculumAtTesting = item.BlockCurriculumAtTesting,
                   BlockTopicAtTesting = item.BlockTopicAtTesting,
                   TestStartDate = Converter.ToString(item.TestStartDate),
                   TestEndDate = Converter.ToString(item.TestEndDate),
                   TheoryStartDate = Converter.ToString(item.TheoryStartDate),
                   TheoryEndDate = Converter.ToString(item.TheoryEndDate),
                   ThresholdOfSuccess = item.ThresholdOfSuccess,
                   TopicName = Storage.GetTopic(item.TopicRef).Name
                });
                var partialViews = model.Select(item => PartialViewAsString("CurriculumChapterTopicRow", item)).ToArray();
                return Json(new {success = true, items = partialViews});
           } catch (Exception ex) {
                return Json(new {success = false});
           }
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int curriculumChapterTopicId)
        {
            var curriculumChapterTopic = Storage.GetCurriculumChapterTopic(curriculumChapterTopicId);
            var curriculumChapter = Storage.GetCurriculumChapter(curriculumChapterTopic.CurriculumChapterRef);
            var curriculum = Storage.GetCurriculum(curriculumChapter.CurriculumRef);

            var model = new CreateCurriculumChapterTopicModel(
                curriculumChapterTopic.ThresholdOfSuccess,
                curriculumChapterTopic.BlockTopicAtTesting,
                curriculumChapterTopic.BlockCurriculumAtTesting,
                curriculumChapterTopic.TestStartDate,
                curriculumChapterTopic.TestEndDate,
                curriculumChapterTopic.TheoryStartDate,
                curriculumChapterTopic.TheoryEndDate);

            Session["CurriculumChapterId"] = curriculumChapter.Id;
            ViewData["GroupName"] = Storage.GetGroup(curriculum.UserGroupRef) != null ? Storage.GetGroup(curriculum.UserGroupRef).Name : Localization.GetMessage("GroupNotExist");
            ViewData["ChapterName"] = Storage.GetChapter(curriculumChapter.ChapterRef).Name;
            return PartialView(model);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int curriculumChapterTopicId, CreateCurriculumChapterTopicModel model)
        {
           try {
              var curriculumChapterTopic = Storage.GetCurriculumChapterTopic(curriculumChapterTopicId);
              curriculumChapterTopic.ThresholdOfSuccess = model.ThresholdOfSuccess;
              curriculumChapterTopic.BlockCurriculumAtTesting = model.BlockCurriculumAtTesting;
              curriculumChapterTopic.BlockTopicAtTesting = model.BlockTopicAtTesting;
              curriculumChapterTopic.TestStartDate = model.SetTestTimeline ? model.TestStartDate : (DateTime?) null;
              curriculumChapterTopic.TestEndDate = model.SetTestTimeline ? model.TestEndDate : (DateTime?) null;
              curriculumChapterTopic.TheoryStartDate = model.SetTheoryTimeline ? model.TheoryStartDate : (DateTime?) null;
              curriculumChapterTopic.TheoryEndDate = model.SetTheoryTimeline ? model.TheoryEndDate : (DateTime?) null;

              AddValidationErrorsToModelState(Validator.ValidateCurriculumChapterTopic(curriculumChapterTopic).Errors);

              if (ModelState.IsValid) {
                 Storage.UpdateCurriculumChapterTopic(curriculumChapterTopic);
                 var curriculum = Storage.GetCurriculum(Storage.GetCurriculumChapter(curriculumChapterTopic.CurriculumChapterRef).CurriculumRef);
                 return Json(new {
                    success = true,
                    curriculumChapterTopicId = curriculumChapterTopicId,
                    curriculumChapterTopicRow = PartialViewAsString("CurriculumChapterTopicRow", new ViewCurriculumChapterTopicModel {
                       Id = curriculumChapterTopic.Id,
                       CurriculumChapterRef = curriculumChapterTopic.CurriculumChapterRef,
                       BlockCurriculumAtTesting = curriculumChapterTopic.BlockCurriculumAtTesting,
                       BlockTopicAtTesting = curriculumChapterTopic.BlockTopicAtTesting,
                       TestStartDate = Converter.ToString(curriculumChapterTopic.TestStartDate),
                       TestEndDate = Converter.ToString(curriculumChapterTopic.TestEndDate),
                       TheoryStartDate = Converter.ToString(curriculumChapterTopic.TheoryStartDate),
                       TheoryEndDate = Converter.ToString(curriculumChapterTopic.TheoryEndDate),
                       ThresholdOfSuccess = curriculumChapterTopic.ThresholdOfSuccess,
                       TopicName = Storage.GetTopic(curriculumChapterTopic.TopicRef).Name
                    }),
                    curriculumInfo = new { Id = curriculum.Id, IsValid = curriculum.IsValid }
                 });
              }
              return Json(new {success = false, curriculumChapterTopicId = curriculumChapterTopicId, html = PartialViewAsString("Edit", model)});
           } catch (Exception ex) {
              return Json(new {success = false, html = ex.Message});
           }
        }
    }
}
