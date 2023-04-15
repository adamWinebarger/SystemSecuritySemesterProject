using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace RSAConsole2
{
    internal class BadMethods
    {
        RSACryptoServiceProvider rsacsp = new();

        public BadMethods()
        {

        }

        byte[] rsaEncrypt(byte[] data2Encrypt, RSAParameters rsaKeyInfo, bool doOEAPPadding)
        {
            try
            {
                byte[] encryptedData;

                rsacsp.ImportParameters(rsaKeyInfo);
                encryptedData = rsacsp.Encrypt(data2Encrypt, doOEAPPadding);

                return encryptedData;
            }
            catch (CryptographicException e)
            {
                WriteLine(e.Message);
                return Encoding.ASCII.GetBytes("Encryption Failed");
            }
        }

        byte[] rsaDecrypt(byte[] data2Decrypt, RSAParameters rsaKeyInfo, bool doOEAPPadding)
        {

            try
            {
                byte[] decryptedData;

                rsacsp.ImportParameters(rsaKeyInfo);
                decryptedData = rsacsp.Decrypt(data2Decrypt, doOEAPPadding);

                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return Encoding.ASCII.GetBytes("Decryption Failed");
            }
        }
    }
}
