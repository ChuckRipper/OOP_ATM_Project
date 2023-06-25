using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Interfaces.DataRandomizing;
//using OOP_ATM_Project.Extensions;
using OOP_ATM_Project.Interfaces.Logging;
using OOP_ATM_Project.Interfaces.Services;
using OOP_ATM_Project.Logging;

namespace OOP_ATM_Project.Data.DataRandomizingStructures
{
    public class ProcessingTimeRandomizeStructure : IProcessingTimeRandomize
    {
        #region Fields 
        private int _timeout;
        private int _minProcessingTime;
        private int _maxProcessingTime;
        private int _randomProcessingTime;
        private bool _isTimeoutExceeded;

        private IAuditLogger _auditLogger;

        private static ProcessingTimeRandomizeStructure _instance; // Singleton
        #endregion

        #region Properties
        public IAuditLogger AuditLogger => _auditLogger;

        public int Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                if (value >= 0)
                {
                    _timeout = value;
                }
                else
                {
                    throw new Exception("Czas przetwarzania nie może być ujemny!");
                }
                
            }
        }

        public int MinProcessingTime
        {
            get
            {
                return _minProcessingTime;
            }
            set
            {
                if (value >= 0)
                {
                    _minProcessingTime = value;
                }
                else
                {
                    throw new Exception("Minimalny czas przetwarzania nie może być ujemny!");
                }
            }
        }

        public int MaxProcessingTime
        {
            get
            {
                return _maxProcessingTime;
            }
            set
            {
                if (value >= 0)
                {
                    _maxProcessingTime = value;
                }
                else
                {
                    throw new Exception("Maksymalny czas przetwarzania nie może być ujemny!");
                }
            }
        }
        
        public int RandomProcessingTime
        {
            get
            {
                return _randomProcessingTime;
            }
            set
            {
                if (value >= 0)
                {
                    _randomProcessingTime = value;
                }
                else
                {
                    throw new Exception("Losowy czas przetwarzania nie może być ujemny!");
                }
            }
        }

        public bool IsTimeoutExceeded
        {
            get
            {
                return _isTimeoutExceeded;
            }
            set
            {
                if (value.GetType() == typeof(bool))
                {
                    _isTimeoutExceeded = value;
                }
                else
                {
                    throw new Exception("Flaga przekroczenia czasu progrowego nie może być innym typem niż typ logiczny (bool)");
                }
            }
        }

        public static ProcessingTimeRandomizeStructure Instance // Singleton
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ProcessingTimeRandomizeStructure();
                }
                return _instance;
            }
        }
        #endregion

        #region Constructors
        public ProcessingTimeRandomizeStructure()
        {
            this.MinProcessingTime = 0;
            this.MaxProcessingTime = 200000; // Czas w milisekundach, 20 sekund 
            this.Timeout = 150000; // Czas w milisekundach, 15 sekund
            this.IsTimeoutExceeded = false;
            this.RandomProcessingTime = this.GenerateRandomProcessingTime(this.MinProcessingTime, this.MaxProcessingTime);
        }
        #endregion

        #region Methods
        //public TimeSpan GenerateRandomProcessingTime(int minProcessingTime, int maxProcessingTime)
        //{
        //    Random random = new Random();
        //    int randomProcessingTime = random.Next(minProcessingTime, maxProcessingTime);
        //    TimeSpan processingTime = new TimeSpan(0, 0, randomProcessingTime);
        //    return processingTime;
        //}

        public int GenerateRandomProcessingTime(int minProcessingTime, int maxProcessingTime)
        {
            Random random = new Random();
            int randomProcessingTime = random.Next(minProcessingTime, maxProcessingTime);
            return randomProcessingTime;
        }

        public int GenerateRandomProcessingTime()
        {
            Random random = new Random();
            int randomProcessingTime = random.Next(this.MinProcessingTime, this.MaxProcessingTime);
            return randomProcessingTime;
        }
        #endregion
    }
}
