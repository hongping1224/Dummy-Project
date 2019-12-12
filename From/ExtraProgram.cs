using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;
using MathWorks.MATLAB.NET.Arrays;

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
                 string s = CreateParFile(DetrendFileName, FactorialResult, VGMModelFile, TemplateFilePath, ParFilePath, la);
                 SetupHeaderForKriging(DetrendingDSMPath);
                 FactorialKrigging(la, (l) =>
                    {
                        SetLabel(l, "Generating Contour...");
                        GenerateContour(FactorialResultPath, ImageOutPath);
                        SetLabel(l, "Done Generate Contour");
                    });
             });
        }

        public static void SetupHeaderForKriging(string filePath)
        {
            //setup header
            string currentContent = String.Empty;
            if (File.Exists(filePath))
            {
                currentContent = File.ReadAllText(filePath);
                File.WriteAllText(filePath, "DetrendingDSM\n3\nx\ny\nz\n" + currentContent);
            }
        }

        private static void GenerateContour(string path, string outpath)
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
                Z = new float[xsize, ysize];
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
            int ttt = bound.GetLength(0);
            int tttt = bound.GetLength(1);
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
                int k = coordinates[i, 0];
                int t = coordinates[i, 1];
                if (k < xsize && t < ysize)
                {
                    bi[k, ysize - t] = false;
                }
            }
            PImage.NetArray2Bitmap(bi, PixelFormat.Format24bppRgb).Save(outpath, ImageFormat.Bmp);
        }

        public static void PlanarDetrending(string openfile, string savefile, Label label)
        {
            SetLabel(label, "Planar Detrending...");
            PImage.processor.ho5dtdem(openfile, savefile);
            SetLabel(label, "Planar Detrending Done");
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

        public static void FactorialKrigging(Label label, Action<Label> onDone = null)
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
    }
}