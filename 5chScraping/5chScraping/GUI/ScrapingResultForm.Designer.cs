namespace _5chScraping
{
    partial class ScrapingResultForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScrapingResultForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.menuItemWordCount = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.labelThreadURL = new System.Windows.Forms.ToolStripStatusLabel();
            this.listViewKakikomi = new System.Windows.Forms.ListView();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.progressBar,
            this.labelThreadURL});
            this.statusStrip1.Location = new System.Drawing.Point(0, 234);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(686, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemWordCount});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(44, 20);
            this.toolStripDropDownButton1.Text = "解析";
            // 
            // menuItemWordCount
            // 
            this.menuItemWordCount.Name = "menuItemWordCount";
            this.menuItemWordCount.Size = new System.Drawing.Size(159, 22);
            this.menuItemWordCount.Text = "最頻出単語リスト";
            this.menuItemWordCount.Click += new System.EventHandler(this.MenuItemWordCount_Click);
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // labelThreadURL
            // 
            this.labelThreadURL.IsLink = true;
            this.labelThreadURL.Name = "labelThreadURL";
            this.labelThreadURL.Size = new System.Drawing.Size(31, 17);
            this.labelThreadURL.Text = "URL:";
            this.labelThreadURL.Click += new System.EventHandler(this.LabelThreadURL_Click);
            // 
            // listViewKakikomi
            // 
            this.listViewKakikomi.BackColor = System.Drawing.SystemColors.Window;
            this.listViewKakikomi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewKakikomi.Font = new System.Drawing.Font("游明朝", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listViewKakikomi.Location = new System.Drawing.Point(0, 0);
            this.listViewKakikomi.Name = "listViewKakikomi";
            this.listViewKakikomi.Size = new System.Drawing.Size(686, 234);
            this.listViewKakikomi.TabIndex = 1;
            this.listViewKakikomi.UseCompatibleStateImageBehavior = false;
            // 
            // ScrapingResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 256);
            this.Controls.Add(this.listViewKakikomi);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ScrapingResultForm";
            this.Text = "ScrapingResultForm";
            this.Load += new System.EventHandler(this.ScrapingResultForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ListView listViewKakikomi;
        private System.Windows.Forms.ToolStripStatusLabel labelThreadURL;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem menuItemWordCount;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
    }
}