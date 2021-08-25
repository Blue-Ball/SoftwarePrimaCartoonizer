using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Imaging.Textures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
namespace PrimaCartoonizer
{
    public class AFrogeHelper
    {
        //Bilal il faut déterminer le nombre de mask pour chaque dossier
        public static int FILTERS_COUNT = 32;
        public static int MERGE_FILTER_ID = 33;
        public static int COLOR_EFFECTS_COUNT = 29;
        public static int INSTAGRAM_EFFECTS_COUNT = 41;
        public static int KIDS_FRAMES_COUNT = 29;
        public static int VARIOUS_FRAMES_COUNT = 27; 
        public static Bitmap CurrentImage = null;
        public static Bitmap ApplyFilter(Bitmap image, int id, Bitmap overlayImage)
        {
            IFilter filter = GetFilter(id, overlayImage);

            if (filter is Composite)
            {
                ResizeBilinear rfilter = new ResizeBilinear(image.Width, image.Height);
                Bitmap resized = rfilter.ApplyArgb(overlayImage);

                ((Composite)filter).OverlayImage = resized;
                return ((Composite)filter).ApplyArgb(image);
            }
            else
            {
                return filter.Apply(image);
            }
        }

        public static Bitmap GetThumbnail(Bitmap image, int id, int r, Bitmap overlayImage)
        {
            Bitmap bmp = null;
            using (Bitmap bmp2 = ApplyFilter(image, id, overlayImage))
            {
                bmp = (Bitmap)bmp2.GetThumbnailImage(r, r, () => false, IntPtr.Zero);
            }

            return bmp;
        }

        private static IFilter GetFilter(int id, Bitmap overlayImage)
        {
            IFilter filter = null;
            if (id == 0)
            {
                filter = new GrayscaleBT709();
            }
            else if (id == 1)
            {
                filter = new Sepia();
            }
            else if (id == 2)
            {
                filter = new Invert();
            }
            else if (id == 3)
            {
                filter = new RotateChannels();
            }
            else if (id == 4)
            {
                filter = new ExtractChannel();
            }
            else if (id == 5)
            {
                filter = new GammaCorrection();
            }
            else if (id == 6)
            {
                filter = new ChannelFiltering(new IntRange(25, 230), new IntRange(25, 230), new IntRange(25, 230));
            }
            else if (id == 7)
            {
                filter = new ColorFiltering(new IntRange(25, 230), new IntRange(25, 230), new IntRange(25, 230));
            }
            else if (id == 8)
            {
                filter = new EuclideanColorFiltering(Color.FromArgb(255, 0, 0), 150);
            }
            else if (id == 9)
            {
                filter = new HueModifier(100);
            }
            else if (id == 10)
            {
                filter = new SaturationCorrection((float)0.15);
            }
            else if (id == 11)
            {
                filter = new BrightnessCorrection();
            }
            else if (id == 12)
            {
                filter = new ContrastCorrection();
            }
            else if (id == 13)
            {
                filter = new HSLFiltering(new IntRange(330, 30), new DoubleRange(0, 1), new DoubleRange(0, 1));
            }
            else if (id == 14)
            {
                filter = new YCbCrLinear();
                ((YCbCrLinear)filter).InCb = new DoubleRange(-0.3, 0.3);
            }
            else if (id == 15)
            {
                filter = new YCbCrFiltering(new DoubleRange(0.2, 0.9), new DoubleRange(-0.3, 0.3), new DoubleRange(-0.3, 0.3));
            }
            else if (id == 16)
            {
                filter = new YCbCrExtractChannel(YCbCr.CbIndex);
            }
            else if (id == 17)
            {
                filter = new Threshold();
            }
            else if (id == 18)
            {
                filter = new FloydSteinbergDithering();
            }
            else if (id == 19)
            {
                filter = new OrderedDithering();
            }
            else if (id == 20)
            {
                filter = new SISThreshold();
            }
            else if (id == 21)
            {
                filter = new Correlation(new int[,] {
                                        { 1, 2, 3, 2, 1 },
                                        { 2, 4, 5, 4, 2 },
                                        { 3, 5, 6, 5, 3 },
                                        { 2, 4, 5, 4, 2 },
                                        { 1, 2, 3, 2, 1 } });
            }
            else if (id == 22)
            {
                filter = new Sharpen();
            }
            else if (id == 23)
            {
                filter = new DifferenceEdgeDetector();
            }

            else if (id == 24)
            {
                filter = new HomogenityEdgeDetector();
            }
            else if (id == 25)
            {
                filter = new SobelEdgeDetector();
            }
            else if (id == 26)
            {
                filter = new LevelsLinear();

                ((LevelsLinear)filter).InRed = new IntRange(30, 230);
                ((LevelsLinear)filter).InGreen = new IntRange(50, 240);
                ((LevelsLinear)filter).InBlue = new IntRange(10, 210);
            }
            else if (id == 27)
            {
                filter = new Median();
            }
            else if (id == 28)
            {
                filter = new ConservativeSmoothing();
            }
            else if (id == 29)
            {
                filter = new Jitter();
            }
            //else if (id == 30)
            //{
            //    filter = new OilPainting();
            //}
            else if (id == 30)
            {
                filter = new GaussianBlur(2.0, 7);
            }
            //else if (id == 32)
            //{
            //    filter = new OilPainting();
            //}
            else if (id == 31)
            {
                filter = new GaussianBlur(2.0, 7);
            }
            else if (id == 32)
            {
                filter = new Texturer(new TextileTexture(), 2.0, 0.2);
            }
            else if (id == 33)
            {
                filter = new Composite(overlayImage);
            }
            return filter;
        }

        public static Bitmap HueModifier(Bitmap b, int value)
        {
            var hue = new HueModifier(value);
            CurrentImage = hue.Apply(b);
            return CurrentImage;
        }

        public static Bitmap OrderDithering(Bitmap b)
        {
            GrayscaleRMY rmy = new GrayscaleRMY();
            b = rmy.Apply(b);
            IFilter od = new OrderedDithering();
            CurrentImage = od.Apply(b);
            b.Dispose();
            b = null;
            return CurrentImage;
        }

    }
}

namespace ImageBlending_PrimaCartoonizer
{
    public class BlendEffects
    {
        private static string GetColorString(string colorname)
        {
            if (colorname == "blue")
            {
                return @"﻿<BitmapFilterData xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
<SourceBlueEnabled>true</SourceBlueEnabled>
<SourceGreenEnabled>true</SourceGreenEnabled>
<SourceRedEnabled>true</SourceRedEnabled>
<OverlayBlueEnabled>false</OverlayBlueEnabled>
<OverlayGreenEnabled>false</OverlayGreenEnabled>
<OverlayRedEnabled>false</OverlayRedEnabled>
<SourceBlueLevel>1</SourceBlueLevel>
<SourceGreenLevel>1</SourceGreenLevel>
<SourceRedLevel>1</SourceRedLevel>
<OverlayBlueLevel>0.34</OverlayBlueLevel>
<OverlayGreenLevel>0.25</OverlayGreenLevel>
<OverlayRedLevel>0.25</OverlayRedLevel>
<BlendTypeBlue>Add</BlendTypeBlue>
<BlendTypeGreen>Average</BlendTypeGreen>
<BlendTypeRed>AscendingOrder</BlendTypeRed>
</BitmapFilterData>";
            }
            else if (colorname == "green")
            {
                return @"﻿<BitmapFilterData xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
    <SourceBlueEnabled>true</SourceBlueEnabled>
    <SourceGreenEnabled>true</SourceGreenEnabled>
    <SourceRedEnabled>true</SourceRedEnabled>
    <OverlayBlueEnabled>false</OverlayBlueEnabled>
    <OverlayGreenEnabled>true</OverlayGreenEnabled>
    <OverlayRedEnabled>false</OverlayRedEnabled>
    <SourceBlueLevel>1</SourceBlueLevel>
    <SourceGreenLevel>1</SourceGreenLevel>
    <SourceRedLevel>1</SourceRedLevel>
    <OverlayBlueLevel>0.34</OverlayBlueLevel>
    <OverlayGreenLevel>0.84</OverlayGreenLevel>
    <OverlayRedLevel>0.34</OverlayRedLevel>
    <BlendTypeBlue>Average</BlendTypeBlue>
    <BlendTypeGreen>Average</BlendTypeGreen>
    <BlendTypeRed>AscendingOrder</BlendTypeRed>
</BitmapFilterData>";
            }
            else if (colorname == "orange")
            {
                return @"﻿<BitmapFilterData xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
<SourceBlueEnabled>true</SourceBlueEnabled>
<SourceGreenEnabled>true</SourceGreenEnabled>
<SourceRedEnabled>true</SourceRedEnabled>
<OverlayBlueEnabled>false</OverlayBlueEnabled>
<OverlayGreenEnabled>false</OverlayGreenEnabled>
<OverlayRedEnabled>false</OverlayRedEnabled>
<SourceBlueLevel>0.72</SourceBlueLevel>
<SourceGreenLevel>0.69</SourceGreenLevel>
<SourceRedLevel>1.62</SourceRedLevel>
<OverlayBlueLevel>0.34</OverlayBlueLevel>
<OverlayGreenLevel>0.41</OverlayGreenLevel>
<OverlayRedLevel>0.34</OverlayRedLevel>
<BlendTypeBlue>Average</BlendTypeBlue>
<BlendTypeGreen>DescendingOrder</BlendTypeGreen>
<BlendTypeRed>Add</BlendTypeRed>
</BitmapFilterData>";
            }
            else if (colorname == "pink")
            {
                return @"﻿﻿<BitmapFilterData xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
<SourceBlueEnabled>true</SourceBlueEnabled>
<SourceGreenEnabled>true</SourceGreenEnabled>
<SourceRedEnabled>true</SourceRedEnabled>
<OverlayBlueEnabled>false</OverlayBlueEnabled>
<OverlayGreenEnabled>false</OverlayGreenEnabled>
<OverlayRedEnabled>false</OverlayRedEnabled>
<SourceBlueLevel>1</SourceBlueLevel>
<SourceGreenLevel>1</SourceGreenLevel>
<SourceRedLevel>1</SourceRedLevel>
<OverlayBlueLevel>0.34</OverlayBlueLevel>
<OverlayGreenLevel>0.22</OverlayGreenLevel>
<OverlayRedLevel>0.25</OverlayRedLevel>
<BlendTypeBlue>Average</BlendTypeBlue>
<BlendTypeGreen>AscendingOrder</BlendTypeGreen>
<BlendTypeRed>Add</BlendTypeRed>
</BitmapFilterData>";
            }
            else if (colorname == "voilet")
            {
                return @"<BitmapFilterData xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
                                <SourceBlueEnabled>true</SourceBlueEnabled>
                                <SourceGreenEnabled>true</SourceGreenEnabled>
                                <SourceRedEnabled>true</SourceRedEnabled>
                                <OverlayBlueEnabled>false</OverlayBlueEnabled>
                                <OverlayGreenEnabled>false</OverlayGreenEnabled>
                                <OverlayRedEnabled>false</OverlayRedEnabled>
                                <SourceBlueLevel>1</SourceBlueLevel>
                                <SourceGreenLevel>1</SourceGreenLevel>
                                <SourceRedLevel>1</SourceRedLevel>
                                <OverlayBlueLevel>0.34</OverlayBlueLevel>
                                <OverlayGreenLevel>0.25</OverlayGreenLevel>
                                <OverlayRedLevel>0.25</OverlayRedLevel>
                                <BlendTypeBlue>Add</BlendTypeBlue>
                                <BlendTypeGreen>AscendingOrder</BlendTypeGreen>
                                <BlendTypeRed>Add</BlendTypeRed>
                            </BitmapFilterData>";
            }
            else if (colorname == "yellow")
            {
                return @"﻿﻿<BitmapFilterData xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
                              <SourceBlueEnabled>true</SourceBlueEnabled>
                              <SourceGreenEnabled>true</SourceGreenEnabled>
                              <SourceRedEnabled>true</SourceRedEnabled>
                              <OverlayBlueEnabled>false</OverlayBlueEnabled>
                              <OverlayGreenEnabled>false</OverlayGreenEnabled>
                              <OverlayRedEnabled>false</OverlayRedEnabled>
                              <SourceBlueLevel>1</SourceBlueLevel>
                              <SourceGreenLevel>1</SourceGreenLevel>
                              <SourceRedLevel>1</SourceRedLevel>
                              <OverlayBlueLevel>0.34</OverlayBlueLevel>
                              <OverlayGreenLevel>0.21</OverlayGreenLevel>
                              <OverlayRedLevel>0.25</OverlayRedLevel>
                              <BlendTypeBlue>AscendingOrder</BlendTypeBlue>
                              <BlendTypeGreen>Add</BlendTypeGreen>
                              <BlendTypeRed>Add</BlendTypeRed>
                            </BitmapFilterData>";
            }
            else
            {
                return @"﻿﻿<BitmapFilterData xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
                              <SourceBlueEnabled>true</SourceBlueEnabled>
                              <SourceGreenEnabled>true</SourceGreenEnabled>
                              <SourceRedEnabled>true</SourceRedEnabled>
                              <OverlayBlueEnabled>false</OverlayBlueEnabled>
                              <OverlayGreenEnabled>false</OverlayGreenEnabled>
                              <OverlayRedEnabled>false</OverlayRedEnabled>
                              <SourceBlueLevel>1</SourceBlueLevel>
                              <SourceGreenLevel>1</SourceGreenLevel>
                              <SourceRedLevel>1</SourceRedLevel>
                              <OverlayBlueLevel>0.34</OverlayBlueLevel>
                              <OverlayGreenLevel>0.21</OverlayGreenLevel>
                              <OverlayRedLevel>0.25</OverlayRedLevel>
                              <BlendTypeBlue>AscendingOrder</BlendTypeBlue>
                              <BlendTypeGreen>Add</BlendTypeGreen>
                              <BlendTypeRed>Add</BlendTypeRed>
                            </BitmapFilterData>";
            }
        }
        public static Bitmap DoEffect(string colorname, Bitmap btemp)
        {
            try
            {
                string xmlString = GetColorString(colorname);
                byte[] bytes = Encoding.Default.GetBytes(xmlString);
                xmlString = "<?xml version='1.0' encoding='utf-8'?>" + Encoding.UTF8.GetString(bytes).Replace("?", "");
                Bitmap bbOverlay = new Bitmap(btemp.Width, btemp.Height, PixelFormat.Format32bppArgb);
                // Bitmap bmpNew = new Bitmap(width, height, PixelFormat.Format32bppArgb);

                using (Graphics graphics = Graphics.FromImage(bbOverlay))
                {
                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                    graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    SolidBrush brush = new SolidBrush(Color.FromArgb(255, 242, 102));
                    graphics.FillRectangle(brush, new Rectangle(0, 0, bbOverlay.Width, bbOverlay.Height));
                    graphics.Flush();
                }

                BitmapFilterData tmpFilter = BitmapFilterData.XmlDeserialize(xmlString);
                using (Bitmap bmpPictureBoxSource = btemp.GetArgb())
                {
                    //bmpPictureBoxSource.Save("h:\\argb.png", ImageFormat.Png);
                    using (Bitmap bmpPictureBoxOverlay = bbOverlay)
                    {
                        Bitmap temp = bmpPictureBoxSource.BlendImage(bmpPictureBoxOverlay, tmpFilter);
                        return temp;
                    }
                }
            }
            catch (Exception ex)
            {
                return btemp;
            }
        }
        public static bool DoEffect(string colorname, Bitmap btemp, string filename)
        {
            try
            {
                string xmlString = GetColorString(colorname);
                //RGB(255,242,102)
                Bitmap bbOverlay = new Bitmap(btemp.Width, btemp.Height, PixelFormat.Format32bppArgb);
                // Bitmap bmpNew = new Bitmap(width, height, PixelFormat.Format32bppArgb);

                using (Graphics graphics = Graphics.FromImage(bbOverlay))
                {
                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                    graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    SolidBrush brush = new SolidBrush(Color.FromArgb(255, 242, 102));
                    graphics.FillRectangle(brush, new Rectangle(0, 0, bbOverlay.Width, bbOverlay.Height));
                    graphics.Flush();
                }

                BitmapFilterData tmpFilter = BitmapFilterData.XmlDeserialize(xmlString);
                using (Bitmap bmpPictureBoxSource = btemp.GetArgb())
                {
                    //bmpPictureBoxSource.Save("h:\\argb.png", ImageFormat.Png);
                    using (Bitmap bmpPictureBoxOverlay = bbOverlay)
                    {
                        Bitmap temp = bmpPictureBoxSource.BlendImage(bmpPictureBoxOverlay, tmpFilter);
                        temp.Save(filename, ImageFormat.Png);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
    public static class ExtBitmap
    {
        public static Bitmap BlendImage(this Bitmap baseImage, Bitmap overlayImage, BitmapFilterData filterData)
        {
            BitmapData baseImageData = baseImage.LockBits(new Rectangle(0, 0, baseImage.Width, baseImage.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            byte[] baseImageBuffer = new byte[baseImageData.Stride * baseImageData.Height];

            Marshal.Copy(baseImageData.Scan0, baseImageBuffer, 0, baseImageBuffer.Length);

            BitmapData overlayImageData = overlayImage.LockBits(new Rectangle(0, 0, overlayImage.Width, overlayImage.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            byte[] overlayImageBuffer = new byte[overlayImageData.Stride * overlayImageData.Height];

            Marshal.Copy(overlayImageData.Scan0, overlayImageBuffer, 0, overlayImageBuffer.Length);

            float sourceBlue = 0;
            float sourceGreen = 0;
            float sourceRed = 0;

            float overlayBlue = 0;
            float overlayGreen = 0;
            float overlayRed = 0;

            for (int k = 0; k < baseImageBuffer.Length && k < overlayImageBuffer.Length; k += 4)
            {
                sourceBlue = (filterData.SourceBlueEnabled ? baseImageBuffer[k] * filterData.SourceBlueLevel : 0);
                sourceGreen = (filterData.SourceGreenEnabled ? baseImageBuffer[k + 1] * filterData.SourceGreenLevel : 0);
                sourceRed = (filterData.SourceRedEnabled ? baseImageBuffer[k + 2] * filterData.SourceRedLevel : 0);

                overlayBlue = (filterData.OverlayBlueEnabled ? overlayImageBuffer[k] * filterData.OverlayBlueLevel : 0);
                overlayGreen = (filterData.OverlayGreenEnabled ? overlayImageBuffer[k + 1] * filterData.OverlayGreenLevel : 0);
                overlayRed = (filterData.OverlayRedEnabled ? overlayImageBuffer[k + 2] * filterData.OverlayRedLevel : 0);

                baseImageBuffer[k] = CalculateColorComponentBlendValue(sourceBlue, overlayBlue, filterData.BlendTypeBlue);
                baseImageBuffer[k + 1] = CalculateColorComponentBlendValue(sourceGreen, overlayGreen, filterData.BlendTypeGreen);
                baseImageBuffer[k + 2] = CalculateColorComponentBlendValue(sourceRed, overlayRed, filterData.BlendTypeRed);
            }

            Bitmap bitmapResult = new Bitmap(baseImage.Width, baseImage.Height, PixelFormat.Format32bppArgb);
            BitmapData resultImageData = bitmapResult.LockBits(new Rectangle(0, 0, bitmapResult.Width, bitmapResult.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Marshal.Copy(baseImageBuffer, 0, resultImageData.Scan0, baseImageBuffer.Length);

            bitmapResult.UnlockBits(resultImageData);
            baseImage.UnlockBits(baseImageData);
            overlayImage.UnlockBits(overlayImageData);

            return bitmapResult;
        }

        private static byte CalculateColorComponentBlendValue(float source, float overlay, ColorComponentBlendType blendType)
        {
            float resultValue = 0;
            byte resultByte = 0;

            if (blendType == ColorComponentBlendType.Add)
            {
                resultValue = source + overlay;
            }
            else if (blendType == ColorComponentBlendType.Subtract)
            {
                resultValue = source - overlay;
            }
            else if (blendType == ColorComponentBlendType.Average)
            {
                resultValue = (source + overlay) / 2.0f;
            }
            else if (blendType == ColorComponentBlendType.AscendingOrder)
            {
                resultValue = (source > overlay ? overlay : source);
            }
            else if (blendType == ColorComponentBlendType.DescendingOrder)
            {
                resultValue = (source < overlay ? overlay : source);
            }

            if (resultValue > 255)
            {
                resultByte = 255;
            }
            else if (resultValue < 0)
            {
                resultByte = 0;
            }
            else
            {
                resultByte = (byte)resultValue;
            }

            return resultByte;
        }
        public static Bitmap GetArgb(this System.Drawing.Image img)
        {
            Bitmap btm = new Bitmap(img);
            return GetArgbCopy(btm, btm.Width, btm.Height);
        }
        public static Bitmap LoadArgbBitmap(this string filePath, Size? imageDimensions = null)
        {
            StreamReader streamReader = new StreamReader(filePath);
            Bitmap fileBmp = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
            streamReader.Close();

            int width = fileBmp.Width;
            int height = fileBmp.Height;

            if (imageDimensions != null)
            {
                width = imageDimensions.Value.Width;
                height = imageDimensions.Value.Height;
            }

            if (fileBmp.PixelFormat != PixelFormat.Format32bppArgb || fileBmp.Width != width || fileBmp.Height != height)
            {
                fileBmp = GetArgbCopy(fileBmp, width, height);
            }

            return fileBmp;
        }

        private static Bitmap GetArgbCopy(Bitmap sourceImage, int width, int height)
        {
            Bitmap bmpNew = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(bmpNew))
            {
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                graphics.DrawImage(sourceImage, new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), new Rectangle(0, 0, sourceImage.Width, sourceImage.Height), GraphicsUnit.Pixel);
                graphics.Flush();
            }

            return bmpNew;
        }

    }

    [Serializable]
    public class BitmapFilterData
    {
        private bool sourceBlueEnabled = false;
        public bool SourceBlueEnabled { get { return sourceBlueEnabled; } set { sourceBlueEnabled = value; } }

        private bool sourceGreenEnabled = false;
        public bool SourceGreenEnabled { get { return sourceGreenEnabled; } set { sourceGreenEnabled = value; } }

        private bool sourceRedEnabled = false;
        public bool SourceRedEnabled { get { return sourceRedEnabled; } set { sourceRedEnabled = value; } }

        private bool overlayBlueEnabled = false;
        public bool OverlayBlueEnabled { get { return overlayBlueEnabled; } set { overlayBlueEnabled = value; } }

        private bool overlayGreenEnabled = false;
        public bool OverlayGreenEnabled { get { return overlayGreenEnabled; } set { overlayGreenEnabled = value; } }

        private bool overlayRedEnabled = false;
        public bool OverlayRedEnabled { get { return overlayRedEnabled; } set { overlayRedEnabled = value; } }

        private float sourceBlueLevel = 1.0f;
        public float SourceBlueLevel { get { return sourceBlueLevel; } set { sourceBlueLevel = value; } }

        private float sourceGreenLevel = 1.0f;
        public float SourceGreenLevel { get { return sourceGreenLevel; } set { sourceGreenLevel = value; } }

        private float sourceRedLevel = 1.0f;
        public float SourceRedLevel { get { return sourceRedLevel; } set { sourceRedLevel = value; } }

        private float overlayBlueLevel = 0.0f;
        public float OverlayBlueLevel { get { return overlayBlueLevel; } set { overlayBlueLevel = value; } }

        private float overlayGreenLevel = 0.0f;
        public float OverlayGreenLevel { get { return overlayGreenLevel; } set { overlayGreenLevel = value; } }

        private float overlayRedLevel = 0.0f;
        public float OverlayRedLevel { get { return overlayRedLevel; } set { overlayRedLevel = value; } }

        private ColorComponentBlendType blendTypeBlue = ColorComponentBlendType.Add;
        public ColorComponentBlendType BlendTypeBlue { get { return blendTypeBlue; } set { blendTypeBlue = value; } }

        private ColorComponentBlendType blendTypeGreen = ColorComponentBlendType.Add;
        public ColorComponentBlendType BlendTypeGreen { get { return blendTypeGreen; } set { blendTypeGreen = value; } }

        private ColorComponentBlendType blendTypeRed = ColorComponentBlendType.Add;
        public ColorComponentBlendType BlendTypeRed { get { return blendTypeRed; } set { blendTypeRed = value; } }

        public static string XmlSerialize(BitmapFilterData filterData)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(BitmapFilterData));

            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Encoding = Encoding.UTF8;
            xmlSettings.Indent = true;

            MemoryStream memoryStream = new MemoryStream();
            XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlSettings);

            xmlSerializer.Serialize(xmlWriter, filterData);
            xmlWriter.Flush();

            string xmlString = xmlSettings.Encoding.GetString(memoryStream.ToArray());

            xmlWriter.Close();
            memoryStream.Close();
            memoryStream.Dispose();

            return xmlString;
        }

        public static BitmapFilterData XmlDeserialize(string xmlString)
        {

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(BitmapFilterData));
            MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString));

            XmlReader xmlReader = XmlReader.Create(memoryStream);

            BitmapFilterData filterData = null;

            if (xmlSerializer.CanDeserialize(xmlReader) == true)
            {
                xmlReader.Close();
                memoryStream.Position = 0;

                filterData = (BitmapFilterData)xmlSerializer.Deserialize(memoryStream);
            }

            memoryStream.Close();
            memoryStream.Dispose();

            return filterData;
        }
    }

    public enum ColorComponentBlendType
    {
        Add,
        Subtract,
        Average,
        DescendingOrder,
        AscendingOrder
    }


}
