using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MessingAroundWithEncryption
{
    public class ECBob
    {
        public byte[] bobPubKey;
        private byte[] bobPrivKey;

        public ECBob()
        {
            using (ECDiffieHellmanCng bob = new())
            {
                bob.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
                bob.HashAlgorithm = CngAlgorithm.Sha256;
                bobPubKey = bob.PublicKey.ToByteArray();
                bobPrivKey = bob.DeriveKeyMaterial(CngKey.Import(ECAlice.alicePubKey, CngKeyBlobFormat.EccPublicBlob));
            }
        }

        public void recieve(byte[] encryptedMessage, byte[] iv)
        {
            using (Aes aes = new AesCryptoServiceProvider())
            {
                aes.Key = bobPrivKey;
                aes.IV = iv;

                //now we decrypt the message
                using (MemoryStream plaintext = new())
                {
                    using (CryptoStream cs = new(plaintext, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(encryptedMessage, 0, encryptedMessage.Length);
                        cs.Close();
                        string message = Encoding.UTF8.GetString(plaintext.ToArray());
                        Console.WriteLine(message);
                    }
                }
            }
        }

    }
}
