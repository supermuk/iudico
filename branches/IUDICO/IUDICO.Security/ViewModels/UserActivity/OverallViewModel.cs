using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Security.ViewModels.UserActivity
{
    public class OverallViewModel : LocalizedViewModel
    {
        private List<UserActivityStats> _stats;

        public OverallViewModel(Object obj) : base(null)
        {
            _stats = new List<UserActivityStats>();
        }

        public OverallViewModel()
        {
            _stats = new List<UserActivityStats>();
        }

        public void AddUserStats(User user,
                int totalNumberOfRequests,
                int todayNumberOfRequests,
                DateTime lastActivityTime)
        {
            _stats.Add(new UserActivityStats(
                user, totalNumberOfRequests, todayNumberOfRequests, lastActivityTime));
        }

        public int GetOverallNumberOfRequests()
        {
            return _stats.Sum(s => s.TotalNumberOfRequests);
        }

        public int GetOverallNumberOfRequestsForToday()
        {
            return _stats
                .Where(s => s.LastActivityTime.Date == DateTime.Today)
                .Sum(s => s.TodayNumberOfRequests);
        }

        public IEnumerable<UserActivityStats> GetStats()
        {
            return _stats;
        }

        public class UserActivityStats
        {
            public UserActivityStats(
                User user,
                int totalNumberOfRequests,
                int todayNumberOfRequests,
                DateTime lastActivityTime)
            {
                User = user;
                TotalNumberOfRequests = totalNumberOfRequests;
                TodayNumberOfRequests = todayNumberOfRequests;
                LastActivityTime = lastActivityTime;
            }

            public User User { get; private set; }
            public int TotalNumberOfRequests { get; private set; }
            public int TodayNumberOfRequests { get; private set; }
            public DateTime LastActivityTime { get; private set; }
        }
    }
}