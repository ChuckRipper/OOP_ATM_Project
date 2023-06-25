using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Security.Cryptography;

using OOP_ATM_Project.Controllers;
using OOP_ATM_Project.Data;
using OOP_ATM_Project.Encryption;
using OOP_ATM_Project.Enums;
using OOP_ATM_Project.Extensions;
using OOP_ATM_Project.Interfaces;
using OOP_ATM_Project.Models;
using OOP_ATM_Project.Services;
using OOP_ATM_Project.Interfaces.Services;

namespace OOP_ATM_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Inicjacja bankomatu
            ATMController atm = new ATMController();

            // Uruchomienie bankomatu
            atm.InitializeATM();

            // Wybór języka
            ILanguageService languageService = new LanguageService();
            string selectedLanguage = languageService.GetLanguage();

            // Ustawienie wybranego języka
            languageService.SetLanguage(selectedLanguage);

            // Powitanie
            Console.WriteLine(languageService.GetMessage("Welcome"));

            // Prośba o włożenie karty
            Console.WriteLine(languageService.GetMessage("InsertCard"));

            // Symulacja włożenia karty
            Card insertedCard = atm.InsertCard();

            // Pętla główna programu
            atm.RunATM();

            // Zakończenie programu
            Console.WriteLine(languageService.GetMessage("TakeCard"));
            Console.WriteLine(languageService.GetMessage("Farewell")); // Słownik niezaimplementowany
        }
    }
}
