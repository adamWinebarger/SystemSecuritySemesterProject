using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RSAOpenSSL3
{
    public class RSAEncryption
    {
        private static RSACryptoServiceProvider rsacsp = new(2048);
        public RSAParameters _privateKey;
        public RSAParameters _publicKey;
        
        public RSAEncryption()
        {
            _privateKey = rsacsp.ExportParameters(true);
            _publicKey = rsacsp.ExportParameters(false);
        }

        public RSAEncryption(RSAParameters privateKey, RSAParameters publicKey)
        {
            _privateKey = privateKey;
            _publicKey = publicKey;
        }

        public string getPublicKey()
        {
            StringWriter sw = new();
            XmlSerializer xs = new(typeof(RSAParameters));
            xs.Serialize(sw, _publicKey);
            return sw.ToString();
        }

        public string getPrivateKey()
        {
            StringWriter sw = new();
            XmlSerializer xs = new(typeof(RSAParameters));
            xs.Serialize(sw, _privateKey);
            return sw.ToString();
        }

        public string Encrypt(string pt)
        {
            //rsacsp = new RSACryptoServiceProvider();
            rsacsp.ImportParameters(_publicKey);
            var data = Encoding.Unicode.GetBytes(pt);
            var ct = rsacsp.Encrypt(data, false);
            return Convert.ToBase64String(ct);
        }

        public string Decrypt(string ct)
        {
            var dataBytes = Convert.FromBase64String(ct);
            rsacsp.ImportParameters(_privateKey);
            var pt = rsacsp.Decrypt(dataBytes, false);
            return Encoding.Unicode.GetString(pt);
        }

    }
}
