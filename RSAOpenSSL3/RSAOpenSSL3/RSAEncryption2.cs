using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RSAOpenSSL3
{
    public class RSAEncryption2
    {

        RSACryptoServiceProvider rsa;
        private string pubKeyPemFilePath;

        public RSAEncryption2(string pubKeyPemFilePath)
        {
            this.pubKeyPemFilePath = File.ReadAllText(pubKeyPemFilePath);
        }

        public string Encrypt(string pt)
        {
            rsa = new RSACryptoServiceProvider();
            rsa.ImportFromPem(pubKeyPemFilePath);
            var data = Encoding.Unicode.GetBytes(pt);
            var ct = rsa.Encrypt(data, false);
            return Convert.ToBase64String(ct);
        }

        public string Decrypt(string ct, string privateKeyFilePath)
        {
            rsa = new RSACryptoServiceProvider();
            var dataBytes = Convert.FromBase64String(ct);
            rsa.ImportFromPem(File.ReadAllText(privateKeyFilePath));
            var pt = rsa.Decrypt(dataBytes, false);
            return Encoding.Unicode.GetString(pt);
        }

    }
}
