using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Controllers;

using IUDICO.Statistics.Models;

namespace IUDICO.Statistics.Controllers
{
    public class StatsController : PluginController
    {
        //
        // GET: /Stats/

        public ActionResult Index()
        {
            InfoOnFirstPage info = new InfoOnFirstPage();
            info.SetFakeData();

            ViewData["GroupsList"] = new SelectList(info.groupList);

            return View(info);
        }

        public ActionResult CurriculumInfo(int id)
        {
            try
            {
                InfoOnFirstPage info = new InfoOnFirstPage();
                info.SetFakeData();
                if (id >= 0 && id < info.curriculums.Count)
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
        public ActionResult CurriculumInfoID(Int32[] idList)
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

                    InfoOnFirstPage info = new InfoOnFirstPage();
                    info.SetFakeData();
                    return View(info);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
