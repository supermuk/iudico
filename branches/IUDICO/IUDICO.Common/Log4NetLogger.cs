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

        public Log4NetLoggerService()
        {
            this.logger = LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }
        public static void InitLogger()
        {
            XmlConfigurator.Configure();
        }



        public void Info(string message)
        {
            this.logger.Info(message);
        }

        public void Warn(string message)
        {
            this.logger.Warn(message);
        }

        public void Debug(string message)
        {
            this.logger.Debug(message);
        }

        public void Error(string message)
        {
            this.logger.Error(message);
        }

        public void Error(Exception ex)
        {
            this.logger.Error(ex.Message, ex);
        }

        public void Fatal(string message)
        {
            this.logger.Fatal(message);
        }

        public void Fatal(Exception ex)
        {
            this.logger.Fatal(ex.Message, ex);
        }
    }
}

