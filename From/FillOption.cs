using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathWorks.MATLAB.NET.Arrays;

namespace From {
    public partial class FillOption : Form {
       
        public FillOption() {
            InitializeComponent();
            radioButton1.Checked = true;
        }
        bool hole = true;

        Form2 image;
        public FillOption(Form2 from) : this() {
            this.Text = "Fill";
            image = from;
        }

        private void textBox2_TextChanged(object sender, EventArgs e) {

        }

        private void textBox1_TextChanged(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {
            if (hole) {
                image.SetImage(PImage.processor.Fill(image.CurrentImageArray, (MWArray)("holes")));
                this.Close();
            } else {
                int x = 1;
                int y = 1;
                try {
                    x = (int)uint.Parse(textBox1.Text);
                    y = (int)uint.Parse(textBox2.Text);
                   
                } catch {
                    MessageBox.Show("Please enter valid x and y coordinates");
                    return;
                }
                if(x> image.CurrentImage.Width || y> image.CurrentImage.Height) {
                    MessageBox.Show("coordinates out of range");
                    return;
                }
                image.SetImage(PImage.processor.Fill(image.CurrentImageArray, (MWNumericArray)new int[] { x, y }));
                this.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            hole = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            hole = false;
        }
    }
}
