namespace StoneCount.UI
{
    partial class ProjectForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.StartPreprocess_Btn = new System.Windows.Forms.Button();
            this.Preprocessing_Box = new System.Windows.Forms.GroupBox();
            this.RevertToOriginal_Btn = new System.Windows.Forms.Button();
            this.Preprocess_lbl = new System.Windows.Forms.Label();
            this.PreProcessedPreview_Btn = new System.Windows.Forms.Button();
            this.DonePreprocess_Btn = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.OriginalImage_Lbl = new System.Windows.Forms.Label();
            this.Preview_Ori_Btn = new System.Windows.Forms.Button();
            this.Done_Ori_Btn = new System.Windows.Forms.Button();
            this.Select_Ori_Btn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.flowbox = new System.Windows.Forms.FlowLayoutPanel();
            this.FirstSieve_box = new System.Windows.Forms.GroupBox();
            this.SecondSieve_box = new System.Windows.Forms.GroupBox();
            this.ThirdSieve_box = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ForthSieve_box = new System.Windows.Forms.GroupBox();
            this.FifthSieve_box = new System.Windows.Forms.GroupBox();
            this.SieveMaster_box = new System.Windows.Forms.GroupBox();
            this.finishProject_btn = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.Preprocessing_Box.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.flowbox.SuspendLayout();
            this.ThirdSieve_box.SuspendLayout();
            this.SieveMaster_box.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartPreprocess_Btn
            // 
            this.StartPreprocess_Btn.Location = new System.Drawing.Point(6, 21);
            this.StartPreprocess_Btn.Name = "StartPreprocess_Btn";
            this.StartPreprocess_Btn.Size = new System.Drawing.Size(75, 23);
            this.StartPreprocess_Btn.TabIndex = 0;
            this.StartPreprocess_Btn.Text = "Start";
            this.StartPreprocess_Btn.UseVisualStyleBackColor = true;
            this.StartPreprocess_Btn.Click += new System.EventHandler(this.StartPreprocessing_Button);
            // 
            // Preprocessing_Box
            // 
            this.Preprocessing_Box.Controls.Add(this.RevertToOriginal_Btn);
            this.Preprocessing_Box.Controls.Add(this.Preprocess_lbl);
            this.Preprocessing_Box.Controls.Add(this.PreProcessedPreview_Btn);
            this.Preprocessing_Box.Controls.Add(this.DonePreprocess_Btn);
            this.Preprocessing_Box.Controls.Add(this.StartPreprocess_Btn);
            this.Preprocessing_Box.Location = new System.Drawing.Point(12, 78);
            this.Preprocessing_Box.Name = "Preprocessing_Box";
            this.Preprocessing_Box.Size = new System.Drawing.Size(730, 60);
            this.Preprocessing_Box.TabIndex = 3;
            this.Preprocessing_Box.TabStop = false;
            this.Preprocessing_Box.Text = "Proprocessing";
            this.Preprocessing_Box.Visible = false;
            // 
            // RevertToOriginal_Btn
            // 
            this.RevertToOriginal_Btn.Location = new System.Drawing.Point(249, 21);
            this.RevertToOriginal_Btn.Name = "RevertToOriginal_Btn";
            this.RevertToOriginal_Btn.Size = new System.Drawing.Size(75, 23);
            this.RevertToOriginal_Btn.TabIndex = 4;
            this.RevertToOriginal_Btn.Text = "Discard All";
            this.RevertToOriginal_Btn.UseVisualStyleBackColor = true;
            this.RevertToOriginal_Btn.Click += new System.EventHandler(this.RevertToOriginal_Btn_Click);
            // 
            // Preprocess_lbl
            // 
            this.Preprocess_lbl.AutoSize = true;
            this.Preprocess_lbl.Location = new System.Drawing.Point(330, 26);
            this.Preprocess_lbl.Name = "Preprocess_lbl";
            this.Preprocess_lbl.Size = new System.Drawing.Size(0, 12);
            this.Preprocess_lbl.TabIndex = 3;
            // 
            // PreProcessedPreview_Btn
            // 
            this.PreProcessedPreview_Btn.Location = new System.Drawing.Point(87, 21);
            this.PreProcessedPreview_Btn.Name = "PreProcessedPreview_Btn";
            this.PreProcessedPreview_Btn.Size = new System.Drawing.Size(75, 23);
            this.PreProcessedPreview_Btn.TabIndex = 2;
            this.PreProcessedPreview_Btn.Text = "Preview";
            this.PreProcessedPreview_Btn.UseVisualStyleBackColor = true;
            this.PreProcessedPreview_Btn.Click += new System.EventHandler(this.PreProcessedPreview_Btn_Click);
            // 
            // DonePreprocess_Btn
            // 
            this.DonePreprocess_Btn.Location = new System.Drawing.Point(168, 21);
            this.DonePreprocess_Btn.Name = "DonePreprocess_Btn";
            this.DonePreprocess_Btn.Size = new System.Drawing.Size(75, 23);
            this.DonePreprocess_Btn.TabIndex = 1;
            this.DonePreprocess_Btn.Text = "Done";
            this.DonePreprocess_Btn.UseVisualStyleBackColor = true;
            this.DonePreprocess_Btn.Click += new System.EventHandler(this.DonePreprocess_Btn_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.OriginalImage_Lbl);
            this.groupBox5.Controls.Add(this.Preview_Ori_Btn);
            this.groupBox5.Controls.Add(this.Done_Ori_Btn);
            this.groupBox5.Controls.Add(this.Select_Ori_Btn);
            this.groupBox5.Location = new System.Drawing.Point(12, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(730, 60);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Original Image";
            // 
            // OriginalImage_Lbl
            // 
            this.OriginalImage_Lbl.AutoSize = true;
            this.OriginalImage_Lbl.Location = new System.Drawing.Point(249, 26);
            this.OriginalImage_Lbl.Name = "OriginalImage_Lbl";
            this.OriginalImage_Lbl.Size = new System.Drawing.Size(78, 12);
            this.OriginalImage_Lbl.TabIndex = 3;
            this.OriginalImage_Lbl.Text = "Select an Image";
            // 
            // Preview_Ori_Btn
            // 
            this.Preview_Ori_Btn.Location = new System.Drawing.Point(87, 21);
            this.Preview_Ori_Btn.Name = "Preview_Ori_Btn";
            this.Preview_Ori_Btn.Size = new System.Drawing.Size(75, 23);
            this.Preview_Ori_Btn.TabIndex = 2;
            this.Preview_Ori_Btn.Text = "Preview";
            this.Preview_Ori_Btn.UseVisualStyleBackColor = true;
            this.Preview_Ori_Btn.Click += new System.EventHandler(this.Preview_Ori_Btn_Click);
            // 
            // Done_Ori_Btn
            // 
            this.Done_Ori_Btn.Location = new System.Drawing.Point(168, 21);
            this.Done_Ori_Btn.Name = "Done_Ori_Btn";
            this.Done_Ori_Btn.Size = new System.Drawing.Size(75, 23);
            this.Done_Ori_Btn.TabIndex = 1;
            this.Done_Ori_Btn.Text = "Done";
            this.Done_Ori_Btn.UseVisualStyleBackColor = true;
            this.Done_Ori_Btn.Click += new System.EventHandler(this.Done_Ori_Btn_Click);
            // 
            // Select_Ori_Btn
            // 
            this.Select_Ori_Btn.Location = new System.Drawing.Point(6, 21);
            this.Select_Ori_Btn.Name = "Select_Ori_Btn";
            this.Select_Ori_Btn.Size = new System.Drawing.Size(75, 23);
            this.Select_Ori_Btn.TabIndex = 0;
            this.Select_Ori_Btn.Text = "Select";
            this.Select_Ori_Btn.UseVisualStyleBackColor = true;
            this.Select_Ori_Btn.Click += new System.EventHandler(this.SelectImage_Btn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // flowbox
            // 
            this.flowbox.Controls.Add(this.FirstSieve_box);
            this.flowbox.Controls.Add(this.SecondSieve_box);
            this.flowbox.Controls.Add(this.ThirdSieve_box);
            this.flowbox.Controls.Add(this.ForthSieve_box);
            this.flowbox.Controls.Add(this.FifthSieve_box);
            this.flowbox.Location = new System.Drawing.Point(6, 21);
            this.flowbox.Name = "flowbox";
            this.flowbox.Size = new System.Drawing.Size(724, 340);
            this.flowbox.TabIndex = 5;
            // 
            // FirstSieve_box
            // 
            this.FirstSieve_box.Location = new System.Drawing.Point(3, 3);
            this.FirstSieve_box.Name = "FirstSieve_box";
            this.FirstSieve_box.Size = new System.Drawing.Size(703, 60);
            this.FirstSieve_box.TabIndex = 0;
            this.FirstSieve_box.TabStop = false;
            this.FirstSieve_box.Text = "First Sieve";
            this.FirstSieve_box.Visible = false;
            // 
            // SecondSieve_box
            // 
            this.SecondSieve_box.Location = new System.Drawing.Point(3, 69);
            this.SecondSieve_box.Name = "SecondSieve_box";
            this.SecondSieve_box.Size = new System.Drawing.Size(703, 60);
            this.SecondSieve_box.TabIndex = 1;
            this.SecondSieve_box.TabStop = false;
            this.SecondSieve_box.Text = "Second Sieve";
            this.SecondSieve_box.Visible = false;
            // 
            // ThirdSieve_box
            // 
            this.ThirdSieve_box.Controls.Add(this.groupBox3);
            this.ThirdSieve_box.Location = new System.Drawing.Point(3, 135);
            this.ThirdSieve_box.Name = "ThirdSieve_box";
            this.ThirdSieve_box.Size = new System.Drawing.Size(703, 60);
            this.ThirdSieve_box.TabIndex = 1;
            this.ThirdSieve_box.TabStop = false;
            this.ThirdSieve_box.Text = "Third Sieve";
            this.ThirdSieve_box.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(6, 66);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(416, 60);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "First Sieve";
            this.groupBox3.Visible = false;
            // 
            // ForthSieve_box
            // 
            this.ForthSieve_box.Location = new System.Drawing.Point(3, 201);
            this.ForthSieve_box.Name = "ForthSieve_box";
            this.ForthSieve_box.Size = new System.Drawing.Size(703, 60);
            this.ForthSieve_box.TabIndex = 2;
            this.ForthSieve_box.TabStop = false;
            this.ForthSieve_box.Text = "Forth Sieve";
            this.ForthSieve_box.Visible = false;
            // 
            // FifthSieve_box
            // 
            this.FifthSieve_box.Location = new System.Drawing.Point(3, 267);
            this.FifthSieve_box.Name = "FifthSieve_box";
            this.FifthSieve_box.Size = new System.Drawing.Size(703, 60);
            this.FifthSieve_box.TabIndex = 3;
            this.FifthSieve_box.TabStop = false;
            this.FifthSieve_box.Text = "Fifth Sieve";
            this.FifthSieve_box.Visible = false;
            // 
            // SieveMaster_box
            // 
            this.SieveMaster_box.Controls.Add(this.flowbox);
            this.SieveMaster_box.Location = new System.Drawing.Point(12, 144);
            this.SieveMaster_box.Name = "SieveMaster_box";
            this.SieveMaster_box.Size = new System.Drawing.Size(730, 369);
            this.SieveMaster_box.TabIndex = 3;
            this.SieveMaster_box.TabStop = false;
            this.SieveMaster_box.Text = "SieveProcess";
            this.SieveMaster_box.Visible = false;
            this.SieveMaster_box.Enter += new System.EventHandler(this.SieveMaster_box_Enter);
            // 
            // finishProject_btn
            // 
            this.finishProject_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.finishProject_btn.Location = new System.Drawing.Point(667, 520);
            this.finishProject_btn.Name = "finishProject_btn";
            this.finishProject_btn.Size = new System.Drawing.Size(75, 23);
            this.finishProject_btn.TabIndex = 5;
            this.finishProject_btn.Text = "Done Project";
            this.finishProject_btn.UseVisualStyleBackColor = true;
            this.finishProject_btn.Click += new System.EventHandler(this.finishProject_btn_Click);
            // 
            // ProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 555);
            this.Controls.Add(this.finishProject_btn);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.Preprocessing_Box);
            this.Controls.Add(this.SieveMaster_box);
            this.Name = "ProjectForm";
            this.Text = "Project";
            this.Preprocessing_Box.ResumeLayout(false);
            this.Preprocessing_Box.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.flowbox.ResumeLayout(false);
            this.ThirdSieve_box.ResumeLayout(false);
            this.SieveMaster_box.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartPreprocess_Btn;
        private System.Windows.Forms.GroupBox Preprocessing_Box;
        private System.Windows.Forms.Button DonePreprocess_Btn;
        private System.Windows.Forms.Button PreProcessedPreview_Btn;
        private System.Windows.Forms.Label Preprocess_lbl;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label OriginalImage_Lbl;
        private System.Windows.Forms.Button Preview_Ori_Btn;
        private System.Windows.Forms.Button Done_Ori_Btn;
        private System.Windows.Forms.Button Select_Ori_Btn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button RevertToOriginal_Btn;
        private System.Windows.Forms.FlowLayoutPanel flowbox;
        private System.Windows.Forms.GroupBox SieveMaster_box;
        private System.Windows.Forms.GroupBox FirstSieve_box;
        private System.Windows.Forms.GroupBox SecondSieve_box;
        private System.Windows.Forms.GroupBox ThirdSieve_box;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox ForthSieve_box;
        private System.Windows.Forms.GroupBox FifthSieve_box;
        private System.Windows.Forms.Button finishProject_btn;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}