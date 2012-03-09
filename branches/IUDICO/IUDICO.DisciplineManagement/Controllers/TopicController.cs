using System;
using System.Linq;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Attributes;
using IUDICO.DisciplineManagement.Models;
using IUDICO.DisciplineManagement.Models.Storage;
using IUDICO.DisciplineManagement.Models.ViewDataClasses;

namespace IUDICO.DisciplineManagement.Controllers
{
    public class TopicController : DisciplineBaseController
    {
        public TopicController(IDisciplineStorage disciplineStorage)
            : base(disciplineStorage)
        {

        }

        [Allow(Role = Role.Teacher)]
        public ActionResult Index(int chapterId)
        {
            var topics = Storage.GetTopics(item => item.ChapterRef == chapterId);
            var chapter = Storage.GetChapter(chapterId);

            ViewData["DisciplineName"] = chapter.Discipline.Name;
            ViewData["ChapterName"] = chapter.Name;
            ViewData["DisciplineId"] = chapter.DisciplineRef;
            return View(
                topics.Select(item => new ViewTopicModel
                {
                    Id = item.Id,
                    Created = Converter.ToString(item.Created),
                    Updated = Converter.ToString(item.Updated),
                    TestCourseName = item.TestCourseRef.HasValue && item.TestCourseRef != Constants.NoCourseId ?
                        Storage.GetCourse(item.TestCourseRef.Value).Name :
                        String.Empty,
                    TestTopicType = item.TestTopicTypeRef.HasValue ?
                        Converter.ToString(Storage.GetTopicType(item.TestTopicTypeRef.Value)) :
                        String.Empty,
                    TheoryCourseName = item.TheoryCourseRef.HasValue ?
                        Storage.GetCourse(item.TheoryCourseRef.Value).Name :
                        String.Empty,
                    TheoryTopicType = item.TheoryTopicTypeRef.HasValue ?
                        Converter.ToString(Storage.GetTopicType(item.TheoryTopicTypeRef.Value)) :
                        String.Empty,
                    TopicName = item.Name
                })
            );
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(int chapterId)
        {
            LoadValidationErrors();

            var chapter = Storage.GetChapter(chapterId);
            var model = new CreateTopicModel("", chapterId, Storage.GetCourses(),
                0, Storage.GetTestTopicTypes(), 0,
                0, Storage.GetTheoryTopicTypes(), 0);

            ViewData["DisciplineName"] = chapter.Discipline.Name;
            ViewData["ChapterName"] = chapter.Name;
            return View(model);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(int chapterId, CreateTopicModel model)
        {
            var topic = new Topic
            {
                ChapterRef = model.ChapterId,
                Name = model.TopicName,
                TestCourseRef = model.BindTestCourse ? model.TestCourseId : (int?)null,
                TestTopicTypeRef = model.BindTestCourse ? model.TestTopicTypeId : (int?)null,
                TheoryCourseRef = model.BindTheoryCourse ? model.TheoryCourseId : (int?)null,
                TheoryTopicTypeRef = model.BindTheoryCourse ? model.TheoryTopicTypeId : (int?)null
            };

            AddValidationErrorsToModelState(Validator.ValidateTopic(topic).Errors);

            if (ModelState.IsValid)
            {
                Storage.AddTopic(topic);
                return RedirectToAction("Index", new {model.ChapterId });
            }
            SaveValidationErrors();
            return RedirectToAction("Create");
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int topicId)
        {
            LoadValidationErrors();

            var topic = Storage.GetTopic(topicId);
            var model = new CreateTopicModel(topic.Name, topic.ChapterRef, Storage.GetCourses(),
                topic.TestCourseRef, Storage.GetTestTopicTypes(), topic.TestTopicTypeRef,
                topic.TheoryCourseRef, Storage.GetTheoryTopicTypes(), topic.TheoryTopicTypeRef);

            ViewData["DisciplineName"] = topic.Chapter.Discipline.Name;
            ViewData["ChapterName"] = topic.Chapter.Name;
            ViewData["TopicName"] = topic.Name;
            return View(model);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int topicId, CreateTopicModel model)
        {
            var topic = Storage.GetTopic(topicId);
            topic.Name = model.TopicName;
            topic.TestCourseRef = model.BindTestCourse ? model.TestCourseId : (int?)null;
            topic.TestTopicTypeRef = model.BindTestCourse ? model.TestTopicTypeId : (int?)null;
            topic.TheoryCourseRef = model.BindTheoryCourse ? model.TheoryCourseId : (int?)null;
            topic.TheoryTopicTypeRef = model.BindTheoryCourse ? model.TheoryTopicTypeId : (int?)null;

            AddValidationErrorsToModelState(Validator.ValidateTopic(topic).Errors);

            if (ModelState.IsValid)
            {
                Storage.UpdateTopic(topic);
                return RedirectToRoute("Topics", new { action = "Index", ChapterId = topic.ChapterRef });
            }
            SaveValidationErrors();
            return RedirectToAction("Edit");
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult DeleteItem(int topicId)
        {
            try
            {
                Storage.DeleteTopic(topicId);
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult DeleteItems(int[] topicIds)
        {
            try
            {
                Storage.DeleteTopics(topicIds);
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult TopicUp(int topicId)
        {
            var topic = Storage.TopicUp(topicId);
            return RedirectToRoute("Topics", new { Action = "Index", ChapterId = topic.ChapterRef });
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult TopicDown(int topicId)
        {
            var topic = Storage.TopicDown(topicId);
            return RedirectToRoute("Topics", new { Action = "Index", ChapterId = topic.ChapterRef });
        }
    }
}
