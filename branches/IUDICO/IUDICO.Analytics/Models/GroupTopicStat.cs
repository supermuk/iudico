namespace IUDICO.Analytics.Models
{
    public class GroupTopicStat
    {
        public GroupTopicStat(double ratingDifference, double topicDifficulty)
        {
            this.RatingDifference = ratingDifference;
            this.TopicDifficulty = topicDifficulty;
        }

        public double RatingDifference { get; protected set; }
        public double TopicDifficulty { get; protected set; }
    }
}