using System.Collections.Generic;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.DisciplineManagement;
using IUDICO.Common.Models.Shared.Statistics;

namespace IUDICO.TestingSystem.Models
{
    public interface IMlcProxy
    {
        long GetAttemptId(int curriculumChapterTopicId, int courseId, TopicTypeEnum topicType);
        IEnumerable<AttemptResult> GetResults();
        IEnumerable<AttemptResult> GetResults(User user, CurriculumChapterTopic curriculumChapterTopic);
        IEnumerable<AttemptResult> GetResults(User user, CurriculumChapterTopic curriculumChapterTopic, TopicTypeEnum topicType);
        IEnumerable<AttemptResult> GetResults(User user);
        IEnumerable<AttemptResult> GetResults(Topic topic);
        IEnumerable<AttemptResult> GetResults(CurriculumChapterTopic curriculumChapterTopic);
        IEnumerable<AnswerResult> GetAnswers(AttemptResult attemptResult);
    }
}
