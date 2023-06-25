using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ATM_Project.Interfaces.NotImplemented
{
    public interface ITransaction
    {
        void ProcessTransaction(decimal amount, IBankAccount bankAccount);
    }
}
