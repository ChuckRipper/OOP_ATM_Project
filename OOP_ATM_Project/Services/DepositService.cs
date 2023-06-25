using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Data.DataRandomizingStructures;
using OOP_ATM_Project.Enums;
using OOP_ATM_Project.Interfaces.DataRandomizing;
using OOP_ATM_Project.Interfaces.Logging;
using OOP_ATM_Project.Interfaces.Models;
//using OOP_ATM_Project.Interfaces.NotImplemented;
using OOP_ATM_Project.Interfaces.Services;
using OOP_ATM_Project.Logging;
using OOP_ATM_Project.Models.ModelState;

namespace OOP_ATM_Project.Services
{
    public class DepositService : IDepositService
    {
        #region Fields 
        private readonly IAccountDataRandomize _account; // Jest to alternatywa dla dziecziczenia, w tym przypadku nie musimy dziedziczyć po klasie BankAccount, wystarczy że w konstruktorze przekażemy obiekt klasy BankAccount. To pole jest tylko do odczytu, więc nie można go zmienić po przypisaniu wartości w konstruktorze.
        private IAuditLogger _auditLogger;
        private ATMDataRandomizeStructure _atmDataRandomizer; // Randomizowane dane dotyczące bankomatu, np. stan kaset z bankomatami
        private ICashScanner _cashScanner; // Symulowanie skanowania wpłaconych banknotów
        private AccountModelService _accountModelService; // Metody do obsługi modelu konta

        private Dictionary<int, int> _customerDeposit; // Stan wpłaty klienta z podziałem na nominały
        private Dictionary<int, int> _cashCassettes;// Stan bankomatu z podziałem na nominały (przed wpłatą)
        private Dictionary<int, int> _cashCassettesAfterDeposit; // Stan bankomatu z podziałem na nominały (po wpłacie)
        private Dictionary<int, int> _cashCassettesLimits; // Limit kaset bankomatu z podziałem na nominały
        #endregion

        #region Properties
        public IAuditLogger AuditLogger => _auditLogger;
        public Dictionary<int, int> CashCassettes => _cashCassettes;
        #endregion

        #region Constructors
        public DepositService()
        {
            this._atmDataRandomizer = new ATMDataRandomizeStructure();
            //this._account = new AccountDataRandomizeStructure();
            this._cashCassettes = new Dictionary<int, int>()
            {
                { 500, 0 },
                { 200, 0 },
                { 100, 0 },
                { 50, 0 },
                { 20, 0 },
                { 10, 0 }
            };
            _atmDataRandomizer.RandomizeCashCassettes(_cashCassettes, _cashCassettesLimits);
        }
        
        public DepositService(AccountDataRandomizeStructure account)
        {
            this._account = account;
        }

        public DepositService(AccountDataRandomizeStructure account,
            IAuditLogger auditLogger,
            ATMDataRandomizeStructure atmDataRandomizer,
            ICashScanner cashScanner) : this(account)
        {
            this._auditLogger = auditLogger;
            this._atmDataRandomizer = atmDataRandomizer;
            this._cashScanner = cashScanner;
        }

        public DepositService(AccountDataRandomizeStructure account,
            IAuditLogger auditLogger,
            ATMDataRandomizeStructure atmDataRandomizer,
            ICashScanner cashScanner,
            Dictionary<int, int> customerDeposit) : this(account, auditLogger, atmDataRandomizer, cashScanner)
        {
            this._customerDeposit = customerDeposit;
        }
        #endregion

        #region Methods
        public void DepositMoney(int amount)
        {
            if (amount <= 0)
            {
                AuditLogger.Log(LogLevel.Level.Error, "Wpłacana kwota środków musi być większa od zera");
                throw new ArgumentException("Wpłacana kwota środków musi być większa od zera", nameof(amount));
            }

            // Przyjmij wpisaną kwotę
            Console.WriteLine($"You have entered {amount}."); // Nie zaimplementowano słownika
            AuditLogger.Log(LogLevel.Level.Info, $"Wpisano kwotę {amount}.");

            // Zwiększ saldo na koncie
            _accountModelService.Deposit(amount);
            AuditLogger.Log(LogLevel.Level.Info, $"Zwiększono saldo o kwotę {amount}.");

            // Wyświetl zaktualizowane saldo
            Console.WriteLine($"Your new balance is {_accountModelService.GetBalance()}."); // Nie zaimplementowano słownika
            AuditLogger.Log(LogLevel.Level.Info, $"Nowe saldo wynosi {_accountModelService.GetBalance()}.");
        }

        public void ProcessCustomerDeposit(int denomination, int count)
        {
            if (_customerDeposit == null)
            {
                _customerDeposit = new Dictionary<int, int>();
            }

            if (_customerDeposit.ContainsKey(denomination))
            {
                _customerDeposit[denomination] += count;
            }
            else
            {
                _customerDeposit[denomination] = count;
            }

            Console.WriteLine($"Processed customer deposit: {count} banknotes of denomination {denomination}.");
            AuditLogger.Log(LogLevel.Level.Info, $"Processed customer deposit: {count} banknotes of denomination {denomination}.");
        }

        public void ResetCustomerDeposit()
        {
            _customerDeposit?.Clear();
            Console.WriteLine("Wpłata klienta została anulowana."); // Nie zaimplementowano słownika
            AuditLogger.Log(LogLevel.Level.Info, "Wpłata klienta została anulowana.");
        }

        //public bool AreBanknotesCompatibleWithCassettes(int cashAmount)
        //{
        //    return cashAmount % 10 == 0;
        //}

        public bool CanDepositMoney(int amount)
        {
            var availableMoney = CashCassettes.Sum(cassette => cassette.Key * cassette.Value);
            var canDeposit = availableMoney >= amount;
            AuditLogger.Log(LogLevel.Level.Info, $"Sprawdzanie możliwości wypłaty: {canDeposit}");
            return canDeposit;
        }

        public bool AreBanknotesCompatibleWithCassettes(int cashAmount) => cashAmount % 10 == 0;

        #endregion
    }
}
