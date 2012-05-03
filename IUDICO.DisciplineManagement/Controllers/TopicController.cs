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

                var partialViews = topics.Select(topic => PartialViewAsString("TopicRow", topic.ToViewTopicModel(Storage))).ToArray();

                return Json(new { success = true, items = partialViews });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(int chapterId)
        {
            var model = new CreateTopicModel(Storage.GetCourses(), new Topic());

            return PartialView("Create", model);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult Create(CreateTopicModel model)
        {
            try
            {
                var topic = new Topic();
                topic.UpdateFromModel(model);

                AddValidationErrorsToModelState(Validator.ValidateTopic(topic).Errors);
                if (ModelState.IsValid)
                {
                    Storage.AddTopic(topic);
                    var viewTopic = topic.ToViewTopicModel(Storage);
                    return Json(new { success = true, chapterId = model.ChapterId, topicRow = PartialViewAsString("TopicRow", viewTopic) });
                }

                var m = new CreateTopicModel(Storage.GetCourses(), topic);
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
            var model = new CreateTopicModel(Storage.GetCourses(), topic);

            return PartialView("Edit", model);
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public JsonResult Edit(int topicId, CreateTopicModel model)
        {
            try
            {
                var topic = new Topic { Id = topicId };
                topic.UpdateFromModel(model);

                AddValidationErrorsToModelState(Validator.ValidateTopic(topic).Errors);
                if (ModelState.IsValid)
                {
                    Storage.UpdateTopic(topic);
                    var viewTopic = topic.ToViewTopicModel(Storage);
                    var discipline = Storage.GetTopic(topicId).Chapter.Discipline;
                    return Json(new { success = true, topicId = topicId, topicRow = PartialViewAsString("TopicRow", viewTopic), disciplineId = discipline.Id, error = discipline.IsValid ? string.Empty : Validator.GetValidationError(discipline) });
                }

                var m = new CreateTopicModel(Storage.GetCourses(), topic);
                return Json(new { success = false, topicId = topicId, html = PartialViewAsString("Edit", m) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, html = ex.Message });
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
                Storage.TopicUp(topicId);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [Allow(Role = Role.Teacher)]
        public JsonResult TopicDown(int topicId)
        {
            try
            {
                Storage.TopicDown(topicId);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
