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
using System.Windows.Forms;
using SoftActivate.Licensing;

namespace PrimaCartoonizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private Rect defaultRect;
        private bool isMaximized = false;

        private IPopupControl visiblePopup;

        public Auth auth { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            auth = new Auth();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void btnClosePopup_Click(object sender, RoutedEventArgs e)
        {
            HidePopup();
        }

        public void HidePopup()
        {
            if (visiblePopup != null)
            {
                if (visiblePopup.CloseApp)
                    this.Close();
            }


            popupControl.Visibility = System.Windows.Visibility.Collapsed;

            if (auth.GetSTATUS() == LICENSE_STATUS.Expired)
            {
                this.Close();
            }
            if (visiblePopup != null)
            {
                if (visiblePopup.ShowOpenDialogOnExit)
                    mainControl.ShowOpenDialog();
            }
        }

        public void ShowPopup(IPopupControl control)
        {
            visiblePopup = control;
            mainPopupGrid.Children.Clear();
            mainPopupGrid.Children.Add((UIElement)control);

            popupControl.Visibility = System.Windows.Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*if (Secure.Instance.IsRegistered == ApplicationStat.Trial || Secure.Instance.IsRegistered == ApplicationStat.Expired)
            {
                RegisterControl register = new RegisterControl(this, string.Empty) { ShowOpenDialogOnExit = true };
                ShowPopup(register);
            }
            if (Secure.Instance.IsRegistered == ApplicationStat.Registered)
            {
                btnBuynow.Visibility = Visibility.Hidden; // Cacher le bouton buy now quand c'est enregistrÃ© !
            }*/

            if (auth.Activate())
            {
                btnBuynow.Visibility = Visibility.Hidden;
            }
            else
            {
                RegisterControl register = new RegisterControl(this, string.Empty) { ShowOpenDialogOnExit = true };
                ShowPopup(register);
            }
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (!isMaximized)
            {
                defaultRect = new Rect(this.Left, this.Top, this.RestoreBounds.Width, this.RestoreBounds.Height);
                mainBorder.Margin = new Thickness(0);
                Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
                Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
                Left = 0;
                Top = 0;
                btnRestoreSize.Visibility = System.Windows.Visibility.Visible;
                btnMaximize.Visibility = System.Windows.Visibility.Collapsed;

                isMaximized = true;
            }
            else
            {
                //mainBorder.Margin = new Thickness(20);
                mainBorder.Margin = new Thickness(0);
                Width = defaultRect.Width;
                Height = defaultRect.Height;
                Left = defaultRect.Left;
                Top = defaultRect.Top;

                btnRestoreSize.Visibility = System.Windows.Visibility.Collapsed;
                btnMaximize.Visibility = System.Windows.Visibility.Visible;

                isMaximized = false;
            }
        }

        private void btnBuynow_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.BuyNowUrl);
        }


        public void SetProgressBarVisibility(System.Windows.Visibility visibility, string message, bool showRegWindow)
        {
            mainPopupGrid.Children.Clear();
            if (message == null)
            {
                borderEndMessage.Visibility = System.Windows.Visibility.Collapsed;
                popupProgress.Visibility = visibility;
                loadingAnimation.Visibility = visibility;
                loadingAnimation.BeginAnimation();
                /*
                int Count1 = Directory.GetFiles(CartoonizeHelper.path).Count();
                if (Count1 == 0) //Bilal c'est pour éviter l'erreur de la division par zero
                {
                    Count1 = 1;
                }
                //int Count1 = 5;
                int PourcentageImagesExtraites = Count1 * 100 / 11;
                //progressbartext.Visibility = System.Windows.Visibility.Visible;
                progressbartext.Visibility = visibility;
                progressbartext.Text = "\r\n In progress...\r\n Please wait..." + PourcentageImagesExtraites.ToString() + "%\r\n";
                */
            }
            else
            {
                popupProgress.Visibility = System.Windows.Visibility.Visible;
                loadingAnimation.Visibility = System.Windows.Visibility.Collapsed;
                borderEndMessage.Visibility = System.Windows.Visibility.Visible;
                txtEndMessage.Text = message;

                Timer t = new Timer();
                t.Tag = showRegWindow;
                t.Interval = 2 * 1000;
                t.Tick += new EventHandler(t_Tick);
                t.Start();
            }
        }

        void t_Tick(object sender, EventArgs e)
        {
            Timer t = sender as Timer;
            bool showRegWindow = (bool)t.Tag;
            t.Stop();
            SetProgressBarVisibility(System.Windows.Visibility.Collapsed, null, false);
            if (showRegWindow)
            {
                mainControl.CheckForRegistration();
            }
        }

        private void DisableProgress(System.Windows.Visibility visibility)
        {

        }

        internal void CheckForRegistration()
        {
            RegisterControl register = new RegisterControl(this, string.Empty);
            ShowPopup(register);
        }



        internal void ShowAbout()
        {
            About a = new About(this);
            ShowPopup(a);
        }

        
    }
}
