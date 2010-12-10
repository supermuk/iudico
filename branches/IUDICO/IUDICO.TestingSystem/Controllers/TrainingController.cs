using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.TestingSystem.Models.Shared;
using IUDICO.TestingSystem.Models;

namespace IUDICO.TestingSystem.Controllers
{
    public class TrainingController : Controller
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
                long attemptID = MlcDataProvider.Instance.CreateAttempt(id);

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
                ZipPackage package = new ZipPackage("C:\\ZipPackages\\scorm1.zip", 1, DateTime.Now, "scorm1.zip");
                Training training = MlcDataProvider.Instance.AddPackage(package);

                //Show success page with detailed info.
                return View("Details", training);
            }
            catch (Exception exc)
            {
                return View("Error", new HandleErrorInfo(exc, "Training", "Add"));
            }
        }

        public ActionResult Details(long packageID, long attemptID =-1)
        {
            try
            {
                // TODO: provide user-based
                IEnumerable<Training> trainings = MlcDataProvider.Instance.GetTrainings(1);
                IEnumerable<Training> trainingsByID = trainings.Where(tr => tr.PackageID == packageID);
                Training training;
                if (attemptID > 0 && trainingsByID.Count() > 1)
                {
                    training = trainingsByID.SingleOrDefault(tr => tr.AttemptID == attemptID);
                }
                else
                {
                    training = trainingsByID.First();
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
