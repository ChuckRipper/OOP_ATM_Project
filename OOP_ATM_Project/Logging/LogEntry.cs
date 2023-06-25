using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Enums;
using OOP_ATM_Project.Interfaces.Logging;

namespace OOP_ATM_Project.Logging
{
    public class LogEntry : ILogEntry
    {
        #region Fields 
        private LogLevel.Level _level;
        private string _message;
        private Exception _exception;
        private DateTime _timeStamp;
        private string _timeStampString;
        private string _source;
        private string _filePath;

        private Random _random = new Random();
        #endregion

        #region Properties
        public LogLevel.Level Level { get; private set; } // Poziom logowania
        public string? Message { get; private set; } // Wiadomość, która ma być zapisana w logu
        public Exception? Exception { get; private set; } // Wyjątek, który został zgłoszony
        public DateTime TimeStamp { get; private set; } // Czas logowania
        public string TimeStampString { get; private set; } // Czas logowania w formacie string

        public string? Source { get; private set; } // Nazwa metody lub klasy, która wywołała logowanie
        public string? FilePath { get; private set; } // Ścieżka do pliku, w którym wywołano logowanie
        #endregion

        #region Constructors
        public LogEntry()
        {
            Level = (LogLevel.Level)_random.Next(Enum.GetValues(typeof(LogLevel.Level)).Length); // Wylosowanie poziomu logowania
            Message = String.Empty;
            Exception = null;
            TimeStamp = DateTime.Now;
            TimeStampString = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss-fff");
            Source = String.Empty;
            FilePath = String.Empty;
        }
        
        public LogEntry(LogLevel.Level level, string message, string source, Exception exception = null)
        {
            Level = level;
            Message = message;
            Exception = exception;
            TimeStamp = DateTime.Now;
            TimeStampString = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss-fff");
            Source = source;
        }

        public LogEntry(LogLevel.Level level, string message)
        {
            Level = level;
            Message = message;
            Exception = null;
            TimeStamp = DateTime.Now;
            TimeStampString = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss-fff");
            Source = String.Empty;
        }

        public LogEntry(LogLevel.Level level, string message, string source)
        {
            Level = level;
            Message = message;
            Exception = null;
            TimeStamp = DateTime.Now;
            TimeStampString = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss-fff");
            Source = source;
        }

        public LogEntry(LogLevel.Level level, string message, Exception exception)
        {
            Level = level;
            Message = message;
            Exception = exception;
            TimeStamp = DateTime.Now;
            TimeStampString = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss-fff");
            Source = String.Empty;
        }

        public LogEntry(LogLevel.Level level, string message, string source, Exception exception, DateTime timeStamp)
        {
            Level = level;
            Message = message;
            Exception = exception;
            TimeStamp = timeStamp;
            TimeStampString = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss-fff");
            Source = source;
        }

        public LogEntry(LogLevel.Level level, string message, string source, DateTime timeStamp)
        {
            Level = level;
            Message = message;
            Exception = null;
            TimeStamp = timeStamp;
            TimeStampString = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss-fff");
            Source = source;
        }

        public LogEntry(LogLevel.Level level, string message, DateTime timeStamp)
        {
            Level = level;
            Message = message;
            Exception = null;
            TimeStamp = timeStamp;
            TimeStampString = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss-fff");
            Source = String.Empty;
        }

        public LogEntry(LogLevel.Level level, string message, Exception exception, DateTime timeStamp)
        {
            Level = level;
            Message = message;
            Exception = exception;
            TimeStamp = timeStamp;
            TimeStampString = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss-fff");
            Source = String.Empty;
        }

        public LogEntry(LogLevel.Level level, string message, Exception exception, string source)
        {
            Level = level;
            Message = message;
            Exception = exception;
            TimeStamp = DateTime.Now;
            TimeStampString = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss-fff");
            Source = source;
        }

        public LogEntry(LogLevel.Level level, string message, string source, string filepath)
        {
            Level = level;
            Message = message;
            Exception = null;
            TimeStamp = DateTime.Now;
            TimeStampString = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss-fff");
            Source = source;
            FilePath = filepath;
        }

        public LogEntry(LogLevel.Level level, string message, string source, Exception exception, string filepath)
        {
            Level = level;
            Message = message;
            Exception = exception;
            TimeStamp = DateTime.Now;
            TimeStampString = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss-fff");
            Source = source;
            FilePath = filepath;
        }
        #endregion

        #region Methods
        public string FormatLogEntry() // Ta metoda jest niejawnie wywoływana - pośrednio przez konstruktory Log w klasie AuditLogger
        {
            var logEntryBuilder = new StringBuilder();

            logEntryBuilder.AppendLine($"Time: {TimeStamp}");
            logEntryBuilder.AppendLine($"Level: [{Level.ToString().ToUpper()}]".PadRight(8)); // Poziom logowania ma być pisany dużymi literami w nawiasach kwadratowych
            logEntryBuilder.AppendLine($"Source: {Source}"); // Nazwa metody lub klasy, która wywołała logowanie
            logEntryBuilder.AppendLine($"Message: {Message}");

            if (Exception != null)
            {
                logEntryBuilder.AppendLine($"Exception: {Exception}");
            }

            return logEntryBuilder.ToString();
        }
        #endregion
    }
}
