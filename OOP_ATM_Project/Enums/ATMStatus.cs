using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ATM_Project.Enums
{
    public class ATMStatus
    {
        public enum Status
        {
            Active, // ATM gotowy do użycia oraz działa prawidłowo
            Inactive, // ATM wyłączony
            NotOperational // ATM gotowy do użycia, ale nie działa prawidłowo
        }
    }
}
