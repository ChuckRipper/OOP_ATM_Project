using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ATM_Project.Enums
{
    public class CardStatus
    {
        public enum Status
        {
            Active, // Karta gotowa do użycia oraz działa prawidłowo
            Blocked, // Karta zablokowana
            Expired, // Karta wygasła
            Closed // Karta została zamknięta
        }
    }
}
