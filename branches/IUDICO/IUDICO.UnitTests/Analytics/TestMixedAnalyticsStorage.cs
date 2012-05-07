using IUDICO.Analytics.Models.Storage;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;

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
    }
}