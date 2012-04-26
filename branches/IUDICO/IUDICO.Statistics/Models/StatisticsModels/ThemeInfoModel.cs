using System;
using System.Collections.Generic;
using System.Linq;

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
        private readonly int disciplineId;

        /// <summary>
        /// 
        /// </summary>
        private readonly List<AttemptResult> lastAttempts;
        private readonly IEnumerable<User> selectGroupStudents;
        private readonly IEnumerable<Topic> selectDisciplineTopics;

        #endregion

        public TopicInfoModel(int groupId, int disciplineId, ILmsService lmsService)
        {
            this.lastAttempts = new List<AttemptResult>();

            this.disciplineId = disciplineId;

            var group = lmsService.FindService<IUserService>().GetGroup(groupId);
            this.selectGroupStudents = lmsService.FindService<IUserService>().GetUsersByGroup(group);

            this.selectDisciplineTopics = lmsService.FindService<IDisciplineService>().GetTopicsByDisciplineId(this.disciplineId);

            throw new NotImplementedException(
                           "Statistics was not implemented due to new design of Discipline/Curriculum services");
            /*foreach (var temp in from student in SelectGroupStudents
                                 from topic in SelectDisciplineTopics
                                 select lmsService.FindService<ITestingService>().GetResults(student, topic)
                                 into temp where temp != null select temp)
            {
                var filteredTemp = temp//.Where(attempt => attempt.CompletionStatus == CompletionStatus.Completed)
                        .OrderBy(attempt => attempt.StartTime);
                if (filteredTemp.Count() != 0)
                    _LastAttempts.Add(filteredTemp.First());
            }*/
        }

        public IEnumerable<Topic> GetSelectDisciplineTopics()
        {
            return this.selectDisciplineTopics;
        }

        public IEnumerable<User> GetSelectStudents()
        {
            return this.selectGroupStudents;
        }
        
        public double GetStudentResultForTopic(User selectStudent, Topic selectTopic)
        {
            if (this.lastAttempts.Count != 0)
            {
                if (this.lastAttempts.Single(x => x.User == selectStudent & x.CurriculumChapterTopic.Topic == selectTopic).Score.ToPercents() != null)
                {
                    var result = this.lastAttempts.Single(x => x.User == selectStudent & x.CurriculumChapterTopic.Topic == selectTopic).Score.ToPercents();
                    
                    return result.HasValue == true ? Math.Round((double)result, 2) : 0;
                }
                
                return 0;
            }
            
            return 0;
        }

        public double GetStudentResultForAllTopicsInSelectedDiscipline(User selectStudent)
        {
            double result = 0;

            if (this.lastAttempts.Count != 0)
            {
                foreach (Topic topic in this.selectDisciplineTopics)
                {
                    if (this.lastAttempts.Count(x => x.User == selectStudent & x.CurriculumChapterTopic.Topic == topic) != 0)
                    {
                        var value = this.lastAttempts.First(x => x.User == selectStudent & x.CurriculumChapterTopic.Topic == topic).Score.ToPercents();
                        
                        if (value.HasValue)
                        {
                            result += Math.Round((double)value, 2);
                        }
                    }
                }
            }

            return result;
        }

        public double GetAllTopicsInSelectedDisciplineMaxMark()
        {
            return 100 * this.selectDisciplineTopics.Count();
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
            var res = this.lastAttempts.Find(x => x.User == selectStudent & x.CurriculumChapterTopic.Topic == selectTopic);
            
            return res == null;
        }

        public long GetAttempId(User selectStudent, Topic selectTopic)
        {
            var res = this.lastAttempts.Find(x => x.User == selectStudent & x.CurriculumChapterTopic.Topic == selectTopic);
            
            return res != null ? res.AttemptId : -1;
        }

        public List<AttemptResult> GetAllAttemts()
        {
            return this.lastAttempts;
        }
    }
}