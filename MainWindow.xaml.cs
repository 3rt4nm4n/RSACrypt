﻿using System;
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

namespace RSACrypt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }
        public bool PrimeChk(int num) //checks if the number is prime or not
        {
            if (num <= 1) return false;
            if (num == 2) return true;
            if (num % 2 == 0) return false;
            int i;
            for(i = 3; i <= num-1; i++)
            {
                if (num % i == 0)
                    return false;
            }
            if (num == i)
                return true;
            return true;
        }
        public int GetN(int p, int q) //calculates n
        {
            int n = p * q;
            return n;
        }
        public int GetZ(int p, int q) //calculates z
        {
            int z = (p - 1) * (q - 1);
            return z;
        }
        public int GenE(int n, int z) //generates e with conditions: e < n, prime to z 
        {                               // relatively prime means that e and z will not have any other common divisor than 1
            Random random = new Random();
            int e;
            for(; ; ) 
            { 
                e = random.Next(n); //generates positive number that is less than n
                if (z % e != 0 && PrimeChk(e))            
                    return e;            
                else
                    continue;
            }

        }
        public int GenD(int e, int z) //generates d with condition: e*d mod z == 1
        {
            Random random = new Random();
            int d;
        for(; ; ) 
            {

                d = random.Next(z, int.MaxValue); //since e*d mod z == 1, d must be greater than z so minimum value is z and maximum value is max value of int data type
                if ((e * d) % z == 1)
                    return d;
                else
                    continue;

            }

        }

        private void CalculateNnZButton_Click(object sender, RoutedEventArgs e) //calculates n and z using GetN and GetZ functions
        {
            int p = Convert.ToInt32(FirstPrimeNumberTextBox.Text);
            int q = Convert.ToInt32(SecondPrimeNumberTextBox.Text);
            NTextBox.Text = GetN(p, q).ToString();
            ZTextBox.Text = GetZ(p, q).ToString();
        }

        private void CalculateKeysButton_Click(object sender, RoutedEventArgs e) //calculates the keys, will generate e if the textbox is empty
        {
            if (String.IsNullOrEmpty(ETextBox.Text))
                PubETextBox.Text = PublicETextbox.Text = ETextBox.Text = GenE(Convert.ToInt32(NTextBox.Text), Convert.ToInt32(ZTextBox.Text)).ToString();
            else
                PubETextBox.Text = PublicETextbox.Text = ETextBox.Text;
            PublicNTextbox.Text = PubNTextBox.Text = PrivateNTextbox.Text = PrivNTextBox.Text = NTextBox.Text ;
            PrivateDTextbox.Text = PrivDTextBox.Text = GenD(Convert.ToInt32(ETextBox.Text), Convert.ToInt32(ZTextBox.Text)).ToString();

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            FirstPrimeNumberTextBox.Text = SecondPrimeNumberTextBox.Text = "";
            NTextBox.Text = ZTextBox.Text = ETextBox.Text = "";
            PublicETextbox.Text = PublicNTextbox.Text = PrivateDTextbox.Text = PrivateNTextbox.Text = PubNTextBox.Text = PrivDTextBox.Text = PrivDTextBox.Text = PubETextBox.Text = PrivNTextBox.Text = "";
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CopyEncButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(PubETextBox.Text + "," + PubNTextBox.Text);
            Clipboard.SetDataObject(sb.ToString());
        }

        private void CopyDecButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(PrivDTextBox.Text + "," + PrivNTextBox.Text);
            Clipboard.SetDataObject(sb.ToString());
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            string origascii = "";
            string hexasc = "";
            string str = PlainTextBox.Text;
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            
            foreach (byte element in bytes)//Converts to ASCII
            {
                int c = 1;
                for ( int i = 0; i < Convert.ToInt32(PubETextBox.Text); i++)
                {
                    c = (c * element) % Convert.ToInt32(PubNTextBox.Text);
               
                }
                c = c % Convert.ToInt32(PubNTextBox.Text);
                hexasc += c.ToString("X4"); //We used the second hint
                origascii += c+" ";
            }
            //get encrypted ascii using keys
            CipherIntTextBox.Text = origascii;
            CipherHexTextBox.Text = hexasc;
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            string enctxt = EncryptedTextBox.Text;
            string dectxt = "";
        }

        private void ClearEncButton_Click(object sender, RoutedEventArgs e)
        {
            CipherIntTextBox.Text = CipherHexTextBox.Text = PlainTextBox.Text = "";
        }

        
    }
}
