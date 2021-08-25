using SoftActivate.Licensing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace PrimaCartoonizer
{
    public class Auth
    {
        public LicensingClient licensing { get; set; }
        private string WorkingPath;
        private string WorkingDirectory;
        private bool active;

        public string Key { get; set; }
        public string Hwd { get; set; }
        public string Act { get; set; }

        public Auth()
        {
            WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\CommonRima";
            WorkingPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\CommonRima\\Settings.cfg";
            licensing = new LicensingClient(Properties.Settings.Default.Auth, CreateLicenseKeyTemplate(), null, null, 162021);//Bilal 162021 c'est le ProductID défini dans le panel d'administration en ligne.
            active = false;
            Init();
        }
        private void Init()
        {
            if (Directory.Exists(WorkingDirectory))
            {
                if (File.Exists(WorkingPath) && File.ReadAllLines(WorkingPath).Length >= 3)
                {
                    Console.WriteLine("Read activation key");
                    string[] lines = File.ReadAllLines(WorkingPath);
                    Key = lines[0];
                    Hwd = lines[1];
                    Act = lines[2];
                    licensing = new LicensingClient(CreateLicenseKeyTemplate(), Key, null, Hwd, Act);
                    if (licensing.IsLicenseValid())
                    {
                        active = true;
                        Console.WriteLine("Licence valid!");
                    }
                    else
                    {
                        Console.WriteLine("invalide key");
                    }
                }
                else
                {
                    File.Create(WorkingPath);
                }
            }
            else
            {
                Directory.CreateDirectory(WorkingDirectory);
            }
        }

        private LicenseTemplate CreateLicenseKeyTemplate()
        //Bilal Paramètres pour l'activation en ligne du logiciel - online activation
        {
            return new LicenseTemplate(5, // number of groups of characters in the license key  
                                    5, // number of character in each group
                                    "ABkHDQELgUXzlogbD65lPAuSAJ/pPL8DOFV4BZCHYHGabCE06gE=", // the public key used to verify the license key signature
                                    null, // private key is not needed for license key validation ! 
                                    109,  // signature size of the license key is 109 bits
                                    16,   // data size embedded in the license key is 16 bits
                                    "162021" // ProductID. this is the name of the data field embedded in the license key
                                    );
        }

        public bool Registernow(string product_key)
        {
            LicenseTemplate l = new LicenseTemplate(5, 5, "ABkHDQELgUXzlogbD65lPAuSAJ/pPL8DOFV4BZCHYHGabCE06gE=", null, 109, 16, "162021");
            LicensingClient lc = new LicensingClient(Properties.Settings.Default.Auth, l, product_key, null, 162021);
            
            try
            {
                lc.AcquireLicense();
            }
            catch (Exception e) // Here we are catching your bug-the unhandled exception
            {
                MessageBox.Show("The Activation Key is not valid", "Activation", MessageBoxButton.OK, MessageBoxImage.Error); //Bilal
            }
            if (lc.IsLicenseValid())
            {
                Key = product_key;
                Hwd = lc.HardwareId;
                Act = lc.ActivationKey;
                return true;
            }
            else
                {
                //MessageBox.Show("Please try again");
                return false;
            }
            
        }

        public void SaveKey()
        {
            File.WriteAllText(WorkingPath, Key + "\n" + Hwd + "\n" + Act);
        }

        public bool Activate()
        {
            return active;
        }

        public bool CheckLicence()
        {
            return (active = licensing.IsLicenseValid());
        }

        public LICENSE_STATUS GetSTATUS()
        {
            return licensing.LicenseStatus;
        }

        public DateTime Days()
        {
            return licensing.LicenseExpirationDate;
        }
    }
}
