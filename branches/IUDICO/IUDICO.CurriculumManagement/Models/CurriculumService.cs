using System;
using System.Collections.Generic;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.Common.Models.Shared.CurriculumManagement;

namespace IUDICO.CurriculumManagement.Models
{
    public class CurriculumService : ICurriculumService
    {
        private readonly ICurriculumStorage _CurriculumStorage;

        public CurriculumService(ICurriculumStorage curriculumStorage)
        {
            _CurriculumStorage = curriculumStorage;
        }

        #region IDisciplineService Members

        //public IEnumerable<Discipline> GetDisciplines()
        //{
        //    return _DisciplineStorage.GetDisciplines();
        //}

        public IEnumerable<Discipline> GetDisciplines(IEnumerable<int> ids)
        {
            return _CurriculumStorage.GetDisciplines(ids);
        }

        public Discipline GetDiscipline(int id)
        {
            return _CurriculumStorage.GetDiscipline(id);
        }

        public IEnumerable<Discipline> GetDisciplinesWithTopicsOwnedByUser(User user)
        {
            return _CurriculumStorage.GetDisciplinesWithTopicsOwnedByUser(user);
        }

        public IEnumerable<Discipline> GetDisciplines()
        {
            return _CurriculumStorage.GetDisciplines();
        }

        public IEnumerable<Discipline> GetDisciplines(User user)
        {
            return _CurriculumStorage.GetDisciplines(user);
        }

        public IEnumerable<Chapter> GetChapters(int disciplineId)
        {
            return _CurriculumStorage.GetChapters(disciplineId);
        }

        public IEnumerable<Chapter> GetChapters(IEnumerable<int> ids)
        {
            return _CurriculumStorage.GetChapters(ids);
        }

        public Chapter GetChapter(int id)
        {
            return _CurriculumStorage.GetChapter(id);
        }

        public IEnumerable<Topic> GetTopicsByChapterId(int chapterId)
        {
            return _CurriculumStorage.GetTopicsByChapterId(chapterId);
        }

        public IEnumerable<Topic> GetTopics(IEnumerable<int> ids)
        {
            return _CurriculumStorage.GetTopics(ids);
        }

        public IEnumerable<Topic> GetTopics()
        {
            return _CurriculumStorage.GetTopics();
        }

        public Topic GetTopic(int id)
        {
            return _CurriculumStorage.GetTopic(id);
        }

        public IEnumerable<Group> GetGroupsAssignedToTopic(int topicId)
        {
            return _CurriculumStorage.GetGroupsAssignedToTopic(topicId);
        }

        public IEnumerable<Discipline> GetDisciplinesByGroupId(int groupId)
        {
            return _CurriculumStorage.GetDisciplinesByGroupId(groupId);
        }

        public IEnumerable<Topic> GetTopicsByDisciplineId(int disciplineId)
        {
            return _CurriculumStorage.GetTopicsByDisciplineId(disciplineId);
        }

        public IEnumerable<Topic> GetTopicsByGroupId(int groupId)
        {
            return _CurriculumStorage.GetTopicsByGroupId(groupId);
        }

        public IEnumerable<Topic> GetTopicsOwnedByUser(User owner)
        {
            return _CurriculumStorage.GetTopicsOwnedByUser(owner);
        }

        public IEnumerable<TopicDescription> GetTopicsAvailableForUser(User user)
        {
            return _CurriculumStorage.GetTopicsAvailableForUser(user);
        }

        public TopicType GetTopicType(int id)
        {
            return _CurriculumStorage.GetTopicType(id);
        }

        public IEnumerable<TopicType> GetTopicTypes()
        {
            return _CurriculumStorage.GetTopicTypes();
        }

        public List<TopicType> GetTheoryTopicTypes()
        {
            return _CurriculumStorage.GetTheoryTopicTypes();
        }

        public List<TopicType> GetTestTopicTypes()
        {
            return _CurriculumStorage.GetTestTopicTypes();
        }

        #endregion
    }
}