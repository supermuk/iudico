using System;

using IUDICO.Common.Models.Shared.CurriculumManagement;

namespace IUDICO.Common.Models.Shared
{
    public class TopicStat : IComparable<TopicStat>
    {
        public TopicStat(TopicDescription topic, double score)
        {
            this.Topic = topic;
            this.Score = score;
        }

        public TopicDescription Topic { get; protected set; }
        public double Score { get; protected set; }

        #region Implementation of IComparable<in TopicStat>

        public int CompareTo(TopicStat topicStat)
        {
            return topicStat.Score.CompareTo(this.Score);
        }

        #endregion
    }
}