using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.Analytics.Models
{
    public class GroupTopicStat
    {
        public GroupTopicStat(double ratingDifference, double topicDifficulty)
        {
            RatingDifference = ratingDifference;
            TopicDifficulty = topicDifficulty;
        }

        public double RatingDifference { get; protected set; }
        public double TopicDifficulty { get; protected set; }
    }
}