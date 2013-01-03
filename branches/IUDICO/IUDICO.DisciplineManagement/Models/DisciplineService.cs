using System.Collections.Generic;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.DisciplineManagement.Models.Storage;
using System;

namespace IUDICO.DisciplineManagement.Models
{
    public class DisciplineService : IDisciplineService
    {
        private readonly IDisciplineStorage disciplineStorage;

        public DisciplineService(IDisciplineStorage disciplineStorage)
        {
            this.disciplineStorage = disciplineStorage;
        }

        #region IDisciplineService Members

        public IList<Discipline> GetDisciplines(IEnumerable<int> ids)
        {
            return this.disciplineStorage.GetDisciplines(ids);
        }

        public Discipline GetDiscipline(int id)
        {
            return this.disciplineStorage.GetDiscipline(id);
        }

        public IList<Discipline> GetDisciplines()
        {
            return this.disciplineStorage.GetDisciplines();
        }

        public IList<Discipline> GetDisciplines(User user)
        {
            return this.disciplineStorage.GetDisciplines(user);
        }

        public IList<Discipline> GetDisciplines(Func<Discipline, bool> predicate)
        {
            return this.disciplineStorage.GetDisciplines(predicate);
        }

        public IList<Chapter> GetChapters(int disciplineId)
        {
            return this.disciplineStorage.GetChapters(item => item.DisciplineRef == disciplineId);
        }

        public IList<Chapter> GetChapters(IEnumerable<int> ids)
        {
            return this.disciplineStorage.GetChapters(ids);
        }

        public Chapter GetChapter(int id)
        {
            return this.disciplineStorage.GetChapter(id);
        }

        public IList<Topic> GetTopicsByChapterId(int chapterId)
        {
            return this.disciplineStorage.GetTopics(item => item.ChapterRef == chapterId);
        }

        public IList<Topic> GetTopics(IEnumerable<int> ids)
        {
            return this.disciplineStorage.GetTopics(ids);
        }

        public IList<Topic> GetTopics()
        {
            return this.disciplineStorage.GetTopics(item => true);
        }

        public Topic GetTopic(int id)
        {
            return this.disciplineStorage.GetTopic(id);
        }

        public IEnumerable<Discipline> GetDisciplinesByGroupId(int groupId)
        {
            return this.disciplineStorage.GetDisciplinesByGroupId(groupId);
        }

        public IList<Topic> GetTopicsByDisciplineId(int disciplineId)
        {
            return this.disciplineStorage.GetTopicsByDisciplineId(disciplineId);
        }

        public IEnumerable<Topic> GetTopicsByGroupId(int groupId)
        {
            return this.disciplineStorage.GetTopicsByGroupId(groupId);
        }

        public IEnumerable<Topic> GetTopicsOwnedByUser(User owner)
        {
            return this.disciplineStorage.GetTopicsOwnedByUser(owner);
        }

        public TopicType GetTopicType(int id)
        {
            return this.disciplineStorage.GetTopicType(id);
        }

        public IList<TopicType> GetTopicTypes()
        {
            return this.disciplineStorage.GetTopicTypes();
        }

        public IList<TopicType> GetTheoryTopicTypes()
        {
            return this.disciplineStorage.GetTheoryTopicTypes();
        }

        public IList<TopicType> GetTestTopicTypes()
        {
            return this.disciplineStorage.GetTestTopicTypes();
        }

        public void DateUpdating(int topicId)
        {
            this.disciplineStorage.DateUpdating(topicId);
        }
        #endregion
    }
}