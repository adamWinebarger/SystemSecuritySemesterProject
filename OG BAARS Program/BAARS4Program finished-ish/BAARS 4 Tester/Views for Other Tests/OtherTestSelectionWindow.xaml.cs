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
    /// Interaction logic for OtherTestSelectionWindow.xaml
    /// </summary>
    public partial class OtherTestSelectionWindow : Window
    {
        private struct othersTableCell
        {
            public string Description { get; private set; }
            public string Path { get; private set; }

            public othersTableCell(string Description, string Path)
            {
                this.Description = Description;
                this.Path = Path;
            }
        }

        List<othersTableCell> others = new List<othersTableCell>();

        string path;
        public string[] otherSelected { get; private set; }

        public OtherTestSelectionWindow(string path)
        {
            InitializeComponent();
            this.path = path + "\\Other Tests";
            loadDataIntoTable();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            //Some stuff
            if (othersTable.SelectedIndex != -1)
            {
                populateOtherSelected();
                Close();
            }
            else
                MessageBox.Show("Nothing Selected");
        }



        void loadDataIntoTable()
        {
            //others.Clear();

            if (Directory.Exists(path)) {
                string[] directories = Directory.GetDirectories(path);

                for (int i = 0; i < directories.Length; i++)
                {
                    
                    string otherDirectoryPath = directories[i];
                    //MessageBox.Show(otherDirectoryPath);
                    string adultAnswersPath = otherDirectoryPath + "\\Adult_Answers.txt",
                        adultResultsPath = otherDirectoryPath + "\\adultResults.txt",
                        childAnswersPath = otherDirectoryPath + "\\Youth_Answers.txt",
                        childResultsPath = otherDirectoryPath + "\\childResults.txt";

                    if (File.Exists(adultAnswersPath) && File.Exists(adultResultsPath))
                    {
                        //string[][] decryptedAdultInfo = {Encrypter.DecryptFile(adultResultsPath, K.pubKey()),
                        //Encrypter.DecryptFile(adultAnswersPath, K.pubKey())};
                        //MessageBox.Show("Adult Exists");
                        try
                        {
                            string adultResults = Encrypter.DecryptFile(adultResultsPath, K.pubKey())[0];
                            others.Add(new othersTableCell(adultResults, otherDirectoryPath));
                        } catch (NullReferenceException n)
                        {
                            MessageBox.Show("Error!");
                        }
                    }

                    if (File.Exists(childAnswersPath) && File.Exists(childResultsPath))
                    {
                        //MessageBox.Show("Child Exists");
                        try
                        {
                            string childResults = Encrypter.DecryptFile(childResultsPath, K.pubKey())[0];                          
                            others.Add(new othersTableCell(childResults, otherDirectoryPath));
                        } catch (NullReferenceException n)
                        {
                            MessageBox.Show("Error!");
                        }
                    }
                    //others.Add(otherDirectoryPath.ToString());
                }

                othersTable.IsReadOnly = true;
                othersTable.ItemsSource = others;
            }
        }

        private void othersTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            populateOtherSelected();
        }

        protected void Table_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            populateOtherSelected();
            Close();
        }


        void populateOtherSelected()
        {
            string currentSelectedPath = others[othersTable.SelectedIndex].Path;
            string typ = others[othersTable.SelectedIndex].Description;
            typ = typ.Substring(0, typ.IndexOf(" "));

            if (typ.Equals("Adult"))
            {
                string[] s =
                {
                    currentSelectedPath + K.adultResults,
                    currentSelectedPath + K.adultAnswers,
                    "Adult",
                    Encrypter.DecryptFile(currentSelectedPath + K.adultResults, K.pubKey())[0]
                };
                otherSelected = s;
            }
            else if (typ.Equals("Youth"))
            {
                string[] s =
                {
                    currentSelectedPath + K.childResults,
                    currentSelectedPath + K.childAnswers,
                    "Youth",
                    Encrypter.DecryptFile(currentSelectedPath + K.childResults, K.pubKey())[0]
                };
                otherSelected = s;
            }
        }
    }
    
}
