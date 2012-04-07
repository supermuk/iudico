using System.Linq;
using System.Web.Mvc;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.DisciplineManagement;
using IUDICO.TestingSystem.Models;
using IUDICO.TestingSystem.Models.VOs;

namespace IUDICO.TestingSystem.Controllers
{
    public class TrainingController : PluginController
    {
        protected readonly IMlcProxy MlcProxy;

        protected IUserService UserService
        {
            get { return LmsService.FindService<IUserService>(); }
        }

        protected ICourseService CourseService
        {
            get { return LmsService.FindService<ICourseService>(); }
        }

        protected ICurriculumService CurriculumService
        {
            get { return LmsService.FindService<ICurriculumService>(); }
        }

        protected IDisciplineService DisciplineService
        {
            get { return LmsService.FindService<IDisciplineService>(); }
        }

        public TrainingController(IMlcProxy mlcProxy)
        {
            MlcProxy = mlcProxy;
        }

        protected User CurrentUser
        {
            get { return UserService.GetCurrentUser(); }
        }
        
        //
        // GET: /Training/
        [Allow(Role=Role.Student)]
        public ActionResult Play(int curriculumChapterTopicId, int courseId, TopicTypeEnum topicType)
        {
            var curriculumChapterTopic = CurriculumService.GetCurriculumChapterTopicById(curriculumChapterTopicId);

            if (curriculumChapterTopic == null)
                return View("Error", "~/Views/Shared/Site.Master", Localization.getMessage("Topic_Not_Found"));

            var course = CourseService.GetCourse(courseId);
            if (course == null)
                return View("Error", "~/Views/Shared/Site.Master", Localization.getMessage("Course_Not_Found"));

            var currentUser = UserService.GetCurrentUser();

            var canPass = CurriculumService.CanPassCurriculumChapterTopic(currentUser, curriculumChapterTopic, topicType);

            if (!canPass)
                return View("Error", "~/Views/Shared/Site.Master", Localization.getMessage("Not_Allowed_Pass_Topic"));

            var attemptId = MlcProxy.GetAttemptId(curriculumChapterTopicId, courseId, topicType);

            ServicesProxy.Instance.Initialize(LmsService);

            return View("Play", new PlayModel { AttemptId = attemptId, CurriculumChapterTopicId = curriculumChapterTopicId });
        }
    }
}
