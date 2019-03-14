using _5chScraping.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _5chScraping.Scraping
{
    public class ScrapingDataUtil
    {
        public static void Save(string path, ICollection<kakikomi> kakikomies)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("スレ数,ID,コメント,投稿時刻");
            foreach(var kakikomi in kakikomies)
            {
                builder.AppendLine($"{kakikomi.Count},{kakikomi.ID},{kakikomi.Comment},{kakikomi.PostTime.ToString()}");
            }

            File.WriteAllText(path, builder.ToString(), Encoding.UTF8);
        }

        public static ICollection<kakikomi> Load(string path)
        {
            return null;
        }

    }
}
