using System.Web.Mvc;
using IUDICO.Common.Controllers;
using System.Collections.Generic;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.LMS.Models;

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

        public IEnumerable<Theme> GetAvailableThemes()
        {
            User user = MvcApplication.StaticContainer.GetService<IUserService>().GetCurrentUser();
            if (user != null)
            {
                return MvcApplication.StaticContainer.GetService<ICurriculumService>().GetThemesAvailableForUser(user);
            }
            else
            {
                return new List<Theme>();
            }
        }

        public ActionResult Index()
        {
            return View(new HomeModel()
            {
                Actions = new Dictionary<IPlugin, IEnumerable<Action>>(MvcApplication.Actions),
                AvailableThemes = GetAvailableThemes()
            });

            //return View(new Dictionary<IPlugin, IEnumerable<Action>>(MvcApplication.Actions));
        }
    }
}
