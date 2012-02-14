using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.Analytics.Models
{
    public class TopicSimilarity
    {
        public TopicSimilarity(int t1, int t2, double sim)
        {
            Topic1Id = t1;
            Topic2Id = t2;
            Similarity = sim;
        }

        public int Topic1Id { get; protected set; }
        public int Topic2Id { get; protected set; }
        public double Similarity { get; protected set; }
    }
}