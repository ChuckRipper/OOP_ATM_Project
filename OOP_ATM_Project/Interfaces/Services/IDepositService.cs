using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ATM_Project.Interfaces.Services
{
    public interface IDepositService
    {
        void DepositMoney(int amount);
        void ProcessCustomerDeposit(int denomination, int count);
        void ResetCustomerDeposit();
        bool AreBanknotesCompatibleWithCassettes(int cashAmount);
        bool CanDepositMoney(int amount);
    }
}
