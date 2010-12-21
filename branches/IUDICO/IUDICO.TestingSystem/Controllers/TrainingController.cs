using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IUDICO.TestingSystem.Models.Shared;
using IUDICO.TestingSystem.Models;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models.Services;

namespace IUDICO.TestingSystem.Controllers
{
    public class TrainingController : PluginController
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
            var attemptId = MlcDataProvider.Instance.CreateAttempt(id);

            // TODO: redirect to frameset.
            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            var package = new ZipPackage("C:\\ZipPackages\\scorm1.zip", 1, DateTime.Now, "scorm1.zip");
            var training = MlcDataProvider.Instance.AddPackage(package);

            //Show success page with detailed info.
            return View("Details", training);
        }

        public ActionResult Details(long packageId, long attemptId = -1)
        {
            // TODO: provide user-based
            var trainings = MlcDataProvider.Instance.GetTrainings(1);
            var trainingsById = trainings.Where(tr => tr.PackageId == packageId);

            Training training;

            if (attemptId > 0 && trainingsById.Count() > 1)
            {
                training = trainingsById.SingleOrDefault(tr => tr.AttemptId == attemptId);
            }
            else
            {
                training = trainingsById.First();
            }

            return View("Details", training);
        }
    }
}
