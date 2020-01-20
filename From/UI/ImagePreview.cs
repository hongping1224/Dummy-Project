using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoneCount.UI
{
    public partial class ImagePreview : Form
    {
        public ImagePreview()
        {
            InitializeComponent();
            PictureBox1 = new PanAndZoom();
            PictureBox1.Bounds = new Rectangle(10, 10, 50, 50);
            groupBox1.Controls.Add(PictureBox1);
            PictureBox1.Bounds = this.Bounds;
            Resize += (ee, s) => { RefreshPictureBoxSize(); };
        }
        public PanAndZoom PictureBox1;

        private void ImagePreview_Load(object sender, EventArgs e)
        {
            RefreshPictureBoxSize();
        }
        private void RefreshPictureBoxSize()
        {
            groupBox1.Size = new Size(Size.Width, Size.Height - 30);
            PictureBox1.SetBounds(groupBox1.Location.X, groupBox1.Location.Y, groupBox1.Size.Width-20 , groupBox1.Size.Height - 20);
        }


    }
}
