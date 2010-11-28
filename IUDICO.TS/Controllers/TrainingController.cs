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

        public ActionResult Index(long id)
        {
            IEnumerable<Training> trainings = MlcDataProvider.Instance.GetTrainings(id);
            
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
    }
}
