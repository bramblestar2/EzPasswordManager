using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EzPasswordManager.Helpers
{
    static class TextEncryption
    {
        #region Key and IV

        private static byte[] IV = { 210, 29, 113, 228, 186, 195, 253, 152, 9, 63, 188, 36, 236, 100, 59, 98 };
        private static byte[] Key = { 11, 228, 250, 9, 33, 100, 137, 95, 173, 75, 170, 180, 233, 103, 227, 158,
                        225, 165, 215, 146, 170, 192, 142, 251, 64, 213, 222, 178, 148, 249, 36, 102 };

        #endregion

        static public string EncryptSHA(string plainText)
        {
            SHA256 sha = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            string hash = Convert.ToBase64String(sha.ComputeHash(bytes));
            
            return hash;
        }

        static public string EncryptAES(string plainText, string salt = "")
        {
            Aes aes = Aes.Create();
            aes.IV = IV;
            aes.Key = Key;

            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encrypted = aes.EncryptCbc(bytes, aes.IV);
            string encryptedText = Convert.ToBase64String(encrypted);

            return encryptedText;
        }

        static public string DecryptAES(string plainText, string salt = "")
        {
            Aes aes = Aes.Create();
            aes.IV = IV;
            aes.Key = Key;

            byte[] encryptedText = Convert.FromBase64String(plainText);
            byte[] decrypted = aes.DecryptCbc(encryptedText, aes.IV);
            string decryptedText = Encoding.UTF8.GetString(decrypted);

            return decryptedText;
        }
    }
}
