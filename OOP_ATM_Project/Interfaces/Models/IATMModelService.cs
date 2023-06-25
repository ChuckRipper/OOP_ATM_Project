using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ATM_Project.Interfaces.Models
{
    public interface IATMModelService
    {
        void DispenseCash(decimal amount);

        bool IsATMWorking();

        bool IsDepositPossible();

        bool IsWithdrawalPossible();

        void ReturnCard();

        void UpdateCashCassettes(Dictionary<int, int> deposit);
        Enums.ATMStatus.Status GetCurrentATMStatus();
        Dictionary<int, int> GetCurrentCashCassettes();
        void SetATMFunctionality(Enums.ATMFunctionality.Functionality functionality);
    }
}
