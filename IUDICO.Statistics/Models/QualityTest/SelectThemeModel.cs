using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;

namespace IUDICO.Statistics.Models.QualityTest
{
    public class SelectThemeModel
    {
        private List<Theme> _AllowedThemes;
        private String _TeacheUserName;
        private String _CurriculumName;

        public SelectThemeModel(ILmsService iLmsService,long selectCurriculumId, String teacherUserName)
        {
            List<Theme> allowedThemes;
            //List<Theme> allowedThemes = iLmsService.FindService<???>().GetThemesByCurriculumId(selectCurriculumId);
            allowedThemes = FakeDataQualityTest.FakeThemesByCurriculumId();
            //
            if (allowedThemes != null & allowedThemes.Count != 0)
                _AllowedThemes = allowedThemes;
            else
                _AllowedThemes = null;
            _TeacheUserName = teacherUserName;
            //_CurriculumName = iLmsService.FindService<???>().GetCurriculumNameByCurriculumId(selectCurriculumId);
            _CurriculumName = FakeDataQualityTest.FakeCurriculumName(selectCurriculumId);
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
        public List<Theme> GetAllowedThemes()
        {
            return this._AllowedThemes;
        }
    }
}