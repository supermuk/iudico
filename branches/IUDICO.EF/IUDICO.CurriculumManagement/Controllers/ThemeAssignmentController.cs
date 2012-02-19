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
    public class TopicAssignmentController : CurriculumBaseController
    {
        public TopicAssignmentController(ICurriculumStorage disciplineStorage)
            : base(disciplineStorage)
        {

        }

        [Allow(Role = Role.Teacher)]
        public ActionResult Index(int curriculumId)
        {
            try
            {
                Curriculum curriculum = Storage.GetCurriculum(curriculumId);
                var topicAssignments = Storage.GetTopicAssignmentsByCurriculumId(curriculumId);

                ViewData["Discipline"] = Storage.GetDiscipline(curriculum.DisciplineRef);
                ViewData["Group"] = Storage.GetGroup(curriculum.UserGroupRef);
                return View
                (
                    from topicAssignment in topicAssignments
                    select new ViewTopicAssignmentModel
                    {
                        TopicAssignment = topicAssignment,
                        Topic = Storage.GetTopic(topicAssignment.TopicRef)
                    }
                );
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int topicAssignmentId)
        {
            try
            {
                TopicAssignment topicAssignment = Storage.GetTopicAssignment(topicAssignmentId);
                Curriculum curriculum = Storage.GetCurriculum(topicAssignment.CurriculumRef);
                Discipline discipline = Storage.GetDiscipline(curriculum.DisciplineRef);
                Topic topic = Storage.GetTopic(topicAssignment.TopicRef);
                Group group = Storage.GetGroup(curriculum.UserGroupRef);

                Session["CurriculumId"] = topicAssignment.CurriculumRef;
                ViewData["GroupName"] = group.Name;
                ViewData["DisciplineName"] = discipline.Name;
                ViewData["ChapterName"] = topic.Name;
                return View(topicAssignment);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Allow(Role = Role.Teacher)]
        public ActionResult Edit(int topicAssignmentId, TopicAssignment topicAssignment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    topicAssignment.Id = topicAssignmentId;
                    Storage.UpdateTopicAssignment(topicAssignment);

                    return RedirectToRoute("TopicAssignments", new { action = "Index", CurriculumId = Session["CurriculumId"] });
                }
                else
                {
                    return RedirectToAction("Edit");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
