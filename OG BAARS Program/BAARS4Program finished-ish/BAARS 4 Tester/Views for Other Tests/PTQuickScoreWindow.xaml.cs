using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace BAARS_4_Tester
{
    /// <summary>
    /// Interaction logic for PTQuickScoreWindow.xaml
    /// </summary>
    public partial class PTQuickScoreWindow : Window
    {
        int adultCount = 0, childCount = 0;
        int[] adultAnswers = new int[27], childAnswers = new int[18];
        string otherTestPath, otherLast, otherFirst, relationship;

        public PTQuickScoreWindow(Tester testerInQuestion, string othersTestPath, string otherLast, 
            string otherFirst, string relationship)
        {
            InitializeComponent();
            this.otherTestPath = othersTestPath;
            this.otherLast = otherLast;
            this.otherFirst = otherFirst;
            this.relationship = relationship;
            adultQuestionLabel.Content = "1.";
            childQuestionLabel.Content = "1.";
            createRelationalDirectory();
        }

        private void adultAnswersInputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch (adultAnswersInputTextBox.Text)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                    adultAnswers[adultCount] = Convert.ToInt32(adultAnswersInputTextBox.Text);
                    if (adultCount < adultAnswers.Length - 1)
                    {
                        adultCount++;
                        updateQuestionNumberLabel();
                    } else
                    {
                        ScoreBAARSAdult sba = new ScoreBAARSAdult(adultAnswers);
                        createAnswersTextfile(sba, TestType.adult);
                        createAdultResultsTextFile(sba);
                    }
                    
                    break;
                case "x":
                case "b":
                    if (adultCount > 0)
                    {
                        adultCount--;
                        updateQuestionNumberLabel();
                    }
                    break;
                case "":
                    break;
                default:
                    MessageBox.Show("Error! Invalid Input detected.");
                    break;

            }
            adultAnswersInputTextBox.Text = "";
        }

        private void childAnswersInputTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch (childAnswersInputTextbox.Text)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                    childAnswers[childCount] = Convert.ToInt32(childAnswersInputTextbox.Text);
                    if (childCount < childAnswers.Length - 1)
                    {
                        childCount++;
                        updateQuestionNumberLabel();
                    }
                    else
                    {
                        ScoreBAARSYouth sby = new ScoreBAARSYouth(childAnswers);
                        createAnswersTextfile(sby, TestType.youth);
                        createChildResultsTextFile(sby);
                    }

                    break;
                case "x":
                case "b":
                    if (childCount > 0)
                    {
                        childCount--;
                        updateQuestionNumberLabel();
                    }
                    break;
                case "":
                    break;
                default:
                    MessageBox.Show("Error! Invalid Input detected.");
                    break;

            }
            childAnswersInputTextbox.Text = "";
        }

        private void finishButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        void updateQuestionNumberLabel()
        {
            adultQuestionLabel.Content = (adultCount + 1).ToString() + ".";
            childQuestionLabel.Content = (childCount + 1).ToString() + ".";
        }

        void createRelationalDirectory()
        {
            string encryptedRelation = Encrypter.encryptString(otherLast + ", " + otherFirst);

            if (!Directory.Exists(otherTestPath + "\\" + encryptedRelation))
            {
                Directory.CreateDirectory(otherTestPath + "\\" + encryptedRelation);
            } else
            {
                int duplicateCount = 2;

                while (Directory.Exists(otherTestPath + "\\" + 
                    Encrypter.encryptString(otherLast + ", " + otherFirst + "(" + duplicateCount.ToString() +
                    ")"))) 
                {
                    duplicateCount++;
                }

                encryptedRelation = Encrypter.encryptString(otherLast + ", " + otherFirst + "(" + 
                    duplicateCount.ToString() + ")");

                Directory.CreateDirectory(otherTestPath + "\\" + encryptedRelation);
            }
            otherTestPath += "\\" + encryptedRelation;
            //File.Create(otherTestPath + "\\Test.txt");
        }

        void createAnswersTextfile(ScoreBAARS score, TestType type)
        {
            string answersTextfile;
            string header;
            int[] answers;

            switch (type)
            {
                case TestType.adult:
                    answersTextfile = otherTestPath + "\\Adult_Answers.txt";
                    header = "Adult Answers from: ";
                    answers = adultAnswers;
                    break;
                case TestType.youth:
                    answersTextfile = otherTestPath + "\\Youth_Answers.txt";
                    header = "Youth Answers from: ";
                    answers = childAnswers;
                    break;
                default:
                    answersTextfile = otherTestPath + "\\???_Answers.txt";
                    header = "??? Answers from: ";
                    answers = childAnswers;
                    break;
            }

            using (StreamWriter writer = File.CreateText(answersTextfile))
            {
                writer.WriteLine("{0} {1}, {2} ({3})", header, otherLast, otherFirst, relationship);
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("Answers: ");

                for (int i = 0; i < answers.Length; i++)
                {
                    writer.WriteLine((i + 1).ToString() + ". " + answers[i].ToString());
                }

            }

            Encrypter.EncryptFile(answersTextfile, K.privKey());
        }

        void createAdultResultsTextFile(ScoreBAARSAdult score)
        {
            string resultsFile = otherTestPath + "\\adultResults.txt";
            string header = "Adult Answers from: ";

            using (StreamWriter writer = File.CreateText(resultsFile))
            {
                writer.WriteLine("{0} {1}, {2} ({3})", header, otherLast, otherFirst, relationship);
                writer.WriteLine("");
                writer.WriteLine("Adult results");
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("Section Results: ");

                for (int i = 1; i <= 4; i++)
                {
                    writer.WriteLine("Section {0} raw score: {1}", i.ToString(),
                        score.GetSectionTotal(i).ToString());
                    writer.WriteLine("Section {0} symptoms count: {1}", i.ToString(),
                        score.GetSymptomTotal(i));
                }

                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("Sum of raw scores section 1 thru 3: " + score.getOther("total1thru3").ToString());
                writer.WriteLine("Section 1 Symptoms count: " + score.GetSymptomTotal(1).ToString());
                writer.WriteLine("Sum  of sections 2 and 3 symptoms count: " + score.getOther("symptom23").ToString());
                writer.WriteLine("Total ADHD Symptoms count (1-3): " + score.getOther("symptom13").ToString());
                writer.WriteLine("SCT Symptoms Count: " + score.GetSymptomTotal(4).ToString());

            }
            Encrypter.EncryptFile(resultsFile, K.privKey());
        }

        void createChildResultsTextFile(ScoreBAARSYouth score)
        {
            string resultsFile = otherTestPath + "\\childResults.txt";
            string header = "Youth Answers from: ";

            string[] inputValues = { "total1", "symptom1", "total2", "symptom2", "sumTotal", "sumSymptoms" },
                displayValues = { "Section 1 Total Score: ", "Section 1 Symptoms Count: ",
                    "Section 2 Total Score: ", "Section 2 Symptoms Count: ", "Sum of Sections 1-2 Total Score: ",
                    "Sum of Sections 1-2 Symptoms Count: "};

            if (!File.Exists(resultsFile))
            {
                using (StreamWriter writer = File.CreateText(resultsFile))
                {
                    writer.WriteLine("{0} {1}, {2} ({3})", header, otherLast, otherFirst, relationship);
                    writer.WriteLine("");
                    writer.WriteLine("Child results");
                    writer.WriteLine("");
                    writer.WriteLine("");
                    ///writer.WriteLine("Section Results: ");

                    for (int i = 0; i < inputValues.Length; i++)
                    {
                        writer.WriteLine("{0}{1}", displayValues[i], score.GetValue(inputValues[i]).ToString());
                    }

                }

                Encrypter.EncryptFile(resultsFile, K.privKey());
            }
        }
    }
}
