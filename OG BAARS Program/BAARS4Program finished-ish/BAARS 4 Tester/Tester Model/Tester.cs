using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace BAARS_4_Tester
{
    public class Tester
    {
        // All info to display in table
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Path { get; set; }

        // Loads all the tester information
        public Tester(string path)
        {
            this.Path = path;

            if (File.Exists(path + "\\testerInfo.txt"))
            {
                //string[] testerInfo = File.ReadAllLines(path + "\\testerInfo.txt");
                string[] testerInfo = Encrypter.DecryptFile(path + "\\testerInfo.txt", K.pubKey());

                LastName = testerInfo[0].Substring(testerInfo[0].IndexOf(":") + 1);
                FirstName = testerInfo[1].Substring(testerInfo[1].IndexOf(":") + 1);
                MiddleName = testerInfo[2].Substring(testerInfo[2].IndexOf(":") + 1);
                Age = Int32.Parse(testerInfo[3].Substring(testerInfo[3].IndexOf(":") + 1));
                Gender = testerInfo[4].Substring(testerInfo[4].IndexOf(":") + 1);
            }
        }
    }
}
