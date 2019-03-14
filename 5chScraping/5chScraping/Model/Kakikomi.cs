using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5chScraping.Model
{
    public class kakikomi
    {
        public int Count { get; set; }
        public string Comment { get; set; }
        public string ID { get; set; }
        public DateTime PostTime { get; set; }
        public KakikomiType KakikomiType { get; set; }

        public override string ToString()
        {
            return $"{Count}: {PostTime} ID:{ID}-{Comment}";
        }
    }
}
