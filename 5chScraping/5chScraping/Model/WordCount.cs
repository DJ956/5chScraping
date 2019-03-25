using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5chScraping.Model
{
    public class WordCount
    {
        public WordCount(Dictionary<string, int> words)
        {
            Words = words;
        }

        public Dictionary<string, int> Words { get; set; }
    }
}
