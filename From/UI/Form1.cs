using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MathWorks.MATLAB.NET.Arrays;
using System.Drawing.Imaging;

namespace StoneCount {
    public partial class Form1 : Form {
        public static Form1 instance;
        BackgroundWorker backgroundWorker1;
        public Form1() {
            InitializeComponent();
            instance = this;
            label1.Text = "Starting MR";
        
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += new DoWorkEventHandler((obj, e) => {
                Bitmap b = new Bitmap("iti.bmp");
                PImage.processor.Erosion(PImage.Bitmap2array(b), "square", 2);
                PImage.init = true;
            });
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler((obj,e) => {
                label1.Text = "MR Done";
             
                label1.Refresh();
            });
            backgroundWorker1.RunWorkerAsync();
        }
        public SaveFileDialog GetSaveFileDialog() {
            return saveFileDialog1;
        }
        public OpenFileDialog GetOpenFileDialog() {
            return openFileDialog1;
        }

        private void toolBarToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Application.OpenForms.OfType<ImageForm>().Count() != 0) {
                if (Application.OpenForms.OfType<ToolBar>().Count() == 0) {
                    if (UI.ProjectForm.instance.ProcessingImageForm != null)
                    {
                        UI.ProjectForm.instance.ProcessingImageForm.OpenToolBar(UI.ProjectForm.instance.currentMode);
                    }
                }
            }
        }
        private void logsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Application.OpenForms.OfType<ImageForm>().Count() != 0) {
                if (Application.OpenForms.OfType<Logs>().Count() == 0) {
                    if (UI.ProjectForm.instance.ProcessingImageForm != null)
                    {
                        UI.ProjectForm.instance.ProcessingImageForm.OpenLogs();
                    }
                }
            }
        }

        private void startProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var project = new UI.ProjectForm();
            project.Show();
        }

        BackgroundWorker backgroundWorker2 = new BackgroundWorker();
        public void CreateShpFile(string output, string ellipse, string boundaries , string refference)
        {
            while (backgroundWorker2.IsBusy)
            {
            }
            backgroundWorker2 = new BackgroundWorker();
            backgroundWorker2.DoWork += new DoWorkEventHandler((s, ee) => {
                string strCmdText;
                string path = Directory.GetCurrentDirectory();
                string exe_path = Path.Combine(path, "DrawShp","main.exe");
                strCmdText = String.Format("/c {4} --o={0} --eclipse={1} --boundaries={2} --ref={3}", output, ellipse, boundaries, refference, exe_path);
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.UseShellExecute = true;
                startInfo.Arguments = strCmdText;
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
            });
            backgroundWorker2.RunWorkerCompleted += new RunWorkerCompletedEventHandler((a, ee) => {

            });
            backgroundWorker2.RunWorkerAsync();
        }

        private void generateLocalWorldFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Select Image File";
            openFileDialog1.Filter = "bitmap files (*.bmp)|*.bmp|All files (*.*)|*.*";
            string openfile;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                openfile = openFileDialog1.FileName;
                if (!openfile.EndsWith(".tif"))
                {
                    Bitmap b = new Bitmap(openfile);
                    string extension = Path.GetExtension(openfile);
                    b.Save(openfile.Replace(extension, ".tif"), System.Drawing.Imaging.ImageFormat.Tiff);
                    string[] tfw = new string[6];
                    tfw[0] = "1";
                    tfw[1] = "0";
                    tfw[2] = "0";
                    tfw[3] = "-1";
                    tfw[4] = "0.5";
                    tfw[5] = (b.Height - 0.5f).ToString();
                    File.WriteAllLines(openfile.Replace(extension, ".tfw"), tfw);
                }

            }
            else
            {
                return;
            }
        }

        private void zerocontourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Pick an DSM file";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            string openfile, savefile;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                openfile = openFileDialog1.FileName;
            }
            else
            {
                return;
            }
            saveFileDialog1.Title = "Save output contour Image";
            saveFileDialog1.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                savefile = saveFileDialog1.FileName;
            }
            else
            {
                return;
            }
            ExtraProgram.DoAll(openfile, savefile, label1);
        }

        private void kriggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ExtraProgram.FactorialKrigging();
        }
    }
}
