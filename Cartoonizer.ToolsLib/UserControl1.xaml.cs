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

namespace Schirmfoto.ToolsLib
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public TextWrapping TextWrapping
        {
            get { return textBox.TextWrapping; }
            set { textBox.TextWrapping = value; }
        }

        public bool AcceptsReturn
        {
            get { return textBox.AcceptsReturn; }
            set { textBox.AcceptsReturn = value; }
        }

        public TextBox TextBox
        {
            get { return textBox; }
        }

        public string Text
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }

        public UserControl1()
        {
            InitializeComponent();
        }
    }
}
