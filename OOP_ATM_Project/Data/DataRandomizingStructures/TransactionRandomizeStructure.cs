using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

using OOP_ATM_Project.Interfaces.DataRandomizing;
using OOP_ATM_Project.Interfaces.Logging;
using OOP_ATM_Project.Enums;
using OOP_ATM_Project.Logging;

namespace OOP_ATM_Project.Data.DataRandomizingStructures
{
    public class TransactionRandomizeStructure : ITransactionRandomize
    {
        #region Fields 
        private List<string> transactionTypes = new List<string> { "Wpłata", "Wypłata", "Przelew" };
        private Random _random = new Random();
        private AuditLogger _auditLogger;
        private readonly Transaction _transaction;

        private static TransactionRandomizeStructure _instance; // Singleton
        #endregion

        #region Properties
        public AuditLogger AuditLogger => _auditLogger;

        public static TransactionRandomizeStructure Instance // Singleton
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TransactionRandomizeStructure();
                }
                return _instance;
            }
        }
        #endregion

        #region Constructors
        public TransactionRandomizeStructure()
        {
            //this._transaction = transaction;
            RandomizeTransactionData();
        }
        
        public TransactionRandomizeStructure(Transaction transaction)
        {
            _transaction = transaction;
            RandomizeTransactionData();
        }
        #endregion

        #region Methods
        public string RandomizeTransactionType()
        {
            int index = _random.Next(transactionTypes.Count);
            return transactionTypes[index];
        }

        public decimal RandomizeTransactionAmount()
        {
            return _random.Next(10, 5000);
        }

        public DateTime RandomizeTransactionDate()
        {
            return DateTime.Now.AddDays(_random.Next(-365, 0));
        }

        public void RandomizeTransactionData()
        {
            RandomizeTransactionType();
            RandomizeTransactionAmount();
            RandomizeTransactionDate();
        }

        //public void RandomizeTransactionData()
        //{
        //    _transaction.Type = RandomizeTransactionType();
        //    _transaction.Amount = RandomizeTransactionAmount();
        //    _transaction.Date = RandomizeTransactionDate();
        //}
        #endregion
    }
}
