using System;

namespace IUDICO.Common.Models.Shared
{
    public class TopicStat : IComparable<TopicStat>
    {
        public TopicStat(Topic topic, double score)
        {
            this.Topic = topic;
            this.Score = score;
        }

        public Topic Topic { get; protected set; }
        public double Score { get; protected set; }

        #region Implementation of IComparable<in TopicStat>

        public int CompareTo(TopicStat topicStat)
        {
            return topicStat.Score.CompareTo(this.Score);
        }

        #endregion
    }
}