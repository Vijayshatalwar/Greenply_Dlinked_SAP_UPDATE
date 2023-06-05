using log4net;

namespace GSPPL.common1.Helper
{
    public static class LogHelper
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void InitLog()
        {
            string batchID = DateTime.Now.ToString("dd-MMM-yyy hh-mm-ss tt");
            log4net.GlobalContext.Properties["LogName"] = batchID;
            log4net.Config.XmlConfigurator.Configure();

            //if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Log4Net-ConfigFile"]) && !String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["Log4Net-ConfigFile"]))
            //{
            //    log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(ConfigurationManager.AppSettings["Log4Net-ConfigFile"].Trim())); 
            //}

        }

        public static void LogError(Exception ex)
        {
            log.Error("System Error :" + ex.Message, ex);
        }

        public static void LogError(string message)
        {
            log.Error("Custom Error :" + message);
        }

        public static void LogError(Exception ex, string message)
        {
            LogError(message);
            LogError(ex);
        }

        public static void LogSuccess(string message)
        {
            log.Info(message);
        }

        public static void LogInfo(string message)
        {
            log.Info(message);
        }

        public static void LogDebug(string message)
        {
            log.Debug(message);
        }

        public static void LogWarning(string message)
        {
            log.Warn(message);
        }
        public static void LogMessage(string message)
        {
            log.Info(message);
        }

    }
}
