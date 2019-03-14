namespace _5chScraping
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxThreadURL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDocumentSource = new System.Windows.Forms.TextBox();
            this.buttonDocumentOpen = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxNextThread = new System.Windows.Forms.TextBox();
            this.buttonScrapingExecute = new System.Windows.Forms.Button();
            this.checkBoxContinueScraping = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(591, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 188);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(591, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "スレURL:";
            // 
            // textBoxThreadURL
            // 
            this.textBoxThreadURL.Location = new System.Drawing.Point(105, 31);
            this.textBoxThreadURL.Name = "textBoxThreadURL";
            this.textBoxThreadURL.Size = new System.Drawing.Size(337, 19);
            this.textBoxThreadURL.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "ドキュメントソース:";
            // 
            // textBoxDocumentSource
            // 
            this.textBoxDocumentSource.Location = new System.Drawing.Point(105, 93);
            this.textBoxDocumentSource.Name = "textBoxDocumentSource";
            this.textBoxDocumentSource.Size = new System.Drawing.Size(305, 19);
            this.textBoxDocumentSource.TabIndex = 5;
            // 
            // buttonDocumentOpen
            // 
            this.buttonDocumentOpen.Location = new System.Drawing.Point(416, 91);
            this.buttonDocumentOpen.Name = "buttonDocumentOpen";
            this.buttonDocumentOpen.Size = new System.Drawing.Size(29, 23);
            this.buttonDocumentOpen.TabIndex = 6;
            this.buttonDocumentOpen.Text = "...";
            this.buttonDocumentOpen.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "次スレ:";
            // 
            // textBoxNextThread
            // 
            this.textBoxNextThread.Location = new System.Drawing.Point(105, 61);
            this.textBoxNextThread.Name = "textBoxNextThread";
            this.textBoxNextThread.ReadOnly = true;
            this.textBoxNextThread.Size = new System.Drawing.Size(337, 19);
            this.textBoxNextThread.TabIndex = 8;
            // 
            // buttonScrapingExecute
            // 
            this.buttonScrapingExecute.Location = new System.Drawing.Point(458, 31);
            this.buttonScrapingExecute.Name = "buttonScrapingExecute";
            this.buttonScrapingExecute.Size = new System.Drawing.Size(121, 79);
            this.buttonScrapingExecute.TabIndex = 9;
            this.buttonScrapingExecute.Text = "スクレイピング";
            this.buttonScrapingExecute.UseVisualStyleBackColor = true;
            this.buttonScrapingExecute.Click += new System.EventHandler(this.ButtonScrapingExecute_Click);
            // 
            // checkBoxContinueScraping
            // 
            this.checkBoxContinueScraping.AutoSize = true;
            this.checkBoxContinueScraping.Location = new System.Drawing.Point(458, 116);
            this.checkBoxContinueScraping.Name = "checkBoxContinueScraping";
            this.checkBoxContinueScraping.Size = new System.Drawing.Size(110, 16);
            this.checkBoxContinueScraping.TabIndex = 10;
            this.checkBoxContinueScraping.Text = "連続スクレイピング";
            this.checkBoxContinueScraping.UseVisualStyleBackColor = true;
            this.checkBoxContinueScraping.CheckedChanged += new System.EventHandler(this.CheckBoxContinueScraping_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 210);
            this.Controls.Add(this.checkBoxContinueScraping);
            this.Controls.Add(this.buttonScrapingExecute);
            this.Controls.Add(this.textBoxNextThread);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonDocumentOpen);
            this.Controls.Add(this.textBoxDocumentSource);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxThreadURL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "スクレイピング:";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxThreadURL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDocumentSource;
        private System.Windows.Forms.Button buttonDocumentOpen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxNextThread;
        private System.Windows.Forms.Button buttonScrapingExecute;
        private System.Windows.Forms.CheckBox checkBoxContinueScraping;
    }
}

