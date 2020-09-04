// <copyright file="LoggerUtil.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the LoggerUtil class used for logging.</summary>
namespace PublicAuction
{
    using log4net;

    /// <summary>Class for logger.</summary>
    public class LoggerUtil
    {
        /*static LoggerUtil()
        {
            log4net.Config.XmlConfigurator.Configure();
        }*/

        /// <summary>The log used in classes.</summary>
        private static readonly ILog Log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>Logs the information.</summary>
        /// <param name="message">The message.</param>
        public static void LogInfo(string message)
        {
            if (Log.IsInfoEnabled)
            {
                Log.Info(message);
            }
        }

        /// <summary>Logs the error.</summary>
        /// <param name="message">The message.</param>
        public static void LogError(string message)
        {
            if (Log.IsInfoEnabled)
            {
                Log.Error(message);
            }
        }

        /// <summary>Logs the warning.</summary>
        /// <param name="message">The message.</param>
        public static void LogWarning(string message)
        {
            if (Log.IsInfoEnabled)
            {
                Log.Warn(message);
            }
        }
    }
}