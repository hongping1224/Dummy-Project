using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoneCount {
    public partial class ComaprisonWindow : Form {
        public ComaprisonWindow() {
            InitializeComponent();
        }

        ImageForm form;
        Image ori;
        Bitmap Current;
        string rd;
        public ComaprisonWindow(ImageForm form ,Image img) : this() {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            rd = GuidString;
            this.form = form;
            Size = new Size(600, 600);
            ori = img;
            pictureBox2.Image = ori;
            pictureBox1.Image = ori;
            UpdateImage(form.CurrentImage);
            form.OnSetImage += UpdateImage;
            this.Resize += Form2_Resize;
            this.FormClosing += Form2_Closing;
        
            pictureBox1.Parent = pictureBox2;
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Location = pictureBox2.Location;
            RefreshPictureBoxSize();
        }
        private void UpdateImage(Bitmap B) {
            Console.WriteLine("Update");
            Current = B.Clone(new Rectangle(0, 0, B.Width, B.Height), B.PixelFormat);
            trackBar1_Scroll_1(null, null);
        }
        private void Form2_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            form.OnSetImage -= UpdateImage;
        }
        bool maximise = false;
        private void Form2_Resize(object sender, EventArgs e) {
            if (!maximise && WindowState == FormWindowState.Maximized) {
                maximise = true;
            } else if (maximise && WindowState == FormWindowState.Normal) {
                maximise = false;
            }
            RefreshPictureBoxSize();
        }

        private void RefreshPictureBoxSize() {
            if(ori == null) {
                return;
            }
            float hwratio = (float)(ori.Height) / ori.Width;
            float winratio = (float)(Size.Height) / Size.Width;
            if (winratio > hwratio) {
                pictureBox2.Size = new Size(Size.Width, (int)((Size.Width) / hwratio));
                pictureBox2.Location = new Point(0, (int)((Size.Height / 2f) - (pictureBox2.Height / 2f)) + 7);
                pictureBox2.Size = new Size(Size.Width - 15, (int)((Size.Width - 15) / hwratio));
                pictureBox2.Refresh();
            } else {
                pictureBox2.Size = new Size((int)((Size.Height) * hwratio), Size.Height);
                pictureBox2.Location = new Point((int)((Size.Width / 2f) - (pictureBox2.Width / 2f)) + SystemInformation.ToolWindowCaptionHeight, 0);
                pictureBox2.Size = new Size((int)((Size.Height - SystemInformation.ToolWindowCaptionHeight * 2) * hwratio), Size.Height - SystemInformation.ToolWindowCaptionHeight * 2);
                pictureBox2.Refresh();
            }


            pictureBox1.Size = pictureBox2.Size;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Refresh();
        }
      
        private void pictureBox2_Click(object sender, EventArgs e) {

        }

        private void ComaprisonWindow_Load(object sender, EventArgs e) {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void trackBar1_Scroll_1(object sender, EventArgs e) {
            if (pictureBox1.Image == null)
                return;
            Console.WriteLine(trackBar1.Value);
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = NativeIP.SetAlpha(Current, Current.Width, Current.Height, rd, trackBar1.Value);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {
            pictureBox1.Visible = false;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) {
            pictureBox1.Visible = true;
        }
    }
}
