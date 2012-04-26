using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Statistics.Models.Storage
{
    public class SpecializedResultProxy
    {
        private ILmsService _LmsService;

        public AllSpecializedResults GetResults(IEnumerable<User> users, int[] selectedCurriculumIds, ILmsService lmsService)
        {
            _LmsService = lmsService;
            AllSpecializedResults asr = new AllSpecializedResults();
            SpecializedResult specializedResult;
            DisciplineResult curRes;
            TopicResult topicResult;

            
            asr.Users = users.ToList();
            asr.SelectedCurriculumIds = selectedCurriculumIds;
            asr.Curriculums = _LmsService.FindService<ICurriculumService>().GetCurriculums(curr => selectedCurriculumIds.Contains(curr.Id));

            foreach (User usr in asr.Users)
            {
                specializedResult = new SpecializedResult();
                specializedResult.Disciplines = _LmsService.FindService<IDisciplineService>().GetDisciplines(asr.Curriculums.Select(curr=> curr.DisciplineRef));
                foreach (Discipline discipline in specializedResult.Disciplines)
                {
                    curRes = new DisciplineResult();
                    curRes.Topics = _LmsService.FindService<IDisciplineService>().GetTopicsByDisciplineId(discipline.Id);
                    
                    #region TopicResult

                    foreach (Topic topic in curRes.Topics)
                    {
                        topicResult = new TopicResult(usr, topic);
                        throw new NotImplementedException(
                            "Statistics was not implemented due to new design of Discipline/Curriculum services");
                        // topicResult.AttemptResults = _LmsService.FindService<ITestingService>().GetResults(usr, );
                        // topicResult.Res = topicResult.GetTopicResultScore();
                        // curRes.TopicResult.Add(topicResult);
                    }

                    #endregion

                    curRes.CalculateSumAndMax(usr, discipline);
                    specializedResult.DisciplineResult.Add(curRes);
                }
                specializedResult.CalculateSpecializedResult(usr);
                asr.SpecializedResults.Add(specializedResult);
            }
            return asr;
        }
    }
}