using System.Collections.Generic;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.DisciplineManagement.Models.Storage;

namespace IUDICO.DisciplineManagement.Models
{
    public class DisciplineService : IDisciplineService
    {
        private readonly IDisciplineStorage _disciplineStorage;

        public DisciplineService(IDisciplineStorage disciplineStorage)
        {
            _disciplineStorage = disciplineStorage;
        }

        #region IDisciplineService Members

        public IList<Discipline> GetDisciplines(IEnumerable<int> ids)
        {
            return _disciplineStorage.GetDisciplines(ids);
        }

        public Discipline GetDiscipline(int id)
        {
            return _disciplineStorage.GetDiscipline(id);
        }

        //public IList<Discipline> GetDisciplinesWithTopicsOwnedByUser(User user)
        //{
        //    return _disciplineStorage.GetDisciplinesWithTopicsOwnedByUser(user);
        //}

        public IList<Discipline> GetDisciplines()
        {
            return _disciplineStorage.GetDisciplines();
        }

        public IList<Discipline> GetDisciplines(User user)
        {
            return _disciplineStorage.GetDisciplines(user);
        }

        public IList<Chapter> GetChapters(int disciplineId)
        {
            return _disciplineStorage.GetChapters(item => item.DisciplineRef == disciplineId);
        }

        public IList<Chapter> GetChapters(IEnumerable<int> ids)
        {
            return _disciplineStorage.GetChapters(ids);
        }

        public Chapter GetChapter(int id)
        {
            return _disciplineStorage.GetChapter(id);
        }

        public IList<Topic> GetTopicsByChapterId(int chapterId)
        {
            return _disciplineStorage.GetTopics(item => item.ChapterRef == chapterId);
        }

        public IList<Topic> GetTopics(IEnumerable<int> ids)
        {
            return _disciplineStorage.GetTopics(ids);
        }

        public IList<Topic> GetTopics()
        {
            return _disciplineStorage.GetTopics(item => true);
        }

        public Topic GetTopic(int id)
        {
            return _disciplineStorage.GetTopic(id);
        }

        public IList<Group> GetGroupsAssignedToTopic(int topicId)
        {
            return _disciplineStorage.GetGroupsAssignedToTopic(topicId);
        }

        public IEnumerable<Discipline> GetDisciplinesByGroupId(int groupId)
        {
            return _disciplineStorage.GetDisciplinesByGroupId(groupId);
        }

        public IList<Topic> GetTopicsByDisciplineId(int disciplineId)
        {
            return _disciplineStorage.GetTopicsByDisciplineId(disciplineId);
        }

        public IEnumerable<Topic> GetTopicsByGroupId(int groupId)
        {
            return _disciplineStorage.GetTopicsByGroupId(groupId);
        }

        public IEnumerable<Topic> GetTopicsOwnedByUser(User owner)
        {
            return _disciplineStorage.GetTopicsOwnedByUser(owner);
        }

        public TopicType GetTopicType(int id)
        {
            return _disciplineStorage.GetTopicType(id);
        }

        public IList<TopicType> GetTopicTypes()
        {
            return _disciplineStorage.GetTopicTypes();
        }

        public IList<TopicType> GetTheoryTopicTypes()
        {
            return _disciplineStorage.GetTheoryTopicTypes();
        }

        public IList<TopicType> GetTestTopicTypes()
        {
            return _disciplineStorage.GetTestTopicTypes();
        }

        #endregion
    }
}