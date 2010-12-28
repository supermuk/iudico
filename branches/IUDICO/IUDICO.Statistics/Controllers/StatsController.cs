using System;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.Common.Controllers;
using IUDICO.Statistics.Models;
using IUDICO.Statistics.Models.Storage;

namespace IUDICO.Statistics.Controllers
{
    public class StatsController : PluginController
    {
        private readonly IStatisticsStorage _storage;

        public StatsController(IStatisticsStorage statsStorage)
        {
            _storage = statsStorage;
        }

        //pages

        public ActionResult Index()
        {
            IEnumerable<Group> groups = _storage.GetAllGroups();

            return View(groups);
        }

        [HttpPost]
        public ActionResult SelectCurriculums(Int32 id)
        {
            IEnumerable<Curriculum> curriculums = _storage.GetCurrilulumsByGroupId(id);
            HttpContext.Session["SelectedGroupId"] = id;

            return View(curriculums);
        }

        [HttpPost]
        public ActionResult ShowCurriculumStatistic(Int32[] selectCurriculumId)
        {
            StatisticsStorage curriculumResult = new StatisticsStorage();
            curriculumResult.Curriculums = _storage.GetSelectedCurriclums(selectCurriculumId);
            curriculumResult.Students = _storage.GetUsersBySelectedGroup(LmsService.FindService<IUserService>().GetGroup((int)HttpContext.Session["SelectedGroupId"]));
            curriculumResult.Themes = _storage.GetAllThemes((int)HttpContext.Session["SelectedGroupId"]);
            ViewData["selectCurriculumId"] = selectCurriculumId;

            #region Students result by themes
            List<KeyValuePair<KeyValuePair<User, Theme>, double?>> results = new List<KeyValuePair<KeyValuePair<User, Theme>, double?>>();
            foreach (User user in curriculumResult.Students)
            {
                foreach (Curriculum curr in curriculumResult.Curriculums)
                {
                    foreach (KeyValuePair<List<Theme>, int> themeAndCurrId in curriculumResult.Themes)
                    {
                        if (themeAndCurrId.Value == curr.Id)
                        {
                            foreach (Theme th in themeAndCurrId.Key)
                            {
                                double? res = _storage.GetResults(user, th).First(x => x.User == user & x.Theme == th).Score.ToPercents();
                                results.Add(new KeyValuePair<KeyValuePair<User, Theme>, double?>(new KeyValuePair<User, Theme>(user, th), res));
                            }
                        }
                    }
                }
            }
            ViewData["points"] = results;
            #endregion

            return View(curriculumResult);
        }

    }
}
