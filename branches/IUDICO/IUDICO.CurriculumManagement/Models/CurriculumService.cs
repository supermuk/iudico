using System.Collections.Generic;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.DisciplineManagement;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.Common.Models.Shared.CurriculumManagement;

namespace IUDICO.CurriculumManagement.Models
{
    public class CurriculumService : ICurriculumService
    {
        private readonly ICurriculumStorage _curriculumStorage;

        public CurriculumService(ICurriculumStorage curriculumStorage)
        {
            _curriculumStorage = curriculumStorage;
        }

        public IList<Curriculum> GetCurriculumsByGroupId(int groupId)
        {
            return _curriculumStorage.GetCurriculumsByGroupId(groupId);
        }

        public IList<TopicDescription> GetTopicDescriptions(User user)
        {
            return _curriculumStorage.GetTopicDescriptions(user);
        }

        public CurriculumChapterTopic GetCurriculumChapterTopicById(int curriculumChapterTopicId)
        {
            return _curriculumStorage.GetCurriculumChapterTopic(curriculumChapterTopicId);
        }

        public bool CanPassCurriculumChapterTopic(User user, CurriculumChapterTopic curriculumChapterTopic, TopicTypeEnum topicType)
        {
            return _curriculumStorage.CanPassCurriculumChapterTopic(user, curriculumChapterTopic, topicType);
        }
    }
}