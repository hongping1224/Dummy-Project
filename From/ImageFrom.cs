using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathWorks.MATLAB.NET.Arrays;
namespace From {
    public partial class ImageForm : Form {

         

    

        public Bitmap OriImage;
        public Bitmap CurrentImage;
        private MWArray currentImageArray;
        public MWArray CurrentImageArray {
            get {
                if (currentImageArray == null) {
                    currentImageArray = PImage.Bitmap2array(CurrentImage);
                }
                return currentImageArray;
            }
            set {
                currentImageArray = value;
            }
        }
        public Stack<Bitmap> undo;
        public Stack<Bitmap> redo;
        public ToolBar tool;
        public Logs logs;

        #region open
        

        private ImageForm() {
            InitializeComponent();
        }
        public ImageForm(Bitmap image, Point p) : this() {
            Bitmap bi = new Bitmap(image.Width, image.Height,PixelFormat.Format1bppIndexed);
            NativeIP.FastBinaryConvert(image, bi);
            OriImage = bi;
            SetImage(OriImage);
            pictureBox2.Image = OriImage;
            this.Resize += Form2_Resize;
            StartPosition = FormStartPosition.Manual;
            p.Y += 150;
            Location = p;
            pictureBox1.Parent = pictureBox2;
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Location = pictureBox2.Location;
            undo = new Stack<Bitmap>();
            redo = new Stack<Bitmap>();
            tool = new ToolBar(this);
            tool.StartPosition = FormStartPosition.Manual;
           
            logs = new Logs();
            logs.StartPosition = FormStartPosition.Manual;

            
            tool.Show();
            logs.Show();
            Reposition();
        }

        private void Reposition() {
            if (!maximise) {
                if (tool != null) {
                    Point tp = Location;
                    tp.Y -= tool.Height;
                    tool.Location = tp;
                }
                if (logs != null) {
                    Point pp = Location;
                    pp.X += Width;
                    logs.Location = pp;
                }
            }
        } 

        private void Form2_Load(object sender, EventArgs e) {
            SetImage(OriImage);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            Form2_ResizeEnd(sender, e);
            
        }
        #endregion
        #region setimage
        public void SetImage(MWArray image) {
            undo.Push(CurrentImage);
            //clear forward when anything new done
            redo = new Stack<Bitmap>();
            setImage(image);
        }
        public void SetImage(Bitmap image) {
            if (CurrentImage != null) {
                undo.Push(CurrentImage);
            }
            //clear forward when anything new done
            redo = new Stack<Bitmap>();
            setImage(image);
        }

        private void setImage(Bitmap image) {
            setImage(image,null);
        }
        private void setImage(MWArray imagearr) {
            setImage(PImage.Array2bitmap(imagearr, OriImage.Width, OriImage.Height), imagearr);
        }
        private void setImage(Bitmap image, MWArray imagearr) {
            CurrentImageArray = imagearr;
            CurrentImage = image;
            trackBar1_Scroll(null, null);
          //  pictureBox1.Image = CurrentImage;
            pictureBox1.Refresh();
        }
        #endregion

        public void Undo() {
            
            if (undo.Count == 0) {
                return;
            }
            Console.WriteLine(undo.Count);
            redo.Push(CurrentImage);
            setImage(undo.Pop());
            logs.Undo();
        }
        public void Redo() {
            if(redo.Count ==0) {
                return;
            }
            Console.WriteLine(redo.Count);
            undo.Push(CurrentImage);
            setImage(redo.Pop());
            logs.Redo();
        }

   
        private void RefreshPictureBoxSize() {
            int windowsize = Size.Width;
            if (Size.Width > Size.Height) {
                windowsize = Size.Height;
            }
          
            pictureBox2.Size = Size;
            pictureBox2.Location = new Point((Size.Width / 2) - (pictureBox2.Width / 2), (Size.Height / 2) - (pictureBox2.Height / 2));
            pictureBox2.Refresh();

            pictureBox1.Size = Size;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Refresh();
        }

      

        private void Form2_ResizeEnd(object sender, EventArgs e) {
            Image b = pictureBox1.Image;
            if (b != null) {
                float hwscale = (float)OriImage.Height / OriImage.Width;
                int windowsize = Size.Height;
                if (Size.Width > Size.Height) {
                    windowsize = Size.Width;
                }
                Console.WriteLine("triggr");
                if (hwscale <= 1) {
                    //width is larger
                    Size = new Size(windowsize, (int)(windowsize / hwscale));
                } else {
                    //height is larger
                    Size = new Size((int)(windowsize * hwscale), windowsize);
                }
                RefreshPictureBoxSize();
            }
        }
        bool maximise = false;
        //Maximize and minimize handle
        private void Form2_Resize(object sender, EventArgs e) {
            if (!maximise && WindowState == FormWindowState.Maximized) {
                maximise = true;
                int windowsize = Size.Width;
                if (Size.Width > Size.Height) {
                    windowsize = Size.Height;
                }
                Image b = pictureBox1.Image;
                if (b.Width >= b.Height) {
                    pictureBox1.Width = windowsize;
                    pictureBox1.Height = (int)(windowsize * ((float)b.Height / b.Width));
                    pictureBox2.Width = windowsize;
                    pictureBox2.Height = (int)(windowsize * ((float)b.Height / b.Width));
                } else {
                    pictureBox1.Height = windowsize;
                    pictureBox1.Width = (int)(windowsize * ((float)b.Width / b.Height));
                    pictureBox2.Height = windowsize;
                    pictureBox2.Width = (int)(windowsize * ((float)b.Width / b.Height));
                }
                pictureBox1.Location = new Point(0,0);
                pictureBox1.Refresh();
                pictureBox2.Location = new Point((Size.Width / 2) - (pictureBox2.Width / 2), (Size.Height / 2) - (pictureBox2.Height / 2));
                pictureBox2.Refresh();
            } else if (maximise && WindowState == FormWindowState.Normal) {
                maximise = false;
                Image b = pictureBox1.Image;
                if (b != null) {
                    float hwscale = (float)OriImage.Height / OriImage.Width;
                    int windowsize = Size.Height;
                    if (Size.Width > Size.Height) {
                        windowsize = Size.Width;
                    }
                    if (hwscale <= 1) {
                        //width is larger
                        Size = new Size(windowsize, (int)(windowsize / hwscale));
                    } else {
                        //height is larger
                        Size = new Size((int)(windowsize * hwscale), windowsize);
                    }
                    RefreshPictureBoxSize();
                }
            }
            Reposition();
        }

        private void ImageForm_Move(object sender, EventArgs e) {
            Reposition();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {
            pictureBox1.Visible = false;
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) {
            pictureBox1.Visible = true;
            return;
           
        }
        private void trackBar1_Scroll(object sender, EventArgs e) {
            if (CurrentImage == null)
                return;
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = NativeIP.SetAlpha((Bitmap)CurrentImage, trackBar1.Value);
        }
    }
}
