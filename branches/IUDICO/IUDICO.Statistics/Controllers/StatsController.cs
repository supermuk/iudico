using System;
using System.Web.Mvc;
using IUDICO.Common.Controllers;

using IUDICO.Statistics.Models;
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
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CurriculumInfoId(Int32[] idList)
        {
            try
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
            catch(Exception ex)
            {
                throw ex;
            }
        }
        //Vitalik Pages
        public ActionResult ThemesInfo(Int32 CurriculumID, Int32 SelectedGroupID)
        {
            try
            {
                ThemeInfoModel model = new ThemeInfoModel();
                model.BuildFrom(CurriculumID, SelectedGroupID);

                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ThemeTestResaults(Int32 StudentID, Int32 ThemeID)
        {
            try
            {
                ThemeTestResaultsModel model = new ThemeTestResaultsModel();
                model.BuildFrom(StudentID, ThemeID);

                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
