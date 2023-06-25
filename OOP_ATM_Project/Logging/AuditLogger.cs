using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

using OOP_ATM_Project.Enums;
using OOP_ATM_Project.Interfaces.Logging;
using System.Runtime.Serialization;

namespace OOP_ATM_Project.Logging
{
    public sealed class AuditLogger : IAuditLogger
    {
        //#region Classes

        //#endregion

        #region Fields 
        private LogColor _logColorizer;

        private static AuditLogger? _instance; // Wzorzec Singleton - tylko jedna instancja klasy
        #endregion

        #region Properties
        public LogColor LogColorizer => _logColorizer;

        public static AuditLogger? Instance // Wzorzec Singleton; Tzw. Leniwa inicjalizacja - instancja klasy tworzona jest dopiero w momencie pierwszego odwołania się do właściwości
        {
            get
            {
                if (_instance == null)
                {
                    //_instance = new AuditLogger(logColorizer: _logColorizer());
                    //_instance = new AuditLogger(new LogColor());
                    _instance = new AuditLogger();
                }
                return _instance;
            }
        }

        //public static AuditLogger Instance2 // Inicjalizacja wymuszona - instancja klasy tworzona jest w momencie wywołania właściwości
        //{
        //    get
        //    {
        //        if (_instance == null)
        //        {
        //            _instance = new AuditLogger();
        //        }
        //        return _instance;
        //    }
        //}
        #endregion

        #region Constructors
        public AuditLogger()
        {
            this._logColorizer = new LogColor();
        }
        
        public AuditLogger(LogColor logColorizer) // Kontruktor prywatny, aby zapobiec tworzeniu nowych instancji klasy spoza klasy
        {
            this._logColorizer = logColorizer;
        }
        #endregion

        #region Methods
        //public static void Initialize(LogColor logColorizer)
        //{
        //    if (_instance != null)
        //    {
        //        throw new Exception("Singleton already initialized. AuditLogger.Initialize() should only be called once.");
        //    }

        //    _instance = new AuditLogger(logColorizer);
        //}

        private string GetSourceInfo()
        {
            var currentMethod = new StackTrace().GetFrame(2).GetMethod(); // 2 - metoda wywołująca metodę Log()
            var methodName = currentMethod.Name; // Nazwa metody wywołującej metodę Log()
            var className = currentMethod.ReflectedType.Name; // Nazwa klasy, w której zdefiniowana jest metoda wywołująca metodę Log()
            var sourceInfo = $"{className}.{methodName}"; // Nazwa klasy i metody wywołującej metodę Log()

            return sourceInfo;
        }

        public void Log(LogLevel.Level level,
            string message)
        {
            var logEntry = new LogEntry(level: level, message: message);
            //{
            //    Level = level,
            //    Message = message,
            //    Timestamp = DateTime.Now,
            //    Source = GetSourceInfo()
            //};

            Console.WriteLine(_logColorizer.Format(logEntry));
        }

        public void Log(LogLevel.Level level,
            string message,
            [CallerMemberName] string source = "")
        {
            var logEntry = new LogEntry(level: level, message: message, source: GetSourceInfo());
            //{
            //    Message = message,
            //    Level = level,
            //    TimeStamp = DateTime.Now,
            //    Source = source
            //};

            string formattedLog = _logColorizer.Format(logEntry);
            Console.WriteLine(formattedLog);
        }

        public void Log(LogLevel.Level level,
            string message,
            Exception exception,
            [CallerMemberName] string source = "")
        {
            var logEntry = new LogEntry(level: level, message: message, source: GetSourceInfo(), exception);
            //{
            //    Message = message,
            //    Level = level,
            //    TimeStamp = DateTime.Now,
            //    Source = source,
            //    Exception = exception
            //};

            string formattedLog = _logColorizer.Format(logEntry);
            Console.WriteLine(formattedLog);
        }

        public void Log(LogLevel.Level level,
            string message,
            [CallerMemberName] string source = "",
            [CallerFilePath] string filePath = "")
        {
            var logEntry = new LogEntry(level: level, message: message, source: GetSourceInfo(), filePath);
            //string sourceFileName = Path.GetFileNameWithoutExtension(sourceFilePath);
            //string logMessage = $"[{logLevel}] {sourceFileName}.{memberName} - {message}";

            //ConsoleColor color = _logColorizer.Colorize(logLevel);
            //LogEntry logEntry = new LogEntry(DateTime.Now, logMessage, logLevel, color);

            //_logFileWriter.Write(_logFile, logEntry);

            string formattedLog = _logColorizer.Format(logEntry);
            Console.WriteLine(formattedLog);
        }

        public void Log(LogLevel.Level level,
            string message,
            Exception exception,
            [CallerMemberName] string source = "",
            [CallerFilePath] string filePath = "")
        {
            var logEntry = new LogEntry(level: level, message: message, source: GetSourceInfo(), exception: exception, filepath: filePath);
            //string sourceFileName = Path.GetFileNameWithoutExtension(sourceFilePath);
            //string logMessage = $"[{logLevel}] {sourceFileName}.{memberName} - {message}";

            //ConsoleColor color = _logColorizer.Colorize(logLevel);
            //LogEntry logEntry = new LogEntry(DateTime.Now, logMessage, logLevel, color);

            //_logFileWriter.Write(_logFile, logEntry);

            string formattedLog = _logColorizer.Format(logEntry);
            Console.WriteLine(formattedLog);
        }

        //public void Log(LogLevel.Level level,
        //    string message,
        //    [CallerMemberName] string source = "",
        //    [CallerFilePath] string filePath = "",
        //    [CallerLineNumber] int lineNumber = 0)
        //{
        //    var logEntry = new LogEntry(LogLevel.Level.Info, message, source, filePath, lineNumber);
        //    //{
        //    //    Message = message,
        //    //    Level = level,
        //    //    TimeStamp = DateTime.Now,
        //    //    Source = source,
        //    //    FilePath = filePath,
        //    //    LineNumber = lineNumber
        //    //};

        //    string formattedLog = _logColorizer.Format(logEntry);
        //    Console.WriteLine(formattedLog);
        //}

        public void Log(LogLevel.Level level,
            string message,
            Exception exception)
        {
            var logEntry = new LogEntry(level: level, message: message, exception: exception);
            //{
            //    Message = message,
            //    Level = level,
            //    TimeStamp = DateTime.Now,
            //    Source = GetSourceInfo(),
            //    Exception = exception
            //};

            string formattedLog = _logColorizer.Format(logEntry);
            Console.WriteLine(formattedLog);
        }

        public void LogException(LogLevel.Level level,
            Exception exception)
        {
            Log(level, exception.ToString());
        }
        #endregion

        //#region Events

        //#endregion
    }
}
