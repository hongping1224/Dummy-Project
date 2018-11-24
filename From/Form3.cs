using System;

using System.Drawing;
using System.Drawing.Imaging;

using MathWorks.MATLAB.NET.Arrays;
using System.Windows.Forms;

namespace From {
    public partial class Form3 : Form {
        public Form3() {
            InitializeComponent();
        }
        public Form2 imageform;
        public Form3(Form2 image) : this() {
            imageform = image;
        }
        private void Inverse_Click(object sender, EventArgs e) {
            Bitmap bi = new Bitmap(imageform.CurrentImage.Width, imageform.CurrentImage.Height, PixelFormat.Format1bppIndexed);
            NativeIP.FastInvertBinary(imageform.CurrentImage, bi);
            imageform.SetImage(bi);
        }

        private void Fill_Click(object sender, EventArgs e) {
            imageform.SetImage(PImage.processor.Fill(imageform.CurrentImageArray,(MWNumericArray)(new int[] {1,1})));
        }

        private void Opening_Click(object sender, EventArgs e) {
            FilterPicker f = new FilterPicker((filter, size) => {
                imageform.SetImage(PImage.processor.Opening(imageform.CurrentImageArray,filter,size));
            });
            f.Show();
        }
        private void Closing_Click(object sender, EventArgs e) {
            FilterPicker f = new FilterPicker((filter, size) => {
                imageform.SetImage(PImage.processor.Closing(imageform.CurrentImageArray, filter, size));
            });
            f.Show();
        }

        private void Trace_Btn_Click(object sender, EventArgs e) {

        }

        private void Undo_Click(object sender, EventArgs e) {
            imageform.Undo();
        }

        private void Redo_Click(object sender, EventArgs e) {
            imageform.Redo();
        }

        private void Form3_Load(object sender, EventArgs e) {
            TopMost = true;
        }
    }
}
