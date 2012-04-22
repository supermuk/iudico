using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Shared;

namespace IUDICO.DisciplineManagement.Models.Storage
{
    public class CachedDisciplineStorage: IDisciplineStorage
    {
        public User GetCurrentUser()
        {
            throw new NotImplementedException();
        }

        public IList<Course> GetCourses()
        {
            throw new NotImplementedException();
        }

        public Course GetCourse(int id)
        {
            throw new NotImplementedException();
        }

        public Group GetGroup(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Group> GetGroups()
        {
            throw new NotImplementedException();
        }

        public IList<Group> GetGroupsByUser(User user)
        {
            throw new NotImplementedException();
        }

        public IList<Curriculum> GetCurriculums(Func<Curriculum, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IList<Discipline> GetDisciplines()
        {
            throw new NotImplementedException();
        }

        public IList<Discipline> GetDisciplines(Func<Discipline, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IList<Discipline> GetDisciplines(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public IList<Discipline> GetDisciplines(User user)
        {
            throw new NotImplementedException();
        }

        public Discipline GetDiscipline(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Discipline> GetDisciplinesByGroupId(int groupId)
        {
            throw new NotImplementedException();
        }

        public int AddDiscipline(Discipline discipline)
        {
            throw new NotImplementedException();
        }

        public void UpdateDiscipline(Discipline discipline)
        {
            throw new NotImplementedException();
        }

        public void DeleteDiscipline(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteDisciplines(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public IList<ShareUser> GetDisciplineSharedUsers(int disciplineId)
        {
            throw new NotImplementedException();
        }

        public IList<ShareUser> GetDisciplineNotSharedUsers(int disciplineId)
        {
            throw new NotImplementedException();
        }

        public void UpdateDisciplineSharing(int disciplineId, IEnumerable<Guid> sharewith)
        {
            throw new NotImplementedException();
        }

        public Chapter GetChapter(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Chapter> GetChapters(Func<Chapter, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IList<Chapter> GetChapters(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public int AddChapter(Chapter chapter)
        {
            throw new NotImplementedException();
        }

        public void UpdateChapter(Chapter chapter)
        {
            throw new NotImplementedException();
        }

        public void DeleteChapter(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteChapters(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Topic GetTopic(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Topic> GetTopics(Func<Topic, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IList<Topic> GetTopics(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public IList<Topic> GetTopicsByDisciplineId(int disciplineId)
        {
            throw new NotImplementedException();
        }

        public IList<Topic> GetTopicsByGroupId(int groupId)
        {
            throw new NotImplementedException();
        }

        public IList<Topic> GetTopicsByCourseId(int courseId)
        {
            throw new NotImplementedException();
        }

        public IList<Topic> GetTopicsOwnedByUser(User owner)
        {
            throw new NotImplementedException();
        }

        public IList<Group> GetGroupsAssignedToTopic(int topicId)
        {
            throw new NotImplementedException();
        }

        public int AddTopic(Topic topic)
        {
            throw new NotImplementedException();
        }

        public void UpdateTopic(Topic topic)
        {
            throw new NotImplementedException();
        }

        public void DeleteTopic(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteTopics(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Topic TopicUp(int topicId)
        {
            throw new NotImplementedException();
        }

        public Topic TopicDown(int topicId)
        {
            throw new NotImplementedException();
        }

        public TopicType GetTopicType(int id)
        {
            throw new NotImplementedException();
        }

        public IList<TopicType> GetTopicTypes()
        {
            throw new NotImplementedException();
        }

        public IList<TopicType> GetTheoryTopicTypes()
        {
            throw new NotImplementedException();
        }

        public IList<TopicType> GetTestTopicTypes()
        {
            throw new NotImplementedException();
        }
    }
}