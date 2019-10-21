using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using MathWorks.MATLAB.NET.Arrays;
using ImageProcess;
using System.Drawing.Imaging;
using System.Threading;

namespace StoneCount
{
    class PImage
    {

        public static Processor processor = new Processor();
        public static bool init = false;
        /*     static void run(string[] args) {
                 Bitmap b = new Bitmap("F:\\PersonalProject\\Stone\\contour.bmp");

                 Processor p = new Processor();
                 MWLogicalArray k0 = (MWLogicalArray)p.ToBinary(Bitmap2array(b), 0);
                 Bitmap o = Array2bitmap(k0, b.Width, b.Height);
                 o.Save("F:\\PersonalProject\\Stone\\out.bmp");
                 MWLogicalArray k1 = (MWLogicalArray)p.Inverse(k0);
                 Bitmap o1 = Array2bitmap(k1, b.Width, b.Height);
                 o1.Save("F:\\PersonalProject\\Stone\\out1.bmp");
             }
             */

        public static bool[,] Bitmap2NetArray(Bitmap orig)
        {
                        
            Bitmap myBitmap = new Bitmap(orig.Width, orig.Height,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            using (Graphics gr = Graphics.FromImage(myBitmap))
            {
                gr.DrawImage(orig, new Rectangle(0, 0, myBitmap.Width, myBitmap.Height));
            }

            PixelFormat Format = myBitmap.PixelFormat;
            bool[,] ImgData = new bool[myBitmap.Width, myBitmap.Height];
            BitmapData byteArray = myBitmap.LockBits(new Rectangle(0, 0, myBitmap.Width, myBitmap.Height), ImageLockMode.ReadWrite, Format);
            IntPtr source_scan = byteArray.Scan0;
            unsafe  //專案－＞屬性－＞建置－＞容許Unsafe程式碼須選取。           
            {
                byte* source_p = (byte*)source_scan.ToPointer();
                Parallel.For(0, byteArray.Height, h =>
                {
                    byte* new_p = source_p + (h* (byteArray.Width*3));
                    Parallel.For(0, byteArray.Width, w =>
                    {
                        byte* new_pw = new_p+(w*3);
                        ImgData[w, h] = new_pw[0] >= 100 ? true : false; 
                    });
                });
               /* for (int i = 0; i < byteArray.Width; i++)
                {
                    for (int j = 0; j < byteArray.Height; j++)
                    {
                        ImgData[i, j] = source_p[0] >=100 ? true:false;  
                        source_p+=3;
                    }
                }*/
            }
            myBitmap.UnlockBits(byteArray);
            return ImgData;
        }

        public static Bitmap NetArray2Bitmap(bool[,] ImgData, PixelFormat Format)
        {
            Bitmap source = new Bitmap(ImgData.GetLength(0), ImgData.GetLength(1), PixelFormat.Format24bppRgb);
            BitmapData sourceData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            IntPtr source_scan = sourceData.Scan0;
            unsafe
            {

                byte* source_p = (byte*)source_scan.ToPointer();
                Parallel.For(0, sourceData.Height, h =>
                {
                    byte* new_p = source_p + (h * (sourceData.Width * 3));
                    Parallel.For(0, sourceData.Width, w =>
                    {
                        byte* new_pw = new_p + (w * 3);
                        if (ImgData[w, h])
                        {
                            new_pw[0] = 255;  //R
                            new_pw[0 + 1] = 255;  //G
                            new_pw[0 + 2] = 255;   //B
                        }
                        else
                        {
                            new_pw[0] = 0;  //R
                            new_pw[0 + 1] = 0;  //G
                            new_pw[0 + 2] = 0;   //B

                        }
                    });
                });
                /*
                byte* source_p = (byte*)source_scan.ToPointer();
                for (int h = 0; h < sourceData.Height; h++)
                {
                    for (int w = 0; w < sourceData.Width; w++)
                    {
                        if (ImgData[h, w])
                        {
                            source_p[0] = 255;  //R
                            source_p[0+1] = 255;  //G
                            source_p[0+2] = 255;   //B
                        }
                        else
                        {
                            source_p[0] = 0;  //R
                            source_p[0+1] = 0;  //G
                            source_p[0+2] = 0;   //B
                          
                        }
                        source_p+=3;
                    }
                }*/
            }
            source.UnlockBits(sourceData);
            //Bitmap mono = new Bitmap(ImgData.GetLength(0), ImgData.GetLength(1), PixelFormat.Format1bppIndexed);
            //NativeIP.FastBinaryConvert(source, mono);
            return source;
        }


        //--------------------

        public static MWLogicalArray Bitmap2array(Bitmap bitmap)
        {
            //Get image dimensions
            int width = bitmap.Width;
            int height = bitmap.Height;
            //Declare the double array of grayscale values to be read from "bitmap"
            bool[,] bnew = new bool[height, width];

            //Loop to read the data from the Bitmap image into the double array
            int i, j;
            for (i = 0; i < width; i++)
            {
                for (j = 0; j < height; j++)
                {
                    Color pixelColor = bitmap.GetPixel(i, j);
                    double b = pixelColor.GetBrightness(); //the Brightness component
                    if (b > 0)
                    {
                        bnew[j, i] = true;
                    }
                    else
                    {
                        bnew[j, i] = false;
                    }
                    //Note that rows in C# correspond to columns in MWarray
                }
            }
            MWLogicalArray arr = new MWLogicalArray(bnew);
            return arr;
        }

        public unsafe static Bitmap Array2bitmap(MWArray arr, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format1bppIndexed);
            var bmdn = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite,
                                 bitmap.PixelFormat);
            var convScan0 = bmdn.Scan0;
            var convStride = bmdn.Stride;
            byte* destPixels = (byte*)(void*)convScan0;

            var srcLineIdx = 0;

            var hmax = bitmap.Height - 1;
            var wmax = bitmap.Width - 1;
            bool[,] image = (bool[,])((MWLogicalArray)arr).ToArray();

            for (int y = 0; y < hmax; y++)
            {
                // find indexes for source/destination lines

                // use addition, not multiplication?
                srcLineIdx += convStride;

                var srcIdx = srcLineIdx;
                for (int x = 0; x < wmax; x++)
                {
                    // index for source pixel (32bbp, rgba format)
                    srcIdx += 1;
                    //var r = pixel[2];
                    //var g = pixel[1];
                    //var b = pixel[0];

                    // could just check directly?
                    //if (Color.FromArgb(r,g,b).GetBrightness() > 0.01f)
                    if (!(image[y, x] == false))
                    {
                        // destination byte for pixel (1bpp, ie 8pixels per byte)
                        var idx = srcLineIdx + (x >> 3);
                        // mask out pixel bit in destination byte
                        destPixels[idx] |= (byte)(0x80 >> (x & 0x7));
                    }
                }
            }
            bitmap.UnlockBits(bmdn);

            /*
                        if (arr.IsLogicalArray) {

                            bool[,] image = (bool[,])((MWLogicalArray)arr).ToArray();
                            //Loop to read the data from the Bitmap image into the double array
                            int i, j;
                            for (i = 0; i < width; i++) {
                                for (j = 0; j < height; j++) {
                                    int bright = 0;
                                    if (image[j, i]) {
                                        bright = 1;
                                    }
                                    Color c = Color.FromArgb(bright, bright, bright);
                                    bitmap.SetPixel(i, j, c);
                                }
                            }
                        }*/
            Console.WriteLine(bitmap.PixelFormat);
            return bitmap;
        }
    }
}


