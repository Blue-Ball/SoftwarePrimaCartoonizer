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
using System.Windows.Controls.Primitives;
using Cartoonizer.ToolsLib;
using Microsoft.Win32;
using System.IO;
using System.Reflection;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Drawing;
using System.Threading;
using System.Drawing.Imaging;
using Cartoonizer.ToolsLib.Desginer;
using Facebook;
using System.Diagnostics;

/*
 * massi.papaleo@gmail.com for any other request 
*/
namespace PrimaCartoonizer.View
{
    /// <summary>
    /// Interaction logic for MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
        public static int Conversion_Value = 3500; //Bilal
        public static int nombre_effects = 26; //Bilal Le nombre du dernier effet cartoon
        public static int nombre_splatters = 405; //Bilal Le nombre du dernier effet splatter
        public static string HD_Conversion_Value = string.Empty;
        public static string OriginalImage = string.Empty;
        public static string MainFolder = System.IO.Path.GetTempPath() + "\\" + System.IO.Path.GetRandomFileName();

        private static int sliderIValue;
        private static Bitmap _origResized;
        private static string _origImageFilePath;
              
        public MainControl()
        {
            InitializeComponent();
        }

        private void btnColorBalance_Click(object sender, RoutedEventArgs e)
        {           
            drawingCanvas.Tool = (ToolType)Enum.Parse(typeof(ToolType), ((ToggleButton)sender).Tag.ToString());
            drawingCanvas.CroppedImageCompleted = this.CroppedImageCompleted;
            //drawingCanvasOrig.Tool = (ToolType)Enum.Parse(typeof(ToolType), ((ToggleButton)sender).Tag.ToString());
        }

        public void CroppedImageCompleted(System.Drawing.Rectangle rect)
        {
            if (_origResized != null)
            {
                System.Drawing.Bitmap bmpImage = new System.Drawing.Bitmap(_origResized);
                System.Drawing.Bitmap bmpCrop = bmpImage.Clone(rect, bmpImage.PixelFormat);
                _origImageFilePath = CartoonizeHelper._EffectDirectory + "tempfiles\\" + System.IO.Path.GetRandomFileName() + "_Converted_Cropped_" + DateTime.Now.ToString("ddMM_hhmmss") + ".jpg";
                bmpCrop.Save(_origImageFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(_origImageFilePath);
                bitmap.EndInit();
                drawingCanvasOrig.Source = bitmap;
                drawingCanvasOrig.Visibility = Visibility.Visible;
                drawingCanvasOrig.Width = drawingCanvas.Width * drawingCanvas.ActualScale;
                drawingCanvasOrig.Height = drawingCanvas.Height * drawingCanvas.ActualScale;
                drawingCanvas.CroppedImageCompleted = null;
            }
        }

        private void zoomControl_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            drawingCanvas.ActualScale = Math.Round(e.NewValue / 2) / 100.0;
            drawingCanvasOrig.Width = drawingCanvas.Width * drawingCanvas.ActualScale;
            drawingCanvasOrig.Height = drawingCanvas.Height * drawingCanvas.ActualScale;
            // drawingCanvasOrig.ActualScale = Math.Round(e.NewValue / 2) / 100.0;
            if (zoomText != null)
                zoomText.Text = string.Format("{0}%", Math.Round((e.NewValue / 2), 0));

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int r = (int)slider1.Value;
            int g = (int)slider2.Value;
            int bc = (int)slider3.Value;

            int b = (int)sliderB.Value / 2;
            int c = (int)sliderC.Value;
            int i = (int)sliderI.Value;
            drawingCanvas.AdjustBrightness(b, c, i, r, g, bc);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            drawingCanvasOrig.Visibility = Visibility.Hidden;
            ShowOpenDialog();
            drawingCanvasOrig.Width = drawingCanvas.Width * drawingCanvas.ActualScale;
            drawingCanvasOrig.Height = drawingCanvas.Height * drawingCanvas.ActualScale;
            ApplyEffectButton1.Visibility = Visibility.Visible; //Bilal afficher bouton appliquer un effet, il sera affiché en permanence, c'est important.
            Button_Open_Image.Visibility = Visibility.Hidden; //Cacher le bouton view full size
            //Menu.Visibility = Visibility.Visible; // pour afficher le menu de gauche après avoir ajouté une photo (pour éviter un bug lors du clique d'un effet avant d'avoir ajouté une photo).
            //effectsPanelPin.Visibility = Visibility.Visible; //pour afficher le petit spin.

            //IMPORTANT: IL FAUT AUSSI CHANGER LE MEME CODE PLUS BAS AU BOUTON: (dlg.ShowDialog().GetValueOrDefault())
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }


            var style = (Style)FindResource("EffectsButtonSkin");

            //============================================Eyes======================================//
            #region Eyes
            List<string> items1 = new List<string>();
            items1.Add("Eyes/01.png");
            items1.Add("Eyes/02.png");
            items1.Add("Eyes/03.png");
            items1.Add("Eyes/04.png");
            items1.Add("Eyes/05.png");
            items1.Add("Eyes/06.png");
            items1.Add("Eyes/07.png");
            items1.Add("Eyes/08.png");
            items1.Add("Eyes/09.png");
            items1.Add("Eyes/10.png");
            items1.Add("Eyes/11.png");
            items1.Add("Eyes/12.png");
            items1.Add("Eyes/13.png");
            items1.Add("Eyes/14.png");
            items1.Add("Eyes/15.png");
            items1.Add("Eyes/16.png");
            items1.Add("Eyes/17.png");
            items1.Add("Eyes/18.png");
            items1.Add("Eyes/19.png");
            items1.Add("Eyes/20.png");
            items1.Add("Eyes/21.png");
            items1.Add("Eyes/22.png");
            items1.Add("Eyes/23.png");
            items1.Add("Eyes/24.png");
            items1.Add("Eyes/25.png");
            items1.Add("Eyes/26.png");
            items1.Add("Eyes/27.png");
            items1.Add("Eyes/28.png");
            items1.Add("Eyes/29.png");
            items1.Add("Eyes/30.png");
            items1.Add("Eyes/30.png");
            items1.Add("Eyes/31.png");
            items1.Add("Eyes/32.png");
            items1.Add("Eyes/33.png");
            items1.Add("Eyes/34.png");
            items1.Add("Eyes/35.png");
            items1.Add("Eyes/36.png");
            items1.Add("Eyes/37.png");
            items1.Add("Eyes/38.png");
            items1.Add("Eyes/39.png");
            items1.Add("Eyes/40.png");
            items1.Add("Eyes/41.png");
            items1.Add("Eyes/42.png");
            items1.Add("Eyes/43.png");
            items1.Add("Eyes/44.png");
            items1.Add("Eyes/45.png");
            items1.Add("Eyes/46.png");
            items1.Add("Eyes/47.png");
            items1.Add("Eyes/48.png");
            items1.Add("Eyes/49.png");
            items1.Add("Eyes/50.png");
            items1.Add("Eyes/51.png");
            items1.Add("Eyes/52.png");
            items1.Add("Eyes/53.png");
            items1.Add("Eyes/54.png");
            items1.Add("Eyes/55.png");
            items1.Add("Eyes/56.png");
            items1.Add("Eyes/57.png");
            items1.Add("Eyes/58.png");
            foreach (string s in items1)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(string.Format("pack://application:,,,/PrimaCartoonizer;component/Resources/{0}", s));
                image.EndInit();

                toolboxObjects1.Items.Add(new System.Windows.Controls.Image() { IsHitTestVisible = false, Source = image, Stretch = Stretch.Fill, IsManipulationEnabled = true, AllowDrop = true });
            }
            #endregion
            //============================================Eyes======================================//

            //============================================EyesWomen======================================//
            #region EyesWomen
            List<string> items = new List<string>();
            items.Add("EyesWomen/01.png");
            items.Add("EyesWomen/02.png");
            items.Add("EyesWomen/03.png");
            items.Add("EyesWomen/04.png");
            items.Add("EyesWomen/05.png");
            items.Add("EyesWomen/06.png");
            items.Add("EyesWomen/07.png");
            items.Add("EyesWomen/08.png");
            items.Add("EyesWomen/09.png");
            items.Add("EyesWomen/10.png");
            items.Add("EyesWomen/11.png");
            items.Add("EyesWomen/12.png");
            items.Add("EyesWomen/13.png");
            items.Add("EyesWomen/14.png");
            items.Add("EyesWomen/15.png");
            items.Add("EyesWomen/16.png");
            items.Add("EyesWomen/17.png");
            items.Add("EyesWomen/18.png");
            items.Add("EyesWomen/19.png");
            items.Add("EyesWomen/20.png");
            items.Add("EyesWomen/21.png");
            items.Add("EyesWomen/22.png");
            items.Add("EyesWomen/23.png");
            items.Add("EyesWomen/24.png");
            items.Add("EyesWomen/25.png");
            items.Add("EyesWomen/26.png");
            items.Add("EyesWomen/27.png");
            items.Add("EyesWomen/28.png");
            items.Add("EyesWomen/29.png");
            items.Add("EyesWomen/30.png");
            items.Add("EyesWomen/30.png");
            items.Add("EyesWomen/31.png");
            items.Add("EyesWomen/32.png");
            items.Add("EyesWomen/33.png");
            items.Add("EyesWomen/34.png");
            items.Add("EyesWomen/35.png");
            items.Add("EyesWomen/36.png");
            items.Add("EyesWomen/37.png");
            items.Add("EyesWomen/38.png");
            items.Add("EyesWomen/39.png");
            items.Add("EyesWomen/40.png");
            items.Add("EyesWomen/41.png");
            items.Add("EyesWomen/42.png");
            items.Add("EyesWomen/43.png");
            items.Add("EyesWomen/44.png");
            items.Add("EyesWomen/45.png");
            items.Add("EyesWomen/46.png");
            items.Add("EyesWomen/47.png");
            items.Add("EyesWomen/48.png");
            items.Add("EyesWomen/49.png");
            items.Add("EyesWomen/50.png");
            items.Add("EyesWomen/51.png");
            items.Add("EyesWomen/52.png");
            items.Add("EyesWomen/53.png");
            items.Add("EyesWomen/54.png");
            items.Add("EyesWomen/55.png");
            items.Add("EyesWomen/56.png");
            items.Add("EyesWomen/57.png");
            items.Add("EyesWomen/58.png");
            items.Add("EyesWomen/59.png");
            items.Add("EyesWomen/60.png");
            foreach (string s in items)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(string.Format("pack://application:,,,/PrimaCartoonizer;component/Resources/{0}", s));
                image.EndInit();

                toolboxObjects.Items.Add(new System.Windows.Controls.Image() { IsHitTestVisible = false, Source = image, Stretch = Stretch.Fill, IsManipulationEnabled = true, AllowDrop = true });
            }
            #endregion
            //============================================EyesWomen======================================//

            //============================================EyesMan======================================//
            #region EyesMan
            List<string> items2 = new List<string>();
            items2.Add("EyesMan/01.png");
            items2.Add("EyesMan/02.png");
            items2.Add("EyesMan/03.png");
            items2.Add("EyesMan/04.png");
            items2.Add("EyesMan/05.png");
            items2.Add("EyesMan/06.png");
            items2.Add("EyesMan/07.png");
            items2.Add("EyesMan/08.png");
            items2.Add("EyesMan/09.png");
            items2.Add("EyesMan/10.png");
            items2.Add("EyesMan/11.png");
            items2.Add("EyesMan/12.png");
            items2.Add("EyesMan/13.png");
            items2.Add("EyesMan/14.png");
            items2.Add("EyesMan/15.png");
            items2.Add("EyesMan/16.png");
            items2.Add("EyesMan/17.png");
            items2.Add("EyesMan/18.png");
            items2.Add("EyesMan/19.png");
            items2.Add("EyesMan/20.png");
            items2.Add("EyesMan/21.png");
            items2.Add("EyesMan/22.png");
            items2.Add("EyesMan/23.png");
            items2.Add("EyesMan/24.png");
            items2.Add("EyesMan/25.png");
            items2.Add("EyesMan/26.png");
            items2.Add("EyesMan/27.png");
            items2.Add("EyesMan/28.png");
            items2.Add("EyesMan/29.png");
            items2.Add("EyesMan/30.png");
            items2.Add("EyesMan/31.png");
            items2.Add("EyesMan/32.png");
            items2.Add("EyesMan/33.png");
            items2.Add("EyesMan/34.png");
            items2.Add("EyesMan/35.png");
            items2.Add("EyesMan/36.png");
            items2.Add("EyesMan/37.png");
            items2.Add("EyesMan/38.png");
            items2.Add("EyesMan/39.png");
            items2.Add("EyesMan/40.png");
            items2.Add("EyesMan/41.png");
            items2.Add("EyesMan/42.png");
            items2.Add("EyesMan/43.png");
            items2.Add("EyesMan/44.png");
            items2.Add("EyesMan/45.png");
            items2.Add("EyesMan/46.png");
            items2.Add("EyesMan/47.png");
            items2.Add("EyesMan/48.png");
            items2.Add("EyesMan/49.png");
            items2.Add("EyesMan/50.png");
            items2.Add("EyesMan/51.png");
            items2.Add("EyesMan/52.png");
            items2.Add("EyesMan/53.png");
            items2.Add("EyesMan/54.png");
            items2.Add("EyesMan/55.png");
            items2.Add("EyesMan/56.png");
            items2.Add("EyesMan/57.png");
            items2.Add("EyesMan/58.png");
            items2.Add("EyesMan/59.png");
            items2.Add("EyesMan/60.png");
            items2.Add("EyesMan/61.png");
            items2.Add("EyesMan/62.png");
            items2.Add("EyesMan/63.png");
            items2.Add("EyesMan/64.png");
            items2.Add("EyesMan/65.png");
            items2.Add("EyesMan/66.png");
            items2.Add("EyesMan/67.png");
            items2.Add("EyesMan/68.png");
            foreach (string s in items2)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(string.Format("pack://application:,,,/PrimaCartoonizer;component/Resources/{0}", s));
                image.EndInit();

                toolboxObjects2.Items.Add(new System.Windows.Controls.Image() { IsHitTestVisible = false, Source = image, Stretch = Stretch.Fill, IsManipulationEnabled = true, AllowDrop = true });
            }
            #endregion
            //============================================EyesMan======================================//

            //============================================EyesToon======================================//
            #region EyesToon
            List<string> items3 = new List<string>();
            items3.Add("EyesToon/01.png");
            items3.Add("EyesToon/02.png");
            items3.Add("EyesToon/03.png");
            items3.Add("EyesToon/04.png");
            items3.Add("EyesToon/05.png");
            items3.Add("EyesToon/06.png");
            items3.Add("EyesToon/07.png");
            items3.Add("EyesToon/08.png");
            items3.Add("EyesToon/09.png");
            items3.Add("EyesToon/10.png");
            items3.Add("EyesToon/11.png");
            items3.Add("EyesToon/12.png");
            items3.Add("EyesToon/13.png");
            items3.Add("EyesToon/14.png");
            items3.Add("EyesToon/15.png");
            items3.Add("EyesToon/16.png");
            items3.Add("EyesToon/17.png");
            items3.Add("EyesToon/18.png");
            items3.Add("EyesToon/19.png");
            items3.Add("EyesToon/20.png");
            items3.Add("EyesToon/21.png");
            items3.Add("EyesToon/22.png");
            items3.Add("EyesToon/23.png");
            items3.Add("EyesToon/24.png");
            items3.Add("EyesToon/25.png");
            items3.Add("EyesToon/26.png");
            items3.Add("EyesToon/27.png");
            items3.Add("EyesToon/28.png");
            items3.Add("EyesToon/29.png");
            items3.Add("EyesToon/30.png");
            items3.Add("EyesToon/31.png");
            items3.Add("EyesToon/32.png");
            items3.Add("EyesToon/33.png");
            items3.Add("EyesToon/34.png");
            items3.Add("EyesToon/35.png");
            items3.Add("EyesToon/36.png");
            items3.Add("EyesToon/37.png");
            items3.Add("EyesToon/38.png");
            items3.Add("EyesToon/39.png");
            items3.Add("EyesToon/40.png");
            items3.Add("EyesToon/41.png");
            items3.Add("EyesToon/42.png");
            items3.Add("EyesToon/43.png");
            items3.Add("EyesToon/44.png");
            items3.Add("EyesToon/45.png");
            items3.Add("EyesToon/46.png");
            items3.Add("EyesToon/47.png");
            items3.Add("EyesToon/48.png");
            items3.Add("EyesToon/49.png");
            items3.Add("EyesToon/50.png");
            items3.Add("EyesToon/51.png");
            items3.Add("EyesToon/52.png");
            items3.Add("EyesToon/53.png");
            items3.Add("EyesToon/54.png");
            items3.Add("EyesToon/55.png");
            items3.Add("EyesToon/56.png");
            items3.Add("EyesToon/57.png");
            items3.Add("EyesToon/58.png");
            foreach (string s in items3)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(string.Format("pack://application:,,,/PrimaCartoonizer;component/Resources/{0}", s));
                image.EndInit();

                toolboxObjects3.Items.Add(new System.Windows.Controls.Image() { IsHitTestVisible = false, Source = image, Stretch = Stretch.Fill, IsManipulationEnabled = true, AllowDrop = true });
            }
            #endregion
            //============================================EyesToon======================================//

            //============================================Glasses======================================//
            #region Glasses
            List<string> items4 = new List<string>();
            items4.Add("Glasses/01.png");
            items4.Add("Glasses/02.png");
            items4.Add("Glasses/03.png");
            items4.Add("Glasses/04.png");
            items4.Add("Glasses/05.png");
            items4.Add("Glasses/06.png");
            items4.Add("Glasses/07.png");
            items4.Add("Glasses/08.png");
            items4.Add("Glasses/09.png");
            items4.Add("Glasses/10.png");
            items4.Add("Glasses/11.png");
            items4.Add("Glasses/12.png");
            items4.Add("Glasses/13.png");
            items4.Add("Glasses/14.png");
            items4.Add("Glasses/15.png");
            items4.Add("Glasses/16.png");
            items4.Add("Glasses/17.png");
            items4.Add("Glasses/18.png");
            items4.Add("Glasses/19.png");
            items4.Add("Glasses/20.png");
            items4.Add("Glasses/21.png");
            items4.Add("Glasses/22.png");
            items4.Add("Glasses/23.png");
            items4.Add("Glasses/24.png");
            items4.Add("Glasses/25.png");
            items4.Add("Glasses/26.png");
            items4.Add("Glasses/27.png");
            items4.Add("Glasses/28.png");
            items4.Add("Glasses/29.png");
            items4.Add("Glasses/30.png");
            items4.Add("Glasses/31.png");
            items4.Add("Glasses/32.png");
            items4.Add("Glasses/33.png");
            items4.Add("Glasses/34.png");
            foreach (string s in items4)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(string.Format("pack://application:,,,/PrimaCartoonizer;component/Resources/{0}", s));
                image.EndInit();

                toolboxObjects4.Items.Add(new System.Windows.Controls.Image() { IsHitTestVisible = false, Source = image, Stretch = Stretch.Fill, IsManipulationEnabled = true, AllowDrop = true });
            }
            #endregion
            //============================================Glasses======================================//

            //============================================Popart======================================//
            #region Popart
            List<string> items5 = new List<string>();
            items5.Add("Popart/01.png");
            items5.Add("Popart/02.png");
            items5.Add("Popart/03.png");
            items5.Add("Popart/04.png");
            items5.Add("Popart/05.png");
            items5.Add("Popart/06.png");
            items5.Add("Popart/07.png");
            items5.Add("Popart/08.png");
            items5.Add("Popart/09.png");
            items5.Add("Popart/10.png");
            items5.Add("Popart/11.png");
            items5.Add("Popart/12.png");
            items5.Add("Popart/13.png");
            items5.Add("Popart/14.png");
            items5.Add("Popart/15.png");
            items5.Add("Popart/16.png");
            items5.Add("Popart/17.png");
            items5.Add("Popart/18.png");
            items5.Add("Popart/19.png");
            items5.Add("Popart/20.png");
            items5.Add("Popart/21.png");
            items5.Add("Popart/22.png");
            items5.Add("Popart/23.png");
            items5.Add("Popart/24.png");
            items5.Add("Popart/25.png");
            items5.Add("Popart/26.png");
            items5.Add("Popart/27.png");
            items5.Add("Popart/28.png");
            items5.Add("Popart/29.png");
            items5.Add("Popart/30.png");
            items5.Add("Popart/30.png");
            items5.Add("Popart/31.png");
            items5.Add("Popart/32.png");
            items5.Add("Popart/33.png");
            items5.Add("Popart/34.png");
            items5.Add("Popart/35.png");
            items5.Add("Popart/36.png");
            items5.Add("Popart/37.png");
            items5.Add("Popart/38.png");
            items5.Add("Popart/39.png");
            items5.Add("Popart/40.png");
            items5.Add("Popart/41.png");
            items5.Add("Popart/42.png");
            items5.Add("Popart/43.png");
            items5.Add("Popart/44.png");
            items5.Add("Popart/45.png");
            items5.Add("Popart/46.png");
            items5.Add("Popart/47.png");
            items5.Add("Popart/48.png");
            items5.Add("Popart/49.png");
            items5.Add("Popart/50.png");
            items5.Add("Popart/51.png");
            items5.Add("Popart/52.png");
            items5.Add("Popart/53.png");
            items5.Add("Popart/54.png");
            items5.Add("Popart/55.png");
            items5.Add("Popart/56.png");
            items5.Add("Popart/57.png");
            items5.Add("Popart/58.png");
            items5.Add("Popart/59.png");
            items5.Add("Popart/60.png");
            items5.Add("Popart/61.png");
            items5.Add("Popart/62.png");
            items5.Add("Popart/63.png");
            items5.Add("Popart/64.png");
            items5.Add("Popart/65.png");
            items5.Add("Popart/66.png");
            items5.Add("Popart/67.png");
            foreach (string s in items5)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(string.Format("pack://application:,,,/PrimaCartoonizer;component/Resources/{0}", s));
                image.EndInit();

                toolboxObjects5.Items.Add(new System.Windows.Controls.Image() { IsHitTestVisible = false, Source = image, Stretch = Stretch.Fill, IsManipulationEnabled = true, AllowDrop = true });
            }
            #endregion
            //============================================Popart======================================//




            List<string> effects = new List<string>();
            effects.Add("effects/effet0.jpg");
            effects.Add("effects/effet100.jpg");
            for (int i = 1; i <= nombre_effects; i++)
            {
                effects.Add("effects/effet" + i + ".jpg");
            }
            foreach (string s in effects)
            {
                string tag = s.Substring(0, s.IndexOf('.'));
                tag = tag.Substring(13);
                int t = int.Parse(tag);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(string.Format("pack://application:,,,/PrimaCartoonizer;component/Resources/{0}", s));
                image.EndInit();
                Button button = new Button()
                {
                    Tag = t,
                    Style = style,
                    Content = new System.Windows.Controls.Image() { IsHitTestVisible = false, Source = image, Stretch = Stretch.Uniform }
                };
                button.Click += button_Click;

                toolboxEffects.Items.Add(button);
            }


            List<string> Splatters = new List<string>();
            for (int i = 401; i <= nombre_splatters; i++)
            {
                Splatters.Add("effects/effet" + i + ".jpg");
            }
            foreach (string s in Splatters)
            {
                string tag = s.Substring(0, s.IndexOf('.'));
                tag = tag.Substring(13);
                int t = int.Parse(tag);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(string.Format("pack://application:,,,/PrimaCartoonizer;component/Resources/{0}", s));
                image.EndInit();
                Button button = new Button()
                {
                    Tag = t,
                    Style = style,
                    Content = new System.Windows.Controls.Image() { IsHitTestVisible = false, Source = image, Stretch = Stretch.Uniform }
                };
                button.Click += button_Click;
                toolboxSplatters.Items.Add(button);
            }



            List<string> Textures = new List<string>();
            Textures.Add("effects/effet101.jpg");
            Textures.Add("effects/effet102.jpg");
            Textures.Add("effects/effet103.jpg");
            Textures.Add("effects/effet104.jpg");
            foreach (string s in Textures)
            {
                string tag = s.Substring(0, s.IndexOf('.'));
                tag = tag.Substring(13);
                int t = int.Parse(tag);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(string.Format("pack://application:,,,/PrimaCartoonizer;component/Resources/{0}", s));
                image.EndInit();
                Button button = new Button()
                {
                    Tag = t,
                    Style = style,
                    Content = new System.Windows.Controls.Image() { IsHitTestVisible = false, Source = image, Stretch = Stretch.Uniform }
                };
                button.Click += button_Click;

                toolboxTextures.Items.Add(button);
            }


            List<string> Frames = new List<string>();
            Frames.Add("effects/effet201.jpg");
            Frames.Add("effects/effet202.jpg");
            Frames.Add("effects/effet203.jpg");
            Frames.Add("effects/effet204.jpg");
            Frames.Add("effects/effet205.jpg");
            Frames.Add("effects/effet206.jpg");
            Frames.Add("effects/effet207.jpg");
            Frames.Add("effects/effet208.jpg");
            Frames.Add("effects/effet209.jpg");
            Frames.Add("effects/effet210.png");
            Frames.Add("effects/effet211.png");
            Frames.Add("effects/effet212.png");
            Frames.Add("effects/effet213.png");
            foreach (string s in Frames)
            {
                string tag = s.Substring(0, s.IndexOf('.'));
                tag = tag.Substring(13);
                int t = int.Parse(tag);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(string.Format("pack://application:,,,/PrimaCartoonizer;component/Resources/{0}", s));
                image.EndInit();
                Button button = new Button()
                {
                    Tag = t,
                    Style = style,
                    Content = new System.Windows.Controls.Image() { IsHitTestVisible = false, Source = image, Stretch = Stretch.Uniform }
                };
                button.Click += button_Click;

                toolboxFrames.Items.Add(button);
            }





            List<string> resize = new List<string>();
            resize.Add("effects/effet300.png");
            resize.Add("effects/effet301.png");
            resize.Add("effects/effet302.png");
            resize.Add("effects/effet303.png");
            foreach (string s in resize)
            {
                string tag = s.Substring(0, s.IndexOf('.'));
                tag = tag.Substring(13);
                int t = int.Parse(tag);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(string.Format("pack://application:,,,/PrimaCartoonizer;component/Resources/{0}", s));
                image.EndInit();
                Button button = new Button()
                {
                    Tag = t,
                    Style = style,
                    Content = new System.Windows.Controls.Image() { IsHitTestVisible = false, Source = image, Stretch = Stretch.Uniform }
                };
                button.Click += button_Click;

                toolboxResize.Items.Add(button);
            }


            //34 effects
            /*
            for (int t = 1000; t <= (1000 + AFrogeHelper.FILTERS_COUNT); t++)
            {
                Button button = new Button()
                {
                    Tag = t,
                    Style = style,
                    Content = new System.Windows.Controls.Image() { IsHitTestVisible = false, Stretch = Stretch.Uniform }
                };
                button.Click += button_Click;
                //toolboxEffects2.Items.Add(button);
            }



            for (int t = 0; t < AFrogeHelper.INSTAGRAM_EFFECTS_COUNT; t++)
            {
                Button button = new Button()
                {
                    Tag = t + 2100,
                    Style = style,
                    Content = new System.Windows.Controls.Image() { IsHitTestVisible = false, Stretch = Stretch.Uniform }
                };
                button.Click += button_Click;
                //toolboxInstagramEffects.Items.Add(button);
            }

            */

            for (int t = 0; t < AFrogeHelper.VARIOUS_FRAMES_COUNT; t++)
            {
                /*
                Button button = new Button()
                {
                    Tag = t + 2300,
                    Style = style,
                    Content = new System.Windows.Controls.Image() { IsHitTestVisible = false, Stretch = Stretch.Uniform }
                };
                button.Click += button_Click;
                toolboxVariousFrames.Items.Add(button);
                */
            }
        }

        //Bilal déterminer les effets trial à bloquer
        void button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                int effectId = (int)btn.Tag;

                //=============Bilal Condition pour la sauvegarde du fichier Vector final_PC_saved.svg==========
                if (effectId == 3 || effectId == 4 || effectId == 5 || effectId == 8 || effectId == 9 || effectId == 15 || effectId == 18 || effectId == 21 || effectId == 22)
                {
                    btnSVG.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    btnSVG.Visibility = System.Windows.Visibility.Collapsed;
                }
                //=============Bilal Condition pour la sauvegarde du fichier Vector final_PC_saved.svg End==========

                if (new Auth().Activate()) //si le logiciel est activé avec une clé d'activation
                {
                    ApplyEffect(effectId, null);
                    progressbartext.Visibility = System.Windows.Visibility.Visible; //Bilal pour afficher le message de progression après avoir cliqué sur un effet
                }
                else
                {
                    if (effectId == 3 || effectId == 4 || effectId == 6 || effectId == 9 || effectId == 10 || effectId == 11 || effectId == 14 || effectId == 17 || effectId == 19 || effectId == 32 || effectId == 36 || effectId == 37 || effectId == 38 || effectId == 39)
                    {
                        System.Windows.MessageBox.Show("This effect only works in the full version." + Environment.NewLine + "You can unlock it by purchasing the full version from PrimaCartoonizer.com", "Locked Effect :(");
                    }
                    else
                    {
                        ApplyEffect(effectId, null);
                        progressbartext.Visibility = System.Windows.Visibility.Visible; //Bilal pour afficher le message de progression après avoir cliqué sur un effet

                    }
                }
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            StringBuilder sp = new StringBuilder();
            sp.Append("JPG Files (*.jpg)|*.jpg|");
            sp.Append("JPEG Files (*.jpeg)|*.jpeg|");
            sp.Append("PNG Files (*.png)|*.png|");
            sp.Append("GIF Files (*.gif)|*.gif|");
            sp.Append("All picture files|*.jpg;*.png;*.jpeg;*.gif|");
            sp.Append("All files (*.*)|*.*");
            dlg.Filter = sp.ToString();
            dlg.FilterIndex = 5;

            if (dlg.ShowDialog().GetValueOrDefault())
            {
                if (!(new Auth().Activate()))
                {
                    System.Drawing.Image image = drawingCanvas.ExportToImage();
                    string tempS = System.IO.Path.GetTempFileName();
                    tempS = tempS.Substring(0, tempS.Length - 3) + "jpg";
                    image.Save(tempS, ImageFormat.Jpeg);
                    image.Dispose();
                    lastApply = tempS;
                    drawingCanvas.DeleteAll();
                    drawingCanvas.SetBackground(lastApply);
                    //drawingCanvasOrig.DeleteAll();
                    //drawingCanvasOrig.SetBackground(lastApply);
                    ApplyEffectButton.Visibility = Visibility.Hidden;
                    Button_Undo.Visibility = Visibility.Hidden;
                    this.SetProgressBarVisibility(System.Windows.Visibility.Visible, "Done", false);
                    progressbartext.Visibility = Visibility.Hidden;
                    progressbartext.Visibility = System.Windows.Visibility.Hidden;
                    ApplyEffect(-1, () =>
                    {
                        SaveToFile(dlg.FileName);
                    });
                }
                else
                {
                    SaveToFile(dlg.FileName);
                }
            }
        }
        private void SaveToFile(string fileName)
        {
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

            EncoderParameters encoderParams = new EncoderParameters(1);

            EncoderParameter qualityParam =
                new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 80L);

            encoderParams.Param[0] = qualityParam;

            //System.Drawing.Image image = drawingCanvas.ExportToImage();
            //Image image = System.Drawing.Image.FromStream(System.IO.Path.GetTempPath() + "image.jpg");
            //Bitmap image = new Bitmap(System.IO.Path.GetTempPath() + "final_PC_saved.jpg");
            Bitmap image = new Bitmap(MainControl.MainFolder + "//final_PC_saved.jpg");
            image.SetResolution(300.0f, 300.0f);
            image.Save(fileName, ImageFormat.Jpeg);
            image.Dispose();
        }

        private void btnSVG_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            StringBuilder sp = new StringBuilder();
            sp.Append("SVG Files (*.svg)|*.svg");
            sp.Append("SVG Files (*.svg)|*.svg");
            sp.Append("All files (*.*)|*.*");
            dlg.Filter = sp.ToString();
            dlg.DefaultExt = "svg";
            dlg.AddExtension = true;
            dlg.FilterIndex = 1;

            if (dlg.ShowDialog().GetValueOrDefault())
            {
                if (!(new Auth().Activate()))
                {
                    System.Drawing.Image image = drawingCanvas.ExportToImage();
                    string tempS = System.IO.Path.GetTempFileName();
                    tempS = tempS.Substring(0, tempS.Length - 3) + "svg";
                    image.Save(tempS, ImageFormat.Png);
                    image.Dispose();
                    lastApply = tempS;
                    drawingCanvas.DeleteAll();
                    drawingCanvas.SetBackground(lastApply);
                    //drawingCanvasOrig.DeleteAll();
                    //drawingCanvasOrig.SetBackground(lastApply);
                    ApplyEffectButton.Visibility = Visibility.Hidden;
                    Button_Undo.Visibility = Visibility.Hidden;
                    this.SetProgressBarVisibility(System.Windows.Visibility.Visible, "Done", false);
                    progressbartext.Visibility = Visibility.Hidden;
                    progressbartext.Visibility = System.Windows.Visibility.Hidden;
                    ApplyEffect(-1, () =>
                    {
                        SaveToFileSVG(dlg.FileName);
                    });
                }
                else
                {
                    SaveToFileSVG(dlg.FileName);
                }
            }
        }

        private void SaveToFileSVG(string fileName)
        {
            //string image = System.IO.Path.GetTempPath() + "final_PC_saved.svg";
            string image = MainControl.MainFolder + "//final_PC_saved.svg";
            File.Copy(image, fileName);
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }


        public static string MainExecutablesPath { get; set; }
        public static string MainExecutablesPath1 { get; set; }
        public static string FFMPEG_EXE_NAME = "ffmpeg.exe";
        public static string CONVERT_EXE_NAME = "ct.exe";
        public static bool IS_INITIALIZED = false;
        string lastApply = string.Empty;



        private void UpdatePercentage()
        {
            progressbartext.Dispatcher.BeginInvoke((Action)(() =>
            {
                try
                {
                    int numberOfConverted = 0;
                    //if (Directory.Exists(CartoonizeHelper.path))
                    //{
                    // if (Directory.GetFiles(CartoonizeHelper.path).Length > 0)
                    //{
                    //Directory.CreateDirectory(CartoonizeHelper.path);
                    numberOfConverted = Directory.GetFiles(CartoonizeHelper.path).Count();
                    int expectedNumberOfFiles = 123; //Bilal c'est le nombre minimum d'image générées par tous les effets dans le dossier tmp pour chaque effet.
                    int percentage = numberOfConverted * 100 / expectedNumberOfFiles;

                    string fileName = System.IO.Path.Combine(CartoonizeHelper.path, "file.txt");
                    string exist = System.IO.Path.Combine(CartoonizeHelper.path, "exist.jpg");
                    if (!File.Exists(fileName))
                    {
                        using (FileStream fs = File.Create(fileName))
                        {
                            Byte[] title = new UTF8Encoding(true).GetBytes("0");
                            fs.Write(title, 0, title.Length);
                        }
                    }

                    /*if (percentage == 0)
                    {
                        //progressbartext.Visibility = System.Windows.Visibility.Visible;
                        //progressbartext.Text = "\r\n Please wait...\r\n";
                        progressbartext.Text = "\r\n Conversion in progress...\r\n Please wait...3%\r\n";
                    }*/

                    //if (percentage > 0 & percentage < 100)
                    if (percentage < 100)
                    {
                        int i = 1;
                        if (i <= 100)
                        {
                            while (File.Exists(fileName) && i < 101)
                            {
                                fileName = System.IO.Path.Combine(CartoonizeHelper.path, "file" + i.ToString() + ".txt");
                                NewProgressBar_Plein.Width = i * 3;
                                i++;
                            }
                            using (FileStream fs = File.Create(fileName))
                            {
                                Byte[] title = new UTF8Encoding(true).GetBytes("0");
                                fs.Write(title, 0, title.Length);
                            }
                        }
                        //progressbartext.Text = "\r\n Conversion in progress...\r\n Please wait..." + percentage.ToString() + "%\r\n";
                        progressbartext.Text = "\r\n Conversion in progress...\r\n " + percentage.ToString() + "%\r\n";
                        /*
                        if (File.Exists(exist))
                        {
                            progressbartext.Visibility = System.Windows.Visibility.Hidden;
                            NewProgressBar_Vide.Visibility = System.Windows.Visibility.Hidden;
                            NewProgressBar_Plein.Visibility = System.Windows.Visibility.Hidden;
                            NewProgressBar_Plein.Visibility = Visibility.Hidden;
                            NewProgressBar_Vide.Visibility = Visibility.Hidden;
                        }*/
                    }

                    /*
                    if (percentage >= 100)
                    {
                        progressbartext.Visibility = System.Windows.Visibility.Visible;
                        progressbartext.Text = "\r\n Conversion in progress...\r\n Please wait...99%\r\n";
                    }
                    */
                    /*
                     else 
                    {
                        progressbartext.Visibility = System.Windows.Visibility.Hidden;
                    }*/
                    // }
                    // }


                }
                catch (Exception ex)
                {
                    //logging goes here
                    //return true;
                }
            }));
        }

        public void ApplyEffect(int effectId, Action a)
        {
            if (radioButtonSD.IsChecked.GetValueOrDefault())
                HD_Conversion_Value = "SD";
            else if (radioButtonHD.IsChecked.GetValueOrDefault())
                HD_Conversion_Value = "HD";
            else if (radioButtonFullHD.IsChecked.GetValueOrDefault())
            {
                HD_Conversion_Value = "Full_HD";
            }
            else
            {
                MessageBox.Show("Please select Conversion Quality");
                return;
            }

            Button_Open_Image.Visibility = Visibility.Hidden; //Cacher le bouton view full size
            ApplyEffectButton.Visibility = Visibility.Hidden;
            Button_Undo.Visibility = Visibility.Hidden;
            //SetProgressBarVisibility(Visibility.Visible, null, true);
            progressbartext.Visibility = Visibility.Visible; //Bilal afficher la barre de progression pendant l'update et l'upload de l'image
            progressbartext.Visibility = System.Windows.Visibility.Visible;
            WaitingImage.Visibility = System.Windows.Visibility.Visible;
            NewProgressBar_Vide.Visibility = System.Windows.Visibility.Visible;
            NewProgressBar_Plein.Visibility = System.Windows.Visibility.Visible;
            //progressbartext.Text = "\r\n Applying changes in progress...\r\n \r\n";
            progressbartext.Text = "\r\n Loading...\r\n Please wait..\r\n";
            Completed = false;
            // System.Drawing.Image image = drawingCanvas.ExportToImage();

            string tempS = CartoonizeHelper._EffectDirectory + "tempfiles\\" + System.IO.Path.GetRandomFileName() + DateTime.Now.ToString("ddMM_hhmmss") + ".jpg";
            //string tempS = System.IO.Path.GetTempFileName();
            tempS = tempS.Substring(0, tempS.Length - 3) + "jpg"; //Bilal PNG TO JPG TEMP FOLDER
            if (File.Exists(tempS))
            {
                //File.Copy(lastApply, tempS);
            }
            else
            {
                File.Copy(lastApply, tempS);
            }
            //  image.Save(tempS);

            Task.Factory.StartNew(() =>
            {
                //int percentage = 0;
                while (!Completed)
                {
                    UpdatePercentage();
                    Thread.Sleep(700); //1s Bilal déterminer la durée de l'update de la barre de progression.
                }
            });
            Task.Factory.StartNew(() =>
            {

                InitCartoonizer();

                OriginalImage = lastApply;
                if (effectId == 306)
                {
                    
                    var per = (sliderIValue * 100) / 255;
                    CartoonizeHelper.CartoonizeImage(MainExecutablesPath, tempS, effectId, GetImageMagicExecutablePath(), (int)per, _origImageFilePath);
                }
                else
                {
                    CartoonizeHelper.CartoonizeImage(MainExecutablesPath, tempS, effectId, GetImageMagicExecutablePath());
                }

                Dispatcher.BeginInvoke((Action)(() =>
                {
                    drawingCanvas.DeleteAll();
                    drawingCanvas.SetBackground(tempS);
                    Bitmap temp = (Bitmap)System.Drawing.Image.FromFile(tempS);
                    Bitmap original = (Bitmap)System.Drawing.Image.FromFile(lastApply);
                    _origResized = new Bitmap(original, new System.Drawing.Size(temp.Width, temp.Height));
                    _origImageFilePath = CartoonizeHelper._EffectDirectory + "tempfiles\\" + System.IO.Path.GetRandomFileName() + "_Cropped_" + DateTime.Now.ToString("ddMM_hhmmss") + ".jpg";
                    _origResized.Save(_origImageFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(_origImageFilePath);
                    bitmap.EndInit();
                    drawingCanvasOrig.Source = bitmap;
                    drawingCanvasOrig.Visibility = Visibility.Visible;
                    drawingCanvasOrig.Width = drawingCanvas.Width * drawingCanvas.ActualScale;
                    drawingCanvasOrig.Height = drawingCanvas.Height * drawingCanvas.ActualScale;
                    //drawingCanvasOrig.DeleteAll();
                    //drawingCanvasOrig.SetBackground(tempfileName);
                }));



                Dispatcher.BeginInvoke((Action)(() =>
                {
                    if (new Auth().Activate())
                    {

                        SetProgressBarVisibility(Visibility.Hidden, "Converted Successfully", true);
                        progressbartext.Visibility = Visibility.Hidden;
                        WaitingImage.Visibility = System.Windows.Visibility.Hidden;
                        progressbartext.Visibility = System.Windows.Visibility.Hidden;
                        progressbartext_Update.Visibility = Visibility.Hidden;
                        progressbartext_Update.Visibility = System.Windows.Visibility.Hidden;
                        ApplyEffectButton.Visibility = Visibility.Visible;
                        Button_Undo.Visibility = Visibility.Visible; //Bilal bouton undo désactivé pour bug avec la nouvelle version 4096
                        //Code pour afficher le message done après avoir cliqué sur Apply button
                        MainWindow mainWindow = VisualTreeHelperExtensions.FindAncestor<Window>(this) as MainWindow;
                        mainWindow.SetProgressBarVisibility(System.Windows.Visibility.Visible, "Done", false);
                        progressbartext.Visibility = Visibility.Hidden;
                        progressbartext.Visibility = System.Windows.Visibility.Hidden;

                        Menu.Visibility = Visibility.Visible; // pour afficher le menu de gauche après avoir ajouté une photo (pour éviter un bug lors du clique d'un effet avant d'avoir ajouté une photo).
                        effectsPanelPin.Visibility = Visibility.Visible; //pour afficher le petit spin.
                        Button_Open_Image.Visibility = Visibility.Visible; //Afficher le bouton View full size
                        NewProgressBar_Vide.Visibility = System.Windows.Visibility.Hidden;
                        NewProgressBar_Plein.Visibility = System.Windows.Visibility.Hidden;
                        NewProgressBar_Plein.Visibility = Visibility.Hidden;
                        NewProgressBar_Vide.Visibility = Visibility.Hidden;
                        //Fin du Code pour afficher le message done après avoir cliqué sur Apply button
                        //File.Delete(CartoonizeHelper.path);
                    }
                    else
                    {
                        /*
                        MessageBox.Show("Converted successfully!\r\n \r\nYou are using a Trial version, please note that Trial version adds " +
                           "watermerk to the cartoonized photo.\r\n \r\nIf you want to convert your photos without watermark, please get the" +
                           " full version from the link below:\r\n \r\n" + Properties.Settings.Default.BuyNowUrl,
                           "Trial Version",
                           MessageBoxButton.OK,
                           MessageBoxImage.Information);
                        */

                        progressbartext.Visibility = Visibility.Hidden;
                        WaitingImage.Visibility = System.Windows.Visibility.Hidden;
                        progressbartext.Visibility = System.Windows.Visibility.Hidden;
                        progressbartext_Update.Visibility = Visibility.Hidden;
                        progressbartext_Update.Visibility = System.Windows.Visibility.Hidden;
                        SetProgressBarVisibility(Visibility.Hidden, "Converted successfully!\r\n \r\n" +
                            "You are using a Trial version, please note that this Trial version will add \r\n" +
                             "a watermark to the cartoonized photo.", true);
                        ApplyEffectButton.Visibility = Visibility.Visible;
                        Button_Undo.Visibility = Visibility.Visible;

                        Menu.Visibility = Visibility.Visible; // pour afficher le menu de gauche après avoir ajouté une photo (pour éviter un bug lors du clique d'un effet avant d'avoir ajouté une photo).
                        effectsPanelPin.Visibility = Visibility.Visible; //pour afficher le petit spin.
                        Button_Open_Image.Visibility = Visibility.Visible; //Afficher le bouton View full size
                        NewProgressBar_Vide.Visibility = System.Windows.Visibility.Hidden;
                        NewProgressBar_Plein.Visibility = System.Windows.Visibility.Hidden;
                        NewProgressBar_Plein.Visibility = Visibility.Hidden;
                        NewProgressBar_Vide.Visibility = Visibility.Hidden;
                        //File.Delete(CartoonizeHelper.path);

                    }

                    if (a != null)
                    {
                        a.Invoke();
                    }
                    Completed = true;
                }));
            });
        }



        private void SetProgressBarVisibility(Visibility visibility, string message, bool showRegWindow)
        {

            MainWindow mainWindow = VisualTreeHelperExtensions.FindAncestor<Window>(this) as MainWindow;

            //mainWindow.SetProgressBarVisibility(visibility, message, showRegWindow); //Bilal désactiver pour supprimer le Loading...(trop lourd) 
            progressbartext.Visibility = Visibility.Hidden;
            progressbartext.Visibility = System.Windows.Visibility.Hidden;
            progressbartext_Update.Visibility = Visibility.Hidden;
            progressbartext_Update.Visibility = System.Windows.Visibility.Hidden;
            WaitingImage.Visibility = System.Windows.Visibility.Hidden;
            NewProgressBar_Vide.Visibility = System.Windows.Visibility.Hidden;
            NewProgressBar_Plein.Visibility = System.Windows.Visibility.Hidden;
            NewProgressBar_Plein.Visibility = Visibility.Hidden;
            NewProgressBar_Vide.Visibility = Visibility.Hidden;
        }


        public void CheckForRegistration()
        {
            if (!(new Auth().Activate()))
            {
                MainWindow mainWindow = VisualTreeHelperExtensions.FindAncestor<Window>(this) as MainWindow;

                mainWindow.CheckForRegistration();
            }
        }

        private static void InitCartoonizer()
        {
            if (!IS_INITIALIZED)
            {
                string gmicPath = GetGmicExecutablePath();
                string imageMagicPath = GetImageMagicExecutablePath();

                //MainExecutablesPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(),System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetRandomFileName()));

                //MainExecutablesPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "PMAMainExecutablesPath");
                MainExecutablesPath = System.IO.Path.Combine(MainControl.MainFolder, System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetRandomFileName()));
                Directory.CreateDirectory(MainExecutablesPath);
                try
                {
                    /*if (!Directory.Exists(MainExecutablesPath))
                    {
                        Directory.CreateDirectory(MainExecutablesPath); //Création du dossier iTMainExecutablesPath dans Temp si il n'existe pas
                    }
                    if (Directory.GetFiles(MainExecutablesPath).Length > 10) //Si le dossier iTMainExecutablesPath existe et il n'est pas vide
                    {
                        IS_INITIALIZED = true;
                    }
                    else
                    {*/
                    System.IO.File.Copy(gmicPath + "\\ca.deb", MainExecutablesPath + "\\ca.exe", true);
                    System.IO.File.Copy(gmicPath + "\\convert1.deb", MainExecutablesPath + "\\libcurl-4.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert2.deb", MainExecutablesPath + "\\libeay32.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert3.deb", MainExecutablesPath + "\\libffi-6.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert4.deb", MainExecutablesPath + "\\libfftw3-3.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert5.deb", MainExecutablesPath + "\\libgcc_s_dw2-1.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert6.deb", MainExecutablesPath + "\\libgmp-10.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert7.deb", MainExecutablesPath + "\\libgnutls-30.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert8.deb", MainExecutablesPath + "\\libgomp-1.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert9.deb", MainExecutablesPath + "\\libHalf-2_2.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert10.deb", MainExecutablesPath + "\\libhogweed-4.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert11.deb", MainExecutablesPath + "\\libiconv-2.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert12.deb", MainExecutablesPath + "\\libidn-11.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert13.deb", MainExecutablesPath + "\\libIex-2_2.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert14.deb", MainExecutablesPath + "\\libIlmImf-2_2.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert15.deb", MainExecutablesPath + "\\libIlmThread-2_2.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert16.deb", MainExecutablesPath + "\\libImath-2_2.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert17.deb", MainExecutablesPath + "\\libintl-8.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert18.deb", MainExecutablesPath + "\\libjasper-4.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert19.deb", MainExecutablesPath + "\\libjpeg-8.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert20.deb", MainExecutablesPath + "\\liblzma-5.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert21.deb", MainExecutablesPath + "\\libnettle-6.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert22.deb", MainExecutablesPath + "\\libnghttp2-14.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert23.deb", MainExecutablesPath + "\\libopencv_core320.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert24.deb", MainExecutablesPath + "\\libopencv_imgcodecs320.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert25.deb", MainExecutablesPath + "\\libopencv_imgproc320.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert26.deb", MainExecutablesPath + "\\libopencv_videoio320.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert27.deb", MainExecutablesPath + "\\libp11-kit-0.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert28.deb", MainExecutablesPath + "\\libpng16-16.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert29.deb", MainExecutablesPath + "\\librtmp-1.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert30.deb", MainExecutablesPath + "\\libssh2-1.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert31.deb", MainExecutablesPath + "\\libstdc++-6.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert32.deb", MainExecutablesPath + "\\libtasn1-6.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert33.deb", MainExecutablesPath + "\\libtiff-5.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert34.deb", MainExecutablesPath + "\\libunistring-2.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert35.deb", MainExecutablesPath + "\\libwebp-6.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert36.deb", MainExecutablesPath + "\\libwinpthread-1.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert37.deb", MainExecutablesPath + "\\ssleay32.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert38.deb", MainExecutablesPath + "\\tbb.dll", true);
                    System.IO.File.Copy(gmicPath + "\\convert39.deb", MainExecutablesPath + "\\zlib1.dll", true);

                    System.IO.File.Copy(imageMagicPath + "\\convert", MainExecutablesPath + "\\convert.exe", true);
                    //System.IO.File.Copy(imageMagicPath + "\\ct1", MainExecutablesPath + "\\ct1.exe", true);
                    System.IO.File.Copy(imageMagicPath + "\\composite", MainExecutablesPath + "\\composite.exe", true); //Composite
                    System.IO.File.Copy(imageMagicPath + "\\ptce", MainExecutablesPath + "\\ptce.exe", true); //Potrace
                    System.IO.File.Copy(imageMagicPath + "\\cartoonize1", MainExecutablesPath + "\\potrace.1", true);

                    System.IO.File.Copy(imageMagicPath + "\\atl90.dll", MainExecutablesPath + "\\atl90.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\mfc90.dll", MainExecutablesPath + "\\mfc90.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\msvcp90.dll", MainExecutablesPath + "\\msvcp90.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\msvcr90.dll", MainExecutablesPath + "\\msvcr90.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\vcomp90.dll", MainExecutablesPath + "\\vcomp90.dll", true);
                    //System.IO.File.Copy(imageMagicPath + "\\vcomp100.dll", MainExecutablesPath + "\\vcomp100.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\X11.dll", MainExecutablesPath + "\\X11.dll", true);

                    //Autotrace fichiers
                    System.IO.File.Copy(imageMagicPath + "\\auto", MainExecutablesPath + "\\automaticktrace.exe", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto1", MainExecutablesPath + "\\CORE_RL_hdf_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto2", MainExecutablesPath + "\\CORE_RL_lcms_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto3", MainExecutablesPath + "\\CORE_RL_magick_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto4", MainExecutablesPath + "\\CORE_RL_ttf_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto5", MainExecutablesPath + "\\CORE_RL_xlib_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto6", MainExecutablesPath + "\\delegates.mgk", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto7", MainExecutablesPath + "\\fonts.mgk", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto8", MainExecutablesPath + "\\IM_MOD_RL_8bim_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto9", MainExecutablesPath + "\\IM_MOD_RL_art_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto10", MainExecutablesPath + "\\IM_MOD_RL_avi_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto11", MainExecutablesPath + "\\IM_MOD_RL_avs_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto12", MainExecutablesPath + "\\IM_MOD_RL_bmp_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto13", MainExecutablesPath + "\\IM_MOD_RL_cmyk_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto14", MainExecutablesPath + "\\IM_MOD_RL_cut_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto15", MainExecutablesPath + "\\IM_MOD_RL_dcm_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto16", MainExecutablesPath + "\\IM_MOD_RL_dps_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto17", MainExecutablesPath + "\\IM_MOD_RL_dpx_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto18", MainExecutablesPath + "\\IM_MOD_RL_ept_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto19", MainExecutablesPath + "\\IM_MOD_RL_fax_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto20", MainExecutablesPath + "\\IM_MOD_RL_fits_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto21", MainExecutablesPath + "\\IM_MOD_RL_fpx_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto22", MainExecutablesPath + "\\IM_MOD_RL_gif_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto23", MainExecutablesPath + "\\IM_MOD_RL_gradient_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto24", MainExecutablesPath + "\\IM_MOD_RL_gray_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto25", MainExecutablesPath + "\\IM_MOD_RL_hdf_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto26", MainExecutablesPath + "\\IM_MOD_RL_histogram_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto27", MainExecutablesPath + "\\IM_MOD_RL_html_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto28", MainExecutablesPath + "\\IM_MOD_RL_icm_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto29", MainExecutablesPath + "\\IM_MOD_RL_icon_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto30", MainExecutablesPath + "\\IM_MOD_RL_iptc_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto31", MainExecutablesPath + "\\IM_MOD_RL_jbig_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto32", MainExecutablesPath + "\\IM_MOD_RL_jpeg_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto33", MainExecutablesPath + "\\IM_MOD_RL_label_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto34", MainExecutablesPath + "\\IM_MOD_RL_logo_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto35", MainExecutablesPath + "\\IM_MOD_RL_map_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto36", MainExecutablesPath + "\\IM_MOD_RL_matte_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto37", MainExecutablesPath + "\\IM_MOD_RL_miff_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto38", MainExecutablesPath + "\\IM_MOD_RL_mono_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto39", MainExecutablesPath + "\\IM_MOD_RL_mpc_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto40", MainExecutablesPath + "\\IM_MOD_RL_mtv_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto41", MainExecutablesPath + "\\IM_MOD_RL_mvg_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto42", MainExecutablesPath + "\\IM_MOD_RL_null_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto43", MainExecutablesPath + "\\IM_MOD_RL_pcd_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto44", MainExecutablesPath + "\\IM_MOD_RL_pcl_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto45", MainExecutablesPath + "\\IM_MOD_RL_pcx_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto46", MainExecutablesPath + "\\IM_MOD_RL_pdb_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto47", MainExecutablesPath + "\\IM_MOD_RL_pdf_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto48", MainExecutablesPath + "\\IM_MOD_RL_pict_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto49", MainExecutablesPath + "\\IM_MOD_RL_pix_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto50", MainExecutablesPath + "\\IM_MOD_RL_plasma_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto51", MainExecutablesPath + "\\IM_MOD_RL_png_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto52", MainExecutablesPath + "\\IM_MOD_RL_pnm_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto53", MainExecutablesPath + "\\IM_MOD_RL_preview_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto54", MainExecutablesPath + "\\IM_MOD_RL_ps2_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto55", MainExecutablesPath + "\\IM_MOD_RL_ps3_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto56", MainExecutablesPath + "\\IM_MOD_RL_psd_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto57", MainExecutablesPath + "\\IM_MOD_RL_ps_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto58", MainExecutablesPath + "\\IM_MOD_RL_pwp_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto59", MainExecutablesPath + "\\IM_MOD_RL_rgb_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto60", MainExecutablesPath + "\\IM_MOD_RL_rla_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto61", MainExecutablesPath + "\\IM_MOD_RL_rle_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto62", MainExecutablesPath + "\\IM_MOD_RL_sct_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto63", MainExecutablesPath + "\\IM_MOD_RL_sfw_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto64", MainExecutablesPath + "\\IM_MOD_RL_sgi_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto65", MainExecutablesPath + "\\IM_MOD_RL_stegano_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto66", MainExecutablesPath + "\\IM_MOD_RL_sun_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto67", MainExecutablesPath + "\\IM_MOD_RL_svg_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto68", MainExecutablesPath + "\\IM_MOD_RL_tga_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto69", MainExecutablesPath + "\\IM_MOD_RL_tiff_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto70", MainExecutablesPath + "\\IM_MOD_RL_tile_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto71", MainExecutablesPath + "\\IM_MOD_RL_tim_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto72", MainExecutablesPath + "\\IM_MOD_RL_ttf_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto73", MainExecutablesPath + "\\IM_MOD_RL_txt_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto74", MainExecutablesPath + "\\IM_MOD_RL_uil_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto75", MainExecutablesPath + "\\IM_MOD_RL_url_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto76", MainExecutablesPath + "\\IM_MOD_RL_uyvy_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto77", MainExecutablesPath + "\\IM_MOD_RL_vicar_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto78", MainExecutablesPath + "\\IM_MOD_RL_vid_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto79", MainExecutablesPath + "\\IM_MOD_RL_viff_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto80", MainExecutablesPath + "\\IM_MOD_RL_wbmp_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto81", MainExecutablesPath + "\\IM_MOD_RL_wmf_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto82", MainExecutablesPath + "\\IM_MOD_RL_wpg_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto83", MainExecutablesPath + "\\IM_MOD_RL_xbm_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto84", MainExecutablesPath + "\\IM_MOD_RL_xc_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto85", MainExecutablesPath + "\\IM_MOD_RL_xpm_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto86", MainExecutablesPath + "\\IM_MOD_RL_xwd_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto87", MainExecutablesPath + "\\IM_MOD_RL_x_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto88", MainExecutablesPath + "\\IM_MOD_RL_yuv_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto89", MainExecutablesPath + "\\LIBR_RL_BZLIB_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto90", MainExecutablesPath + "\\LIBR_RL_FPXjpeg_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto91", MainExecutablesPath + "\\LIBR_RL_FPX_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto92", MainExecutablesPath + "\\LIBR_RL_JBIG_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto93", MainExecutablesPath + "\\LIBR_RL_JPEG_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto94", MainExecutablesPath + "\\LIBR_RL_PNG_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto95", MainExecutablesPath + "\\LIBR_RL_TIFF_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto96", MainExecutablesPath + "\\LIBR_RL_ZLIB_.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto97", MainExecutablesPath + "\\magic.mgk", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto98", MainExecutablesPath + "\\modules.mgk", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto99", MainExecutablesPath + "\\COPYING.LIB", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto100", MainExecutablesPath + "\\msvcp70.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto101", MainExecutablesPath + "\\msvcr70.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto102", MainExecutablesPath + "\\pstoedit.dll", true);
                    System.IO.File.Copy(imageMagicPath + "\\auto103", MainExecutablesPath + "\\stddrivers.dll", true);
                    //Autotrace fichiers

                    System.IO.File.Copy(imageMagicPath + "\\cartoonize12589.tis", MainExecutablesPath + "\\colors.xml", true);
                    System.IO.File.Copy(imageMagicPath + "\\cartoonize13589.tis", MainExecutablesPath + "\\configure.xml", true);
                    System.IO.File.Copy(imageMagicPath + "\\cartoonize14589.tis", MainExecutablesPath + "\\delegates.xml", true);
                    System.IO.File.Copy(imageMagicPath + "\\cartoonize15589.tis", MainExecutablesPath + "\\english.xml", true);
                    System.IO.File.Copy(imageMagicPath + "\\cartoonize16589.tis", MainExecutablesPath + "\\locale.xml", true);
                    System.IO.File.Copy(imageMagicPath + "\\cartoonize17589.tis", MainExecutablesPath + "\\log.xml", true);
                    System.IO.File.Copy(imageMagicPath + "\\cartoonize18589.tis", MainExecutablesPath + "\\magic.xml", true);
                    System.IO.File.Copy(imageMagicPath + "\\cartoonize19589.tis", MainExecutablesPath + "\\mime.xml", true);
                    System.IO.File.Copy(imageMagicPath + "\\cartoonize20589.tis", MainExecutablesPath + "\\thresholds.xml", true);
                    System.IO.File.Copy(imageMagicPath + "\\cartoonize21589.tis", MainExecutablesPath + "\\type.xml", true);
                    System.IO.File.Copy(imageMagicPath + "\\cartoonize22589.tis", MainExecutablesPath + "\\type-ghostscript.xml", true);
                    System.IO.File.Copy(imageMagicPath + "\\cartoonize23589.tis", MainExecutablesPath + "\\sRGB.icm", true);

                    //mask dossier Bin/Debug/IM/
                    Directory.CreateDirectory(MainExecutablesPath + "\\mask");
                    System.IO.File.Copy(imageMagicPath + "\\mask/frame201", MainExecutablesPath + "\\mask/frame201.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/frame202", MainExecutablesPath + "\\mask/frame202.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/frame203", MainExecutablesPath + "\\mask/frame203.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/frame204", MainExecutablesPath + "\\mask/frame204.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/frame205", MainExecutablesPath + "\\mask/frame205.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/frame206", MainExecutablesPath + "\\mask/frame206.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/frame207", MainExecutablesPath + "\\mask/frame207.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/frame208", MainExecutablesPath + "\\mask/frame208.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/frame209", MainExecutablesPath + "\\mask/frame209.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/frame210", MainExecutablesPath + "\\mask/frame210.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/frame211", MainExecutablesPath + "\\mask/frame211.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/frame212", MainExecutablesPath + "\\mask/frame212.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/frame213", MainExecutablesPath + "\\mask/frame213.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/texture101", MainExecutablesPath + "\\mask/texture101.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/texture102", MainExecutablesPath + "\\mask/texture102.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/texture102", MainExecutablesPath + "\\mask/texture102.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/texture103", MainExecutablesPath + "\\mask/texture103.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/texture104", MainExecutablesPath + "\\mask/texture104.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/texture105", MainExecutablesPath + "\\mask/texture105.png", true);
                    //System.IO.File.Copy(imageMagicPath + "\\mask/texture106", MainExecutablesPath + "\\mask/texture106.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/note", MainExecutablesPath + "\\mask/note.jpg", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/Background_painting", MainExecutablesPath + "\\mask/Background_painting.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/19", MainExecutablesPath + "\\mask/19.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/20", MainExecutablesPath + "\\mask/20.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/21", MainExecutablesPath + "\\mask/21.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/23", MainExecutablesPath + "\\mask/23.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/mask6", MainExecutablesPath + "\\mask/mask6.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/mask7", MainExecutablesPath + "\\mask/mask7.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/mask1", MainExecutablesPath + "\\mask/mask1.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/mask2", MainExecutablesPath + "\\mask/mask2.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/mask8", MainExecutablesPath + "\\mask/mask8.png", true);
                    //System.IO.File.Copy(imageMagicPath + "\\mask/p9", MainExecutablesPath + "\\mask/p9.jpg", true);
                    //System.IO.File.Copy(imageMagicPath + "\\mask/p12", MainExecutablesPath + "\\mask/p12.jpg", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/stripe2", MainExecutablesPath + "\\mask/stripe2.png", true);
                    
                    System.IO.File.Copy(imageMagicPath + "\\mask/p43", MainExecutablesPath + "\\mask/p43.jpg", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/p44", MainExecutablesPath + "\\mask/p44.jpg", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/p45", MainExecutablesPath + "\\mask/p45.jpg", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/p46", MainExecutablesPath + "\\mask/p46.jpg", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/color1", MainExecutablesPath + "\\mask/color1.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/color2", MainExecutablesPath + "\\mask/color2.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/c32", MainExecutablesPath + "\\mask/c32.jpg", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/c33", MainExecutablesPath + "\\mask/c33.jpg", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/mask35", MainExecutablesPath + "\\mask/mask35.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/rain", MainExecutablesPath + "\\mask/rain.png", true);

                    System.IO.File.Copy(imageMagicPath + "\\mask/l1", MainExecutablesPath + "\\mask/l1.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/l2", MainExecutablesPath + "\\mask/l2.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/l3", MainExecutablesPath + "\\mask/l3.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/color3", MainExecutablesPath + "\\mask/color3.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/Background7", MainExecutablesPath + "\\mask/Background7.jpg", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/m7it", MainExecutablesPath + "\\mask/m7it.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/m1", MainExecutablesPath + "\\mask/m1.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/m8", MainExecutablesPath + "\\mask/m8.png", true);
                    System.IO.File.Copy(imageMagicPath + "\\mask/texture_5", MainExecutablesPath + "\\mask/texture_5.gif", true);

                    //System.IO.File.Copy(imageMagicPath + "\\mask/spc", MainExecutablesPath + "\\mask/spc.png", true);
                    for (int i = 401; i <= nombre_splatters; i++)
                    {
                        System.IO.File.Copy(imageMagicPath + "\\mask/sp" + i, MainExecutablesPath + "\\mask/sp" + i + ".png", true);
                        System.IO.File.Copy(imageMagicPath + "\\mask/spc" + i, MainExecutablesPath + "\\mask/spc" + i + ".png", true);
                    }

                    //==================Transfert du fichier update203.gmic===============================//
                    string gmic = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\gmic\\";
                    Directory.CreateDirectory(gmic); //Bilal création du dossier gmic.
                    string updategmicfile = gmic + "update203.gmic";
                    System.IO.File.Copy(gmicPath + "\\u203g", updategmicfile, true);
                    //==================Transfert du fichier update203.gmic End===============================//

                    IS_INITIALIZED = true;
                    //}
                }
                finally
                {
                    try
                    {
                        //   Directory.Delete(MainExecutablesPath, true);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        private static string GetGmicExecutablePath()
        {
            string exePath = System.IO.Path.GetDirectoryName(Assembly.GetAssembly(typeof(MainControl)).Location);

            return System.IO.Path.Combine(exePath, "C32");
        }

        private static string GetImageMagicExecutablePath()
        {
            string exePath = System.IO.Path.GetDirectoryName(Assembly.GetAssembly(typeof(MainControl)).Location);

            return System.IO.Path.Combine(exePath, "IM");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            InitCartoonizer();
            Dispatcher.BeginInvoke((Action)(() =>
            {
                drawingCanvas.DeleteAll();
                //drawingCanvasOrig.DeleteAll();
                MemoryManage.minimizeMemory();
            }));

            ShowAbout();
        }




        private void ShowAbout()
        {
            MainWindow mainWindow = VisualTreeHelperExtensions.FindAncestor<Window>(this) as MainWindow;

            if (new Auth().Activate())
            {
                mainWindow.ShowAbout();
            }
            else
            {
                mainWindow.CheckForRegistration();
            }

        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.HelpUrl);
        }
        private void btnSoftwares_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.SoftwaresUrl);
        }

        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int r = (int)slider1.Value;
            int g = (int)slider2.Value;
            int bc = (int)slider3.Value;

            int b = (int)sliderB.Value / 2;
            int c = (int)sliderC.Value;
            int i = (int)sliderI.Value;
            drawingCanvas.AdjustBrightness(b, c, i, r, g, bc);

        }

        private void Slider2_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int r = (int)slider1.Value;
            int g = (int)slider2.Value;
            int bc = (int)slider3.Value;

            int b = (int)sliderB.Value / 2;
            int c = (int)sliderC.Value;
            int i = (int)sliderI.Value;
            drawingCanvas.AdjustBrightness(b, c, i, r, g, bc);
        }

        private void sliderI_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            sliderIValue = (int)sliderI.Value;
            int r = (int)slider1.Value;
            int g = (int)slider2.Value;
            int bc = (int)slider3.Value;

            int b = (int)sliderB.Value / 2;
            int c = (int)sliderC.Value;
            int i = (int)sliderI.Value;
            drawingCanvas.AdjustBrightness(b, c, i, r, g, bc);
            //if (sliderI.Value < 99)
            //{
            //    //var arr = OriginalImage.Split('\\');
            //    //arr[9] = 
            //    string tempS = CartoonizeHelper._EffectDirectory + "tempfiles\\" + System.IO.Path.GetRandomFileName() + DateTime.Now.ToString("ddMM_hhmmss") + ".jpg";
            //    //string tempS = System.IO.Path.GetTempFileName();
            //    tempS = tempS.Substring(0, tempS.Length - 3) + "jpg"; //Bilal PNG TO JPG TEMP FOLDER
            //    if (File.Exists(tempS))
            //    {
            //        //File.Copy(lastApply, tempS);
            //    }
            //    else
            //    {
            //        File.Copy(OriginalImage, tempS);
            //    }
            //    CartoonizeHelper.CartoonizeImage(MainExecutablesPath, tempS, 555, GetImageMagicExecutablePath(), (int)sliderI.Value);
            //    Dispatcher.BeginInvoke((Action)(() =>
            //    {
            //        drawingCanvas.DeleteAll();
            //        drawingCanvas.SetBackground(tempS);
            //    }));
            //}
        }

        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            sliderZoom.Value = sliderZoom.Value * 1.25;
        }

        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            sliderZoom.Value = sliderZoom.Value / 1.25;
        }

        string lastOpened = null;

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        internal void ShowOpenDialog()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            //this.SetProgressBarVisibility(System.Windows.Visibility.Visible, "Done", false);
            StringBuilder sp = new StringBuilder();
            sp.Append("JPEG Files (*.jpeg)|*.jpeg|");
            sp.Append("PNG Files (*.png)|*.png|");
            sp.Append("JPG Files (*.jpg)|*.jpg|");
            sp.Append("GIF Files (*.gif)|*.gif|");
            sp.Append("All picture files|*.jpeg;*.png;*.jpg;*.gif|");
            sp.Append("All files (*.*)|*.*");
            dlg.Filter = sp.ToString();
            dlg.FilterIndex = 5;

            if (dlg.ShowDialog().GetValueOrDefault())
            {
                //CartoonizeHelper.DeleteEffectImages();
                //CartoonizeHelper._EffectDirectory = System.IO.Path.GetTempPath() + "\\" + System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetRandomFileName()) + "\\"; //Générer un nouveau dossier temp
                //Thread.Sleep(1000);

                Directory.CreateDirectory(MainFolder);
                CartoonizeHelper._EffectDirectory = MainControl.MainFolder + "\\" + System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetRandomFileName()) + "\\"; //Générer un nouveau dossier temp
                ApplyEffectButton1.Visibility = Visibility.Visible; //Bilal afficher bouton appliquer un effet, il sera affiché en permanence, c'est important.
                Button_Open_Image.Visibility = Visibility.Hidden; //Cacher le bouton View full size
                //Menu.Visibility = Visibility.Visible; // pour afficher le menu de gauche après avoir ajouté une photo (pour éviter un bug lors du clique d'un effet avant d'avoir ajouté une photo).
                //effectsPanelPin.Visibility = Visibility.Visible; //pour afficher le petit spin.

                string fileName = dlg.FileName;
                // code changed by anis
                SetProgressBarVisibility(Visibility.Visible, null, true);
                Task.Factory.StartNew(() =>
                {

                    /*
                    if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "tempfiles"))
                     {
                      Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "tempfiles");
                     }
                      string tempfileName = AppDomain.CurrentDomain.BaseDirectory + "tempfiles\\" + DateTime.Now.ToString("ddMM_hhmmss") + ".jpg";
                      */

                    //MainExecutablesPath1 = System.IO.Path.Combine(System.IO.Path.GetTempPath());
                    MainExecutablesPath1 = MainControl.MainFolder + "\\";
                    //System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetRandomFileName()));

                    try
                    {
                        if (!Directory.Exists(CartoonizeHelper._EffectDirectory + "tempfiles\\"))
                        {
                            Directory.CreateDirectory(CartoonizeHelper._EffectDirectory + "tempfiles\\"); //bilal 

                        }
                    }
                    catch (Exception ex)
                    {

                    }


                    string tempfileName = CartoonizeHelper._EffectDirectory + "tempfiles\\" + System.IO.Path.GetRandomFileName() + DateTime.Now.ToString("ddMM_hhmmss") + ".jpg";
                    string tempfileName_canvas = CartoonizeHelper._EffectDirectory + "tempfiles\\canvas.jpg";
                    //bilal resize l'image avant l'upload

                    string imagemagick_original_size = MainExecutablesPath1 + "imagemagick_original_size.jpg";
                    string imagemagick_original_original_size = MainExecutablesPath1 + "imagemagick_original_original_size.jpg";

                    int imageWidth = 0;
                    int imageHeight = 0;

                    using (System.Drawing.Image originalImage = System.Drawing.Image.FromFile(fileName))
                    {
                        imageWidth = originalImage.Width;
                        imageHeight = originalImage.Height;
                        File.Copy(fileName, imagemagick_original_size, true);
                        if (imageWidth >= imageHeight)
                        {
                            if (imageWidth >= 1024)
                            {
                                System.Drawing.Bitmap resizedImage;
                                float ratio = Conversion_Value / (float)imageWidth;
                                float height = (float)imageHeight * ratio;
                                int imageHeight_new = (int)height;
                                resizedImage = new System.Drawing.Bitmap(originalImage, new System.Drawing.Size(Conversion_Value, imageHeight_new));
                                /*
                                    var encoderParameters = new EncoderParameters(1);
                                    encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 50L);
                                    resizedImage.Save(tempfileName, GetEncoder(ImageFormat.Jpeg), encoderParameters);
                                    */
                                resizedImage.Save(tempfileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                resizedImage.Dispose();

                                System.Drawing.Bitmap resizedImage_canvas;
                                float ratio_canvas = 1500 / (float)imageWidth;
                                float height_canvas = (float)imageHeight * ratio_canvas;
                                int imageHeight_new_cavas = (int)height_canvas;
                                resizedImage_canvas = new System.Drawing.Bitmap(originalImage, new System.Drawing.Size(1500, imageHeight_new_cavas));
                                resizedImage_canvas.Save(tempfileName_canvas, System.Drawing.Imaging.ImageFormat.Jpeg);
                                resizedImage_canvas.Dispose();

                                string fileName_canvas = tempfileName_canvas;
                                Dispatcher.BeginInvoke((Action)(() =>
                                {
                                    drawingCanvas.DeleteAll();
                                    drawingCanvas.SetBackground(tempfileName_canvas);
                                    //drawingCanvasOrig.DeleteAll();
                                    //drawingCanvasOrig.SetBackground(tempfileName_canvas);
                                    //ApplyEffect(0, null); //Bilal resize image pour le canvas 
                                    MemoryManage.minimizeMemory();
                                    SetProgressBarVisibility(Visibility.Collapsed, null, false);
                                    string defaultEffect = ReadDefaultEffect();
                                    Menu.Visibility = Visibility.Visible; // pour afficher le menu de gauche après avoir ajouté une photo (pour éviter un bug lors du clique d'un effet avant d'avoir ajouté une photo).   
                                    MemoryManage.minimizeMemory();
                                    drawingCanvas.UpdateLayout();
                                    //drawingCanvasOrig.UpdateLayout();
                                }));
                                //File.Copy(tempfileName, imagemagick_original_size, true);
                            }
                            else if (imageWidth < 1024)
                            {
                                System.Drawing.Bitmap resizedImage;
                                float ratio = 1450 / (float)imageWidth;
                                float height = (float)imageHeight * ratio;
                                int imageHeight_new = (int)height;
                                resizedImage = new System.Drawing.Bitmap(originalImage, new System.Drawing.Size(1450, imageHeight_new));
                                resizedImage.Save(tempfileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                resizedImage.Dispose();
                                Dispatcher.BeginInvoke((Action)(() =>
                                {
                                    drawingCanvas.DeleteAll();
                                    drawingCanvas.SetBackground(fileName);
                                    //drawingCanvasOrig.DeleteAll();
                                    //drawingCanvasOrig.SetBackground(fileName);
                                    MemoryManage.minimizeMemory();
                                    SetProgressBarVisibility(Visibility.Collapsed, null, false);
                                    string defaultEffect = ReadDefaultEffect();
                                    Menu.Visibility = Visibility.Visible; // pour afficher le menu de gauche après avoir ajouté une photo (pour éviter un bug lors du clique d'un effet avant d'avoir ajouté une photo).
                                    MemoryManage.minimizeMemory();
                                    drawingCanvas.UpdateLayout();
                                    //drawingCanvasOrig.UpdateLayout();
                                }));
                                //File.Copy(fileName, imagemagick_original_size, true);
                            }
                        }



                        else if (imageWidth < imageHeight)
                        {
                            if (imageHeight >= 1024)
                            {
                                System.Drawing.Bitmap resizedImage;
                                float ratio = Conversion_Value / (float)imageHeight;
                                float width = (float)imageWidth * ratio;
                                int imageWidth_new = (int)width;
                                resizedImage = new System.Drawing.Bitmap(originalImage, new System.Drawing.Size(imageWidth_new, Conversion_Value));
                                resizedImage.Save(tempfileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                resizedImage.Dispose();

                                System.Drawing.Bitmap resizedImage_canvas;
                                float ratio_canvas = 1500 / (float)imageHeight;
                                float width_canvas = (float)imageWidth * ratio_canvas;
                                int imageWidth_new_cavas = (int)width_canvas;
                                resizedImage_canvas = new System.Drawing.Bitmap(originalImage, new System.Drawing.Size(imageWidth_new_cavas, 1500));
                                resizedImage_canvas.Save(tempfileName_canvas, System.Drawing.Imaging.ImageFormat.Jpeg);
                                resizedImage_canvas.Dispose();

                                string fileName_canvas = tempfileName_canvas;
                                Dispatcher.BeginInvoke((Action)(() =>
                                {
                                    drawingCanvas.DeleteAll();
                                    drawingCanvas.SetBackground(tempfileName_canvas);
                                    //drawingCanvasOrig.DeleteAll();
                                    //drawingCanvasOrig.SetBackground(tempfileName_canvas);
                                    //ApplyEffect(0, null); //Bilal resize image pour le canvas 
                                    MemoryManage.minimizeMemory();
                                    SetProgressBarVisibility(Visibility.Collapsed, null, false);
                                    string defaultEffect = ReadDefaultEffect();
                                    Menu.Visibility = Visibility.Visible; // pour afficher le menu de gauche après avoir ajouté une photo (pour éviter un bug lors du clique d'un effet avant d'avoir ajouté une photo).   
                                    MemoryManage.minimizeMemory();
                                    drawingCanvas.UpdateLayout();
                                    //drawingCanvasOrig.UpdateLayout();
                                }));
                                //File.Copy(tempfileName, imagemagick_original_size, true);
                            }
                            else if (imageHeight < 1024)
                            {
                                System.Drawing.Bitmap resizedImage;
                                float ratio = 1450 / (float)imageHeight;
                                float width = (float)imageWidth * ratio;
                                int imageWidth_new = (int)width;
                                resizedImage = new System.Drawing.Bitmap(originalImage, new System.Drawing.Size(imageWidth_new, 1450));
                                resizedImage.Save(tempfileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                resizedImage.Dispose();
                                Dispatcher.BeginInvoke((Action)(() =>
                                {
                                    drawingCanvas.DeleteAll();
                                    drawingCanvas.SetBackground(fileName);
                                    //drawingCanvasOrig.DeleteAll();
                                    //drawingCanvasOrig.SetBackground(fileName);
                                    MemoryManage.minimizeMemory();
                                    SetProgressBarVisibility(Visibility.Collapsed, null, false);
                                    string defaultEffect = ReadDefaultEffect();
                                    Menu.Visibility = Visibility.Visible; // pour afficher le menu de gauche après avoir ajouté une photo (pour éviter un bug lors du clique d'un effet avant d'avoir ajouté une photo).
                                    MemoryManage.minimizeMemory();
                                    drawingCanvas.UpdateLayout();
                                    //drawingCanvasOrig.UpdateLayout();
                                }));
                                //File.Copy(fileName, imagemagick_original_size, true);
                            }
                        }
                    }

                    MemoryManage.minimizeMemory();
                    fileName = tempfileName;
                    lastOpened = fileName;
                    lastApply = fileName;
                    string imagemagick_new_size = MainExecutablesPath1 + "imagemagick_new_size.jpg";
                    File.Copy(tempfileName, imagemagick_new_size, true);

                    /*
                    Dispatcher.BeginInvoke((Action)(() =>
                    {
                        drawingCanvas.DeleteAll();
                        if (imageWidth > 1500)
                        {
                            drawingCanvas.SetBackground(tempfileName_canvas);
                            //ApplyEffect(0, null); //Bilal resize image pour le canvas 
                            //UpdateIcons(lastOpened);
                            MemoryManage.minimizeMemory();
                            SetProgressBarVisibility(Visibility.Collapsed, null, false);
                            string defaultEffect = ReadDefaultEffect();
                            /*
                            Dispatcher.BeginInvoke((Action)(() =>
                            {
                                //Thread.Sleep(1000);                         
                                progressbartext_Update.Visibility = Visibility.Visible; //Bilal afficher la barre de progression pendant l'update et l'upload de l'image
                                progressbartext_Update.Visibility = System.Windows.Visibility.Visible;
                                //progressbartext_Update.Text = "\r\n We adjust your image quality...\r\n Please wait..\r\n";
                                progressbartext_Update.Text = "\r\n Checking image quality in progress...\r\n Please wait..\r\n";
                            }));*/
                    /*
                    Menu.Visibility = Visibility.Visible; // pour afficher le menu de gauche après avoir ajouté une photo (pour éviter un bug lors du clique d'un effet avant d'avoir ajouté une photo).
                }
                else
                {
                    drawingCanvas.SetBackground(fileName); //Bilal désactivé pour contourner le problème de crash pour le chargement des grandes images
                    //UpdateIcons(lastOpened);
                    MemoryManage.minimizeMemory();
                    SetProgressBarVisibility(Visibility.Collapsed, null, false);
                    string defaultEffect = ReadDefaultEffect();

                    Menu.Visibility = Visibility.Visible; // pour afficher le menu de gauche après avoir ajouté une photo (pour éviter un bug lors du clique d'un effet avant d'avoir ajouté une photo).
                }
                MemoryManage.minimizeMemory();
                SetProgressBarVisibility(Visibility.Collapsed, null, false);
                MemoryManage.minimizeMemory();
                drawingCanvas.UpdateLayout();
            }));*/



                });

                sp.Clear();
                sp = null;
                dlg = null;  //not working
                GC.Collect();
            }
            else
            {
                sp.Clear();
                sp = null;
                dlg = null;  //not working
                GC.Collect();
            }


        }

        //Image resizing
        public static System.Drawing.Image ResizeImage(int maxWidth, int maxHeight, System.Drawing.Image Image)
        {
            int width = Image.Width;
            int height = Image.Height;

            //return the resized image
            return Image.GetThumbnailImage(width, height, null, IntPtr.Zero);
            //return the original resized image
        }

        //Anis
        public System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, System.Drawing.Size size)
        {
            int sourceWidth = size.Width; //imgToResize.Width;
            int sourceHeight = size.Height;//imgToResize.Height;

            Bitmap b = new Bitmap(sourceWidth, sourceHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            g.DrawImage(imgToResize, 0, 0, sourceWidth, sourceHeight);
            g.Dispose();

            return (System.Drawing.Image)b;
        }
        //Fin Anis

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            StackPanel sp = sender as StackPanel;
            ToggleButton tb = sp.Tag as ToggleButton;
            Thickness t = (Thickness)tb.Tag;

            if (tb.IsChecked.GetValueOrDefault())
            {
                ThicknessAnimation sboard = new ThicknessAnimation(
                    t,
                    new Duration(new TimeSpan(0, 0, 0, 0, 400)),
                    FillBehavior.HoldEnd);
                sboard.BeginTime = new TimeSpan(0, 0, 0, 0, 600);
                sboard.AccelerationRatio = 1;

                sp.BeginAnimation(StackPanel.MarginProperty, sboard);
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (lastOpened != null)
            {
                drawingCanvas.DeleteAll();
                drawingCanvas.SetBackground(lastOpened);
                //drawingCanvasOrig.DeleteAll();
                //drawingCanvasOrig.SetBackground(lastOpened);
                SetProgressBarVisibility(Visibility.Visible, null, false);
                ApplyEffect(308, null); //Bilal apply effect308 pour le bouton Reload pour éviter le bug avec la nouvelle taille 4096
                //UpdateIcons(lastOpened);
                MemoryManage.minimizeMemory();
                lastApply = lastOpened;
            }
        }

        private void Button_undo(object sender, RoutedEventArgs e)
        {

            if (lastOpened != null)
            {
                //SetProgressBarVisibility(Visibility.Visible, null, true);
                ApplyEffect(307, null); //Bilal apply effect307 pour le bouton Undo pour éviter le bug avec la nouvelle taille 4096
                //drawingCanvas.DeleteAll();
                drawingCanvas.SetBackground(lastApply);
                //drawingCanvasOrig.SetBackground(lastApply);
                //UpdateIcons(lastApply);
                MemoryManage.minimizeMemory();
                Button_Undo.Visibility = Visibility.Hidden;
                SetProgressBarVisibility(Visibility.Hidden, null, false);
                progressbartext.Visibility = Visibility.Visible;
                progressbartext.Visibility = System.Windows.Visibility.Visible;
                progressbartext_Update.Visibility = Visibility.Hidden;
                progressbartext_Update.Visibility = System.Windows.Visibility.Hidden;
                WaitingImage.Visibility = System.Windows.Visibility.Hidden;
                NewProgressBar_Vide.Visibility = System.Windows.Visibility.Hidden;
                NewProgressBar_Plein.Visibility = System.Windows.Visibility.Hidden;
                NewProgressBar_Plein.Visibility = Visibility.Hidden;
                NewProgressBar_Vide.Visibility = Visibility.Hidden;
            }

        }

        private void Button_Click_Open_Image(object sender, RoutedEventArgs e)
        {
            //Process.Start(System.IO.Path.GetTempPath() + "final_PC_saved.jpg");
            Process.Start(MainControl.MainFolder + "//final_PC_saved.jpg");
        }


        private void UpdateIcons(string lastOpened)
        {
            /*Task.Factory.StartNew(() =>
            {
                Bitmap bmp = new Bitmap(System.Drawing.Image.FromFile(lastOpened),new System.Drawing.Size(100, 100));

                AForge.Imaging.Image.FormatImage(ref bmp);
                // image type

                for (int t = 0; t <= AFrogeHelper.FILTERS_COUNT; t++)
                {
                    // int t = (int)item.Tag;
                    Bitmap thumb = AFrogeHelper.GetThumbnail(bmp, t, 50, null);

                    Dispatcher.BeginInvoke(
                        new Action<Bitmap, Toolbox, int>(ProcessImage), new object[] { thumb, toolboxEffects2, t });
                }
                
            });


            Task.Factory.StartNew(() =>
            {
                Bitmap bmp2 = new Bitmap(System.Drawing.Image.FromFile(lastOpened), new System.Drawing.Size(100, 100));

                AForge.Imaging.Image.FormatImage32(ref bmp2);

                for (int t = 0; t < AFrogeHelper.INSTAGRAM_EFFECTS_COUNT; t++)
                {
                    Bitmap overlayImage = CartoonizeHelper.GetImae(t + 100);
                    // int t = (int)item.Tag;
                    Bitmap thumb = AFrogeHelper.GetThumbnail(bmp2, AFrogeHelper.MERGE_FILTER_ID, 50, overlayImage);

                    Dispatcher.BeginInvoke(
                        new Action<Bitmap, Toolbox, int>(ProcessImage), new object[] { thumb, toolboxInstagramEffects, t });
                }
            });

            Task.Factory.StartNew(() =>
            {
                Bitmap bmp2 = new Bitmap(System.Drawing.Image.FromFile(lastOpened), new System.Drawing.Size(100, 100));

                AForge.Imaging.Image.FormatImage32(ref bmp2);

                for (int t = 0; t < AFrogeHelper.VARIOUS_FRAMES_COUNT; t++)
                {
                    Bitmap overlayImage = CartoonizeHelper.GetImae(t + 300);
                    // int t = (int)item.Tag;
                    Bitmap thumb = AFrogeHelper.GetThumbnail(bmp2, AFrogeHelper.MERGE_FILTER_ID, 50, overlayImage);

                    Dispatcher.BeginInvoke(
                        new Action<Bitmap, Toolbox, int>(ProcessImage), new object[] { thumb, toolboxVariousFrames, t });
                }

            });

            */
            MemoryManage.minimizeMemory();
        }

        private void ProcessImage(Bitmap m, Toolbox toolBox, int tVal)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                m.Save(memory, System.Drawing.Imaging.ImageFormat.Jpeg);

                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                Button item = toolBox.Items[tVal] as Button;
                System.Windows.Controls.Image img = item.Content as System.Windows.Controls.Image;
                img.Source = bitmapImage;
            }


            MemoryManage.minimizeMemory();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            var pdialog = new PrintDialog();
            if (pdialog.ShowDialog() == true)
            {
                var bi = drawingCanvas.GetImageSource();

                var vis = new DrawingVisual();
                var dc = vis.RenderOpen();
                dc.DrawImage(bi, new Rect
                {
                    Width = bi.Width,
                    Height = bi.Height,
                    X = Math.Max(0, (pdialog.PrintableAreaWidth - bi.Width) / 2),
                    Y = Math.Max(0, (pdialog.PrintableAreaHeight - bi.Height) / 2)
                });
                dc.Close();

                pdialog.PrintVisual(vis, "My Image");
            }
        }
        FacebookLogin fbLogin = new FacebookLogin();

        private void btnShare_Click(object sender, RoutedEventArgs e)
        {
            if (!fbLogin.IsLoggedIn)
            {
                fbLogin.ShowDialog();
            }
            if (fbLogin.IsLoggedIn)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                SetProgressBarVisibility(Visibility.Visible, null, false);
                System.Drawing.Image image = drawingCanvas.ExportToImage();

                tempSForUpload = System.IO.Path.GetTempFileName();
                tempSForUpload = tempSForUpload.Substring(0, tempSForUpload.Length - 3) + "jpg";

                image.Save(tempSForUpload, ImageFormat.Jpeg);
                worker.RunWorkerAsync();

            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool r = (bool)e.Result;
            SetProgressBarVisibility(Visibility.Hidden, null, true);

        }
        string tempSForUpload = "";
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                e.Result = false;
                e.Result = PostImage(fbLogin.AccessToken, "https://www.facebook.com/converttocartoon", tempSForUpload);
            }
            catch (Exception ex)
            {
                e.Result = false;
            }
        }

        public bool PostImage(string AccessToken, string Status, string ImagePath)
        {
            try
            {
                FacebookClient fb = new FacebookClient(AccessToken);

                var imgstrema = File.OpenRead(ImagePath);

                dynamic res = fb.Post("/me/photos", new
                {

                    message = Status,
                    file = new FacebookMediaStream
                    {
                        ContentType = "image/jpg",
                        FileName = System.IO.Path.GetFileName(ImagePath)
                    }.SetValue(imgstrema)

                });

                return true;
            }
            catch
            {

                return false;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            try
            {
                //CartoonizeHelper.DeleteEffectImages(); //Bilal pour effacer le dossier PrimaCartoonizer dans Temp afin d'appliquer les nouveaux changements.
                //CartoonizeHelper._EffectDirectory = System.IO.Path.GetTempPath() + "\\" + System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetRandomFileName()) + "\\"; //Générer un nouveau dossier temp

                CartoonizeHelper._EffectDirectory = MainControl.MainFolder + "\\" + System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetRandomFileName()) + "\\"; //Générer un nouveau dossier temp
                if (!Directory.Exists(CartoonizeHelper._EffectDirectory + "tempfiles\\"))
                {
                    Directory.CreateDirectory(CartoonizeHelper._EffectDirectory + "tempfiles\\"); //bilal recréer le dossier afin d'appliquer les changements sur l'image dans le dossier FXCartoonizer

                }
            }
            catch (Exception ex)
            {

            }

            this.SetProgressBarVisibility(System.Windows.Visibility.Visible, "Done", false);
            progressbartext.Visibility = Visibility.Hidden;
            progressbartext.Visibility = System.Windows.Visibility.Hidden;
            System.Drawing.Image image = drawingCanvas.ExportToImage();

            try
            {
                string tempS = System.IO.Path.GetTempFileName();
                tempS = tempS.Substring(0, tempS.Length - 3) + "jpg";
                image.Save(tempS, ImageFormat.Jpeg);
                lastApply = tempS;
                drawingCanvas.DeleteAll();
                drawingCanvas.SetBackground(lastApply);
                //drawingCanvasOrig.DeleteAll();
                //drawingCanvasOrig.SetBackground(lastApply);
                ApplyEffectButton.Visibility = Visibility.Hidden;
                Button_Undo.Visibility = Visibility.Hidden;

                MemoryManage.minimizeMemory();

                ApplyEffect(306, null); //Bilal appliquer l'effet 306 après avoir appuyé sur le bouton Apply effect
            }
            catch (Exception ex)
            {

            }
            /*
            ApplyEffect(1, null);
            SaveDefaultEffect("0");
            progressbartext_Update.Visibility = Visibility.Visible; //Bilal afficher la barre de progression pendant l'update et l'upload de la première image
            progressbartext_Update.Visibility = System.Windows.Visibility.Visible;
            progressbartext_Update.Text = "\r\n We update the effects...Please wait...\r\n";
             * */
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.SetProgressBarVisibility(System.Windows.Visibility.Visible, "Done", false);
            progressbartext.Visibility = Visibility.Hidden;
            progressbartext.Visibility = System.Windows.Visibility.Hidden;
            System.Drawing.Image image = drawingCanvas.ExportToImage();

            string tempS = System.IO.Path.GetTempFileName();
            tempS = tempS.Substring(0, tempS.Length - 3) + "jpg";
            image.Save(tempS, ImageFormat.Jpeg);
            lastApply = tempS;
            drawingCanvas.DeleteAll();
            drawingCanvas.SetBackground(lastApply);
            //drawingCanvasOrig.DeleteAll();
            //drawingCanvasOrig.SetBackground(lastApply);
            ApplyEffectButton.Visibility = Visibility.Hidden;
            Button_Undo.Visibility = Visibility.Hidden;
            MemoryManage.minimizeMemory();
        }

        protected bool Completed { get; set; }


        /// Bilal Method is used to Save Default Effect
        #region Methods
        public static void SaveDefaultEffect(string effectName)
        {
            string effectFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\PrimaCartoonizer\\";

            if (!Directory.Exists(effectFilePath))
                Directory.CreateDirectory(effectFilePath);

            string filePath = effectFilePath + "PrimaCartoonizerEffect0.txt";

            using (System.IO.StreamWriter outfile = new System.IO.StreamWriter(filePath, false))
            {
                outfile.Write(effectName);
            }
        }

        /// <summary>
        /// Read Default Effect
        /// </summary>
        /// <returns></returns>
        public static string ReadDefaultEffect()
        {
            string effect = "";
            string outputFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\PrimaCartoonizer\\";
            if (!Directory.Exists(outputFilePath))
            {
                Directory.CreateDirectory(outputFilePath);

            }
            if (Directory.Exists(outputFilePath))
            {
                string filePath = outputFilePath + "PrimaCartoonizerEffect0.txt";
                if (File.Exists(filePath))
                {
                    using (System.IO.StreamReader outfile = new System.IO.StreamReader(filePath))
                    {
                        effect = outfile.ReadLine();
                    }
                }
            }
            /*-------Copier fichier de définition gmic update203.gmic-----*
            string gmic = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\gmic\\";
            if (!Directory.Exists(gmic))
            {
                Directory.CreateDirectory(gmic); //Bilal création du dossier gmic dans APPData/Roaming si il n'existe pas déja.
            }
            string updategmicfile = gmic + "update203.gmic";
            if (!File.Exists(updategmicfile))
            {
                string gmicPath = GetGmicExecutablePath();
                System.IO.File.Copy(gmicPath + "\\u203g", updategmicfile, true);
            }
            /*-------Fin Copier fichier de définition gmic update203.gmic-----*/
            return effect;
        }

        #endregion
               
    }
}


