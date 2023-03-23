using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace BAARS_4_Tester
{
    static class Encrypter
    {

        public static void EncryptFile(string filePath, byte[] key)
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

        public static void DecryptFile2(string filepath, byte[] key)
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

        public static string[] DecryptFile(string filepath, byte[] key)
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
