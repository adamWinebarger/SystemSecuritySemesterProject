// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using static System.Console;

string str = "Hello World 2";

byte[] data2Encrypt = Encoding.ASCII.GetBytes(str), encryptedData, decryptedData;
//RSACryptoServiceProvider rsacsp = new(2048);
//RSAParameters rsaKeyInfo = new RSAParameters();
byte[] rsaPrivKey = Encoding.ASCII.GetBytes(File.ReadAllText("Keys\\PrivKey.txt"));
byte[] rsaPubKey = Encoding.ASCII.GetBytes(File.ReadAllText("Keys\\PubKey.txt"));
//rsacsp.ImportParameters(rsaKeyInfo);

WriteLine(Encoding.ASCII.GetString(rsaPrivKey));
WriteLine();
WriteLine(Encoding.ASCII.GetString(rsaPubKey));

//functions

//encryptedData = rsaEncrypt(data2Encrypt, rsacsp.ExportParameters(true), false);
//decryptedData = rsaDecrypt(encryptedData, rsacsp.ExportParameters(true), false);
//WriteLine(Encoding.ASCII.GetString(decryptedData));

//RSAParameters rsaKeyinfo = rsacsp.ExportParameters(true);

//WriteLine(Encoding.ASCII.GetString(rsaKeyInfo.D));
//WriteLine(Encoding.ASCII.GetString(rsaKeyInfo.Exponent));

//string privKey2 = Encoding.UTF8.GetString(rsaKeyInfo.D);

//WriteLine(privKey2);

byte[] rsaEncrypt2(byte[] data, byte[] pubKey)
{
    using (RSACryptoServiceProvider rsacsp = new(2048))
    {
        rsacsp.ImportRSAPublicKey(pubKey, out int _);
        return rsacsp.Encrypt(data, true);
    }
}

byte[] rsaDecrypt2(byte[] data, byte[] privKey)
{
    using (RSACryptoServiceProvider rsacsp = new())
    {
        rsacsp.ImportRSAPrivateKey(privKey, out int _);
        return rsacsp.Decrypt(data, true);
    }
}


encryptedData = rsaEncrypt2(data2Encrypt, rsaPubKey);
//decryptedData = rsaDecrypt2(encryptedData, rsaPrivKey);

//string str2 = Encoding.ASCII.GetString(decryptedData);
//WriteLine(str2);