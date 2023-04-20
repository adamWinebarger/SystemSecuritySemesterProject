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
using System.ComponentModel;

namespace BAARS_4_Tester
{

    /// <summary>
    /// Interaction logic for TesterAnswers.xaml
    /// </summary>
    public partial class TesterAnswers : Window
    {
        static private string name; // For changing the name label have to keep what it orginally was
        private bool adultAnswersEncrypted = true, childAnswersEncrypted = true, adultResultsEncrypted = true, 
            childResultsEncrypted = true;
        private struct AnswersTable
        {
           
            public AnswersTable(string answer, string question)
            {
                Question = question;
                Answers = answer;
            }

            public string Question { get; set; }
            public string Answers { get; set; }
        }

        Tester t;
        //private AnswersTable[] tableData = new AnswersTable[25];
        
        public TesterAnswers(Tester t)
        {
            
            this.t = t;
            InitializeComponent();
            //getAnswers(K.adultAnswers);
            name = t.FirstName + " " + t.LastName;
            nameLabel.Text = name;
            //Closing += TesterAnswers.OnWindowClosing();
            //adultAnswersEncrypted = containsString(t.path + K.adultAnswers, t.lastName);
            //adultResultsEncrypted = containsString(t.path + K.adultResults, t.lastName);
            //childResultsEncrypted = containsString(t.path + K.adultResults, t.lastName);
            //childAnswersEncrypted = containsString(t.path + K.adultResults, t.lastName);
            //Closing += OnWindowClosing;
            resultsLabel.Content = "";
            resultsLabel_2.Content = "";
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            if (!adultAnswersEncrypted)
                Encrypter.EncryptFileRSA(t.Path + K.adultAnswers);
            if (!adultResultsEncrypted)
                Encrypter.EncryptFileRSA(t.Path + K.adultResults);
            if (!childAnswersEncrypted)
                Encrypter.EncryptFileRSA(t.Path + K.childAnswers);
            if (!childResultsEncrypted)
                Encrypter.EncryptFileRSA(t.Path + K.childResults);
        }

        private void showAdult_Click(object sender, RoutedEventArgs e)
        {
            nameLabel.Text = name;
            if (Encrypter.compareHashes(K.RSAPrivateKeyFilePath()))
            {
                nameLabel.Text += " | Adult Results";
                getAnswers(t.Path + K.adultAnswers, K.adultQuestions);
                resultsLabel.Content = loadAdultResults(t.Path + K.adultResults, 1);
                resultsLabel_2.Content = loadAdultResults(t.Path + K.adultResults, 2);
                adultAnswersEncrypted = false;
                adultResultsEncrypted = false;
            } else
            {
                
                Table.ItemsSource = null;
                resultsLabel.Content = "";
                MessageBox.Show("This is sensitive information and you should not be viewing it without proper authorization");
                Close();
            }
        }

        private void showChild_Click(object sender, RoutedEventArgs e)
        {
            nameLabel.Text = name;
            if (Encrypter.compareHashes(K.RSAPrivateKeyFilePath()))
            {
                nameLabel.Text += " | Child Results";
                getAnswers(t.Path + K.childAnswers, K.childQuestions);
                resultsLabel.Content = loadChildResults(t.Path + K.childResults, 1);
                resultsLabel_2.Content = loadChildResults(t.Path + K.childResults, 2);
                childAnswersEncrypted = false;
                childResultsEncrypted = false;
            } else
            {
                Table.ItemsSource = null;
                resultsLabel.Content = "";
                MessageBox.Show("This is sensitive information and you should not be viewing it without proper authorization");
                Close();
            }
        }

        private void showOther_Click(object sender, RoutedEventArgs e)
        {
            if (Encrypter.compareHashes(K.RSAPrivateKeyFilePath()))
            {
                OtherTestSelectionWindow oth = new OtherTestSelectionWindow(this.t.Path);
                oth.ShowDialog();
                string[] s = oth.otherSelected;
                if (s != null)
                {
                    if (s[2].Equals("Adult"))
                    {
                        getAnswers(s[1], K.adultQuestions);
                        resultsLabel.Content = loadAdultResults(s[0], 1);
                        resultsLabel_2.Content = loadAdultResults(s[0], 2);
                        nameLabel.Text = s[3];
                    }
                    else if (s[2].Equals("Youth"))
                    {
                        getAnswers(s[1], K.childQuestions);
                        resultsLabel.Content = loadChildResults(s[0], 1);
                        resultsLabel_2.Content = loadChildResults(s[0], 2);
                        nameLabel.Text = s[3];
                    }
                }
                else
                {
                    //MessageBox.Show("Nothing Here");
                }
            } else
            {
                Table.ItemsSource = null;
                resultsLabel.Content = "";
                MessageBox.Show("This is sensitive information and you should not be viewing it without proper authorization");
                Close();
            }
        }

        // This function will get the answer information from the users answers text file and the questions and load it into a table
        private void getAnswers(string answersPath, string testQuestionPath)
        {
            //if (answersPath.Equals(K.adultAnswers) && adultAnswersEncrypted)
                //Encrypter.DecryptFile(t.path + answersPath, K.key);

            //if (answersPath.Equals(K.childAnswers) && childAnswersEncrypted)
                //Encrypter.DecryptFile(t.path + answersPath, K.key);

            List<AnswersTable> answersList = new List<AnswersTable>();

            if (File.Exists(answersPath) && File.Exists(testQuestionPath))
            {
                //MessageBox.Show(" ");
                //string[] answers = File.ReadAllLines(t.path + answersPath);
                string[] answers = Encrypter.DecryptFileRSA(answersPath, K.RSAPrivateKeyFilePath());
                string[] questions = File.ReadAllLines(testQuestionPath);
                //tableData = new AnswersTable[27];

                for (int i = 0; i < answers.Length - 5; i++) // Starts at 4 to skip beginning of line
                {
                    //tableData[i].Answers = answers[i].Substring(answers[i].IndexOf(".") + 1).ToString();
                    //tableData[i].Quesetions = "LOLWHAT";

                    string answer = answers[i + 5].Substring(answers[i + 5].IndexOf(" ") + 1).ToString();
                    string question = questions[i].ToString();//answers[i + 5].Substring(0, answers[i + 5].IndexOf(".")).ToString();


                    answersList.Add(new AnswersTable(answer, question)); //This shouldn't work but it does... why?

                    //MessageBox.Show(tableData[i].Answers);
                }

                Table.IsReadOnly = true;
                Table.ItemsSource = answersList;

            }
        }

        private string loadAdultResults(string filePath, int whichLabel)
        {
            string results = "";

            if (adultResultsEncrypted)
                Encrypter.DecryptFileRSA(filePath, K.RSAPrivateKeyFilePath());


            if (File.Exists(filePath))
            {
                //string[] lines = File.ReadAllLines(filePath);
                string[] lines = Encrypter.DecryptFileRSA(filePath, K.RSAPrivateKeyFilePath());

                if (whichLabel == 1) // To use for the other label in the form kidna shit code but it works 
                {

                    for (int i = 5; i < 14; i++)
                    {
                        results += lines[i] + "\n";
                    }

                    return results;
                } else
                {
                    for (int j = 16; j < lines.Length; j++)
                    {
                        results += lines[j] + "\n";
                    }
                    return results;
                }
            }
            return results;
        }

        private string loadChildResults(string filePath, int whichLabel)
        {

            string results = "";

            if (File.Exists(filePath))
            {
                //string[] lines = File.ReadAllLines(filePath);
                string[] lines = Encrypter.DecryptFileRSA(filePath, K.RSAPrivateKeyFilePath());
                

                if (whichLabel == 1)
                {
                    for (int i = 5; i < 9; i++)
                    {
                        results += lines[i] + "\n";
                    }
                    return results;
                } else
                {
                    return lines[9] + "\n" + lines[10] + "\n";
                }
            }
            return results;
        }

        private bool containsString(string filepath, string str)
        {
            foreach (var line in File.ReadAllLines(filepath))
            {
                if (line.Contains(str))
                    return true;
            }

            return false;
        }

    }
}
