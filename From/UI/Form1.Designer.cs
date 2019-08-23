namespace StoneCount {
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
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batchProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.planarDetrendingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vgmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.windowToolStripMenuItem,
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
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.batchProcessToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // batchProcessToolStripMenuItem
            // 
            this.batchProcessToolStripMenuItem.Name = "batchProcessToolStripMenuItem";
            this.batchProcessToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.batchProcessToolStripMenuItem.Text = "BatchProcess";
            this.batchProcessToolStripMenuItem.Click += new System.EventHandler(this.batchProcessToolStripMenuItem_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarToolStripMenuItem,
            this.logsToolStripMenuItem,
            this.testToolStripMenuItem});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.windowToolStripMenuItem.Text = "Window";
            // 
            // toolBarToolStripMenuItem
            // 
            this.toolBarToolStripMenuItem.Name = "toolBarToolStripMenuItem";
            this.toolBarToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.toolBarToolStripMenuItem.Text = "ToolBar";
            this.toolBarToolStripMenuItem.Click += new System.EventHandler(this.toolBarToolStripMenuItem_Click);
            // 
            // logsToolStripMenuItem
            // 
            this.logsToolStripMenuItem.Name = "logsToolStripMenuItem";
            this.logsToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.logsToolStripMenuItem.Text = "Logs";
            this.logsToolStripMenuItem.Click += new System.EventHandler(this.logsToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.testToolStripMenuItem.Text = "Comparison Window";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.ComparisonToolStripMenuItem_Click);
            // 
            // extraToolStripMenuItem
            // 
            this.extraToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.planarDetrendingToolStripMenuItem,
            this.vgmToolStripMenuItem});
            this.extraToolStripMenuItem.Name = "extraToolStripMenuItem";
            this.extraToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.extraToolStripMenuItem.Text = "Extra";
            // 
            // planarDetrendingToolStripMenuItem
            // 
            this.planarDetrendingToolStripMenuItem.Name = "planarDetrendingToolStripMenuItem";
            this.planarDetrendingToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.planarDetrendingToolStripMenuItem.Text = "planar detrending";
            this.planarDetrendingToolStripMenuItem.Click += new System.EventHandler(this.planarDetrendingToolStripMenuItem_Click);
            // 
            // vgmToolStripMenuItem
            // 
            this.vgmToolStripMenuItem.Name = "vgmToolStripMenuItem";
            this.vgmToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.vgmToolStripMenuItem.Text = "vgm";
            this.vgmToolStripMenuItem.Click += new System.EventHandler(this.vgmToolStripMenuItem_Click);
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
            this.label1.Click += new System.EventHandler(this.label1_Click);
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
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logsToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem batchProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem planarDetrendingToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem vgmToolStripMenuItem;
    }
}

