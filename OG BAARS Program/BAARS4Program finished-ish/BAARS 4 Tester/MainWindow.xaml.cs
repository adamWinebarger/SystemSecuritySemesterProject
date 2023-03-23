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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;

namespace BAARS_4_Tester
{
    /* 
     * Main Window for the BAARS 4 Tester
     * */

    public partial class MainWindow : Window
    {
        byte[] key = null;

        // Our two lists for the table
        List<Tester> names = new List<Tester>();
        List<Tester> unauthorized = new List<Tester>();
        List<Tester> filteredNames = new List<Tester>();

        // Runs on start of window
        public MainWindow()
        {
            InitializeComponent();
            QuickScoreButton.Content = "Quickscore for \nParent/Teacher Forms";
            
            if (K.pubKey() != null)
                LoadDataIntoTable();


            firstNameCheckBox.Visibility = Visibility.Hidden;
            middleNameCheckBox.Visibility = Visibility.Hidden;
            lastNameCheckBox.Visibility = Visibility.Hidden;
            firstNameCheckBox.IsChecked = true;
            middleNameCheckBox.IsChecked = true;
            lastNameCheckBox.IsChecked = true;
        }

        // Button to get user to take test, opens user information input window
        private void TakeTestButton_Click(object sender, RoutedEventArgs e)
        {
            new TesterInfoWindow().ShowDialog();

            //ReloadTable();
        }

        // Quickly calculate the score
        private void QuickScoreButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Tester currentSelected = filteredNames[Table.SelectedIndex];

                //MessageBox.Show(currentSelected.Path);
                if (MessageBox.Show(K.quickScoreDialog, "", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    bool authorized = false;
                    string verification = Encrypter.DecryptFile(currentSelected.Path + "\\testerInfo.txt", K.pubKey())[0];

                    
                    if (verification.Contains(currentSelected.LastName))
                        authorized = true;
                    

                    if (authorized)
                    {
                        new PTInfoWindow(currentSelected).Show();
                    } else
                    {
                        MessageBox.Show(K.quickScoreBadInput, "Unauthorized");
                    }


                } else
                {
                    //Nothing
                }


            }
            catch
            {
                MessageBox.Show("Nothing Selected");
            }
        }

        //Button to refresh datagrid. Mainly for testing purposes. May toggle visibility before final release
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ReloadTable();
            ReloadTable(); //Reload Table has to be called twice here in order to work... no idea why.
        }

        //Button for deleting directories. Mainly for development purposes. May be made invisible for final release
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            //var cellInfos = Table.SelectedItem;
            int rowNumber = Table.SelectedIndex;


            try
            {
                if (Directory.Exists(names.ElementAt(rowNumber).Path))
                {
                    if (MessageBox.Show("Warning! Deletion is permanent, are you sure you want to delete " +
                        "the file for" + names.ElementAt(rowNumber).FirstName + " " +
                        names.ElementAt(rowNumber).LastName + "?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Directory.Delete(names.ElementAt(rowNumber).Path, true);
                        names.RemoveAt(rowNumber);
                    } else
                    {
                        return;
                    }
                }
            } catch
            {
                MessageBox.Show("Nothing selected", null);
            }

            
            ReloadTable();
        }

        private void showResultsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Tester currentSelected = filteredNames[Table.SelectedIndex];
                new TesterAnswers(currentSelected).Show();
            }
            catch
            {
                MessageBox.Show("Nothing Selected");
            }
        }

        //Button for toggling advanced search functionality
        //This should make things relatively more simple/straightforward based on what we already have
        private void ToggleAdvancedSearchButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (!firstNameCheckBox.IsVisible)
            {
                firstNameCheckBox.Visibility = Visibility.Visible;
                middleNameCheckBox.Visibility = Visibility.Visible;
                lastNameCheckBox.Visibility = Visibility.Visible;
                ToggleAdvancedSearchButton.Content = "Basic Search";
            }
            else
            {
                firstNameCheckBox.Visibility = Visibility.Hidden;
                middleNameCheckBox.Visibility = Visibility.Hidden;
                lastNameCheckBox.Visibility = Visibility.Hidden;
                firstNameCheckBox.IsChecked = true;
                middleNameCheckBox.IsChecked = true;
                lastNameCheckBox.IsChecked = true;
                ToggleAdvancedSearchButton.Content = "Advanced Search Options";
            }
        }

        //This gives us a way to update the table any time new content is added
        private void ReloadTable()
        {
            //Table.ItemsSource = null; //In theory, this is unneccessary. But we'll leave it in for now anyways.
            if (K.pubKey() != null)
            {
                SearchTable(); //LoadData only sort of works so we needed to call search first.
                LoadDataIntoTable();
            }
            else
            {
                names.Clear();
                Table.ItemsSource = unauthorized;
            }
            
        }

        // Loads the names into the main window table
        private void LoadDataIntoTable()
        {

            names.Clear(); //clearing names here prevents duplicates from showing up on reload
            names = new List<Tester>();


            if (Directory.Exists("Tester_Profiles\\"))
            {
                //This should work for now, but could definitely use a bit of cleaning.
                //Might be worthwhile to have some sort of "on first entry" method that creates the Tester_Profiles
                // directory in mainwindow instead of creating it the first time somebody creates a new tester
                string[] files1 = Directory.GetDirectories("Tester_Profiles\\");

                int numOfPeople = files1.Length;

                for (int i = 0; i < numOfPeople; i++)
                {
                    names.Add(new Tester(files1[i]));
                    filteredNames.Add(new Tester(files1[i]));
                }

                names.Sort((x, y) => string.Compare(x.LastName, y.LastName));

                Table.IsReadOnly = true;
                Table.ItemsSource = names;
            }
        }

        // Runs upon automatically generating collumns used to formate table
        // https://docs.microsoft.com/en-us/windows/communitytoolkit/controls/datagrid_guidance/customize_autogenerated_columns
        private void Table_AutoGenerateColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            
            if (e.Column.Header.ToString() == "LastName")
            {
                e.Column.Width = 100;
            } 
            else if (e.Column.Header.ToString() == "MiddleName")
            {
                e.Column.Width = 100;
            }
            else if (e.Column.Header.ToString() == "FirstName")
            {
                e.Column.Width = 100;
            }
            else if (e.Column.Header.ToString() == "Age")
            {
                e.Column.Width = 30;
            }
            else if (e.Column.Header.ToString() == "Gender")
            {
                e.Column.Width = 50;
            }
            else if (e.Column.Header.ToString() == "Path")
            {
                e.Column.Width = 364;
            }
        }

        // Researches table everytime the text is changed
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReloadTable();
            SearchTable();
        }

        // Searches the table depending on first/middle/last name depending on user
        private void SearchTable()
        {
            filteredNames.Clear();

            if (SearchBox.Text.Equals(""))
            {
                filteredNames.AddRange(names);
            }
            else
            {
                if (firstNameCheckBox.IsChecked.GetValueOrDefault())
                {
                    foreach (Tester t in names)
                    {
                        if (t.FirstName.Contains(SearchBox.Text, StringComparison.InvariantCultureIgnoreCase) && !filteredNames.Contains(t))
                        {
                            filteredNames.Add(t);
                        }
                    }
                }
                if (middleNameCheckBox.IsChecked.GetValueOrDefault())
                {
                    foreach (Tester t in names)
                    {
                        if (t.MiddleName.Contains(SearchBox.Text, StringComparison.InvariantCultureIgnoreCase) && !filteredNames.Contains(t))
                        {
                            filteredNames.Add(t);
                        }
                    }
                }
                if (lastNameCheckBox.IsChecked.GetValueOrDefault())
                {
                    foreach (Tester t in names)
                    {
                        if (t.LastName.Contains(SearchBox.Text, StringComparison.InvariantCultureIgnoreCase) && !filteredNames.Contains(t))
                        {
                            filteredNames.Add(t);
                        }
                    }
                }
            }

            Table.ItemsSource = filteredNames.ToList();
        }

        // Researches table through first name
        private void FirstName_Checked(object sender, RoutedEventArgs e)
        {
            firstNameCheckBox.IsChecked = true;
            SearchTable();
        }

        // Researches table through middle name
        private void MiddleName_Checked(object sender, RoutedEventArgs e)
        {
            middleNameCheckBox.IsChecked = true;
            SearchTable();
        }

        // Researches table through last name
        private void LastName_Checked(object sender, RoutedEventArgs e)
        {
            lastNameCheckBox.IsChecked = true;
            SearchTable();
        }

        // Opens up new windoww with the testers answers 
        private void Table_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataGridRow row = sender as DataGridRow;
                new TesterAnswers(filteredNames[row.GetIndex()]).Show();
            }
            catch
            {

            }
        }
    }
}
