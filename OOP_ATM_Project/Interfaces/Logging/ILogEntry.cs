using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Enums;

namespace OOP_ATM_Project.Interfaces.Logging
{
    public interface ILogEntry
    {
        LogLevel.Level Level { get; }
        string Message { get; }
        Exception Exception { get; }
        DateTime TimeStamp { get; }
        string Source { get; } // Nazwa metody lub klasy, która wywołała logowanie
        string FormatLogEntry();
    }
}
