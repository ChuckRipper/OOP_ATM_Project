using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ATM_Project.Interfaces.Services
{
    public interface IMoneyDispenser
    {
        bool CanDispenseMoney(int amount);
        void DispenseMoney(int amount);
        List<int> GetAvailableDenominations();
    }
}
