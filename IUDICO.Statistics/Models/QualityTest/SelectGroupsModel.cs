using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;

namespace IUDICO.Statistics.Models.QualityTest
{
    public class SelectGroupsModel
    {
        private List<Group> _AllowedGroups;
        private String _TeacheUserName;
        private String _CurriculumName;
        private String _ThemeName;

        public SelectGroupsModel(ILmsService iLmsService, int selectThemeId, String teacherUserName, String curriculumName)
        {
            List<Group> allowedGroups;
            //List<Theme> allowedThemes = iLmsService.FindService<???>().GetThemesByCurriculumId(selectCurriculumId);
            allowedGroups = FakeDataQualityTest.FakeGroupsByThemeIdOrCurriculumId(selectThemeId);
            //
            if (allowedGroups != null & allowedGroups.Count != 0)
                _AllowedGroups = allowedGroups;
            else
                _AllowedGroups = null;
            _TeacheUserName = teacherUserName;
            _CurriculumName = curriculumName;
            //_ThemeName = iLmsService.FindService<???>().GetThemeNameByThemeId(selectThemeId);
            _ThemeName = FakeDataQualityTest.FakeThemeName(selectThemeId);
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
        public bool NoData()
        {
            return _AllowedGroups == null;
        }
        public List<Group> GetAllowedGroups()
        {
            return this._AllowedGroups;
        }
    }
}