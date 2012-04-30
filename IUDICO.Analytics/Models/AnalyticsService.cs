using IUDICO.Analytics.Models.Storage;
using IUDICO.Common.Models.Services;

namespace IUDICO.Analytics.Models
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IAnalyticsStorage analyticsStorage;

        public AnalyticsService(IAnalyticsStorage analyticsStoragea)
        {
            this.analyticsStorage = analyticsStoragea;
        }
        /*
        public IEnumerable<TopicStat> GetRecommendationsByPerformance(User user)
        {
            return _AnalyticsStorage.GetRecommendedTopics(user);
        }
        */
    }
}