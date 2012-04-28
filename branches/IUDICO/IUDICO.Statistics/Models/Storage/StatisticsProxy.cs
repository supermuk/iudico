using System.Collections.Generic;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Statistics.Models.Storage
{
    public class StatisticsProxy : IStatisticsProxy, IStatisticsService
    {
        private readonly ILmsService lmsService;

        #region Constructors

        /// <summary>
        /// Default constructor .
        /// </summary>
        public StatisticsProxy()
        {

        }

        /// <summary>
        /// Constructor with parameters .
        /// </summary>
        /// <param name="lmsService">ILmsService service .</param>
        public StatisticsProxy(ILmsService lmsService)
        {
            this.lmsService = lmsService;
        }

        #endregion

        #region StatisticsMethods

        /// <summary>
        /// Gets all groups, using method GetGroups() from IUserService .
        /// </summary>
        /// <returns>IEnumerable<IUDICO.Common.Models.Group> All groups .</returns>
        public IEnumerable<Group> GetAllGroups()
        {
            return this.lmsService.FindService<IUserService>().GetGroups();
        }

        /// <summary>
        /// Gets all discipline dy group id using method GetDisciplinesByGroupId(int groupId) from IDisciplineService .
        /// </summary>
        /// <param name="groupId">int Selected group id .</param>
        /// <returns>IEnumerable<Discipline> Curriculums by group id .</returns>
        public IEnumerable<Curriculum> GetCurrilulumsByGroupId(int groupId)
        {
            return this.lmsService.FindService<ICurriculumService>().GetCurriculums(curr => curr.UserGroupRef == groupId);
        }
        
        #endregion
    }
}
