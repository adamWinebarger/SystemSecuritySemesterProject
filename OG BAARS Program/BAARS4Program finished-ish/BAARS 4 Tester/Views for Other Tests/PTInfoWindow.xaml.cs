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
    /// Interaction logic for PTInfoWindow.xaml
    /// </summary>
    public partial class PTInfoWindow : Window
    {
        string path, ammendedPath, lastName;
        Tester tester;

        public PTInfoWindow(Tester testerInQuestion)
        {
            InitializeComponent();
            relationshipLabel2.Visibility = Visibility.Hidden;
            relationshipTextBox.Visibility = Visibility.Hidden;

            path = testerInQuestion.Path;
            lastName = testerInQuestion.LastName;
            tester = testerInQuestion;
            makeOtherTestsDirectory();


        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (relationshipComboBox.SelectedIndex == 3)
            {
                relationshipLabel2.Visibility = Visibility.Visible;
                relationshipTextBox.Visibility = Visibility.Visible;
            } else
            {
                relationshipLabel2.Visibility = Visibility.Hidden;
                relationshipTextBox.Visibility = Visibility.Hidden;
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (!allInputsValid())
            {
                MessageBox.Show("Invalid");
            } else
            {
                string lastName = lastNameTextbox.Text,
                    firstName = firstNameTextBox.Text,
                    relationship;

                if (relationshipComboBox.SelectedIndex == 3)
                    relationship = relationshipTextBox.Text;
                else
                    relationship = relationshipComboBox.Text;

                try
                {
                    string s = Encrypter.DecryptFile(path + "\\testerInfo.txt", K.pubKey())[0];

                    if (s.Contains(this.lastName))
                    {
                        //MessageBox.Show("fdoiagjfoig");
                        
                        new PTQuickScoreWindow(tester, ammendedPath, lastName, firstName, relationship).Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Error! Auth-key is incorrect!");
                    }

                } catch (NullReferenceException n)
                {
                    MessageBox.Show("Error! Key has been removed. Returning to main menu");
                    Close();
                }
            }
        }

        bool allInputsValid()
        {
            if (firstNameTextBox.Text.Equals(""))
                return false;
            if (lastNameTextbox.Text.Equals(""))
                return false;
            if (relationshipComboBox.SelectedItem == null)
                return false;
            if (relationshipComboBox.SelectedIndex == 3 && relationshipTextBox.Text.Equals(""))
                return false;

            return true;
        }

        void makeOtherTestsDirectory()
        {
            string otherTestsDirectory = "\\Other Tests";

            if (!Directory.Exists(path + otherTestsDirectory)) {
                Directory.CreateDirectory(path + otherTestsDirectory);
            }

            ammendedPath = path + otherTestsDirectory;
        }

    }
}
