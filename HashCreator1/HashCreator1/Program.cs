// See https://aka.ms/new-console-template for more information
using System;
using System.Security.Cryptography;

Console.WriteLine("Hello, World!");

string text2Hash = File.ReadAllText("Keys\\PrivKey.pem");
string hashedText;

using (var sha = SHA256.Create())
{
    byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text2Hash + "");
    byte[] hashBytes = sha.ComputeHash(textBytes);

    hashedText = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
}

using (StreamWriter writer = File.CreateText("PrivKeyHash.txt"))
{
    writer.WriteLine(hashedText);
}