using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models.Services;
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

        public List<SpecializedResult> SpecializedResults { get; set; }

        public IEnumerable<Curriculum> Curriculums { get; set; }
    }

    /// <summary>
    /// Represents result of user on different curriculums
    /// </summary>
    public class SpecializedResult
    {
        private char ects;

        public SpecializedResult()
        {
            this.User = new User();
            this.DisciplineResults = new List<DisciplineResult>();
        }

        public void CalculateSpecializedResult(User user)
        {
            this.Sum = 0.0;
            this.Max = 0.0;
            this.Percent = 0.0;
            this.User = user;

            foreach (var curr in this.DisciplineResults)
            {
                this.Sum += curr.Sum;
                this.Max += curr.Max;
            }

            if (this.Max != 0)
                this.Percent = this.Sum / this.Max * 100.0;
            else
                this.Percent = 0;
            this.ects = this.Ects(this.Percent);
        }

        public IEnumerable<Curriculum> Curriculums { get; set; }

        public User User { get; private set; }

        public List<DisciplineResult> DisciplineResults { get; set; }

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
        public DisciplineResult()
        {
            this.Curriculum = new Curriculum();
            this.TopicResults = new List<TopicResult>();
        }

        public void CalculateSumAndMax(User user, Curriculum curr)
        {
            this.Max = 0.0;
            this.Sum = 0.0;
            this.Curriculum = curr;
            foreach (TopicResult topic in this.TopicResults)
            {
                this.Sum += topic.Res;
                this.Max += topic.ResMax;
            }
        }

        public double? Sum { get; private set; }

        public double? Max { get; private set; }

        public Curriculum Curriculum { get; private set; }

        public List<TopicResult> TopicResults { get; set; }

        public IEnumerable<CurriculumChapterTopic> CurriculumChapterTopics { get; set; }
    }

    public class TopicResult
    {
        private readonly User user;
        private readonly CurriculumChapterTopic curriculumChapterTopic;

        public TopicResult()
        {
            this.user = new User();
            this.curriculumChapterTopic = new CurriculumChapterTopic();
        }
        public TopicResult(User user, CurriculumChapterTopic curriculumChapterTopic)
        {
            this.user = user;
            this.curriculumChapterTopic = curriculumChapterTopic;
        }

        public double? GetTopicResultScore(ILmsService lmsService)
        {
            if (this.AttemptResults.Count() == 0 || this.AttemptResults.First().Score.ScaledScore == null)
            {
                this.Res = 0.0;
                this.ResMax = 0.0;
            }
            else
            {
                this.Res = this.AttemptResults.First(x => x.User.Id == this.user.Id & x.CurriculumChapterTopic.Id == this.curriculumChapterTopic.Id).Score.RawScore;
                var attemptResult =
                    this.AttemptResults.First(
                        x => x.User.Id == this.user.Id & x.CurriculumChapterTopic.Id == this.curriculumChapterTopic.Id);
                var answerResults = lmsService.FindService<ITestingService>().GetAnswers(attemptResult);

                int iudicoCourseRef = attemptResult.IudicoCourseRef;
                var courseInfo = lmsService.FindService<ICourseService>().GetCourseInfo(iudicoCourseRef);

                double maxScore = 0;
                foreach (var node in courseInfo.NodesInfo)
                {
                    if (answerResults.Any(answer => int.Parse(answer.PrimaryResourceFromManifest.Replace(".html", string.Empty)) == node.Id))
                    {
                        maxScore += node.MaxScore;
                    }
                }
                this.ResMax = maxScore;
            }

            return this.Res;
        }

        public IEnumerable<AttemptResult> AttemptResults { get; set; }

        public double? Res { get; set; }

        public double? ResMax { get; set; }
    }
}