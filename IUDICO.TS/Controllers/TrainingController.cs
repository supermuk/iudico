using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.TS.Models.Shared;
using IUDICO.TS.Models;

namespace IUDICO.TS.Controllers
{
    public class TrainingController : Controller
    {
        //
        // GET: /Training/

        public ActionResult Index()
        {
            IEnumerable<Training> trainings = MlcDataProvider.Instance.GetTrainings(1);
            
            return View(trainings);
        }

        public ActionResult Play(long id)
        {

            // TODO: redirect to frameset.
            return new RedirectResult("http://localhost:1339/BasicWebPlayer/Frameset/Frameset.aspx?View=0&AttemptId=" + id.ToString());
        }

        public ActionResult Create(long id)
        {
            long attemptID = MlcDataProvider.Instance.CreateAttempt(id);

            // TODO: redirect to frameset.
            return View("Index");
        }

        public ActionResult Add()
        {
            ZipPackage package = new ZipPackage("C:\\ZipPackages\\scorm1.zip", 1, DateTime.Now, "scorm1.zip");
            Training training = MlcDataProvider.Instance.AddPackage(package);
            
            //Show success page with detailed info.
            return View("Details", training);
        }

        public ActionResult Details(long id)
        {
            // TODO: provide user-based
            IEnumerable<Training> trainings = MlcDataProvider.Instance.GetTrainings(1);
            Training training = trainings.Single(tr => tr.PackageID == id);

            return View("Details", training);
        }
    }
}
