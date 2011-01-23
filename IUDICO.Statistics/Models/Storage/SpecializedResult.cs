using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;

namespace IUDICO.Statistics.Models.Storage
{
    public class AllSpecializedResults
    {
        private readonly ILmsService _LmsService;

        private readonly List<SpecializedResult> _SpecializedResult;

        private readonly IEnumerable<User> _Users;
        private readonly int[] _SelectCurriculumIds;

        public AllSpecializedResults(IEnumerable<User> users, int[] selectCurriculumIds, ILmsService ilmsService)
        {
            _LmsService = ilmsService;
            _Users = users;
            _SelectCurriculumIds = selectCurriculumIds;
            _SpecializedResult = new List<SpecializedResult>();
            
            foreach (var user in users)
            {
                _SpecializedResult.Add(new SpecializedResult(user, selectCurriculumIds, _LmsService));
            }
        }

        public IEnumerable<Curriculum> Curriculums
        {
            get { return _LmsService.FindService<ICurriculumService>().GetCurriculums(_SelectCurriculumIds); }
        }

        public IEnumerable<User> Users
        {
            get { return _Users; }
            //set { _Users = value; }
        }

        public int[] SelectCurriculumIds
        {
            get { return _SelectCurriculumIds; }
            //set { _SelectCurriculumIds = value; }
        }

        public List<SpecializedResult> SpecializedResultPar
        {
            get { return _SpecializedResult; }
        }

    }

    public class SpecializedResult
    {
        private readonly ILmsService _LmsService;

        private readonly User _User;
        private readonly List<CurriculumResult> _CurriculumResult;
        private int[] _Ids;
        private readonly double? _Sum;
        private readonly double? _Max;
        private readonly double? _Percent;
        private readonly char _ECTS;

        public SpecializedResult(User user, int[] ids, ILmsService ilmsService)
        {
            _LmsService = ilmsService;
            _Sum = 0.0;
            _Max = 0.0;
            _Percent = 0.0;
            _User = new User();
            _User = user;
            _Ids = ids;
            _CurriculumResult = new List<CurriculumResult>();
            
            CurriculumResult curriculumRes;
            IEnumerable<int> ieIds = ids;
            var curriculums = _LmsService.FindService<ICurriculumService>().GetCurriculums(ieIds);

            foreach (var curr in curriculums)
            {
                _CurriculumResult.Add(curriculumRes = new CurriculumResult(_User, curr, _LmsService));
                _Sum += curriculumRes.Sum;
                _Max += curriculumRes.Max;
            }

            _Percent = _Sum / _Max * 100.0;
            _ECTS = Ects(_Percent);
        }

        public char Ects(double? percent)
        {
            if (percent > 91.0)
            {
                return 'A';
            }
            else if (percent > 81.0)
            {
                return 'B';
            }
            else if (percent > 71.0)
            {
                return 'C';
            }
            else if (percent > 61.0)
            {
                return 'D';
            }
            else if (percent > 51.0)
            {
                return 'E';
            }
            else
            {
                return 'F';
            }
        }

        public User User
        {
            get { return _User; }
        }

        public List<CurriculumResult> CurriculumResult
        {
            get { return _CurriculumResult; }
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
        private readonly ILmsService _LmsService;

        private readonly User _User;
        private readonly Curriculum _Curriculum;
        private readonly List<ThemeResult> _ThemeResult;
        private readonly double? _Sum;
        private readonly double? _Max;

        public CurriculumResult (User user, Curriculum curriculum, ILmsService ilmsService)
        {
            _LmsService = ilmsService;
            _Sum = 0.0;
            _Max = 0.0;

            _User = new User();
            _User = user;

            _Curriculum = new Curriculum();
            _Curriculum = curriculum;

            _ThemeResult = new List<ThemeResult>();

            var themes = _LmsService.FindService<ICurriculumService>().GetThemesByCurriculumId(_Curriculum.Id);
            var themeResult = new ThemeResult();
            
            foreach (var theme in themes)
            {
                _ThemeResult.Add(themeResult.GetThemeResult(_User, theme, _LmsService));
                _Sum += themeResult.GetThemeResultScore(_User, theme);
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
        }
    }

    public class ThemeResult
    {
        private ILmsService _LmsService;

        private User _User;
        private Theme _Theme;
        private double? _Res;

        public ThemeResult GetThemeResult(User user, Theme theme, ILmsService ilmsService)
        {
            _LmsService = ilmsService;
            _User = user;
            _Theme = theme;
            _Res = _LmsService.FindService<ITestingService>().GetResults(_User, _Theme).First(x => x.User == _User & x.Theme == _Theme).Score.ToPercents();

            return this;
        }

        public double? GetThemeResultScore(User user, Theme theme)
        {
            _User = user;
            _Theme = theme;
            _Res = _LmsService.FindService<ITestingService>().GetResults(_User, _Theme).First(x => x.User == _User & x.Theme == _Theme).Score.ToPercents();

            return _Res;
        }

    }
}