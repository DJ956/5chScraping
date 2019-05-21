using _5chScraping.Model;
using System;
using System.Collections.Generic;
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
        private List<Tuple<ChThread, DateTime>> threads;
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

        public ScrapingResultForm(List<Tuple<ChThread, DateTime>> threads)
        {
            InitializeComponent();
            this.threads = threads;
            InitializeCThreadsListView();
        }

        /// <summary>
        /// 1個のスレッドの表示
        /// </summary>
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

        /// <summary>
        /// 複数スレッドの表示
        /// </summary>
        private void InitializeCThreadsListView()
        {
            listViewKakikomi.GridLines = true;
            listViewKakikomi.FullRowSelect = true;
            listViewKakikomi.View = View.Details;

            this.Text = $"該当スレッド数:{threads.Count}";

            var columnCount = new ColumnHeader();
            var columnThreadTitle = new ColumnHeader();
            var columnThreadUpdateDay = new ColumnHeader();

            columnCount.Text = "スレ数";
            columnCount.Width = 80;

            columnThreadTitle.Text = "スレッドタイトル";
            columnThreadTitle.Width = 400;
            columnThreadTitle.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            columnThreadUpdateDay.Text = "更新日";
            columnThreadUpdateDay.Width = 200;

            var headers = new ColumnHeader[] { columnCount, columnThreadTitle, columnThreadUpdateDay };
            listViewKakikomi.Columns.AddRange(headers);

            var index = 1;
            foreach (var tuple in threads)
            {
                var thread = tuple.Item1;
                var item = new string[] { index.ToString(), thread.Name, tuple.Item2.ToShortDateString() };
                listViewKakikomi.Items.Add(new ListViewItem(item));
                index++;
            }

            listViewKakikomi.DoubleClick += (sender, e) =>
            {
                var selectIndex = listViewKakikomi.SelectedIndices[0];
                new ScrapingResultForm(threads[selectIndex].Item1, "").Show();
            };

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
