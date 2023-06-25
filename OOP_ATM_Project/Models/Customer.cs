using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Interfaces.DataRandomizing;

namespace OOP_ATM_Project.Models
{
    public class Customer
    {
        #region Fields 
        private ICustomerDataRandomize _customerDataRandomizer;
        #endregion

        #region Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PESEL { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        #endregion

        #region Constructors
        public Customer()
        {
            FirstName = _customerDataRandomizer.GenerateRandomFirstName();
            LastName = _customerDataRandomizer.GenerateRandomLastName();
            PESEL = _customerDataRandomizer.GenerateRandomPESEL();
            PhoneNumber = _customerDataRandomizer.GenerateRandomPhoneNumber();
            Email = _customerDataRandomizer.GenerateRandomEmail();
            Address = _customerDataRandomizer.GenerateRandomAddress();
            BirthDate = _customerDataRandomizer.GenerateRandomBirthDate();
        }
        public Customer(ICustomerDataRandomize customerDataRandomizer)
        {
            FirstName = customerDataRandomizer.GenerateRandomFirstName();
            LastName = customerDataRandomizer.GenerateRandomLastName();
            PESEL = customerDataRandomizer.GenerateRandomPESEL();
            PhoneNumber = customerDataRandomizer.GenerateRandomPhoneNumber();
            Email = customerDataRandomizer.GenerateRandomEmail();
            Address = customerDataRandomizer.GenerateRandomAddress();
            BirthDate = customerDataRandomizer.GenerateRandomBirthDate();
        }
        #endregion

        #region Methods

        #endregion

    }
}
