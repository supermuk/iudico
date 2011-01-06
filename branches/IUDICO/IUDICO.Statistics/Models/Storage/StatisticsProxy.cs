using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;


namespace IUDICO.Statistics.Models.Storage
{
    //Roma
    public class StatisticsProxy : IStatisticsProxy, IStatisticsService
    {
        private readonly ILmsService _LmsService;

        #region Constructors

        /// <summary>
        /// Default constructor .
        /// </summary>
        public StatisticsProxy()
        {

        }

        /// <summary>
        /// Constructor with parameters .
        /// </summary>
        /// <param name="lmsService">ILmsService service .</param>
        public StatisticsProxy(ILmsService lmsService)
        {
            _LmsService = lmsService;
        }

        #endregion

        #region StatisticsMethods

        /// <summary>
        /// Gets all groups, using method GetGroups() from IUserService .
        /// </summary>
        /// <returns>IEnumerable<IUDICO.Common.Models.Group> All groups .</returns>
        public IEnumerable<Group> GetAllGroups()
        {
            return _LmsService.FindService<IUserService>().GetGroups();
        }

        /// <summary>
        /// Gets all curriculum dy group id using method GetCurriculumsByGroupId(int groupId) from ICurriculumService .
        /// </summary>
        /// <param name="groupId">int Selected group id .</param>
        /// <returns>IEnumerable<Curriculum> Curriculums by group id .</returns>
        public IEnumerable<Curriculum> GetCurrilulumsByGroupId(int groupId)
        {
            return _LmsService.FindService<ICurriculumService>().GetCurriculumsByGroupId(groupId);
        }

        /// <summary>
        /// Gets all theme by curriculum id using method GetThemesByCurriculumId(int curriculumId) from ICurriculumService .
        /// </summary>
        /// <param name="curriculumsId">int Selected curriculum id .</param>
        /// <returns>IEnumerable<IUDICO.Common.Models.Theme> Themes .</returns>
        public IEnumerable<Theme> GetThemesByCurriculumId(int curriculumId)
        {
            return _LmsService.FindService<ICurriculumService>().GetThemesByCurriculumId(curriculumId);
        }

        /// <summary>
        /// Gets AttemptResult (result) by user, theme using method GetResults(User user, Theme theme) from IUserService .
        /// </summary>
        /// <param name="user">IUDICO.Common.Models.User Selected user .</param>
        /// <param name="theme">IUDICO.Common.Models.Theme Selected theme .</param>
        /// <returns>IEnumerable<AttemptResult> User theme results .</returns>
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
