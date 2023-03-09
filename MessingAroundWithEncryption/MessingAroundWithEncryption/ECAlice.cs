using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MessingAroundWithEncryption
{
    //So it loooks like this is our sender class. 
    public class ECAlice
    {
        public static byte[] alicePubKey;

        public ECAlice()
        {
            using (ECDiffieHellmanCng alice = new())
            {
                alice.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
                alice.HashAlgorithm = CngAlgorithm.Sha256;
                alicePubKey = alice.PublicKey.ToByteArray();
                ECBob bob = new();
                CngKey bobKey = CngKey.Import(bob.bobPubKey, CngKeyBlobFormat.EccPublicBlob);
                byte[] aliceKey = alice.DeriveKeyMaterial(bobKey);
                byte[] encryptedMessage = null;
                byte[] iv = null;
                send(aliceKey, "This is a test Message", out encryptedMessage, out iv);
                bob.recieve(encryptedMessage, iv);
            }
        }

        public void send(byte[] key, string secretMessage, out byte[] encryptedMessage, out byte[] iv)
        {
            using (Aes aes = new AesCryptoServiceProvider())
            {
                aes.Key = key;
                iv = aes.IV;

                //encrypt message
                using (MemoryStream ct = new())
                {
                    using (CryptoStream cs = new(ct, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] ptmsg = Encoding.UTF8.GetBytes(secretMessage);
                        cs.Write(ptmsg, 0, ptmsg.Length);
                        cs.Close();
                        encryptedMessage = ct.ToArray();
                    }
                }
            }
        }

    }
}
