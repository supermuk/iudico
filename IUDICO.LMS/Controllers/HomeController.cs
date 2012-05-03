using System.Web.Mvc;
using IUDICO.Common.Controllers;
using System.Collections.Generic;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.LMS.Models;
using IUDICO.Common.Models.Shared.CurriculumManagement;
using System.Linq;

namespace IUDICO.LMS.Controllers
{
    using IUDICO.Common.Models;

    public class HomeController : BaseController
    {
        /// <summary>
        /// Gets descriptions of topics available for playing.
        /// </summary>
        /// <returns></returns>
        // [OutputCache(Duration = 3600, VaryByParam = "none", VaryByCustom = "lang")]
        public IEnumerable<TopicDescription> GetTopicsDescriptions()
        {
            User user = MvcApplication.StaticContainer.GetService<IUserService>().GetCurrentUser();

            return user != null
                ? MvcApplication.StaticContainer.GetService<ICurriculumService>().GetTopicDescriptions(user)
                : new List<TopicDescription>();
        }

        public ActionResult Index()
        {
            User user = MvcApplication.StaticContainer.GetService<IUserService>().GetCurrentUser();
            
            var actions = MvcApplication.LmsService.GetActions();

            IEnumerable<TopicDescription> description = this.GetTopicsDescriptions();

            var groupedTopicsDescriptions = description.GroupBy(topicDescription_ => topicDescription_.Discipline.Name).ToDictionary(x => x.Key, x => x.GroupBy(y => y.Chapter.Name).ToDictionary(z => z.Key, z => z.ToList()));

            return View(
                new HomeModel
                {
                    Actions = actions,
                    TopicsDescriptions = description,
                    GroupedTopicsDescriptions = groupedTopicsDescriptions
                });
        }

        [OutputCache(Duration = 3600, VaryByParam = "none")]
        public ActionResult Error()
        {
            // log4net.ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
            // log.Error(HttpContext.Server.GetLastError().Message);
            return View();
        }
    }
}
