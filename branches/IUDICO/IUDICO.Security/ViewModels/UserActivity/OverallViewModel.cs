using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Security.ViewModels.UserActivity
{
    public class OverallViewModel : LocalizedViewModel
    {
        private List<UserActivityStats> stats;

        public OverallViewModel(object obj) : base()
        {
            this.stats = new List<UserActivityStats>();
        }

        public OverallViewModel()
        {
            this.stats = new List<UserActivityStats>();
        }

        public void AddUserStats(
            User user,
            int totalNumberOfRequests,
            int todayNumberOfRequests,
            DateTime lastActivityTime)
        {
            this.stats.Add(new UserActivityStats(
                user, totalNumberOfRequests, todayNumberOfRequests, lastActivityTime));
        }

        public int GetOverallNumberOfRequests()
        {
            return this.stats.Sum(s => s.TotalNumberOfRequests);
        }

        public int GetOverallNumberOfRequestsForToday()
        {
            return this.stats
                .Where(s => s.LastActivityTime.Date == DateTime.Today)
                .Sum(s => s.TodayNumberOfRequests);
        }

        public IEnumerable<UserActivityStats> GetStats()
        {
            return this.stats;
        }

        public IEnumerable<UserActivityStats> GetUserActivity(string userID)
        {
            return this.stats.Where(s => s.User.UserId == userID);
        }

        public IEnumerable<UserActivityStats> GetUserActivityForToday(string userID)
        {
            return this.stats.Where(s => (s.User.UserId == userID) && (s.LastActivityTime == DateTime.Today));
        }

        public class UserActivityStats
        {
            public UserActivityStats(
                User user,
                int totalNumberOfRequests,
                int todayNumberOfRequests,
                DateTime lastActivityTime)
            {
                this.User = user;
                this.TotalNumberOfRequests = totalNumberOfRequests;
                this.TodayNumberOfRequests = todayNumberOfRequests;
                this.LastActivityTime = lastActivityTime;
            }

            public User User { get; private set; }
            public int TotalNumberOfRequests { get; private set; }
            public int TodayNumberOfRequests { get; private set; }
            public DateTime LastActivityTime { get; private set; }
        }
    }
}