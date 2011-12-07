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
    public class SelectGroupsModel
    {
        private IEnumerable<Group> _AllowedGroups;
        private String _TeacheUserName;
        private String _CurriculumName;
        private String _ThemeName;

        public SelectGroupsModel(ILmsService iLmsService, int selectThemeId, String teacherUserName, String curriculumName)
        {
            IEnumerable<Group> allowedGroups;
            Theme selectTheme;
            selectTheme = iLmsService.FindService<ICurriculumService>().GetTheme(selectThemeId);
            _ThemeName = selectTheme.Name;
            _TeacheUserName = teacherUserName;
            _CurriculumName = curriculumName;
            allowedGroups = iLmsService.FindService<ICurriculumService>().GetGroupsAssignedToTheme(selectThemeId);
            //
            if (allowedGroups != null & allowedGroups.Count() != 0)
                _AllowedGroups = allowedGroups;
            else
                _AllowedGroups = null;            
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
        public IEnumerable<Group> GetAllowedGroups()
        {
            return this._AllowedGroups;
        }
    }
}