using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Interfaces.DataRandomizing;
using OOP_ATM_Project.Interfaces.Logging;

namespace OOP_ATM_Project.Data.DataRandomizingStructures
{
    public class AccountDataRandomizeStructure : IAccountDataRandomize
    {
        #region Fields 
        private string _accountNumber;
        private decimal _accountBalance;
        private string _accountType;
        private DateTime _openingDate;
        private Random _random;
        private IAuditLogger _auditLogger;

        private static AccountDataRandomizeStructure _instance; // Wzorzec Singleton
        #endregion

        #region Properties
        public string AccountNumber => _accountNumber;
        public decimal AccountBalance => _accountBalance;
        public string AccountType => _accountType;
        public DateTime OpeningDate => _openingDate;
        public IAuditLogger AuditLogger => _auditLogger;
        public static AccountDataRandomizeStructure Instance // Wzorzec Singleton
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AccountDataRandomizeStructure();
                }
                return _instance;
            }
        }
        #endregion

        #region Constructors
        public AccountDataRandomizeStructure()
        {
            _accountNumber = string.Empty;
            _accountBalance = 0m;
            _accountType = string.Empty;
            _openingDate = DateTime.Now;
            _random = new Random();

            RandomizeAccountData();
        }
        #endregion

        #region Methods
        public string GenerateRandomAccountNumber()
        {
            // Wygeneruj losowy numer konta
            return "ACCT" + _random.Next(1000, 9999);
        }

        public decimal GenerateRandomAccountBalance()
        {
            // Wygeneruj losową wartość salda konta
            return _random.Next(100, 10000);
        }

        public string GenerateRandomAccountType()
        {
            // Wygeneruj losowy typ konta
            string[] accountTypes = { "Checking", "Savings", "Investment" };
            return accountTypes[_random.Next(accountTypes.Length)];
        }

        public DateTime GenerateRandomOpeningDate()
        {
            // Wygeneruj losową datę otwarcia konta
            DateTime startDate = new DateTime(2000, 1, 1);
            int range = ( DateTime.Today - startDate ).Days;
            return startDate.AddDays(_random.Next(range));
        }

        public void RandomizeAccountData()
        {
            _accountNumber = GenerateRandomAccountNumber();
            _accountBalance = GenerateRandomAccountBalance();
            _accountType = GenerateRandomAccountType();
            _openingDate = GenerateRandomOpeningDate();
        }
        #endregion
    }
}
