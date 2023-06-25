using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace OOP_ATM_Project.Interfaces
{
    public interface IKeyStore
    {
        string GetEncryptionKey();
        void SetEncryptionKey(string key);
    }
}
