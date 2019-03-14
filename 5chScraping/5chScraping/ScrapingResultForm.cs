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

namespace _5chScraping
{
    public partial class ScrapingResultForm : Form
    {
        private ICollection<kakikomi> Kakikomies;

        public ScrapingResultForm(ICollection<kakikomi> kakikomies)
        {
            InitializeComponent();
            this.Kakikomies = kakikomies;
            InitializeListView();            
        }

        private void InitializeListView()
        {            
            listViewKakikomi.GridLines = true;
            listViewKakikomi.FullRowSelect = true;
            listViewKakikomi.View = View.Details;            
            

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
            
            foreach(var kakikomi in Kakikomies)
            {
                var item = new string[] { kakikomi.Count.ToString(), kakikomi.ID, kakikomi.Comment, kakikomi.PostTime.ToString() };
                listViewKakikomi.Items.Add(new ListViewItem(item));
            }
        }

        private void ScrapingResultForm_Load(object sender, EventArgs e)
        {

        }
    }
}
