using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models.Plugin;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models;
using System.Web.Routing;
using IUDICO.Security.Models.Storages;
using IUDICO.Security.Models.Storages.Database;
using Action = IUDICO.Common.Models.Action;
using IUDICO.Common.Models.Notifications;
using System.IO;
using IUDICO.Security.Helpers;
using IUDICO.Common.Models.Shared;
using System.Text;

namespace IUDICO.Security
{
    public class SecurityPlugin : IPlugin, IWindsorInstaller
    {
        internal static IWindsorContainer Container; 
        private const String SECURITY_PLUGIN_NAME = "SecurityPlugin";

        #region IWindsorInstaller

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes
                    .FromThisAssembly()
                    .BasedOn<IController>()
                    .Configure(c => c.LifeStyle.Transient
                                        .Named(c.Implementation.Name)),
                Component.For<IPlugin>().ImplementedBy<SecurityPlugin>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<ISecurityService>().ImplementedBy<SecurityService>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<IBanStorage>().ImplementedBy<DatabaseBanStorage>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<ISecurityStorage>().ImplementedBy<DatabaseSecurityStorage>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton)
            );

            Container = container;
        }
        #endregion

        #region IPlugin

        public string GetName()
        {
            return Localization.GetMessage(Localization.Keys.SECURITY_PLUGIN);
        }

        public IEnumerable<Action> BuildActions()
        {
            return new Action[]
            {
                new Action(Localization.GetMessage(Localization.Keys.SECURITY), "Security/Index"),
                new Action(Localization.GetMessage(Localization.Keys.USER_ACTIVITY), "UserActivity/Index")
            };
        }

        public IEnumerable<MenuItem> BuildMenuItems()
        {
            return new MenuItem[]
            {
                new MenuItem(Localization.GetMessage(Localization.Keys.SECURITY), "Security", "Index")
            };
        }

        public void Setup(IWindsorContainer container)
        {
            // Unused?
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "Ban",
                "Ban/{action}",
                new { controller = "Ban" });

            routes.MapRoute(
                "Security",
                "Security/{action}",
                new { controller = "Security" });

            routes.MapRoute(
                "UserActivity",
                "UserActivity/{action}",
                new { controller = "UserActivity" });
        }

        public void Update(string evt, params object[] data)
        {
            if (evt == LMSNotifications.ApplicationRequestStart)
            {
                ApplicationRequestStart(data);
            }
            else if (evt == LMSNotifications.ApplicationRequestEnd)
            {
                ApplicationRequestEnd(data);
            }
        }

        #endregion

        private void ApplicationRequestStart(params object[] data)
        {
            var response = HttpContext.Current.Response;
            response.Filter = new ObserveResponseLengthStream(response.Filter);

            var context = HttpContext.Current;
            context.Items["REQUEST_START_DATETIME"] = DateTime.Now;

            var securityService = Container.Resolve<ISecurityService>();
            var action = HttpContext.Current.Request.RequestContext.RouteData.Values["action"];
            if (action != null && action.ToString() != "Banned")
            {
                if (!securityService.CheckRequestSafety(new HttpRequestWrapper(HttpContext.Current.Request)))
                {
                    HttpContext.Current.Response.Redirect("/Ban/Banned", true);
                }
            }
        }

        private void ApplicationRequestEnd(params object[] data)
        {
            var context = HttpContext.Current;
            var filter = context.Response.Filter;

            var storage = Container.Resolve<ISecurityStorage>();
            var usersService = Container.Resolve<IUserService>();

            var userActivity = new UserActivity
            {
                ResponseLength = (int) filter.Length,
                RequestStartTime = (DateTime)context.Items["REQUEST_START_DATETIME"],
                RequestEndTime = DateTime.Now
            };

            var currentUser = usersService.GetCurrentUser();
            if (currentUser != null)
            {
                userActivity.UserRef = currentUser.Id;
            }

            userActivity.Request = GetRawRequest(context.Request);
            userActivity.RequestLength = userActivity.Request.Length;

            storage.CreateUserActivity(userActivity);
        }

        private string GetRawRequest(HttpRequest request)
        {
            var sb = new StringBuilder();
            
            sb.AppendFormat("{0} {1} {2} ",
                request.HttpMethod,
                request.RawUrl,
                request.ServerVariables["SERVER_PROTOCOL"]);

            sb.AppendLine(request.Headers.ToString());

            var requestStream = request.InputStream;
            var reader = new StreamReader(requestStream);

            sb.Append(" ");
            sb.AppendLine(reader.ReadToEnd());

            reader.Close();

            return sb.ToString();
        }
    }
}