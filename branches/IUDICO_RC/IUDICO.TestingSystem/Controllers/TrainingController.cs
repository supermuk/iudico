// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TrainingController.cs" company="">
//   
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Web.Mvc;

using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.DisciplineManagement;
using IUDICO.TestingSystem.Models;
using IUDICO.TestingSystem.ViewModels;

namespace IUDICO.TestingSystem.Controllers
{
    using System;

    using IUDICO.Common;

    public class TrainingController : PluginController
    {
        protected readonly IMlcProxy MlcProxy;

        protected IUserService UserService
        {
            get
            {
                return LmsService.FindService<IUserService>();
            }
        }

        protected ICourseService CourseService
        {
            get
            {
                return LmsService.FindService<ICourseService>();
            }
        }

        protected ICurriculumService CurriculumService
        {
            get
            {
                return LmsService.FindService<ICurriculumService>();
            }
        }

        protected IDisciplineService DisciplineService
        {
            get
            {
                return LmsService.FindService<IDisciplineService>();
            }
        }

        public TrainingController(IMlcProxy mlcProxy)
        {
            this.MlcProxy = mlcProxy;
        }

        protected User CurrentUser
        {
            get
            {
                return this.UserService.GetCurrentUser();
            }
        }

        // GET: /Training/
        [Allow(Role = Role.Student)]
        public ActionResult Play(int curriculumChapterTopicId, int courseId, TopicTypeEnum topicType)
        {
            var curriculumChapterTopic = this.CurriculumService.GetCurriculumChapterTopicById(curriculumChapterTopicId);

            if (curriculumChapterTopic == null)
            {
                return this.View("Error", "~/Views/Shared/Site.Master", Localization.GetMessage("Topic_Not_Found"));
            }

            Course course;
            try
            {
                course = this.CourseService.GetCourse(courseId);
            }
            catch (InvalidOperationException)
            {
                course = null;    
            }
            
            if (course == null)
            {
                return this.View("Error", "~/Views/Shared/Site.Master", Localization.GetMessage("Course_Not_Found"));
            }

            var currentUser = this.UserService.GetCurrentUser();

            var canPass = this.CurriculumService.CanPassCurriculumChapterTopic(
                currentUser, curriculumChapterTopic, topicType);

            if (!canPass)
            {
                return this.View(
                    "Error", "~/Views/Shared/Site.Master", Localization.GetMessage("Not_Allowed_Pass_Topic"));
            }

            var attemptId = this.MlcProxy.GetAttemptId(curriculumChapterTopicId, courseId, topicType);

            ServicesProxy.Instance.Initialize(LmsService);

            return this.View(
                "Play",
                new PlayModel
                    {
                        AttemptId = attemptId,
                        CurriculumChapterTopicId = curriculumChapterTopicId,
                        TopicType = topicType
                    });
        }
    }
}