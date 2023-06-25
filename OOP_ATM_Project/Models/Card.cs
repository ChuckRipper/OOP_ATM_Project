using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Interfaces.DataRandomizing;

namespace OOP_ATM_Project.Models
{
    public class Card
    {
        #region Fields
        private ICardDataRandomize _cardDataRandomizer;
        #endregion

        #region Properties
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }
        public string OwnerName { get; set; }
        public string CardType { get; set; }
        public string CardVendor { get; set; }
        public string PinHash { get; set; }
        public bool IsCardBlocked { get; set; }
        public bool IsCardInserted { get; set; }

        #endregion

        #region Constructors
        public Card()
        {
            CardNumber = _cardDataRandomizer.GenerateRandomCardNumber();
            ExpirationDate = _cardDataRandomizer.GenerateRandomCardExpirationDate();
            CVV = _cardDataRandomizer.GenerateRandomCardCVV();
            OwnerName = _cardDataRandomizer.GenerateRandomCardOwnerName();
            CardType = _cardDataRandomizer.GenerateRandomCardType();
            CardVendor = _cardDataRandomizer.GenerateRandomCardVendor();
            IsCardBlocked = _cardDataRandomizer.GenerateRandomCardBlocking();
            IsCardInserted = false;
        }

        public Card(ICardDataRandomize cardDataRandomizer)
        {
            CardNumber = cardDataRandomizer.GenerateRandomCardNumber();
            ExpirationDate = cardDataRandomizer.GenerateRandomCardExpirationDate();
            CVV = cardDataRandomizer.GenerateRandomCardCVV();
            OwnerName = cardDataRandomizer.GenerateRandomCardOwnerName();
            CardType = cardDataRandomizer.GenerateRandomCardType();
            CardVendor = cardDataRandomizer.GenerateRandomCardVendor();
            IsCardBlocked = cardDataRandomizer.GenerateRandomCardBlocking();
            IsCardInserted = false;
        }
        #endregion

        #region Methods

        #endregion
    }
}
