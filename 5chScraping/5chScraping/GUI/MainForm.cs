using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using _5chScraping.Model;
using _5chScraping.Scraping;
using _5chScraping.Analyze;
using _5chScraping.GUI;

namespace _5chScraping
{
    public partial class Form1 : Form
    {
        private Scrapinger scrapinger;
        private bool scrapingContinue = false;

        private Regex urlRegex = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
       
        public Form1()
        {
            InitializeComponent();            
            scrapinger = new Scrapinger();
            textBoxThreadURL.Text = "http://nozomi.2ch.sc/test/read.cgi/comic/1552405298/0-";
            
            if (Properties.Settings.Default.rootDirectory == "")
            {
                Properties.Settings.Default.rootDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                Properties.Settings.Default.Save();
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {           
        }

        private async void ButtonScrapingExecute_Click(object sender, EventArgs e)
        {
            if (!urlRegex.IsMatch(textBoxThreadURL.Text))
            {
                MessageBox.Show("適切なURLを入力してください", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var rootDirectory = Properties.Settings.Default.rootDirectory;

            buttonScrapingExecute.Enabled = false;
            toolStripProgressBar.Value = 0;
            toolStripProgressBar.Style = ProgressBarStyle.Marquee;

            var uri = new Uri(textBoxThreadURL.Text);
            var items = await scrapinger.ScrapingThread(uri);

            var chThread = items.Item1;
            var nextThread = items.Item2;

            //スクレイピングできなかった場合 終了する
            if (chThread == null)
            {
                MessageBox.Show("スクレイピングに失敗しました", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonScrapingExecute.Enabled = true;
                toolStripProgressBar.Value = 0;
                toolStripProgressBar.Style = ProgressBarStyle.Blocks;
                buttonScrapingExecute.Enabled = true;
                return;
            }

            if (nextThread != null)
            {
                //次スレを取得できればTextBoxに表示させる
                textBoxNextThread.Text = nextThread.ToString();
                //連続スクレイピングを行う場合
                if (scrapingContinue)
                {
                    this.Text = "スクレイピング: " + chThread.Name;
                    //スクレイピング結果を保存する
                    try
                    {
                        ScrapingDataUtil.Save(Path.Combine(rootDirectory, $"{chThread.Name}.csv"),
                                chThread);
                    }catch(ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                        
                    }
                    //次スレURLを現行スレURLに置き換えて再びスクレイピングを行う
                    textBoxThreadURL.Text = textBoxNextThread.Text;
                    buttonScrapingExecute.Enabled = true; //一時的にEnableにしないとPerformClickが動かない
                    buttonScrapingExecute.PerformClick();
                    return;
                }
            }

            //スクレイピング結果を保存する
            var savePath = Path.Combine(rootDirectory, $"{chThread.Name}.csv");
            try
            {
                ScrapingDataUtil.Save(savePath, chThread);
            }catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            this.Text = "スクレイピング: " + chThread.Name;

            toolStripProgressBar.Value = 100;
            toolStripProgressBar.Style = ProgressBarStyle.Blocks;
            buttonScrapingExecute.Enabled = true;

            //スクレイピング結果を表示させる
            var scrapingResultForm = new ScrapingResultForm(chThread, savePath);
            scrapingResultForm.ShowDialog();
        }

        private void CheckBoxContinueScraping_CheckedChanged(object sender, EventArgs e)
        {
            scrapingContinue = checkBoxContinueScraping.Checked;
        }

        private void OpenCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "スクレイピング結果を読み込む";
            dialog.Filter = "CSVファイル(*.csv)|*.csv";
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                var chThread = ScrapingDataUtil.Load(dialog.FileName);
                var scrapingResultForm = new ScrapingResultForm(chThread, dialog.FileName);
                scrapingResultForm.ShowDialog();
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //保存先を指定
            var dialog = new FolderBrowserDialog();
            dialog.Description = "CSVファイルを保存するフォルダ";            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.rootDirectory = dialog.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }
    }
}
