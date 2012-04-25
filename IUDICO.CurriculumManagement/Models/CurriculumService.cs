using System;
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
        private readonly ICurriculumStorage curriculumStorage;

        public CurriculumService(ICurriculumStorage curriculumStorage)
        {
            this.curriculumStorage = curriculumStorage;
        }

        public IList<Curriculum> GetCurriculums(Func<Curriculum, bool> predicate)
        {
            return this.curriculumStorage.GetCurriculums(predicate);
        }

        public IList<TopicDescription> GetTopicDescriptions(User user)
        {
            return this.curriculumStorage.GetTopicDescriptions(user);
        }

        public IEnumerable<TopicDescription> GetTopicDescriptionsByTopics(IEnumerable<Topic> topics, User user)
        {
            return this.curriculumStorage.GetTopicDescriptionsByTopics(topics, user);
        }

        public CurriculumChapterTopic GetCurriculumChapterTopicById(int curriculumChapterTopicId)
        {
            return this.curriculumStorage.GetCurriculumChapterTopic(curriculumChapterTopicId);
        }

        public bool CanPassCurriculumChapterTopic(User user, CurriculumChapterTopic curriculumChapterTopic, TopicTypeEnum topicType)
        {
            return this.curriculumStorage.CanPassCurriculumChapterTopic(user, curriculumChapterTopic, topicType);
        }
    }
}