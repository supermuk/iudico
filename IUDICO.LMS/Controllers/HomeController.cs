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
    public class HomeController : BaseController
    {
        /// <summary>
        /// Gets descriptions of topics available for playing.
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 3600, VaryByParam = "none", VaryByCustom = "lang")]
        //TODO: FatTony; why for 3600 seconds and as I understand topics will ba cached for all users?
        public IEnumerable<TopicDescription> GetTopicsDescriptions()
        {
            User user = MvcApplication.StaticContainer.GetService<IUserService>().GetCurrentUser();

            if (user != null)
            {
                return MvcApplication.StaticContainer.GetService<ICurriculumService>().GetTopicDescriptions(user);
            }
            return new List<TopicDescription>();
        }

        public ActionResult Index()
        {
            User user = MvcApplication.StaticContainer.GetService<IUserService>().GetCurrentUser();
            if (user.UserId == null && user.Username == null)
            {
                ViewData["ShowReg"] = true;
            }
            else
            {
                ViewData["ShowReg"] = false;
            }
            var temp = MvcApplication.LmsService.GetActions();

            IEnumerable<TopicDescription> description = GetTopicsDescriptions();

            Dictionary<string, Dictionary<string, List<TopicDescription>>> groupedTopicsDescriptions = 
                description.GroupBy(topicDescription_ => topicDescription_.Discipline.Name).
                ToDictionary(x => x.Key, x => x.GroupBy(y => y.Chapter.Name).
                    ToDictionary(z => z.Key, z => z.ToList()));
            
            return View(new HomeModel
                {
                    Actions = temp,
                    TopicsDescriptions = description,
                    GroupedTopicsDescriptions = groupedTopicsDescriptions
                });
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
