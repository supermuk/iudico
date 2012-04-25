using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.DisciplineManagement;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.DisciplineManagement.Models;
using IUDICO.DisciplineManagement.Models.Storage;

namespace IUDICO.UnitTests.CurriculumManagement
{
    public class DataPreparer
    {
        #region Private properties

        private readonly IDisciplineStorage disciplineStorage;

        private readonly ICurriculumStorage curriculumStorage;

        private readonly ICourseService courseService;

        private readonly IUserService userService;

        #endregion

        #region Public properties

        public List<int> CurriculumIds { get; private set; }

        public List<int> DisciplineIds { get; private set; }

        public List<int> ChapterIds { get; private set; }

        public List<int> TopicIds { get; private set; }

        #endregion

        #region Public methods

        #region Get data

        public DataPreparer(IDisciplineStorage disciplineStorage, ICurriculumStorage curriculumStorage, ILmsService lmsService)
        {
            this.disciplineStorage = disciplineStorage;
            this.curriculumStorage = curriculumStorage;
            this.courseService = lmsService.FindService<ICourseService>();
            this.userService = lmsService.FindService<IUserService>();
        }

        public IList<Course> GetCourses()
        {
            return new[]
            {
                new Course { Id = 1, Name = "Course1", Owner = "panza" },
                new Course { Id = 2, Name = "Course2", Owner = "panza" },
                new Course { Id = 3, Name = "Course3", Owner = "ozo" }
            };
        }

        public IList<User> GetUsers()
        {
            return new[]
            {
                new User
                {
                    Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    Username = Users.Panza,
                    Email = "ipetrovych@gmail.com",
                    Password = string.Empty,
                },
                new User
                {
                    Id = new Guid("bbbbbbbb-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    Username = Users.Ozo,
                    Email = "boblox@gmail.com",
                    Password = string.Empty,
                }
            };
        }

        public IList<Group> GetGroups()
        {
            return new[]
                {
                    new Group { Id = 1, Name = "PMI51", Deleted = false },
                    new Group { Id = 2, Name = "PMI31", Deleted = false }
                };
        }

        public IList<GroupUser> GetGroupUsers(IList<Group> groups, IList<User> users)
        {
            return new[]
                {
                    new GroupUser { GroupRef = groups[0].Id, UserRef = users[0].Id },
                    new GroupUser { GroupRef = groups[1].Id, UserRef = users[1].Id }
                };
        }

        public IList<TopicType> GetTopicTypes()
        {
            return new[]
                {
                    new TopicType { Id = 1, Name = "Test" }, 
                    new TopicType { Id = 2, Name = "Theory" },
                    new TopicType { Id = 3, Name = "TestWithoutCourse" }
                };
        }

        public List<Discipline> GetDisciplines()
        {
            return Enumerable.Range(1, 3)
                .Select(i => new Discipline
                {
                    Name = string.Format("Discipline{0}", i),
                    Owner = Users.Panza,
                    IsValid = true
                })
                .ToList();
        }

        public List<Chapter> GetChapters()
        {
            return DisciplineIds.Select(i => new Chapter
            {
                DisciplineRef = i,
                Name = string.Format("Chapter{0}", i),
            })
            .ToList();
        }

        public List<Topic> GetTopics()
        {
            var topics = ChapterIds.Select(i => new Topic
            {
                SortOrder = 1,
                TestCourseRef = this.courseService.GetCourse(2).Id,
                TestTopicTypeRef = this.disciplineStorage.GetTestTopicTypes().First(item => item.ToTopicTypeEnum() == TopicTypeEnum.Test).Id,
                TheoryCourseRef = this.courseService.GetCourse(1).Id,
                TheoryTopicTypeRef = this.disciplineStorage.GetTheoryTopicTypes().First(item => item.ToTopicTypeEnum() == TopicTypeEnum.Theory).Id,
                Name = string.Format("TopicA{0}", i),
                ChapterRef = i
            })
            .ToList();
            topics.AddRange(ChapterIds.Select(i => new Topic
            {
                SortOrder = 2,
                TestCourseRef = -1,
                TestTopicTypeRef = this.disciplineStorage.GetTestTopicTypes().First(item => item.ToTopicTypeEnum() == TopicTypeEnum.TestWithoutCourse).Id,
                TheoryCourseRef = null,
                TheoryTopicTypeRef = null,
                Name = string.Format("TopicB{0}", i),
                ChapterRef = i,
            })
            .ToList());
            topics.AddRange(ChapterIds.Select(i => new Topic
            {
                SortOrder = 3,
                TestCourseRef = null,
                TestTopicTypeRef = null,
                TheoryCourseRef = this.courseService.GetCourse(2).Id,
                TheoryTopicTypeRef = this.disciplineStorage.GetTheoryTopicTypes().First(item => item.ToTopicTypeEnum() == TopicTypeEnum.Theory).Id,
                Name = string.Format("TopicC{0}", i),
                ChapterRef = i,
            })
            .ToList());

            return topics;
        }

        public List<Curriculum> GetCurriculums()
        {
            return DisciplineIds.Select(i => new Curriculum
                {
                    DisciplineRef = i,
                    UserGroupRef = i % 2 != 0
                        ? this.userService.GetGroup(1).Id
                        : this.userService.GetGroup(2).Id,
                    IsValid = true
                }
            )
            .ToList();
        }

        #endregion

        #region Create data

        #region Discipline sets

        /// <summary>
        /// Creates disciplines.
        /// </summary>
        /// <returns></returns>
        public List<int> CreateDisciplinesSet1()
        {
            return DisciplineIds = GetDisciplines().Select(item => this.disciplineStorage.AddDiscipline(item)).ToList();
        }

        /// <summary>
        /// Creates disciplines and curriculums.
        /// </summary>
        /// <returns></returns>
        public List<int> CreateDisciplinesSet2()
        {
            CreateDisciplinesSet1();
            CurriculumIds = GetCurriculums().Select(item => this.curriculumStorage.AddCurriculum(item)).ToList();
            return DisciplineIds;
        }

        /// <summary>
        /// Creates disciplines, stages and curriculums.
        /// </summary>
        /// <returns></returns>
        public List<int> CreateDisciplinesSet3()
        {
            CreateDisciplinesSet2();
            return ChapterIds = GetChapters().Select(item => this.disciplineStorage.AddChapter(item)).ToList();
        }

        /// <summary>
        /// Creates disciplines, stages, topics and curriculums.
        /// </summary>
        /// <returns></returns>
        public List<int> CreateDisciplinesSet4()
        {
            CreateDisciplinesSet3();
            return TopicIds = GetTopics().Select(item => this.disciplineStorage.AddTopic(item)).ToList();
        }

        #endregion

        #region Curriculum sets

        /// <summary>
        /// Creates disciplines, stages and topics 
        /// </summary>
        /// <returns></returns>
        public List<int> CreateCurriculumsSet1()
        {
            DisciplineIds = GetDisciplines().Select(item => this.disciplineStorage.AddDiscipline(item)).ToList();
            ChapterIds = GetChapters().Select(item => this.disciplineStorage.AddChapter(item)).ToList();
            TopicIds = GetTopics().Select(item => this.disciplineStorage.AddTopic(item)).ToList();
            return DisciplineIds;
        }

        #endregion

        #endregion

        #endregion
    }
}
