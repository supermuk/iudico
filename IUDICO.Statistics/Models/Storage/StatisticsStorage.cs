using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;


namespace IUDICO.Statistics.Models.Storage
{
    public class StatisticsStorage : IStatisticsStorage, IStatisticsService
    {
        private readonly ILmsService _LmsService;

        #region Methods to get and show results on ShowCurriculumStatistic.aspx

        public IEnumerable<Curriculum> GetSelectedCurriclums(System.Int32[] curriculumsId)
        {
            IEnumerable<int> ids = curriculumsId;
            return _LmsService.FindService<ICurriculumService>().GetCurriculums(curriculumsId);
        }

        public IEnumerable<User> GetUsersBySelectedGroup(Group group)
        {
            return _LmsService.FindService<IUserService>().GetUsersByGroup(group);
        }

        public List<KeyValuePair<List<Theme>, int>> GetAllThemes(int groupId)
        {
            List<KeyValuePair<List<Theme>, int>> temp = new List<KeyValuePair<List<Theme>, int>>();
            List<Curriculum> curriculums = new List<Curriculum>();
            curriculums = _LmsService.FindService<ICurriculumService>().GetCurriculumsByGroupId(groupId).ToList<Curriculum>();

            foreach (Curriculum curr in curriculums)
            {
                KeyValuePair<List<Theme>, int> temp1 = new KeyValuePair<List<Theme>, int>();
                temp1 = new KeyValuePair<List<Theme>, int>(_LmsService.FindService<ICurriculumService>().GetThemesByCurriculumId(curr.Id).ToList<Theme>(), curr.Id);
                temp.Add(temp1);
            }

            return temp;
        }

        #endregion

        #region StatisticsStorage constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public StatisticsStorage()
        {

        }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="lmsService">Lms service</param>
        public StatisticsStorage(ILmsService lmsService)
        {
            _LmsService = lmsService;
        }

        #endregion

        #region StatisticsMethods

        /// <summary>
        /// Gets all groups, using method GetGroups() from IUserService
        /// </summary>
        /// <returns>All groups</returns>
        public IEnumerable<Group> GetAllGroups()
        {
            return _LmsService.FindService<IUserService>().GetGroups();
        }

        /// <summary>
        /// Gets all curriculum dy group id using method GetCurriculumsByGroupId(int groupId) from ICurriculumService 
        /// </summary>
        /// <param name="groupId">Select group id</param>
        /// <returns>Curriculums</returns>
        public IEnumerable<Curriculum> GetCurrilulumsByGroupId(int groupId)
        {
            return _LmsService.FindService<ICurriculumService>().GetCurriculumsByGroupId(groupId);
        }

        /// <summary>
        /// Gets all theme by curriculum id using method GetThemesByCurriculumId(int curriculumId) from ICurriculumService 
        /// </summary>
        /// <param name="curriculumsId">Select curriculum id</param>
        /// <returns>Themes</returns>
        public IEnumerable<Theme> GetThemesByCurriculumId(int curriculumsId)
        {
            return _LmsService.FindService<ICurriculumService>().GetThemesByCurriculumId(curriculumsId);
        }

        /// <summary>
        /// Gets AttemptResult (result) by user, theme using method GetResults(User user, Theme theme) from IUserService
        /// </summary>
        /// <param name="user">Selected user</param>
        /// <param name="theme">Selected theme</param>
        /// <returns></returns>
        public IEnumerable<AttemptResult> GetResults(User user, Theme theme)
        {
            return _LmsService.FindService<ITestingService>().GetResults(user, theme);
        }

        #endregion

    }

    //Vitalik
    public class ThemeInfoModel
    {
        private ILmsService LmsService;
        public int CurriculumId;
        private List<AttemptResult> LastAttempts;
        public IEnumerable<User> SelectStudents;
        public IEnumerable<Theme> SelectCurriculumThemes;
        public ThemeInfoModel(int groupId, int curriculumId, ILmsService lmsService)
        {
            LmsService = lmsService;
            CurriculumId = curriculumId;
            SelectStudents = LmsService.FindService<IUserService>().GetUsersByGroup
                (
                LmsService.FindService<IUserService>().GetGroup(groupId)
                );
            SelectCurriculumThemes = LmsService.FindService<ICurriculumService>().GetThemesByCurriculumId
                (
                CurriculumId
                );
            //LastAttempts = LmsService.FindService<ITestingService>().GetLastCompleteAttemptsForThemes
            //    (
            //    LmsService.FindService<IUserService>().GetGroup(groupId),
            //    SelectCurriculumThemes
            //    );
            LastAttempts = new List<AttemptResult>();
            foreach (User student in SelectStudents)
            {
                foreach (Theme theme in SelectCurriculumThemes)
                {
                    IEnumerable<AttemptResult> temp = LmsService.FindService<ITestingService>().GetResults(student, theme);
                    if (temp != null)
                        LastAttempts.Add(temp.First());
                }
            }
        }
        public double? GetStudentResautForTheme(User SelectStudent, Theme SelectTheme)
        {
            return LastAttempts.First(x => x.User == SelectStudent & x.Theme == SelectTheme).Score.ToPercents();
        }
        public double? GetStudentResautForAllThemesInSelectedCurriculum(User SelectStudent)
        {
            double? resault = 0;
            foreach (Theme theme in SelectCurriculumThemes)
            {
                //resault += LastAttempts.First(x => x.User == SelectStudent & x.Theme == theme).Score.ToPercents();
                resault += 10;
            }
            return resault;
        }
        public double? GetAllThemesInSelectedCurriculumMaxMark()
        {
            return 100; //* SelectCurriculumThemes.Count();
        }
        public double? GetMaxResautForTheme(Theme SelectTheme)
        {
            return 100;
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
        public AttemptResult GetStudentAttempt(User SelectStudent, Theme SelectTheme)
        {
            return LastAttempts.First();//(x => x.User == SelectStudent & x.Theme == SelectTheme);
        }
        public List<AttemptResult> GetAllAttemts()
        {
            return this.LastAttempts;
        }
    }
    public class ThemeTestResaultsModel
    {
        private ILmsService LmsService;
        public AttemptResult Attempt;

        public ThemeTestResaultsModel(Int32 attemptId, List<AttemptResult> attList, ILmsService lmsService)
        {
            LmsService = lmsService;
            Attempt = attList.First(c => c.AttemptID == attemptId);
        }
    }
}
