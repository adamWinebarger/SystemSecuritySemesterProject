using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Windows;

namespace RSATextEncryptor
{
    public class HashStuff
    {

        private string privKeyHashSignature;

        public HashStuff()
        {
            this.privKeyHashSignature = File.ReadAllText("PrivKeyHash.txt");
        }

        public bool compareHashes(string filePath)
        {
            if (File.Exists(filePath))
            {
                string text2Hash = File.ReadAllText(filePath);

                using (var sha = SHA256.Create())
                {
                    //MessageBox.Show("Hashing Started");
                    byte[] textBytes = Encoding.UTF8.GetBytes(text2Hash + "");
                    byte[] hashBytes = sha.ComputeHash(textBytes);

                    string hashedText = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                    //MessageBox.Show(String.Format("{0}\n{1}\n", hashedText, privKeyHashSignature));
                    //MessageBox.Show(String.Format("{0}\n{1}\n", hashedText.Length, privKeyHashSignature.Length));

                    return hashedText.Equals(privKeyHashSignature);
                }
            }
            return false;
        }


    }
}
