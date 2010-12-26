using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models;
using IUDICO.CurriculumManagement.Models.Storage;

namespace IUDICO.CurriculumManagement.Models
{
    public class CurriculumService : ICurriculumService
    {
        private ICurriculumStorage curriculumStorage;

        public CurriculumService(ICurriculumStorage curriculumStorage)
        {
            this.curriculumStorage = curriculumStorage;
        }

        #region ICurriculumService Members

        public IEnumerable<Curriculum> GetCurriculums()
        {
            return curriculumStorage.GetCurriculums();
        }

        public IEnumerable<Curriculum> GetCurriculums(IEnumerable<int> ids)
        {
            return curriculumStorage.GetCurriculums(ids);
        }

        public Curriculum GetCurriculum(int id)
        {
            return curriculumStorage.GetCurriculum(id);
        }

        public IEnumerable<Stage> GetStages(int curriculumId)
        {
            return curriculumStorage.GetStages(curriculumId);
        }

        public IEnumerable<Stage> GetStages(IEnumerable<int> ids)
        {
            return curriculumStorage.GetStages(ids);
        }

        public Stage GetStage(int id)
        {
            return curriculumStorage.GetStage(id);
        }

        public IEnumerable<Theme> GetThemes(int stageId)
        {
            return curriculumStorage.GetThemes(stageId);
        }

        public IEnumerable<Theme> GetThemes(IEnumerable<int> ids)
        {
            return curriculumStorage.GetThemes(ids);
        }

        public Theme GetTheme(int id)
        {
            return curriculumStorage.GetTheme(id);
        }

        public IEnumerable<Timeline> GetTimelines()
        {
            return curriculumStorage.GetTimelines();
        }
        #endregion
    }
}