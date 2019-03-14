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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.listViewKakikomi = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 234);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(686, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // listViewKakikomi
            // 
            this.listViewKakikomi.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.Name = "ScrapingResultForm";
            this.Text = "ScrapingResultForm";
            this.Load += new System.EventHandler(this.ScrapingResultForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ListView listViewKakikomi;
    }
}