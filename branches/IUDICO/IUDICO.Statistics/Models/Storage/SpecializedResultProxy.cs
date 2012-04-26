using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Statistics.Models.Storage
{
    public class SpecializedResultProxy
    {
        private ILmsService lmsService;

        public AllSpecializedResults GetResults(IEnumerable<User> users, int[] selectedCurriculumIds, ILmsService lmsService)
        {
            this.lmsService = lmsService;
            var asr = new AllSpecializedResults();
            SpecializedResult specializedResult;
            DisciplineResult curRes;
            TopicResult topicResult;

            
            asr.Users = users.ToList();
            asr.SelectedCurriculumIds = selectedCurriculumIds;
            asr.Curriculums = this.lmsService.FindService<ICurriculumService>().GetCurriculums(curr => selectedCurriculumIds.Contains(curr.Id));

            foreach (var usr in asr.Users)
            {
                specializedResult = new SpecializedResult
                    {
                        Disciplines =
                            this.lmsService.FindService<IDisciplineService>().GetDisciplines(
                                asr.Curriculums.Select(curr => curr.DisciplineRef))
                    };

                foreach (var discipline in specializedResult.Disciplines)
                {
                    curRes = new DisciplineResult
                        {
                            Topics =
                                this.lmsService.FindService<IDisciplineService>().GetTopicsByDisciplineId(discipline.Id)
                        };

                    #region TopicResult

                    foreach (var topic in curRes.Topics)
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