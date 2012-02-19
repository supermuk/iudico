using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Statistics.Models.Storage
{
    public class StatisticsProxy : IStatisticsProxy, IStatisticsService
    {
        private readonly ILmsService _LmsService;

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
            _LmsService = lmsService;
        }

        #endregion

        #region StatisticsMethods

        /// <summary>
        /// Gets all groups, using method GetGroups() from IUserService .
        /// </summary>
        /// <returns>IEnumerable<IUDICO.Common.Models.Group> All groups .</returns>
        public IEnumerable<Group> GetAllGroups()
        {
            return _LmsService.FindService<IUserService>().GetGroups();
        }

        /// <summary>
        /// Gets all discipline dy group id using method GetDisciplinesByGroupId(int groupId) from IDisciplineService .
        /// </summary>
        /// <param name="groupId">int Selected group id .</param>
        /// <returns>IEnumerable<Discipline> Disciplines by group id .</returns>
        public IEnumerable<Discipline> GetCurrilulumsByGroupId(int groupId)
        {
            IEnumerable<Discipline> disciplines = _LmsService.FindService<ICurriculumService>().GetDisciplinesByGroupId(groupId);
            return disciplines;
        }
        
        #endregion
    }
}
