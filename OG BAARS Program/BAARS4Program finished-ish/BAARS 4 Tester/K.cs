using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BAARS_4_Tester
{
    static class K
    {
        //Text blocks for the sections of the test. Shown as Messageboxes in TestWindow
        public const string adultDescription = "For the first section of this test, select the item that best describes your behavior" +
                 " DURING THE PAST 6 MONTHS.",
             youthDescription = "For the next section of this test, select the item that best describes your" +
                 " behavior when you were a child BETWEEN 5 AND 12 YEARS OF AGE.";

        public const string testerProfiles = "Tester_Profiles\\";

        public const string adultQuestions = "BAARS4Questions\\Adult_Questions.txt";
        public const string childQuestions = "BAARS4Questions\\Child_Questions.txt";

        public const string adultAnswers = "\\Adult_Answers.txt",
            adultResults = "\\adultResults.txt",
            childResults = "\\childResults.txt",
            childAnswers = "\\Youth_Answers.txt",
            testerInfo = "\\TesterInfo.txt";

        public const string quickScoreDialog = "Warning, this portion of the program is meant for administrators only\n" +
            "Are you sure you want to procceed?",
            quickScoreBadInput = "Error! It would seem that you're not authorized to be viewing this.";

        //private const string encoder = "8x/A?D(G+KbPeShVkYp3s6v9y$B&E)H@";

        public static byte[] privKey()
        {
            if (File.Exists("Key\\privKey.txt"))
            {
                string[] lines = File.ReadAllLines("Key\\privKey.txt");
                return Encoding.ASCII.GetBytes(lines[0]);
            }

            //Environment.Exit(0);
            return null;
        }

        public static byte[] pubKey()
        {
            string[] keyChecker = { @"D:/Key.txt", @"E:/Key.txt", @"F:/Key.txt", @"G:/Key.txt", @"H:/Key.txt"};

            foreach (string key in keyChecker)
            {
                if (File.Exists(key))
                {
                    string[] lines = File.ReadAllLines(key);
                    return Encoding.ASCII.GetBytes(lines[0]);

                }
            }

            return null;
        }
    }
}
