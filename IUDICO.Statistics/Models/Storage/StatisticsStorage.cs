using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;


namespace IUDICO.Statistics.Models.Storage
{
    public class StatisticsStorage : IStatisticsStorage, IStatisticsService
    {
        private readonly ILmsService _LmsService;

        #region Results for ShowCurriculumStatistic.aspx
        public IEnumerable<User> Students { get; set; }
        public IEnumerable<Curriculum> Curriculums { get; set; }
        public List<KeyValuePair<List<Theme>, int>> Themes { get; set; }
        public IEnumerable<AttemptResult> Results { get; set; }

        public IEnumerable<Curriculum> GetSelectedCurriclums(System.Int32[] curriculumsId)
        {
            IEnumerable<int> ids = curriculumsId;
            return _LmsService.FindService<ICurriculumService>().GetCurriculums(curriculumsId);
        }

        public IEnumerable<User> GetUsersBySelectedGroup(Group group)
        {
            return _LmsService.FindService<IUserService>().GetUsersByGroup(group);
        }

        public List<KeyValuePair<List<Theme>, int>> GetAllThemes(int groupId)
        {
            List<KeyValuePair<List<Theme>, int>> temp = new List<KeyValuePair<List<Theme>, int>>();
            List<Curriculum> curriculums = new List<Curriculum>();
            curriculums = _LmsService.FindService<ICurriculumService>().GetCurriculumsByGroupId(groupId).ToList<Curriculum>();

            foreach (Curriculum curr in curriculums)
            {
                KeyValuePair<List<Theme>, int> temp1 = new KeyValuePair<List<Theme>, int>();
                temp1 = new KeyValuePair<List<Theme>, int>(_LmsService.FindService<ICurriculumService>().GetThemesByCurriculumId(curr.Id).ToList<Theme>(), curr.Id);
                temp.Add(temp1);
            }

            return temp;
        }

        #endregion

        #region StatisticsStorage constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public StatisticsStorage()
        {

        }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="lmsService">Lms service</param>
        public StatisticsStorage(ILmsService lmsService)
        {
            _LmsService = lmsService;
        }

        #endregion

        #region StatisticsMethods

        /// <summary>
        /// Gets all groups, using method GetGroups() from IUserService
        /// </summary>
        /// <returns>All groups</returns>
        public IEnumerable<Group> GetAllGroups()
        {
            return _LmsService.FindService<IUserService>().GetGroups();
        }

        /// <summary>
        /// Gets all curriculum dy group id using method GetCurriculumsByGroupId(int groupId) from ICurriculumService 
        /// </summary>
        /// <param name="groupId">Select group id</param>
        /// <returns>Curriculums</returns>
        public IEnumerable<Curriculum> GetCurrilulumsByGroupId(int groupId)
        {
            return _LmsService.FindService<ICurriculumService>().GetCurriculumsByGroupId(groupId);
        }

        /// <summary>
        /// Gets all theme by curriculum id using method GetThemesByCurriculumId(int curriculumId) from ICurriculumService 
        /// </summary>
        /// <param name="curriculumsId">Select curriculum id</param>
        /// <returns>Themes</returns>
        public IEnumerable<Theme> GetThemesByCurriculumId(int curriculumsId)
        {
            return _LmsService.FindService<ICurriculumService>().GetThemesByCurriculumId(curriculumsId);
        }

        /// <summary>
        /// Gets AttemptResult (result) by user, theme using method GetResults(User user, Theme theme) from IUserService
        /// </summary>
        /// <param name="user">Selected user</param>
        /// <param name="theme">Selected theme</param>
        /// <returns></returns>
        public IEnumerable<AttemptResult> GetResults(User user, Theme theme)
        {
            return _LmsService.FindService<ITestingService>().GetResults(user, theme);
        }

        #endregion

    }
}
