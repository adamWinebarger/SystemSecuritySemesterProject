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
    /* 
     * Windows form to get the information of the tester before allowing them to take the test
     * */
    public partial class TesterInfoWindow : Window
    {
        private int age;
        private string firstName, middleName, lastName, gender;

        private string path = "Tester_Profiles";

        public TesterInfoWindow()
        {
            InitializeComponent();
           // TesterInfoWindow_Load();
        }


      //  IFirebaseConfig ifc = new FirebaseConfig()
        //{
          //  AuthSecret = "OxXQFMnimCdKgNOXdJerbIrODB9v4mIAfVrLq1dp",
            //BasePath = "https://baars-4-default-rtdb.firebaseio.com/"
        //};



        // Ensures all information is valid, creates directory for the tester & saves tester info to a file, then opens the test window
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if(!CheckRequiredFields())
            {
                return;
            }

            if (CheckRequiredFields())
            {
                CreateDirectory4Tester();
                CreateTesterInfoTextFile();

                TestType testType;

                if (age > 18)
                {
                    testType = TestType.adult;
                } else if (age <= 18)
                {
                    testType = TestType.youth;
                } else
                {
                    return; //~should be unreachable
                }

                new TestWindow(firstName, lastName, middleName, gender, path, age).Show();
                Close();
            }
        }

        // If Age isn't valid, show error and repromt for input
        private void AgeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!AgeIsValid())
            {
                MessageBox.Show("Error! Invalid Input Detected", "Response");
                ageTextBox.Text = null;
            }
        }

        // Sets other radio button to not checked
        private void MaleRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            femaleRadioButtton.IsChecked = false;
        }

        // Sets other radio button to not checked
        private void FemaleRadioButtton_Checked(object sender, RoutedEventArgs e)
        {
            maleRadioButton.IsChecked = false;
        }

        // Returns false if first name or last name are left blank, age isn't an int, and if radio buttons aren't checked
        private bool CheckRequiredFields()
        {
            if (firstNameTextBox.Text.Equals("") || lastNameTextbox.Text.Equals(""))
            {
                return false;
            }

            if (!int.TryParse(ageTextBox.Text, out age))
            {
                return false;
            }

            if ((bool)!maleRadioButton.IsChecked && (bool)!femaleRadioButtton.IsChecked)
            {
                return false;
            }

            return true;
        }

        // Returns true if Age is an INT, not blank, and not null
        private bool AgeIsValid()
        {
            if (!int.TryParse(ageTextBox.Text, out age) && ageTextBox.Text != "" && ageTextBox.Text != null)
            {
                return false;
            }

            return true;
        }

        // Creates the directory for all the users information and scores
        private void CreateDirectory4Tester()
        {
            firstName = firstNameTextBox.Text;
            middleName = middleNameTextBox.Text;
            lastName = lastNameTextbox.Text;

            if (maleRadioButton.IsChecked.Equals(true))
            {
                gender = "male";
            } else if (femaleRadioButtton.IsChecked.Equals(true))
            {
                gender = "female";
            } else
            {
                gender = "???"; //should be unreachable code
            }

           

            string foldername = lastName + ", " + firstName;

            if (middleName != "" && middleName != null)
            {
                foldername +=  " " + middleName[0] + ".";
            }

            string encryptedFolderName = Encrypter.encryptString(foldername);

            string encryptedDirectoryPath = path + "\\" + encryptedFolderName;

            if (!Directory.Exists(encryptedDirectoryPath))
            {
                Directory.CreateDirectory(encryptedDirectoryPath).Attributes = FileAttributes.Directory | FileAttributes.Normal;

                path = encryptedDirectoryPath;
                return;
            } else
            {
                int duplicateCount = 2;

                while (Directory.Exists(encryptedDirectoryPath + " (" + duplicateCount.ToString() + ")"))
                {
                    duplicateCount++;
                }

                encryptedDirectoryPath += " (" + duplicateCount.ToString() + ")";
                Directory.CreateDirectory(encryptedDirectoryPath);
                path = encryptedDirectoryPath;
            }
        }

        // Writes all the tester information into a text file in the users directory
        private void CreateTesterInfoTextFile()
        {
            string testerInfoFilePath = path + "\\TesterInfo.txt";

            string[] info = {"Last Name: ", lastName, "First Name: ", firstName, "Middle Name: ",
                middleName, "Age: ", age.ToString(), "Gender: ", gender, "Path: ", path};

            if (!File.Exists(testerInfoFilePath))
            {
                using (StreamWriter writer = File.CreateText(testerInfoFilePath))
                {
                    for (int i = 0; i < info.Length; i += 2)
                    {
                        writer.WriteLine("{0}{1}", info[i], info[i + 1]);
                    }
                }
            }

            Encrypter.EncryptFile(testerInfoFilePath, K.privKey());
        }
    }
}
