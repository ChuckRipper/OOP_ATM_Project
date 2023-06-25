using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ATM_Project.Enums
{
    public class AccountStatus
    {
        public enum Status
        {
            Active,       // Konto jest aktywne i gotowe do transakcji
            Inactive,     // Konto jest nieaktywne, transakcje nie są możliwe
            Suspended,    // Konto jest zawieszone z powodu podejrzeń o działania niezgodne z prawem
            Closed        // Konto zostało zamknięte, żadne dalsze transakcje nie są możliwe
        }
    }
}
