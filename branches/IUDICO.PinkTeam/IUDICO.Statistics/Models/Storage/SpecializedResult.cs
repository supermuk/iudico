using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Statistics.Models.Storage
{
    public class AllSpecializedResults
    {
        private List<SpecializedResult> _SpecializedResult; 
        private List<User> _Users;                         
        private int[] _SelectCurriculumIds;            
        private IEnumerable<Curriculum> _Curriculums;    

        public AllSpecializedResults()
        {
            _SpecializedResult = new List<SpecializedResult>();
            _Users = new List<User>();
        }

        public List<User> Users
        {
            get { return _Users; }
            set { _Users = value; }
        }
        public int[] SelectCurriculumIds
        {
            get { return _SelectCurriculumIds; }
            set { _SelectCurriculumIds = value; }
        }
        public List<SpecializedResult> SpecializedResult
        {
            get { return _SpecializedResult; }
            set { _SpecializedResult = value; }
        }
        public IEnumerable<Curriculum> Curriculums
        {
            get { return _Curriculums; }
            set { _Curriculums = value; }
        }
    }

    public class SpecializedResult
    {
        private User _User;
        private IEnumerable<Curriculum> _Curriculums;    
        private List<CurriculumResult> _CurriculumResult; 
        private double? _Sum;
        private double? _Max;
        private double? _Percent;
        private char _ECTS;

        public SpecializedResult()
        {
            _User = new User();
            _CurriculumResult = new List<CurriculumResult>();
        }

        public void CalculateSpecializedResult(User user)
        {
            _Sum = 0.0;
            _Max = 0.0;
            _Percent = 0.0;
            _User = user;

            foreach (CurriculumResult curr in _CurriculumResult)
            {
                _Sum += curr.Sum;
                _Max += curr.Max;
            }

            _Percent = _Sum / _Max * 100.0;
            _ECTS = Ects(_Percent);
        }

        public IEnumerable<Curriculum> Curriculums
        {
            get { return _Curriculums; }
            set { _Curriculums = value; }
        }
        public User User
        {
            get { return _User; }
        }
        public List<CurriculumResult> CurriculumResult
        {
            get { return _CurriculumResult; }
            set { CurriculumResult = value; }
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

    public class CurriculumResult 
    {
        private User _User;
        private Curriculum _Curriculum;
        private IEnumerable<Theme> _Themes;           
        private List<ThemeResult> _ThemeResult;    
        private double? _Sum;
        private double? _Max;


        public CurriculumResult()
        {
            _User = new User();
            _Curriculum = new Curriculum();
            _ThemeResult = new List<ThemeResult>();
        }

        public void CalculateSumAndMax(User user, Curriculum curr)
        {
            _Max = 0.0;
            _Sum = 0.0;
            _User = user;
            _Curriculum = curr;
            foreach (ThemeResult theme in _ThemeResult)
            {
                _Sum += theme.Res;
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
        public Curriculum Curriculum
        {
            get { return _Curriculum; }
        }
        public List<ThemeResult> ThemeResult
        {
            get { return _ThemeResult; }
            set { _ThemeResult = value; }
        }
        public IEnumerable<Theme> Themes
        {
            set { _Themes = value; }
            get { return _Themes; }
        }
    }

    public class ThemeResult
    {
        private User _User;
        private Theme _Theme;
        private IEnumerable<AttemptResult> _AttemptResults;
        private double? _Res;

        public ThemeResult()
        {
            _User = new User();
            _Theme = new Theme();
        }
        public ThemeResult(User user, Theme theme)
        {
            _User = user;
            _Theme = theme;
        }

        public double? GetThemeResultScore()
        {
            if (_AttemptResults.Count() == 0 || _AttemptResults.First().Score.ScaledScore == null)
            {
                _Res = 0.0;
            }
            else
            {
                _Res = _AttemptResults.First(x => x.User == _User & x.Theme == _Theme).Score.ToPercents();
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