﻿namespace StoneCount {
    partial class Form1 {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent() {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateLocalWorldFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zerocontourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zerocontourComponentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.planerDetrendingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vGMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.factorialKrigingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zeroContourToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.visualizeToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dSMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detrendDSMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kriggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zeroContourToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.extraToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(430, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startProjectToolStripMenuItem,
            this.generateLocalWorldFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // startProjectToolStripMenuItem
            // 
            this.startProjectToolStripMenuItem.Name = "startProjectToolStripMenuItem";
            this.startProjectToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.startProjectToolStripMenuItem.Text = "StartProject";
            this.startProjectToolStripMenuItem.Click += new System.EventHandler(this.startProjectToolStripMenuItem_Click);
            // 
            // generateLocalWorldFileToolStripMenuItem
            // 
            this.generateLocalWorldFileToolStripMenuItem.Name = "generateLocalWorldFileToolStripMenuItem";
            this.generateLocalWorldFileToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.generateLocalWorldFileToolStripMenuItem.Text = "Generate Local World File";
            this.generateLocalWorldFileToolStripMenuItem.Click += new System.EventHandler(this.generateLocalWorldFileToolStripMenuItem_Click);
            // 
            // extraToolStripMenuItem
            // 
            this.extraToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zerocontourToolStripMenuItem,
            this.zerocontourComponentToolStripMenuItem,
            this.visualizeToolToolStripMenuItem});
            this.extraToolStripMenuItem.Name = "extraToolStripMenuItem";
            this.extraToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.extraToolStripMenuItem.Text = "Extra";
            // 
            // zerocontourToolStripMenuItem
            // 
            this.zerocontourToolStripMenuItem.Name = "zerocontourToolStripMenuItem";
            this.zerocontourToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.zerocontourToolStripMenuItem.Text = "zero-contour";
            this.zerocontourToolStripMenuItem.Click += new System.EventHandler(this.zerocontourToolStripMenuItem_Click);
            // 
            // zerocontourComponentToolStripMenuItem
            // 
            this.zerocontourComponentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.planerDetrendingToolStripMenuItem,
            this.vGMToolStripMenuItem,
            this.factorialKrigingToolStripMenuItem,
            this.zeroContourToolStripMenuItem1});
            this.zerocontourComponentToolStripMenuItem.Name = "zerocontourComponentToolStripMenuItem";
            this.zerocontourComponentToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.zerocontourComponentToolStripMenuItem.Text = "zero-contour component";
            // 
            // planerDetrendingToolStripMenuItem
            // 
            this.planerDetrendingToolStripMenuItem.Name = "planerDetrendingToolStripMenuItem";
            this.planerDetrendingToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.planerDetrendingToolStripMenuItem.Text = "planer detrending";
            this.planerDetrendingToolStripMenuItem.Click += new System.EventHandler(this.planerDetrendingToolStripMenuItem_Click);
            // 
            // vGMToolStripMenuItem
            // 
            this.vGMToolStripMenuItem.Name = "vGMToolStripMenuItem";
            this.vGMToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.vGMToolStripMenuItem.Text = "VGM";
            this.vGMToolStripMenuItem.Click += new System.EventHandler(this.vGMToolStripMenuItem_Click);
            // 
            // factorialKrigingToolStripMenuItem
            // 
            this.factorialKrigingToolStripMenuItem.Name = "factorialKrigingToolStripMenuItem";
            this.factorialKrigingToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.factorialKrigingToolStripMenuItem.Text = "factorial kriging";
            this.factorialKrigingToolStripMenuItem.Click += new System.EventHandler(this.factorialKrigingToolStripMenuItem_Click);
            // 
            // zeroContourToolStripMenuItem1
            // 
            this.zeroContourToolStripMenuItem1.Name = "zeroContourToolStripMenuItem1";
            this.zeroContourToolStripMenuItem1.Size = new System.Drawing.Size(176, 22);
            this.zeroContourToolStripMenuItem1.Text = "Zero Contour";
            this.zeroContourToolStripMenuItem1.Click += new System.EventHandler(this.zeroContourToolStripMenuItem1_Click);
            // 
            // visualizeToolToolStripMenuItem
            // 
            this.visualizeToolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dSMToolStripMenuItem,
            this.detrendDSMToolStripMenuItem,
            this.kriggingToolStripMenuItem,
            this.zeroContourToolStripMenuItem2});
            this.visualizeToolToolStripMenuItem.Name = "visualizeToolToolStripMenuItem";
            this.visualizeToolToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.visualizeToolToolStripMenuItem.Text = "visualize tool";
            // 
            // dSMToolStripMenuItem
            // 
            this.dSMToolStripMenuItem.Name = "dSMToolStripMenuItem";
            this.dSMToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.dSMToolStripMenuItem.Text = "DSM";
            this.dSMToolStripMenuItem.Click += new System.EventHandler(this.dSMToolStripMenuItem_Click);
            // 
            // detrendDSMToolStripMenuItem
            // 
            this.detrendDSMToolStripMenuItem.Name = "detrendDSMToolStripMenuItem";
            this.detrendDSMToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.detrendDSMToolStripMenuItem.Text = "Detrend DSM";
            this.detrendDSMToolStripMenuItem.Click += new System.EventHandler(this.detrendDSMToolStripMenuItem_Click);
            // 
            // kriggingToolStripMenuItem
            // 
            this.kriggingToolStripMenuItem.Name = "kriggingToolStripMenuItem";
            this.kriggingToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.kriggingToolStripMenuItem.Text = "Krigging";
            this.kriggingToolStripMenuItem.Click += new System.EventHandler(this.kriggingToolStripMenuItem_Click);
            // 
            // zeroContourToolStripMenuItem2
            // 
            this.zeroContourToolStripMenuItem2.Name = "zeroContourToolStripMenuItem2";
            this.zeroContourToolStripMenuItem2.Size = new System.Drawing.Size(151, 22);
            this.zeroContourToolStripMenuItem2.Text = "Zero Contour";
            this.zeroContourToolStripMenuItem2.Click += new System.EventHandler(this.zeroContourToolStripMenuItem2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(337, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 27);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(446, 66);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(446, 66);
            this.Name = "Form1";
            this.Text = "StoneCount";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        public System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem extraToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem startProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateLocalWorldFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zerocontourToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zerocontourComponentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem planerDetrendingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vGMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem factorialKrigingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zeroContourToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem visualizeToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dSMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detrendDSMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kriggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zeroContourToolStripMenuItem2;
    }
}

