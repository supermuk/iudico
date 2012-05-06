using System.Data.Linq;

using IUDICO.Analytics.Models;
using IUDICO.Analytics.Models.Storage;
using IUDICO.Common.Models.Caching.Provider;
using IUDICO.Common.Models.Services;

using Moq;

namespace IUDICO.UnitTests.Analytics
{
    using System;

    using IUDICO.Common.Models;
    using IUDICO.Common.Models.Shared;

    public class AnalyticsTests
    {
        #region Protected members

        protected static AnalyticsTests instance;

        #endregion

        #region Public properties

        public Mock<IDataContext> MockDataContext { get; protected set; }

        public Mock<ILmsService> MockLmsService { get; protected set; }

        public Mock<MixedAnalyticsStorage> MockMixedStorage { get; protected set; }

        public Mock<HttpCache> MockCacheProvider { get; protected set; }

        public Mock<CachedAnalyticsStorage> MockStorage { get; protected set; }

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

        public IAnalyticsStorage Storage
        {
            get
            {
                return this.MockStorage.Object;
            }
        }

        public Mock<ITable> ForecastingTrees { get; protected set; }

        public Mock<ITable> TopicScores { get; protected set; }

        public Mock<ITable> UserScores { get; protected set; }

        public Mock<ITable> Tags { get; protected set; }

        public Mock<ITable> TopicTags { get; protected set; }

        #endregion

        private AnalyticsTests()
        {
            this.MockDataContext = new Mock<IDataContext>();
            this.MockLmsService = new Mock<ILmsService>();
            this.MockMixedStorage = new Mock<MixedAnalyticsStorage>(this.MockLmsService.Object);
            this.MockCacheProvider = new Mock<HttpCache>();
            this.MockStorage = new Mock<CachedAnalyticsStorage>(this.MockMixedStorage.Object, this.MockCacheProvider.Object);

            this.ForecastingTrees = new Mock<ITable>();
            this.TopicScores = new Mock<ITable>();
            this.UserScores = new Mock<ITable>();
            this.Tags = new Mock<ITable>();
            this.TopicTags = new Mock<ITable>();

            this.Setup();
            this.SetupTables();
        }

        public static AnalyticsTests GetInstance()
        {
            return instance ?? (instance = new AnalyticsTests());
        }

        public void Setup()
        {

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
                    new TopicTag { TopicId = 1, TagId = 4 }
                };

            var userId = Guid.NewGuid();

            var mockUserScoresData = new[]
                {
                    new UserScore { Score = 80, TagId = 1, UserId = userId },
                    new UserScore { Score = 90, TagId = 2, UserId = userId },
                    new UserScore { Score = 60, TagId = 3, UserId = userId }
                };

            var mockTopicScoresData = new[]
                {
                    new TopicScore { Score = 60, TagId = 1, TopicId = 1 },
                    new TopicScore { Score = 70, TagId = 2, TopicId = 1 },
                    new TopicScore { Score = 90, TagId = 4, TopicId = 1 }
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
    }
}
