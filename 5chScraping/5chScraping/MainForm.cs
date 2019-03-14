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
using _5chScraping.Model;
using _5chScraping.Scraping;

namespace _5chScraping
{
    public partial class Form1 : Form
    {
        private Scrapinger scrapinger;        
        private bool scrapingContinue = false;


        private Regex urlRegex = new Regex("http://.*|https://.*");
        private Regex currentRegex = new Regex("http.*2ch.*/");

        public Form1()
        {
            InitializeComponent();            
            scrapinger = new Scrapinger();
            textBoxThreadURL.Text = "http://nozomi.2ch.sc/test/read.cgi/comic/1552405298/0-";
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private async void ButtonScrapingExecute_Click(object sender, EventArgs e)
        {
            if (urlRegex.IsMatch(textBoxThreadURL.Text))
            {
                var items = new Tuple<ICollection<kakikomi>, Uri>(null, null);
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
                
                var kakikomes = items.Item1;
                var nextThread = items.Item2;
                if(nextThread != null)
                {
                    textBoxNextThread.Text = nextThread.ToString();
                }

                //連続でスクレイピングを行う
                if (scrapingContinue)
                {

                    return;
                }

                ScrapingDataUtil.Save(
                    System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "data.csv"),
                    kakikomes);

                var scrapingResultForm = new ScrapingResultForm(kakikomes);
                scrapingResultForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("適切なURLを入力してください", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckBoxContinueScraping_CheckedChanged(object sender, EventArgs e)
        {
            scrapingContinue = checkBoxContinueScraping.Checked;
        }
    }
}
