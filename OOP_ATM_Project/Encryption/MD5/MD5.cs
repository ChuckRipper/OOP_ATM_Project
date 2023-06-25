using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace OOP_ATM_Project.Encryption.MD5
{
    public sealed class MD5 : IDisposable
    {
        #region Fields 
        private string _md5Hash;
        private string _salt;
        #endregion

        #region Properties
        public string MD5Hash => _md5Hash;
        public string Salt => _salt;
        #endregion

        #region Constructors
        public MD5()
        {
            _md5Hash = string.Empty;
            _salt = string.Empty;
        }

        public MD5(string md5Hash)
        {
            _md5Hash = md5Hash;
            GenerateRandomSalt();
        }

        public MD5(string md5Hash, string salt)
        {
            _md5Hash = md5Hash;
            _salt = salt;
        }
        #endregion

        #region Methods
        public static MD5 Create()
        {
            return new MD5();
        }

        private void GenerateRandomSalt()
        {
            int saltLength = 8; // Długość losowej soli
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder saltBuilder = new StringBuilder();

            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] saltBytes = new byte[saltLength];
                rng.GetBytes(saltBytes);

                foreach (byte saltByte in saltBytes)
                {
                    saltBuilder.Append(validChars[saltByte % validChars.Length]);
                }
            }

            _salt = saltBuilder.ToString();
        }

        public void ComputeHash(string input)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                _md5Hash = BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }

        public void ComputeHash(byte[] inputBytes)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                _md5Hash = BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }

        //public byte[] ComputeHash(byte[] buffer)
        //{
        //    // Tworzenie instancji obiektu MD5
        //    using (MD5 md5 = MD5.Create())
        //    {
        //        // Obliczanie hasha dla podanego bufora
        //        byte[] hash = md5.ComputeHash(buffer);

        //        // Zwracanie obliczonego hasha
        //        return hash;
        //    }
        //}

        //public static MD5 Create()
        //{
        //    return new MD5CryptoServiceProvider();
        //}

        public void ComputeHashWithSalt(string input)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input + _salt);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                _md5Hash = BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }

        public bool Compare(MD5 md5_1, MD5 md5_2)
        {
            return md5_1.MD5Hash.Equals(md5_2.MD5Hash) && md5_1.Salt.Equals(md5_2.Salt);
        }

        public bool VerifyHash(string input)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                string computedHash = BitConverter.ToString(hashBytes).Replace("-", "");
                return _md5Hash.Equals(computedHash);
            }
        }

        public bool VerifyHash(byte[] inputBytes)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                string computedHash = BitConverter.ToString(hashBytes).Replace("-", "");
                return _md5Hash.Equals(computedHash);
            }
        }

        //public bool CompareMD5Hashes(string input, string hashedValue)
        //{
        //    using (MD5 md5 = MD5.Create())
        //    {
        //        // Oblicz hash dla wprowadzonego tekstu
        //        byte[] inputHash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

        //        // Zamień hash na string w formacie szesnastkowym
        //        StringBuilder builder = new StringBuilder();
        //        for (int i = 0; i < inputHash.Length; i++)
        //        {
        //            builder.Append(inputHash[i].ToString("x2"));
        //        }
        //        string inputHashString = builder.ToString();

        //        // Porównaj hashe
        //        return String.Equals(inputHashString, hashedValue, StringComparison.OrdinalIgnoreCase);
        //    }
        //}

        public bool CompareHashes(byte[] hash1Bytes, byte[] hash2Bytes)
        {
            string hash1 = BitConverter.ToString(hash1Bytes).Replace("-", "");
            string hash2 = BitConverter.ToString(hash2Bytes).Replace("-", "");
            return hash1.Equals(hash2);
        }

        public static bool IsEqualMD5(MD5 md5, string input)
        {
            return md5 == input;
        }

        public static bool IsNotEqualMD5(MD5 md5, string input)
        {
            return md5 != input;
        }
        public static MD5 JoinMD5(MD5 md5, string salt)
        {
            return md5 + salt;
        }

        //public static bool IsEqualMD5(MD5 md5, byte[] inputBytes)
        //{
        //    return md5 == inputBytes;
        //}

        //public static bool IsNotEqualMD5(MD5 md5, byte[] inputBytes)
        //{
        //    return md5 != inputBytes;
        //}

        //public bool IsEqualMD5(byte[] inputBytes)
        //{
        //    return this == inputBytes;
        //}

        //public bool IsNotEqualMD5(byte[] inputBytes)
        //{
        //    return this != inputBytes;
        //}

        public void Dispose()
        {
            _md5Hash = null;
            _salt = null;
        }
        #endregion

        #region Operators
        public static MD5 operator +(MD5 md5, string salt)
        {
            return new MD5(md5._md5Hash, salt);
        }

        public static bool operator ==(MD5 md5, string input)
        {
            return md5.VerifyHash(input);
        }

        public static bool operator !=(MD5 md5, string input)
        {
            return !md5.VerifyHash(input);
        }
        #endregion
    }
}
