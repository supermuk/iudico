using System.Collections.Generic;
using IUDICO.Analytics.Models.Storage;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Analytics.Models
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IAnalyticsStorage _AnalyticsStorage;

        public AnalyticsService(IAnalyticsStorage analyticsStoragea)
        {
            _AnalyticsStorage = analyticsStoragea;
        }
        /*
        public IEnumerable<TopicStat> GetRecommendationsByPerformance(User user)
        {
            return _AnalyticsStorage.GetRecommendedTopics(user);
        }
        */
    }
}