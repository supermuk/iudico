using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.Statistics;

namespace IUDICO.Statistics.Models.Storage
{
    public class AllSpecializedResults
    {
        public AllSpecializedResults()
        {
            this.SpecializedResults = new List<SpecializedResult>();
            this.Users = new List<User>();
        }

        public List<User> Users { get; set; }

        public int[] SelectedCurriculumIds { get; set; }

        public List<SpecializedResult> SpecializedResults { get; set; }

        public IEnumerable<Curriculum> Curriculums { get; set; }
    }

    public class SpecializedResult
    {
        private char ects;

        public SpecializedResult()
        {
            this.User = new User();
            this.DisciplineResult = new List<DisciplineResult>();
        }

        public void CalculateSpecializedResult(User user)
        {
            this.Sum = 0.0;
            this.Max = 0.0;
            this.Percent = 0.0;
            this.User = user;

            foreach (var curr in this.DisciplineResult)
            {
                this.Sum += curr.Sum;
                this.Max += curr.Max;
            }

            this.Percent = this.Sum / this.Max * 100.0;
            this.ects = this.Ects(this.Percent);
        }

        public IEnumerable<Discipline> Disciplines { get; set; }

        public User User { get; private set; }

        public List<DisciplineResult> DisciplineResult { get; set; }

        public char Ects(double? percent)
        {
            if (percent >= 90.0)
            {
                return 'A';
            }
            else if (percent >= 81.0)
            {
                return 'B';
            }
            else if (percent >= 71.0)
            {
                return 'C';
            }
            else if (percent >= 61.0)
            {
                return 'D';
            }
            else if (percent >= 51.0)
            {
                return 'E';
            }
            else
            {
                return 'F';
            }
        }

        public double? Sum { get; private set; }

        public double? Max { get; private set; }

        public double? Percent { get; private set; }

        public char? ECTS
        {
            get { return this.ects; }
        }

    }

    public class DisciplineResult 
    {
        private User user;

        public DisciplineResult()
        {
            this.user = new User();
            this.Discipline = new Discipline();
            this.TopicResult = new List<TopicResult>();
        }

        public void CalculateSumAndMax(User user, Discipline curr)
        {
            this.Max = 0.0;
            this.Sum = 0.0;
            this.user = user;
            this.Discipline = curr;
            foreach (TopicResult topic in this.TopicResult)
            {
                this.Sum += topic.Res;
                this.Max += 100;
            }
        }

        public double? Sum { get; private set; }

        public double? Max { get; private set; }

        public Discipline Discipline { get; private set; }

        public List<TopicResult> TopicResult { get; set; }

        public IEnumerable<Topic> Topics { get; set; }
    }

    public class TopicResult
    {
        private User user;
        private Topic topic;

        public TopicResult()
        {
            this.user = new User();
            this.topic = new Topic();
        }
        public TopicResult(User user, Topic topic)
        {
            this.user = user;
            this.topic = topic;
        }

        public double? GetTopicResultScore()
        {
            if (this.AttemptResults.Count() == 0 || this.AttemptResults.First().Score.ScaledScore == null)
            {
                this.Res = 0.0;
            }
            else
            {
                this.Res = this.AttemptResults.First(x => x.User == this.user & x.CurriculumChapterTopic.Topic == this.topic).Score.ToPercents();
            }

            return this.Res;
        }

        public IEnumerable<AttemptResult> AttemptResults { get; set; }

        public double? Res { get; set; }
    }
}