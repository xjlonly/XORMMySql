using System.Configuration;

namespace XORM.Db
{
    public class LogConfig
    {
        private static string DBLogEnable = ConfigurationManager.AppSettings["DBLog_Enable"] ?? "false";
        private static string DBLogPath = ConfigurationManager.AppSettings["DBLog_Path"] ?? "";
        private static string DBLog_Throw = ConfigurationManager.AppSettings["DBLog_Throw"] ?? "false";

        public static bool Enable
        {
            get
            {
                return LogConfig.DBLogEnable.ToUpper() == "TRUE";
            }
        }

        public static string Path
        {
            get
            {
                return LogConfig.DBLogPath;
            }
        }

        public static bool ThrowException
        {
            get
            {
                return LogConfig.DBLog_Throw.ToUpper() == "TRUE";
            }
        }

        static LogConfig()
        {
        }
    }
}
