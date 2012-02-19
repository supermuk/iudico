using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.Common.Models.Shared;

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
        public double GetUserScoreForTest(long activityPackageID,int numberOfPosition)
        {
            if (_ListOfAnswers.Count(c => c.ActivityPackageId == activityPackageID) == 0)
                return 0.0;
            else
            {
                IEnumerable<AnswerResult> temp = _ListOfAnswers.Where(c => c.ActivityPackageId == activityPackageID);
                if (temp.Count() <numberOfPosition)
                    return 0.0;
                else
                {
                    AnswerResult answer = temp.ElementAt(numberOfPosition - 1);
                    if (answer.ScaledScore.HasValue == true)
                        return answer.ScaledScore.Value;
                    else
                        return 0.0;
                }
            }

            
        }
        public bool HaveUserThisQuestion(long activityPackageId)
        {
            return this._ListOfAnswers.Count(answer => answer.ActivityPackageId == activityPackageId) != 0;
        }

        //
        //public IEnumerable<long> GetActivityPackageIds()
        //{
        //    List<long> resultList = new List<long>();
        //    foreach (AnswerResult answerResult in this._ListOfAnswers)
        //    {
        //        resultList.Add(answerResult.ActivityPackageId);
        //    }
        //    return resultList;
        //}
        //
        public double GetUserScore()
        {
            if (this._Score.HasValue == true)
                return this._Score.Value;
            else
                return 0.0;
        }
        public IEnumerable<AnswerResult> GetListOfAnswers()
        {
            return this._ListOfAnswers;
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
        public class QuestionModel
        {
            #region Fields

            /// <summary>
            /// String that showed in column "Number" ("#")
            /// </summary>
            private String _QuesionNumber;

            /// <summary>
            /// 
            /// </summary>
            private long _ActivityPackageId;

            /// <summary>
            /// 
            /// </summary>
            private double _Coefficient;

            private int _NumberOfStudents;
            /// <summary>
            /// 
            /// </summary>
            private bool _NoData;

            #endregion
            
            public QuestionModel(String quesionNumber,IEnumerable<UserAnswers> usersAnswers, long activityPackageId,int numberOfPosition = 1)
            {
                _QuesionNumber = quesionNumber;
                _ActivityPackageId = activityPackageId;
                _Coefficient = _CalculateCoeficient(usersAnswers,numberOfPosition);
            }

            #region Calculations
            private double _Calc_y_mean(IEnumerable<UserAnswers> answeredStudents)
            {
                double sum = 0;
                int i = 0;
                foreach (UserAnswers userAnswer in answeredStudents)
                {
                    sum += userAnswer.GetUserScore();
                    i++;
                }
                return sum / i;
            }
            private double _Calc_S_y(double y_mean,IEnumerable<UserAnswers> answeredStudents)
            {
                double sum = 0;
                foreach (UserAnswers userAnswer in answeredStudents)
                {
                    sum += Math.Pow(userAnswer.GetUserScore() - y_mean, 2);
                }
                return Math.Sqrt(sum);
            }
            private double _Calc_x_mean_i(IEnumerable<UserAnswers> answeredStudents, int numberOfPosition)
            {
                double sum = 0;
                foreach (UserAnswers userAnswer in answeredStudents)
                {
                    sum += userAnswer.GetUserScoreForTest(this._ActivityPackageId,numberOfPosition);
                }
                return sum / answeredStudents.Count();
            }
            private double _Calc_S_x_i(double x_i_mean,IEnumerable<UserAnswers> answeredStudents, int numberOfPosition)
            {
                double sum = 0;
                foreach (UserAnswers userAnswer in answeredStudents)
                {
                    sum += Math.Pow(userAnswer.GetUserScoreForTest(this._ActivityPackageId,numberOfPosition) - x_i_mean, 2);
                }
                return Math.Sqrt(sum);
            }


            private double _Calc_R_i(double y_mean, double S_y, IEnumerable<UserAnswers> answeredStudents, int numberOfPosition)
            {
                double res = 0;
                foreach (UserAnswers userAnswer in answeredStudents)
                {
                    res += userAnswer.GetUserScore() * userAnswer.GetUserScoreForTest(this._ActivityPackageId,numberOfPosition);
                }
                double x_mean_i = _Calc_x_mean_i(answeredStudents,numberOfPosition);
                res -= y_mean * answeredStudents.Count() * x_mean_i;
                res /= S_y * _Calc_S_x_i(x_mean_i, answeredStudents,numberOfPosition);
                return res;
            }


            private double _CalculateCoeficient(IEnumerable<UserAnswers> usersAnswers,int numberOfPosition)
            {
                IEnumerable<UserAnswers> answeredStudents = usersAnswers.Where
                    (userAnswer =>userAnswer.HaveUserThisQuestion(this._ActivityPackageId)==true);
                if (answeredStudents.Count()>5)
                {
                    this._NoData = false;
                    this._NumberOfStudents = answeredStudents.Count();
                    double y_mean = _Calc_y_mean(answeredStudents);
                    double S_y = _Calc_S_y(y_mean,answeredStudents);
                    return _Calc_R_i(y_mean, S_y,usersAnswers,numberOfPosition);
                }
                else
                {
                    this._NumberOfStudents = answeredStudents.Count();
                    this._NoData = true;
                    return 0;
                }
                
            }

            #endregion

            #region Get Methods

            public long GetActivityPackageId()
            {
                return this._ActivityPackageId;
            }
            public String GetQuestionNumber()
            {
                return this._QuesionNumber;
            }
            public Double GetCoefficient()
            {
                return this._Coefficient;
            }
            public int GetNumberOfStudents()
            {
                return this._NumberOfStudents;
            }
            public bool NoData()
            {
                return this._NoData;
            }
            #endregion
        }


        private String _DisciplineName;
        private String _TopicName;
        private IEnumerable<QuestionModel> _ListOfQuestionModels;
        private IEnumerable<UserAnswers> _ListOfUserAnswers;

        private IEnumerable<UserAnswers> StudentsAnswers(ILmsService iLmsService, int[] selectGroupIds, Topic selectTopic)
        {
            List<UserAnswers> listOfUserAnswers = new List<UserAnswers>();
            //Creation of list of all students in selected groups
            IEnumerable<User> studentsFromSelectedGroups = new List<User>();
            foreach (var groupId in selectGroupIds)
            {
                Group temp = iLmsService.FindService<IUserService>().GetGroup(groupId);
                studentsFromSelectedGroups = studentsFromSelectedGroups.Union(iLmsService.FindService<IUserService>().GetUsersByGroup(temp));
            }
            //
            foreach (User student in studentsFromSelectedGroups)
            {
                IEnumerable<AttemptResult> temp = iLmsService.FindService<ITestingService>().GetResults(student, selectTopic);
                if (temp != null & temp.Count() != 0)
                {
                    temp = temp//.Where(attempt => attempt.CompletionStatus == CompletionStatus.Completed)
                        .OrderBy(attempt => attempt.StartTime);
                    if (temp.Count() != 0)
                    {
                        listOfUserAnswers.Add(new UserAnswers(student, temp.First(), iLmsService));
                    }
                }
            }
            return listOfUserAnswers;
        }
        private IEnumerable<QuestionModel> CreationOfQuestionModels()
        {
            List<QuestionModel> listOfQuestionModels = new List<QuestionModel>();
            int numberOfQuestion = 1;
            foreach (UserAnswers userAnswer in this._ListOfUserAnswers)
            {
                IEnumerable<AnswerResult> temp = userAnswer.GetListOfAnswers();
                if (temp.Count() > 0)
                    foreach (AnswerResult userAnswerResult in temp)
                    {
                        if (listOfQuestionModels.Count(questionModel => questionModel.GetActivityPackageId() == userAnswerResult.ActivityPackageId) == 0)
                        {
                            int numberOfQuestionInPackage = temp.Count(answer => answer.ActivityPackageId == userAnswerResult.ActivityPackageId);
                            if(numberOfQuestionInPackage>1)
                            {
                                for (int i = 1; i <= numberOfQuestionInPackage; i++)
                                {
                                    String numberOfQuestionStr = numberOfQuestion.ToString() + "." + i.ToString();
                                    listOfQuestionModels.Add(new QuestionModel(numberOfQuestionStr,this._ListOfUserAnswers, userAnswerResult.ActivityPackageId,i));
                                }
                            }
                            else
                            {
                                listOfQuestionModels.Add(new QuestionModel(numberOfQuestion.ToString(), this._ListOfUserAnswers, userAnswerResult.ActivityPackageId));
                            }
                            numberOfQuestion++;
                            //
                        }
                    }
            }
            return listOfQuestionModels;
        }
        public ShowQualityTestModel(ILmsService iLmsService, int[] selectGroupIds, String disciplineName, int selectTopicId)
        {
            _DisciplineName = disciplineName;
            
            //Topic object that needs for geting user answers
            Topic selectTopic = iLmsService.FindService<ICurriculumService>().GetTopic(selectTopicId);
            _TopicName = selectTopic.Name;

            //Creation of list of students answers
            _ListOfUserAnswers = StudentsAnswers(iLmsService, selectGroupIds, selectTopic);
            _ListOfQuestionModels = CreationOfQuestionModels();
        }

        #region Get methods

        public bool NoData()
        {
            return this._ListOfQuestionModels.Count() == 0;
        }
        public String GetDisciplineName()
        {
            return this._DisciplineName;
        }
        public String GetTopicName()
        {
            return this._TopicName;
        }
        public IEnumerable<QuestionModel> GetListOfQuestionModels()
        {
            return this._ListOfQuestionModels;
        }

        #endregion
    }
}