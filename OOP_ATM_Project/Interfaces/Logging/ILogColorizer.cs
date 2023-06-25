using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Enums;
using OOP_ATM_Project.Logging;

namespace OOP_ATM_Project.Interfaces.Logging
{
    /// <summary>
    /// Zgodnie z Single Responsibility Principle (SOLID), ten interfejs powinien być odpowiedzialny TYLKO za formatowanie logów.
    /// </summary>
    public interface ILogColorizer
    {
        //string FormatLog(LogLevel level, string message);
        //string FormatException(LogLevel level, Exception exception);
        string Format(LogEntry logEntry);
        void ResetColor();
    }
}
