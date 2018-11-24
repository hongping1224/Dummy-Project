using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace From {
    class NativeIP {

        public static unsafe void FastInvertBinary(Bitmap src, Bitmap conv) {
            // Lock source and destination in memory for unsafe access
            var bmbo = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadOnly,
                                     src.PixelFormat);
            var bmdn = conv.LockBits(new Rectangle(0, 0, conv.Width, conv.Height), ImageLockMode.ReadWrite,
                                     conv.PixelFormat);

            var srcScan0 = bmbo.Scan0;
            var convScan0 = bmdn.Scan0;

            var srcStride = bmbo.Stride;
            var convStride = bmdn.Stride;

            byte* sourcePixels = (byte*)(void*)srcScan0;
            byte* destPixels = (byte*)(void*)convScan0;

            var srcLineIdx = 0;
            var convLineIdx = 0;
            var hmax = src.Height - 1;
            var wmax = src.Width - 1;
            for (int y = 0; y < hmax; y++) {
                // find indexes for source/destination lines

                // use addition, not multiplication?
                srcLineIdx += srcStride;
                convLineIdx += convStride;

                var srcIdx = srcLineIdx;
                for (int x = 0; x < wmax; x++) {
                    // index for source pixel (32bbp, rgba format)
                    srcIdx += 1;
                    //var r = pixel[2];
                    //var g = pixel[1];
                    //var b = pixel[0];

                    // could just check directly?
                    //if (Color.FromArgb(r,g,b).GetBrightness() > 0.01f)
                    // destination byte for pixel (1bpp, ie 8pixels per byte)
                    var idx = convLineIdx + (x >> 3);
                    // mask out pixel bit in destination byte
                    destPixels[idx] = (byte)~sourcePixels[idx];
                }
            }
            src.UnlockBits(bmbo);
            conv.UnlockBits(bmdn);
        }

        public static void FastBinaryConvert(Bitmap src, Bitmap conv) {
            Console.WriteLine(src.PixelFormat);
            Console.WriteLine(conv.PixelFormat);
            // Lock source and destination in memory for unsafe access
            var bmbo = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadOnly,
                                     src.PixelFormat);
            var bmdn = conv.LockBits(new Rectangle(0, 0, conv.Width, conv.Height), ImageLockMode.ReadWrite,
                                     conv.PixelFormat);

            var srcScan0 = bmbo.Scan0;
            var convScan0 = bmdn.Scan0;

            var srcStride = bmbo.Stride;
            var convStride = bmdn.Stride;
            unsafe {
                byte* sourcePixels = (byte*)(void*)srcScan0;
                byte* destPixels = (byte*)(void*)convScan0;

                var srcLineIdx = 0;
                var convLineIdx = 0;
                var hmax = src.Height - 1;
                var wmax = src.Width - 1;
                for (int y = 0; y < hmax; y++) {
                    // find indexes for source/destination lines

                    // use addition, not multiplication?
                    srcLineIdx += srcStride;
                    convLineIdx += convStride;

                    var srcIdx = srcLineIdx;
                    for (int x = 0; x < wmax; x++) {
                        // index for source pixel (32bbp, rgba format)
                        srcIdx += 1;
                        //var r = pixel[2];
                        //var g = pixel[1];
                        //var b = pixel[0];

                        // could just check directly?
                        //if (Color.FromArgb(r,g,b).GetBrightness() > 0.01f)
                        if (!(sourcePixels[srcIdx] == 0)) {
                            // destination byte for pixel (1bpp, ie 8pixels per byte)
                            var idx = convLineIdx + (x >> 3);
                            // mask out pixel bit in destination byte
                            destPixels[idx] |= (byte)(0x80 >> (x & 0x7));
                        }
                    }
                }
            }
            src.UnlockBits(bmbo);
            conv.UnlockBits(bmdn);
        }

    }
}
