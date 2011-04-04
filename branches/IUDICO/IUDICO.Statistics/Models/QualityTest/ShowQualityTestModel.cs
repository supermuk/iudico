using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;

namespace IUDICO.Statistics.Models.QualityTest
{
    public class UserAnswers
    {
        private User _User;
        private double? _Score;
        private IEnumerable<AnswerResult> _ListOfAnswers;

        public UserAnswers(User user, AttemptResult score, ILmsService iLmsService)
        {
            _User = user;
            _Score = score.Score.ToPercents();
            //_ListOfAnswers = iLmsService.FindService<ITestingService>().GetAnswers(score);
            _ListOfAnswers = FakeDataQualityTest.GetFakeAnswers(score);
        }
        public double GetUserScoreForTest(int numberOfTest)
        {
            //temp
            if (_ListOfAnswers.Count(c => c.ActivityTitle == numberOfTest.ToString()) == 0)
                return 0.0;
            AnswerResult temp = _ListOfAnswers.Single(c => c.ActivityTitle == numberOfTest.ToString());
            if (temp.ScaledScore.HasValue == true & temp != null)
                return _ListOfAnswers.Single(c => c.ActivityTitle == numberOfTest.ToString()).ScaledScore.Value;
            else
                return 0.0;
        }
        public double GetUserScore()
        {
            //temp???
            if (this._Score.HasValue == true)
                return this._Score.Value;
            else
                return 0.0;
        }
    }
    class UserAnswerComparer : IComparer<UserAnswers>
    {
        public int Compare(UserAnswers y, UserAnswers x)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y == null)                
                {
                    return 1;
                }
                else
                {
                    return x.GetUserScore().CompareTo(y.GetUserScore());
                }
            }
        }
    }
    public class ShowQualityTestModel
    {
        //private List<Group> _AllowedGroups;
        private String _TeacheUserName;
        private String _CurriculumName;
        private String _ThemeName;
        private List<KeyValuePair<int, double>> _ListOfCoefficient;
        private List<UserAnswers> _ListOfUserAnswers;
        public ShowQualityTestModel(ILmsService iLmsService, int[] selectGroupIds, String teacherUserName, String curriculumName, String themeName, int selectThemeId)
        {
            _ListOfCoefficient = new List<KeyValuePair<int, double>>();
            _TeacheUserName = teacherUserName;
            _CurriculumName = curriculumName;
            _ThemeName = themeName;
            
            List<User> studentsFromSelectedGroups = new List<User>();
            foreach (var groupId in selectGroupIds)
            {
                //studentsFromSelectedGroups = studentsFromSelectedGroups.Union(iLmsService.FindService<IUserService>().GetUsersByGroup(iLmsService.FindService<IUserService>().GetGroup(groupId)));
                studentsFromSelectedGroups.AddRange(FakeDataQualityTest.FakeUsersByGroupId(groupId));
            }

            Theme selectTheme;
            //selectTheme = iLmsService.FindService<ICurriculumService>().GetTheme(selectThemeId);
            Theme fakeTheme = new Theme();
            fakeTheme.Id = 1;
            fakeTheme.Name = "Тема1";
            //
            selectTheme = fakeTheme;

            _ListOfUserAnswers = new List<UserAnswers>();
            foreach (User student in studentsFromSelectedGroups)
            {
                _ListOfUserAnswers.Add(new UserAnswers(student,FakeDataQualityTest.GetFakeAttempt(student,selectTheme),iLmsService));
            }
            
            UserAnswerComparer comparer = new UserAnswerComparer();
            _ListOfUserAnswers.Sort(comparer);
            
            this.Calculations();
        }
        public String GetCurriculumName()
        {
            return this._CurriculumName;
        }
        public String GetTeacherUserName()
        {
            return this._TeacheUserName;
        }
        public String GetThemeName()
        {
            return this._ThemeName;
        }
        public IEnumerable<UserAnswers> GetListOfUserAnswers()
        {
            return this._ListOfUserAnswers;
        }
        public IEnumerable<KeyValuePair<int, double>> GetListOfCoefficient()
        {
            return this._ListOfCoefficient;
        }
        private void Calculations()
        {
            for (int i = 1; i < 11; i++)
            {
                double y_mean = _Calc_y_mean();
                double S_y = _Calc_S_y(y_mean);
                _ListOfCoefficient.Add(new KeyValuePair<int,double>(i,_Calc_R_i(y_mean,S_y,i)));
            }
        }
        private double _Calc_R_i(double y_mean, double S_y, int i)
        {
            double res = 0;
            foreach (UserAnswers userAnswer in _ListOfUserAnswers)
            {
                res += userAnswer.GetUserScore() * userAnswer.GetUserScoreForTest(i);
            }
            double x_mean_i = _Calc_x_mean_i(i);
            res -= y_mean * _ListOfUserAnswers.Count * x_mean_i;
            res /= S_y * _Calc_S_x_i(x_mean_i, i);
            return res;
        }
        private double _Calc_x_mean_i(int i)
        {
            double sum = 0;
            foreach (UserAnswers userAnswer in _ListOfUserAnswers)
            {
                sum += userAnswer.GetUserScoreForTest(i);
            }
            return sum / _ListOfUserAnswers.Count;
        }
        private double _Calc_y_mean()
        {
            double sum = 0;
            int i = 0;
            foreach (UserAnswers userAnswer in _ListOfUserAnswers)
            {
                sum += userAnswer.GetUserScore();
                i++;
            }
            return sum / i;
        }
        private double _Calc_S_y(double y_mean)
        {
            double sum = 0;
            foreach (UserAnswers userAnswer in _ListOfUserAnswers)
            {
                sum += Math.Pow(userAnswer.GetUserScore() - y_mean, 2);
            }
            return Math.Sqrt(sum);
        }
        private double _Calc_S_x_i(double x_i_mean,int i)
        {
            double sum = 0;
            foreach (UserAnswers userAnswer in _ListOfUserAnswers)
            {
                sum += Math.Pow(userAnswer.GetUserScoreForTest(i) - x_i_mean, 2);
            }
            return Math.Sqrt(sum);
        }
    }
}