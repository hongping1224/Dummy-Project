using System;
using System.Threading;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using MathWorks.MATLAB.NET.Arrays;
using System.Drawing;

namespace StoneCount
{
    static class ExtraProgram
    {
        public static void DoAll(string DSMPath, string ImageOutPath, Label label)
        {
            string DetrendFileName = "DetrendingDSM.txt";
            string FactorialResult = "DetrendingDSM.out";
            string DetrendingDSMPath = Path.Combine(Directory.GetCurrentDirectory(), "factorialkriging", DetrendFileName);
            string FactorialResultPath = Path.Combine(Directory.GetCurrentDirectory(), "factorialkriging", FactorialResult);
            //hot5dtdem
            PlanarDetrending(DSMPath, DetrendingDSMPath, label);
            //vgm
            string VGMFile = Path.Combine(Directory.GetCurrentDirectory(), "VariogramModel.txt");
            string VGMModelFile = Path.Combine(Directory.GetCurrentDirectory(), "variogram_modelling.txt");
            string TemplateFilePath = Path.Combine(Directory.GetCurrentDirectory(), "factorialkriging", "template.par");
            string ParFilePath = Path.Combine(Directory.GetCurrentDirectory(), "factorialkriging", "fk_large.exe.par");

            VGM(DetrendingDSMPath, VGMFile, VGMModelFile, label, (la) =>
             {
                 //Facotorial
                 CreateParFile(DetrendFileName, FactorialResult, VGMModelFile, TemplateFilePath, ParFilePath, la);
                 SetupHeaderForKriging(DetrendingDSMPath);
                 FactorialKrigging(la, FactorialResultPath, (l) =>
                    {
                        SetLabel(l, "Generating Contour...");
                        GenerateSHPContour(FactorialResultPath, ImageOutPath,l, () => {
                            Bitmap zero = new Bitmap(ImageOutPath);
                            ExtraProgram.OpenPreviewForm(zero, "zero contour");
                        });
                        SetLabel(l, "Done Generate Contour");
                    });
             });
        }

        public static void SetupHeaderForKriging(string filePath,string outpath = "")
        {
            if (string.IsNullOrEmpty(outpath))
            {
                outpath = filePath;
            }
            //setup header
            string currentContent = String.Empty;
            if (File.Exists(filePath))
            {
                currentContent = File.ReadAllText(filePath);
                if (!currentContent.StartsWith("DetrendingDSM\n3\nx\ny\nz\n"))
                {
                    File.WriteAllText(outpath, "DetrendingDSM\n3\nx\ny\nz\n" + currentContent);
                }
            }
        }

        public static void ReformatKriggingResult(string openfile,string savepath)
        {
            string[] krigResult = File.ReadAllLines(openfile);
            int columenum = int.Parse(krigResult[1]);
            float diff, sx, sy;
            StringBuilder sb = new StringBuilder();
            int xsize, ysize;
            {
                string line = krigResult[2 + columenum].Replace("      ", ",");
                line = line.Replace("     ", ",");
                string[] colume = line.Split(',');
                sx = float.Parse(colume[1]);
                sy = float.Parse(colume[2]);
                line = krigResult[krigResult.Length - 1].Replace("      ", ",");
                line = line.Replace("     ", ",");
                colume = line.Split(',');
                float ex = float.Parse(colume[1]);
                float ey = float.Parse(colume[2]);
                line = krigResult[krigResult.Length - 2].Replace("      ", ",");
                line = line.Replace("     ", ",");
                colume = line.Split(',');
                diff = Math.Abs(ex - float.Parse(colume[1]));
                xsize = (int)((ex - sx) / diff) + 1;
                ysize = (int)((ey - sy) / diff) + 1;
               
            }
            for (int i = 2 + columenum; i < krigResult.Length; i++)
            {
                string line = krigResult[i].Replace("      ", ",");
                line = line.Replace("     ", ",");
                string[] colume = line.Split(',');
                float x = float.Parse(colume[1]);
                float y = float.Parse(colume[2]);
                float z = float.Parse(colume[5]);
                int xindex = (int)((x - sx) / diff);
                int yindex = (int)((y - sy) / diff);
                sb.AppendFormat("{0}\t{1}\t{2}\n", x, y, z);
            }
            File.WriteAllText(savepath,sb.ToString());
        }

        public static Bitmap GenerateContour(string path)
        {
            //path = @"F:\PersonalProject\Stone\UIForm\From\bin\x64\Debug\factorialkriging\ho1S4RPdtNZdsm-49.out";
            string[] krigResult = File.ReadAllLines(path);
            int columenum = int.Parse(krigResult[1]);
            float[,] Z;
            float diff, sx, sy;
            int xsize, ysize;
            {
                string line = krigResult[2 + columenum].Replace("      ", ",");
                line = line.Replace("     ", ",");
                string[] colume = line.Split(',');
                sx = float.Parse(colume[1]);
                sy = float.Parse(colume[2]);
                line = krigResult[krigResult.Length - 1].Replace("      ", ",");
                line = line.Replace("     ", ",");
                colume = line.Split(',');
                float ex = float.Parse(colume[1]);
                float ey = float.Parse(colume[2]);
                line = krigResult[krigResult.Length - 2].Replace("      ", ",");
                line = line.Replace("     ", ",");
                colume = line.Split(',');
                diff = Math.Abs(ex - float.Parse(colume[1]));
                xsize = (int)((ex - sx) / diff) + 1;
                ysize = (int)((ey - sy) / diff) + 1;
                Z = new float[xsize,ysize];
            }
            for (int i = 2 + columenum; i < krigResult.Length; i++)
            {
                string line = krigResult[i].Replace("      ", ",");
                line = line.Replace("     ", ",");
                string[] colume = line.Split(',');
                float x = float.Parse(colume[1]);
                float y = float.Parse(colume[2]);
                float z = float.Parse(colume[5]);
                int xindex = (int)((x - sx) / diff);
                int yindex = (int)((y - sy) / diff);
                Z[xindex, yindex] = z;
            }
            MWNumericArray arr = new MWNumericArray(Z);
            MWArray BoundResult = PImage.processor.Contour(arr);
            double[,] bound = (double[,])BoundResult.ToArray();
            int[,] coordinates = new int[bound.GetLength(1), 2];
            for (int i = 0; i < bound.GetLength(1); i++)
            {
                coordinates[i, 0] = (int)Math.Round(bound[0, i]);
                coordinates[i, 1] = (int)Math.Round(bound[1, i]);
            }
            //Bitmap bi = new Bitmap(Z.GetLength(0), Z.GetLength(1), PixelFormat.Format24bppRgb);
            bool[,] bi = new bool[Z.GetLength(0), Z.GetLength(1)];

            for (int i = 0; i < xsize; i++)
            {
                for (int j = 0; j < ysize; j++)
                {
                    bi[i, j] = true;
                }
            }
            for (int i = 0; i < bound.GetLength(1); i++)
            {
                int x = coordinates[i, 1];
                int y = coordinates[i, 0];
                if (x < xsize && y < ysize)
                {
                    bi[x, ysize-y-1] = false;
                }
            }
            return PImage.NetArray2Bitmap(bi, PixelFormat.Format24bppRgb);
        }

        public static void GenerateSHPContour(string openfile, string savefile, Label label,Action OnDone)
        {
            string intemediatekrggingResult = Path.Combine(Directory.GetCurrentDirectory(), "zerocontour", "kriged_result_img.txt");
            ExtraProgram.ReformatKriggingResult(openfile, intemediatekrggingResult);
            string intemediate = Path.Combine(Directory.GetCurrentDirectory(), "zerocontour", "OUTPUT.shp");

            SetLabel(label, "Generating Contour...");
            string[] krigResult = File.ReadAllLines(intemediatekrggingResult);
            float diff, sx, sy;
            int xsize, ysize;
            {
                string[] colume = krigResult[0].Split('\t');
                sx = float.Parse(colume[0]);
                sy = float.Parse(colume[1]);
                colume = krigResult[krigResult.Length - 1].Split('\t');
                float ex = float.Parse(colume[0]);
                float ey = float.Parse(colume[1]);
                colume = krigResult[krigResult.Length - 2].Split('\t');
                diff = Math.Abs(ex - float.Parse(colume[0]));
                xsize = (int)((ex - sx) / diff) + 1;
                ysize = (int)((ey - sy) / diff) + 1;
            }

            BackgroundWorker backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += new DoWorkEventHandler((s, ee) =>
            {
                string strCmdText;
                string path = Directory.GetCurrentDirectory();
                string exe_path = Path.Combine(path, "zerocontour/gdal_contour");
                string command = string.Format("-b 1 -a ELEV -i 10.0 {0} {1}", intemediatekrggingResult, intemediate);
                strCmdText = "/c " + exe_path+ " "+command;
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                startInfo.FileName = "cmd.exe";
                startInfo.WorkingDirectory = Path.Combine(path);
                startInfo.Arguments = strCmdText;
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();

            });
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler((a, ee) =>
            {
                BackgroundWorker backgroundWorker2 = new BackgroundWorker();
                backgroundWorker2.DoWork += new DoWorkEventHandler((s, eee) =>
                {
                    string strCmdText;
                    string path = Directory.GetCurrentDirectory();
                    string exe_path = Path.Combine(path, "zerocontour/gdal_rasterize");
                    string command = string.Format("-burn 255 -burn 255 -burn 255 -ot Byte -ts {0} {1} -l OUTPUT {2} {3}", xsize*4,ysize*4, intemediate, savefile);
                    strCmdText = "/c " + exe_path + " " + command;
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    startInfo.FileName = "cmd.exe";
                    startInfo.WorkingDirectory = Path.Combine(path);
                    startInfo.Arguments = strCmdText;
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();

                });
                backgroundWorker2.RunWorkerCompleted += new RunWorkerCompletedEventHandler((aa, eee) =>
                {
                    File.Delete(intemediatekrggingResult);
                    File.Delete(intemediate.Replace(".shp",".dbf"));
                    File.Delete(intemediate.Replace(".shp", ".shx"));
                    File.Delete(intemediate);
                    if(OnDone != null)
                    {
                        OnDone();
                    }
                });
                backgroundWorker2.RunWorkerAsync();
            });
            backgroundWorker1.RunWorkerAsync();
        }
                
        public static void OpenPreviewForm(Bitmap image, string title = "Preview Image")
        {
            UI.ImagePreview form = new UI.ImagePreview();
            form.PictureBox1.Image = image;
            form.Text = title;
            form.Show();
            form.Refresh();
        }

        public static void PlanarDetrending(string openfile, string savefile, Label label)
        {
            OpenPreviewForm(DrawInputDSM(openfile), "Original DSM");
            SetLabel(label, "Planar Detrending...");
            Thread.Sleep(100);
            PImage.processor.ho5dtdem(openfile, savefile);
            SetLabel(label, "Planar Detrending Done");
            OpenPreviewForm(DrawDetrendDSM(savefile), "Detrended DSM");
        }

        public static string CreateParFile(string DSMFile, string ReusltFile, string VGMFile, string TemplateFile, string ParFile, Label label)
        {
            string[] KrigRaw = File.ReadAllLines(VGMFile);
            if (KrigRaw.Length != 4)
            {
                return "Invalid File";
            }
            string[] a = KrigRaw[1].Split(' ');
            float nug = 0;
            if (!float.TryParse(a[5], out nug))
            {
                return "Invalid nug";

            }
            string[] b = KrigRaw[2].Split(' ');
            float cc = 0;
            if (!float.TryParse(b[4], out cc))
            {
                return "Invalid cc";

            }
            float a_hmax = 0;
            if (!float.TryParse(b[5], out a_hmax))
            {
                return "Invalid a_hmax";
            }
            string[] c = KrigRaw[3].Split(' ');
            float cc2 = 0;
            if (!float.TryParse(c[4], out cc2))
            {
                return "Invalid cc2";
            }
            float a_hmax2 = 0;
            if (!float.TryParse(c[5], out a_hmax2))
            {
                return "Invalid a_hmax2";

            }
            //read in template 
            string Par = File.ReadAllText(TemplateFile);
            //replace string
            Par = Par.Replace("$(cc)", cc.ToString());
            Par = Par.Replace("$(a_hmax)", a_hmax.ToString());
            Par = Par.Replace("$(cc2)", cc2.ToString());
            Par = Par.Replace("$(a_hmax2)", a_hmax2.ToString());
            Par = Par.Replace("$(filename)", DSMFile);
            Par = Par.Replace("$(outname)", ReusltFile);
            //save par and return save path
            File.WriteAllText(ParFile, Par);
            return "";
        }

        public static void FactorialKrigging(Label label,String resultPath, Action<Label> onDone = null)
        {
   
            SetLabel(label, "Factorial Kriging...");
            BackgroundWorker backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += new DoWorkEventHandler((s, ee) =>
            {
                string strCmdText;
                string path = Directory.GetCurrentDirectory();
                string exe_path = Path.Combine(path, "factorialkriging/filter_Auto");
                strCmdText = "/c " + exe_path;
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                startInfo.FileName = "cmd.exe";
                startInfo.WorkingDirectory = Path.Combine(path, "factorialkriging");
                startInfo.Arguments = strCmdText;
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();

            });
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler((a, ee) =>
            {
                SetLabel(label, "Kriging Done");
                Bitmap[] bitmaps = DrawKrigingShortAndLongComponent(resultPath);
                if(bitmaps == null)
                {
                    return;
                }
                OpenPreviewForm(bitmaps[0], "Local Component");
                OpenPreviewForm(bitmaps[1], "Region Component");
                OpenPreviewForm(bitmaps[2], "Combine Component");
                if (onDone != null)
                {
                    onDone(label);
                }
                backgroundWorker1.Dispose();
            });
            backgroundWorker1.RunWorkerAsync();
        }

        public static void VGM(string openfile, string savefile,string modelfile, Label label, Action<Label> onDone = null)
        {
            SetLabel(label, "Running vgm...");
            BackgroundWorker backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += new DoWorkEventHandler((s, ee) =>
            {
                string strCmdText;

                strCmdText = "/c Rscript --vanilla ./vgm/ho1vgm.r " + openfile + " " + savefile+" "+ modelfile;
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                startInfo.FileName = "cmd.exe";
                string path = Directory.GetCurrentDirectory();
                startInfo.WorkingDirectory = path;
                startInfo.Arguments = strCmdText;
                startInfo.UseShellExecute = true;
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();

            });
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler((a, ee) =>
            {
               
                SetLabel(label, "vgm Done");
                if (onDone != null)
                {
                    onDone(label);
                }
                backgroundWorker1.Dispose();
            });
            backgroundWorker1.RunWorkerAsync();
        }
        public static void SetLabel(Label label, string s)
        {
            if (label != null)
            {
                label.Text = s;
                label.Refresh();
            }
        }

        public static Bitmap DrawInputDSM(string name)
        {
            string[] dsm = File.ReadAllLines(name);
            float[,] Z;
            float diff, sx, sy;
            int xsize, ysize;
            {
                string[] colume = dsm[0].Split(' ');
                sx = float.Parse(colume[0]);
                sy = float.Parse(colume[1]);
                colume = dsm[dsm.Length - 1].Split(' ');
                float ex = float.Parse(colume[0]);
                float ey = float.Parse(colume[1]);
                colume = dsm[1].Split(' ');
                diff = Math.Abs(sy - float.Parse(colume[1]));
                xsize = (int)((ex - sx) / diff) + 1;
                ysize = (int)((ey - sy) / diff) + 1;
                Z = new float[xsize+2,ysize+2];
            }
            float maxz = float.MinValue;
            float minz = float.MaxValue;
            for (int i = 0; i < dsm.Length; i++)
            {
                string[] colume = dsm[i].Split(' ');
                float x = float.Parse(colume[0]);
                float y = float.Parse(colume[1]);
                float z = float.Parse(colume[2]);
                int xindex = (int)((x - sx) / diff);
                int yindex = (int)((y - sy) / diff);
                Z[xindex,yindex] = z;
                Console.WriteLine("a");
                maxz = Math.Max(z, maxz);
                minz = Math.Min(z, minz);
            }
            Color[,] bi = new Color[Z.GetLength(0), Z.GetLength(1)];

            for (int i = 0; i < xsize; i++)
            {
                for (int j = 0; j < ysize; j++)
                {
                    bi[i, ysize-j-1] = RampGenerator.HotCold(Z[i,j],maxz,minz);
                }
            }
            return PImage.NetArray2Bitmap(bi);
        }
        public static Bitmap DrawDetrendDSM(string name)
        {
            string[] dsm = File.ReadAllLines(name);
            float[,] Z;
            float diff, sx, sy;
            int xsize, ysize;
            {
                string[] colume = dsm[0].Split(' ');
                sx = float.Parse(colume[0]);
                sy = float.Parse(colume[1]);
                colume = dsm[dsm.Length - 1].Split(' ');
                float ex = float.Parse(colume[0]);
                float ey = float.Parse(colume[1]);
                colume = dsm[1].Split(' ');
                diff = Math.Abs(sy - float.Parse(colume[1]));
                xsize = (int)((ex - sx) / diff) + 1;
                ysize = (int)((ey - sy) / diff) + 1;
                Z = new float[xsize+2, ysize+2];
            }
            float maxz = float.MinValue;
            float minz = float.MaxValue;
            for (int i = 0; i < dsm.Length; i++)
            {
                string[] colume = dsm[i].Split(' ');
                float x = float.Parse(colume[0]);
                float y = float.Parse(colume[1]);
                float z = float.Parse(colume[2]);
                int xindex = (int)((x - sx) / diff);
                int yindex = (int)((y - sy) / diff);
                Z[xindex, yindex] = z;
                maxz = Math.Max(z, maxz);
                minz = Math.Min(z, minz);
            }
            Color[,] bi = new Color[Z.GetLength(0), Z.GetLength(1)];
            for (int i = 0; i < xsize; i++)
            {
                for (int j = 0; j < ysize; j++)
                {
                    bi[i, ysize-j-1] = RampGenerator.HotCold(Z[i, j], maxz, minz);
                }
            }
            return PImage.NetArray2Bitmap(bi);

        }
        public static Bitmap[] DrawKrigingShortAndLongComponent(string name)
        {
            try
            {
                string[] krigResult = File.ReadAllLines(name);
                int columenum = int.Parse(krigResult[1]);
                float[,] Region;
                float[,] Local;
                float diff, sx, sy;
                int xsize, ysize;
                {
                    string line = krigResult[2 + columenum].Replace("      ", ",");
                    line = line.Replace("     ", ",");
                    string[] colume = line.Split(',');
                    sx = float.Parse(colume[1]);
                    sy = float.Parse(colume[2]);
                    line = krigResult[krigResult.Length - 1].Replace("      ", ",");
                    line = line.Replace("     ", ",");
                    colume = line.Split(',');
                    float ex = float.Parse(colume[1]);
                    float ey = float.Parse(colume[2]);
                    line = krigResult[krigResult.Length - 2].Replace("      ", ",");
                    line = line.Replace("     ", ",");
                    colume = line.Split(',');
                    diff = Math.Abs(ex - float.Parse(colume[1]));
                    xsize = (int)((ex - sx) / diff) + 1;
                    ysize = (int)((ey - sy) / diff) + 1;
                    Local = new float[xsize, ysize];
                    Region = new float[xsize, ysize];
                }
                float maxz = float.MinValue;
                float minz = float.MaxValue;
                float maxz2 = float.MinValue;
                float minz2 = float.MaxValue;
                float maxzC = float.MinValue;
                float minzC = float.MaxValue;
                for (int i = 2 + columenum; i < krigResult.Length; i++)
                {
                    string line = krigResult[i].Replace("      ", ",");
                    line = line.Replace("     ", ",");
                    string[] colume = line.Split(',');
                    float x = float.Parse(colume[1]);
                    float y = float.Parse(colume[2]);
                    float z = float.Parse(colume[5]);
                    float z2 = float.Parse(colume[6]);
                    int xindex = (int)((x - sx) / diff);
                    int yindex = (int)((y - sy) / diff);
                    Local[xindex, yindex] = z;
                    Region[xindex, yindex] = z2;
                    maxz = Math.Max(z, maxz);
                    minz = Math.Min(z, minz);
                    maxz2 = Math.Max(z2, maxz2);
                    minz2 = Math.Min(z2, minz2);
                    maxzC = Math.Max(z + z2, maxzC);
                    minzC = Math.Min(z + z2, minzC);
                }
                Color[,] Localbi = new Color[Local.GetLength(0), Local.GetLength(1)];
                for (int i = 0; i < xsize; i++)
                {
                    for (int j = 0; j < ysize; j++)
                    {
                        Localbi[i, ysize-j-1] = RampGenerator.HotCold(Local[i, j], maxz, minz);
                    }
                }
                Color[,] Regionbi = new Color[Region.GetLength(0), Region.GetLength(1)];
                for (int i = 0; i < xsize; i++)
                {
                    for (int j = 0; j < ysize; j++)
                    {
                        Regionbi[i, ysize - j-1] = RampGenerator.HotCold(Region[i, j], maxz2, minz2);
                    }
                }
                Color[,] Combinebi = new Color[Region.GetLength(0), Region.GetLength(1)];
                for (int i = 0; i < xsize; i++)
                {
                    for (int j = 0; j < ysize; j++)
                    {
                        Combinebi[i, ysize - j-1] = RampGenerator.HotCold(Region[i, j] + Local[i, j], maxzC, minzC);
                    }
                }
            return new Bitmap[] { PImage.NetArray2Bitmap(Localbi), PImage.NetArray2Bitmap(Regionbi), PImage.NetArray2Bitmap(Combinebi) };
            }
            catch
            {
                return null;
            }
        }
    }
}