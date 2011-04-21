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
            _ListOfAnswers = iLmsService.FindService<ITestingService>().GetAnswers(score);
        }
        public double GetUserScoreForTest(long activityPackageID)
        {
            if (_ListOfAnswers.Count(c => c.ActivityPackageId == activityPackageID) == 0)
                return 0.0;
            AnswerResult temp = _ListOfAnswers.Single(c => c.ActivityPackageId == activityPackageID);
            if (temp.ScaledScore.HasValue == true & temp != null)
                return _ListOfAnswers.Single(c => c.ActivityPackageId == activityPackageID).ScaledScore.Value;
            else
                return 0.0;
        }
        public IEnumerable<long> GetActivityPackageIds()
        {
            List<long> resultList = new List<long>();
            foreach (AnswerResult answerResult in this._ListOfAnswers)
            {
                resultList.Add(answerResult.ActivityPackageId);
            }
            return resultList;
        }
        public double GetUserScore()
        {
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
        private List<KeyValuePair<long, double>> _ListOfCoefficient;
        private List<UserAnswers> _ListOfUserAnswers;
        public ShowQualityTestModel(ILmsService iLmsService, int[] selectGroupIds, String teacherUserName, String curriculumName, int selectThemeId)
        {
            //Inialization
            _ListOfCoefficient = new List<KeyValuePair<long, double>>();
            _TeacheUserName = teacherUserName;
            _CurriculumName = curriculumName;

            //Creation of list of students in selected groups
            List<User> studentsFromSelectedGroups = new List<User>();
            foreach (var groupId in selectGroupIds)
            {                
                Group temp = iLmsService.FindService<IUserService>().GetGroup(groupId);                
                studentsFromSelectedGroups.AddRange(iLmsService.FindService<IUserService>().GetUsersByGroup(temp));
            }

            //
            Theme selectTheme;
            selectTheme = iLmsService.FindService<ICurriculumService>().GetTheme(selectThemeId);
            _ThemeName = selectTheme.Name;

            //Creation of list of students answers
            _ListOfUserAnswers = new List<UserAnswers>();
            foreach (User student in studentsFromSelectedGroups)
            {
                IEnumerable< AttemptResult> temp = iLmsService.FindService<ITestingService>().GetResults(student, selectTheme);
                if (temp != null & temp.Count() != 0)
                {
                    temp = temp//.Where(attempt => attempt.CompletionStatus == CompletionStatus.Completed)
                        .OrderBy(attempt => attempt.StartTime);
                    if (temp.Count() != 0)
                    {
                        _ListOfUserAnswers.Add(new UserAnswers(student, temp.First(), iLmsService));
                    }
                }
            }

            //_ListOfUserAnswers.OrderBy(userAnswer => userAnswer.GetUserScore());
            _ListOfUserAnswers.Sort(new UserAnswerComparer());
            this.Calculations();
        }

        #region Get methods
        public bool NoData()
        {
            return this._ListOfCoefficient.Count == 0;
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
        public IEnumerable<KeyValuePair<long, double>> GetListOfCoefficient()
        {
            return this._ListOfCoefficient;
        }
        #endregion

        #region Calculation methods
        private IEnumerable<long> _GetActivityPackageIds()
        {
            IEnumerable<long> resultList = new List<long>();
            foreach (UserAnswers userAnswers in this._ListOfUserAnswers)
            {
                resultList = resultList.Union(userAnswers.GetActivityPackageIds());
            }
            return resultList;
        }

        private void Calculations()
        {
            foreach (long activityPackageId in _GetActivityPackageIds())
            {
                double y_mean = _Calc_y_mean();
                double S_y = _Calc_S_y(y_mean);
                _ListOfCoefficient.Add(new KeyValuePair<long, double>(activityPackageId, _Calc_R_i(y_mean, S_y, activityPackageId)));
            }
        }
        private double _Calc_R_i(double y_mean, double S_y, long activityPackageID)
        {
            double res = 0;
            foreach (UserAnswers userAnswer in _ListOfUserAnswers)
            {
                res += userAnswer.GetUserScore() * userAnswer.GetUserScoreForTest(activityPackageID);
            }
            double x_mean_i = _Calc_x_mean_i(activityPackageID);
            res -= y_mean * _ListOfUserAnswers.Count * x_mean_i;
            res /= S_y * _Calc_S_x_i(x_mean_i, activityPackageID);
            return res;
        }
        private double _Calc_x_mean_i(long activityPackageID)
        {
            double sum = 0;
            foreach (UserAnswers userAnswer in _ListOfUserAnswers)
            {
                sum += userAnswer.GetUserScoreForTest(activityPackageID);
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
        private double _Calc_S_x_i(double x_i_mean, long activityPackageID)
        {
            double sum = 0;
            foreach (UserAnswers userAnswer in _ListOfUserAnswers)
            {
                sum += Math.Pow(userAnswer.GetUserScoreForTest(activityPackageID) - x_i_mean, 2);
            }
            return Math.Sqrt(sum);
        }
        #endregion
    }
}