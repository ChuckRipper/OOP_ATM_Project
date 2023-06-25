using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Interfaces.Logging;
using OOP_ATM_Project.Models;
using OOP_ATM_Project.Encryption.MD5;
using OOP_ATM_Project.Enums;
using OOP_ATM_Project.Interfaces.DataRandomizing;

namespace OOP_ATM_Project.Data.DataRandomizingStructures
{
    public class CardDataRandomizeStructure : ICardDataRandomize
    {
        #region Fields 
        private IAuditLogger _auditLogger;
        private static Customer _customer;
        private MD5 _pinMD5;
        private string _pin;
        private static Random random = new Random();
        private static string cardTypes = "Credit,Debit,Business,Student,Prepaid";
        private static string cardVendors = "VISA,MasterCard,American Express,Maestro";
        private bool _isCardBlocked;

        private static CardDataRandomizeStructure _instance; // Singleton
        #endregion

        #region Properties
        public IAuditLogger AuditLogger => _auditLogger;
        public bool IsCardBlocked => _isCardBlocked;

        public static CardDataRandomizeStructure Instance // Singleton
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CardDataRandomizeStructure();
                }
                return _instance;
            }
        }
        #endregion

        #region Constructors
        public CardDataRandomizeStructure()
        {
            RandomizeCardData();
        }

        public CardDataRandomizeStructure(Customer customer)
        {
            _customer = customer;
            RandomizeCardData();
        }
        #endregion

        #region Methods
        public string GenerateRandomCardNumber()
        {
            AuditLogger.Log(LogLevel.Level.Debug, "Generowanie losowego numeru karty");
            var cardNumber = new string(Enumerable.Repeat("0123456789", 16)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return cardNumber;
        }

        public string GenerateRandomCardExpirationDate()
        {
            AuditLogger.Log(LogLevel.Level.Debug, "Generowanie losowej daty wygaśnięcia karty");
            var month = random.Next(1, 13);
            var year = random.Next(DateTime.Now.Year, DateTime.Now.Year + 6);
            return $"{month:D2}/{year - 2000:D2}";
        }

        public string GenerateRandomCardCVV()
        {
            AuditLogger.Log(LogLevel.Level.Debug, "Generowanie losowego kodu CVV/CVC");
            var cvv = new string(Enumerable.Repeat("0123456789", 3)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return cvv;
        }

        public string GenerateRandomCardOwnerName() // Generowanie danych właściciela z danych klienta
        {
            AuditLogger.Log(LogLevel.Level.Debug, "Generowanie losowych danych właściciela"); 
            return _customer.FirstName + " " + _customer.LastName;
        }

        public string GenerateRandomCardType()
        {
            AuditLogger.Log(LogLevel.Level.Debug, "Generowanie losowego typu karty");
            var types = cardTypes.Split(",");
            return types[random.Next(types.Length)];
        }

        public string GenerateRandomCardVendor()
        {
            AuditLogger.Log(LogLevel.Level.Debug, "Generowanie losowego vendora karty");
            var vendors = cardVendors.Split(",");
            return vendors[random.Next(vendors.Length)];
        }

        public string GenerateRandomCardPIN()
        {
            AuditLogger.Log(LogLevel.Level.Debug, "Generowanie losowego kodu PIN");
            _pin = new string(Enumerable.Repeat("0123456789", 4)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            AuditLogger.Log(LogLevel.Level.Debug, "Kod PIN: " + _pin);
            _pinMD5 = new MD5();
            _pinMD5.ComputeHash(_pin);
            return _pin;
        }

        public bool GenerateRandomCardBlocking()
        {
            AuditLogger.Log(LogLevel.Level.Debug, "Generowanie losowego stanu karty (zablokowana/niezablokowana)");
            var isBlocked = random.Next(0, 2);
            if (isBlocked == 1)
            {
                _isCardBlocked = true;
            }
            else
            {
                _isCardBlocked = false;
            }
            return _isCardBlocked;
        }

        public void RandomizeCardData()
        {
            GenerateRandomCardNumber();
            GenerateRandomCardExpirationDate();
            GenerateRandomCardCVV();
            GenerateRandomCardOwnerName();
            GenerateRandomCardType();
            GenerateRandomCardVendor();
            GenerateRandomCardPIN();
            GenerateRandomCardBlocking();
        }
        #endregion
    }
}
