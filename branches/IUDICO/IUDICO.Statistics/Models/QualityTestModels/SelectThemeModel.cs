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
    public class SelectTopicModel
    {
        private IEnumerable<Topic> _AllowedTopics;
        private String _TeacheUserName;
        private String _DisciplineName;

        public SelectTopicModel(ILmsService iLmsService,long selectDisciplineId, String teacherUserName)
        {
            IEnumerable<Topic> allowedTopics;
            User teacherUser = iLmsService.FindService<IUserService>().GetCurrentUser();
            IEnumerable<Course> availableCourses = iLmsService.FindService<ICourseService>().GetCourses(teacherUser);
            //
            allowedTopics = iLmsService.FindService<ICurriculumService>().GetTopicsByDisciplineId((int)selectDisciplineId)
                .Where(topic => availableCourses.Count(course => course.Id == topic.CourseRef) != 0);
            //
            if (allowedTopics != null & allowedTopics.Count() != 0)
                _AllowedTopics = allowedTopics;
            else
                _AllowedTopics = null;
            _TeacheUserName = teacherUserName;
            _DisciplineName = iLmsService.FindService<ICurriculumService>().GetDiscipline((int)selectDisciplineId).Name;
        }
        public String GetDisciplineName()
        {
            return this._DisciplineName;
        }
        public String GetTeacherUserName()
        {
            return this._TeacheUserName;
        }
        public bool NoData()
        {
            return _AllowedTopics == null;
        }
        public IEnumerable<Topic> GetAllowedTopics()
        {
            return this._AllowedTopics;
        }
    }
}