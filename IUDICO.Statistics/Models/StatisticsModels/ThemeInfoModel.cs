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
    /// This class contain all information that need for displaying of Stats/TopicInfo page
    /// </summary>
    public class TopicInfoModel
    {
        #region Fields

        /// <summary>
        /// Id of selected discipline
        /// </summary>
        private readonly int DisciplineId;

        /// <summary>
        /// 
        /// </summary>
        private readonly List<AttemptResult> _LastAttempts;
        private readonly IEnumerable<User> SelectGroupStudents;
        private readonly IEnumerable<Topic> SelectDisciplineTopics;

        #endregion

        private TopicInfoModel() 
        {
            List<AttemptResult> testAttemptList = new List<AttemptResult>();
            List<User> testUserList = new List<User>();
            List<Topic> testTopicList = new List<Topic>();
            float? attemptScore;
            AttemptResult testAttempt;

            User testUser1 = new User();
            testUser1.Name = "user1";
            Topic testTopic1 = new Topic();
            testTopic1.Name = "topic1";
            User testUser2 = new User();
            testUser2.Name = "user2";
            Topic testTopic2 = new Topic();
            testTopic2.Name = "topic2";

            attemptScore = (float?)0.55;
            testAttempt = new AttemptResult(1, testUser1, testTopic1, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, attemptScore);
            testAttemptList.Add(testAttempt);
            
            attemptScore = (float?)0.65;
            testAttempt = new AttemptResult(1, testUser1, testTopic2, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), null, attemptScore);
            testAttemptList.Add(testAttempt);

            attemptScore = (float?)0.85;
            testAttempt = new AttemptResult(1, testUser2, testTopic1, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, attemptScore);
            testAttemptList.Add(testAttempt);

            attemptScore = (float?)0.95;
            testAttempt = new AttemptResult(1, testUser2, testTopic2, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), null, attemptScore);
            testAttemptList.Add(testAttempt);

            testUserList.Add(testUser1);
            testTopicList.Add(testTopic1);
            testUserList.Add(testUser2);
            testTopicList.Add(testTopic2);

            this._LastAttempts = testAttemptList;
            this.SelectGroupStudents = testUserList;
            this.SelectDisciplineTopics = testTopicList;
        }

        public static TopicInfoModel TopicInfoModelTestObject()
        {
            return new TopicInfoModel();
        }

        public TopicInfoModel(int groupId, int disciplineId, ILmsService lmsService)
        {
            _LastAttempts = new List<AttemptResult>();

            DisciplineId = disciplineId;

            Group group = lmsService.FindService<IUserService>().GetGroup(groupId);
            SelectGroupStudents = lmsService.FindService<IUserService>().GetUsersByGroup(group);

            SelectDisciplineTopics = lmsService.FindService<IDisciplineService>().GetTopicsByDisciplineId(DisciplineId);

            foreach (var temp in from student in SelectGroupStudents
                                 from topic in SelectDisciplineTopics
                                 select lmsService.FindService<ITestingService>().GetResults(student, topic)
                                 into temp where temp != null select temp)
            {
                var filteredTemp = temp//.Where(attempt => attempt.CompletionStatus == CompletionStatus.Completed)
                        .OrderBy(attempt => attempt.StartTime);
                if (filteredTemp.Count() != 0)
                    _LastAttempts.Add(filteredTemp.First());
            }
        }

        public IEnumerable<Topic> GetSelectDisciplineTopics()
        {
            return this.SelectDisciplineTopics;
        }

        public IEnumerable<User> GetSelectStudents()
        {
            return this.SelectGroupStudents;
        }
        
        public double GetStudentResultForTopic(User selectStudent, Topic selectTopic)
        {
            if (_LastAttempts.Count != 0)
            {
                if (_LastAttempts.Single(x => x.User == selectStudent & x.Topic == selectTopic).Score.ToPercents() != null)
                {
                    double? result =_LastAttempts.Single(x => x.User == selectStudent & x.Topic == selectTopic).Score.ToPercents();
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

        public double GetStudentResultForAllTopicsInSelectedDiscipline(User selectStudent)
        {
            double result = 0;

            if (_LastAttempts.Count != 0)
            {
                foreach (Topic topic in SelectDisciplineTopics)
                {                    
                    if (_LastAttempts.Count(x => x.User == selectStudent & x.Topic == topic) != 0)
                    {
                        double? value = _LastAttempts.First(x => x.User == selectStudent & x.Topic == topic).Score.ToPercents();
                        if (value.HasValue == true)
                            result += Math.Round((double)value, 2);
                    }
                }
            }

            return result;
        }

        public double GetAllTopicsInSelectedDisciplineMaxMark()
        {
            return 100 * SelectDisciplineTopics.Count();
        }

        public double GetMaxResutForTopic(Topic selectTopic)
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

        public bool NoData(User selectStudent, Topic selectTopic)
        {
            AttemptResult res = _LastAttempts.Find(x => x.User == selectStudent & x.Topic == selectTopic);
            if (res != null)
                return false;
            return true;
        }

        public long GetAttempId(User selectStudent, Topic selectTopic)
        {
            AttemptResult res = _LastAttempts.Find(x => x.User == selectStudent & x.Topic == selectTopic);
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