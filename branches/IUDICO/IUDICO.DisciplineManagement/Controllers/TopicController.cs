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

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult ViewTopics(int parentId)
        {
            try
            {
                var topics = Storage.GetTopics(item => item.ChapterRef == parentId);

                var partialViews = topics.Select(topic => PartialViewAsString("TopicRow", ToViewTopicModel(topic))).ToArray();

                return Json(new { success = true, items = partialViews });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult Index(int chapterId)
        {
            var topics = Storage.GetTopics(item => item.ChapterRef == chapterId);
            var chapter = Storage.GetChapter(chapterId);
            var discipline = Storage.GetDiscipline(chapter.DisciplineRef);

            ViewData["DisciplineName"] = discipline.Name;
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
            var chapter = Storage.GetChapter(chapterId);
            var model = ToCreateTopicModel(new Topic());

            ViewData["DisciplineName"] = chapter.Discipline.Name;
            ViewData["ChapterName"] = chapter.Name;

            return PartialView("Create", model);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult Create(CreateTopicModel model)
        {
            try
            {
                var topic = ToTopic(model);

                AddValidationErrorsToModelState(Validator.ValidateTopic(topic).Errors);

                if (ModelState.IsValid)
                {
                    Storage.AddTopic(topic);

                    var viewTopic = ToViewTopicModel(topic);

                    return Json(new { success = true, chapterId = model.ChapterId, topicRow = PartialViewAsString("TopicRow", viewTopic) });
                }

                var m = ToCreateTopicModel(topic);
                return Json(new { success = false, chapterId = model.ChapterId, html = PartialViewAsString("Create", m) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, html = ex.Message });
            }
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int topicId)
        {
            var topic = Storage.GetTopic(topicId);
            var model = new CreateTopicModel(topic.Name, topic.ChapterRef, Storage.GetCourses(),
                topic.TestCourseRef, Storage.GetTestTopicTypes(), topic.TestTopicTypeRef,
                topic.TheoryCourseRef, Storage.GetTheoryTopicTypes(), topic.TheoryTopicTypeRef);

            ViewData["DisciplineName"] = topic.Chapter.Discipline.Name;
            ViewData["ChapterName"] = topic.Chapter.Name;
            ViewData["TopicName"] = topic.Name;
            return PartialView("Edit", model);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult Edit(int topicId, CreateTopicModel model)
        {
            try
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
                    var viewTopic = ToViewTopicModel(topic);
                    return Json(new { success = true, topicId = topicId, topicRow = PartialViewAsString("TopicRow", viewTopic) });
                }

                return Json(new { success = false, topicId = topicId, html = PartialView("Edit", model) });
            }
            catch (Exception ex)
            {
                return Json(new {success = false, html = ex.Message});
            }
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
        public JsonResult TopicUp(int topicId)
        {
            try
            {
                var topic = Storage.TopicUp(topicId);
                return Json(new {success = true});
            }
            catch (Exception ex)
            {
                return Json(new {success = false, message = ex.Message});
            }
        }

        [Allow(Role = Role.Teacher)]
        public JsonResult TopicDown(int topicId)
        {
            try
            {
                var topic = Storage.TopicDown(topicId);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new {success = false, message = ex.Message});
            }
        }


    }
}
