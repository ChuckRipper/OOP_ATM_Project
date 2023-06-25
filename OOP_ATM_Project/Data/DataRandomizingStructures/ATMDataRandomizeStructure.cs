using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Enums;
using OOP_ATM_Project.Interfaces.DataRandomizing;
using OOP_ATM_Project.Interfaces.Logging;
using OOP_ATM_Project.Interfaces.Models;
using OOP_ATM_Project.Models;
using OOP_ATM_Project.Interfaces.Services;
using OOP_ATM_Project.Logging;

namespace OOP_ATM_Project.Data.DataRandomizingStructures
{
    public class ATMDataRandomizeStructure : IATMDataRandomize
    {
        #region Fields 
        private Dictionary<int, int> _cashCassettes; // Stan kaset z banknotami
        private readonly Dictionary<int, int> _cashCassettesLimits; // Limit pojemności kaset z banknotami
        //private Dictionary<int, int> _customerDeposit; // Stan wpłaty klienta z podziałem na nominały
        private Random _random;
        private ATMStatus.Status _atmStatus = ATMStatus.Status.Active;
        private ATMFunctionality.Functionality _atmFunctionality = ATMFunctionality.Functionality.None; // domyślny stan bankomatu
        private IAuditLogger _auditLogger;
        private ATM _ATM;

        private static ATMDataRandomizeStructure _instance; // Singleton
        #endregion

        #region Properties
        public IAuditLogger AuditLogger => _auditLogger;
        public Dictionary<int, int> CashCassettes => _cashCassettes;
        public Dictionary<int, int> CashCassettesLimits => _cashCassettesLimits;

        public static ATMDataRandomizeStructure Instance // Singleton
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ATMDataRandomizeStructure();
                }
                return _instance;
            }
        }
        #endregion

        #region Constructors
        public ATMDataRandomizeStructure()
        {
            _random = new Random();
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

            _atmStatus = ATMStatus.Status.Active;
            _atmFunctionality = ATMFunctionality.Functionality.None;
            _ATM = new ATM();

            _random = new Random();
            _auditLogger = new AuditLogger();

            //_customerDeposit = new Dictionary<int, int>();

            _atmStatus = (ATMStatus.Status)Enum.Parse(typeof(ATMStatus.Status), GenerateRandomATMStatus());
            RandomizeCashCassettes();
        }

        public ATMDataRandomizeStructure(IAuditLogger auditLogger)
        {
            _auditLogger = auditLogger;
            _random = new Random();
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

            RandomizeCashCassettes();
            _random = new Random();

            //_customerDeposit = new Dictionary<int, int>();

            _atmStatus = (ATMStatus.Status)Enum.Parse(typeof(ATMStatus.Status), GenerateRandomATMStatus());
        }
        #endregion

        #region Methods
        public string GenerateRandomATMID()
        {
            var atmID = Guid.NewGuid().ToString();
            AuditLogger.Log(LogLevel.Level.Debug, $"Generowanie losowego ID bankomatu: {atmID}");
            return atmID;
        }

        public string GenerateRandomATMStatus()
        {
            Array values = Enum.GetValues(typeof(ATMStatus.Status));
            Random random = new Random();
            var randomStatus = (ATMStatus.Status)values.GetValue(random.Next(values.Length));
            AuditLogger.Log(LogLevel.Level.Debug, $"Generowanie losowego statusu bankomatu: {randomStatus}");
            return randomStatus.ToString();
        }

        public void RandomizeCashCassettes()
        {
            Random rand = new Random();

            AuditLogger.Log(LogLevel.Level.Info, $"Stan kaset:");
            foreach (var cassette in CashCassettes.Keys.ToList())
            {
                CashCassettes[cassette] = _random.Next(0, CashCassettesLimits[cassette]);
                AuditLogger.Log(LogLevel.Level.Info,$"Nominał {cassette} PLN: {CashCassettes[cassette]} / {CashCassettesLimits[cassette]} ");
            }
        }

        public void RandomizeCashCassettes(Dictionary<int, int> cashCassettes)
        {
            Random rand = new Random();

            AuditLogger.Log(LogLevel.Level.Info, $"Stan kaset:");
            foreach (var cassette in cashCassettes.Keys.ToList())
            {
                cashCassettes[cassette] = _random.Next(0, CashCassettesLimits[cassette]);
                AuditLogger.Log(LogLevel.Level.Info, $"Nominał {cassette} PLN: {CashCassettes[cassette]} / {CashCassettesLimits[cassette]} ");
            }
        }

        public void RandomizeCashCassettes(Dictionary<int, int> cashCassettes,
            Dictionary<int, int> cashCassettesLimits)
        {
            Random rand = new Random();

            AuditLogger.Log(LogLevel.Level.Info, $"Stan kaset:");
            foreach (var cassette in cashCassettes.Keys.ToList())
            {
                cashCassettes[cassette] = _random.Next(0, cashCassettesLimits[cassette]);
                AuditLogger.Log(LogLevel.Level.Info, $"Nominał {cassette} PLN: {cashCassettes[cassette]} / {cashCassettesLimits[cassette]} ");
            }
        }

        public void RandomizeATMData()
        {
            GenerateRandomATMID();
            GenerateRandomATMStatus();
            RandomizeCashCassettes();

            _atmStatus = (ATMStatus.Status)Enum.Parse(typeof(ATMStatus.Status), GenerateRandomATMStatus());

            AuditLogger.Log(LogLevel.Level.Debug, _atmStatus.ToString());
            AuditLogger.Log(LogLevel.Level.Debug, "Randomizacja danych bankomatu zakończona pomyślnie.");
        }
        #endregion
    }
}
