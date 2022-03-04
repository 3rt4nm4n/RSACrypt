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
        {
            Random random = new Random();
            int e;
            while (true) 
            {
                e = random.Next(n); //generates positive number that is less than n
                if (z % e != 0)
                {
                    return e;
                }
            }
        }
        public int GenD(int e, int z)
        {
            Random random = new Random();
            int d = random.Next(z, int.MaxValue); //since e*d mod z == 1, d must be greaeter than z so minimum value is z and maximum value is max value of int data type


        }
    }
}
