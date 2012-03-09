using System.Linq;
using System.Web.Mvc;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.TestingSystem.Models;
using IUDICO.TestingSystem.Models.VOs;

namespace IUDICO.TestingSystem.Controllers
{
    public class TrainingController : PluginController
    {
        protected readonly IMlcProxy MlcProxy;

        protected IUserService UserService
        {
            get
            {
                IUserService result = LmsService.FindService<IUserService>();
                return result;
            }
        }

        protected ICourseService CourseService
        {
            get
            {
                ICourseService result = LmsService.FindService<ICourseService>();
                return result;
            }
        }

        public TrainingController(IMlcProxy mlcProxy)
        {
            MlcProxy = mlcProxy;
        }

        protected User CurrentUser
        {
            get
            {
                var result = UserService.GetCurrentUser();
                return result;
            }
        }
        
        //
        // GET: /Training/
        [Allow(Role=Role.Student)]
        public ActionResult Play(int id)
        {
            var curriculumService = LmsService.FindService<ICurriculumService>();
            var disciplineService = LmsService.FindService<IDisciplineService>();

            var topic = disciplineService.GetTopic(id);

            if (topic == null)
                return View("Error", "~/Views/Shared/Site.Master", Localization.getMessage("Topic_Not_Found"));

            var currentUser = UserService.GetCurrentUser();
            var topics = curriculumService.GetTopicDescriptions(currentUser).Select(t => t.Topic).Where(t => t.Id == topic.Id);
            var containsTopic = topics.Count() == 1;
            if (!containsTopic)
                return View("Error", "~/Views/Shared/Site.Master", Localization.getMessage("Not_Allowed_Pass_Topic"));

            long attemptId = MlcProxy.GetAttemptId(topic);

            ServicesProxy.Instance.Initialize(LmsService);

            return View("Play", new PlayModel { AttemptId = attemptId, TopicId = topic.Id });
        }
    }
}
