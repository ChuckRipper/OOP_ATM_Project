using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Enums;
using OOP_ATM_Project.Interfaces.DataRandomizing;
using OOP_ATM_Project.Interfaces.Logging;
using OOP_ATM_Project.Interfaces.Services;
//using OOP_ATM_Project.Encryption;
using OOP_ATM_Project.Encryption.MD5;
using OOP_ATM_Project.Interfaces.Models;

namespace OOP_ATM_Project.Services
{
    public class PinChecker : IPinChecker
    {
        #region Fields 
        private IAuditLogger _auditLogger;
        //private readonly ICardDataRandomize _cardData;
        private readonly ICardModelService _cardModelService;
        private readonly MD5 _md5;
        private ILanguageService _languageService;
        #endregion

        #region Properties
        public IAuditLogger AuditLogger => _auditLogger;
        public MD5 MD5 => _md5;
        #endregion

        #region Constructors
        public PinChecker(IAuditLogger auditLogger, ICardModelService cardModelService)
        {
            _auditLogger = auditLogger;
            _cardModelService = cardModelService;
        }
        #endregion

        #region Methods
        public bool CheckPin(string enteredPin)
        {
            var md5Pin = new MD5(_cardModelService.GetCard().PinHash);
            AuditLogger.Log(LogLevel.Level.Debug, $"Skrót MD5 wprowadzonego PINu: {md5Pin.MD5Hash}");

            var isPinValid = md5Pin.VerifyHash(enteredPin);

            if (isPinValid)
            {
                AuditLogger.Log(LogLevel.Level.Debug, "PIN jest prawidłowy.");
            }
            else
            {
                AuditLogger.Log(LogLevel.Level.Debug, "PIN jest nieprawidłowy.");
            }

            return isPinValid;
        }

        public bool ChangePin(string newPin)
        {
            string oldPin = GetOldPin(); // Pobierz aktualny PIN (np. poprzez zapytanie użytkownika)

            if (CheckPin(oldPin)) // Sprawdź poprawność aktualnego PINu
            {
                var newPinHash = GeneratePinHash(newPin); // Wygeneruj skrót nowego PINu

                _cardModelService.UpdatePinHash(newPinHash); // Zaktualizuj skrót PINu w danych karty

                AuditLogger.Log(LogLevel.Level.Info, "PIN został pomyślnie zmieniony.");
                return true; // Zwróć true, jeśli zmiana PINu powiodła się
            }
            else
            {
                AuditLogger.Log(LogLevel.Level.Error, "Aktualny PIN jest nieprawidłowy. Zmiana PINu nie powiodła się.");
                return false; // Zwróć false, jeśli zmiana PINu nie powiodła się
            }
        }

        private string GetOldPin()
        {
            Console.WriteLine(_languageService.GetMessage("EnterCurrentPIN"));
            string oldPin = Console.ReadLine();
            return oldPin;
        }

        //private void GeneratePinHash(string pin)
        //{
        //    var md5 = new MD5();
        //    md5.ComputeHash(pin); // Zastąp to odpowiednią logiką generowania skrótu PINu
        //}

        private string GeneratePinHash(string pin)
        {
            MD5 md5 = new MD5();
            md5.ComputeHash(pin);
            return md5.MD5Hash;

            //var md5 = new MD5();
            //md5.ComputeHash(pin); // Zastąp to odpowiednią logiką generowania skrótu PINu
            //return md5.MD5Hash;
        }
        #endregion
    }
}
