namespace IUDICO.Analytics.Models
{
    public class TopicSimilarity
    {
        public TopicSimilarity(int t1, int t2, double sim)
        {
            this.Topic1Id = t1;
            this.Topic2Id = t2;
            this.Similarity = sim;
        }

        public int Topic1Id { get; protected set; }
        public int Topic2Id { get; protected set; }
        public double Similarity { get; protected set; }
    }
}