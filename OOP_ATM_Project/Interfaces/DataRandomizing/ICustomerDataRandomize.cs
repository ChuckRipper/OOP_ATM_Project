using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Models;

namespace OOP_ATM_Project.Interfaces.DataRandomizing
{
    /// <summary>
    /// Randomizowanie danych klienta, tj. imię, nazwisko, PESEL, numer telefonu, adres, data urodzenia
    /// </summary>
    public interface ICustomerDataRandomize
    {
        string GenerateRandomFirstName();
        string GenerateRandomLastName();
        string GenerateRandomPESEL();
        string GenerateRandomPhoneNumber();
        string GenerateRandomEmail();
        string GenerateRandomAddress();
        DateTime GenerateRandomBirthDate();
        //void RandomizeCustomerData();
        void RandomizeCustomerData();
        void RandomizeCustomerData(Customer customer);
    }
}
