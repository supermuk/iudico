using IUDICO.Analytics.Models.Storage;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;

using System;
using System.Collections.Generic;

namespace IUDICO.UnitTests.Analytics
{
    public class TestMixedAnalyticsStorage : MixedAnalyticsStorage
    {
        public TestMixedAnalyticsStorage(ILmsService lmsService)
            : base(lmsService)
        {
        }

        public double TestCustomDistance(User user, Topic topic)
        {
            return this.CustomDistance(user, topic);
        }

        public IEnumerable<TopicTag> TestGetTopicTags(Func<TopicTag, bool> predicate)
        {
            return this.GetTopicTags(predicate);
        }

        public IEnumerable<TopicScore> TestGetTopicTagScores(Topic topic)
        {
            return this.GetTopicTagScores(topic);
        }

        public IEnumerable<UserScore> TestGetUserTagScores(User user)
        {
            return this.GetUserTagScores(user);
        }
    }
}