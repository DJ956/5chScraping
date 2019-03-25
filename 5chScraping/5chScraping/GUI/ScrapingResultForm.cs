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

namespace _5chScraping
{
    public partial class ScrapingResultForm : Form
    {
        private ChThread chThread;

        public ScrapingResultForm(ChThread chThread)
        {
            InitializeComponent();
            this.chThread = chThread;
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
    }
}
