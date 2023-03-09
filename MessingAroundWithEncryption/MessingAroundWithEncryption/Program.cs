// See https://aka.ms/new-console-template for more information
using MessingAroundWithEncryption;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace MessingAroundWithEncryption
{
    static class Program
    {
        static void Main()
        {
            //rsaTest();
            //new ECAlice();

            string testString = "This is a test string";

            string hashedGuy = HashDirectory.computeSHA256Hash(testString);
            Console.WriteLine(hashedGuy);
        }



        static void rsaTest()
        {
            try
            {
                RSACSPSample rsaSample = new();

                //need a UnicodeEncoder to convert between byte array and string
                UnicodeEncoding byteConverter = new();

                byte[] data2Encrypt = byteConverter.GetBytes("Hello World"),
                    encryptedData, decryptedData;

                //create new instance of RSACryptoServiceProvider to generate public/private key data
                using (RSACryptoServiceProvider RSA = new())
                {

                    //pass data to encrypt the pubKey information
                    //(using RSACryptoServiceProvider.ExportParameters(false)
                    //and a boolean flag specifying no OAEP padding
                    encryptedData = rsaSample.RSAEncrypt(data2Encrypt, RSA.ExportParameters(false), false);

                    //pass the data to decrypt, the private key information
                    //(using RSACryptoServiceProvider.ExportParameters(true),
                    //and a boolean flag specifying no OAEP padding
                    decryptedData = rsaSample.RSADecrypt(encryptedData, RSA.ExportParameters(true), false);

                    //Display the decrypted plaintext to the console
                    Console.WriteLine("Decrypted Plaintext: {0}", byteConverter.GetString(decryptedData));

                }
            }
            catch (ArgumentNullException)
            {
                //catchblock for if encryption doesn't succeed
                Console.WriteLine("Encryption failed.");
            }
        }
    }
}