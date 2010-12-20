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
            try
            {
                IEnumerable<Training> trainings = MlcDataProvider.Instance.GetTrainings(1);
                
                return View(trainings);
            }
            catch (Exception exc)
            {
                return View("Error", new HandleErrorInfo(exc, "Training", "Index"));
            }
        }

        public ActionResult Play(long id)
        {
            try
            {
                // TODO: redirect to frameset.
                return new RedirectResult("http://localhost:1339/BasicWebPlayer/Frameset/Frameset.aspx?View=0&AttemptId=" + id.ToString());
            }
            catch (Exception exc)
            {
                return View("Error", new HandleErrorInfo(exc, "Training", "Play"));
            }
        }

        public ActionResult Create(long id)
        {
            try
            {
                var attemptId = MlcDataProvider.Instance.CreateAttempt(id);

                // TODO: redirect to frameset.
                return RedirectToAction("Index");
            }
            catch (Exception exc)
            {
                return View("Error", new HandleErrorInfo(exc, "Training", "Create"));
            }
        }

        public ActionResult Add()
        {
            try
            {
                var package = new ZipPackage("C:\\ZipPackages\\scorm1.zip", 1, DateTime.Now, "scorm1.zip");
                var training = MlcDataProvider.Instance.AddPackage(package);

                //Show success page with detailed info.
                return View("Details", training);
            }
            catch (Exception exc)
            {
                return View("Error", new HandleErrorInfo(exc, "Training", "Add"));
            }
        }

        public ActionResult Details(long packageId, long attemptId = -1)
        {
            try
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
            catch (Exception exc)
            {
                return View("Error", new HandleErrorInfo(exc, "Training", "Details"));
            }
        }
    }
}
