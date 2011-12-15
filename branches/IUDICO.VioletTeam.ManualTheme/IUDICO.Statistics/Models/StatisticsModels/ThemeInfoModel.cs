using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Statistics.Models.StatisticsModels
{
    /// <summary>
    /// This class contain all information that need for displaying of Stats/ThemeInfo page
    /// </summary>
    public class ThemeInfoModel
    {
        #region Fields

        /// <summary>
        /// Id of selected curriculum
        /// </summary>
        private readonly int CurriculumId;

        /// <summary>
        /// 
        /// </summary>
        private readonly List<AttemptResult> _LastAttempts;
        private readonly IEnumerable<User> SelectGroupStudents;
        private readonly IEnumerable<Theme> SelectCurriculumThemes;

        #endregion

        private ThemeInfoModel() 
        {
            List<AttemptResult> testAttemptList = new List<AttemptResult>();
            List<User> testUserList = new List<User>();
            List<Theme> testThemeList = new List<Theme>();
            float? attemptScore;
            AttemptResult testAttempt;

            User testUser1 = new User();
            testUser1.Name = "user1";
            Theme testTheme1 = new Theme();
            testTheme1.Name = "theme1";
            User testUser2 = new User();
            testUser2.Name = "user2";
            Theme testTheme2 = new Theme();
            testTheme2.Name = "theme2";

            attemptScore = (float?)0.55;
            testAttempt = new AttemptResult(1, testUser1, testTheme1, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, attemptScore);
            testAttemptList.Add(testAttempt);
            
            attemptScore = (float?)0.65;
            testAttempt = new AttemptResult(1, testUser1, testTheme2, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), null, attemptScore);
            testAttemptList.Add(testAttempt);

            attemptScore = (float?)0.85;
            testAttempt = new AttemptResult(1, testUser2, testTheme1, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, attemptScore);
            testAttemptList.Add(testAttempt);

            attemptScore = (float?)0.95;
            testAttempt = new AttemptResult(1, testUser2, testTheme2, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), null, attemptScore);
            testAttemptList.Add(testAttempt);

            testUserList.Add(testUser1);
            testThemeList.Add(testTheme1);
            testUserList.Add(testUser2);
            testThemeList.Add(testTheme2);

            this._LastAttempts = testAttemptList;
            this.SelectGroupStudents = testUserList;
            this.SelectCurriculumThemes = testThemeList;
        }

        public static ThemeInfoModel ThemeInfoModelTestObject()
        {
            return new ThemeInfoModel();
        }

        public ThemeInfoModel(int groupId, int curriculumId, ILmsService lmsService)
        {
            _LastAttempts = new List<AttemptResult>();

            CurriculumId = curriculumId;

            Group group = lmsService.FindService<IUserService>().GetGroup(groupId);
            SelectGroupStudents = lmsService.FindService<IUserService>().GetUsersByGroup(group);

            SelectCurriculumThemes = lmsService.FindService<ICurriculumService>().GetThemesByCurriculumId(CurriculumId);

            foreach (var temp in from student in SelectGroupStudents
                                 from theme in SelectCurriculumThemes
                                 select lmsService.FindService<ITestingService>().GetResults(student, theme)
                                 into temp where temp != null select temp)
            {
                var filteredTemp = temp//.Where(attempt => attempt.CompletionStatus == CompletionStatus.Completed)
                        .OrderBy(attempt => attempt.StartTime);
                if (filteredTemp.Count() != 0)
                    _LastAttempts.Add(filteredTemp.First());
            }
        }

        public IEnumerable<Theme> GetSelectCurriculumThemes()
        {
            return this.SelectCurriculumThemes;
        }

        public IEnumerable<User> GetSelectStudents()
        {
            return this.SelectGroupStudents;
        }
        
        public double GetStudentResultForTheme(User selectStudent, Theme selectTheme)
        {
            if (_LastAttempts.Count != 0)
            {
                if (_LastAttempts.Single(x => x.User == selectStudent & x.Theme == selectTheme).Score.ToPercents() != null)
                {
                    double? result =_LastAttempts.Single(x => x.User == selectStudent & x.Theme == selectTheme).Score.ToPercents();
                    if (result.HasValue == true)
                        return Math.Round((double)result,2);
                    else
                        return 0;
                }
                else
                    return 0;
            }
            else
                return 0;
        }

        public double GetStudentResultForAllThemesInSelectedCurriculum(User selectStudent)
        {
            double result = 0;

            if (_LastAttempts.Count != 0)
            {
                foreach (Theme theme in SelectCurriculumThemes)
                {                    
                    if (_LastAttempts.Count(x => x.User == selectStudent & x.Theme == theme) != 0)
                    {
                        double? value = _LastAttempts.First(x => x.User == selectStudent & x.Theme == theme).Score.ToPercents();
                        if (value.HasValue == true)
                            result += Math.Round((double)value, 2);
                    }
                }
            }

            return result;
        }

        public double GetAllThemesInSelectedCurriculumMaxMark()
        {
            return 100 * SelectCurriculumThemes.Count();
        }

        public double GetMaxResutForTheme(Theme selectTheme)
        {
            return 100;
        }

        public char Ects(double percent)
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

        public bool NoData(User selectStudent, Theme selectTheme)
        {
            AttemptResult res = _LastAttempts.Find(x => x.User == selectStudent & x.Theme == selectTheme);
            if (res != null)
                return false;
            return true;
        }

        public long GetAttempId(User selectStudent, Theme selectTheme)
        {
            AttemptResult res = _LastAttempts.Find(x => x.User == selectStudent & x.Theme == selectTheme);
            if (res != null)
                return res.AttemptId;
            return -1;
        }

        public List<AttemptResult> GetAllAttemts()
        {
            return this._LastAttempts;
        }
    }
}