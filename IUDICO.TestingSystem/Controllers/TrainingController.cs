using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IUDICO.TestingSystem.Models.VO;
using IUDICO.TestingSystem.Models;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.TestingSystem.Controllers
{
    public class TrainingController : PluginController
    {

        protected readonly IMlcProxy MlcProxy;

        protected IUserService UserService
        {
            get
            {
                IUserService result = LmsService.FindService<IUserService>();
                return result;
            }
        }

        protected ITestingService TestingService
        {
            get
            {
                return LmsService.FindService<ITestingService>();
            }
        }

        public TrainingController(IMlcProxy mlcProxy)
        {
            MlcProxy = mlcProxy;
        }

        protected IUDICO.Common.Models.User CurrentUser
        {
            get
            {
                var result = UserService.GetCurrentUser();
                return result;
            }
        }


        //
        // GET: /Training/

        public ActionResult Index()
        {
            // TODO: provide user-based
            IEnumerable<Training> trainings = MlcProxy.GetTrainings(1);
            return View(trainings);
        }

        public ActionResult Play(long id)
        {
            // TODO: redirect to frameset.
            //return new RedirectResult("http://localhost:1339/BasicWebPlayer/Frameset/Frameset.aspx?View=0&AttemptId=" + id.ToString());
            TestingService.GetAttempt(LmsService.FindService<ICourseService>().GetCourse((int)id));

            return View("Play", id);
        }

        public ActionResult Create(long id)
        {
            var attemptId = MlcProxy.CreateAttempt(id);

            // TODO: redirect to frameset.
            return RedirectToAction("Play", new { id = attemptId });
        }

        public ActionResult Details(long packageId, long attemptId = -1)
        {
            // TODO: provide user-based

            var trainings = MlcProxy.GetTrainings(1);
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

        public ActionResult Delete(long id)
        {
            MlcProxy.DeletePackage(id);

            return RedirectToAction("Index");
        }
    }
}
