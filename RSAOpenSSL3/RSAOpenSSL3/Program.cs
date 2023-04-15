// See https://aka.ms/new-console-template for more information
using RSAOpenSSL3;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using static System.Console;


/*RSAEncryption rsa = new();

string str = "Hello World!";
string str2 = rsa.Encrypt(str);
string str3 = rsa.Decrypt(str2);

WriteLine("{0}\n\n{1}\n\n{2}\n", str, str2, str3);

WriteLine("{0}\n\n{1}\n", rsa.getPublicKey(), rsa.getPrivateKey()); */

/*using (StreamWriter write = File.CreateText("PrivateKey.xml"))
{
    write.WriteLine(rsa.getPrivateKey());
}

using (StreamWriter writer = File.CreateText("PublicKey.xml"))
{
    writer.WriteLine(rsa.getPublicKey());
}*/

/*var privKeyXMLStr = File.ReadAllText("PrivateKey.xml");

var privKeyStr = XElement.Parse(privKeyXMLStr);
var PKmod = privKeyStr.Element("Modulus").Value;
var pkExponent = privKeyStr.Element("Exponent").Value;

RSAParameters privatKey = new RSAParameters();
privatKey.Exponent = Encoding.ASCII.GetBytes(pkExponent);
privatKey.Modulus = Encoding.ASCII.GetBytes(PKmod);

//WriteLine(PKmod);

RSAParameters pubKey = new RSAParameters();
var pubKeySTR = XElement.Parse(File.ReadAllText("PublicKey.xml"));
var pubKeyMod = pubKeySTR.Element("Modulus").Value;
var pubKeyExponenet = pubKeySTR.Element("Exponent").Value;
pubKey.Exponent = Encoding.ASCII.GetBytes(pubKeyExponenet);
pubKey.Modulus = Encoding.ASCII.GetBytes(pubKeyMod);

RSAEncryption rsa = new(privatKey, pubKey);

string str = "This is a test string", str2 = rsa.Encrypt(str);//, str3 = rsa.Decrypt(str2);

WriteLine(str);
WriteLine(str2);

string str3 = rsa.Decrypt(str2);
WriteLine(str3);*/

RSAEncryption2 rsa = new("Keys\\PubKey.pem");

string str = "This is a test String";

string str2 = rsa.Encrypt(str);
WriteLine(str2);

string str3 = rsa.Decrypt(str2, "Keys\\PrivKey.pem");
WriteLine(str3);


