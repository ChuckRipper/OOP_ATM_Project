using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ATM_Project.Interfaces.NotImplemented
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBankDataRandomize
    {
        //string GenerateRandomBankName(); // 0
        string GenerateRandomBankID();
        //string GenerateRandomBranchCode();
        string GenerateRandomSWIFTCode();
        void RandomizeBankData();
    }
}
