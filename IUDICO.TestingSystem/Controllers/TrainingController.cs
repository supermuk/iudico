using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IUDICO.TestingSystem.Models.VO;
using IUDICO.TestingSystem.Models;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Attributes;

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

        protected IUDICO.Common.Models.User CurrentUser
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

            if (!curriculumService.GetThemesAvailableForUser(UserService.GetCurrentUser()).Select(t => t.Theme).Contains(theme))
                return View("Error", "You are not allowed to pass this theme.");

            long attemptId = MlcProxy.GetAttemptId(theme);

            ServicesProxy.Instance.Initialize(LmsService);

            return View("Play", attemptId);
        }
    }
}
