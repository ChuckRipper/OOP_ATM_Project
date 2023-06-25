using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Data.DataRandomizingStructures;
using OOP_ATM_Project.Enums;
using OOP_ATM_Project.Interfaces.DataRandomizing;
using OOP_ATM_Project.Interfaces.Logging;
//using OOP_ATM_Project.Interfaces.NotImplemented;
using OOP_ATM_Project.Interfaces.Services;
using OOP_ATM_Project.Logging;

namespace OOP_ATM_Project.Services
{
    public class MoneyDispenser : IMoneyDispenser
    {
        #region Fields 
        private readonly IAccountDataRandomize _account; // Jest to alternatywa dla dziecziczenia, w tym przypadku nie musimy dziedziczyć po klasie BankAccount, wystarczy że w konstruktorze przekażemy obiekt klasy BankAccount. To pole jest tylko do odczytu, więc nie można go zmienić po przypisaniu wartości w konstruktorze.
        private IAuditLogger _auditLogger;
        private IATMDataRandomize _atmDataRandomizer; // Randomizowane dane dotyczące bankomatu, np. stan kaset z bankomatami

        private Dictionary<int, int> _cashCassettes;
        #endregion

        #region Properties
        public IAuditLogger AuditLogger => _auditLogger;
        public Dictionary<int, int> CashCassettes => _cashCassettes;
        #endregion

        #region Constructors
        public MoneyDispenser()
        {
            _atmDataRandomizer = new ATMDataRandomizeStructure();
            _account = new AccountDataRandomizeStructure();
        }

        public MoneyDispenser(IAuditLogger auditLogger)
        {
                _cashCassettes = new Dictionary<int, int>()
            {
                { 500, 0 },
                { 200, 0 },
                { 100, 0 },
                { 50, 0 },
                { 20, 0 },
                { 10, 0 }
            };
                _auditLogger = auditLogger;
        }

        public MoneyDispenser(IAccountDataRandomize account, IAuditLogger auditLogger) : this(auditLogger)
        {
            _cashCassettes = new Dictionary<int, int>()
            {
                { 500, 0 },
                { 200, 0 },
                { 100, 0 },
                { 50, 0 },
                { 20, 0 },
                { 10, 0 }
            };
            _account = account;
            this._auditLogger = auditLogger;
        }

        public MoneyDispenser(IAccountDataRandomize account, IAuditLogger auditLogger, IATMDataRandomize atmDataRandomizer, Dictionary<int, int> cashCassettes) : this(account, auditLogger)
        {
            this._atmDataRandomizer = atmDataRandomizer;
            this._cashCassettes = cashCassettes;
        }
        #endregion

        #region Methods

        public bool CanDispenseMoney(int amount)
        {
            var availableMoney = CashCassettes.Sum(cassette => cassette.Key * cassette.Value);
            var canDispense = availableMoney >= amount;
            AuditLogger.Log(LogLevel.Level.Info, $"Sprawdzanie możliwości wypłaty: {canDispense}");
            return canDispense;
        }

        public void DispenseMoney(int amount)
        {
            if (!CanDispenseMoney(amount))
            {
                AuditLogger.Log(LogLevel.Level.Warn, "Próba wypłaty większej kwoty niż dostępna. Operacja przerwana.");
                //return false;
            }

            foreach (var denomination in CashCassettes.Keys.OrderByDescending(x => x))
            {
                var notesRequired = (int)( amount / denomination );
                var notesToDispense = Math.Min(notesRequired, CashCassettes[denomination]);
                CashCassettes[denomination] -= notesToDispense;
                amount -= notesToDispense * denomination;

                if (notesToDispense > 0)
                {
                    AuditLogger.Log(LogLevel.Level.Info, $"Wydawanie banknotów: Nominał {denomination} PLN - liczba banknotów: {notesToDispense}");
                }
            }

            //return amount == 0;
        }

        public List<int> GetAvailableDenominations()
        {
            var denominations = CashCassettes.Where(cassette => cassette.Value > 0).Select(cassette => cassette.Key).ToList();
            AuditLogger.Log(LogLevel.Level.Info, $"Dostępne nominały: {string.Join(", ", denominations)}");
            return denominations;
        }
        #endregion
    }
}
