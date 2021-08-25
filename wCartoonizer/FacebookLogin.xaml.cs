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
using System.Windows.Shapes;
using System.Reflection;

namespace PrimaCartoonizer
{
    /// <summary>
    /// Interaction logic for FacebookLogin.xaml
    /// </summary>
    public partial class FacebookLogin : Window
    {

        bool isLoggedIn = false;

        public bool IsLoggedIn
        {
            get { return isLoggedIn; }
            set { isLoggedIn = value; }
        }
        string accessToken = "";

        public string AccessToken
        {
            get { return accessToken; }
            set { accessToken = value; }
        }
        System.Windows.Forms.WebBrowser wb = null;
        public FacebookLogin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string uri = @"https://www.facebook.com/dialog/oauth?client_id=192456800944738&redirect_uri=https://www.facebook.com/connect/login_success.html&response_type=token&scope=user_photos,publish_stream,status_update,publish_actions,photo_upload";
            wb = windowsFormsHost1.Child as System.Windows.Forms.WebBrowser;

            wb.Navigate(uri);
        }

        private void WebBrowser_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            if (wb.Url.AbsoluteUri.Contains("access_token"))
            {
                string uri1 = wb.Url.AbsoluteUri;
                string uri2 = uri1.Substring(uri1.IndexOf("access_token") + 13);
                accessToken = uri2.Substring(0, uri2.IndexOf('&'));

                isLoggedIn = true;
                this.Close();

            }
        }
    }
}
