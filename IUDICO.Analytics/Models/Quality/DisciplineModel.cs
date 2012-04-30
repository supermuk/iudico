using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Analytics.Models.Quality
{
    public class DisciplineModel
    {
        private IEnumerable<Discipline> allowedDisciplines;
        public DisciplineModel(ILmsService ilmsService)
        {
            IEnumerable<Discipline> temp_allowedDisciplines;
            temp_allowedDisciplines = ilmsService.FindService<IDisciplineService>().GetDisciplines();
            if (temp_allowedDisciplines != null & temp_allowedDisciplines.Count() != 0)
            {
                this.allowedDisciplines = temp_allowedDisciplines;
            }
            else
            {
                this.allowedDisciplines = null;
            }
        }
        public bool NoData()
        {
            return this.allowedDisciplines == null;
        }
        public IEnumerable<Discipline> GetAllowedDisciplines()
        {
            return this.allowedDisciplines;
        }
    }

}