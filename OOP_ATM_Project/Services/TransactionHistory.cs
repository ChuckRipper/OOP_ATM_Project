using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Interfaces.Services;
using OOP_ATM_Project.Logging;
using OOP_ATM_Project.Models;

namespace OOP_ATM_Project.Services
{
    public class TransactionHistory : ITransactionHistory
    {
        #region Fields 
        private List<Transaction> _transactions = new List<Transaction>();
        private AuditLogger _auditLogger;
        #endregion

        #region Properties
        public AuditLogger AuditLogger => _auditLogger;
        #endregion

        #region Constructors
        public TransactionHistory()
        {
            this._transactions = new List<Transaction>();
        }
        #endregion

        #region Methods
        public void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public List<Transaction> GetLastTransactions(int count)
        {
            return _transactions.OrderByDescending(t => t.Date).Take(count).ToList();
        }
        #endregion
    }
}
