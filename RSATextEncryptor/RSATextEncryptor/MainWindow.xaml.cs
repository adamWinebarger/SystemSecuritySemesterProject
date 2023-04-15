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

namespace RSATextEncryptor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RSAEncryptor rsa;
        HashStuff hashStuff;

        public MainWindow()
        {
            InitializeComponent();
            rsa = new RSAEncryptor("Keys\\PubKey.pem");
            hashStuff = new();

        }

        private void EncryptionButton_Click(object sender, RoutedEventArgs e)
        {
            string pt = plainTextTextBox.Text;
            string ct = rsa.Encrypt(pt);
            encryptedTextTextBlock.Text = ct;

        }

        private void decryptionButton_Click(object sender, RoutedEventArgs e)
        {
            string[] drives2Check = { "D", "E", "F", "G", "H", "I", " J" };
            bool foundKey = false;
            string privKeyPath = "";

            foreach (var drive in drives2Check)
            {
                if (File.Exists(drive + ":\\PrivKey.pem"))
                {
                    foundKey = true;
                    privKeyPath = drive + ":\\PrivKey.pem";
                }
            }

            bool hashesMatch = hashStuff.compareHashes(privKeyPath);

            reDecryptedTextTextBlock.Text = foundKey ? hashesMatch ?
                rsa.Decrypt(encryptedTextTextBlock.Text, privKeyPath) :
                "Incorrect Private Key" : "No Key Found";
        }

        
    }
}
