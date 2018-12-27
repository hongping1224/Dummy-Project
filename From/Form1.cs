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

namespace From {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }


        ImageForm form2;

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            openFileDialog1.Title = "Pick an image file";
            openFileDialog1.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                var filePath = openFileDialog1.FileName;
                Bitmap b = new Bitmap(filePath);
                Console.WriteLine("Start Form2");
                if(form2 != null) {
                    form2.Close();
                }
                form2 = new ImageForm(b,this.Location);
                form2.Show();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            saveFileDialog1.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                var filePath = saveFileDialog1.FileName;
                form2.CurrentImage.Save(filePath);
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
    }
}
