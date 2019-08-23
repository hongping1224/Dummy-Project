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

namespace StoneCount {
    public partial class Form1 : Form {
        BackgroundWorker backgroundWorker1;
        public Form1() {
            InitializeComponent();
            label1.Text = "Starting MR";
            planarDetrendingToolStripMenuItem.Enabled = false;
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += new DoWorkEventHandler(initMatlab);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.RunWorkerAsync();
        }
        public SaveFileDialog GetSaveFileDialog() {
            return saveFileDialog1;
        }
        public OpenFileDialog GetOpenFileDialog() {
            return openFileDialog1;
        }

        public void initMatlab(object sender, DoWorkEventArgs e) {
            Bitmap b = new Bitmap("iti.bmp");
            PImage.processor.Erosion(PImage.Bitmap2array(b), "square", 2);
            PImage.init = true;

        }
        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            label1.Text = "MR Done";
            planarDetrendingToolStripMenuItem.Enabled = true;
            label1.Refresh();
        }

        ImageForm form2;

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            openFileDialog1.Title = "Pick an image file";
            openFileDialog1.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                var filePath = openFileDialog1.FileName;
                Bitmap b = new Bitmap(filePath);
                Console.WriteLine("Start Form2");
                if (form2 != null) {
                    form2.Close();
                }
                form2 = new ImageForm(b, this.Location);
                form2.mainForm = this;
                form2.Show();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            saveFileDialog1.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                var filePath = saveFileDialog1.FileName;
                form2.CurrentImage.Save(filePath,System.Drawing.Imaging.ImageFormat.Bmp);
            }
        }



        private void toolBarToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Application.OpenForms.OfType<ImageForm>().Count() != 0) {
                if (Application.OpenForms.OfType<ToolBar>().Count() == 0) {
                    form2.OpenToolBar();
                }
            }
        }

        private void logsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Application.OpenForms.OfType<ImageForm>().Count() != 0) {
                if (Application.OpenForms.OfType<Logs>().Count() == 0) {
                    form2.OpenLogs();
                }
            }
        }

        private void batchProcessToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Application.OpenForms.OfType<BatchProcess>().Count() == 0) {
                BatchProcess batchProcess = new BatchProcess();
                StartPosition = FormStartPosition.Manual;
                Point p = Location;
                p.Y += 90;
                batchProcess.Show();
                batchProcess.Location = p;
                batchProcess.Show();
            } else {
                BatchProcess batchProcess = Application.OpenForms.OfType<BatchProcess>().First();
                batchProcess.Focus();
            }

        }

        private void ComparisonToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Application.OpenForms.OfType<ImageForm>().Count() != 0) {
                openFileDialog1.Title = "Pick an image file";
                openFileDialog1.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    var filePath = openFileDialog1.FileName;
                    Bitmap b = new Bitmap(filePath);
                    if (form2 != null) {
                        var w = new ComaprisonWindow(form2, b);
                        w.Show();
                    }
                }
            }
        }

        private void planarDetrendingToolStripMenuItem_Click(object sender, EventArgs e) {

            openFileDialog1.Title = "Pick an DSM file";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            string openfile, savefile;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                openfile = openFileDialog1.FileName;
            } else {
                return;
            }
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                savefile = saveFileDialog1.FileName;
            } else {
                return;
            }
            PImage.processor.ho5dtdem(openfile, savefile);
        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void vgmToolStripMenuItem_Click(object sender, EventArgs e) {
            openFileDialog1.Title = "Pick an DSM file";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            string openfile, savefile;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                openfile = openFileDialog1.FileName;
            } else {
                return;
            }
            saveFileDialog1.Title = "Variogram Modelling Save As";
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                savefile = saveFileDialog1.FileName;
                if (File.Exists(savefile)) {
                    File.Delete(savefile);
                }
            } else {
                return;
            }
            label1.Text = "Running vgm";
            backgroundWorker1.DoWork += new DoWorkEventHandler((s, ee) => {
                string strCmdText;
                strCmdText = "/c Rscript --vanilla ./vgm/ho1vgm.r " + openfile + " " + savefile;
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = strCmdText;
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();

            });
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler((a, ee) => {
                label1.Text = "vgm Done";
                label1.Refresh();
            });
            backgroundWorker1.RunWorkerAsync();
        }

        private void factorialKrigingToolStripMenuItem_Click(object sender, EventArgs e) {
            label1.Text = "Factorial Kriging";
            BackgroundWorker backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += new DoWorkEventHandler((s, ee) => {
                string strCmdText;
                string path = Directory.GetCurrentDirectory();
                string exe_path = Path.Combine(path, "factorialkriging/filter_large");
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
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler((a, ee) => {
                label1.Text = "Kriging Done";
                label1.Refresh();
            });
            backgroundWorker1.RunWorkerAsync();
        }

        private void createPARFileToolStripMenuItem_Click(object sender, EventArgs e) {
            openFileDialog1.Title = "Select Factorial Kriging Result";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            string openfile, savefile;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                openfile = openFileDialog1.FileName;
            } else {
                return;
            }
            string[] KrigRaw = File.ReadAllLines(openfile);
            if (KrigRaw.Length != 4) {
                label1.Text = "Invalid File";
                return;
            }
            string[] a = KrigRaw[1].Split(' ');
            float nug = 0;
            if (!float.TryParse(a[5], out nug)) {
                label1.Text = "Invalid nug";
                return;
            }
            string[] b = KrigRaw[2].Split(' ');
            float cc = 0;
            if (!float.TryParse(b[4], out cc)) {
                label1.Text = "Invalid cc";
                return;
            }
            float a_hmax = 0;
            if (!float.TryParse(b[5], out a_hmax)) {
                label1.Text = "Invalid a_hmax";
                return;
            }
            string[] c = KrigRaw[3].Split(' ');
            float cc2 = 0;
            if (!float.TryParse(c[4], out cc2)) {
                label1.Text = "Invalid Sph";
                return;
            }
            return;
        }

    }
}
