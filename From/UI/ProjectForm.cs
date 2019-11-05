using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace StoneCount.UI
{
    public partial class ProjectForm : Form
    {

        #region Constructor
        public static ProjectForm instance;
        public ProjectForm()
        {
            InitializeComponent();
            instance = this;
            initialAll_Button();
            sieves = new Sieve[5];
            sieves[4] = new Sieve(FifthSieve_box, null, this);
            sieves[3] = new Sieve(ForthSieve_box, sieves[4], this);
            sieves[2] = new Sieve(ThirdSieve_box, sieves[3], this);
            sieves[1] = new Sieve(SecondSieve_box, sieves[2], this);
            sieves[0] = new Sieve(FirstSieve_box, sieves[1], this);
        }


        public ProjectForm(string FilePath)
        {
            OriginalImageFilePath = FilePath;
            OriginalImage = new Bitmap(FilePath);
        }
        private void initialAll_Button()
        {
            Preview_Ori_Btn.Enabled = false;
            Done_Ori_Btn.Enabled = false;
            StartPreprocess_Btn.Enabled = false;
            PreProcessedPreview_Btn.Enabled = false;
            DonePreprocess_Btn.Enabled = false;
            RevertToOriginal_Btn.Enabled = false;
        }

        #endregion

        #region Parameters
        private string OriginalImageFilePath;
        public Bitmap OriginalImage;
        private const string PreprocessedImageFilePath = "tmp/preprocess.bmp";
        private Bitmap PreprocessedImage;
        public ImageForm ProcessingImageForm;
        private Sieve[] sieves;
        public ToolBar.Mode currentMode;
        #endregion

        #region Original Function

        private void SelectImage_Btn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Pick an image file";
            openFileDialog1.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SetOriginalImage(openFileDialog1.FileName);
                OriginalImage_Lbl.Text = "Processing :" + Path.GetFileName(openFileDialog1.FileName);
            }
        }

        public SaveFileDialog SaveFilePrompt(string Title, string Filter)
        {
            saveFileDialog1.Title = Title;
            saveFileDialog1.Filter = Filter;
            return saveFileDialog1;
        }

        private void SetOriginalImage(string Path)
        {
            OriginalImageFilePath = Path;
            OriginalImage = new Bitmap(Path);
            Preview_Ori_Btn.Enabled = true;
            Done_Ori_Btn.Enabled = true;
        }

        private void Preview_Ori_Btn_Click(object sender, EventArgs e)
        {
            if (OriginalImage == null)
            {
                return;
            }
            OpenPreviewForm(OriginalImage);
        }

        private void Done_Ori_Btn_Click(object sender, EventArgs e)
        {
            Preprocessing_Box.Visible = true;
            PreprocessedImage = OriginalImage;
            Select_Ori_Btn.Enabled = false;
            Done_Ori_Btn.Enabled = false;
            PreProcessedPreview_Btn.Enabled = true;
            StartPreprocess_Btn.Enabled = true;
            DonePreprocess_Btn.Enabled = true;
            RevertToOriginal_Btn.Enabled = true;
            Preprocess_lbl.Text = "Start Preprocess Image";
        }


        #endregion

        #region Preprocessing
        private void StartPreprocessing_Button(object sender, EventArgs e)
        {
            if (ProcessingImageForm != null)
            {
                ShowImageFormOpenAlertPopUpWindow();
                return;
            }
            Preprocess_lbl.Text = "Preprocessing Image";
            Preprocess_lbl.Refresh();
            OpenImageForm(OriginalImage, ReturnPreProcessedImage, ToolBar.Mode.Preprocessing, () =>
            {
                StartPreprocess_Btn.Enabled = true;
                PreProcessedPreview_Btn.Enabled = true;
                ProcessingImageForm = null;
            });
            StartPreprocess_Btn.Enabled = false;
            PreProcessedPreview_Btn.Enabled = false;

        }
        private void FormCloseAlert(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Discard Change?",
                  "Discard all change in this session?",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        public void ReturnPreProcessedImage(Bitmap m, ImageForm imform)
        {
            PreprocessedImage = m;
            if (!Directory.Exists("tmp"))
            {
                Directory.CreateDirectory("tmp");
            }
            PreprocessedImage.Save(PreprocessedImageFilePath, System.Drawing.Imaging.ImageFormat.Bmp);
            Preprocess_lbl.Text = "Click Done to start Counting";
            Preprocess_lbl.Refresh();
        }

        private void PreProcessedPreview_Btn_Click(object sender, EventArgs e)
        {
            OpenPreviewForm(PreprocessedImage);
        }

        private void DonePreprocess_Btn_Click(object sender, EventArgs e)
        {
            Preprocess_lbl.Text = "Done";
            Preprocess_lbl.Refresh();
            SieveMaster_box.Visible = true;
            DonePreprocess_Btn.Enabled = false;
            RevertToOriginal_Btn.Enabled = false;
            StartPreprocess_Btn.Enabled = false;
            flowbox.Visible = true;
            FirstSieve_box.Visible = true;
            sieves[0].InitiateSieve(PreprocessedImage);
            flowbox.Refresh();
        }

        private void RevertToOriginal_Btn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Discard all changes?", "Are you sure to discard all change?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                PreprocessedImage = OriginalImage;
            }
        }
        #endregion

        private void ShowImageFormOpenAlertPopUpWindow()
        {
            //TODO Show PopUP
            MessageBox.Show("Process window has Opened", "Process window had already been opened.", MessageBoxButtons.OK);
        }

        private void SieveMaster_box_Enter(object sender, EventArgs e)
        {

        }

        public void OpenImageForm(Bitmap image, Action<Bitmap, ImageForm> OnDone, ToolBar.Mode mode, Action OnCloseCallBack = null)
        {
            if (ProcessingImageForm != null)
            {
                ShowImageFormOpenAlertPopUpWindow();
                return;
            }
            ImageForm form = new ImageForm(image, this.Location, (s, ev) =>
            {
                ProcessingImageForm.FormClosing -= FormCloseAlert;
                if (OnDone != null)
                {
                    OnDone(s, ev);
                }
            }, mode,OriginalImage);
            ProcessingImageForm = form;
            currentMode = mode;
            ProcessingImageForm.FormClosing += FormCloseAlert;
            ProcessingImageForm.FormClosed += (s, ev) =>
            {
                ProcessingImageForm = null;
                if (OnCloseCallBack != null)
                {
                    OnCloseCallBack();
                }

            };
            ProcessingImageForm.Show();

        }


        public void OpenPreviewForm(Bitmap image, string title = "Preview Image")
        {
            ImageForm form = new ImageForm(image, this.Location, true,OriginalImage);
            form.Text = title;
            form.Show();
        }

        public void CombineAllSieveResult()
        {
            int width = sieves[0].image.Width;
            int height = sieves[0].image.Height;
            Bitmap baseImage = NativeIP.FastBinaryConvert(sieves[0].image);


            for (int i = 1; i < sieves.Length; i++)
            {
                if (sieves[i].image == null || sieves[i].OriImage == null)
                {
                    break;
                }
                Bitmap NextImage = NativeIP.FastBinaryConvert(sieves[i].image);

                baseImage = NativeIP.FastCombineBinary(NextImage, baseImage);
            }
            OpenPreviewForm(baseImage);
        }

        private void finishProject_btn_Click(object sender, EventArgs e)
        {
            List<string> Ellipse = new List<string>();
            List<int> size = new List<int>();
            int coCount = 0;
            for (int i = 0; i < sieves.Length; i++)
            {
                if (sieves[i] == null)
                {
                    break;
                }
                if (sieves[i].Ellipse == null)
                {
                    break;
                }
                for (int j = 0; j < sieves[i].Ellipse.Length; j++)
                {
                    Ellipse.Add(sieves[i].Ellipse[j]);
                    size.Add(sieves[i].size[j]);
                }
                coCount += sieves[i].coordinates.GetLength(0);
            }
            int[,] coor = new int[coCount, 2];
            int index = 0;
            foreach (Sieve s in sieves)
            {
                if (s == null)
                {
                    break;
                }
                if (s.Ellipse == null)
                {
                    break;
                }
                for (int i = 0; i < s.coordinates.GetLength(0); i++)
                {
                    coor[index, 0] = s.coordinates[i, 0];
                    coor[index, 1] = s.coordinates[i, 1];
                    index++;
                }
            }
            Sieve combine = new Sieve(this);
            combine.OriImage = OriginalImage;
            combine.Ellipse = Ellipse.ToArray();
            combine.size = size.ToArray();
            combine.coordinates = coor;
            combine.SaveResult(sender, e);
        }
    }
}
