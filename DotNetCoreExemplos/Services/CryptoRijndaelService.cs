using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace DotNetCoreExemplos.Services
{
    public class CryptoRijndaelService
    {
        private readonly string key = "avMfwIi0VVmXnSaEv1twdivHWMYcDB4LM+B8vYzm/kg=";
        private readonly string IV = "7GwtrCDpXUFYeeJYIDK2xA==";

        public string GetEncryptPassword(string password)
        {
            byte[] encrypted;

            using Rijndael rijAlg = Rijndael.Create();
            rijAlg.Key = Convert.FromBase64String(key);
            rijAlg.IV = Convert.FromBase64String(IV);

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for encryption.
            using MemoryStream msEncrypt = new MemoryStream();
            using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
                //Write all data to the stream.
                swEncrypt.Write(password);
            }

            encrypted = msEncrypt.ToArray();

            // Return the encrypted bytes from the memory stream.
            return Convert.ToBase64String(encrypted);
        }

        public string GetDecryptPassword(string encryptPassword)
        {
            byte[] password = Convert.FromBase64String(encryptPassword);

            using Rijndael rijAlg = Rijndael.Create();
            rijAlg.Key = Convert.FromBase64String(key);
            rijAlg.IV = Convert.FromBase64String(IV);

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for decryption.
            using MemoryStream msDecrypt = new MemoryStream(password);
            using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new StreamReader(csDecrypt);


            // Read the decrypted bytes from the decrypting stream
            // and place them in a string.
            string decrypted = srDecrypt.ReadToEnd();

            return decrypted;
        }
    }
}
