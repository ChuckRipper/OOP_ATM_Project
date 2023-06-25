using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Enums;
using OOP_ATM_Project.Interfaces.DataRandomizing;
using OOP_ATM_Project.Data.DataRandomizingStructures;

namespace OOP_ATM_Project.Models
{
    public class ATM
    {
        #region Fields 
        private readonly IATMDataRandomize _atmDataRandomizer;
        #endregion

        #region Properties
        public string ATMID { get; set; }
        //public ATMStatus.Status ATMStatus { get; set; }
        public Dictionary<int, int> CashCassettes { get; set; }
        #endregion

        #region Constructors
        public ATM()
        {
            ATMID = _atmDataRandomizer.GenerateRandomATMID();
            //ATMStatus = ATMStatus.Status.Active;
            CashCassettes = _atmDataRandomizer.CashCassettes;
        }

        public ATM(IATMDataRandomize atmDataRandomizer)
        {
            _atmDataRandomizer = atmDataRandomizer;
            ATMID = _atmDataRandomizer.GenerateRandomATMID();
            //ATMStatus = (ATMStatus.Status)Enum.Parse(typeof(ATMStatus.Status), atmDataRandomizer.GenerateRandomATMStatus());
            CashCassettes = _atmDataRandomizer.CashCassettes;
        }
        #endregion

        #region Methods

        #endregion
    }
}
