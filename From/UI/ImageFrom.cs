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
namespace StoneCount {
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
        PanAndZoom PictureBox1;

        private ImageForm() {
            InitializeComponent();
        }

        public ImageForm(Bitmap image, Point p) : this() {
            PictureBox1 = new PanAndZoom();
            PictureBox1.Bounds = new Rectangle(10, 10, 50, 50);
            PictureBox1.MouseDown += PictureBox1_MouseDown;
            PictureBox1.MouseUp += PictureBox1_MouseUp;
            this.Controls.Add(PictureBox1);
            Bitmap bi = new Bitmap(image.Width, image.Height,PixelFormat.Format1bppIndexed);
            NativeIP.FastBinaryConvert(image, bi);
            OriImage = bi;
            PictureBox1.Image = OriImage;
            
            if (OriImage.Width < 600 && OriImage.Height < 600) {
                Size = new Size(OriImage.Width, OriImage.Height);
            } else {
                Size = new Size(600, 600);
            }
            this.Resize += Form2_Resize;
            this.FormClosing += Form2_Closing;
            StartPosition = FormStartPosition.Manual;
            p.Y += 150;
            Location = p;
            undo = new Stack<Bitmap>();
            redo = new Stack<Bitmap>();
            OpenToolBar();
            OpenLogs();
            Reposition();
        }
      
        private void PictureBox1_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Middle) {
                trackBar1_Scroll(null, null);
            }
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Middle) {
                PictureBox1.Image = OriImage;
            }
        }

        public void OpenToolBar() {
            tool = new ToolBar(this);
            tool.StartPosition = FormStartPosition.Manual;
            tool.Show();
            Point tp = Location;
            tp.Y -= tool.Height;
            tool.Location = tp;
        }

        public void OpenLogs() {
            logs = new Logs();
            logs.StartPosition = FormStartPosition.Manual;
            logs.image = this;
            logs.Show();
            Point pp = Location;
            pp.X += Width;
            logs.Location = pp;
        }

        private void Reposition() {
            if (!maximise) {
                if (Application.OpenForms.OfType<ToolBar>().Count() != 0) {
                    Point tp = Location;
                    tp.Y -= tool.Height;
                    tool.Location = tp;
                }
                if (Application.OpenForms.OfType<Logs>().Count() != 0) {
                    Point pp = Location;
                    pp.X += Width;
                    logs.Location = pp;
                }
            } else {
                if (Application.OpenForms.OfType<ToolBar>().Count() != 0) {
                    Point tp = Location;
                    tp.Y += 100;
                    tool.Location = tp;
                }
                if (Application.OpenForms.OfType<Logs>().Count() != 0) {
                        Point pp = Location;
                    if (Application.OpenForms.OfType<ToolBar>().Count() != 0) {
                        pp.Y += 100 + tool.Height;
                    } else {
                        pp.Y += 100;
                    }
                    logs.Location = pp;

                }
            }
        }
        private void Form2_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            tool.Close();
            logs.Close();
        }
        private void Form2_Load(object sender, EventArgs e) {
            SetImage(OriImage);
            RefreshPictureBoxSize();
           // Form2_ResizeEnd(sender, e);
            
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
        public event Action<Bitmap> OnSetImage;

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
            PictureBox1.Refresh();
            if (OnSetImage != null) {
                OnSetImage(CurrentImage);
            }
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
            PictureBox1.Size = new Size(Size.Width -20 , Size.Height -50);
        }

      

        bool maximise = false;
        //Maximize and minimize handle
        private void Form2_Resize(object sender, EventArgs e) {
            if (!maximise && WindowState == FormWindowState.Maximized) {
                maximise = true;
                tool.TopMost = true;
            } else if (maximise && WindowState == FormWindowState.Normal) {
                maximise = false;
                tool.TopMost = false;

            }
            RefreshPictureBoxSize();
            Reposition();
        }

        private void ImageForm_Move(object sender, EventArgs e) {
            Reposition();
        }

  
        string rd = "Image";
        private void trackBar1_Scroll(object sender, EventArgs e) {
            if (CurrentImage == null)
                return;
            PictureBox1.Image = NativeIP.Combine(OriImage,CurrentImage, trackBar1.Value,rd);
        }
    }
}
