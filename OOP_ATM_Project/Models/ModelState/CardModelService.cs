using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Data.DataRandomizingStructures;
using OOP_ATM_Project.Encryption.MD5;
using OOP_ATM_Project.Enums;
//using OOP_ATM_Project.Interfaces.DataRandomizing;
using OOP_ATM_Project.Interfaces.Logging;
using OOP_ATM_Project.Interfaces.Models;
using OOP_ATM_Project.Logging;

namespace OOP_ATM_Project.Models.ModelState
{
    public class CardModelService : ICardModelService
    {
        #region Fields
        private Card _card;
        private IAuditLogger _auditLogger;
        private readonly CardDataRandomizeStructure _cardDataRandomizer;
        private readonly MD5 _mD5 = new MD5();
        #endregion

        #region Properties
        public IAuditLogger AuditLogger => _auditLogger;
        #endregion

        #region Constructors
        public CardModelService()
        {
            _card = new Card(_cardDataRandomizer);
        }

        public CardModelService(CardDataRandomizeStructure cardDataRandomize)
        {
            _cardDataRandomizer = cardDataRandomize;
            _card = new Card(_cardDataRandomizer);
        }
        #endregion

        #region Methods
        public Card GetCard()
        {
            AuditLogger.Log(LogLevel.Level.Debug, "Pobrano dane karty");
            return _card;
        }

        public void UpdateCard(Card card)
        {
            AuditLogger.Log(LogLevel.Level.Debug, "Zaktualizowano dane karty");
            _card = card;
        }

        public void BlockCard()
        {
            if (_card.IsCardBlocked)
            {
                AuditLogger.Log(LogLevel.Level.Warn, "Karta jest już zablokowana");
            }
            else
            {
                _card.IsCardBlocked = true;
            }
        }

        public void BlockCard(Card card)
        {
            if (_card.IsCardBlocked)
            {
                AuditLogger.Log(LogLevel.Level.Warn, "Karta jest już zablokowana");
            }
            else
            {
                _card.IsCardBlocked = true;
            }
        }

        public void UpdatePinHash(string newPinHash)
        {
            _card.PinHash = newPinHash;
        }

        public string GetPinHash()
        {
            return _mD5.MD5Hash;
        }

        public bool IsCardBlocked()
        {
            return _card.IsCardBlocked;
        }

        public bool IsCardInserted()
        {
            return _card.IsCardInserted;
        }

        public bool IsPinHashMatched(string TypedPin, string GeneratedPin)
        {
            MD5 typedPinMD5 = new MD5();
            typedPinMD5.ComputeHash(TypedPin);

            return _mD5 == typedPinMD5;
        }
        #endregion
    }
}
