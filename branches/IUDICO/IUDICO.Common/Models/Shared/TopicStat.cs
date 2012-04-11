using System;

namespace IUDICO.Common.Models.Shared
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