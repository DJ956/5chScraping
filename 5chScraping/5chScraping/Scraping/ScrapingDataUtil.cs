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
        private static int INDEX_COUNT = 0;
        private static int INDEX_ID = 1;
        private static int INDEX_COMMENT = 2;
        private static int INDEX_DATETIME = 3;
      
        public static void Save(string fileName, ChThread chThread)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"スレ数,ID,コメント,投稿時刻_URL:{chThread.Uri.ToString()}_タイトル:{chThread.Name}");
            foreach(var kakikomi in chThread.Kakikomies)
            {
                builder.AppendLine($"{kakikomi.Count},{kakikomi.ID},{kakikomi.Comment},{kakikomi.PostTime.ToString()}");
            }
            
            fileName = fileName.Trim();
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
            try
            {
                File.WriteAllText(path, builder.ToString(), Encoding.UTF8);
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }

        public static ICollection<kakikomi> Load(string path)
        {
            var kakikomies = new List<kakikomi>();
            var lines = File.ReadAllLines(path, Encoding.UTF8);
            bool skip = true;
            foreach(var line in lines)
            {
                if(skip == true) { skip = false; continue; } //最初の1行は読み飛ばす
                var documents = line.Split(',');
                if(documents.Length == 4)
                {
                    var count = int.Parse(documents[INDEX_COUNT]);
                    var id = documents[INDEX_ID];
                    var comment = documents[INDEX_COMMENT];
                    var postTime = DateTime.Parse(documents[INDEX_DATETIME]);

                    var kakikomi = new kakikomi()
                    {
                        Count = count,
                        ID = id,
                        Comment = comment,
                        PostTime = postTime,
                        KakikomiType = KakikomiType.None
                    };

                    kakikomies.Add(kakikomi);
                }
            }

            return kakikomies;
        }

    }
}
