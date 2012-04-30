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
    public class IndexModel
    {
        private IEnumerable<Discipline> _AllowedDisciplines;
        private User _TeacherUser;
        public IndexModel(ILmsService iLmsService)
        {
            IEnumerable<Discipline> allowedDisciplines;
            User teacherUser;
            teacherUser = iLmsService.FindService<IUserService>().GetCurrentUser();
            //TODO: review this method
            allowedDisciplines=new Discipline[]{};
            //allowedDisciplines = iLmsService.FindService<IDisciplineService>().GetDisciplinesWithTopicsOwnedByUser(teacherUser);
            //
            if (allowedDisciplines != null & allowedDisciplines.Count() != 0)
                _AllowedDisciplines = allowedDisciplines;
            else
                _AllowedDisciplines = null;
            _TeacherUser = teacherUser;
        }
        public bool NoData()
        {
            return _AllowedDisciplines == null;
        }
        public String GetTeacherUserName()
        {
            return this._TeacherUser.Username;
        }
        public IEnumerable<Discipline> GetAllowedDisciplines()
        {
            return this._AllowedDisciplines;
        }
    }
}