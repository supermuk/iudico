using System.Web.Mvc;
using IUDICO.Common.Controllers;
using System.Collections.Generic;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.LMS.Models;
using IUDICO.Common.Models.Shared.CurriculumManagement;

namespace IUDICO.LMS.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        /*private ILmsService _lmsService;

        public HomeController(ILmsService lmsService)
        {
            _lmsService = lmsService;
        }*/

        /// <summary>
        /// Gets descriptions of themes available for playing.
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 3600, VaryByParam = "none", VaryByCustom = "lang")]
        public IEnumerable<ThemeDescription> GetThemesDescriptions()
        {
            User user = MvcApplication.StaticContainer.GetService<IUserService>().GetCurrentUser();
            if (user != null)
            {
                return MvcApplication.StaticContainer.GetService<ICurriculumService>().GetThemesAvailableForUser(user);
            }
            else
            {
                return new List<ThemeDescription>();
            }
        }

        public ActionResult Index()
        {
            return View(new HomeModel()
            {
                Actions = new Dictionary<IPlugin, IEnumerable<Action>>(MvcApplication.Actions),
                ThemesDescriptions = GetThemesDescriptions()
            });

            //return View(new Dictionary<IPlugin, IEnumerable<Action>>(MvcApplication.Actions));
        }
    }
}
