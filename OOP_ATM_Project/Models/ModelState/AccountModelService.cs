using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Interfaces.Logging;
using OOP_ATM_Project.Interfaces.Models;
using OOP_ATM_Project.Logging;
using OOP_ATM_Project.Data.DataRandomizingStructures;
using OOP_ATM_Project.Interfaces.DataRandomizing;

namespace OOP_ATM_Project.Models.ModelState
{
    public class AccountModelService : IAccountModelService
    {
        #region Fields 
        private decimal _accountBalance = 200.00m;
        private IAuditLogger _auditLogger;
        private IAccountDataRandomize _accountDataRandomize;
        #endregion

        #region Properties
        public IAuditLogger AuditLogger => _auditLogger;
        #endregion

        #region Constructors
        public AccountModelService()
        {
            _accountDataRandomize = new AccountDataRandomizeStructure();
            _accountBalance = 0.00m;
            _accountDataRandomize.RandomizeAccountData();
        }
        
        public AccountModelService(decimal accountBalance)
        {
            _accountBalance = accountBalance;
        }
        #endregion

        #region Methods
        public decimal GetBalance()
        {
            return _accountBalance;
        }

        public void Deposit(int amount)
        {
            _accountBalance += amount;
        }

        public void Withdraw(int amount)
        {
            if (_accountBalance >= amount)
            {
                _accountBalance -= amount;
            }
            else
            {
                throw new InvalidOperationException("Insufficient funds");
            }
        }

        #endregion
    }
}
