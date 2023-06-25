using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ATM_Project.Interfaces.Services
{
    public interface ICashScanner
    {
        decimal ScanCash(); // Powinno zwrócić kwotę wprowadzoną do bankomatu
        void ReturnCash(decimal cashAmount); // Powinno zwrócić wprowadzoną kwotę
    }
}
