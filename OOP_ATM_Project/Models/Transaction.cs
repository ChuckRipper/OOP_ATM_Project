using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Interfaces.DataRandomizing;

namespace OOP_ATM_Project.Models
{
    public class Transaction
    {
        #region Fields 
        private ITransactionRandomize _transactionDataRandomizer;
        #endregion

        #region Properties
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        #endregion

        #region Constructors
        public Transaction()
        {
            Type = _transactionDataRandomizer.RandomizeTransactionType();
            Amount = _transactionDataRandomizer.RandomizeTransactionAmount();
            Date = _transactionDataRandomizer.RandomizeTransactionDate();
        }

        public Transaction(ITransactionRandomize transactionDataRandomizer)
        {
            Type = transactionDataRandomizer.RandomizeTransactionType();
            Amount = transactionDataRandomizer.RandomizeTransactionAmount();
            Date = transactionDataRandomizer.RandomizeTransactionDate();
        }
        #endregion

        #region Methods

        #endregion
    }
}
