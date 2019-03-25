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
using _5chScraping.Model;

namespace _5chScraping.GUI
{
    public partial class WordResultForm : Form
    {
        private WordCount wordCount;

        public WordResultForm(WordCount wordCount)
        {
            InitializeComponent();

            this.wordCount = wordCount;

            listViewWords.GridLines = true;
            listViewWords.FullRowSelect = true;
            listViewWords.View = View.Details;

            var columnIndex = new ColumnHeader();
            var columnWord = new ColumnHeader();
            var columnCount = new ColumnHeader();

            columnIndex.Text = "順位";
            columnIndex.Width = 100;
            columnWord.Text = "単語";
            columnWord.Width = 100;            
            columnCount.Text = "頻出回数";
            columnCount.Width = 100;
            
            var headers = new ColumnHeader[] { columnIndex, columnWord, columnCount };
            listViewWords.Columns.AddRange(headers);

            var index = 1;
            foreach (var key in wordCount.Words.Keys)
            {
                var count = wordCount.Words[key];
                var items = new string[] { $"{wordCount.Words.Count}/{index}", key, count.ToString() };
                listViewWords.Items.Add(new ListViewItem(items));
                index++;
            }
        }

        private void WordResultForm_Load(object sender, EventArgs e)
        {

        }
    }
}
