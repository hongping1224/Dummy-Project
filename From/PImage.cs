using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using MathWorks.MATLAB.NET.Arrays;
using ImageProcess;
using System.Drawing.Imaging;
namespace StoneCount {
    class PImage{

        public static Processor processor = new Processor();

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

        /*   public static bool[,] Bitmap2Array(Bitmap myBitmap) {
               int LayerNumber = 0;
               PixelFormat Format = new PixelFormat();
               if (myBitmap.PixelFormat == PixelFormat.Format8bppIndexed) {
                   LayerNumber = 1;
                   Format = PixelFormat.Format8bppIndexed;
               }   //判斷8位元灰階影像
               char[,,] ImgData = new char[myBitmap.Height, myBitmap.Width, LayerNumber];
               BitmapData byteArray = myBitmap.LockBits(new Rectangle(0, 0, myBitmap.Width, myBitmap.Height), ImageLockMode.ReadWrite, Format);
               int ByteOfSkip = byteArray.Stride - byteArray.Width * LayerNumber;
               unsafe　　//專案－＞屬性－＞建置－＞容許Unsafe程式碼須選取。           
               {
                   byte* imgPtr = (byte*)(byteArray.Scan0);
                   for (int i = 0; i < byteArray.Height; i++) {
                       for (int j = 0; j < byteArray.Width; j++) {
                           for (int k = 0; k < LayerNumber; k++)
                               ImgData[i, j, k] = (char)*(imgPtr + k);
                           imgPtr += LayerNumber;
                       }
                       imgPtr += ByteOfSkip;
                   }
               }
               myBitmap.UnlockBits(byteArray);
               return ImgData;
           }

           public static Bitmap Array2Bitmap(bool[,] ImgData, PixelFormat Format) {
               int W = ImgData.GetLength(0);
               int H = ImgData.GetLength(1);
               Bitmap myBitmap = new Bitmap(H, W, Format);
               BitmapData byteArray = myBitmap.LockBits(new Rectangle(0, 0, H, W), ImageLockMode.WriteOnly, Format);
               ColorPalette tempPalette;
               int LayerNumber = 0;
               if (Format == PixelFormat.Format24bppRgb) {
                   LayerNumber = 3;
               }//判斷24位元彩色影像(R,G,B)
               if (Format == PixelFormat.Format8bppIndexed) {
                   LayerNumber = 1;
                   using (Bitmap tempBmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed)) {
                       tempPalette = tempBmp.Palette;
                   }
                   for (int i = 0; i < 256; i++) {
                       tempPalette.Entries[i] = Color.FromArgb(i, i, i);
                   }
                   myBitmap.Palette = tempPalette;
               }//判斷8位元灰階影像LayerNumber

               int ByteOfSkip = byteArray.Stride - byteArray.Width * LayerNumber;
               unsafe　　//專案－＞屬性－＞建置－＞容許Unsafe程式碼須選取。           
               {
                   byte* imgPtr = (byte*)(byteArray.Scan0);
                   for (int i = 0; i < byteArray.Height; i++) {
                       for (int j = 0; j < byteArray.Width; j++) {
                           for (int k = 0; k < LayerNumber; k++)
                               *(imgPtr + k) = (byte)ImgData[i, j, k];
                           imgPtr += LayerNumber;
                       }
                       imgPtr += ByteOfSkip;
                   }
               }
               myBitmap.UnlockBits(byteArray);
               return myBitmap;
           }
           }
           */
        //--------------------

        public static MWLogicalArray Bitmap2array(Bitmap bitmap) {
            //Get image dimensions
            int width = bitmap.Width;
            int height = bitmap.Height;
            //Declare the double array of grayscale values to be read from "bitmap"
            bool[,] bnew = new bool[height, width];

            //Loop to read the data from the Bitmap image into the double array
            int i, j;
            for (i = 0; i < width; i++) {
                for (j = 0; j < height; j++) {
                    Color pixelColor = bitmap.GetPixel(i, j);
                    double b = pixelColor.GetBrightness(); //the Brightness component
                    if (b > 0) {
                        bnew[j, i] = true;
                    } else {
                        bnew[j, i] = false;
                    }
                    //Note that rows in C# correspond to columns in MWarray
                }
            }
            MWLogicalArray arr = new MWLogicalArray(bnew);
            return arr;
        }

        public unsafe static Bitmap Array2bitmap(MWArray arr, int width, int height) {
            Bitmap bitmap = new Bitmap(width, height,PixelFormat.Format1bppIndexed);
            var bmdn = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite,
                                 bitmap.PixelFormat);
            var convScan0 = bmdn.Scan0;
            var convStride = bmdn.Stride;
            byte* destPixels = (byte*)(void*)convScan0;

            var srcLineIdx = 0;

            var hmax = bitmap.Height - 1;
            var wmax = bitmap.Width - 1;
            bool[,] image = (bool[,])((MWLogicalArray)arr).ToArray();

            for (int y = 0; y < hmax; y++) {
                // find indexes for source/destination lines

                // use addition, not multiplication?
                srcLineIdx += convStride;

                var srcIdx = srcLineIdx;
                for (int x = 0; x < wmax; x++) {
                    // index for source pixel (32bbp, rgba format)
                    srcIdx += 1;
                    //var r = pixel[2];
                    //var g = pixel[1];
                    //var b = pixel[0];

                    // could just check directly?
                    //if (Color.FromArgb(r,g,b).GetBrightness() > 0.01f)
                    if (!(image[y, x] == false)) {
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


