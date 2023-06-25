using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Interfaces.DataRandomizing;

namespace OOP_ATM_Project.Models
{
    public class Account
    {
        #region Fields 
        private IAccountDataRandomize _accountDataRandomizer;
        #endregion

        #region Properties
        public string AccountNumber { get; set; }
        public decimal AccountBalance { get; set; }
        public string AccountType { get; set; }
        public DateTime OpeningDate { get; set; }
        #endregion
        
        #region Constructors
        public Account()
        {
            this.AccountNumber = _accountDataRandomizer.GenerateRandomAccountNumber();
            this.AccountBalance = _accountDataRandomizer.GenerateRandomAccountBalance();
            this.AccountType = _accountDataRandomizer.GenerateRandomAccountType();
            this.OpeningDate = _accountDataRandomizer.GenerateRandomOpeningDate();
        }

        public Account(IAccountDataRandomize accountDataRandomizer)
        {
            this.AccountNumber = accountDataRandomizer.GenerateRandomAccountNumber();
            this.AccountBalance = accountDataRandomizer.GenerateRandomAccountBalance();
            this.AccountType = accountDataRandomizer.GenerateRandomAccountType();
            this.OpeningDate = accountDataRandomizer.GenerateRandomOpeningDate();
        }
        #endregion

        #region Methods

        #endregion
    }
}
