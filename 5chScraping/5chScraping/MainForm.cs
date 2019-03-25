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

namespace _5chScraping
{
    public partial class Form1 : Form
    {
        private Scrapinger scrapinger;        
        private bool scrapingContinue = false;


        private Regex urlRegex = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        private Regex currentRegex = new Regex("http.*nozomi.*/");
        private Regex fateRegex = new Regex("http.*fate.*");
        private Regex karmaRegex = new Regex("http.*karma.*");
        private Regex ikuraRegex = new Regex("http.*ikura.*");
        private Regex maturiRegex = new Regex("http.*matsuri.*");
        private Regex rosieRegex = new Regex("http.*rosie.*");


        public Form1()
        {
            InitializeComponent();            
            scrapinger = new Scrapinger();
            textBoxThreadURL.Text = "http://nozomi.2ch.sc/test/read.cgi/comic/1552405298/0-";
            //textBoxThreadURL.Text = "https://fate.5ch.net/test/read.cgi/comic/1543879008/";
            //textBoxThreadURL.Text = "https://karma.5ch.net/test/read.cgi/comic/1495688463/";
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            var root = @"D:\Documents\GitHub\5chScraping\Analyze";
            var path = Path.Combine(root, "analyzer.py");
            var data = Path.Combine(root, "data.txt");
            var output = Path.Combine(root, "result.txt");
            var showCount = "20";

            var argument = path + " " + data + " " + output + " " + showCount;

            var pyProcess = new PyProcess(new AnalyzerProcessObserver());

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

            var items = new Tuple<ChThread, Uri>(null, null);
            //現行スレ(nozomi系)
            if (currentRegex.IsMatch(textBoxThreadURL.Text))
            {
                items = await scrapinger.Scraping(new Uri(textBoxThreadURL.Text));
            }
            //過去スレ(fate系)
            else if (fateRegex.IsMatch(textBoxThreadURL.Text))
            {
                items = await scrapinger.ScrapingPast(new Uri(textBoxThreadURL.Text));
            }
            //過去スレ(karma系)
            else if (karmaRegex.IsMatch(textBoxThreadURL.Text))
            {
                items = await scrapinger.ScrapingKarma(new Uri(textBoxThreadURL.Text));
            }
            //過去スレ(イクラ系)
            else if (ikuraRegex.IsMatch(textBoxThreadURL.Text))
            {
                items = await scrapinger.ScrapingIkura(new Uri(textBoxThreadURL.Text));
            }
            else if (maturiRegex.IsMatch(textBoxThreadURL.Text))
            {
                items = await scrapinger.ScrapingMaturi(new Uri(textBoxThreadURL.Text));
            }
            else if (rosieRegex.IsMatch(textBoxThreadURL.Text))
            {
                items = await scrapinger.ScrapingRosie(new Uri(textBoxThreadURL.Text));
            }

            //スクレイピングできなかった場合
            if(items.Item1 == null)
            {
                MessageBox.Show("スクレイピングに失敗しました", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonScrapingExecute.Enabled = true;
                return;
            }

            var chThread = items.Item1;
            var nextThread = items.Item2;
            
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
            var scrapingResultForm = new ScrapingResultForm(chThread.Kakikomies);
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
                var kakikomies = ScrapingDataUtil.Load(dialog.FileName);
                var scrapingResultForm = new ScrapingResultForm(kakikomies);
                scrapingResultForm.ShowDialog();
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
