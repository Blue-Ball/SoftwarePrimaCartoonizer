using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace PrimaCartoonizer
{
    /// <summary>
    /// Interaction logic for RegisterControl.xaml
    /// </summary>
    public partial class RegisterControl : UserControl, IPopupControl
    {
        private Rect defaultRect;
        private bool isMaximized = false;
        string getpassword;
        MainWindow _parent;

        public RegisterControl(MainWindow parent, String passname)
        {
            InitializeComponent();

            btnLink.Content = Properties.Settings.Default.BuyNowUrl;

            getpassword = passname;
            _parent = parent;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if( _parent.auth.GetSTATUS() == SoftActivate.Licensing.LICENSE_STATUS.Expired)
            {
                lblAppStatus.Text = "Your evaluation period has expired.";
                lblAppStatus.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
				lblAppStatus.Text = "Thank you for evaluating our software, your trial period will end soon.";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.BuyNowUrl);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (RegisterNow())
            {
                _parent.HidePopup();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(KeyGen.GenerateSerial());
            _parent.HidePopup();
        }



        private bool RegisterNow()
        {
				if (!_parent.auth.Registernow(textBox1.Text.Trim()))
                {
					return false;
                }
				_parent.auth.SaveKey();
				//=====================Bilal Pour Fixer le bug de l'activation qui se répète après la fin de la periode d'essai End========================
				MessageBox.Show("Software Successfully Activated!", "Activation", MessageBoxButton.OK, MessageBoxImage.Information);
                MessageBox.Show("The software will close to be fully activated!", "Activation Successed",
                       MessageBoxButton.OK, MessageBoxImage.Information);
                Environment.Exit(0);//Bilal pour fermer l'application après l'enregistrement
                return true;
        }


        private string SerialFromKey(int Key)
        {
            string serial = null;
            List<List<string>> lst = new List<List<string>>();
            string[] arr1 = {
		"A",
		"A",
		"B",
		"C",
		"C",
		"D",
		"E",
		"E",
		"F",
		"G",
		"G",
		"H",
		"I",
		"I",
		"J",
		"K",
		"K",
		"L",
		"M",
		"M",
		"N",
		"O",
		"O",
		"P",
		"Q",
		"Q",
		"R",
		"S",
		"S",
		"T",
		"U",
		"U",
		"V",
		"W",
		"W",
		"X",
		"Y",
		"Y",
		"Z",
		"0",
		"0",
		"1",
		"2",
		"2",
		"3",
		"4",
		"4",
		"5",
		"6",
		"6",
		"7",
		"8",
		"8",
		"9"
	};
            string[] arr2 = {
		"B",
		"C",
		"D",
		"F",
		"G",
		"H",
		"J",
		"K",
		"L",
		"M",
		"N",
		"P",
		"Q",
		"R",
		"S",
		"T",
		"V",
		"W",
		"X",
		"Y",
		"Z",
		"0",
		"1",
		"2",
		"3",
		"4",
		"5",
		"6",
		"7",
		"8",
		"9"
	};
            string[] arr3 = {
		"A",
		"E",
		"I",
		"O",
		"U",
		"0",
		"1",
		"2",
		"3",
		"4",
		"5",
		"6",
		"7",
		"8",
		"9",
		"0",
		"1",
		"2",
		"3",
		"4",
		"5",
		"6",
		"7",
		"8",
		"9",
		"0",
		"1",
		"2",
		"3",
		"4",
		"5",
		"6",
		"7",
		"8",
		"9"
	};
            string[] arr4 = {
		"0",
		"2",
		"4",
		"6",
		"8",
		"A",
		"C",
		"E",
		"G",
		"I",
		"K",
		"M",
		"O",
		"Q",
		"S",
		"U",
		"W",
		"Y"
	};
            string[] arr5 = {
		"0",
		"1",
		"2",
		"6",
		"7",
		"8",
		"A",
		"B",
		"C",
		"G",
		"H",
		"I",
		"M",
		"N",
		"O",
		"S",
		"T",
		"U",
		"Y",
		"Z"
	};
            string[] arr6 = {
		"L",
		"X",
		"M",
		"N",
		"T",
		"O",
		"P",
		"U",
		"V",
		"Q",
		"W",
		"A",
		"K",
		"0",
		"2",
		"4",
		"5",
		"6",
		"8",
		"9"
	};
            string[] arr7 = {
		"N",
		"T",
		"O",
		"S",
		"P",
		"R",
		"I",
		"E",
		"L",
		"X",
		"Q",
		"Z",
		"C",
		"B",
		"H",
		"8",
		"7",
		"2",
		"1",
		"6",
		"0",
		"3"
	};
            string[] arr8 = {
		"L",
		"S",
		"D",
		"M",
		"N",
		"O",
		"Q",
		"R",
		"S",
		"N",
		"V",
		"Q",
		"Y",
		"Q",
		"K",
		"X",
		"C",
		"A",
		"4",
		"5",
		"9",
		"2"
	};
            string[] arr9 = {
		"9",
		"7",
		"6",
		"4",
		"3",
		"1",
		"Q",
		"E",
		"T",
		"U",
		"O",
		"A",
		"S",
		"F",
		"H",
		"K",
		"Z",
		"C",
		"B",
		"M"
	};
            string[] arr10 = {
		"9",
		"8",
		"7",
		"3",
		"2",
		"1",
		"5",
		"5",
		"5",
		"W",
		"R",
		"Y",
		"I",
		"P",
		"S",
		"F",
		"H",
		"K",
		"Z",
		"C",
		"B",
		"M"
	};
            string[] arr11 = {
		"Q",
		"R",
		"I",
		"A",
		"F",
		"J",
		"Z",
		"V",
		"M",
		"E",
		"U",
		"P",
		"D",
		"H",
		"L",
		"C",
		"N",
		"0",
		"2",
		"3",
		"5",
		"7",
		"8"
	};
            string[] arr12 = {
		"Q",
		"A",
		"Z",
		"E",
		"D",
		"C",
		"T",
		"G",
		"B",
		"U",
		"J",
		"M",
		"O",
		"L",
		"7",
		"4",
		"5",
		"6",
		"3",
		"2"
	};
            string[] arr13 = {
		"Q",
		"A",
		"S",
		"E",
		"R",
		"F",
		"G",
		"H",
		"Y",
		"U",
		"J",
		"K",
		"I",
		"O",
		"L",
		"Z",
		"X",
		"S",
		"D",
		"F",
		"V",
		"B",
		"G",
		"H",
		"J",
		"M",
		"K",
		"9",
		"5",
		"1",
		"7",
		"5",
		"3"
	};
            string[] arr14 = {
		"Q",
		"W",
		"E",
		"T",
		"Y",
		"U",
		"O",
		"P",
		"A",
		"D",
		"F",
		"G",
		"J",
		"K",
		"L",
		"X",
		"C",
		"V",
		"N",
		"M",
		"1",
		"4",
		"6",
		"9"
	};
            string[] arr15 = {
		"Q",
		"A",
		"Z",
		"X",
		"C",
		"D",
		"E",
		"R",
		"T",
		"G",
		"B",
		"N",
		"M",
		"J",
		"U",
		"I",
		"O",
		"L",
		"1",
		"5",
		"4",
		"8",
		"6",
		"2",
		"3"
	};
            string[] arr16 = {
		"W",
		"E",
		"D",
		"C",
		"V",
		"B",
		"G",
		"T",
		"Y",
		"U",
		"J",
		"M",
		"K",
		"L",
		"O",
		"H",
		"F",
		"S",
		"1",
		"5",
		"4",
		"2",
		"3"
	};
            string[] arr17 = {
		"O",
		"I",
		"U",
		"T",
		"G",
		"B",
		"C",
		"X",
		"Z",
		"Q",
		"W",
		"E",
		"R",
		"F",
		"V",
		"B",
		"N",
		"M"
	};
            string[] arr18 = {
		"Q",
		"A",
		"Z",
		"X",
		"S",
		"D",
		"C",
		"V",
		"F",
		"R",
		"T",
		"G",
		"B",
		"N",
		"H",
		"J",
		"M",
		"K",
		"I",
		"O",
		"L",
		"P",
		"1",
		"3",
		"2",
		"5",
		"6",
		"7",
		"9"
	};
            string[] arr19 = {
		"L",
		"I",
		"A",
		"M",
		"I",
		"S",
		"C",
		"O",
		"O",
		"L",
		"I",
		"S",
		"N",
		"T",
		"H",
		"E",
		"3",
		"3",
		"3"
	};
            string[] arr20 = {
		"L",
		"S",
		"D",
		"I",
		"S",
		"C",
		"O",
		"O",
		"L",
		"S",
		"O",
		"I",
		"S",
		"C",
		"O",
		"C",
		"A",
		"I",
		"N",
		"E",
		"A",
		"N",
		"D",
		"P",
		"O",
		"T",
		"A",
		"N",
		"D",
		"M",
		"E",
		"T",
		"H"
	};

            //
            // Add arrays as lists
            //
            lst.Add(new List<string>(arr1));
            lst.Add(new List<string>(arr2));
            lst.Add(new List<string>(arr3));
            lst.Add(new List<string>(arr4));
            lst.Add(new List<string>(arr5));
            lst.Add(new List<string>(arr6));
            lst.Add(new List<string>(arr7));
            lst.Add(new List<string>(arr8));
            lst.Add(new List<string>(arr9));
            lst.Add(new List<string>(arr10));
            lst.Add(new List<string>(arr11));
            lst.Add(new List<string>(arr12));
            lst.Add(new List<string>(arr13));
            lst.Add(new List<string>(arr14));
            lst.Add(new List<string>(arr15));
            lst.Add(new List<string>(arr16));
            lst.Add(new List<string>(arr17));
            lst.Add(new List<string>(arr18));
            lst.Add(new List<string>(arr19));
            lst.Add(new List<string>(arr20));

            //
            // Initialize the random using Random(key)
            //
            Random r1 = new Random(Key);

            serial = CConvert.ToBase36(Key);
            // Append extra 0's if the key isn't already five characters long
            while (!(serial.Length == 5))
            {
                serial = "0" + serial;
            }

            //
            // Generate the key using the unique 'array' for each character.
            //
            int x = 0;
            while (!(serial.Length == 29))
            {
                x = serial.Length;
                // Use modulus to see if a hyphen ("-") belongs here
                if (x % 6 == 5)
                {
                    serial += "R"; // Bilal: la valeur à changer apres chaque 5 character
                }
                else
                {
                    serial += lst[x - (5 + (x + 1) / 6)][r1.Next(0, lst[x - (5 + (x + 1) / 6)].Count - 1)];
                }
            }

            return serial;
        }



        public bool CloseApp
        {
            get;
            set;
        }

        public bool ShowOpenDialogOnExit
        {
            get;
            set;
        }
    }
}

