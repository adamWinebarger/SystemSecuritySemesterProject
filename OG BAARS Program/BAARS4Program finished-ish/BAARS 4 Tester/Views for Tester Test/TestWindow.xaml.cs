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
     * This class allows the user to take the BAARS 4 test and then saves all their information 
     * into a text file in their directory
     * */
    public partial class TestWindow : Window
    {
        private string firstname, lastname, middlename = " ", gender, path;
        private int age;
        private string[] lines;
        private int[] answers;
        private TestType type = TestType.adult;

        private string questionPath = "BAARS4Questions";

        private int count, maxCount;


        public TestWindow(string firstname, string lastname, string middlename, string gender,
                string path, int age)
        {
            InitializeComponent();
            

            this.firstname = firstname;
            this.lastname = lastname;
            this.gender = gender;
            this.path = path;
            this.age = age;


            if (middlename != "" && middlename != null)
            {
                this.middlename = middlename;
            }

            MessageBox.Show(K.adultDescription, "Ok");
            startTest();
        }

        // Loads the next question and stores the current answer 
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (storeAnswer())
            {
                nextQuestion();
                updateMultiBackComboBox();
                checkButtonVisibility();
            }
            else
            {
                MessageBox.Show("Error! No button selected. Please select a valid value", "response");
            }
        }

        // Goes to the previous question and resets the radio button
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            count--;
            questionLabel.Text = lines[count];
            resetRadioButtons();
            checkButtonVisibility();
        }

        // Scores the test and closes the window
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            scoreTest();

            if (type == TestType.adult)
            {
                MessageBox.Show(K.youthDescription, "ok");
                type = TestType.youth;
                startTest();
            }
            else
            {
                
                Close();

            }
        }

        private void startTest()
        {
            string questionPath;

            backButton.Visibility = Visibility.Hidden;
            submitButton.Visibility = Visibility.Hidden;
            nextButton.Visibility = Visibility.Visible;
            count = 0;

            //Sets up test based on the TestType. 
            if (type == TestType.adult)
            {
                questionPath = this.questionPath + "\\Adult_Questions.txt";
                maxCount = 27;
            }
            else
            {
                questionPath = this.questionPath + "\\Child_Questions.txt";
                maxCount = 18;
            }

            lines = File.ReadAllLines(questionPath);
            answers = new int[maxCount];
            questionLabel.Text = lines[0];
        }

        // Displays the next question and resets the radio buttons
        void nextQuestion()
        {
            count++;
            questionLabel.Text = lines[count];
            //questionLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            //resetRadioButtons();
        }

        bool storeAnswer()
        {
            if ((bool)radioButton1.IsChecked)
            {
                answers[count] = 1;
            }
            else if ((bool)radioButton2.IsChecked)
            {
                answers[count] = 2;
            }
            else if ((bool)radioButton3.IsChecked)
            {
                answers[count] = 3;
            }
            else if ((bool)radioButton4.IsChecked)
            {
                answers[count] = 4;
            }
            else
            {
                return false;
            }

            return true;
        }

        //This toggles visibility of next, back, and submit buttons based on what answer a tester is on in the test
        void checkButtonVisibility()
        {
            if (count > 0)
            {
                backButton.Visibility = Visibility.Visible;
            }
            else
            {
                backButton.Visibility = Visibility.Hidden;
            }

            if (count == maxCount)
            {
                nextButton.Visibility = Visibility.Hidden;
                submitButton.Visibility = Visibility.Visible;
            }
            else
            {
                nextButton.Visibility = Visibility.Visible;
                submitButton.Visibility = Visibility.Hidden;
            }
        }

        void resetRadioButtons()
        {
            try
            {
                switch (answers[count])
                {
                    case 1:
                        radioButton1.IsChecked = true;
                        return;
                    case 2:
                        radioButton2.IsChecked = true;
                        return;
                    case 3:
                        radioButton3.IsChecked = true;
                        return;
                    case 4:
                        radioButton4.IsChecked = true;
                        return;
                    default:
                        radioButton1.IsChecked = false;
                        radioButton2.IsChecked = false;
                        radioButton3.IsChecked = false;
                        radioButton4.IsChecked = false;
                        return;
                }
            } catch
            {
                radioButton1.IsChecked = false;
                radioButton2.IsChecked = false;
                radioButton3.IsChecked = false;
                radioButton4.IsChecked = false;
            }
        }

        private void multiBackComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (multiBackComboBox.SelectedIndex != -1) 
            {
                count = Convert.ToInt32(multiBackComboBox.SelectedItem) - 1;
                //MessageBox.Show(count.ToString());
                questionLabel.Text = lines[count];
                //questionLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
                resetRadioButtons();
            }
        }

        void updateMultiBackComboBox()
        {
            

            if (count + 1 > multiBackComboBox.Items.Count)
            {
                multiBackComboBox.Items.Clear();

                for (int i = 0; i < answers.Length; i++)
                {
                    if (answers[i] == 1 || answers[i] == 2 || answers[i] == 3 || answers[i] == 4)
                    {
                        multiBackComboBox.Items.Add((i + 1).ToString());
                    }
                }
                if (count + 1 <= answers.Length)
                {
                    multiBackComboBox.Items.Add((count + 1).ToString());
                }
            }
            
        }

        void scoreTest()
        {
            ScoreBAARS score;

            if (type == TestType.adult)
            {
                score = new ScoreBAARSAdult(answers);
                multiBackComboBox.Items.Clear();
                writeAdultResults2TextFile((ScoreBAARSAdult)score);

            }
            else if (type == TestType.youth)
            {
                score = new ScoreBAARSYouth(answers);
                writeYouthResults2TextFile((ScoreBAARSYouth)score);
            }
            else
            {
                return;
            }

            saveAnswers2Textfile(score);
        }

        void saveAnswers2Textfile(ScoreBAARS score)
        {
            string answersTextfile;

            switch (type)
            {
                case TestType.adult:
                    answersTextfile = path + "\\Adult_Answers.txt";
                    break;
                case TestType.youth:
                    answersTextfile = path + "\\Youth_Answers.txt";
                    break;
                default:
                    answersTextfile = path + "\\???_Answers.txt";
                    break;
            }

            if (!File.Exists(answersTextfile))
            {
                using (StreamWriter writer = File.CreateText(answersTextfile))
                {
                    writer.WriteLine("Name: {0}, {1} {2}", lastname, firstname, middlename);
                    writer.WriteLine("Age: {0}    Gender: {1}", age.ToString(), gender);
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
        }

        void writeAdultResults2TextFile(ScoreBAARSAdult score)
        {
            string resultsFile = path + "\\adultResults.txt";

            using (StreamWriter writer = File.CreateText(resultsFile))
            {
                writer.WriteLine("Name: {0}, {1} {2}", lastname, firstname, middlename);
                writer.WriteLine("Age: {0}    Gender: {1}", age.ToString(), gender);
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

        void writeYouthResults2TextFile(ScoreBAARSYouth score)
        {
            string resultsFile = path + "\\childResults.txt";

            string[] inputValues = { "total1", "symptom1", "total2", "symptom2", "sumTotal", "sumSymptoms" },
                displayValues = { "Section 1 Total Score: ", "Section 1 Symptoms Count: ",
                    "Section 2 Total Score: ", "Section 2 Symptoms Count: ", "Sum of Sections 1-2 Total Score: ",
                    "Sum of Sections 1-2 Symptoms Count: "};

            using (StreamWriter writer = File.CreateText(resultsFile))
            {
                writer.WriteLine("Name: {0}, {1} {2}", lastname, firstname, middlename);
                writer.WriteLine("Age: {0}    Gender: {1}", age.ToString(), gender);
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
