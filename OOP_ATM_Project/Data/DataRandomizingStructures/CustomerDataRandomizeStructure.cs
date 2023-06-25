using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Controllers;
using OOP_ATM_Project.Interfaces.DataRandomizing;
using OOP_ATM_Project.Interfaces.Logging;
using OOP_ATM_Project.Models;

namespace OOP_ATM_Project.Data.DataRandomizingStructures
{
    public class CustomerDataRandomizeStructure : ICustomerDataRandomize
    {
        #region Fields 
        private IAuditLogger _auditLogger;
        private Random _random = new Random();
        private Customer _customer;
        private List<string> _firstNamesMale = new List<string> { "Jan", "Piotr", "Marek", /*...*/ };
        private List<string> _firstNamesFemale = new List<string> { "Anna", "Maria", "Katarzyna", /*...*/ };
        private List<string> _lastNamesMale = new List<string> { "Kowalski", "Nowak", "Wisniewski", /*...*/ };
        private List<string> _lastNamesFemale = new List<string> { "Kowalska", "Nowak", "Wisniewska", /*...*/ };

        private static CustomerDataRandomizeStructure _instance; // Singleton
        #endregion

        #region Properties
        public IAuditLogger AuditLogger => _auditLogger;

        public static CustomerDataRandomizeStructure Instance // Singleton
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CustomerDataRandomizeStructure();
                }
                return _instance;
            }
        }
        #endregion

        #region Constructors
        public CustomerDataRandomizeStructure()
        {
            RandomizeCustomerData();
        }
        #endregion

        #region Methods
        public string GenerateRandomFirstName()
        {
            bool isMale = _random.Next(2) == 0;

            if (isMale)
            {
                return _firstNamesMale[_random.Next(_firstNamesMale.Count)];
            }
            else
            {
                return _firstNamesFemale[_random.Next(_firstNamesFemale.Count)];
            }
        }

        public string GenerateRandomLastName()
        {
            bool isMale = _random.Next(2) == 0;

            if (isMale)
            {
                return _lastNamesMale[_random.Next(_lastNamesMale.Count)];
            }
            else
            {
                return _lastNamesFemale[_random.Next(_lastNamesFemale.Count)];
            }
        }

        public string GenerateRandomPESEL()
        {
            // Uproszczona wersja generowania numeru PESEL, bez walidacji
            return string.Concat(Enumerable.Range(0, 11).Select(n => _random.Next(10).ToString()));
        }

        public string GenerateRandomPhoneNumber()
        {
            return string.Concat(Enumerable.Range(0, 9).Select(n => _random.Next(10).ToString()));
        }

        public string GenerateRandomEmail()
        {
            // Uproszczona wersja generowania adresu email
            return $"test{_random.Next(1000, 9999)}@test.com";
        }

        public string GenerateRandomAddress()
        {
            // Uproszczona wersja generowania adresu
            return $"Street {_random.Next(1, 200)}, City {_random.Next(1, 200)}";
        }

        public DateTime GenerateRandomBirthDate()
        {
            DateTime start = new DateTime(1950, 1, 1);
            int range = ( DateTime.Today - start ).Days;
            return start.AddDays(_random.Next(range));
        }

        public void RandomizeCustomerData(Customer customer)
        {
            GenerateRandomFirstName();
            GenerateRandomLastName();
            GenerateRandomPESEL();
            GenerateRandomPhoneNumber();
            GenerateRandomEmail();
            GenerateRandomAddress();
            GenerateRandomBirthDate();
        }

        public void RandomizeCustomerData()
        {
            GenerateRandomFirstName();
            GenerateRandomLastName();
            GenerateRandomPESEL();
            GenerateRandomPhoneNumber();
            GenerateRandomEmail();
            GenerateRandomAddress();
            GenerateRandomBirthDate();
        }
        #endregion
    }
}
