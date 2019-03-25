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

        private static int INDEX_URL = 4;
        private static int INDEX_TITLE = 5;

        private static Regex filterRegex = new Regex(" |【|】|©2ch.net|[|]★");

        /// <summary>
        /// スレ内容をCSVに保存する
        /// フォーマット:
        /// (1行目)       スレ数,ID,コメント,投稿時刻_URL:https://~..._タイトル:---
        /// (2行目以降)   
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="chThread"></param>
        public static void Save(string fileName, ChThread chThread)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"スレ数,ID,コメント,投稿時刻,{chThread.Uri.ToString()},{chThread.Name}");
            foreach(var kakikomi in chThread.Kakikomies)
            {
                builder.AppendLine($"{kakikomi.Count},{kakikomi.ID},{kakikomi.Comment},{kakikomi.PostTime.ToString()}");
            }
            
            fileName = fileName.Trim();
            fileName = filterRegex.Replace(fileName, "");
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

        /// <summary>
        /// CSVファイルを読み込む
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static ChThread Load(string path)
        {
            var chThread = new ChThread();
            var kakikomies = new List<kakikomi>();
            var lines = File.ReadAllLines(path, Encoding.UTF8);

            bool skip = true;
            foreach(var line in lines)
            {
                //最初の一行目でスレのURI,タイトルを取る
                if (skip == true)
                {
                    var split = line.Split(',');
                    var uri = split[INDEX_URL];
                    var title = split[INDEX_TITLE];

                    chThread.Uri = new Uri(uri);
                    chThread.Name = title;

                    skip = false;
                }
                //スレ内容を取得する。
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
            chThread.Kakikomies = kakikomies;
            return chThread;
        }

        /// <summary>
        /// 単語数テキストを読み込む
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static WordCount LoadWordResult(string path)
        {
            var dict = new Dictionary<string, int>();

            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                if (!line.Contains(":")) { continue; }
                var split = line.Split(':');

                var word = split[0];
                int count = -1;
                if (!int.TryParse(split[1], out count)) { continue; }

                dict.Add(word, count);
            }

            return new WordCount(dict);
        }

    }
}
