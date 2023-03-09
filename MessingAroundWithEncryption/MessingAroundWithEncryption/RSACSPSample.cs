using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace MessingAroundWithEncryption
{
    public  class RSACSPSample
    {
        public byte[] RSAEncrypt(byte[] data2Encrypt, RSAParameters RSAKeyInfo, bool doOEAPPadding)
        {
            try
            {
                byte[] encryptedData;

                //create new instance of RSACryptoServiceProvider
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This only needs
                    //toinclude the public key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Encrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    encryptedData = RSA.Encrypt(data2Encrypt, doOEAPPadding);
                }

                return encryptedData;
            }
            //Catch and display CryptographicException to console
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public byte[] RSADecrypt(byte[] data2Decrypt, RSAParameters rsaKeyInfo, bool doOAEPPadding)
        {
            try
            {
                byte[] decryptedData;

                //create new instance of RSACrpytoservice
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //import rsa key info. This needs to include private key information
                    RSA.ImportParameters(rsaKeyInfo);

                    //decrypt the passed byte array and specify OEAP padding
                    //only available on windows
                    decryptedData = RSA.Decrypt(data2Decrypt, doOAEPPadding);
                }
                return decryptedData;
            } 
            //catch and display cryptographic exception to the console
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

       
    }
}
