using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOP_ATM_Project.Models;

namespace OOP_ATM_Project.Interfaces.Services
{
    public interface ICustomerDataEncryptor
    {
        #region Fields 

        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion

        #region Methods
        string GetEncryptionKey();
        void SetEncryptionKey(string key);
        void EncryptCustomerData(Customer customer);
        string GenerateEncryptionKey();
        string SerializeToXml(Customer customer);
        byte[] EncryptData(string data, string key);
        #endregion
    }
}
