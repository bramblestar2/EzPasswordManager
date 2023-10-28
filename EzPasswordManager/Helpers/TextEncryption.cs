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
        static public string EncryptSHA(string plainText)
        {
            SHA256 sha = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            string hash = Convert.ToBase64String(sha.ComputeHash(bytes));
            
            return hash;
        }
    }
}
