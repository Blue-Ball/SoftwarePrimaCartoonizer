using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Drawing;

namespace Cartoonizer.ToolsLib
{
    //This class fetches rgb date from an JPEG image and creates new bitmapSource clones from passed data.
    //The display of the image is updated simply by changing its source with a bitmapSource produced here.
    public static class BmsEngine
    {
        static BitmapSource parentBms;  //all clones produced from this bms
        static public int dataLength;   //size of rgb data associated with parent bitmapSource
        static private int stride;
        static private int pixelWidth;
        static private int pixelHeight;
        static private double dpiX;
        static private double dpiY;
        static private PixelFormat format;


        static Bitmap _bmp;

        static public Bitmap Bmp
        {
            get { return _bmp; }
            set { _bmp = value; }
        }

        static public void Init(BitmapSource imageBms)
        {
            parentBms = imageBms;   //save the passed bms of parent
            //calc its image stride
            stride = imageBms.PixelWidth * ((imageBms.Format.BitsPerPixel + 7) / 8);
            //calc its image data length
            dataLength = stride * imageBms.PixelHeight;
            //save orignal bms data needed to clone a BitmapSource using new passed data
            pixelWidth = imageBms.PixelWidth;
            pixelHeight = imageBms.PixelHeight;
            dpiX = imageBms.DpiX;
            dpiY = imageBms.DpiY;
            format = imageBms.Format;

            rgb = null;
        } //Init()
        static public void Init(Bitmap bmp)
        {
            _bmp = bmp;
        }

        static public BitmapSource CloneBms(byte[] newRgbData)
        {
            if (newRgbData.Length > 0)
            {
                BitmapSource childBms = BitmapSource.Create(pixelWidth, pixelHeight, dpiX, dpiY, format, null, newRgbData, stride);
                return childBms;
            }

            return null;
        } // CloneBms

        static byte[] rgb;
        static byte[] distination;
        static public byte[] GetRgbData()
        {
            rgb = new byte[dataLength];
            if (parentBms != null)
            {
                parentBms.CopyPixels(rgb, stride, 0);
            }
            return rgb;
        }

        static public void SetDestination(byte[] d)
        {
            distination = d;

        }
    }

}
