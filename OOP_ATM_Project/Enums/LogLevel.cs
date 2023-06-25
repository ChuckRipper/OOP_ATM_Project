using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ATM_Project.Enums
{
    /// <summary>
    /// Poziomy logowania: Debug, Info, Warn, Error, Fatal
    /// </summary>
    public class LogLevel
    {
        public enum Level
        {
            Debug, // Debug - informacje pomocnicze, które nie są potrzebne w normalnym działaniu aplikacji
            Info, // Info - informacje o normalnym działaniu aplikacji
            Warn, // Warn - ostrzeżenia o nieprawidłowym działaniu aplikacji
            Error, // Error - błędy, które nie powodują przerwania działania aplikacji
            Fatal // Fatal - błędy, które powodują przerwanie działania aplikacji
        }
    }
}
