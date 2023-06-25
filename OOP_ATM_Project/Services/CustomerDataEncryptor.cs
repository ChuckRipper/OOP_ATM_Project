using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using OOP_ATM_Project.Interfaces;
using OOP_ATM_Project.Interfaces.DataRandomizing;
using OOP_ATM_Project.Interfaces.Logging;
using OOP_ATM_Project.Interfaces.Services;
using OOP_ATM_Project.Models;
using OOP_ATM_Project.Encryption.Twofish;
using OOP_ATM_Project.Enums;

namespace OOP_ATM_Project.Services
{
    public class CustomerDataEncryptor : IKeyStore, ICustomerDataEncryptor
    {
        #region Fields 
        private readonly ICustomerDataRandomize _customerDataRandomizer;
        private IAuditLogger _auditLogger;
        private string _encryptionKey;
        #endregion

        #region Properties
        public IAuditLogger AuditLogger => _auditLogger;
        public string EncryptionKey
        {
            get { return _encryptionKey; }
            set { SetEncryptionKey(value); }
        }
        #endregion

        #region Constructors
        public CustomerDataEncryptor(ICustomerDataRandomize customerDataRandomizer, IAuditLogger auditLogger)
        {
            _customerDataRandomizer = customerDataRandomizer;
            _auditLogger = auditLogger;
            _encryptionKey = GenerateEncryptionKey();
        }
        #endregion

        #region Methods
        public string GetEncryptionKey()
        {
            return _encryptionKey;
        }

        public void SetEncryptionKey(string key)
        {
            _encryptionKey = key;
        }

        public void EncryptCustomerData(Customer customer)
        {
            // Randomize customer data
            _customerDataRandomizer.RandomizeCustomerData(customer);

            // Serialize customer object to XML
            string serializedData = SerializeToXml(customer);

            // Encrypt the serialized data using Twofish algorithm
            byte[] encryptedData = EncryptData(serializedData, _encryptionKey);

            // Log the encrypted data as DEBUG
            AuditLogger.Log(LogLevel.Level.Debug, $"Zaszyfrowane dane klienta: {Convert.ToBase64String(encryptedData)}");
        }

        public string GenerateEncryptionKey()
        {
            // Generate a random encryption key
            byte[] keyBytes = new byte[32]; // 256-bit key
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(keyBytes);
            }

            return Convert.ToBase64String(keyBytes);
        }

        public string SerializeToXml(Customer customer)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Customer));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, customer);
                return writer.ToString();
            }
        }

        public byte[] EncryptData(string data, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            using (var twofish = new TwofishManaged())
            {
                twofish.Key = keyBytes;
                twofish.Mode = CipherMode.ECB; // Use ECB mode for simplicity

                using (ICryptoTransform encryptor = twofish.CreateEncryptor())
                {
                    byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                    byte[] encryptedData = encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length);
                    return encryptedData;
                }
            }
        }

        #endregion
    }
}
