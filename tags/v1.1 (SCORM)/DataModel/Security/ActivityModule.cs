using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Collections.Specialized;
using System.Linq;
using System.Collections.Generic;

namespace IUDICO.DataModel.Security
{
    public class ActivityModule : IHttpModule
    {
        public ActivityModule () { }

        void IHttpModule.Dispose() { }

        void IHttpModule.Init(HttpApplication context)
        {
            context.PostAuthenticateRequest += new EventHandler(this.PostAuthenticateRequest);
        }

        private void PostAuthenticateRequest(object sender, EventArgs evargs)
        {
            HttpApplication app = sender as HttpApplication;
            Dictionary<string, DateTime> activity = (Dictionary<string, DateTime>)app.Application["activityList"];

            if (activity == null)
            {
                activity = new Dictionary<string, DateTime>();
            }
            try
            {
                string login = app.User.Identity.Name;
                if (activity.Keys.Contains(login))
                {
                    activity[login] = DateTime.Now;
                }
                else
                {
                    activity.Add(login, DateTime.Now);
                }
            }
            catch (Exception ex) { }

            app.Application["activityList"] = activity;
        }
    }
}
