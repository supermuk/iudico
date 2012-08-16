using System;
using System.Data.Linq;
using System.Linq;

using IUDICO.Analytics.Models.Storage;
using IUDICO.Common.Models.Caching.Provider;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.DisciplineManagement.Models;
using IUDICO.DisciplineManagement.Models.Storage;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.LMS.Models;

using Moq;
using Moq.Protected;
using Castle.Windsor;

using IDataContext = IUDICO.Analytics.Models.IDataContext;

namespace IUDICO.UnitTests.Analytics
{
    public class AnalyticsTests
    {
        #region Protected members

        protected static AnalyticsTests instance;

        #endregion

        #region Public properties

        public Mock<IDataContext> MockDataContext { get; protected set; }

        public Mock<LmsService> MockLmsService { get; protected set; }

        public Mock<TestMixedAnalyticsStorage> MockMixedStorage { get; protected set; }

        public Mock<CachedAnalyticsStorage> MockStorage { get; protected set; }

        public Mock<IWindsorContainer> MockWindsorContainer { get; protected set; }

        public Mock<IDisciplineService> MockDisciplineService { get; protected set; }

        public Mock<IUserService> MockUserService { get; protected set; }

        public Mock<ITestingService> MockTestingService { get; protected set; }


        public ITestingService TestingService
        {
            get
            {
                return this.MockTestingService.Object;
            }
        }

        public IUserService UserService
        {
            get
            {
                return this.MockUserService.Object;
            }
        }

        public IDisciplineService DisciplineService
        {
            get
            {
                return this.MockDisciplineService.Object;
            }
        }

        public IDataContext DataContext
        {
            get
            {
                return this.MockDataContext.Object;
            }
        }

        public ILmsService LmsService
        {
            get
            {
                return this.MockLmsService.Object;
            }
        }

        public TestMixedAnalyticsStorage Storage
        {
            get
            {
                return this.MockMixedStorage.Object;
            }
        }

        public IWindsorContainer WindsorContainer
        {
            get
            {
                return this.MockWindsorContainer.Object;
            }
        }

        #region Tables

        public Mock<ITable> ForecastingTrees { get; protected set; }

        public Mock<ITable> TopicScores { get; protected set; }

        public Mock<ITable> UserScores { get; protected set; }

        public Mock<ITable> Tags { get; protected set; }

        public Mock<ITable> TopicTags { get; protected set; }

        public Guid UserId { get; protected set; }

        #endregion Tables

        #endregion

        private AnalyticsTests()
        {
            var mockCacheProvider = new Mock<HttpCache>();

            this.MockDataContext = new Mock<IDataContext>();
            this.MockWindsorContainer = new Mock<IWindsorContainer>();
            this.MockLmsService = new Mock<LmsService>(this.MockWindsorContainer.Object);
            this.MockMixedStorage = new Mock<TestMixedAnalyticsStorage>(this.MockLmsService.Object);
            
            this.MockDisciplineService = new Mock<IDisciplineService>();
            this.MockUserService = new Mock<IUserService>();
            this.MockTestingService = new Mock<ITestingService>();

            this.MockStorage = new Mock<CachedAnalyticsStorage>(this.MockMixedStorage.Object, mockCacheProvider.Object);

            this.ForecastingTrees = new Mock<ITable>();
            this.TopicScores = new Mock<ITable>();
            this.UserScores = new Mock<ITable>();
            this.Tags = new Mock<ITable>();
            this.TopicTags = new Mock<ITable>();

            this.Setup();
            this.SetupTables();

            this.SetupServices();
        }

        public static AnalyticsTests GetInstance()
        {
            return instance ?? (instance = new AnalyticsTests());
        }

        public void Setup()
        {
            this.MockMixedStorage.Protected().Setup<IDataContext>("GetDbContext").Returns(this.MockDataContext.Object);
            
            this.MockWindsorContainer.Setup(l => l.Resolve<IDisciplineService>()).Returns(this.MockDisciplineService.Object);
            this.MockWindsorContainer.Setup(l => l.Resolve<IUserService>()).Returns(this.MockUserService.Object);
            this.MockWindsorContainer.Setup(l => l.Resolve<ITestingService>()).Returns(this.MockTestingService.Object);
        }

        public void SetupTables()
        {
            var mockTagsData = new[]
                {
                    new Tag { Id = 1, Name = "C++" },
                    new Tag { Id = 2, Name = "C++ STL" },
                    new Tag { Id = 3, Name = "C#" },
                    new Tag { Id = 4, Name = "C#.NET" }
                };

            var mockTopicTagsData = new[]
                {
                    new TopicTag { TopicId = 1, TagId = 1 },
                    new TopicTag { TopicId = 1, TagId = 2 },
                    new TopicTag { TopicId = 1, TagId = 4 },
                    new TopicTag { TopicId = 2, TagId = 1 },
                    new TopicTag { TopicId = 2, TagId = 2 },
                    new TopicTag { TopicId = 2, TagId = 4 }
                };

            this.UserId = Guid.NewGuid();

            var mockUserScoresData = new[]
                {
                    new UserScore { Score = 80, TagId = 1, UserId = this.UserId },
                    new UserScore { Score = 90, TagId = 2, UserId = this.UserId },
                    new UserScore { Score = 60, TagId = 3, UserId = this.UserId }
                };

            var mockTopicScoresData = new[]
                {
                    new TopicScore { Score = 40, TagId = 1, TopicId = 1 },
                    new TopicScore { Score = 40, TagId = 2, TopicId = 1 },
                    new TopicScore { Score = 40, TagId = 4, TopicId = 1 }
                };

            var mockTags = new MemoryTable<Tag>(mockTagsData);
            var mockTopicTags = new MemoryTable<TopicTag>(mockTopicTagsData);
            var mockUserScores = new MemoryTable<UserScore>(mockUserScoresData);
            var mockTopicScores = new MemoryTable<TopicScore>(mockTopicScoresData);

            this.MockDataContext.SetupGet(c => c.Tags).Returns(mockTags);
            this.MockDataContext.SetupGet(c => c.TopicTags).Returns(mockTopicTags);
            this.MockDataContext.SetupGet(c => c.UserScores).Returns(mockUserScores);
            this.MockDataContext.SetupGet(c => c.TopicScores).Returns(mockTopicScores);
        }

        public void SetupServices()
        {
            var topics = new[]
            {
                new Topic
                    {
                        Id = 1,
                        ChapterRef = 1,
                        Created = DateTime.Now,
                        IsDeleted = false,
                        Name = "Topic 1",
                        SortOrder = 1,
                        TestCourseRef = 1
                    },
                new Topic
                {
                    Id = 2,
                    ChapterRef = 1,
                    Created = DateTime.Now,
                    IsDeleted = false,
                    Name = "Topic 2",
                    SortOrder = 2,
                    TestCourseRef = 1
                },
            };

            this.MockDisciplineService.Setup(c => c.GetTopics()).Returns(topics);
            this.MockDisciplineService.Setup(c => c.GetTopic(1)).Returns(topics[0]);
            this.MockDisciplineService.Setup(c => c.GetTopic(2)).Returns(topics[1]);

            var users = new[]
                {
                    new User
                        {
                            Id = this.UserId, 
                            Username = "panza", 
                            Email = "ipetrovych@gmail.com", 
                        }, 
                    new User
                        {
                            Id = Guid.NewGuid(), 
                            Username = "ip", 
                            Email = "ip@ip.com", 
                        }, 
                };

            var group = new Group { Id = 1, Name = "PMI31", Deleted = false };

            this.MockUserService.Setup(c => c.GetGroup(1)).Returns(group);
            this.MockUserService.Setup(c => c.GetUsers()).Returns(users);
            this.MockUserService.Setup(c => c.GetCurrentUser()).Returns(users[0]);
            this.MockUserService.Setup(c => c.GetUsers(It.IsAny<Func<User, bool>>())).Returns((Func<User, bool> expr) => (users.Where(expr)));

            this.MockUserService.Setup(c => c.GetCurrentUser()).Returns(users[0]);

            var attemptResults = new[]
                {
                    new AttemptResult
                        {
                            AttemptId = 1,
                            Score = new Score(0, 50, 25, 0.5F),
                            User = users[0],
                            CurriculumChapterTopic = new CurriculumChapterTopic
                                {
                                    TopicRef = 1,
                                }
                        },
                    new AttemptResult
                        {
                            AttemptId = 2,
                            Score = new Score(0, 50, 35, 0.7F),
                            User = users[1],
                            CurriculumChapterTopic = new CurriculumChapterTopic
                                {
                                    TopicRef = 1,
                                }
                        },
                    new AttemptResult
                        {
                            AttemptId = 3,
                            Score = new Score(0, 50, 45, 0.9F),
                            User = users[0],
                            CurriculumChapterTopic = new CurriculumChapterTopic
                                {
                                    TopicRef = 2,
                                }
                        },
                    new AttemptResult
                        {
                            AttemptId = 3,
                            Score = new Score(0, 50, 30, 0.6F),
                            User = users[1],
                            CurriculumChapterTopic = new CurriculumChapterTopic
                                {
                                    TopicRef = 2,
                                }
                        },
                };

            this.MockTestingService.Setup(c => c.GetResults(topics[0])).Returns(attemptResults.Where(r => r.CurriculumChapterTopic.TopicRef == topics[0].Id));
            this.MockTestingService.Setup(c => c.GetResults(topics[1])).Returns(attemptResults.Where(r => r.CurriculumChapterTopic.TopicRef == topics[1].Id));
            this.MockTestingService.Setup(c => c.GetResults(users[0])).Returns(attemptResults.Where(r => r.User.Id == users[0].Id));
            this.MockTestingService.Setup(c => c.GetResults(users[1])).Returns(attemptResults.Where(r => r.User.Id == users[1].Id));
        }
    }
}
