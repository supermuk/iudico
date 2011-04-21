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
        private IEnumerable<Curriculum> _AllowedCurriculums;
        private User _TeacherUser;
        public IndexModel(ILmsService iLmsService)
        {
            IEnumerable<Curriculum> allowedCurriculums;
            User teacherUser;
            teacherUser = iLmsService.FindService<IUserService>().GetCurrentUser();
            allowedCurriculums = iLmsService.FindService<ICurriculumService>().GetCurriculumsWithThemesOwnedByUser(teacherUser);
            //
            if (allowedCurriculums != null & allowedCurriculums.Count() != 0)
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
        public IEnumerable<Curriculum> GetAllowedCurriculums()
        {
            return this._AllowedCurriculums;
        }
    }
}