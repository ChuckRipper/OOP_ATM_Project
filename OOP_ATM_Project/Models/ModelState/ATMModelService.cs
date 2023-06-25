using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Interfaces.Models;
using OOP_ATM_Project.Enums;
using OOP_ATM_Project.Interfaces.DataRandomizing;
using OOP_ATM_Project.Interfaces.Logging;
using OOP_ATM_Project.Interfaces.Services;
using OOP_ATM_Project.Logging;

namespace OOP_ATM_Project.Models.ModelState
{
    public class ATMModelService : IATMModelService
    {
        #region Fields 
        private Dictionary<int, int> _cashCassettes; // Stan kaset z banknotami
        private readonly Dictionary<int, int> _cashCassettesLimits; // Limit pojemności kaset z banknotami
        private ATM _ATM;
        private ATMStatus.Status _atmStatus;
        private ATMFunctionality.Functionality _atmFunctionality = ATMFunctionality.Functionality.None; // domyślny stan bankomatu
        private IAuditLogger _auditLogger;
        #endregion

        #region Properties
        public IAuditLogger AuditLogger => _auditLogger;
        public Dictionary<int, int> CashCassettes => _cashCassettes;
        public Dictionary<int, int> CashCassettesLimits => _cashCassettesLimits;
        #endregion

        #region Constructors
        public ATMModelService()
        {
            _cashCassettes = new Dictionary<int, int>
            {
                { 10, 0 },
                { 20, 0 },
                { 50, 0 },
                { 100, 0 },
                { 200, 0 },
                { 500, 0 }
            };

            _cashCassettesLimits = new Dictionary<int, int>
            {
                { 10, 1000 },
                { 20, 1000 },
                { 50, 1000 },
                { 100, 1000 },
                { 200, 1000 },
                { 500, 1000 }
            };

            _atmStatus = ATMStatus.Status.NotOperational;
        }
        #endregion

        #region Methods
        public void DispenseCash(decimal amount)
        {
            // Metoda ta powinna symulować wypłatę gotówki. Możesz dodać tu odpowiednią logikę.
        }

        public ATMStatus.Status GetCurrentATMStatus()
        {
            return _atmStatus;
        }

        public Dictionary<int, int> GetCurrentCashCassettes()
        {
            return CashCassettes;
        }

        public bool IsATMWorking()
        {
            var isWorking = _atmStatus != ATMStatus.Status.NotOperational;
            //!(isWorking) ? AuditLogger.Log(LogLevel.Level.Error, $"Bankomat nie działa!") : AuditLogger.Log(LogLevel.Level.Info, $"Sprawdzanie czy bankomat działa: {isWorking}");

            if (!isWorking)
            {
                AuditLogger.Log(LogLevel.Level.Error, $"Bankomat nie działa!");
            }
            else
            {
                AuditLogger.Log(LogLevel.Level.Debug, $"Health Check: {isWorking}");
            }

            return isWorking;
        }

        public bool IsDepositPossible()
        {
            foreach (var cassette in CashCassettes)
            {
                if (cassette.Value < CashCassettesLimits[cassette.Key])
                {
                    return true;
                }
            }

            AuditLogger.Log(LogLevel.Level.Warn, "Wpłata niemożliwa - wszystkie kasety są pełne.");
            return false;
            //return IsATMWorking();
        }

        public bool IsWithdrawalPossible()
        {
            // Metoda ta powinna zwracać prawdę, jeśli wypłata jest możliwa. Możesz dodać tu odpowiednią logikę.
            return IsATMWorking();
        }

        public void ReturnCard()
        {
            // Metoda ta powinna symulować zwrot karty. Możesz dodać tu odpowiednią logikę.
        }

        public void SetATMFunctionality(ATMFunctionality.Functionality functionality)
        {
            _atmFunctionality = functionality;
            AuditLogger.Log(LogLevel.Level.Info, $"Wybrano funkcjonalność: {_atmFunctionality}");
        }

        public void UpdateCashCassettes(Dictionary<int, int> depositOrWithdrawal)
        {
            if (_atmFunctionality == ATMFunctionality.Functionality.Deposit)
            {
                foreach (var depositItem in depositOrWithdrawal)
                {
                    _cashCassettes[depositItem.Key] += depositItem.Value;
                    AuditLogger.Log(LogLevel.Level.Info, $"Aktualizacja kasety: Nominał {depositItem.Key} PLN - wpłacono {depositItem.Value} sztuk.");
                }
            }
            else if (_atmFunctionality == ATMFunctionality.Functionality.Withdrawal)
            {
                foreach (var withdrawalItem in depositOrWithdrawal)
                {
                    _cashCassettes[withdrawalItem.Key] -= withdrawalItem.Value;
                    AuditLogger.Log(LogLevel.Level.Info, $"Aktualizacja kasety: Nominał {withdrawalItem.Key} PLN - wypłacono {withdrawalItem.Value} sztuk.");
                }
            }

            foreach (var cassette in _cashCassettes)
            {
                AuditLogger.Log(LogLevel.Level.Info, $"Nominał {cassette.Key} PLN: {cassette.Value}/{_cashCassettesLimits[cassette.Key]}");
            }
        }
        #endregion

    }
}
