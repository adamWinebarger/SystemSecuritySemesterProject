using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RSATextEncryptor
{
    public class RSAEncryptor
    {
        RSACryptoServiceProvider? rsa = null;
        private string pubKeyPemFilePath;

        public RSAEncryptor(string pubKeyPemFilePath)
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
