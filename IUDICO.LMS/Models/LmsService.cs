using System.Diagnostics;
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
    public class LmsService: ILmsService
    {
        protected readonly IWindsorContainer _Container;

        protected Menu menu
        {
            get
            {
                return HttpContext.Current.Session["menu"] as Menu;
            }

            set
            {
                HttpContext.Current.Session["menu"] = value;
            }
        }

        protected Dictionary<IPlugin, IEnumerable<IUDICO.Common.Models.Action>> actions
        {
            get
            {
                return HttpContext.Current.Session["actions"] as Dictionary<IPlugin, IEnumerable<IUDICO.Common.Models.Action>>;
            }

            set
            {
                HttpContext.Current.Session["actions"] = value;
            }
        }

        public LmsService(IWindsorContainer container)
        {
            _Container = container;
        }

        #region ILmsService Members
        public T FindService<T>() where T : IService
        {
            return _Container.Resolve<T>();
        }

        public string GetDbConnectionString()
        {
            return Common.Properties.Settings.Default.IUDICOConnectionString;
        }

        /*public DBDataContext GetDbDataContext()
        {
            return new DBDataContext();
        }

        public IDataContext GetIDataContext()
        {
            return GetDbDataContext();
        }

        public DbConnection GetDbConnection()
        {
            throw new NotImplementedException();
        }*/

        public Menu GetMenu()
        {
            if (menu == null)
                RebuildMenuAndActions();

            return menu;
        }

        public Dictionary<IPlugin, IEnumerable<IUDICO.Common.Models.Action>> GetActions()
        {
            if (actions == null)
                RebuildMenuAndActions();

            return actions;
        }

        public void Inform(string evt, params object[] data)
        {

            if (evt == LMSNotifications.ApplicationRequestStart)
            {
                var context = (HttpContext) data[0];

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

                var request = (HttpRequest) data[1];

                Logger.Instance.Request(this, request, timespan);
            }
            else
            {
                Logger.Instance.Info(this, "Notification:"+evt);
            }


            var plugins = _Container.ResolveAll<IPlugin>();

            Update(evt, data);
            foreach (var plugin in plugins)
            {
                plugin.Update(evt, data);
            }

        }
        #endregion

        protected void Update(string evt, params object[] data)
        {
            if (evt == UserNotifications.UserLogin || evt == UserNotifications.UserLogout)
            {
                // temporary hack
                menu = null;
                actions = null;
                //RebuildMenuAndActions();
            }
        }

        protected void RebuildMenuAndActions()
        {
            menu = new Menu();
            actions = new Dictionary<IPlugin, IEnumerable<Common.Models.Action>>();

            var roles = FindService<IUserService>().GetCurrentUserRoles();
            var plugins = _Container.ResolveAll<IPlugin>();

            foreach (var plugin in plugins)
            {
                menu.Add(plugin.BuildMenuItems().Where(m => IsAllowed(m.Controller, m.Action, roles)));
                actions.Add(
                    plugin,
                    plugin.BuildActions().Where(a =>
                        IsAllowed(a.Link.Split('/').First(), a.Link.Split('/').Skip(1).First(), roles)
                    )
                );
            }
        }

        protected bool IsPost(MethodInfo action)
        {
            return Attribute.GetCustomAttribute(action, typeof(HttpPostAttribute), false) != null;
        }

        protected bool IsAllowed(string controller, string action, IEnumerable<Role> roles)
        {
            // if can't resolve controller, don't allow access to it
            try
            {
                var _controller = _Container.Resolve<IController>(controller + "controller");
                var _action = _controller.GetType().GetMethods().Where(m => m.Name == action && !IsPost(m) && m.GetParameters().Length == 0).FirstOrDefault();

                var attribute = Attribute.GetCustomAttribute(_action, typeof(AllowAttribute), false) as AllowAttribute;

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