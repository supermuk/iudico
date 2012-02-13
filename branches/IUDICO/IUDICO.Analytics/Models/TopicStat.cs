using System;

namespace IUDICO.Analytics.Models
{
    public class TopicStat : IComparable<TopicStat>
    {
        public TopicStat(int topicId, double score)
        {
            TopicId = topicId;
            Score = score;
        }

        public int TopicId { get; protected set; }
        public double Score { get; protected set; }

        #region Implementation of IComparable<in TopicStat>

        public int CompareTo(TopicStat topicStat)
        {
            return topicStat.Score.CompareTo(Score);
        }

        #endregion
    }
}