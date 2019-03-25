using _5chScraping.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using _5chScraping.Analyze;
using _5chScraping.GUI;

namespace _5chScraping
{
    public partial class ScrapingResultForm : Form, IMainForm
    {
        private ChThread chThread;
        private string csvPath;
        private WordCount wordCount;
        private AnalyzerProcessObserver observer;

        public ScrapingResultForm(ChThread chThread, string csvPath)
        {
            InitializeComponent();
            this.chThread = chThread;
            this.csvPath = csvPath;
            observer = new AnalyzerProcessObserver(this);
            InitializeListView();            
        }

        private void InitializeListView()
        {            
            listViewKakikomi.GridLines = true;
            listViewKakikomi.FullRowSelect = true;
            listViewKakikomi.View = View.Details;

            this.Text = $"{chThread.Name} - {chThread.Uri}";
            labelThreadURL.Text = chThread.Uri.ToString();            

            var columnCount = new ColumnHeader();
            var columnComment = new ColumnHeader();
            var columnID = new ColumnHeader();
            var columnDateTime = new ColumnHeader();

            columnCount.Text = "スレ数";
            columnCount.Width = 50;
            columnID.Text = "ID";
            columnID.Width = 100;
            columnComment.Text = "コメント";
            columnComment.Width = 500;
            columnComment.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);            
            columnDateTime.Text = "投稿時刻";
            columnDateTime.Width = 150;

            var headers = new ColumnHeader[] { columnCount, columnID, columnComment, columnDateTime };
            listViewKakikomi.Columns.AddRange(headers);

            var index = 1;
            foreach(var kakikomi in chThread.Kakikomies)
            {
                var item = new string[] { index.ToString(), kakikomi.ID, kakikomi.Comment, kakikomi.PostTime.ToString() };
                listViewKakikomi.Items.Add(new ListViewItem(item));
                //if(index % 2 == 0) { listViewKakikomi.Items[index - 1].BackColor = Color.LightGray; }
                index++;
            }
        }

        private void ScrapingResultForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// スレのURLを踏むとブラウザが起動するようにする。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelThreadURL_Click(object sender, EventArgs e)
        {
            Process.Start(labelThreadURL.Text);
        }

        private void MenuItemWordCount_Click(object sender, EventArgs e)
        {
            var pyProcess = new PyProcess(observer);
            wordCount = pyProcess.CallWordCount(csvPath, 20);

            var resultForm = new WordResultForm(wordCount);
            resultForm.ShowDialog();
        }

        public void StartProcess()
        {
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.Value = 0;
        }

        public void EndProcess()
        {
            progressBar.Style = ProgressBarStyle.Blocks;
            progressBar.Value = 100;
        }
    }
}
