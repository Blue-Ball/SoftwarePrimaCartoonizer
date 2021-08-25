using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using PrimaCartoonizer.View;

namespace PrimaCartoonizer
{

    public class CartoonizeHelper
    {
        //public static string _EffectDirectory = System.IO.Path.GetTempPath() + "\\PrimaCartoonizer\\"; 
        //public static string _EffectDirectory = System.IO.Path.GetTempPath() + "\\" + System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetRandomFileName()) + "\\";
        public static string _EffectDirectory = MainControl.MainFolder + "\\" + System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetRandomFileName()) + "\\";
        private static string _path = "";
        public static string path
        {
            get
            {
                if (String.IsNullOrEmpty(_path))
                {

                }
                return _path;
            }
        }
        public static void CartoonizeImage(string tempExePath, string imagePath, int effectType, string imageMagicPath, int percentage = 50, string origFilePath = "")
        {

            string fileName = Path.GetFileName(imagePath);
            string outFile = imagePath;

            decimal factor = 1;
            int width; int heigh;


            if (effectType < 1000)
            {
                //imagePath = ValidateImageSize(imagePath, imageMagicPath, out factor, out  width, out  heigh); //Bilal resizer l'image avant la conversion

                //If effect needs ImageMagice process efect and exit.
                if (!ImageMagicConvert(tempExePath, imagePath, effectType, fileName, outFile, imageMagicPath, percentage, origFilePath))
                {
                    //ExecuteGmic(tempExePath, GetGmicArguments(effectType - 1, imagePath, outFile)); //Bilal désactivation important
                }
            }
            #region old
            /*
        else if (effectType < 2000)
        {
            // imagePath = ValidateImageSize(imagePath, imageMagicPath, out factor, out  width, out  heigh); //Bilal resizer l'image avant la conversion
            Bitmap cartoonized = null;
            Bitmap bmp = new Bitmap(imagePath);

            AForge.Imaging.Image.FormatImage(ref bmp);

            // image type
            if (bmp.PixelFormat == PixelFormat.Format24bppRgb)
            {
                cartoonized = AFrogeHelper.ApplyFilter(bmp, effectType - 1000, null);
                bmp.Dispose();
                bmp = null;

                cartoonized.Save(outFile, ImageFormat.Jpeg);
                cartoonized.Dispose();
                cartoonized = null;
            }
        }
        else if (effectType < 3000)
        {
            //imagePath = ValidateImageSize(imagePath, imageMagicPath, out factor, out  width, out  heigh); //Bilal resizer l'image avant la conversion
            Bitmap cartoonized = null;
            Bitmap bmp = new Bitmap(imagePath);

            AForge.Imaging.Image.FormatImage32(ref bmp);

            // image type
            if (bmp.PixelFormat == PixelFormat.Format32bppArgb)
            {

                cartoonized = AFrogeHelper.ApplyFilter(bmp, AFrogeHelper.MERGE_FILTER_ID, GetImae(effectType - 2000));
                bmp.Dispose();
                bmp = null;

                cartoonized.Save(outFile, ImageFormat.Jpeg);
                cartoonized.Dispose();
                cartoonized = null;
            }

        }

        if (factor != 1)
        {
            //RestoreSize(outFile, imageMagicPath, factor); //Bilal pour restorer la taille d'origine après la conversion
            //  File.Delete(imagePath);
        }
    }

    public static Bitmap GetImae(int p)
    {
        string imagePath = Path.GetDirectoryName(typeof(CartoonizeHelper).Assembly.Location) + "\\Masks";

        if (p < 100)
        {
            imagePath = imagePath + "\\ColorEffects\\";
        }
        else if (p < 200)
        {
            imagePath = imagePath + "\\InstagramEffects\\";
            p -= 100;
        }
        /*else if (p < 300)
        {
            path = path + "\\KidsFrames\\";
            p -= 200;
        }*
        else if (p < 400)
        {
            imagePath = imagePath + "\\VariousFrames\\";
            p -= 300;
        }

        imagePath += "effect_" + p.ToString() + ".png";
        Bitmap bmp = new Bitmap(imagePath);
        return bmp;
        */
            #endregion
        }


        private static bool ImageMagicConvert(string tempExePath, string imagemagick,
             int effectType, string fileName, string final, string imageMagicPath, int percentage = 100, string origFilePath = "")
        {
            bool processed = false;

            _path = _EffectDirectory + "Effect" + effectType.ToString();
            Directory.CreateDirectory(_EffectDirectory); //pour créer le dossier PrimaCartoonizer dans Temp
            Directory.CreateDirectory(_path);
            if (!Directory.Exists(_EffectDirectory))
            {
                Directory.CreateDirectory(_EffectDirectory);
            }
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }

            #region Declaration Effect images
            string Effect = "\"" + Path.Combine(_EffectDirectory + "Effect" + effectType.ToString(), "Effect" + effectType.ToString() + ".jpg") + "\"";
            string EffectTemp = "\"" + Path.Combine(_EffectDirectory + "EffectTemp" + effectType.ToString(), "EffectTemp" + effectType.ToString() + ".jpg") + "\"";
            string EffectCopy = "\"" + Path.Combine(path, "../Effect" + effectType.ToString() + "Copy.jpg") + "\"";
            //string Effect_Same = "\"" + Path.Combine(path, "../Effect" + effectType.ToString() + "Effect_Same") + "\"";

            string Effect_Same1_directory = _EffectDirectory + "Effect_Same1";
            string Effect_Same1 = "\"" + Path.Combine(_EffectDirectory + "Effect_Same1", "Effect_Same1.jpg") + "\"";
            string Effect_Same4_directory = _EffectDirectory + "Effect_Same4";
            string Effect_Same4 = "\"" + Path.Combine(_EffectDirectory + "Effect_Same4", "Effect_Same4.jpg") + "\"";
            string Effect_Same6_directory = _EffectDirectory + "Effect_Same6";
            string Effect_Same6 = "\"" + Path.Combine(_EffectDirectory + "Effect_Same6", "Effect_Same6.jpg") + "\"";
            string Effect_Same8_directory = _EffectDirectory + "Effect_Same8";
            string Effect_Same8 = "\"" + Path.Combine(_EffectDirectory + "Effect_Same8", "Effect_Same8.jpg") + "\"";
            string Effect_Same23_directory = _EffectDirectory + "Effect_Same23";
            string Effect_Same23 = "\"" + Path.Combine(_EffectDirectory + "Effect_Same23", "Effect_Same23.jpg") + "\"";
            string Effect_Same46_directory = _EffectDirectory + "Effect_Same46";
            string Effect_Same46 = "\"" + Path.Combine(_EffectDirectory + "Effect_Same46", "Effect_Same46.jpg") + "\"";
            string Effect_Same50_directory = _EffectDirectory + "Effect_Same50";
            string Effect_Same50 = "\"" + Path.Combine(_EffectDirectory + "Effect_Same50", "Effect_Same50.jpg") + "\"";
            #endregion
            #region Declaration EffectCopy images
            string Effect1Copy = "\"" + Path.Combine(path, "../Effect1Copy.jpg") + "\"";
            string Effect2Copy = "\"" + Path.Combine(path, "../Effect2Copy.jpg") + "\"";
            string Effect3Copy = "\"" + Path.Combine(path, "../Effect3Copy.jpg") + "\"";
            string Effect4Copy = "\"" + Path.Combine(path, "../Effect4Copy.jpg") + "\"";
            string Effect5Copy = "\"" + Path.Combine(path, "../Effect5Copy.jpg") + "\"";
            string Effect6Copy = "\"" + Path.Combine(path, "../Effect6Copy.jpg") + "\"";
            string Effect7Copy = "\"" + Path.Combine(path, "../Effect7Copy.jpg") + "\"";
            string Effect8Copy = "\"" + Path.Combine(path, "../Effect8Copy.jpg") + "\"";
            string Effect9Copy = "\"" + Path.Combine(path, "../Effect9Copy.jpg") + "\"";
            string Effect10Copy = "\"" + Path.Combine(path, "../Effect10Copy.jpg") + "\"";
            string Effect11Copy = "\"" + Path.Combine(path, "../Effect11Copy.jpg") + "\"";
            string Effect12Copy = "\"" + Path.Combine(path, "../Effect12Copy.jpg") + "\"";
            string Effect13Copy = "\"" + Path.Combine(path, "../Effect13Copy.jpg") + "\"";
            string Effect14Copy = "\"" + Path.Combine(path, "../Effect14Copy.jpg") + "\"";
            string Effect15Copy = "\"" + Path.Combine(path, "../Effect15Copy.jpg") + "\"";
            string Effect16Copy = "\"" + Path.Combine(path, "../Effect16Copy.jpg") + "\"";
            string Effect17Copy = "\"" + Path.Combine(path, "../Effect17Copy.jpg") + "\"";
            string Effect18Copy = "\"" + Path.Combine(path, "../Effect18Copy.jpg") + "\"";
            string Effect19Copy = "\"" + Path.Combine(path, "../Effect19Copy.jpg") + "\"";
            string Effect20Copy = "\"" + Path.Combine(path, "../Effect20Copy.jpg") + "\"";
            string Effect21Copy = "\"" + Path.Combine(path, "../Effect21Copy.jpg") + "\"";
            string Effect22Copy = "\"" + Path.Combine(path, "../Effect22Copy.jpg") + "\"";
            string Effect23Copy = "\"" + Path.Combine(path, "../Effect23Copy.jpg") + "\"";
            string Effect24Copy = "\"" + Path.Combine(path, "../Effect24Copy.jpg") + "\"";
            string Effect25Copy = "\"" + Path.Combine(path, "../Effect25Copy.jpg") + "\"";
            string Effect26Copy = "\"" + Path.Combine(path, "../Effect26Copy.jpg") + "\"";
            string Effect27Copy = "\"" + Path.Combine(path, "../Effect27Copy.jpg") + "\"";
            string Effect28Copy = "\"" + Path.Combine(path, "../Effect28Copy.jpg") + "\"";
            string Effect29Copy = "\"" + Path.Combine(path, "../Effect29Copy.jpg") + "\"";
            string Effect30Copy = "\"" + Path.Combine(path, "../Effect30Copy.jpg") + "\"";
            string Effect31Copy = "\"" + Path.Combine(path, "../Effect31Copy.jpg") + "\"";
            string Effect32Copy = "\"" + Path.Combine(path, "../Effect32Copy.jpg") + "\"";
            string Effect33Copy = "\"" + Path.Combine(path, "../Effect33Copy.jpg") + "\"";
            string Effect34Copy = "\"" + Path.Combine(path, "../Effect34Copy.jpg") + "\"";
            string Effect35Copy = "\"" + Path.Combine(path, "../Effect35Copy.jpg") + "\"";
            string Effect36Copy = "\"" + Path.Combine(path, "../Effect36Copy.jpg") + "\"";
            string Effect37Copy = "\"" + Path.Combine(path, "../Effect37Copy.jpg") + "\"";
            string Effect38Copy = "\"" + Path.Combine(path, "../Effect38Copy.jpg") + "\"";
            string Effect39Copy = "\"" + Path.Combine(path, "../Effect39Copy.jpg") + "\"";
            string Effect40Copy = "\"" + Path.Combine(path, "../Effect40Copy.jpg") + "\"";
            string Effect41Copy = "\"" + Path.Combine(path, "../Effect41Copy.jpg") + "\"";
            string Effect42Copy = "\"" + Path.Combine(path, "../Effect42Copy.jpg") + "\"";
            string Effect43Copy = "\"" + Path.Combine(path, "../Effect43Copy.jpg") + "\"";
            string Effect44Copy = "\"" + Path.Combine(path, "../Effect44Copy.jpg") + "\"";
            string Effect45Copy = "\"" + Path.Combine(path, "../Effect45Copy.jpg") + "\"";
            string Effect46Copy = "\"" + Path.Combine(path, "../Effect46Copy.jpg") + "\"";
            string Effect47Copy = "\"" + Path.Combine(path, "../Effect47Copy.jpg") + "\"";
            string Effect48Copy = "\"" + Path.Combine(path, "../Effect48Copy.jpg") + "\"";

            string Effect1Copy_100 = "\"" + Path.Combine(path, "../Effect1Copy_100.jpg") + "\"";
            string Effect2Copy_100 = "\"" + Path.Combine(path, "../Effect2Copy_100.jpg") + "\"";
            string Effect3Copy_100 = "\"" + Path.Combine(path, "../Effect3Copy_100.jpg") + "\"";
            string Effect4Copy_100 = "\"" + Path.Combine(path, "../Effect4Copy_100.jpg") + "\"";
            string Effect5Copy_100 = "\"" + Path.Combine(path, "../Effect5Copy_100.jpg") + "\"";
            string Effect6Copy_100 = "\"" + Path.Combine(path, "../Effect6Copy_100.jpg") + "\"";
            string Effect7Copy_100 = "\"" + Path.Combine(path, "../Effect7Copy_100.jpg") + "\"";
            string Effect8Copy_100 = "\"" + Path.Combine(path, "../Effect8Copy_100.jpg") + "\"";
            string Effect9Copy_100 = "\"" + Path.Combine(path, "../Effect9Copy_100.jpg") + "\"";
            string Effect10Copy_100 = "\"" + Path.Combine(path, "../Effect10Copy_100.jpg") + "\"";
            string Effect11Copy_100 = "\"" + Path.Combine(path, "../Effect11Copy_100.jpg") + "\"";
            string Effect12Copy_100 = "\"" + Path.Combine(path, "../Effect12Copy_100.jpg") + "\"";
            string Effect13Copy_100 = "\"" + Path.Combine(path, "../Effect13Copy_100.jpg") + "\"";
            string Effect14Copy_100 = "\"" + Path.Combine(path, "../Effect14Copy_100.jpg") + "\"";
            string Effect15Copy_100 = "\"" + Path.Combine(path, "../Effect15Copy_100.jpg") + "\"";
            string Effect16Copy_100 = "\"" + Path.Combine(path, "../Effect16Copy_100.jpg") + "\"";
            string Effect17Copy_100 = "\"" + Path.Combine(path, "../Effect17Copy_100.jpg") + "\"";
            string Effect18Copy_100 = "\"" + Path.Combine(path, "../Effect18Copy_100.jpg") + "\"";
            string Effect19Copy_100 = "\"" + Path.Combine(path, "../Effect19Copy_100.jpg") + "\"";
            string Effect20Copy_100 = "\"" + Path.Combine(path, "../Effect20Copy_100.jpg") + "\"";
            string Effect21Copy_100 = "\"" + Path.Combine(path, "../Effect21Copy_100.jpg") + "\"";
            string Effect22Copy_100 = "\"" + Path.Combine(path, "../Effect22Copy_100.jpg") + "\"";
            string Effect23Copy_100 = "\"" + Path.Combine(path, "../Effect23Copy_100.jpg") + "\"";
            string Effect24Copy_100 = "\"" + Path.Combine(path, "../Effect24Copy_100.jpg") + "\"";
            string Effect25Copy_100 = "\"" + Path.Combine(path, "../Effect25Copy_100.jpg") + "\"";
            string Effect26Copy_100 = "\"" + Path.Combine(path, "../Effect26Copy_100.jpg") + "\"";
            string Effect27Copy_100 = "\"" + Path.Combine(path, "../Effect27Copy_100.jpg") + "\"";
            string Effect28Copy_100 = "\"" + Path.Combine(path, "../Effect28Copy_100.jpg") + "\"";
            string Effect29Copy_100 = "\"" + Path.Combine(path, "../Effect29Copy_100.jpg") + "\"";
            string Effect30Copy_100 = "\"" + Path.Combine(path, "../Effect30Copy_100.jpg") + "\"";
            string Effect31Copy_100 = "\"" + Path.Combine(path, "../Effect31Copy_100.jpg") + "\"";
            string Effect32Copy_100 = "\"" + Path.Combine(path, "../Effect32Copy_100.jpg") + "\"";
            string Effect33Copy_100 = "\"" + Path.Combine(path, "../Effect33Copy_100.jpg") + "\"";
            string Effect34Copy_100 = "\"" + Path.Combine(path, "../Effect34Copy_100.jpg") + "\"";
            string Effect35Copy_100 = "\"" + Path.Combine(path, "../Effect35Copy_100.jpg") + "\"";
            string Effect36Copy_100 = "\"" + Path.Combine(path, "../Effect36Copy_100.jpg") + "\"";
            string Effect37Copy_100 = "\"" + Path.Combine(path, "../Effect37Copy_100.jpg") + "\"";
            string Effect38Copy_100 = "\"" + Path.Combine(path, "../Effect38Copy_100.jpg") + "\"";
            string Effect39Copy_100 = "\"" + Path.Combine(path, "../Effect39Copy_100.jpg") + "\"";
            string Effect40Copy_100 = "\"" + Path.Combine(path, "../Effect40Copy_100.jpg") + "\"";
            string Effect41Copy_100 = "\"" + Path.Combine(path, "../Effect41Copy_100.jpg") + "\"";
            string Effect42Copy_100 = "\"" + Path.Combine(path, "../Effect42Copy_100.jpg") + "\"";
            string Effect43Copy_100 = "\"" + Path.Combine(path, "../Effect43Copy_100.jpg") + "\"";
            string Effect44Copy_100 = "\"" + Path.Combine(path, "../Effect44Copy_100.jpg") + "\"";
            string Effect45Copy_100 = "\"" + Path.Combine(path, "../Effect45Copy_100.jpg") + "\"";
            string Effect46Copy_100 = "\"" + Path.Combine(path, "../Effect46Copy_100.jpg") + "\"";
            string Effect47Copy_100 = "\"" + Path.Combine(path, "../Effect47Copy_100.jpg") + "\"";
            string Effect48Copy_100 = "\"" + Path.Combine(path, "../Effect48Copy_100.jpg") + "\"";

            string Effect1Copy_200 = "\"" + Path.Combine(path, "../Effect1Copy_200.jpg") + "\"";
            string Effect2Copy_200 = "\"" + Path.Combine(path, "../Effect2Copy_200.jpg") + "\"";
            string Effect3Copy_200 = "\"" + Path.Combine(path, "../Effect3Copy_200.jpg") + "\"";
            string Effect4Copy_200 = "\"" + Path.Combine(path, "../Effect4Copy_200.jpg") + "\"";
            string Effect5Copy_200 = "\"" + Path.Combine(path, "../Effect5Copy_200.jpg") + "\"";
            string Effect6Copy_200 = "\"" + Path.Combine(path, "../Effect6Copy_200.jpg") + "\"";
            string Effect7Copy_200 = "\"" + Path.Combine(path, "../Effect7Copy_200.jpg") + "\"";
            string Effect8Copy_200 = "\"" + Path.Combine(path, "../Effect8Copy_200.jpg") + "\"";
            string Effect9Copy_200 = "\"" + Path.Combine(path, "../Effect9Copy_200.jpg") + "\"";
            string Effect10Copy_200 = "\"" + Path.Combine(path, "../Effect10Copy_200.jpg") + "\"";
            string Effect11Copy_200 = "\"" + Path.Combine(path, "../Effect11Copy_200.jpg") + "\"";
            string Effect12Copy_200 = "\"" + Path.Combine(path, "../Effect12Copy_200.jpg") + "\"";
            string Effect13Copy_200 = "\"" + Path.Combine(path, "../Effect13Copy_200.jpg") + "\"";
            string Effect14Copy_200 = "\"" + Path.Combine(path, "../Effect14Copy_200.jpg") + "\"";
            string Effect15Copy_200 = "\"" + Path.Combine(path, "../Effect15Copy_200.jpg") + "\"";
            string Effect16Copy_200 = "\"" + Path.Combine(path, "../Effect16Copy_200.jpg") + "\"";
            string Effect17Copy_200 = "\"" + Path.Combine(path, "../Effect17Copy_200.jpg") + "\"";
            string Effect18Copy_200 = "\"" + Path.Combine(path, "../Effect18Copy_200.jpg") + "\"";
            string Effect19Copy_200 = "\"" + Path.Combine(path, "../Effect19Copy_200.jpg") + "\"";
            string Effect20Copy_200 = "\"" + Path.Combine(path, "../Effect20Copy_200.jpg") + "\"";
            string Effect21Copy_200 = "\"" + Path.Combine(path, "../Effect21Copy_200.jpg") + "\"";
            string Effect22Copy_200 = "\"" + Path.Combine(path, "../Effect22Copy_200.jpg") + "\"";
            string Effect23Copy_200 = "\"" + Path.Combine(path, "../Effect23Copy_200.jpg") + "\"";
            string Effect24Copy_200 = "\"" + Path.Combine(path, "../Effect24Copy_200.jpg") + "\"";
            string Effect25Copy_200 = "\"" + Path.Combine(path, "../Effect25Copy_200.jpg") + "\"";
            string Effect26Copy_200 = "\"" + Path.Combine(path, "../Effect26Copy_200.jpg") + "\"";
            string Effect27Copy_200 = "\"" + Path.Combine(path, "../Effect27Copy_200.jpg") + "\"";
            string Effect28Copy_200 = "\"" + Path.Combine(path, "../Effect28Copy_200.jpg") + "\"";
            string Effect29Copy_200 = "\"" + Path.Combine(path, "../Effect29Copy_200.jpg") + "\"";
            string Effect30Copy_200 = "\"" + Path.Combine(path, "../Effect30Copy_200.jpg") + "\"";
            string Effect31Copy_200 = "\"" + Path.Combine(path, "../Effect31Copy_200.jpg") + "\"";
            string Effect32Copy_200 = "\"" + Path.Combine(path, "../Effect32Copy_200.jpg") + "\"";
            string Effect33Copy_200 = "\"" + Path.Combine(path, "../Effect33Copy_200.jpg") + "\"";
            string Effect34Copy_200 = "\"" + Path.Combine(path, "../Effect34Copy_200.jpg") + "\"";
            string Effect35Copy_200 = "\"" + Path.Combine(path, "../Effect35Copy_200.jpg") + "\"";
            string Effect36Copy_200 = "\"" + Path.Combine(path, "../Effect36Copy_200.jpg") + "\"";
            string Effect37Copy_200 = "\"" + Path.Combine(path, "../Effect37Copy_200.jpg") + "\"";
            string Effect38Copy_200 = "\"" + Path.Combine(path, "../Effect38Copy_200.jpg") + "\"";
            string Effect39Copy_200 = "\"" + Path.Combine(path, "../Effect39Copy_200.jpg") + "\"";
            string Effect40Copy_200 = "\"" + Path.Combine(path, "../Effect40Copy_200.jpg") + "\"";
            string Effect41Copy_200 = "\"" + Path.Combine(path, "../Effect41Copy_200.jpg") + "\"";
            string Effect42Copy_200 = "\"" + Path.Combine(path, "../Effect42Copy_200.jpg") + "\"";
            string Effect43Copy_200 = "\"" + Path.Combine(path, "../Effect43Copy_200.jpg") + "\"";
            string Effect44Copy_200 = "\"" + Path.Combine(path, "../Effect44Copy_200.jpg") + "\"";
            string Effect45Copy_200 = "\"" + Path.Combine(path, "../Effect45Copy_200.jpg") + "\"";
            string Effect46Copy_200 = "\"" + Path.Combine(path, "../Effect46Copy_200.jpg") + "\"";
            string Effect47Copy_200 = "\"" + Path.Combine(path, "../Effect47Copy_200.jpg") + "\"";
            string Effect48Copy_200 = "\"" + Path.Combine(path, "../Effect48Copy_200.jpg") + "\"";
            #endregion
            #region Declaration autres images
            string imagemagick_repeat = "\"" + Path.Combine(path, "../imagemagick_repeat.jpg") + "\"";
            string imagemagick3_repeat = "\"" + Path.Combine(path, "../imagemagick3_repeat.jpg") + "\"";
            string imagemagick5_repeat = "\"" + Path.Combine(path, "../imagemagick5_repeat.jpg") + "\"";
            string imagemagick_repeat_100 = "\"" + Path.Combine(path, "../imagemagick_repeat_100.jpg") + "\"";
            string imagemagick3_repeat_100 = "\"" + Path.Combine(path, "../imagemagick3_repeat_100.jpg") + "\"";
            string imagemagick5_repeat_100 = "\"" + Path.Combine(path, "../imagemagick5_repeat_100.jpg") + "\"";
            string imagemagick_repeat_200 = "\"" + Path.Combine(path, "../imagemagick_repeat_200.jpg") + "\"";
            string imagemagick3_repeat_200 = "\"" + Path.Combine(path, "../imagemagick3_repeat_200.jpg") + "\"";
            string imagemagick5_repeat_200 = "\"" + Path.Combine(path, "../imagemagick5_repeat_200.jpg") + "\"";
            string tmpA1PngCopy = "\"" + Path.Combine(path, "../tmpA1PngCopy.png") + "\"";
            string tmpA1PngCopy_100 = "\"" + Path.Combine(path, "../tmpA1PngCopy_100.png") + "\"";
            string tmpA1PngCopy_200 = "\"" + Path.Combine(path, "../tmpA1PngCopy_200.png") + "\"";
            string imagemagick1 = "\"" + Path.Combine(path, "imagemagick1.jpg") + "\"";
            string imagemagick2 = "\"" + Path.Combine(path, "imagemagick2.jpg") + "\"";
            string imagemagick3 = "\"" + Path.Combine(path, "imagemagick3.jpg") + "\"";
            string imagemagick4 = "\"" + Path.Combine(path, "imagemagick4.jpg") + "\"";
            string imagemagick5 = "\"" + Path.Combine(path, "imagemagick5.jpg") + "\"";
            string imagemagick6 = "\"" + Path.Combine(path, "imagemagick6.jpg") + "\"";
            string imagemagick7 = "\"" + Path.Combine(path, "imagemagick7.jpg") + "\"";
            string imagemagick8 = "\"" + Path.Combine(path, "imagemagick8.jpg") + "\"";
            string imagemagick9 = "\"" + Path.Combine(path, "imagemagick9.jpg") + "\"";
            string imagemagick10 = "\"" + Path.Combine(path, "imagemagick10.jpg") + "\"";
            string imagemagick11 = "\"" + Path.Combine(path, "imagemagick11.jpg") + "\"";
            string imagemagick12 = "\"" + Path.Combine(path, "imagemagick12.jpg") + "\"";
            string imagemagick13 = "\"" + Path.Combine(path, "imagemagick13.jpg") + "\"";
            string imagemagick14 = "\"" + Path.Combine(path, "imagemagick14.jpg") + "\"";
            string imagemagick15 = "\"" + Path.Combine(path, "imagemagick15.jpg") + "\"";
            string imagemagick16 = "\"" + Path.Combine(path, "imagemagick16.jpg") + "\"";
            string rgb = "\"" + Path.Combine(path, "rgb_d%.jpg") + "\"";
            string rgb0 = "\"" + Path.Combine(path, "rgb_0.jpg") + "\"";
            string rgb1 = "\"" + Path.Combine(path, "rgb_1.jpg") + "\"";
            string rgb2 = "\"" + Path.Combine(path, "rgb_2.jpg") + "\"";
            string imagemagick_original_size = "\"" + Path.Combine(path, "../../imagemagick_original_size.jpg") + "\"";
            string imagemagick_original_original_size = "\"" + Path.Combine(path, "../../imagemagick_original_original_size.jpg") + "\"";
            string imagemagick_new_size = "\"" + Path.Combine(path, "../../imagemagick_new_size.jpg") + "\"";
            string imagemagick_new_painter = "\"" + Path.Combine(path, "../../imagemagick_new_painter.jpg") + "\"";
            string exist = "\"" + Path.Combine(path, "exist.jpg") + "\"";
            string tmpA = "\"" + Path.Combine(path, "tmpA.jpg") + "\"";
            string tmpA1 = "\"" + Path.Combine(path, "tmpA1.jpg") + "\"";
            string tmpA2 = "\"" + Path.Combine(path, "tmpA2.jpg") + "\"";
            string tmpA22 = "\"" + Path.Combine(path, "tmpA22.jpg") + "\"";
            string tmpA11 = "\"" + Path.Combine(path, "tmpA11.jpg") + "\"";
            string tmpA222 = "\"" + Path.Combine(path, "tmpA222.jpg") + "\"";
            string tmpA3 = "\"" + Path.Combine(path, "tmpA3.jpg") + "\"";
            string tmpA33 = "\"" + Path.Combine(path, "tmpA33.jpg") + "\"";
            string tmpA333 = "\"" + Path.Combine(path, "tmpA333.jpg") + "\"";
            string thumb = "\"" + Path.Combine(path, "thumb.jpg") + "\"";
            string final1 = "\"" + Path.Combine(path, "final1.jpg") + "\"";
            string final2 = "\"" + Path.Combine(path, "final2.jpg") + "\"";
            string final3 = "\"" + Path.Combine(path, "final3.jpg") + "\"";
            string finalPng = "\"" + Path.Combine(path, "finalPng.png") + "\"";
            string final1Png = "\"" + Path.Combine(path, "final1Png.png") + "\"";
            string final2Png = "\"" + Path.Combine(path, "final2Png.png") + "\"";
            string tmpAPng = "\"" + Path.Combine(path, "tmpAPng.png") + "\"";
            string tmpA1Png = "\"" + Path.Combine(path, "tmpA1Png.png") + "\"";
            string tmpA2Png = "\"" + Path.Combine(path, "tmpA2Png.png") + "\"";
            string thumbPng = "\"" + Path.Combine(path, "thumbPng.Png") + "\"";
            string thumbpbm = "\"" + Path.Combine(path, "thumb.pbm") + "\"";
            string thumbsvg = "\"" + Path.Combine(path, "thumb.svg") + "\"";
            string thumbpbm1 = "\"" + Path.Combine(path, "thumbpbm1.pbm") + "\"";
            string thumbsvg1 = "\"" + Path.Combine(path, "thumbsvg1.svg") + "\"";

            string note = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/note.jpg") + "\"";
            string note_temp = "\"" + Path.Combine(path, "../../note.jpg") + "\"";

            string p9 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/p9.jpg") + "\"";
            string p9_temp = "\"" + Path.Combine(path, "../../p9.jpg") + "\"";
            string p12 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/p12.jpg") + "\"";
            string p12_temp = "\"" + Path.Combine(path, "../../p12.jpg") + "\"";

            string Mask6 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/mask6.png") + "\"";
            string Mask6_temp = "\"" + Path.Combine(path, "../../Mask6_temp.png") + "\"";
            string Mask7 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/mask7.png") + "\"";
            string Mask7_temp = "\"" + Path.Combine(path, "../../Mask7_temp.png") + "\"";
            string Mask1 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/Mask1.png") + "\"";
            string Mask1_temp = "\"" + Path.Combine(path, "../../Mask1_temp.png") + "\"";
            string Mask2 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/Mask2.png") + "\"";
            string Mask2_temp = "\"" + Path.Combine(path, "../../Mask2_temp.png") + "\"";
            string Mask8 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/Mask8.png") + "\"";
            string Mask8_temp = "\"" + Path.Combine(path, "../../Mask8.png") + "\"";
            string Mask35 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/mask35.png") + "\"";
            string Mask35_temp = "\"" + Path.Combine(path, "../../mask35.png") + "\"";
            string Background19 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/19.png") + "\"";
            string Background19_temp = "\"" + Path.Combine(path, "../../19.png") + "\"";
            string Background20 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/20.png") + "\"";
            string Background20_temp = "\"" + Path.Combine(path, "../../20.png") + "\"";
            string Background21 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/21.png") + "\"";
            string Background21_temp = "\"" + Path.Combine(path, "../../21.png") + "\"";
            string Background23 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/23.png") + "\"";
            string Background23_temp = "\"" + Path.Combine(path, "../../23.png") + "\"";

            string light1 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/l1.png") + "\"";
            string light2 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/l2.png") + "\"";
            string light3 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/l3.png") + "\"";


            string Background_painting = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/Background_painting.png") + "\"";
            string Background_painting_temp = "\"" + Path.Combine(path, "../../Background_painting.png") + "\"";
            string stripe = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/texture103.png") + "\"";
            string stripe2 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/stripe2.png") + "\"";
            string texture105 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/texture105.png") + "\"";
            string logo1 = "\"" + Path.Combine(path, "../../logo1.png") + "\"";
            string logo2 = "\"" + Path.Combine(path, "../../logo2.png") + "\"";

            string color1 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/color1.png") + "\"";
            string color1_temp = "\"" + Path.Combine(path, "../../color1.png") + "\"";
            string color2 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/color2.png") + "\"";
            string color2_temp = "\"" + Path.Combine(path, "../../color2.png") + "\"";
            string p2 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/p2.png") + "\"";
            string paper_temp = "\"" + Path.Combine(path, "../../paper_temp.png") + "\"";

            string c32 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/c32.jpg") + "\"";
            string c32_temp = "\"" + Path.Combine(path, "../../c32_temp.jpg") + "\"";
            string c33 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/c33.jpg") + "\"";
            string c33_temp = "\"" + Path.Combine(path, "../../c33_temp.jpg") + "\"";

            string rain = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/rain.png") + "\"";
            string rain_c = "\"" + Path.Combine(path, "../../rain_c.png") + "\"";
            string alpha = "\"" + Path.Combine(path, "../../alpha.png") + "\"";

            string color3 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/color3.png") + "\"";
            string color3_temp = "\"" + Path.Combine(path, "../../color3.png") + "\"";

            string Background7 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/Background7.jpg") + "\"";
            string Background7_temp = "\"" + Path.Combine(path, "../../Background7_temp.jpg") + "\"";
            string m7it = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/m7it.png") + "\"";
            string m7it_temp = "\"" + Path.Combine(path, "../../m7it_temp.png") + "\"";
            string m1 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/m1.png") + "\"";
            string m1_temp = "\"" + Path.Combine(path, "../../m1_temp.png") + "\"";
            string m8 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/m8.png") + "\"";
            string m8_temp = "\"" + Path.Combine(path, "../../m8_temp.png") + "\"";
            string texture_5 = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/texture_5.gif") + "\"";
            string texture_5_temp = "\"" + Path.Combine(path, "../../texture_5_temp.gif") + "\"";

            string final_it_saved = "\"" + Path.Combine(path, "../../final_PC_saved.jpg") + "\"";
            string final_it_saved_svg = "\"" + Path.Combine(path, "../../final_PC_saved.svg") + "\"";
            imagemagick = "\"" + imagemagick + "\"";
            final = "\"" + final + "\"";
            #endregion

            //////////////////////////////////////////////////////////////////////////////////// GTA Effects //////////////////////////////////////////////////////////////////////////////

            string effectPath = _EffectDirectory;
            string currentEffectPath = effectPath + "Effect" + effectType.ToString();
            string currentEffectPath_copy = effectPath + "Effect" + effectType.ToString() + "Copy";
            if (Directory.Exists(currentEffectPath_copy))
            {
                ExecuteImageMagic(imageMagicPath, "convert", "  " + EffectCopy + " " + final);
            }
            else
            {

                /*--------------------------------------------Resize avant---------------------------------------*/
                System.Drawing.Image image = System.Drawing.Image.FromFile(Path.Combine(path, "../../imagemagick_original_size.jpg"));


                //======================================Resize en cas le radio HD coché par défault===============================================
                //Bilal la resize automatique est en Full HD dans la page MainControl.xaml.cs
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\CommonRima/Settings.cfg"))
                {
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " " + imagemagick);
                }
                else
                {
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " -resize 1 " + imagemagick);
                }

                if (MainControl.HD_Conversion_Value == "SD")
                {
                    double value_resize;
                    if (image.Width >= image.Height)
                    {
                        if (image.Width >= 1024) { value_resize = 1800; } else { value_resize = 1024; }
                        ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " -resize " + value_resize + " " + imagemagick);
                    }
                    else if (image.Width < image.Height)
                    {
                        if (image.Height >= 1024) { value_resize = 1800; } else { value_resize = 1024; }
                        ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " -resize x" + value_resize + " " + imagemagick);
                    }
                }
                else if (MainControl.HD_Conversion_Value == "HD")
                {
                    double value_resize;
                    if (image.Width >= image.Height)
                    {
                        if (image.Width >= 1024) { value_resize = 2600; } else { value_resize = 1100; }
                        ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " -resize " + value_resize + " " + imagemagick);
                    }
                    else if (image.Width < image.Height)
                    {
                        if (image.Height >= 1024) { value_resize = 2600; } else { value_resize = 1100; }
                        ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " -resize x" + value_resize + " " + imagemagick);
                    }
                }
                else if (MainControl.HD_Conversion_Value == "Full_HD")
                {
                    double value_resize;
                    if (image.Width < image.Height)
                    {
                        if (image.Height >= 1024) { value_resize = MainControl.Conversion_Value; } else { value_resize = 1500; }
                        ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " -resize x" + value_resize + " " + imagemagick);
                    }
                }
                //======================================Resize en cas le radio HD coché par défault===============================================

                if (effectType == 40 || effectType == 41 || effectType == 42 || effectType == 43 || effectType == 44 || effectType == 45 || effectType == 46 || effectType == 47 || effectType == 49 || effectType == 50)
                {
                    double value_resize;
                    if (image.Width >= image.Height)
                    {
                        if (image.Width >= 1024) { value_resize = 1800; } else { value_resize = 1024; }
                        ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " -resize " + value_resize + " " + imagemagick);
                    }
                    else if (image.Width < image.Height)
                    {
                        if (image.Height >= 1024) { value_resize = 1800; } else { value_resize = 1024; }
                        ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " -resize x" + value_resize + " " + imagemagick);
                    }
                }

                ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " " + imagemagick_new_size);
                System.Drawing.Image image1 = System.Drawing.Image.FromFile(Path.Combine(path, "../../imagemagick_new_size.jpg"));


                #region Effect0
                if (effectType == 0)
                {
                    if (Directory.Exists(Effect_Same6_directory))
                    {
                        ExecuteImageMagic(imageMagicPath, "convert", "  " + Effect_Same6 + " " + final);
                    }
                    else if (!Directory.Exists(Effect_Same1_directory) && !Directory.Exists(Effect_Same6_directory)) //si le dossier n'existe pas (le point d'exlamation prÃ©sent dans le code)
                    {
                        ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " -colorspace gray " + final);
                        
                    }
                    else if (Directory.GetFiles(Effect_Same1_directory).Length >= 0)
                    {
                       
                    }

                }
                #endregion

                #region Effect00 Special Landscape
                else if (effectType == 100)
                {
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " -colorspace gray " + final);
                }
                #endregion

                #region Effect1
                else if (effectType == 1)
                {
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " -colorspace gray " + final);
                }
                #endregion

                #region Effect2
                else if (effectType == 2)
                {
                    
                }
                #endregion

                #region Effect3
                else if (effectType == 3)
                {

                    

                }
                #endregion

                #region Effect4
                else if (effectType == 4)
                {
                    

                }
                #endregion

                #region Effect5
                else if (effectType == 5)
                {
                    
                }
                #endregion

                #region Effect6
                else if (effectType == 6)
                {
                    
                }
                #endregion

                #region Effect7 Corvo
                else if (effectType == 7)
                {
                    
                }
                #endregion

                #region Effect8
                else if (effectType == 8)
                {
                    

                }
                #endregion

                #region Effect9

                else if (effectType == 9)
                {
                   
                }
                #endregion

                #region Effect10
                else if (effectType == 10)
                {
                    
                }
                #endregion

                #region Effect11
                else if (effectType == 11)
                {
                   
                }
                #endregion

                #region Effect12
                else if (effectType == 12)
                {
                    
                }
                #endregion

                #region Effect13 BronzÃ© dorÃ©
                else if (effectType == 13)
                {
                   
                }
                #endregion

                #region Effect14
                else if (effectType == 14)
                {
                    
                }
                #endregion

                #region Effect15 ---Effet Papier
                else if (effectType == 15)
                {
                    

                }
                #endregion

                #region Effect16 ---Effet 8 sans edge
                else if (effectType == 16)
                {
                    
                }
                #endregion

                #region Effect17 ---Effet 8 + feltpen
                else if (effectType == 17)
                {
                    
                }
                #endregion

                #region Effect18 ---Dessin AnimÃ©
                else if (effectType == 18)
                {
                    
                }
                #endregion

                #region Effect19 ---Painting
                else if (effectType == 19)
                {
                    
                }
                #endregion

                #region Effect20 
                else if (effectType == 20)
                {
                    
                }
                #endregion

                #region Effect21 
                else if (effectType == 21)
                {
                    
                }
                #endregion

                #region Effect22
                else if (effectType == 22)
                {
                    
                }
                #endregion

                #region Effect23
                else if (effectType == 23)
                {
                    
                }
                #endregion

                #region Effect24
                if (effectType == 24)
                {
                    
                }
                #endregion

                #region Effect25
                if (effectType == 25)
                {
                    
                }
                #endregion

                #region Effect26

                else if (effectType == 26)
                {
                    

                }
                #endregion

                



                //////////////////////////////////// Textures Effects //////////////////////////////////////////
                #region Texture Effect 101-104
                for (int i = 101; i <= 104; i++)
                {
                    string texture = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/texture" + i + ".png") + "\"";
                    if (effectType == i)
                    {
                        
                    }
                }
                #endregion
                /////////////////////////////////////////////////// End Textures Effects //////////////////////////////////////////////////////////////////////////////

                ////////////////////////////////// Frames Effects //////////////////////////////////
                #region Frames Effect 201-213
                for (int i = 201; i <= 213; i++)
                {
                    
                }
                #endregion
                /////////////////////////////////////////// End Frames Effects //////////////////////////////////////////////////////////////////////////////

                ////////////////////////////////// Splatters Effects //////////////////////////////////
                #region Splatters Effects 401
                for (int i = 401; i <= 405; i++)
                {
                    string sp = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/sp" + i + ".png") + "\"";
                    string spc = "\"" + Path.Combine(PrimaCartoonizer.View.MainControl.MainExecutablesPath, "mask/spc" + i + ".png") + "\"";
                    if (effectType == i)
                    {
                       
                    }
                }
                #endregion
                /////////////////////////////////////////// End Splatters Effects //////////////////////////////////////////////////////////////////////////////

                //////////////////////////////////////////// Resize Effects //////////////////////////////////////////////////////////////////////////////
                #region effect 300
                if (effectType == 300)
                {
                    //100%
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " -resize 1%  " + imagemagick1);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick2);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick3);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick4);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick5);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick6);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick7);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick8);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick9);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick10);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick11);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick12);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick13);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick14);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " -resize 100%  " + final);
                    processed = true;
                }
                #endregion

                #region effect 301
                else if (effectType == 301)
                {
                    //75%
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " -resize 1%  " + imagemagick1);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick2);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick3);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick4);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick5);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick6);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick7);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick8);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick9);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick10);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick11);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick12);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick13);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick14);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " -resize 75%  " + final);
                    processed = true;
                }
                #endregion

                #region effect 302
                else if (effectType == 302)
                {
                    //50%
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " -resize 1%  " + imagemagick1);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick2);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick3);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick4);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick5);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick6);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick7);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick8);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick9);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick10);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick11);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick12);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick13);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick14);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " -resize 50%  " + final);
                    processed = true;
                }
                #endregion

                #region effect 303
                else if (effectType == 303)
                {
                    //25%
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " -resize 1%  " + imagemagick1);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick2);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick3);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick4);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick5);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick6);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick7);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick8);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick9);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick10);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick11);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick12);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick13);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1%  " + imagemagick14);
                    ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " -resize 25%  " + final);
                    processed = true;
                }
                #endregion
                ///////////////////////////////////////// End Resize Effects //////////////////////////////////////////////////////////////////////////////

                #region effect 306 Apply Button 
                else if (effectType == 306) //Bilal pour valider l'effet en appuyant sur le bouton Appliquer
                {
                    if (!string.IsNullOrEmpty(origFilePath))
                    {
                        System.Drawing.Image imageOrig = System.Drawing.Image.FromFile(origFilePath);
                        ExecuteImageMagic(imageMagicPath, "convert", "  " + final + " -resize " + imageOrig.Width + "x" + imageOrig.Height + "!  " + final);
                        ExecuteImageMagic(imageMagicPath, "composite", " -dissolve " + percentage.ToString() + " " + final + " " + origFilePath + "  " + final);
                        //ExecuteImageMagic(imageMagicPath, "composite", " -dissolve " + percentage.ToString() + " " + imagemagick_original_original_size + " " + final + "  " + final); // it was like that
                        ExecuteImageMagic(imageMagicPath, "convert", "  " + final + " " + final_it_saved);
                    }
                    else
                    {
                        ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick_original_size + " -resize " + image1.Width + "x" + image1.Height + "!  " + imagemagick_original_original_size);
                        ExecuteImageMagic(imageMagicPath, "composite", " -dissolve " + percentage.ToString() + " " + final + " " + imagemagick_original_original_size + "  " + final);
                        ExecuteImageMagic(imageMagicPath, "convert", "  " + final + " " + final_it_saved);
                    }
                    processed = true;
                }
                #endregion

                #region effect 307
                else if (effectType == 307) //Bilal pour restaurer l'effet original en appuyant sur le bouton Undo
                {
                    System.Drawing.Image image_original = System.Drawing.Image.FromFile(Path.Combine(path, "../../imagemagick_original_size.jpg"));
                    if (image_original.Width >= image_original.Height & image_original.Width > 1500)
                    {
                        ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick_original_size + " -resize 1500 " + final);
                    }
                    else
                    {
                        ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick_original_size + "  " + final);
                    }
                    if (image_original.Width < image_original.Height & image_original.Height > 1024)
                    {
                        ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick_original_size + " -resize x1500 " + final);
                    }
                    else
                    {
                        ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick_original_size + "  " + final);
                    }
                    processed = true;
                }
                #endregion

                #region effect 308

                else if (effectType == 308) //Bilal pour restaurer l'effet original en appuyant sur le bouton Reload
                {
                    System.Drawing.Image image_original = System.Drawing.Image.FromFile(Path.Combine(path, "../../imagemagick_original_size.jpg"));
                    if (image_original.Width >= image_original.Height & image_original.Width > 1500)
                    {
                        ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick_original_size + " -resize 1500 " + final);
                    }
                    else
                    {
                        ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick_original_size + "  " + final);
                    }
                    if (image_original.Width < image_original.Height & image_original.Height > 1024)
                    {
                        ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick_original_size + " -resize x1500 " + final);
                    }
                    else
                    {
                        ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick_original_size + "  " + final);
                    }
                    processed = true;
                }
                #endregion

            }

            //if (effectType != 0)
            //{
            ExecuteImageMagic(imageMagicPath, "convert", "  " + final + " " + EffectCopy); //Copier l'effet
            Directory.CreateDirectory(currentEffectPath_copy);
            ///////////////////////////////////////////////////////////////////////////////////
            //Code pour effacer brouiller les fichiers après l'execution de l'effet 
            
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " -resize 1 " + imagemagick1);
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + imagemagick2);
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + imagemagick3);
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + imagemagick4);
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + imagemagick5);
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + imagemagick6);
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + imagemagick7);
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + imagemagick8);
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + imagemagick9);
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + imagemagick10);
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + imagemagick11);
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + imagemagick12);
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + imagemagick13);
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + imagemagick14);
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + imagemagick15);
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + imagemagick16);
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + final1Png);
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + thumbpbm);
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + thumbsvg);
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + tmpA1Png);
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + tmpA2Png);
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + tmpA2);
            ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + tmpAPng);
            //ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick1 + " -resize 1 " + exist);
            
            ////////////////////////////////////////////////////////////////////////////////////
            //}

            if (new Auth().Activate())
            {
                processed = true;
            }
            else
            {
                ExecuteGmic(tempExePath, "  " + imagemagick + " -watermark_visible \"PrimaCartoonizer_Demo\",0.40,50,25,0.50,1 -o " + final);
                processed = true;
            }




            /*--------------------------------------------Resize l'image Finale à 4096px---------------------------------------*/
            System.Drawing.Image image_original_saved = System.Drawing.Image.FromFile(Path.Combine(path, "../../imagemagick_original_size.jpg"));
            //if (image_original_saved.Width >= image_original_saved.Height & image_original_saved.Width > 1500)

            if (image_original_saved.Width >= 1500)
            {
                ExecuteImageMagic(imageMagicPath, "convert", "  " + final + " -resize 4096  " + final_it_saved); //Bilal resizer l'image finale à 4096px uniquement si le width de la photo d'origine est plus grand que 1500
                //ExecuteImageMagic(imageMagicPath, "composite", "   " + logo1 + " -gravity SouthEast -geometry +20+20 " + final_it_saved + "  " + final_it_saved); //For Sana
            }
            else if (image_original_saved.Width > 1024 & image_original_saved.Width < 1500)
            {
                ExecuteImageMagic(imageMagicPath, "convert", "  " + final + " -resize 1500 " + final_it_saved);
                //ExecuteImageMagic(imageMagicPath, "composite", "   " + logo2 + " -gravity SouthEast -geometry +20+20 " + final_it_saved + "  " + final_it_saved); //For Sana
            }
            else if (image_original_saved.Width <= 1024)
            {
                ExecuteImageMagic(imageMagicPath, "convert", "  " + final + " -resize 1024 " + final_it_saved);
                //ExecuteImageMagic(imageMagicPath, "composite", "   " + logo2 + " -gravity SouthEast -geometry +20+20 " + final_it_saved + "  " + final_it_saved); //For Sana
            }

            //ExecuteImageMagic(imageMagicPath, "convert", "  " + final + " -resize " + image_original_saved.Width +"x"+ image_original_saved.Height + "! " + final_it_saved); //Bilal resizer l'image à la taille d'origine
            /*--------------------------------------------Fin Resize l'image Finale à 4096px---------------------------------------*/



            /*--------------------------------------------Resize l'image Finale pour la Canvas---------------------------------------*/
            System.Drawing.Image image_newsize = System.Drawing.Image.FromFile(Path.Combine(path, "../../imagemagick_new_size.jpg"));
            if (image_newsize.Width > 1500)
            {
                ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " -resize 1500 " + final);
            }
            else if (image_newsize.Height >= 1500)
            {
                ExecuteImageMagic(imageMagicPath, "convert", "  " + imagemagick + " -resize x1500 " + final);
            }
            /*--------------------------------------------End Resize l'image Finale pour la Canvas---------------------------------------*/



            //Directory.Delete(path, true); //Bilal c'est pour effacer à la fin le dossier tmp dans lequel les fichiers seront convertit
            return processed;

        }

        public static void DeleteEffectImages()
        {
            List<int> effectList = new List<int>();
            effectList.Add(1);
            effectList.Add(2);
            effectList.Add(3);
            effectList.Add(4);
            effectList.Add(5);
            effectList.Add(6);
            effectList.Add(7);
            effectList.Add(8);
            effectList.Add(300);
            effectList.Add(301);
            effectList.Add(302);
            effectList.Add(303);

            string effectPath = _EffectDirectory;

            if (Directory.Exists(_EffectDirectory))
            {
                try
                {
                    Directory.Delete(_EffectDirectory, true); //Bilal pour effacer tout le dossier iToon
                }
                catch (Exception ex)
                {
                    //logging goes here
                    //return true;
                }
            }

            foreach (var item in effectList)
            {
                /*
                string currentEffectPath = effectPath + "Effect" + item.ToString();
                if (Directory.Exists(currentEffectPath))
                {
                    Directory.Delete(currentEffectPath, true);
                    //File.Delete(_EffectDirectory);
                }

                if (!Directory.Exists(currentEffectPath))
                {
                    //Directory.CreateDirectory(currentEffectPath);
                }
                 */
            }
        }

        private static void ExecuteImageMagic(string imageMagicPath, string tempExePath, string arguments)
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = Path.Combine(imageMagicPath, tempExePath);
            process.StartInfo.Arguments = arguments;

            process.Start();

            StreamReader myStreamReader = process.StandardOutput;
            process.WaitForExit(60 * 10000000);
            if (!process.HasExited)
            {
                process.Kill();
                throw new Exception("Timeout");
            }

            string s = myStreamReader.ReadToEnd();
        }
        static void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {

        }
        static void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
        }
        private static void ExecuteGmic(string tempExePath, string arguments)
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = tempExePath + "\\ca";
            process.StartInfo.Arguments = arguments;
            process.Start();

            process.WaitForExit(60 * 10000000);
            if (!process.HasExited)
            {
                process.Kill();
                throw new Exception("Timeout");
            }
        }
        private static string ValidateImageSize(string imagePath, string tempExePath, out decimal factor, out int width, out int heigh)
        {
            string outFilePath = imagePath;
            factor = 1;
            int w = 0, h = 0;
            width = heigh = 100;
            //Bilal
            using (System.Drawing.Image source = System.Drawing.Image.FromFile(imagePath))
            {
                width = source.Width;
                heigh = source.Height;
                if (source.Width < 500)
                {
                    decimal xFactor = (decimal)(source.Width * 1.5) / (decimal)source.Width;
                    decimal yFactor = (decimal)(source.Height * 1.5) / (decimal)source.Height;

                    factor = Math.Min(xFactor, yFactor);

                    w = (int)(source.Width * factor);
                    h = (int)(source.Height * factor);
                }

                if (source.Width > 500 & source.Width < 1300)
                {
                    decimal xFactor = (decimal)(source.Width * 1.3) / (decimal)source.Width;
                    decimal yFactor = (decimal)(source.Height * 1.3) / (decimal)source.Height;

                    factor = Math.Min(xFactor, yFactor);

                    w = (int)(source.Width * factor);
                    h = (int)(source.Height * factor);
                }
                /*
                if (source.Width > 1000)
                {
                    decimal xFactor = (decimal)(source.Width * 1.1) / (decimal)source.Width;
                    decimal yFactor = (decimal)(source.Height * 1.1) / (decimal)source.Height;

                    factor = Math.Min(xFactor, yFactor);

                    w = (int)(source.Width * factor);
                    h = (int)(source.Height * factor);
                }
                */
                if (source.Width > 1700) //bilal désactivé ( le resize se fait directement avec les effets)
                {
                    decimal xFactor = (decimal)2200 / (decimal)source.Width;
                    decimal yFactor = (decimal)1800 / (decimal)source.Height;

                    factor = Math.Min(xFactor, yFactor);

                    w = (int)(source.Width * factor);
                    h = (int)(source.Height * factor);
                }



            }

            if (factor != 1)
            {
                outFilePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(imagePath));

                ImageResize(imagePath, tempExePath, factor, outFilePath);

            }


            return outFilePath;
        }




        private static string ValidateImageSize_Mask(string imagePath, string tempExePath, out decimal factor, out int width, out int heigh)
        {
            string outFilePath = imagePath;
            factor = 1;
            int w = 0, h = 0;
            width = heigh = 100;

            using (System.Drawing.Image source = System.Drawing.Image.FromFile(imagePath))
            {
                width = source.Width;
                heigh = source.Height;
                if (source.Width > 1024)
                {
                    decimal xFactor = (decimal)1200 / (decimal)source.Width;
                    decimal yFactor = (decimal)1200 / (decimal)source.Height;

                    factor = Math.Min(xFactor, yFactor);

                    w = (int)(source.Width * factor);
                    h = (int)(source.Height * factor);
                }
            }

            if (factor != 1)
            {
                outFilePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(imagePath));

                ImageResize(imagePath, tempExePath, factor, outFilePath);

            }
            return outFilePath;
        }






        private static void ImageResize(string imagePath, string tempExePath, decimal factor, string outFilePath)
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = tempExePath + "\\ct";
            process.StartInfo.Arguments = GetGmicResizeArguments(imagePath, outFilePath, (int)(factor * 100));
            process.Start();

            process.WaitForExit(60 * 10000000);

            if (!process.HasExited)
            {
                process.Kill();
                throw new Exception("Timeout");
            }
        }


        /* Code Anis resize photo avec GDI+ sans ImageMagick 
         private static void ImageResize(string imagePath, string tempExePath, decimal factor, string outFilePath)
        {
            Bitmap img = LoadImage(imagePath);
            int newwidth = (int)((float)img.Width * (float)factor);
            int newheight = (int)((float)img.Height * (float)factor);
            img.Dispose();
            img = null;

            Image imgTemp = resizeImage(imagePath, new Size(newwidth, newheight));
            imgTemp.Save(outFilePath, ImageFormat.Jpeg);
        }

        //------------------------ anis changes ----------
        #region Anis code
        public static Bitmap LoadImage(string path)
        {
            Bitmap originalBitmap;
            StreamReader rdr = new StreamReader(path);
            originalBitmap = new Bitmap(Image.FromStream(rdr.BaseStream));
            rdr.Close();
            return originalBitmap;
        }
        public static bool SaveImage(string path, Image img)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(path, false);
                img.Save(streamWriter.BaseStream, ImageFormat.Jpeg);
                streamWriter.Flush();
                streamWriter.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public static Image resizeImage(string imagePath, Size size)
        {
            Image imgToResize = Image.FromFile(imagePath);
            int sourceWidth = size.Width; //imgToResize.Width;
            int sourceHeight = size.Height;//imgToResize.Height;

            Bitmap b = new Bitmap(sourceWidth, sourceHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            g.DrawImage(imgToResize, 0, 0, sourceWidth, sourceHeight);
            g.Dispose();
            imgToResize.Dispose();
            imgToResize = null;
            return (System.Drawing.Image)b;
        }
        //--------------------------------
        #endregion
         */
        // Fin code Anis resize sans ImageMagick 





        private static string GetGmicArguments(int effectType, string imagePath, string outFile)
        {


            string args = string.Empty;



            return args;
        }
        private static void RestoreSize(string outPath, string tempExePath, decimal factor)
        {
            factor = 1 / factor;
            ImageResize(outPath, tempExePath, factor, outPath);
        }
        private static string GetGmicResizeArguments(string imagePath, string outFilePath, int factor)
        {
            return string.Format("\"{0}\" -resize {1}% \"{2}\"", new object[] { imagePath, factor, outFilePath });
        }
        private static System.Drawing.Imaging.ImageFormat GetImageFormat(string p)
        {
            if (p.Equals(".bmp"))
                return ImageFormat.Bmp;
            else if (p.Equals(".gif"))
                return ImageFormat.Gif;
            else if (p.Equals(".ico"))
                return ImageFormat.Icon;
            else if (p.Equals(".jpg"))
                return ImageFormat.Jpeg;
            else if (p.Equals(".png"))
                return ImageFormat.Png;
            else
                return ImageFormat.Bmp;
        }

        /*
        private static void SetupRegestry()
        {
            string version = "6.6.0";
            string imPath = Path.Combine(Path.GetDirectoryName(typeof(CartoonizeHelper).Assembly.Location), "IM");

            string key = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\ImageMagick\\" + version + "\\Q:16";

            Microsoft.Win32.Registry.SetValue(key, "Version", version, Microsoft.Win32.RegistryValueKind.String);
            Microsoft.Win32.Registry.SetValue(key, "QuantumDepth", 0x10, Microsoft.Win32.RegistryValueKind.DWord);
            Microsoft.Win32.Registry.SetValue(key, "BinPath", imPath, Microsoft.Win32.RegistryValueKind.String);
            Microsoft.Win32.Registry.SetValue(key, "ConfigurePath", imPath + "\\config", Microsoft.Win32.RegistryValueKind.String);
            Microsoft.Win32.Registry.SetValue(key, "LibPath", imPath, Microsoft.Win32.RegistryValueKind.String);
            Microsoft.Win32.Registry.SetValue(key, "CoderModulesPath", Path.Combine(imPath, "modules\\coders"), Microsoft.Win32.RegistryValueKind.String);
            Microsoft.Win32.Registry.SetValue(key, "FilterModulesPath", Path.Combine(imPath, "modules\\filters"), Microsoft.Win32.RegistryValueKind.String);

            key = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\ImageMagick\\Current";

            Microsoft.Win32.Registry.SetValue(key, "Version", version, Microsoft.Win32.RegistryValueKind.String);
            Microsoft.Win32.Registry.SetValue(key, "QuantumDepth", 0x10, Microsoft.Win32.RegistryValueKind.DWord);
            Microsoft.Win32.Registry.SetValue(key, "BinPath", imPath, Microsoft.Win32.RegistryValueKind.String);
            Microsoft.Win32.Registry.SetValue(key, "ConfigurePath", imPath + "\\config", Microsoft.Win32.RegistryValueKind.String);
            Microsoft.Win32.Registry.SetValue(key, "LibPath", imPath, Microsoft.Win32.RegistryValueKind.String);
            Microsoft.Win32.Registry.SetValue(key, "CoderModulesPath", Path.Combine(imPath, "modules\\coders"), Microsoft.Win32.RegistryValueKind.String);
            Microsoft.Win32.Registry.SetValue(key, "FilterModulesPath", Path.Combine(imPath, "modules\\filters"), Microsoft.Win32.RegistryValueKind.String);



        }
         * */
    }
}
