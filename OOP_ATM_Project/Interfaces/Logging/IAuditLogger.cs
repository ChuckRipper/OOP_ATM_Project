using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Enums;

namespace OOP_ATM_Project.Interfaces.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuditLogger
    {
        void Log(LogLevel.Level level, string message);
        void LogException(LogLevel.Level level, Exception exception);
        //void Log(LogLevel.Level level, string message, string source = "");
        //void Log(LogLevel.Level level, string message, string source = "", string filePath = "");
        //void Log(LogLevel.Level level, string message, Exception exception);
        //void Log(LogLevel.Level level, string message, Exception exception, string source = "");
        //void Log(LogLevel.Level level, string message, Exception exception, string source = "", string filePath = "");

        //void Log(LogLevel.Level level, string message);
        //void LogTransaction(LogLevel.Level level, string transactionDetails);
        //void LogError(LogLevel.Level level, string errorMessage);
        //void LogEvent(LogLevel.Level level, string eventMessage);
        //void LogDebug(string debugMessage);
        //void LogWarning(string warningMessage);
        //void LogException(LogLevel.Level level, Exception exception);
        //void LogCritical(LogLevel.Level level, string criticalMessage);

        //void Log(LogLevel.Level level, string message);
        //void LogTransaction(string transactionDetails);

        //void LogError(string errorMessage);
        //void LogEvent(string eventMessage);
    }
}
