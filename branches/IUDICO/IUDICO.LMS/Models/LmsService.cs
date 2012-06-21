using System.Diagnostics;
using System.Threading;
using IUDICO.Common;
using IUDICO.Common.Models.Services;
using Castle.Windsor;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Reflection;
using IUDICO.Common.Models.Attributes;
using System.Web;
using IUDICO.Common.Models.Notifications;

namespace IUDICO.LMS.Models
{
    public class LmsService : ILmsService
    {
        protected readonly IWindsorContainer Container;
        protected string culture;
        protected readonly string ServerPath;

        protected Menu Menu
        {
            get
            {
                return HttpContext.Current.Items["menu"] as Menu;
            }


            set
            {
                if (HttpContext.Current.Items.Contains("menu"))
                    HttpContext.Current.Items["menu"] = value;
                else
                    HttpContext.Current.Items.Add("menu", value);
            }
        }


        protected Dictionary<IPlugin, IEnumerable<IUDICO.Common.Models.Action>> Actions
        {
            get
            {
                return HttpContext.Current.Items["actions"] as Dictionary<IPlugin, IEnumerable<IUDICO.Common.Models.Action>>;
            }


            set
            {
                if (HttpContext.Current.Items.Contains("actions"))
                    HttpContext.Current.Items["actions"] = value;
                else
                    HttpContext.Current.Items.Add("actions", value);
            }
        }

        public LmsService(IWindsorContainer container)
        {
            this.Container = container;
            this.ServerPath = AppDomain.CurrentDomain.BaseDirectory;
            // new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
        }

        #region ILmsService Members
        public T FindService<T>() where T : IService
        {
            return this.Container.Resolve<T>();
        }

        public string GetDbConnectionString()
        {
            return Common.Properties.Settings.Default.IUDICOConnectionString;
        }

        public Menu GetMenu()
        {
            if (this.Menu == null || Thread.CurrentThread.CurrentCulture.ToString() != this.culture)
                this.RebuildMenuAndActions();

            return this.Menu;
        }

        public Dictionary<IPlugin, IEnumerable<IUDICO.Common.Models.Action>> GetActions()
        {
            if (this.Actions == null || Thread.CurrentThread.CurrentCulture.ToString() != this.culture)
            {
                this.RebuildMenuAndActions();
               
            }
            return this.Actions;
        }

        public void Inform(string evt, params object[] data)
        {
            if (evt == LMSNotifications.ApplicationRequestStart)
            {
                var context = (HttpContext)data[0];

                var stopwatch = new Stopwatch();
                context.Items["stopwatch"] = stopwatch;
                stopwatch.Start();
            }
            else if (evt == LMSNotifications.ApplicationRequestEnd)
            {
                var context = (HttpContext)data[0];
                var stopwatch = (Stopwatch)context.Items["stopwatch"];
                stopwatch.Stop();

                var timespan = stopwatch.Elapsed;

                var request = (HttpRequest)data[1];

                Logger.Instance.Request(this, request, timespan);
            }
            else
            {
                Logger.Instance.Info(this, "Notification:" + evt);
            }

            var plugins = this.Container.ResolveAll<IPlugin>();

            this.Update(evt, data);
            foreach (var plugin in plugins)
            {
                plugin.Update(evt, data);
            }

        }

        public string GetServerPath()
        {
            return this.ServerPath;
        }
        #endregion

        protected void Update(string evt, params object[] data)
        {
            if (evt == UserNotifications.UserLogin || evt == UserNotifications.UserLogout || evt == LMSNotifications.ActionsChanged)
            {
                // temporary hack
                this.Menu = null;
                this.Actions = null;
                // RebuildMenuAndActions();
            }
        }

        protected void RebuildMenuAndActions()
        {
            this.Menu = new Menu();
            this.Actions = new Dictionary<IPlugin, IEnumerable<Common.Models.Action>>();

            var roles = FindService<IUserService>().GetCurrentUserRoles();
            var plugins = this.Container.ResolveAll<IPlugin>();

            foreach (var plugin in plugins)
            {
                this.Menu.Add(plugin.BuildMenuItems().Where(m => this.IsAllowed(m.Controller, m.Action, roles)));
                this.Actions.Add(
                    plugin,
                    plugin.BuildActions().Where(a =>
                        this.IsAllowed(a.Link.Split('/').First(), a.Link.Split('/').Skip(1).First(), roles)));
            }
            this.culture = Thread.CurrentThread.CurrentCulture.ToString();
        }

        protected bool IsPost(MethodInfo action)
        {
            return Attribute.GetCustomAttribute(action, typeof(HttpPostAttribute), false) != null;
        }

        protected bool IsAllowed(string strController, string strAction, IEnumerable<Role> roles)
        {
            // if can't resolve controller, don't allow access to it
            try
            {
                var controller = this.Container.Resolve<IController>(strController + "controller");
                var action = controller.GetType().GetMethods().FirstOrDefault(m => m.Name == strAction && !this.IsPost(m) && m.GetParameters().Length == 0);

                var attribute = Attribute.GetCustomAttribute(action, typeof(AllowAttribute), false) as AllowAttribute;

                if (attribute == null)
                {
                    return true;
                }
                else if (attribute.Role == Role.None)
                {
                    return !roles.Contains(Role.None);
                }
                else
                {
                    return roles.Any(i => i != Role.None && attribute.Role.HasFlag(i));
                }
            }
            catch
            {
                return false;
            }
        }
    }
}