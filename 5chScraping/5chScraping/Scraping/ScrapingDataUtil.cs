using _5chScraping.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace _5chScraping.Scraping
{
    public class ScrapingDataUtil
    {
        private static Regex indivialRegex = new Regex("[\\x00-\\x1f<>:\"/\\\\|?*]" +
        "|^(CON|PRN|AUX|NUL|COM[0-9]|LPT[0-9]|CLOCK\\$)(\\.|$)" +
        "|[\\. ]$", RegexOptions.IgnoreCase);

        public static void Save(string fileName, ICollection<kakikomi> kakikomies)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("スレ数,ID,コメント,投稿時刻");
            foreach(var kakikomi in kakikomies)
            {
                builder.AppendLine($"{kakikomi.Count},{kakikomi.ID},{kakikomi.Comment},{kakikomi.PostTime.ToString()}");
            }

            fileName =  indivialRegex.Replace(fileName, "").Trim();
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
            File.WriteAllText(path, builder.ToString(), Encoding.UTF8);
        }

        public static ICollection<kakikomi> Load(string path)
        {
            return null;
        }

    }
}
