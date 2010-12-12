using System.Web.Mvc;
using IUDICO.Common.Controllers;

namespace IUDICO.LMS.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
    }
}
