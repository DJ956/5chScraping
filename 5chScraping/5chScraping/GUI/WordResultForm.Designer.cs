namespace _5chScraping.GUI
{
    partial class WordResultForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WordResultForm));
            this.listViewWords = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listViewWords
            // 
            this.listViewWords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewWords.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listViewWords.Location = new System.Drawing.Point(0, 0);
            this.listViewWords.Name = "listViewWords";
            this.listViewWords.Size = new System.Drawing.Size(311, 333);
            this.listViewWords.TabIndex = 2;
            this.listViewWords.UseCompatibleStateImageBehavior = false;
            // 
            // WordResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 333);
            this.Controls.Add(this.listViewWords);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WordResultForm";
            this.Text = "最頻出単語リスト";
            this.Load += new System.EventHandler(this.WordResultForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView listViewWords;
    }
}