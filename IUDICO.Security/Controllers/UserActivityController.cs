using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models.Attributes;
using IUDICO.Common.Models;
using IUDICO.Security.Models.Storages;
using IUDICO.Security.ViewModels.UserActivity;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Controllers;

namespace IUDICO.Security.Controllers
{
    public class UserActivityController : PluginController
    {
        protected ISecurityStorage SecurityStorage { get; private set; }

        public UserActivityController(ISecurityStorage securityStorage)
        {
            this.SecurityStorage = securityStorage;
        }
        // GET: /UserActivity/

        [Allow(Role = Role.Admin)]
        public ActionResult Index()
        {
            return View(new IndexViewModel());
        }


        [Allow(Role = Role.Admin)]
        public ActionResult Overall()
        {
            var activitiesByUser = this.SecurityStorage
                .GetUserActivities()
                .GroupBy(ua => ua.UserRef)
                .Select(g => new
                    {
                        UserRef = g.Key,
                        TotalNumberOfRequests = g.Count(),
                        TodayNumberOfRequests = g.Count(ua => ua.RequestStartTime.Date == DateTime.Today),
                        LastActivityTime = g.Max(ua => ua.RequestStartTime)
                    });

            var userService = SecurityPlugin.Container.Resolve<IUserService>();
            var users = userService.GetUsers().ToList();
            
            var model = new OverallViewModel();
            
            foreach (var stats in activitiesByUser.OrderBy(x => x.UserRef))
            {
                model.AddUserStats(
                    users.FirstOrDefault(u => u.Id == stats.UserRef),
                    stats.TotalNumberOfRequests,
                    stats.TodayNumberOfRequests,
                    stats.LastActivityTime);
            }

            return View(model);
        }
    }
}
