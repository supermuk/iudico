using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.TS.Models.Shared;
using IUDICO.TS.Models;

namespace IUDICO.TS.Controllers
{
    public class PackageController : Controller
    {
        //
        // GET: /Assignment/

        public ActionResult Index(long id)
        {
            IEnumerable<Package> packages = MlcDataProvider.Instance.GetPackages(id);
            

            return View(packages);
        }

    }
}
