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
    public partial class Form1 : Form, IMainForm
    {
        private Scrapinger scrapinger;
        private AnalyzerProcessObserver analyzerObserver;
        private bool scrapingContinue = false;

        private Regex urlRegex = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");

        public Form1()
        {
            InitializeComponent();            
            scrapinger = new Scrapinger();
            analyzerObserver = new AnalyzerProcessObserver(this);
            textBoxThreadURL.Text = "http://nozomi.2ch.sc/test/read.cgi/comic/1552405298/0-";
            //textBoxThreadURL.Text = "https://fate.5ch.net/test/read.cgi/comic/1543879008/";
            //textBoxThreadURL.Text = "https://karma.5ch.net/test/read.cgi/comic/1495688463/";
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
                        ScrapingDataUtil.Save(
                            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"{chThread.Name}.csv"),
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
            try
            {
                ScrapingDataUtil.Save($"{chThread.Name}.csv",
                    chThread);
            }catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            //スクレイピング結果を表示させる
            var scrapingResultForm = new ScrapingResultForm(chThread);
            scrapingResultForm.ShowDialog();

            this.Text = "スクレイピング: " + chThread.Name;

            toolStripProgressBar.Value = 100;
            toolStripProgressBar.Style = ProgressBarStyle.Blocks;
            buttonScrapingExecute.Enabled = true;
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
                var scrapingResultForm = new ScrapingResultForm(chThread);
                scrapingResultForm.ShowDialog();
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Python プロセスを実行開始した場合
        /// 進行バーを表示させる
        /// </summary>
        public void StartProcess()
        {
            Console.WriteLine("Start Process");
        }

        /// <summary>
        /// Pythonプロセスが終了したとき
        /// 進行バーを停止させ、結果を表示させる。
        /// </summary>
        public void EndProcess()
        {
            Console.WriteLine("End Process");
        }
    }
}
