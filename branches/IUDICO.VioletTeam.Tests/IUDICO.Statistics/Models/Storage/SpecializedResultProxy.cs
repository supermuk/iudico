using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Statistics.Models.Storage
{
    public class SpecializedResultProxy
    {
        private ILmsService _LmsService;

        public AllSpecializedResults GetResults(IEnumerable<User> users, int[] selectCurriculumIds, ILmsService ILMS)
        {
            _LmsService = ILMS;
            AllSpecializedResults asr = new AllSpecializedResults();
            SpecializedResult specializedResult;
            CurriculumResult curRes;
            ThemeResult themeResult;

            
            asr.Users = users.ToList();
            asr.SelectCurriculumIds = selectCurriculumIds;
            asr.Curriculums = _LmsService.FindService<ICurriculumService>().GetCurriculums(selectCurriculumIds);

            IEnumerable<int> ieIds = selectCurriculumIds;
            foreach (User usr in asr.Users)
            {
                specializedResult = new SpecializedResult();
                specializedResult.Curriculums = _LmsService.FindService<ICurriculumService>().GetCurriculums(ieIds);
                foreach (Curriculum curr in specializedResult.Curriculums)
                {
                    curRes = new CurriculumResult();
                    curRes.Themes = _LmsService.FindService<ICurriculumService>().GetThemesByCurriculumId(curr.Id);
                    
                    #region ThemeResult

                    foreach (Theme theme in curRes.Themes)
                    {
                        themeResult = new ThemeResult(usr, theme);
                        themeResult.AttemptResults = _LmsService.FindService<ITestingService>().GetResults(usr, theme);
                        themeResult.Res = themeResult.GetThemeResultScore();
                        curRes.ThemeResult.Add(themeResult);
                    }

                    #endregion

                    curRes.CalculateSumAndMax(usr, curr);
                    specializedResult.CurriculumResult.Add(curRes);
                }
                specializedResult.CalculateSpecializedResult(usr);
                asr.SpecializedResult.Add(specializedResult);
            }
            return asr;
        }
    }
}