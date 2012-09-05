using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using log4net.Config;


namespace IUDICO.Common
{
   public interface ILoggerService
   {
      void Info(string message);
      void Warn(string message);
      void Debug(string message);
      void Error(string message);
      void Error(Exception ex);
      void Fatal(string message);
      void Fatal(Exception ex);
   }

   public class Log4NetLoggerService : ILoggerService
   {
      private readonly ILog logger;
      private bool enableLogging;

      public Log4NetLoggerService()
      {
         this.logger = LogManager.GetLogger(
             System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
      public static void InitLogger()
      {
         XmlConfigurator.Configure();
      }



      public void Info(string message)
      {
         if (!this.enableLogging)
         {
            return;
         }
         this.logger.Info(message);
      }

      public void Warn(string message)
      {
         if (!this.enableLogging)
         {
            return;
         }

         this.logger.Warn(message);
      }

      public void Debug(string message)
      {
         if (!this.enableLogging)
         {
            return;
         }

         this.logger.Debug(message);
      }

      public void Error(string message)
      {
         if (!this.enableLogging)
         {
            return;
         }

         this.logger.Error(message);
      }

      public void Error(Exception ex)
      {
         if (!this.enableLogging)
         {
            return;
         }

         this.logger.Error(ex.Message, ex);
      }

      public void Fatal(string message)
      {
         if (!this.enableLogging)
         {
            return;
         }

         this.logger.Fatal(message);
      }

      public void Fatal(Exception ex)
      {
         if (!this.enableLogging)
         {
            return;
         }

         this.logger.Fatal(ex.Message, ex);
      }
   }
}

