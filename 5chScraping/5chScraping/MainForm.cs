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

namespace _5chScraping
{
    public partial class Form1 : Form
    {
        private Scrapinger scrapinger;        
        private bool scrapingContinue = false;


        private Regex urlRegex = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        private Regex currentRegex = new Regex("http.*nozomi.*/");


        public Form1()
        {
            InitializeComponent();            
            scrapinger = new Scrapinger();
            //textBoxThreadURL.Text = "http://nozomi.2ch.sc/test/read.cgi/comic/1552405298/0-";
            //textBoxThreadURL.Text = "https://fate.5ch.net/test/read.cgi/comic/1543879008/";
            textBoxThreadURL.Text = "https://karma.5ch.net/test/read.cgi/comic/1495688463/";
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

            var items = new Tuple<ChThread, Uri>(null, null);
            //現行スレ
            if (currentRegex.IsMatch(textBoxThreadURL.Text))
            {
                items = await scrapinger.Scraping(new Uri(textBoxThreadURL.Text));
            }
            //過去スレ
            else
            {                
                items = await scrapinger.ScrapingPast(new Uri(textBoxThreadURL.Text));
            }
            //スクレイピングできなかった場合
            if(items.Item1 == null)
            {
                MessageBox.Show("スクレイピングに失敗しました", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    //スクレイピング結果を保存する
                    ScrapingDataUtil.Save(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"{chThread.Name}.csv"),
                            chThread.Kakikomies);
                    //次スレURLを現行スレURLに置き換えて再びスクレイピングを行う
                    textBoxThreadURL.Text = textBoxNextThread.Text;
                    buttonScrapingExecute.PerformClick();
                    return;
                }
            }

            //スクレイピング結果を保存する
            ScrapingDataUtil.Save($"{chThread.Name}.csv",
                chThread.Kakikomies);
            //スクレイピング結果を表示させる
            var scrapingResultForm = new ScrapingResultForm(chThread.Kakikomies);
            scrapingResultForm.ShowDialog();

        }

        private void CheckBoxContinueScraping_CheckedChanged(object sender, EventArgs e)
        {
            scrapingContinue = checkBoxContinueScraping.Checked;
        }
    }
}
