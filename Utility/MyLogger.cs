using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University.Utility
{
    public class MyLogger : ILogger
    {
        private static MyLogger instance; 
        private static Logger logger; 

        public MyLogger()
        {

        }

        public static MyLogger GetInstance()
        {
            return instance == null ? instance = new MyLogger() : instance;
        }

        private Logger GetLogger(string theLogger)
        {
            return MyLogger.logger == null ? MyLogger.logger = LogManager.GetLogger(theLogger) : MyLogger.logger;
        }

        public void Debug(string message, string arg = null)
        {
            if (arg == null)
                GetLogger("UniversityLoggerRule").Debug(message);
            else
                GetLogger("UniversityLoggerRule").Debug(message, arg);
        }

        public void Error(string message, string arg = null)
        {
            if (arg == null)
                GetLogger("UniversityLoggerRule").Error(message);
            else
                GetLogger("UniversityLoggerRule").Error(message, arg);
        }

        public void Info(string message, string arg = null)
        {
            if (arg == null)
                GetLogger("UniversityLoggerRule").Info(message);
            else
                GetLogger("UniversityLoggerRule").Info(message, arg);
        }

        public void Warning(string message, string arg = null)
        {
            if (arg == null)
                GetLogger("UniversityLoggerRule").Warn(message);
            else
                GetLogger("UniversityLoggerRule").Warn(message, arg);
        }
    }
}