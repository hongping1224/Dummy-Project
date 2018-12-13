using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using MathWorks.MATLAB.NET.Arrays;
namespace From {
    public partial class TraceBoundaryOption : Form {
        static string[] Conntype = new string[] { "4", "8" };
        static string[] OptionType = new string[] { "holes", "noholes" };
        public TraceBoundaryOption() {
            InitializeComponent();
            for (int i = 0; i < Conntype.Length; i++) {
                comboBox1.Items.Add(Conntype[i]);
            }
            for (int i = 0; i < OptionType.Length; i++) {
                comboBox2.Items.Add(OptionType[i]);
            }
          
            comboBox2.SelectedIndex = 0;
            comboBox1.SelectedIndex = 1;
            button1.Focus();
        }
   
        Form2 image;
        public TraceBoundaryOption(Form2 imageform) : this() {
            this.Text = "Trace Boundary";
            image = imageform;
        }

        private void button2_Click(object sender, EventArgs e) {

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) {
            int conn = 4;
            if (comboBox1.SelectedIndex == 1) {
                conn = 8;
            }
            string option = OptionType[comboBox2.SelectedIndex];
           var a = PImage.processor.TraceBoundary(image.CurrentImageArray, (MWNumericArray)conn, (MWArray)(option));
            double[,] bound = (double[,])a.ToArray();
            Bitmap bi = new Bitmap(image.CurrentImage.Width, image.CurrentImage.Height, PixelFormat.Format24bppRgb);

            for (int i = 0; i< bound.GetLength(0); i++) {
                bi.SetPixel((int)bound[i, 1]-1, (int)bound[i, 0]-1, Color.White);
            }
            Bitmap aa = new Bitmap(image.CurrentImage.Width, image.CurrentImage.Height, PixelFormat.Format1bppIndexed);
            NativeIP.FastBinaryConvert(bi, aa);
            Bitmap ab = new Bitmap(image.CurrentImage.Width, image.CurrentImage.Height, PixelFormat.Format1bppIndexed);
            NativeIP.FastInvertBinary(aa, ab);
            image.SetImage(ab);
            image.logs.AddLog(new Step("TraceBoundary", parameters: new string[] { "connection:"+ conn.ToString(), "option:" + option }));
            //image.SetImage(PImage.processor.ToBinary(a,0));
            this.Close();
        }
    }
}
