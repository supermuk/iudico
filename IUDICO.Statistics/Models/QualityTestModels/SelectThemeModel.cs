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
    public class SelectThemeModel
    {
        private IEnumerable<Theme> _AllowedThemes;
        private String _TeacheUserName;
        private String _CurriculumName;

        public SelectThemeModel(ILmsService iLmsService,long selectCurriculumId, String teacherUserName)
        {
            IEnumerable<Theme> allowedThemes;
            User teacherUser = iLmsService.FindService<IUserService>().GetCurrentUser();
            IEnumerable<Course> availableCourses = iLmsService.FindService<ICourseService>().GetCourses(teacherUser);
            //
            allowedThemes = iLmsService.FindService<ICurriculumService>().GetThemesByCurriculumId((int)selectCurriculumId)
                .Where(theme => availableCourses.Count(course => course.Id == theme.CourseRef) != 0);
            //
            if (allowedThemes != null & allowedThemes.Count() != 0)
                _AllowedThemes = allowedThemes;
            else
                _AllowedThemes = null;
            _TeacheUserName = teacherUserName;
            _CurriculumName = iLmsService.FindService<ICurriculumService>().GetCurriculum((int)selectCurriculumId).Name;
        }
        public String GetCurriculumName()
        {
            return this._CurriculumName;
        }
        public String GetTeacherUserName()
        {
            return this._TeacheUserName;
        }
        public bool NoData()
        {
            return _AllowedThemes == null;
        }
        public IEnumerable<Theme> GetAllowedThemes()
        {
            return this._AllowedThemes;
        }
    }
}