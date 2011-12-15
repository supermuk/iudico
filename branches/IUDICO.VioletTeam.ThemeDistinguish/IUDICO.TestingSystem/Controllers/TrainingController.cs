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

            var theme = curriculumService.GetTheme(id);

            var currentUser = UserService.GetCurrentUser();
            var themes = curriculumService.GetThemesAvailableForUser(currentUser).Select(t => t.Theme).Where(t => t.Id == theme.Id);
            var containsTheme = themes.Count() == 1;
            if (!containsTheme)
                return View("Error", "~/Views/Shared/Site.Master", Localization.getMessage("Not_Allowed_Pass_Theme"));

            long attemptId = MlcProxy.GetAttemptId(theme);

            ServicesProxy.Instance.Initialize(LmsService);

            return View("Play", new PlayModel { AttemptId = attemptId, ThemeId = theme.Id });
        }
    }
}
