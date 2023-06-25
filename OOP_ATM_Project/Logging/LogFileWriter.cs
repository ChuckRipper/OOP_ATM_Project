using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using OOP_ATM_Project.Enums;
using OOP_ATM_Project.Interfaces.Logging;

namespace OOP_ATM_Project.Logging
{
    public class LogFileWriter : IAuditLogger
    {
        //#region Classes

        //#endregion

        #region Fields 
        private string _filePath;
        private ILogColorizer _logColorizer;
        private IAuditLogger _auditLogger;
        #endregion

        #region Properties
        public IAuditLogger AuditLogger => _auditLogger;
        #endregion

        #region Constructors
        public LogFileWriter(string logDirectory, ILogColorizer logColorizer)
        {
            // Formatuj nazwę pliku logów z uwzględnieniem daty i godziny
            string logFileName = $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss-fff}_AuditLog.log";
            _filePath = Path.Combine(logDirectory, logFileName);

            _logColorizer = logColorizer;
        }
        #endregion

        #region Methods
        public void Log(LogLevel.Level level, string message)
        {
            LogEntry entry = new LogEntry(level, message);
            //{
            //    Message = message,
            //    Level = level,
            //    TimeStamp = DateTime.Now,
            //};

            string formattedMessage = _logColorizer.Format(entry);

            File.AppendAllText(_filePath, formattedMessage + Environment.NewLine);
        }

        public void LogException(LogLevel.Level level, Exception exception)
        {
            Log(level, exception.ToString());
        }
        #endregion
    }
}
