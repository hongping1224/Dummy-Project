﻿using System;
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
        public Bitmap Overlay;
        public Bitmap CurrentImage;
        private MWArray currentImageArray;
        private bool preview;
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
        public Form1 mainForm;
        public Stack<Bitmap> undo;
        public Stack<Bitmap> redo;
        public ToolBar tool;
        public Logs logs;
        #region open
        PanAndZoom PictureBox1;

        private ImageForm() {
            InitializeComponent();
        }
        
        public ImageForm(Bitmap image, Point p, bool Preview, Bitmap overlay = null) : this(image, p, Preview, ToolBar.Mode.All,overlay)
        {
        }

        public ImageForm(Bitmap image, Point p, Action<Bitmap, ImageForm> OnDone,ToolBar.Mode mode, Bitmap overlay = null) : this(image, p, false,mode,overlay)
        {
            tool.OnDoneClick += OnDone;

        }
        public ImageForm(Bitmap image, Point p, bool Preview,ToolBar.Mode mode,Bitmap overlay = null,bool convertbinary = true,bool hideTrack = false) : this()
        {
          
            preview = Preview;
            PictureBox1 = new PanAndZoom();
            PictureBox1.Bounds = new Rectangle(10, 10, 50, 50);
            PictureBox1.MouseDown += PictureBox1_MouseDown;
            PictureBox1.MouseUp += PictureBox1_MouseUp;
            this.Controls.Add(PictureBox1);
            Bitmap bi = image;
            if (convertbinary)
            {
                bi = NativeIP.FastBinaryConvert(image); ;
            }
            OriImage = bi;
            if (overlay == null)
            {
                overlay = bi;
            }
            Overlay = overlay;
            PictureBox1.Image = OriImage;

            if (OriImage.Width < 700 && OriImage.Height < 700)
            {
                PictureBox1.SetZoomScale(1, new Point(0, 0));
                Size = new Size(OriImage.Width, OriImage.Height);
            }
            else
            {
                PictureBox1.SetZoomScale(0.25, new Point(0, 0));
                Size = new Size(750, 750);
            }
            this.Resize += Form2_Resize;
            this.FormClosed += Form2_Closing;
            StartPosition = FormStartPosition.Manual;
            p.Y += 150;
            Location = p;
            undo = new Stack<Bitmap>();
            redo = new Stack<Bitmap>();
            if (!Preview)
            {
                OpenToolBar(mode);
                OpenLogs();
            }
            if (hideTrack)
            {
                trackBar1.Enabled = false;
                trackBar1.Visible = false;
            }
            Reposition();
        }
      
        private void PictureBox1_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                trackBar1_Scroll(null, null);
            }
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu cm = new ContextMenu();
                cm.MenuItems.Add("Save", new EventHandler(SaveImage));
                cm.MenuItems.Add("Change Overlay", new EventHandler(ChangeOverlay));
                this.ContextMenu = cm;
            }
        }
        private void ChangeOverlay(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Overlay = new Bitmap(openFileDialog.FileName);
            }
        }
        private void SaveImage(object sender, EventArgs e)
        {
            var saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var filePath = saveFileDialog1.FileName;

                Bitmap clone = new Bitmap(PictureBox1.Image.Width, PictureBox1.Image.Height, PixelFormat.Format24bppRgb);

                using (Graphics gr = Graphics.FromImage(clone))
                {
                    gr.DrawImage(PictureBox1.Image, new Rectangle(0, 0, clone.Width, clone.Height));
                }

                clone.Save(filePath, ImageFormat.Bmp);
            }
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                PictureBox1.Image = Overlay;
            }

        }

        public void OpenToolBar(ToolBar.Mode mode) {
            tool = new ToolBar(this,mode);
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

        private void Reposition()
        {
            if (!maximise)
            {
                if (tool != null)
                {
                    Point tp = Location;
                    tp.Y -= tool.Height;
                    tool.Location = tp;
                }
                if (logs != null)
                {
                    Point pp = Location;
                    pp.X += Width;
                    logs.Location = pp;
                }
            }
            else
            {
                if (tool != null)
                {
                    Point tp = Location;
                    tp.Y += 100;
                    tool.Location = tp;
                }
                if (logs != null)
                {
                    Point pp = Location;
                    if (tool != null)
                    {
                        pp.Y += 100 + tool.Height;
                    }
                    else
                    {
                        pp.Y += 100;
                    }
                    logs.Location = pp;

                }

            }
        }
        private void Form2_Closing(object sender, FormClosedEventArgs e)
        {
            if (tool != null)
                tool.Close();
            if (logs != null)
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
            if (!maximise && WindowState == FormWindowState.Maximized)
            {
                maximise = true;
                if (tool != null)
                    tool.TopMost = true;
            }
            else if (maximise && WindowState == FormWindowState.Normal)
            {
                maximise = false;
                if (tool != null)
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
            PictureBox1.Image = NativeIP.Combine(Overlay,CurrentImage, trackBar1.Value,rd);
        }
    }
}
