using System;
using System.Web.Mvc;
using IUDICO.Common.Controllers;
using IUDICO.Statistics.Models.Storage;

namespace IUDICO.Statistics.Controllers
{
    public class StatsController : PluginController
    {
        //
        // GET: /Stats/
        //Roma Pages
        public ActionResult Index()
        {
            var info = new InfoOnFirstPage();

            info.SetFakeData();

            ViewData["GroupsList"] = new SelectList(info.GroupList);

            return View(info);
        }

        public ActionResult CurriculumInfo(int id)
        {
            var info = new InfoOnFirstPage();

            info.SetFakeData();

            if (id >= 0 && id < info.Curriculums.Count)
            {
                var curriculum = new Curriculum();

                curriculum = info.GetCurriculum(id);

                return View(curriculum);
            }
            else
            {
                throw new Exception("Invalid id");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CurriculumInfoId(Int32[] idList)
        {
            if (idList == null)
            {
                throw new Exception("Please select one or more curriculums");
            }
            else
            {
                ViewData["IDs"] = idList;

                var info = new InfoOnFirstPage();
                info.SetFakeData();
                return View(info);
            }
        }

        //Vitalik Pages
        public ActionResult ThemesInfo(Int32 curriculumId, Int32 selectedGroupId)
        {
            var model = new ThemeInfoModel();
            model.BuildFrom(curriculumId, selectedGroupId);

            return View(model);
        }

        public ActionResult ThemeTestResaults(Int32 studentId, Int32 themeId)
        {
            var model = new ThemeTestResaultsModel();
            model.BuildFrom(studentId, themeId);

            return View(model);
        }
    }
}
