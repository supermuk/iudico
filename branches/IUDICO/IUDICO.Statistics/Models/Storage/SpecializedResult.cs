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
            SpecializedResults = new List<SpecializedResult>();
            Users = new List<User>();
        }

        public List<User> Users { get; set; }

        public int[] SelectedCurriculumIds { get; set; }

        public List<SpecializedResult> SpecializedResults { get; set; }

        public IEnumerable<Curriculum> Curriculums { get; set; }
    }

    public class SpecializedResult
    {
        private User _User;
        private IEnumerable<Discipline> _Disciplines;    
        private List<DisciplineResult> _DisciplineResult; 
        private double? _Sum;
        private double? _Max;
        private double? _Percent;
        private char _ECTS;

        public SpecializedResult()
        {
            _User = new User();
            _DisciplineResult = new List<DisciplineResult>();
        }

        public void CalculateSpecializedResult(User user)
        {
            _Sum = 0.0;
            _Max = 0.0;
            _Percent = 0.0;
            _User = user;

            foreach (DisciplineResult curr in _DisciplineResult)
            {
                _Sum += curr.Sum;
                _Max += curr.Max;
            }

            _Percent = _Sum / _Max * 100.0;
            _ECTS = Ects(_Percent);
        }

        public IEnumerable<Discipline> Disciplines
        {
            get { return _Disciplines; }
            set { _Disciplines = value; }
        }
        public User User
        {
            get { return _User; }
        }
        public List<DisciplineResult> DisciplineResult
        {
            get { return _DisciplineResult; }
            set { DisciplineResult = value; }
        }
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
        public double? Sum
        {
            get { return _Sum; }
        }
        public double? Max
        {
            get { return _Max; }
        }
        public double? Percent
        {
            get { return _Percent; }
        }
        public char? ECTS
        {
            get { return _ECTS; }
        }

    }

    public class DisciplineResult 
    {
        private User _User;
        private Discipline _Discipline;
        private IEnumerable<Topic> _Topics;           
        private List<TopicResult> _TopicResult;    
        private double? _Sum;
        private double? _Max;


        public DisciplineResult()
        {
            _User = new User();
            _Discipline = new Discipline();
            _TopicResult = new List<TopicResult>();
        }

        public void CalculateSumAndMax(User user, Discipline curr)
        {
            _Max = 0.0;
            _Sum = 0.0;
            _User = user;
            _Discipline = curr;
            foreach (TopicResult topic in _TopicResult)
            {
                _Sum += topic.Res;
                _Max += 100;
            }
        }

        public double? Sum
        {
            get { return _Sum; }
        }
        public double? Max
        {
            get { return _Max; }
        }
        public Discipline Discipline
        {
            get { return _Discipline; }
        }
        public List<TopicResult> TopicResult
        {
            get { return _TopicResult; }
            set { _TopicResult = value; }
        }
        public IEnumerable<Topic> Topics
        {
            set { _Topics = value; }
            get { return _Topics; }
        }
    }

    public class TopicResult
    {
        private User _User;
        private Topic _Topic;
        private IEnumerable<AttemptResult> _AttemptResults;
        private double? _Res;

        public TopicResult()
        {
            _User = new User();
            _Topic = new Topic();
        }
        public TopicResult(User user, Topic topic)
        {
            _User = user;
            _Topic = topic;
        }

        public double? GetTopicResultScore()
        {
            if (_AttemptResults.Count() == 0 || _AttemptResults.First().Score.ScaledScore == null)
            {
                _Res = 0.0;
            }
            else
            {
                _Res = _AttemptResults.First(x => x.User == _User & x.CurriculumChapterTopic.Topic == _Topic).Score.ToPercents();
            }
            return _Res;
        }

        public IEnumerable<AttemptResult> AttemptResults
        {
            get { return _AttemptResults; }
            set { _AttemptResults = value; }

        }
        public double? Res
        {
            get { return _Res; }
            set { _Res = value; }
        }
    }
}