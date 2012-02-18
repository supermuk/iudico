using System.Web.Mvc;
using IUDICO.Common.Controllers;
using System.Collections.Generic;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.LMS.Models;
using IUDICO.Common.Models.Shared.CurriculumManagement;

namespace IUDICO.LMS.Controllers
{
    public class HomeController : BaseController
    {
        /// <summary>
        /// Gets descriptions of topics available for playing.
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 3600, VaryByParam = "none", VaryByCustom = "lang")]
        public IEnumerable<TopicDescription> GetTopicsDescriptions()
        {
            User user = MvcApplication.StaticContainer.GetService<IUserService>().GetCurrentUser();

            if (user != null)
            {
                return MvcApplication.StaticContainer.GetService<ICurriculumService>().GetTopicsAvailableForUser(user);
            }
            else
            {
                return new List<TopicDescription>();
            }
        }

        public ActionResult Index()
        {
            return View(new HomeModel()
            {
                Actions = MvcApplication.LmsService.GetActions(),
                TopicsDescriptions = GetTopicsDescriptions()
            });

            //return View(new Dictionary<IPlugin, IEnumerable<Action>>(MvcApplication.Actions));
        }

        [OutputCache(Duration = 3600, VaryByParam = "none")]
        public ActionResult Error()
        {
            //log4net.ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
            //log.Error(HttpContext.Server.GetLastError().Message);
            return View();
        }
    }
}
