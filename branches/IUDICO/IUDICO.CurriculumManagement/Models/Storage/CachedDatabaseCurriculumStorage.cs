using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Cache;

namespace IUDICO.CurriculumManagement.Models.Storage
{
    public class CachedDatabaseCurriculumStorage: DatabaseCurriculumStorage
    {
        protected ICacheProvider _cacheProvider;

        public CachedDatabaseCurriculumStorage(ILmsService lmsService, ICacheProvider cachePrvoider)
            : base(lmsService)
        {
            _cacheProvider = cachePrvoider;
        }

        #region Curriculum methods

        public IList<Curriculum> GetCurriculums()
        {
            return base.GetCurriculums();
        }

        public IList<Curriculum> GetCurriculums(IEnumerable<int> ids)
        {
            return base.GetCurriculums(ids);
        }

        public IList<Curriculum> GetCurriculums(User user)
        {
            return base.GetCurriculums(user);
        }
        /*
        public IList<Curriculum> GetCurriculumsByDisciplineId(int disciplineId)
        {
            return base.GetCurriculumsByDisciplineId(disciplineId);
        }
        */
        #endregion
    }
}