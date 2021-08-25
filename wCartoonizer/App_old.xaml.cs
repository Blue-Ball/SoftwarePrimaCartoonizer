using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace ImageCartoonizerPremium
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            string abc = @"Software\WinRAR ICP\Protection"; //Bilal winrar
            Secure scr = Secure.Instance;
            scr.RegKey = abc;
            RegistryKey regkey = Registry.CurrentUser;
            regkey = regkey.CreateSubKey(abc); //path
            string Br = (string)regkey.GetValue("Password");
            if (Br != null)
                Br = Secure.Decrypt(Br, true);
            else
                Br = "";
            scr.IsRegistered = scr.Algorithm(Br, abc);
            //if (logic == true)
            //{
            base.OnStartup(e);
            //}
            //else
            //{
            //    this.StartupUri = new Uri("Register.xaml", UriKind.Relative);
            //}

            //this.StartupUri = new Uri("ImageEditor.xaml", UriKind.Relative);
        }
    }
}
