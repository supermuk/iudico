using System.Collections.Generic;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.DisciplineManagement;
using IUDICO.Common.Models.Shared.Statistics;

namespace IUDICO.TestingSystem.Models
{
    public interface IMlcProxy
    {
        //IEnumerable<Training> GetTrainings(long userKey);
        long GetAttemptId(int curriculumChapterTopicId, int courseId, TopicTypeEnum topicType);
        IEnumerable<AttemptResult> GetAllAttempts();
        IEnumerable<AttemptResult> GetResults(User user, Topic topic);
        IEnumerable<AttemptResult> GetResults(User user);
        IEnumerable<AttemptResult> GetResults(Topic topic);
        IEnumerable<AnswerResult> GetAnswers(AttemptResult attemptResult);
        //Training AddPackage(Package package);
        //void DeletePackage(long packId);
    }
}
