// AForge Image Processing Library
//
// Copyright © Andrew Kirillov, 2005-2006
// andrew.kirillov@gmail.com
//

namespace AForge.Imaging.Filters
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    /// <summary>
    /// Merge filter - get MAX of two pixels
    /// </summary>
    /// 
    /// <remarks></remarks>
    /// 
    public sealed class Composite : FilterAnyToAny
    {
        private Bitmap overlayImage;
        private Point overlayPos = new Point(0, 0);

        /// <summary>
        /// Overlay image
        /// </summary>
        public Bitmap OverlayImage
        {
            get { return overlayImage; }
            set { overlayImage = value; }
        }

        /// <summary>
        /// Overlay position
        /// </summary>
        public Point OverlayPos
        {
            get { return overlayPos; }
            set { overlayPos = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Merge"/> class
        /// </summary>
        public Composite() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Merge"/> class
        /// </summary>
        /// 
        /// <param name="overlayImage">Overlay image</param>
        /// 
        public Composite(Bitmap overlayImage)
        {
            this.overlayImage = overlayImage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Merge"/> class
        /// </summary>
        /// 
        /// <param name="overlayImage">Overlay image</param>
        /// <param name="position">Overlay position</param>
        /// 
        public Composite(Bitmap overlayImage, Point position)
        {
            this.overlayImage = overlayImage;
            this.overlayPos = position;
        }

        /// <summary>
        /// Process the filter on the specified image
        /// </summary>
        /// 
        /// <param name="imageData">image data</param>
        /// 
        protected override unsafe void ProcessFilter(BitmapData imageData)
        {

        }

        public Bitmap ApplyArgb(Bitmap image)
        {
            // lock source bitmap data
            BitmapData srcData = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly, image.PixelFormat);

            // apply the filter
            Bitmap dstImage = ApplyArgb(srcData);

            // unlock source image
            image.UnlockBits(srcData);

            return dstImage;
        }

        private Bitmap ApplyArgb(BitmapData imageData)
        {

            // get image dimension
            int width = imageData.Width;
            int height = imageData.Height;

            // create new image
            Bitmap dstImage = new Bitmap(width, height, imageData.PixelFormat);

            // lock destination bitmap data
            BitmapData dstData = dstImage.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, imageData.PixelFormat);

            // copy image
            Win32.memcpy(dstData.Scan0, imageData.Scan0, imageData.Stride * height);

            // process the filter
            ProcessFilterArgb(dstData);

            // unlock destination images
            dstImage.UnlockBits(dstData);

            return dstImage;
        }

        private unsafe void ProcessFilterArgb(BitmapData imageData)
        {
            // source image and overlay must have same pixel format
            if (imageData.PixelFormat != overlayImage.PixelFormat)
                throw new ArgumentException("Source and overlay images must have same pixel format ");

            if (imageData.Width != overlayImage.Width)
                throw new ArgumentException("Source and overlay images Width didn't match");
            if (imageData.Height != overlayImage.Height)
                throw new ArgumentException("Source and overlay images Height didn't match");

            // get image dimension
            int width = imageData.Width;
            int height = imageData.Height;

            // overlay position and dimension
            int ovrX = overlayPos.X;
            int ovrY = overlayPos.Y;
            int ovrW = overlayImage.Width;
            int ovrH = overlayImage.Height;

            // lock overlay image
            BitmapData ovrData = overlayImage.LockBits(
                new Rectangle(0, 0, ovrW, ovrH),
                ImageLockMode.ReadOnly, imageData.PixelFormat);

            // initialize other variables
            int pixelSize = (imageData.PixelFormat == PixelFormat.Format8bppIndexed) ? 1 : 4;
            int stride = imageData.Stride;
            int offset = stride - pixelSize * width;
            int ovrStide = ovrData.Stride;
            int ovrOffset, lineSize;

            // do the job
            byte* ptr = (byte*)imageData.Scan0.ToPointer();
            byte* ovr = (byte*)ovrData.Scan0.ToPointer();

            // pixel's value
            int v;

            if ((width == ovrW) && (height == ovrH) && (ovrX == 0) && (ovrY == 0))
            {
                // overlay image has the same size as the source image and its position is (0, 0)
                lineSize = width * pixelSize;


                // for each row
                for (int y = 0; y < height; y++)
                {
                    // for each pixel
                    for (int x = 0; x < width; x++, ptr += 4, ovr += 4)
                    {
                        ptr[0] = (byte)(((ptr[0] * (255 - ovr[3])) + (ovr[0] * ovr[3])) / 255);
                        ptr[1] = (byte)(((ptr[1] * (255 - ovr[3])) + (ovr[1] * ovr[3])) / 255);
                        ptr[2] = (byte)(((ptr[2] * (255 - ovr[3])) + (ovr[2] * ovr[3])) / 255);
                        ptr[3] = (byte)(((ptr[3] * (255 - ovr[3])) + (ovr[3] * ovr[3])) / 255);
                    }
                }

                //// for each line
                //for (int y = 0; y < height; y++)
                //{
                //    // for each pixel
                //    for (int x = 0; x < lineSize; x++, ptr++, ovr++)
                //    {
                //        v = (int)*ptr + (int)*ovr;
                //        *ptr = (v > 255) ? (byte)255 : (byte)v;
                //    }
                //    ptr += offset;
                //    ovr += offset;
                //}

            }


            // unlock overlay image
            overlayImage.UnlockBits(ovrData);
        }
    }
}
