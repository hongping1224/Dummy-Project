﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using MathWorks.MATLAB.NET.Arrays;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace StoneCount
{
    public class Sieve
    {
        private UI.ProjectForm Project;
        private GroupBox groupBox;
        private FlowLayoutPanel flowPanel;
        private Button Sieve_Btn;
        //private Button Discard_Btn;
        //private Button Process_Btn;
        //private Button Count_Btn;
        //private Button Preview_Btn;
        //private Button Save_Btn;
        //private Button NextSieve_Btn;
        private Label Log_Lbl;
        private Sieve NextSieve;
        public Bitmap image;
        public Bitmap OriImage;
        public string[] Ellipse;
        public int[] size;
        public int[,] coordinates;
        private string titleText;
        BackgroundWorker backgroundWorker1;
        private int threshold=0;

        public Sieve(UI.ProjectForm project)
        {
            this.Project = project;
        }

        public Sieve(GroupBox groupBox, Sieve NextSieve, UI.ProjectForm project)
        {
            this.Project = project;
            this.groupBox = groupBox;
            titleText = groupBox.Text;
            this.NextSieve = NextSieve;
            GenerateButton(groupBox);

        }

        private void GenerateButton(GroupBox groupBox)
        {
            flowPanel = new FlowLayoutPanel();
            flowPanel.Size = new Size(680, 30);
            flowPanel.TabIndex = 0;

            groupBox.Controls.Add(flowPanel);
            flowPanel.Location = new Point(6, 21);
            flowPanel.SuspendLayout();
            Sieve_Btn = new Button();
            Sieve_Btn.Size = new Size(60, 23);
            Sieve_Btn.Text = "Start";
            Sieve_Btn.UseVisualStyleBackColor = true;
            Sieve_Btn.Click += new EventHandler(this.StartSieve);
            flowPanel.Controls.Add(Sieve_Btn);
            /*
            Discard_Btn = new Button();
            Discard_Btn.Size = new Size(60, 23);
            Discard_Btn.Text = "Discard All";
            Discard_Btn.UseVisualStyleBackColor = true;
            Discard_Btn.Click += new EventHandler(this.DiscardPrompt);
            flowPanel.Controls.Add(Discard_Btn);
        
            Preview_Btn = new Button();
            Preview_Btn.Size = new Size(90, 23);
            Preview_Btn.Text = "View Result";
            Preview_Btn.UseVisualStyleBackColor = true;
            Preview_Btn.Click += new EventHandler(this.Preview);
            flowPanel.Controls.Add(Preview_Btn);

            NextSieve_Btn = new Button();
            NextSieve_Btn.Size = new Size(60, 23);
            NextSieve_Btn.Text = "Next Sieve";
            NextSieve_Btn.UseVisualStyleBackColor = true;
            NextSieve_Btn.Click += new EventHandler(this.StartNextSieve);
            flowPanel.Controls.Add(NextSieve_Btn);

            Save_Btn = new Button();
            Save_Btn.Size = new Size(60, 23);
            Save_Btn.Text = "Save";
            Save_Btn.UseVisualStyleBackColor = true;
            Save_Btn.Click += new EventHandler(this.SaveResult);
            flowPanel.Controls.Add(Save_Btn);
            */

            Log_Lbl = new Label();
            Log_Lbl.Size = new Size(300, 30);
            Log_Lbl.TextAlign = ContentAlignment.MiddleLeft;
            Log_Lbl.Text = "Start Sieve Process"; 
            flowPanel.Controls.Add(Log_Lbl);
            flowPanel.ResumeLayout();

        }

        public void InitiateSieve(Bitmap image,int threshold)
        {
            this.image = image;
            OriImage = image;
            groupBox.Visible = true;
            flowPanel.Visible = true;
           /* Discard_Btn.Enabled = false;
            Preview_Btn.Enabled = false;
            Save_Btn.Enabled = false;*/
            this.threshold = threshold;
            //Process_Btn.Enabled = true;

            //NextSieve_Btn.Enabled = false;
            if (NextSieve != null)
            {
                Sieve_Btn.Enabled = true;
            }
            else
            {
                //NextSieve_Btn.Visible = false;
            }
        }
        public void DisableSieve()
        {
            Sieve_Btn.Enabled = false;
            /*Discard_Btn.Enabled = false;
            Preview_Btn.Enabled = false;*/
           // Process_Btn.Enabled = false;
          
            //NextSieve_Btn.Enabled = false;
            groupBox.Visible = false;
        }
        public void EnableSieve()
        {
            Sieve_Btn.Enabled = true;
            //Discard_Btn.Enabled = true;
            //Preview_Btn.Enabled = true;
            //Process_Btn.Enabled = true;
            //NextSieve_Btn.Enabled = false;
            groupBox.Visible = true;
        }

        private void StartNextSieve(object sender, EventArgs e)
        {
            if (HaveNextSieve())
            {
                Sieve_Btn.Enabled = false;
                //Discard_Btn.Enabled = false;
                //NextSieve_Btn.Enabled = false;
                NextSieve.EnableSieve();
                return;
            }
        }
        private bool HaveNextSieve()
        {
            if (NextSieve != null)
            {
                if (NextSieve.OriImage != null)
                {
                    return true;
                }
            }
            return false;
        }
        public void SaveResult(object sender, EventArgs e)
        {
            var svd  = Project.SaveFilePrompt("SaveAs", "All files (*.*)|*.*");
            if(svd.ShowDialog()== DialogResult.OK)
            {
                string filename = svd.FileName;
                filename = filename.Split('.')[0];
                string eCSVExtend = "_e.csv";
                string bTXTExtend = "_b.txt";
                string refTXTExtend = "_ref.txt";
                File.WriteAllLines(filename + eCSVExtend, Ellipse);
                File.WriteAllLines(filename + bTXTExtend, BoundaryTxt());
                var edge = new string[] { OriImage.Width.ToString(), OriImage.Height.ToString() };
                File.WriteAllLines(filename + refTXTExtend, edge);
                Form1.instance.CreateShpFile(filename, filename + eCSVExtend, filename + bTXTExtend, filename + refTXTExtend);
            }
        }
        private string[] BoundaryTxt()
        {
            List<string> ou = new List<string>();
            ou.Add(size.Length.ToString());
            for (int i = 0; i < size.Length; i++)
            {
                ou.Add(size[i].ToString());
            }
            for (int i = 0; i < coordinates.GetLength(0); i++)
            {
                ou.Add(coordinates[i, 0] + "," + (OriImage.Height - coordinates[i, 1]-1));
            }
            return ou.ToArray();
        }
        private void StartSieve(object sender, EventArgs e)
        {
            if (NextSieve == null)
            {  
            }
            Log_Lbl.Text = "Calculating Segments...";
            Log_Lbl.Refresh();
            UI.SieveUI ui = new UI.SieveUI(OriImage,this,UI.ProjectForm.instance.OriginalImage,(int)Math.Floor(((float)threshold)/2f));
            Log_Lbl.Text = "Done";
            Log_Lbl.Refresh();
            ui.Show();
        }
        public void StartSieveCallBack(int threshold, Bitmap larger, Bitmap smaller)
        {
            image = larger;
            if (NextSieve != null)
            {
                NextSieve.InitiateSieve(smaller, threshold);
                NextSieve.DisableSieve();
            }
            groupBox.Text = titleText + "threshold = " + threshold;
            StartProcess(this, EventArgs.Empty);
        }

        private void DiscardPrompt(object sender, EventArgs e)
        {
            if (MessageBox.Show(" Discard ALL changes?",
               "Are You Sure to DISCARD ALL changes in this sieve and sieve below?",
               MessageBoxButtons.YesNo,
                  MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DiscardAll(sender, e);
            }
        }

        private void DiscardAll(object sender, EventArgs e)
        {
            image = OriImage;
            Sieve_Btn.Enabled = true;
            //NextSieve_Btn.Enabled = false;
          /*  Save_Btn.Enabled = false;
            Preview_Btn.Enabled = false;
            Discard_Btn.Enabled = false;*/
            groupBox.Text = titleText;
            size = null;
            coordinates = null;
            Ellipse = null;
            if (NextSieve != null)
            {
                NextSieve.OriImage = null;
                NextSieve.image = null;
                NextSieve.DiscardAll(sender, e);
                NextSieve.DisableSieve();
            }

        }
        private void StartProcess(object sender, EventArgs e)
        {
            Project.OpenImageForm(image, ProcessImageCallback);
        }
        private void ProcessImageCallback(Bitmap m, ImageForm imform)
        {
            image = StartCount(m);
            //Project.OpenPreviewForm(image);
            Sieve_Btn.Enabled = false;
            /*  Discard_Btn.Enabled = true;
              Preview_Btn.Enabled = true;*/
            NextSieve.EnableSieve();
        }
        private Bitmap StartCount(Bitmap image)
        {

            Log_Lbl.Text = "Tracing Boundary";
            Log_Lbl.Refresh();
            MWLogicalArray imagearr = PImage.Bitmap2array(image);
            MWArray[] BoundResult = new MWArray[2];
            BoundResult = PImage.processor.TraceBoundary(2,imagearr, 8, (MWArray)("noholes"));
            var boundMW = BoundResult[0];
            var sizeMW = BoundResult[1];
            double[,] bound = (double[,])boundMW.ToArray();
            double[,] sizeAr = (double[,])sizeMW.ToArray();
            size = new int[sizeAr.GetLength(0)];
            for (int i = 0; i < size.Length; i++)
            {
                size[i] = (int)(sizeAr[i, 0]);
            }
            coordinates = new int[bound.GetLength(0), 2];
            for (int i = 0; i < bound.GetLength(0); i++)
            {
                coordinates[i,0] = (int)(bound[i, 1]-1);
                coordinates[i,1] = (int)(bound[i, 0]-1);
            }
            Bitmap bi = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
            for (int i = 0; i < bound.GetLength(0); i++)
            {
                bi.SetPixel(coordinates[i, 0], coordinates[i, 1], Color.White);
            }
           
            Bitmap aa = NativeIP.FastBinaryConvert(bi);

            Bitmap ab = NativeIP.FastInvertBinary(aa);

            //image = ab;
            Log_Lbl.Text = "Fitting Ellipse";
            Log_Lbl.Refresh();
            MWNumericArray el = (MWNumericArray)PImage.processor.FitEllipse(imagearr, 8, (MWArray)("noholes"));
            double[,] elarr = (double[,])el.ToArray();
            Bitmap clone = new Bitmap(ab.Width, ab.Height, PixelFormat.Format24bppRgb);
            Log_Lbl.Text = "Drawing Ellipse";
            Log_Lbl.Refresh();
            using (Graphics gr = Graphics.FromImage(clone))
            {
                gr.DrawImage(ab, new Rectangle(0, 0, clone.Width, clone.Height));
            }
            List<string> output = new List<string>();
            using (Graphics gr = Graphics.FromImage(clone))
            {
                using (Pen thick_pen = new Pen(Color.Red, 5))
                {
                    for (int i = 0; i < elarr.GetLength(0); i++)
                    {
                        int a = (int)Math.Floor(elarr[i, 1]);
                        int b = (int)Math.Floor(elarr[i, 0]);
                        int dx = (int)Math.Floor(elarr[i, 6]);
                        int dy = (int)Math.Floor(elarr[i, 5]);
                        double theta = elarr[i, 2] * 180 / Math.PI;
                        gr.SmoothingMode = SmoothingMode.AntiAlias;
                        Rectangle rect = new Rectangle(-a, -b, a * 2, b * 2);
                        gr.TranslateTransform(dx, dy);
                        gr.RotateTransform((float)theta);
                        gr.DrawEllipse(thick_pen, rect);
                        gr.RotateTransform((float)-theta);
                        gr.TranslateTransform(-dx, -dy);
                        output.Add(String.Format("{0},{1},{2},{3},{4}", a, b, dx, OriImage.Height-dy-1, theta));
                    }
                }
            }
            Ellipse = output.ToArray();
            Log_Lbl.Text = "Done";
            Log_Lbl.Refresh();
           // NextSieve_Btn.Enabled = true;
           // Save_Btn.Enabled = true;
            return clone;
        }

   

        private void Preview(object sender, EventArgs e)
        {
            Project.OpenPreviewForm(image);
        }
    }
}