using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace IUDICO.Common
{
   public class Logger
   {
      private static volatile Logger instance;
      private static object syncRoot = new object();
      private log4net.ILog log;
      private bool enableLogging;

      protected Logger()
      {
         System.Configuration.Configuration rootWebConfig =
          System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);
         if (rootWebConfig.AppSettings.Settings.Count > 0)
         {
            System.Configuration.KeyValueConfigurationElement customSetting =
               rootWebConfig.AppSettings.Settings["EnableLogging"];
            if (customSetting != null)
            {
               this.enableLogging = bool.Parse(customSetting.Value);
            }
         }
      }

      public static Logger Instance
      {
         get
         {
            if (instance == null)
            {
               lock (syncRoot)
               {
                  if (instance == null)
                  {
                     instance = new Logger();
                  }
               }
            }

            return instance;
         }
      }

      public void Info(object cls, string msg)
      {
         if (!this.enableLogging)
         {
            return;
         }

         log4net.GlobalContext.Properties["Hostname"] = Dns.GetHostName();
         this.log = log4net.LogManager.GetLogger(cls.ToString());
         this.log.Info(msg);
      }

      public void Error(object cls, string msg)
      {
         if (!this.enableLogging)
         {
            return;
         }

         log4net.GlobalContext.Properties["Hostname"] = Dns.GetHostName();
         this.log = log4net.LogManager.GetLogger(cls.ToString());
         this.log.Error(msg);
      }

      public void Request(object cls, HttpRequest request, TimeSpan time)
      {
         if (!this.enableLogging)
         {
            return;
         }

         var msg = "Notification:request";
         msg += " requestTime:";
         msg += time.TotalMilliseconds + "ms";
         msg += " ip:";
         msg += request.UserHostAddress;
         msg += " method:";
         msg += request.HttpMethod;
         msg += " path:";
         msg += request.Path;
         msg += " userId:";
         msg += request.RequestContext.HttpContext.User.Identity.Name;
         msg += " userAgent:";
         msg += request.UserAgent;
         this.Info(cls, msg);
      }
   }
}