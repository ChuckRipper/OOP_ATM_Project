using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Enums;
//using OOP_ATM_Project.Logging;
//using OOP_ATM_Project.Enums.Logging;
using OOP_ATM_Project.Interfaces.Logging;

namespace OOP_ATM_Project.Logging
{
    public class LogColor : ILogColorizer
    {
        //#region Classes

        //#endregion

        #region Fields 
        private static ConsoleColor DefaultColor;
        private static Dictionary<LogLevel.Level, ConsoleColor> _logLevelColorMap;
            /*= new Dictionary<LogLevel.Level, ConsoleColor>
        {
            { LogLevel.Level.Debug, ConsoleColor.Blue },
            { LogLevel.Level.Info, ConsoleColor.Green },
            { LogLevel.Level.Warn, ConsoleColor.Yellow },
            { LogLevel.Level.Error, ConsoleColor.Red },
            { LogLevel.Level.Fatal, ConsoleColor.DarkRed },
        };*/

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LogColor()
        {
            DefaultColor = ConsoleColor.White;
            _logLevelColorMap = new Dictionary<LogLevel.Level, ConsoleColor>
            {
                { LogLevel.Level.Debug, ConsoleColor.Blue },
                { LogLevel.Level.Info, ConsoleColor.Green },
                { LogLevel.Level.Warn, ConsoleColor.Yellow },
                { LogLevel.Level.Error, ConsoleColor.Red },
                { LogLevel.Level.Fatal, ConsoleColor.DarkRed },
            };
        }
        #endregion

        //#region Destructors

        //#endregion

        #region Operators

        #endregion

        #region Methods
        //public string FormatLog(LogLevel.Level level, string message)
        //{
        //    return string.Format("{0} {1} {2}", DateTime.Now, level, message);
        //}

        //public string FormatException(LogLevel.Level level, Exception exception)
        //{
        //    return string.Format("{0} {1} {2}", DateTime.Now, level, exception.Message);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logEntry"></param>
        /// <returns></returns>
        public string Format(LogEntry logEntry)
        {
            // Get color for log level
            var color = DefaultColor;
            if (_logLevelColorMap.ContainsKey(logEntry.Level))
            {
                color = _logLevelColorMap[logEntry.Level];
            }

            // Set console color and write log
            Console.ForegroundColor = color;
            return logEntry.FormatLogEntry();
        }

        public void ResetColor()
        {
            Console.ResetColor();
        }

        //public string Format(LogEntry entry)
        //{
        //    string timestamp = entry.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //    string level = $"[{entry.Level}]".PadRight(8);
        //    string source = string.IsNullOrEmpty(entry.Source) ? string.Empty : $"{entry.Source} - ";
        //    string message = entry.Message;

        //    string formattedLog = $"{timestamp} {level} {source}{message}";

        //    return formattedLog;
        //}

        #endregion
    }
}
