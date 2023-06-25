using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ATM_Project.Interfaces.Models
{
    public interface IAccountModelService
    {
        decimal GetBalance();

        void Withdraw(int amount);

        void Deposit(int amount);
    }
}
