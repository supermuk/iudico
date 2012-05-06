namespace IUDICO.Analytics.Models
{
    using IUDICO.Common.Models.Interfaces;
    using IUDICO.Common.Models.Shared;

    public interface IDataContext : IMockableDataContext
    {
        IMockableTable<ForecastingTree> ForecastingTrees { get; }
        IMockableTable<TopicScore> TopicScores { get; }
        IMockableTable<UserScore> UserScores { get; }
        IMockableTable<Tag> Tags { get; }
        IMockableTable<TopicTag> TopicTags { get; }
    }
}