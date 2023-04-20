using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RSATextEncryptor
{
    public class ECDSAEncrypter
    {
        private string pubkeyPemFilePath;

        public ECDSAEncrypter(string pubKeyPemFilePath)
        {
            this.pubkeyPemFilePath = pubKeyPemFilePath;
        }

        /*public string Encrypt(string pt)
        {
            var ecdsa = ECDsa.Create();
            ecdsa.ImportFromPem(File.ReadAllText(pubkeyPemFilePath));
            ecdsa.
        }*/

    }
}
