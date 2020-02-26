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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.OriginalImage_Lbl = new System.Windows.Forms.Label();
            this.Select_Ori_Btn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.flowbox = new System.Windows.Forms.FlowLayoutPanel();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.SieveMaster_box = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.finishProject_btn = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox5.SuspendLayout();
            this.SieveMaster_box.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.OriginalImage_Lbl);
            this.groupBox5.Controls.Add(this.Select_Ori_Btn);
            this.groupBox5.Location = new System.Drawing.Point(12, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(268, 60);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Original Image";
            // 
            // OriginalImage_Lbl
            // 
            this.OriginalImage_Lbl.AutoSize = true;
            this.OriginalImage_Lbl.Location = new System.Drawing.Point(98, 26);
            this.OriginalImage_Lbl.Name = "OriginalImage_Lbl";
            this.OriginalImage_Lbl.Size = new System.Drawing.Size(78, 12);
            this.OriginalImage_Lbl.TabIndex = 3;
            this.OriginalImage_Lbl.Text = "Select an Image";
            // 
            // Select_Ori_Btn
            // 
            this.Select_Ori_Btn.Location = new System.Drawing.Point(6, 21);
            this.Select_Ori_Btn.Name = "Select_Ori_Btn";
            this.Select_Ori_Btn.Size = new System.Drawing.Size(75, 23);
            this.Select_Ori_Btn.TabIndex = 0;
            this.Select_Ori_Btn.Text = "Preprocess";
            this.Select_Ori_Btn.UseVisualStyleBackColor = true;
            this.Select_Ori_Btn.Click += new System.EventHandler(this.SelectImage_Btn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // flowbox
            // 
            this.flowbox.Location = new System.Drawing.Point(6, 21);
            this.flowbox.Name = "flowbox";
            this.flowbox.Size = new System.Drawing.Size(252, 340);
            this.flowbox.TabIndex = 5;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar1.LargeChange = 1;
            this.vScrollBar1.Location = new System.Drawing.Point(261, 18);
            this.vScrollBar1.Maximum = 0;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(21, 337);
            this.vScrollBar1.TabIndex = 7;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // SieveMaster_box
            // 
            this.SieveMaster_box.Controls.Add(this.vScrollBar1);
            this.SieveMaster_box.Controls.Add(this.label1);
            this.SieveMaster_box.Controls.Add(this.flowbox);
            this.SieveMaster_box.Location = new System.Drawing.Point(12, 78);
            this.SieveMaster_box.Name = "SieveMaster_box";
            this.SieveMaster_box.Size = new System.Drawing.Size(285, 435);
            this.SieveMaster_box.TabIndex = 3;
            this.SieveMaster_box.TabStop = false;
            this.SieveMaster_box.Text = "SieveProcess";
            this.SieveMaster_box.Visible = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 364);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 36);
            this.label1.TabIndex = 6;
            // 
            // finishProject_btn
            // 
            this.finishProject_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.finishProject_btn.Location = new System.Drawing.Point(220, 447);
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
            this.ClientSize = new System.Drawing.Size(307, 482);
            this.Controls.Add(this.finishProject_btn);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.SieveMaster_box);
            this.Name = "ProjectForm";
            this.Text = "Project";
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.SieveMaster_box.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label OriginalImage_Lbl;
        private System.Windows.Forms.Button Select_Ori_Btn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FlowLayoutPanel flowbox;
        private System.Windows.Forms.GroupBox SieveMaster_box;
        private System.Windows.Forms.Button finishProject_btn;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
    }
}