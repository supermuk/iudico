using System;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Analytics.Models
{
    public class TopicStat : IComparable<TopicStat>
    {
        public TopicStat(Topic topic, double score)
        {
            Topic = topic;
            Score = score;
        }

        public Topic Topic { get; protected set; }
        public double Score { get; protected set; }

        #region Implementation of IComparable<in TopicStat>

        public int CompareTo(TopicStat topicStat)
        {
            return topicStat.Score.CompareTo(Score);
        }

        #endregion
    }
}