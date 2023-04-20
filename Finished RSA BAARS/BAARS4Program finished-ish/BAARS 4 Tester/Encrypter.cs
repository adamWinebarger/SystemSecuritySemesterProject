using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Windows;

namespace BAARS_4_Tester
{
    static class Encrypter
    {

        private static string pubKeyFilePath = "Key\\pubKey.pem";

        public static string EncryptRSA(string pt)
        {
            RSACryptoServiceProvider rsacsp = new();
            rsacsp.ImportFromPem(File.ReadAllText(pubKeyFilePath));
            var data = Encoding.Unicode.GetBytes(pt);
            var ct = rsacsp.Encrypt(data, false);
            return Convert.ToBase64String(ct);
        }

        public static void EncryptFileRSA(string filepath)
        {
            string tempFileName = Path.GetTempFileName();
            string[] ptText = File.ReadAllLines(filepath), ctLines = new string[ptText.Length];
            for (int i = 0; i < ptText.Length; i++)
                ctLines[i] = EncryptRSA(ptText[i]);
            
            using (StreamWriter writer = File.CreateText(tempFileName))
            {
                foreach (var line in ctLines)
                    writer.WriteLine(line);
            }

            File.Delete(filepath);
            File.Move(tempFileName, filepath);
        }

        public static string DecryptRSA(string ct, string privateKeyFilePath)
        {
            RSACryptoServiceProvider rsacsp = new();
            var dataBytes = Convert.FromBase64String(ct);
            rsacsp.ImportFromPem(File.ReadAllText(privateKeyFilePath));
            var pt = rsacsp.Decrypt(dataBytes, false);
            return Encoding.Unicode.GetString(pt);
        }

        public static string[] DecryptFileRSA(string textFilePath,  string privateKeyFilePath)
        {
            string[] ctLines = File.ReadAllLines(textFilePath), ptLines = new string[ctLines.Length];

            for (int i = 0; i < ctLines.Length; i++)
                ptLines[i] = DecryptRSA(ctLines[i], privateKeyFilePath);

            return ptLines;
        }

        public static bool compareHashes(string filePath)
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
                    //MessageBox.Show(hashedText);
                    //MessageBox.Show(text2Hash);

                    return hashedText.Equals(K.RSAPrivateKeyHash);
                }
            }
            return false;
        }

        public static void AESEncryptFile(string filePath, byte[] key)
        {
            string tempFileName = Path.GetTempFileName();

            using (SymmetricAlgorithm cipher = Aes.Create())
            using (FileStream fileStream = File.OpenRead(filePath))
            using (FileStream tempFile = File.Create(tempFileName))
            {
                cipher.Key = key;
                //aes.IV will be automatically populated with secure random value
                byte[] iv = cipher.IV;

                //write marker header to identify how to read the file in the future
                tempFile.WriteByte(69);
                tempFile.WriteByte(74);
                tempFile.WriteByte(66);
                tempFile.WriteByte(65);
                tempFile.WriteByte(69);
                tempFile.WriteByte(83);

                tempFile.Write(iv, 0, iv.Length);

                using (var cryptoStream = new CryptoStream(tempFile, cipher.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    fileStream.CopyTo(cryptoStream);
                }
            }

            File.Delete(filePath);
            File.Move(tempFileName, filePath);

        }

        public static void AESDecryptFile2(string filepath, byte[] key)
        {
            string tempFileName = Path.GetTempFileName();

            using (SymmetricAlgorithm cipher = Aes.Create())
            using (FileStream fileStream = File.OpenRead(filepath))
            using (FileStream tempFile = File.Create(tempFileName))
            {
                cipher.Key = key;
                byte[] iv = new byte[cipher.BlockSize / 8];
                byte[] headerBytes = new byte[6];
                int remain = headerBytes.Length;

                while (remain != 0)
                {
                    int read = fileStream.Read(headerBytes, headerBytes.Length - remain, remain);

                    if (read == 0)
                        throw new EndOfStreamException();

                    remain -= read;

                }

                if (headerBytes[0] != 69 || headerBytes[1] != 74 || headerBytes[2] != 66 ||
                    headerBytes[3] != 65 || headerBytes[4] != 69 || headerBytes[5] != 83)
                    throw new InvalidOperationException();

                remain = iv.Length;

                while (remain != 0)
                {
                    int read = fileStream.Read(iv, iv.Length - remain, remain);

                    if (read == 0)
                        throw new EndOfStreamException();

                    remain -= read;
                }

                cipher.IV = iv;

                using (var cryptoStream = new CryptoStream(tempFile, cipher.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    fileStream.CopyTo(cryptoStream);
                }
            }

            File.Delete(filepath);
            File.Move(tempFileName, filepath);
        }

        public static string[] AESDecryptFile(string filepath, byte[] key)
        {

            try
            {
                string tempFileName = Path.GetTempFileName();

                using (SymmetricAlgorithm cipher = Aes.Create())
                using (FileStream fileStream = File.OpenRead(filepath))
                using (FileStream tempFile = File.Create(tempFileName))
                {
                    cipher.Key = key;
                    byte[] iv = new byte[cipher.BlockSize / 8];
                    byte[] headerBytes = new byte[6];
                    int remain = headerBytes.Length;

                    while (remain != 0)
                    {
                        int read = fileStream.Read(headerBytes, headerBytes.Length - remain, remain);

                        if (read == 0)
                            throw new EndOfStreamException();

                        remain -= read;

                    }

                    if (headerBytes[0] != 69 || headerBytes[1] != 74 || headerBytes[2] != 66 ||
                        headerBytes[3] != 65 || headerBytes[4] != 69 || headerBytes[5] != 83)
                        throw new InvalidOperationException();

                    remain = iv.Length;

                    while (remain != 0)
                    {
                        int read = fileStream.Read(iv, iv.Length - remain, remain);

                        if (read == 0)
                            throw new EndOfStreamException();

                        remain -= read;
                    }

                    cipher.IV = iv;

                    using (var cryptoStream = new CryptoStream(tempFile, cipher.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        fileStream.CopyTo(cryptoStream);
                    }
                }

                //File.Delete(filepath);
                //File.Move(tempFileName, filepath);
                string[] lines = File.ReadAllLines(tempFileName);
                File.Delete(tempFileName);
                return lines;
            } catch
            {
                return null;
            }
        }

        public static string encryptString(string plain)
        {
            byte[] b = ASCIIEncoding.ASCII.GetBytes(plain);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }

        public static string decryptString(string cipher)
        {
            //byte[] b = ASCIIEncoding.ASCII.GetBytes(cipher);
            byte[] decrypt = Convert.FromBase64String(cipher);
            string data = Encoding.UTF8.GetString(decrypt);
            return data;
        }

    }
}
