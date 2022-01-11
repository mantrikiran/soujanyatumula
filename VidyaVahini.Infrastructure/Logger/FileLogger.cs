using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;
using System.Xml;
using VidyaVahini.Infrastructure.Contracts;

namespace VidyaVahini.Infrastructure.Logger
{
    public class FileLogger : ILogger
    {
        private readonly ILog Logger;
        private static readonly string _log4netConfigFilePath = @"log4net.config";

        public FileLogger()
        {
            Logger = LogManager.GetLogger(typeof(FileLogger));
            SetLog4NetConfiguration();
        }

        private static void SetLog4NetConfiguration()
        {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead(_log4netConfigFilePath));

            var repo = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
        }

        public void LogError(string message)
        {
            Logger.Error(message);
        }

        public void LogError(string message, System.Exception ex)
        {
            message = (message == "") ? (ex != null ? ex.Message : string.Empty) : message;
            Logger.Error(message, ex);
        }

        public void LogInformation(string message)
        {
            Logger.Info(message);
        }

        public void LogDebug(string message)
        {
            Logger.Debug(message);
        }

        public void LogWarning(string message)
        {
            throw new NotImplementedException();
        }

        public void LogException(System.Exception ex)
        {
            Logger.Error(string.Empty, ex);
        }
    }
}
