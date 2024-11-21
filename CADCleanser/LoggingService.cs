using System;
using System.IO;

namespace CADCleanser.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly StreamWriter _logWriter;
        private readonly string _logFilePath;

        public LoggingService()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            _logFilePath = Path.Combine(desktopPath, $"SolidWorks_Block_Import_Log_{DateTime.Now:yyyyMMddHHmmss}.txt");
            _logWriter = new StreamWriter(_logFilePath);
            LogInfo("Log started.");
        }

        public void LogInfo(string message)
        {
            _logWriter.WriteLine($"{DateTime.Now:G} [INFO]: {message}");
            _logWriter.Flush();
        }

        public void LogError(string message, Exception ex)
        {
            _logWriter.WriteLine($"{DateTime.Now:G} [ERROR]: {message}");
            _logWriter.WriteLine(ex.ToString());
            _logWriter.Flush();
        }

        public void FinalizeLog()
        {
            LogInfo("Log ended.");
            _logWriter.Close();
        }
    }
}