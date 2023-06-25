using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Enums;
using OOP_ATM_Project.Interfaces.DataRandomizing;
using OOP_ATM_Project.Interfaces.Logging;
//using OOP_ATM_Project.Interfaces.Services;
using OOP_ATM_Project.Models;
using OOP_ATM_Project.Models.ModelState;
using OOP_ATM_Project.Interfaces.Models;
using OOP_ATM_Project.Logging;
using OOP_ATM_Project.Services;
using OOP_ATM_Project.Data.DataRandomizingStructures;
using OOP_ATM_Project.Interfaces.Services;
//using System.Security.Principal;

namespace OOP_ATM_Project.Controllers
{
    public class ATMController
    {
        #region Fields 
        // Randomizery
        private IAccountDataRandomize _accountDataRandomizer;
        private IATMDataRandomize _atmDataRandomizer;
        private ICardDataRandomize _cardDataRandomizer;
        private ICustomerDataRandomize _customerDataRandomizer;
        private IProcessingTimeRandomize _processingTimeRandomizer;
        private ITransactionRandomize _transactionDataRandomizer;

        // Modele
        private Account _account;
        private ATM _atm;
        private Card _currentCard;
        private Customer _customer;
        //private ProcessingTimeRandomizeStructure _processingTime;
        private Transaction _transaction;

        // Stany modeli
        private IAccountModelService _accountModelService;
        private IATMModelService _atmModelService;
        private ICardModelService _cardModelService;

        // Usługi
        private ICustomerDataEncryptor _customerDataEncryptor;
        private IDepositService _depositService;
        private ILanguageService _languageService;
        private IMoneyDispenser _moneyDispenser;
        private IPinChecker _pinChecker;
        private ITransactionHistory _transactionHistory;

        // Logowanie
        private IAuditLogger _auditLogger;
        //private LogColor _logColorizer;
        //private LogEntry _logEntry;
        //private LogFileWriter _logFileWriter;

        // Stan kaset bankomatu
        private Dictionary<int, int> _cashCassettes;
        #endregion

        #region Properties
        public IAuditLogger AuditLogger => _auditLogger;
        public string ATMID { get; set; }
        public ATMStatus.Status ATMStatus { get; set; }
        public Dictionary<int, int> CashCassettes { get; set; }
        #endregion

        #region Constructors
        public ATMController()
        {
            // Inicjalizacja zależności za pomocą domyślnych implementacji interfejsów
            _accountModelService = new AccountModelService();
            _moneyDispenser = new MoneyDispenser();
            _pinChecker = new PinChecker(this._auditLogger, this._cardModelService);
            _transactionHistory = new TransactionHistory();
            //_auditLogger = new AuditLogger(_logColorizer);
            _depositService = new DepositService();
            //_languageService = new LanguageService();
        }
        #endregion

        #region Methods
        public void InitializeATM()
        {
            // Inicjacja bankomatu
            AuditLogger.Log(LogLevel.Level.Info, "Inicjacja bankomatu");
        }

        public Card InsertCard()
        {
            Console.WriteLine(_languageService.GetMessage("InsertCard"));
            // TODO: Zaimplementować logikę włożenia karty
            return null; // Placeholder
        }

        public void RunATM()
        {
            bool isCardInserted = false;
            bool isPINValidated = false;
            int invalidPINAttempts = 0;

            while (true)
            {
                // Wybór języka
                string selectedLanguage = _languageService.GetLanguage();
                _languageService.SetLanguage(selectedLanguage);

                // Powitanie
                Console.WriteLine(_languageService.GetMessage("Welcome"));

                // Prośba o włożenie karty
                Console.WriteLine(_languageService.GetMessage("InsertCard"));
                Card insertedCard = InsertCard();

                if (insertedCard.IsCardBlocked)
                {
                    // Karta jest zablokowana
                    Console.WriteLine(_languageService.GetMessage("CardBlocked"));
                    break; // Koniec programu
                }
                else
                {
                    // Karta nie jest zablokowana
                    isCardInserted = true;
                }

                // Prośba o podanie kodu PIN
                Console.WriteLine(_languageService.GetMessage("EnterPIN"));
                string enteredPIN = Console.ReadLine();

                // Sprawdzenie poprawności PINu
                if (_cardModelService.IsPinHashMatched(enteredPIN, insertedCard.PinHash))
                {
                    // Poprawny PIN
                    isPINValidated = true;
                    invalidPINAttempts = 0;
                }
                else
                {
                    // Błędny PIN
                    invalidPINAttempts++;

                    if (invalidPINAttempts == 1)
                    {
                        // Pierwsza próba błędnego PINu
                        Console.WriteLine(_languageService.GetMessage("WrongPIN"));
                    }
                    else if (invalidPINAttempts == 2)
                    {
                        // Druga próba błędnego PINu
                        Console.WriteLine(_languageService.GetMessage("WrongPIN"));

                        // Ostrzeżenie o blokadzie karty przy kolejnym błędnym PINie
                        Console.WriteLine(_languageService.GetMessage("CardLockWarning"));
                    }
                    else if (invalidPINAttempts >= 3)
                    {
                        // Trzecia próba błędnego PINu
                        Console.WriteLine(_languageService.GetMessage("WrongPIN"));

                        // Blokada karty
                        Console.WriteLine(_languageService.GetMessage("CardLockError"));
                        insertedCard.IsCardBlocked = true;
                        //_cardModelService.EjectCard(); // Niezaimplementowane
                        break; // Koniec programu
                    }

                    // Powtórne podanie kodu PIN
                    Console.WriteLine(_languageService.GetMessage("EnterPIN"));
                    enteredPIN = Console.ReadLine();
                }

                if (isPINValidated)
                {
                    // Wyświetlanie opcji
                    Console.WriteLine(_languageService.GetMessage("Options"));
                    Console.WriteLine("1. " + _languageService.GetMessage("WithdrawCash"));
                    Console.WriteLine("2. " + _languageService.GetMessage("DepositCash"));
                    Console.WriteLine("3. " + _languageService.GetMessage("DisplayBalance"));
                    Console.WriteLine("4. " + _languageService.GetMessage("ShowLastTransactions"));
                    Console.WriteLine("5. " + _languageService.GetMessage("ChangePIN"));
                    Console.WriteLine();

                    // Wybór opcji
                    Console.WriteLine(_languageService.GetMessage("ChooseOption"));
                    string selectedOption = Console.ReadLine();

                    switch (selectedOption)
                    {
                        case "1":
                            // Wypłata gotówki
                            HandleWithdrawal();
                            break;

                        case "2":
                            // Wpłata gotówki
                            HandleDeposit();
                            break;

                        case "3":
                            // Wyświetl saldo
                            ShowBalance();
                            break;

                        case "4":
                            // Pokaż ostatnie 5 transakcji
                            ShowTransactionHistory();
                            break;

                        case "5":
                            // Zmień kod PIN
                            ChangePIN();
                            break;

                        default:
                            Console.Clear();
                            Console.WriteLine(_languageService.GetMessage("InvalidOption"));
                            AuditLogger.Log(LogLevel.Level.Fatal, "Nie wybrano właściwej opcji"); // Słownik niezaimplementowany
                            break;
                    }
                }
            }
        }

        //private void AuthenticateUser()
        //{
        //    Console.WriteLine(_languageService.GetMessage("EnterPIN"));
        //    string pin = Console.ReadLine();

        //    if (_pinChecker.CheckPin(pin))
        //    {
        //        Console.WriteLine(_languageService.GetMessage("WelcomeUser"));
        //        ShowMenu();
        //    }
        //    else
        //    {
        //        Console.WriteLine(_languageService.GetMessage("WrongPIN"));
        //        bool pinMatches = RetryPINVerification();
        //        if (pinMatches)
        //        {
        //            Console.WriteLine(_languageService.GetMessage("CardLockWarning"));
        //            bool pinMatchesAgain = RetryPINVerification();
        //            if (pinMatchesAgain)
        //            {
        //                Console.WriteLine(_languageService.GetMessage("CardLockError"));
        //                _cardModelService.BlockCard();
        //                Console.WriteLine(_languageService.GetMessage("TakeCard"));
        //            }
        //            else
        //            {
        //                AuthenticateUser();
        //            }
        //        }
        //        else
        //        {
        //            AuthenticateUser();
        //        }
        //    }
        //}

        //private bool RetryPINVerification()
        //{
        //    Console.WriteLine(_languageService.GetMessage("RepeatPIN"));
        //    string pin = Console.ReadLine();
        //    return _pinChecker.CheckPin(pin);
        //}

        private void ShowMenu()
        {
            // Lista funkcji
            //Console.WriteLine(_languageService.GetMessage("FunctionList"));
            Console.WriteLine("1. " + _languageService.GetMessage("CashWithdrawal"));
            Console.WriteLine("2. " + _languageService.GetMessage("CashDeposit"));
            Console.WriteLine("3. " + _languageService.GetMessage("ShowLast5Transactions"));
            Console.WriteLine("4. " + _languageService.GetMessage("ShowBalance"));
            Console.WriteLine("5. " + _languageService.GetMessage("ChangePIN"));

            string selectedOption = Console.ReadLine();

            switch (selectedOption)
            {
                case "1":
                    HandleWithdrawal();
                    break;
                case "2":
                    HandleDeposit();
                    break;
                case "3":
                    ShowTransactionHistory();
                    break;
                case "4":
                    ShowBalance();
                    break;
                case "5":
                    ChangePIN();
                    break;
                default:
                    //Console.WriteLine(_languageService.GetMessage("InvalidOption"));
                    break;
            }
        }

        private void HandleWithdrawal()
        {
            // Method for handling withdrawal
            Console.WriteLine(_languageService.GetMessage("WithdrawalAmountPrompt")); // Niezaimplementowane
            Console.WriteLine(_languageService.GetMessage("WithdrawalAmount")); // Niezaimplementowane
            Console.WriteLine(_languageService.GetMessage("CashWithdrawal"));
            string amountString = Console.ReadLine();

            if (int.TryParse(amountString, out int amount))
            {
                if (_moneyDispenser != null && _moneyDispenser.CanDispenseMoney(amount))
                {
                    if (_moneyDispenser != null && _moneyDispenser.CanDispenseMoney(amount))
                    {
                        _moneyDispenser.DispenseMoney(amount);

                        if (_transactionHistory != null)
                        {
                            _transactionHistory.AddTransaction(_transaction);
                        }

                        Console.WriteLine(_languageService.GetMessage("WithdrawalSuccess"));
                    }
                    else
                    {
                        Console.WriteLine(_languageService.GetMessage("WithdrawalNotPossibleFunds"));
                        // TO DO: Słownik zaimplementowany, jednak brak logiki na sprawdzenie czy klient może mieć wypłacone środki, tj. czy kwota zadeklarowana przez klienta nie przekracza jego stanu konta.
                        // Zaimplementowana jedynie logika na sprawdzenie, czy bankomat ma wystarczająco środków na wypłatę.
                    }
                }
                else
                {
                    Console.WriteLine(_languageService.GetMessage("WithdrawalNotPossibleATM"));
                }
            }
            else
            {
                Console.WriteLine(_languageService.GetMessage("InvalidAmount")); // Niezaimplementowane
            }
        }

        private void HandleDeposit()
        {
            Console.Clear();
            Console.WriteLine(_languageService.GetMessage("CashDeposit"));
            Console.WriteLine(_languageService.GetMessage("EnterDepositAmount")); // Słownik niezaimplementowany
            string depositAmountString = Console.ReadLine();
            if (int.TryParse(depositAmountString, out int depositAmount))
            {
                if (_depositService.CanDepositMoney(depositAmount))
                {
                    Console.WriteLine(_languageService.GetMessage("EnterDenomination"));
                    string denominationString = Console.ReadLine();
                    if (int.TryParse(denominationString, out int denomination))
                    {
                        Console.WriteLine(_languageService.GetMessage("EnterCount"));
                        string countString = Console.ReadLine();
                        if (int.TryParse(countString, out int count))
                        {
                            _depositService.ProcessCustomerDeposit(denomination, count);
                            Console.WriteLine(_languageService.GetMessage("DepositSuccess"));
                        }
                        else
                        {
                            Console.WriteLine(_languageService.GetMessage("InvalidCount"));
                        }
                    }
                    else
                    {
                        Console.WriteLine(_languageService.GetMessage("InvalidDenomination"));
                    }
                }
                else
                {
                    Console.WriteLine(_languageService.GetMessage("CannotAcceptDeposit"));
                }
            }
            else
            {
                Console.WriteLine(_languageService.GetMessage("InvalidAmount"));
            }
        }

        private void ShowTransactionHistory()
        {
            // Prezentacja historii transakcji
            Console.Clear();
            Console.WriteLine(_languageService.GetMessage("ShowLast5Transactions"));
            List<Transaction> lastTransactions = _transactionHistory.GetLastTransactions(5);

            if (lastTransactions.Count > 0)
            {
                Console.WriteLine("Last 5 transactions:");
                Console.WriteLine(_languageService.GetMessage("TransactionHistoryHeader")); // Niezaimplementowane
                AuditLogger.Log(LogLevel.Level.Info, "Lista transakcji:");
                foreach (var transaction in lastTransactions)
                {
                    Console.WriteLine($"Type: {transaction.Type}, Amount: {transaction.Amount.ToString("N2")}, Date: {transaction.Date}");
                }
            }
            else
            {
                Console.WriteLine(_languageService.GetMessage("NoTransactionHistory")); // Niezaimplementowane
                AuditLogger.Log(LogLevel.Level.Warn, "No transaction history to show");
            }
        }

        private void ShowBalance()
        {
            Console.Clear();
            Console.WriteLine(_languageService.GetMessage("ShowBalance"));
            decimal balance = _accountModelService.GetBalance();
            Console.WriteLine($"Twój stan konta to: \n{balance.ToString("N2")}"); // Słownik niezaimplementowany
            //// Method for showing balance
            //decimal balance = _accountModelService.GetBalance();
            //Console.WriteLine(_languageService.GetMessage("BalancePrompt") + " " + balance);
        }

        private void ChangePIN()
        {
            // Zmiana PINu
            Console.Clear();

            bool isPINValidated = false;
            int invalidPINAttempts = 0;

            Console.WriteLine(_languageService.GetMessage("ChangePIN"));
            Console.WriteLine(_languageService.GetMessage("EnterNewPIN")); // Słownik niezaimplementowany
            string newPin = Console.ReadLine();
            Console.WriteLine(_languageService.GetMessage("RepeatNewPIN"));
            string repeatedPin = Console.ReadLine();

            if (newPin == repeatedPin)
            {
                if (_pinChecker.ChangePin(newPin))
                {
                    Console.WriteLine(_languageService.GetMessage("PINChangeSuccess")); // Niezaimplementowane
                }
                else
                {
                    Console.WriteLine(_languageService.GetMessage("PINChangeFailure")); // Niezaimplementowane
                }
            }
            else
            {
                Console.WriteLine(_languageService.GetMessage("PinMismatchWarning"));
                Console.WriteLine(_languageService.GetMessage("EnterPIN"));
            }

            // Powtórzenie PINu przy jego zmianie
            //RetryPINVerification();


            //if (RetryPINVerification())
            //{
            //    if (_pinChecker.ChangePin(newPin))
            //    {
            //        Console.WriteLine(_languageService.GetMessage("PINChangeSuccess")); // Niezaimplementowane
            //    }
            //    else
            //    {
            //        Console.WriteLine(_languageService.GetMessage("PINChangeFailure")); // Niezaimplementowane
            //    }
            //}
            //else
            //{
            //    Console.WriteLine(_languageService.GetMessage("PinMismatchWarning"));
            //    Console.WriteLine(_languageService.GetMessage("EnterPIN"));
            //}
        }

        #endregion
    }

}
