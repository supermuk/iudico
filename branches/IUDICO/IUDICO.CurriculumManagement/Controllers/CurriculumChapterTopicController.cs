using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        [Allow(Role = Role.Teacher)]
        public ActionResult Index(int curriculumChapterId)
        {
            var curriculumChapter = Storage.GetCurriculumChapter(curriculumChapterId);
            var curriculum = Storage.GetCurriculum(curriculumChapter.CurriculumRef);
            var curriculumChapterTopics = Storage.GetCurriculumChapterTopics(item => item.CurriculumChapterRef == curriculumChapterId);

            ViewData["DisciplineName"] = Storage.GetDiscipline(curriculum.DisciplineRef).Name;
            ViewData["CurriculumId"] = curriculum.Id;
            ViewData["GroupName"] = Storage.GetGroup(curriculum.UserGroupRef).Name;
            ViewData["ChapterName"] = Storage.GetChapter(curriculumChapter.ChapterRef).Name;
            return View
            (
                curriculumChapterTopics
                .Select(item => new ViewCurriculumChapterTopicModel
                    {
                        Id = item.Id,
                        BlockCurriculumAtTesting = item.BlockCurriculumAtTesting,
                        BlockTopicAtTesting = item.BlockTopicAtTesting,
                        TestStartDate = Converter.ToString(item.TestStartDate),
                        TestEndDate = Converter.ToString(item.TestEndDate),
                        TheoryStartDate = Converter.ToString(item.TheoryStartDate),
                        TheoryEndDate = Converter.ToString(item.TheoryEndDate),
                        MaxScore = item.MaxScore,
                        TopicName = Storage.GetTopic(item.TopicRef).Name
                    })
            );
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int curriculumChapterTopicId)
        {
            var curriculumChapterTopic = Storage.GetCurriculumChapterTopic(curriculumChapterTopicId);
            var curriculumChapter = Storage.GetCurriculumChapter(curriculumChapterTopic.CurriculumChapterRef);
            var curriculum = Storage.GetCurriculum(curriculumChapter.CurriculumRef);

            var model = new CreateCurriculumChapterTopicModel(curriculumChapterTopic.MaxScore,
                curriculumChapterTopic.BlockTopicAtTesting, curriculumChapterTopic.BlockCurriculumAtTesting,
                curriculumChapterTopic.TestStartDate, curriculumChapterTopic.TestEndDate,
                curriculumChapterTopic.TheoryStartDate, curriculumChapterTopic.TheoryEndDate);

            Session["CurriculumChapterId"] = curriculumChapter.Id;
            ViewData["GroupName"] = Storage.GetGroup(curriculum.UserGroupRef).Name;
            ViewData["DisciplineName"] = Storage.GetDiscipline(curriculum.DisciplineRef).Name;
            ViewData["ChapterName"] = Storage.GetChapter(curriculumChapter.ChapterRef).Name;
            ViewData["TopicName"] = Storage.GetTopic(curriculumChapterTopic.TopicRef).Name;
            return View(model);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int curriculumChapterTopicId, CreateCurriculumChapterTopicModel model)
        {
            var curriculumChapterTopic = Storage.GetCurriculumChapterTopic(curriculumChapterTopicId);
            curriculumChapterTopic.MaxScore = model.MaxScore;
            curriculumChapterTopic.BlockCurriculumAtTesting = model.BlockCurriculumAtTesting;
            curriculumChapterTopic.BlockTopicAtTesting = model.BlockTopicAtTesting;
            curriculumChapterTopic.TestStartDate = model.SetTestTimeline ? model.TestStartDate : (DateTime?)null;
            curriculumChapterTopic.TestEndDate = model.SetTestTimeline ? model.TestEndDate : (DateTime?)null;
            curriculumChapterTopic.TheoryStartDate = model.SetTheoryTimeline ? model.TheoryStartDate : (DateTime?)null;
            curriculumChapterTopic.TheoryEndDate = model.SetTheoryTimeline ? model.TheoryEndDate : (DateTime?)null;

            AddValidationErrorsToModelState(Validator.ValidateCurriculumChapterTopic(curriculumChapterTopic).Errors);

            if (ModelState.IsValid)
            {
                Storage.UpdateCurriculumChapterTopic(curriculumChapterTopic);
                return RedirectToRoute("CurriculumChapterTopics", new { action = "Index", CurriculumChapterId = Session["CurriculumChapterId"] });
            }
            return View(model);
        }
    }
}
