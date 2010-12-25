using System.Web.Mvc;
using IUDICO.Common.Controllers;

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

        public ActionResult Index()
        {
            return View(MvcApplication.Actions);
        }
    }
}
