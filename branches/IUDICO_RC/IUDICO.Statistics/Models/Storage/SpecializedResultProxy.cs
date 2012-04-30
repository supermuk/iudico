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

        public AllSpecializedResults GetResults(IEnumerable<User> users, int[] selectedCurriculumIds, ILmsService lmsServiceParam)
        {
            this.lmsService = lmsServiceParam;
            var asr = new AllSpecializedResults
                {
                    Users = users.ToList(),
                    // SelectedDisciplineIds = selectedCurriculumIds,
                    Curriculums =
                        this.lmsService.FindService<ICurriculumService>().GetCurriculums(
                            curr => selectedCurriculumIds.Contains(curr.Id))
                };


            foreach (var usr in asr.Users)
            {
                var specializedResult = new SpecializedResult
                    {
                        Curriculums = asr.Curriculums
                    };

                foreach (var curriculum in specializedResult.Curriculums)
                {
                    var disciplineResult = new DisciplineResult
                        {
                            CurriculumChapterTopics =
                                this.lmsService.FindService<ICurriculumService>().GetCurriculumChapterTopicsByCurriculumId(curriculum.Id)
                        };

                    #region TopicResult

                    foreach (var curriculumChapterTopic in disciplineResult.CurriculumChapterTopics)
                    {
                        var topicResult = new TopicResult(usr, curriculumChapterTopic)
                            {
                                AttemptResults =
                                    this.lmsService.FindService<ITestingService>().GetResults(
                                        usr, curriculumChapterTopic)
                            };
                        topicResult.Res = topicResult.GetTopicResultScore();
                        disciplineResult.TopicResults.Add(topicResult);
                    }

                    #endregion

                    disciplineResult.CalculateSumAndMax(usr, curriculum);
                    specializedResult.DisciplineResults.Add(disciplineResult);
                }
                specializedResult.CalculateSpecializedResult(usr);
                asr.SpecializedResults.Add(specializedResult);
            }
            return asr;
        }
    }
}