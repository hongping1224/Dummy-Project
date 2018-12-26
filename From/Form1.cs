using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace From {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }


        ImageForm form2;

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            openFileDialog1.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                var filePath = openFileDialog1.FileName;
                Bitmap b = new Bitmap(filePath);
                Console.WriteLine("Start Form2");
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

        private void fileToolStripMenuItem_Click(object sender, EventArgs e) {

        }
    }
}
