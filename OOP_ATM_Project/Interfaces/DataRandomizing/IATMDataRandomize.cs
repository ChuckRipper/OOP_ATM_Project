using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ATM_Project.Interfaces.DataRandomizing
{
    /// <summary>
    /// Randomizowanie danych bankomatu, tj. 
    /// </summary>
    public interface IATMDataRandomize
    {
        string GenerateRandomATMID();
        string GenerateRandomATMStatus();
        void RandomizeATMData();
        void RandomizeCashCassettes();
        void RandomizeCashCassettes(Dictionary<int, int> cashCassettes);
        void RandomizeCashCassettes(Dictionary<int, int> cashCassettes, Dictionary<int, int> cashCassettesLimits);
        Dictionary<int, int> CashCassettes { get; }
    }
}
