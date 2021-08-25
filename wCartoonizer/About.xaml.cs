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

namespace PrimaCartoonizer
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl, IPopupControl
    {
        MainWindow _parent;
        public About()
        {
            InitializeComponent();
        }

        public About(MainWindow parent)
        {
            InitializeComponent();
            _parent = parent;

        }
        public bool CloseApp
        {
            get;
            set;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _parent.HidePopup();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.UpdateUrl);
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.HomeUrl);
        }


        public bool ShowOpenDialogOnExit
        {
            get;
            set;
        }
    }
}
