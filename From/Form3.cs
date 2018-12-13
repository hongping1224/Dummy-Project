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
        public Form optionform = null;
     
        public Form3(Form2 image) : this() {
            imageform = image;
     
        }

        private void Form3_Load(object sender, EventArgs e) {
            TopMost = true;
        }

        private void Inverse_Click(object sender, EventArgs e) {
            Bitmap bi = new Bitmap(imageform.CurrentImage.Width, imageform.CurrentImage.Height, PixelFormat.Format1bppIndexed);
            NativeIP.FastInvertBinary(imageform.CurrentImage, bi);
            imageform.SetImage(bi);
            imageform.logs.AddLog(new Step("Inverse",new string[0]));
        }

        private void Fill_Click(object sender, EventArgs e) {
            if (optionform != null) {
                return;
            }
            FillOption f = new FillOption(imageform);
            optionform = f;
            f.FormClosed += OptionFormClosed;
            f.Show();

        }

        private void Opening_Click(object sender, EventArgs e) {
            if (optionform != null) {
                return;
            }
            FilterPicker f = new FilterPicker("Opening",(filter, size) => {
                imageform.SetImage(PImage.processor.Opening(imageform.CurrentImageArray,filter,size));
                imageform.logs.AddLog(new Step("Opening", new string[] {"filter:"+filter, "size:"+size.ToString() }));
            });
            optionform = f;
            f.FormClosed += OptionFormClosed;
            f.Show();
        }
        private void Closing_Click(object sender, EventArgs e) {
            if (optionform != null) {
                return;
            }
            FilterPicker f = new FilterPicker("Closing", (filter, size) => {
                imageform.SetImage(PImage.processor.Closing(imageform.CurrentImageArray, filter, size));
                imageform.logs.AddLog(new Step("Closing", new string[] { "filter:" + filter, "size:" + size.ToString() }));
            });
            optionform = f;
            f.FormClosed += OptionFormClosed;
            f.Show();
        }

        private void Trace_Btn_Click(object sender, EventArgs e) {
            if (optionform != null) {
                return;
            }
            TraceBoundaryOption f = new TraceBoundaryOption(imageform);
            optionform = f;
            f.FormClosed += OptionFormClosed;
            f.Show();
        }

     


        private void erosion_Click(object sender, EventArgs e) {
            if (optionform != null) {
                return;
            }
            FilterPicker f = new FilterPicker("Erosion",(filter, size) => {
                imageform.SetImage(PImage.processor.Erosion(imageform.CurrentImageArray, filter, size));
                imageform.logs.AddLog(new Step("Erosion", new string[] { "filter:" + filter, "size:" + size.ToString() }));
            });
            optionform = f;
            f.FormClosed += OptionFormClosed;
            f.Show();
        }

        private void dilation_Click(object sender, EventArgs e) {
            if(optionform != null) {
                return;
            }

            FilterPicker f = new FilterPicker("Dilation",(filter, size) => {
                imageform.SetImage(PImage.processor.Dialation(imageform.CurrentImageArray, filter, size));
                imageform.logs.AddLog(new Step("Dilation", new string[] { "filter:" + filter, "size:" + size.ToString() }));
            });
            optionform = f;
            f.FormClosed += OptionFormClosed;
            f.Show();

        }

        private void OptionFormClosed(object sender, FormClosedEventArgs e) {
            optionform = null;
        }
        private void Undo_Click(object sender, EventArgs e) {
            imageform.Undo();
        }

        private void Redo_Click(object sender, EventArgs e) {
            imageform.Redo();
        }
    }
}
