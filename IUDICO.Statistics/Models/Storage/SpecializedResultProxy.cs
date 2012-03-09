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

        public AllSpecializedResults GetResults(IEnumerable<User> users, int[] selectDisciplineIds, ILmsService ILMS)
        {
            _LmsService = ILMS;
            AllSpecializedResults asr = new AllSpecializedResults();
            SpecializedResult specializedResult;
            DisciplineResult curRes;
            TopicResult topicResult;

            
            asr.Users = users.ToList();
            asr.SelectDisciplineIds = selectDisciplineIds;
            asr.Disciplines = _LmsService.FindService<ICurriculumService>().GetDisciplines(selectDisciplineIds);

            IEnumerable<int> ieIds = selectDisciplineIds;
            foreach (User usr in asr.Users)
            {
                specializedResult = new SpecializedResult();
                specializedResult.Disciplines = _LmsService.FindService<ICurriculumService>().GetDisciplines(ieIds);
                foreach (Discipline curr in specializedResult.Disciplines)
                {
                    curRes = new DisciplineResult();
                    curRes.Topics = _LmsService.FindService<ICurriculumService>().GetTopicsByDisciplineId(curr.Id);
                    
                    #region TopicResult

                    foreach (Topic topic in curRes.Topics)
                    {
                        topicResult = new TopicResult(usr, topic);
                        topicResult.AttemptResults = _LmsService.FindService<ITestingService>().GetResults(usr, topic);
                        topicResult.Res = topicResult.GetTopicResultScore();
                        curRes.TopicResult.Add(topicResult);
                    }

                    #endregion

                    curRes.CalculateSumAndMax(usr, curr);
                    specializedResult.DisciplineResult.Add(curRes);
                }
                specializedResult.CalculateSpecializedResult(usr);
                asr.SpecializedResult.Add(specializedResult);
            }
            return asr;
        }
    }
}