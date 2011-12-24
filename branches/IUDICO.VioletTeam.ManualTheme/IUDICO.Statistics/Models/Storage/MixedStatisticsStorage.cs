using System;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using System.Collections.Generic;

namespace IUDICO.Statistics.Models.Storage
{
    public class MixedStatisticsStorage : IStatisticsStorage
    {
        #region Private Fields

        private ICurriculumService _CurriculumService;
        private IUserService _UserService;
        private ITestingService _TestingService;

        #endregion

        #region Constructors

        public MixedStatisticsStorage(ILmsService lmsService)
        {
            LmsService = lmsService;
        }

        #endregion

        #region Protected Properties

        protected readonly ILmsService LmsService;

        protected ICurriculumService CurriculumService
        {
            get { return _CurriculumService ?? (_CurriculumService = LmsService.FindService<ICurriculumService>()); }
        }

        protected IUserService UserService
        {
            get
            {
                if (_UserService == null)
                {
                    _UserService = LmsService.FindService<IUserService>();
                }
                return _UserService;
            }
        }

        protected ITestingService TestingService
        {
            get
            {
                if (_TestingService == null)
                {
                    _TestingService = LmsService.FindService<ITestingService>();
                }
                return _TestingService;
            }
        }
        
        protected virtual IDataContext DBDataContext
        {
            get
            {
                return new DBDataContext();
            }
        }

        #endregion

        #region Protected Methods
        
        #endregion

        #region IStatisticsStorage Members

        public void SaveManualResult(Guid userId, int themeId, double score)
        {
            throw new NotImplementedException();
        }
        
        public double GetTotalForUserCurriculum(Guid userId, int curriculumId)
        {
            throw new NotImplementedException();
        }

        public int GetCurriculumIdByThemeId(int themeId)
        {
            var theme = CurriculumService.GetTheme(themeId);

            if (theme == null)
                throw new NullReferenceException("Can't find theme by specified themeId");

            if (theme.Stage == null)
                throw new NullReferenceException("Can't find stage by specified themeId");

            if (theme.Stage.Curriculum == null)
                throw new NullReferenceException("Can't find curriculum by specified themeId");
            
            return theme.Stage.Curriculum.Id;
        }

        public IEnumerable<Group> GetAllGroups()
        {
            return UserService.GetGroups();
        }

        public Group GetGroupById(int id)
        {
            return UserService.GetGroup(id);
        }

        public IEnumerable<Curriculum> GetCurrilulumsByGroupId(int id)
        {
            return CurriculumService.GetCurriculumsByGroupId(id);
        }

        #endregion
    }
}