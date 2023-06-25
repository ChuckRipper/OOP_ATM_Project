using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Interfaces.DataRandomizing;

namespace OOP_ATM_Project.Models
{
    public class ProcessingTime
    {
        #region Fields 
        private IProcessingTimeRandomize _processingTimeRandomizer;
        #endregion

        #region Properties
        public int Timeout { get; set; }
        public int MinProcessingTime { get; set; }
        public int MaxProcessingTime { get; set; }
        public int RandomProcessingTime { get; set; }
        public bool IsTimeoutExceeded { get; set; }
        #endregion

        #region Constructors
        public ProcessingTime()
        {
            Timeout = _processingTimeRandomizer.Timeout;
            MinProcessingTime = _processingTimeRandomizer.MinProcessingTime;
            MaxProcessingTime = _processingTimeRandomizer.MaxProcessingTime;
            RandomProcessingTime = _processingTimeRandomizer.RandomProcessingTime;
            IsTimeoutExceeded = _processingTimeRandomizer.IsTimeoutExceeded;
        }

        public ProcessingTime(IProcessingTimeRandomize processingTimeRandomizer)
        {
            Timeout = processingTimeRandomizer.Timeout;
            MinProcessingTime = processingTimeRandomizer.MinProcessingTime;
            MaxProcessingTime = processingTimeRandomizer.MaxProcessingTime;
            RandomProcessingTime = processingTimeRandomizer.RandomProcessingTime;
            IsTimeoutExceeded = processingTimeRandomizer.IsTimeoutExceeded;
        }
        #endregion

        #region Methods

        #endregion
    }
}
