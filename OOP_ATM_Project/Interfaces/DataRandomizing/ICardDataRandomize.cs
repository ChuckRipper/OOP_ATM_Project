using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ATM_Project.Interfaces.DataRandomizing
{
    /// <summary>
    /// Randomizowanie danych karty kredytowej, tj. numer karty, data ważności, CVV, nazwa właściciela, typ karty, dostawca karty.
    /// </summary>
    public interface ICardDataRandomize
    {
        string GenerateRandomCardNumber();
        string GenerateRandomCardExpirationDate();
        string GenerateRandomCardCVV();
        string GenerateRandomCardOwnerName();
        string GenerateRandomCardType();
        string GenerateRandomCardVendor(); // np. VISA, MasterCard, American Express, Maestro, itp.
        string GenerateRandomCardPIN(); // 4 cyfry
        bool GenerateRandomCardBlocking(); // true - zablokowana, false - niezablokowana
        //string GetPinHash(); // Hash MD5 z PINu
        void RandomizeCardData();
    }
}
