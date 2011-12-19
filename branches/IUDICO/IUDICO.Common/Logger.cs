using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common
{
    public class Logger
    {
        private static volatile Logger _instance;
        private static object syncRoot = new Object();
        private log4net.ILog log;

        protected Logger() { }

        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new Logger();
                        }
                    }
                }

                return _instance;
            }
        }

        public void Info(Object cls, string msg)
        {
            log4net.GlobalContext.Properties["Hostname"] = Dns.GetHostName();
            this.log = log4net.LogManager.GetLogger(cls.ToString());
            this.log.Info(msg);
        }

        public void Error(Object cls, string msg)
        {
            log4net.GlobalContext.Properties["Hostname"] = Dns.GetHostName();
            this.log = log4net.LogManager.GetLogger(cls.ToString());
            this.log.Error(msg);
        }
    }
}