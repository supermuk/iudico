using System.Collections.Generic;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models;
using IUDICO.CurriculumManagement.Models.Storage;

namespace IUDICO.CurriculumManagement.Models
{
    public class CurriculumService : ICurriculumService
    {
        private readonly ICurriculumStorage _CurriculumStorage;

        public CurriculumService(ICurriculumStorage curriculumStorage)
        {
            _CurriculumStorage = curriculumStorage;
        }

        #region ICurriculumService Members

        public IEnumerable<Curriculum> GetCurriculums()
        {
            return _CurriculumStorage.GetCurriculums();
        }

        public IEnumerable<Curriculum> GetCurriculums(IEnumerable<int> ids)
        {
            return _CurriculumStorage.GetCurriculums(ids);
        }

        public Curriculum GetCurriculum(int id)
        {
            return _CurriculumStorage.GetCurriculum(id);
        }

        public IEnumerable<Stage> GetStages(int curriculumId)
        {
            return _CurriculumStorage.GetStages(curriculumId);
        }

        public IEnumerable<Stage> GetStages(IEnumerable<int> ids)
        {
            return _CurriculumStorage.GetStages(ids);
        }

        public Stage GetStage(int id)
        {
            return _CurriculumStorage.GetStage(id);
        }

        public IEnumerable<Theme> GetThemesByStageId(int stageId)
        {
            return _CurriculumStorage.GetThemesByStageId(stageId);
        }

        public IEnumerable<Theme> GetThemes(IEnumerable<int> ids)
        {
            return _CurriculumStorage.GetThemes(ids);
        }

        public Theme GetTheme(int id)
        {
            return _CurriculumStorage.GetTheme(id);
        }

        public IEnumerable<Curriculum> GetCurriculumsByGroupId(int groupId)
        {
            return _CurriculumStorage.GetCurriculumsByGroupId(groupId);
        }

        public IEnumerable<Theme> GetThemesByCurriculumId(int curriculumId)
        {
            return _CurriculumStorage.GetThemesByCurriculumId(curriculumId);
        }

        public IEnumerable<Theme> GetThemesByGroupId(int groupId)
        {
            return _CurriculumStorage.GetThemesByGroupId(groupId);
        }

        #endregion
    }
}