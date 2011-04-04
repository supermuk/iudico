using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;

namespace IUDICO.Statistics.Models.QualityTest
{
    public class IndexModel
    {
        private List<Curriculum> _AllowedCurriculums;
        private User _TeacherUser;
        public IndexModel(ILmsService iLmsService)
        {
            List<Curriculum> allowedCurriculums;
            User teacherUser;
            //teacherUser = iLmsService.FindService<IUserService>().GetTeacherId(???);
            //List<Curriculum> allowedCurriculums = iLmsService.FindService<???>().GetAlowedCurriculums();
            allowedCurriculums = FakeDataQualityTest.FakeAllowedCurriculums();
            teacherUser = FakeDataQualityTest.FakeTeacherUser();
            //
            if (allowedCurriculums != null & allowedCurriculums.Count != 0)
                _AllowedCurriculums = allowedCurriculums;
            else
                _AllowedCurriculums = null;
            _TeacherUser = teacherUser;
        }
        public bool NoData()
        {
            return _AllowedCurriculums == null;
        }
        public String GetTeacherUserName()
        {
            return this._TeacherUser.Username;
        }
        public List<Curriculum> GetAllowedCurriculums()
        {
            return this._AllowedCurriculums;
        }
    }
}