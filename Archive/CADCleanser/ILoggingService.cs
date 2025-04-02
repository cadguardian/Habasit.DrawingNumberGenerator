﻿namespace CADCleanser.Services
{
    public interface ILoggingService
    {
        void LogInfo(string message);

        void LogError(string message, Exception ex);

        void FinalizeLog();
    }
}