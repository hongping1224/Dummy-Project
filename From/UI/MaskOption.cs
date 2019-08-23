using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MathWorks.MATLAB.NET.Arrays;

namespace StoneCount
{
    public partial class MaskOption : Form
    {
        static string[] operatortype = new string[] { "larger", "smaller" };
        static string[] optiontype = new string[] { "keep", "remove" };
        ImageForm image;
        public MaskOption(ImageForm imageform)
        {
            InitializeComponent();
            for (int i = 0; i < operatortype.Length; i++)
            {
                operatorCombo.Items.Add(operatortype[i]);
            }
            for (int i = 0; i < optiontype.Length; i++)
            {
                optionCombo.Items.Add(optiontype[i]);
            }
            optionCombo.SelectedIndex = 0;
            operatorCombo.SelectedIndex = 0;
            button1.Focus();
            image = imageform;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static MWLogicalArray Pick(MWArray image, int threshold, string ope, string option) {
            bool[,] img = (bool[,])((MWLogicalArray)image).ToArray();
            int w = img.GetLength(0);
            int h = img.GetLength(1);
            bool[,] flag = new bool[w, h];

            Queue<position> queue = new Queue<position>();
            List<Queue<position>> segments = new List<Queue<position>>();
            //true mean white
            for (int i = 0; i < w; i++) {
                for (int j = 0; j < h; j++) {
                    if (flag[i, j] == false && img[i, j] == true) {
                        queue.Enqueue(new position(i, j));
                        flag[i, j] = true;
                    }
                    int[,] dir = new int[4, 2] { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
                    segments.Add(new Queue<position>());
                    int segmentindex = segments.Count - 1;
                    while (queue.Count != 0) {
                        position p = queue.Dequeue();
                        segments[segmentindex].Enqueue(p);
                        for (int k = 0; k < dir.GetLength(0); k++) {
                            position np = new position(p.x + dir[k, 0], p.y + dir[k, 1]);
                            if (np.x < 0 || np.y < 0 || np.x >= w || np.y >= h) {
                                continue;
                            }
                            if (flag[np.x, np.y] == true) {
                                continue;
                            }
                            flag[np.x, np.y] = true;
                            if (img[np.x, np.y] == true) {
                                queue.Enqueue(np);
                            }
                        }
                    }
                }
            }
            if ((ope == "larger" && option == "keep") || (ope == "smaller" && option == "remove")) {
                for (int i = 0; i < segments.Count; i++) {
                    if (segments[i].Count < threshold) {
                        while (segments[i].Count > 0) {
                            position p = segments[i].Dequeue();
                            img[p.x, p.y] = false;
                        }
                    }
                }
            } else {
                for (int i = 0; i < segments.Count; i++) {
                    if (segments[i].Count > threshold) {
                        while (segments[i].Count > 0) {
                            position p = segments[i].Dequeue();
                            img[p.x, p.y] = false;
                        }
                    }
                }
            }
            return new MWLogicalArray(img);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int threshold = int.Parse(thresholdNum.Value.ToString());
            string ope = operatortype[operatorCombo.SelectedIndex];
            string option = optiontype[optionCombo.SelectedIndex];

            MWLogicalArray newim = Pick(image.CurrentImageArray, threshold, ope, option);
            image.SetImage(newim);
            image.logs.AddLog(new Step(Step.Pick, parameters: new string[] { "threshold:" + threshold, "operator:" + ope,"option:"+option }));
            this.Close();
        }
        struct position
        {
            public position(int x,int y){
                this.x = x;
                this.y = y;
            }
            public int x, y;
        }

        public static MWLogicalArray PickByBoundary(MWArray image,string openfile) {


            bool[,] img = (bool[,])((MWLogicalArray)image).ToArray();
            int w = img.GetLength(0);
            int h = img.GetLength(1);
            bool[,] flag = new bool[w, h];
            //for each pixel do region growing
            string[] boundary = File.ReadAllLines(openfile);
            Queue<position> queue = new Queue<position>();
            int[,] dir = new int[4, 2] { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
            foreach (string b in boundary) {
                string[] spl = b.Split(',');
                position pos = new position(int.Parse(spl[0]), int.Parse(spl[1]));
                if (flag[pos.x, pos.y] == false) {
                    flag[pos.x, pos.y] = true;
                    queue.Enqueue(pos);
                }
                while (queue.Count != 0) {
                    position p = queue.Dequeue();
                    img[p.x, p.y] = false;
                    for (int k = 0; k < dir.GetLength(0); k++) {
                        position np = new position(p.x + dir[k, 0], p.y + dir[k, 1]);
                        if (np.x < 0 || np.y < 0 || np.x >= w || np.y >= h) {
                            continue;
                        }
                        if (flag[np.x, np.y] == true) {
                            continue;
                        }
                        flag[np.x, np.y] = true;
                        if (img[np.x, np.y] == true) {
                            queue.Enqueue(np);
                        }
                    }
                }
            }
            return new MWLogicalArray(img);
        }

        private void button3_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog1 =  image.mainForm.GetOpenFileDialog();
            openFileDialog1.Title = "Select Boundary File";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            string openfile;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                openfile = openFileDialog1.FileName;
            } else {
                return;
            }

            MWLogicalArray newim = PickByBoundary(image.CurrentImageArray,openfile);
            image.SetImage(newim);
            image.logs.AddLog(new Step(Step.PickByBoundary, parameters: new string[] { "path:"+ openfile }));
            this.Close();
        }
    }
}
