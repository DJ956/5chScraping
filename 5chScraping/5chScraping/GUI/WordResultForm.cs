using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _5chScraping.Model;

namespace _5chScraping.GUI
{
    public partial class WordResultForm : Form
    {
        private string title;
        private Uri uri;
        private WordCount wordCount;

        public WordResultForm(string title, Uri uri, WordCount wordCount)
        {
            InitializeComponent();

            this.title = title;
            this.uri = uri;
            this.wordCount = wordCount;

            this.Text = title;

            listViewWords.GridLines = true;
            listViewWords.FullRowSelect = true;
            listViewWords.View = View.Details;


            var columnWord = new ColumnHeader();
            var columnCount = new ColumnHeader();
            
            columnCount.Text = "単語";
            columnCount.Width = 50;            
            columnCount.Text = "頻出回数";
            columnCount.Width = 50;
            
            var headers = new ColumnHeader[] { columnWord, columnCount };
            listViewWords.Columns.AddRange(headers);

            var index = 0;
            foreach (var key in wordCount.Words.Keys)
            {
                var count = wordCount.Words[key];
                listViewWords.Items.Add($"{wordCount.Words.Count}/{index} {key} : {count}");
                index++;
            }
        }

        private void WordResultForm_Load(object sender, EventArgs e)
        {

        }
    }
}
