using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ExifReader
{
    class Program
    {
        public enum EXIF: int
        {
            Make = 0,   
            Model = 1,
            Orientation = 0x0112,
            XResolution	= 0x011a,
            YResolution	= 0x011b,
            ResolutionUnit = 0x0128,
            Software = 0x0131,
            ModifyDate = 0x0132,
            YCbCrPositioning = 0x0213,
            Copyright = 0x8298,
            FNumber	= 0x829d,
            ExposureProgram = 0x8822,
            ISO	= 0x8827,
            ExifVersion = 0x9000,	
            DateTimeOriginal = 0x9003,	
            CreateDate = 0x9004,	
            ComponentsConfiguration = 0x9101,
            CompressedBitsPerPixel	= 0x9102,
            ShutterSpeedValue = 0x9201,	
            ApertureValue = 0x9202,
            BrightnessValue	= 0x9203,
            ExposureCompensation = 0x9204,	
            MaxApertureValue = 0x9205,	
            MeteringMode = 0x9207,
            Flash = 0x9209,
            FocalLength	= 0x920a,
            FlashpixVersion	= 0xa000,
            ColorSpace = 0xa001,
            ExifImageWidth = 0xa002,
            ExifImageHeight = 0xa003,
            InteropIndex = 0x0001,
            InteropVersion = 0x0002,
            FocalPlaneXResolution = 0x920e,
            FocalPlaneYResolution = 0x920f,
            FocalPlaneResolutionUnit = 0x9210,
            SensingMethod = 0x9217,
            FileSource = 0xa300,
            SceneType = 0xa301,
            Compression = 0x0103,
            ThumbnailOffset	= 0x0201,
            ThumbnailLength	= 0x0202
        }

        public 
        //public static UInt64 getExifValue(int iterator)
        //{
        //    switch(iterator)
        //    {
        //        case 0:
        //            return 0x010f;
                  
        //        case 1:
        //            break;
        //    }
        //}

        //public void EnumerateAllPropertyId()
        //{
        //    foreach (PropertyID id in (PropertyID[]) Enum.GetValues(typeof(PropertyID)))
        //    {
               
        //    }
        //}
        static void Main(string[] args)
        {
            string pathToImage = @"C:\Users\pati\Desktop\ExifReader\exif.jpg";

            //Image image = Image.FromFile(pathToImage);
            //EXIF exif;
            //var size = Enum.GetNames(typeof(EXIF)).Length;
            //PropertyItem[] propertyItems = new PropertyItem[size];

            //for (int i = 0; i < size; ++i)
            //{
            //    propertyItems[i] = image.GetPropertyItem(size);
            //}

            //foreach (EXIF member in (EXIF[])Enum.GetValues(typeof(EXIF)))
            //{
            //    Console.WriteLine("{0} = {1}", member, Encoding.UTF8.GetString());
            //}

            // Create a new bitmap.
            Bitmap bmp = new Bitmap(pathToImage);

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            int stride = Math.Abs(bmpData.Stride);
            Point pointBegin = new Point(1500, 400);
            Point pointEnd = new Point(1516, 500);
            int width = pointEnd.X - pointBegin.X;
            int heigth = pointEnd.Y - pointEnd.Y;

            // Copy the RGB values into the array.
            Marshal.Copy(ptr, rgbValues, 0, bytes);

            Console.WriteLine("stride = {0}", Math.Abs(bmpData.Stride));

            // Set every third value to 255. A 24bpp bitmap will look red.  
            
            for (int i = 0; i < rgbValues.Length; i += 3)
            {
                for (int j = pointBegin.Y; j < pointEnd.Y; j++)
                {
                    if (i > j * stride + pointBegin.X && i < j * stride + pointEnd.X)
                    {
                        rgbValues[i] = 0;
                        rgbValues[i+1] = 204;
                        rgbValues[i+2] = 0;
                    }
                }
            }

            //rgbValues[counter + 1] ^= rgbValues[counter];

            // Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);

            bmp.Save(@"C:\Users\pati\Desktop\ExifReader\exif5.jpg");

            //Console.ReadKey();
        }
    }
}
