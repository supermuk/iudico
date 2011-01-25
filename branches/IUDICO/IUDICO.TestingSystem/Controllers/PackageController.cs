using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models.Services;
using IUDICO.TestingSystem.Models.VO;
using IUDICO.TestingSystem.Models;
using System.IO;
using System;

namespace IUDICO.TestingSystem.Controllers
{
    public class PackageController : PluginController
    {
        protected ICourseService CourseService
        {
            get
            {
                return LmsService.FindService<ICourseService>();
            }
        }

        protected readonly IMlcProxy MlcProxy;

        protected IUserService UserService
        {
            get
            {
                IUserService result = LmsService.FindService<IUserService>();
                return result;
            }
        }

        public PackageController(IMlcProxy mlcProxy)
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
        // GET: /Package/

        public ActionResult Index()
        {
            var courses = CourseService.GetCourses().Where(course => course.Deleted == false);
            
            return View(courses);
        }

        //
        // GET : /Package/Import

        public ActionResult Import()
        {
            return View();
        }

        //
        // POST: Package/Import

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase fileUpload)
        {
            if (fileUpload == null)
            {
                return RedirectToAction("Import");
            }
            var path = HttpContext.Request.PhysicalApplicationPath;

            path = Path.Combine(path, @"Data\WorkFolder");
            path = Path.Combine(path, Guid.NewGuid().ToString());

            Directory.CreateDirectory(path);

            path = Path.Combine(path, fileUpload.FileName.Split('\\').Last());

            fileUpload.SaveAs(path);

            string zipName = Path.GetFileName(path);

            var package = new ZipPackage(path, 1, DateTime.Now, zipName);
            var training = MlcProxy.AddPackage(package);

            //Show success page with detailed info.
            return RedirectToAction("Details", "Training", new {packageID = training.PackageId, attemptID=training.AttemptId});
        }
    }
}
