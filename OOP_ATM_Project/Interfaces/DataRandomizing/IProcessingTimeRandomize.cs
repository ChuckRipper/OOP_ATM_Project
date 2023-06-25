using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ATM_Project.Interfaces.DataRandomizing
{
    /// <summary>
    /// Generowanie losowego czasu procesowania operacji lub transakcji a także porównywanie go z wartością progową (timeout) oraz odrzucanie transakcji jeśli czas procesowania przekroczy wartość progową (timeout)
    /// </summary>
    public interface IProcessingTimeRandomize
    {
        int Timeout { get; set; } // Właściwość przechowująca wartość progową - timeout
        int MinProcessingTime { get; set; } // Właściwość przechowująca minimalny czas procesowania
        int MaxProcessingTime { get; set; } // Właściwość przechowująca maksymalny czas procesowania
        int RandomProcessingTime { get; set; } // Właściwość przechowująca wylosowany czas procesowania
        bool IsTimeoutExceeded { get; set; } // Właściwość przechowująca informację, czy czas procesowania przekroczył wartość progową (timeout)
        //bool IsTimeoutExceeded(TimeSpan processingTime); // Metoda sprawdzająca, czy czas procesowania przekroczył wartość progową (timeout)
        int GenerateRandomProcessingTime(); // Metoda generująca losowy czas procesowania
        int GenerateRandomProcessingTime(int minProcessingTime, int maxProcessingTime);
    }
}
