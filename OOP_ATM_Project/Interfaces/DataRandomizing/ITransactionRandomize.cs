using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ATM_Project.Interfaces.DataRandomizing
{
    public interface ITransactionRandomize
    {
        string RandomizeTransactionType();
        decimal RandomizeTransactionAmount();
        DateTime RandomizeTransactionDate();
        void RandomizeTransactionData();
    }
}
