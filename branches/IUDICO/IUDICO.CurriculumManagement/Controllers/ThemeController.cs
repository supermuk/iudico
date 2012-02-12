using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.CurriculumManagement.Models;
using IUDICO.CurriculumManagement.Models.ViewDataClasses;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class TopicController : CurriculumBaseController
    {
        public TopicController(ICurriculumStorage disciplineStorage)
            : base(disciplineStorage)
        {

        }

        [Allow(Role = Role.Teacher)]
        public ActionResult Index(int chapterId)
        {
            try
            {
                var topics = Storage.GetTopicsByChapterId(chapterId);
                Chapter chapter = Storage.GetChapter(chapterId);

                ViewData["DisciplineName"] = chapter.Discipline.Name;
                ViewData["ChapterName"] = chapter.Name;
                ViewData["DisciplineId"] = chapter.DisciplineRef;
                return View(topics);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(int chapterId)
        {
            try
            {
                LoadValidationErrors();

                Chapter chapter = Storage.GetChapter(chapterId);
                var model = new CreateTopicModel(chapterId, Storage.GetCourses(), 0, Storage.GetTopicTypes(), 0, "");

                ViewData["DisciplineName"] = chapter.Discipline.Name;
                ViewData["ChapterName"] = chapter.Name;
                return View(model);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Create(int chapterId, CreateTopicModel model)
        {
            try
            {
                Topic topic = new Topic
                {
                    CourseRef = model.CourseId == Constants.NoCourseId ? (int?)null : model.CourseId,
                    ChapterRef = model.ChapterId,
                    TopicTypeRef = model.TopicTypeId,
                    Name = model.TopicName
                };

                AddValidationErrorsToModelState(Validator.ValidateTopic(topic).Errors);

                if (ModelState.IsValid)
                {
                    Storage.AddTopic(topic);

                    return RedirectToAction("Index", new { ChapterId = model.ChapterId });
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
        public ActionResult Edit(int topicId)
        {
            try
            {
                LoadValidationErrors();

                var topic = Storage.GetTopic(topicId);
                var model = new CreateTopicModel(topic.ChapterRef, Storage.GetCourses(), topic.CourseRef,
                    Storage.GetTopicTypes(), topic.TopicTypeRef, topic.Name);

                ViewData["DisciplineName"] = topic.Chapter.Discipline.Name;
                ViewData["ChapterName"] = topic.Chapter.Name;
                ViewData["TopicName"] = topic.Name;
                return View(model);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int topicId, CreateTopicModel model)
        {
            try
            {
                Topic topic = Storage.GetTopic(topicId);
                topic.CourseRef = model.CourseId == Constants.NoCourseId ? (int?)null : model.CourseId;
                topic.TopicTypeRef = model.TopicTypeId;
                topic.Name = model.TopicName;

                AddValidationErrorsToModelState(Validator.ValidateTopic(topic).Errors);

                if (ModelState.IsValid)
                {
                    Storage.UpdateTopic(topic);

                    return RedirectToRoute("Topics", new { action = "Index", ChapterId = topic.ChapterRef });
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
            try
            {
                var topic = Storage.TopicUp(topicId);

                return RedirectToRoute("Topics", new { Action = "Index", ChapterId = topic.ChapterRef });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Allow(Role = Role.Teacher)]
        public ActionResult TopicDown(int topicId)
        {
            try
            {
                var topic = Storage.TopicDown(topicId);

                return RedirectToRoute("Topics", new { Action = "Index", ChapterId = topic.ChapterRef });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
