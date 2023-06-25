using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ATM_Project.Interfaces.DataRandomizing
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccountDataRandomize
    {
        string GenerateRandomAccountNumber();
        decimal GenerateRandomAccountBalance();
        string GenerateRandomAccountType();
        DateTime GenerateRandomOpeningDate();
        void RandomizeAccountData();
    }
}
