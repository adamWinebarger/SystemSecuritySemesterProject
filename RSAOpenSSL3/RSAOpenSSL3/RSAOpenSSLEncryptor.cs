using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RSAOpenSSL3
{
    public class RSAOpenSSLEncryptor
    {
        RSA rsa = RSA.Create();

        public RSAOpenSSLEncryptor()
        {
            var rsaPem = File.ReadAllText("Keys\\PrivKey.pem");
            rsa.ImportFromPem(rsaPem);
        }

        
    }
}
