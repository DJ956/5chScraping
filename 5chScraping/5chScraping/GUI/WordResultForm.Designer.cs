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
            this.listViewWords = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listViewWords
            // 
            this.listViewWords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewWords.Location = new System.Drawing.Point(0, 0);
            this.listViewWords.Name = "listViewWords";
            this.listViewWords.Size = new System.Drawing.Size(563, 258);
            this.listViewWords.TabIndex = 0;
            this.listViewWords.UseCompatibleStateImageBehavior = false;
            // 
            // WordResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 258);
            this.Controls.Add(this.listViewWords);
            this.Name = "WordResultForm";
            this.Text = "WordResultForm";
            this.Load += new System.EventHandler(this.WordResultForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewWords;
    }
}