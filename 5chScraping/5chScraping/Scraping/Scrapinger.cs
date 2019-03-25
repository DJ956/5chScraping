using _5chScraping.Model;
using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using AngleSharp.Html.Parser;
using AngleSharp.Dom;
using System.Text.RegularExpressions;
using AngleSharp.Html.Dom;

namespace _5chScraping.Scraping
{
    public class Scrapinger
    {
        private HtmlParser parser;
        private Regex nextRegex;

        public Scrapinger()
        {
            parser = new HtmlParser();         
            nextRegex = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        }

        /// <summary>
        /// 処理する対象のサイトがどのメソッドを使用すればいいかに正規表現を使っていたが限界が来たために自動で判断するようにした
        /// サイトのタグが現行スレ系か、過去スレ経過を見定める
        /// 今後新しいタイプのタグのサイトが増える可能性あり。
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private async Task<Tuple<SiteType, IHtmlDocument>> WhichTypeAsync(Uri uri)
        {
            using (var webClient = new WebClient())
            {
                var document = await webClient.DownloadStringTaskAsync(uri);
                var root = await parser.ParseDocumentAsync(document);
                var type = SiteType.None;

                if (root.GetElementsByTagName("dd").Length > 10)
                {
                    type = SiteType.Current;
                }
                else if (root.GetElementsByTagName("div").HasClass("message"))
                {
                    type = SiteType.Past;
                }

                var result = new Tuple<SiteType, IHtmlDocument>(type, root);
                return result;
            }
        }

        private Uri SearchUri(string comment, Uri currentUri)
        {
            //次スレのURLを探す                
            if (nextRegex.IsMatch(comment))
            {
                foreach (var item in nextRegex.Matches(comment))
                {
                    var match = item.ToString();
                    //過去スレ以外のURLが引っかからないようにする
                    if (!match.Contains("read.cgi") || match.Contains(currentUri.ToString())) { continue; }

                    var endIndex = match.IndexOf(' ');
                    //URLのあとに空白やごみがあった場合取り除く
                    if (endIndex > 0)
                    {
                        match = match.Substring(0, endIndex);
                    }

                    //URL内にitestという文字があると移動した際にfateに置き換わるためページ内容が正常に取得できない問題対策用
                    if (match.Contains("itest")) { match = match.Replace("itest", "fate"); }

                    return new Uri(match);
                }                
            }
            return null;
        }

        /// <summary>
        /// スレのタグからどのタイプのスクレイピングを行えばいいか自動で判断するようにした改良版
        /// </summary>
        /// <param name="threadUri"></param>
        /// <returns></returns>
        public async Task<Tuple<ChThread, Uri>> ScrapingThread(Uri threadUri)
        {
            var result = await WhichTypeAsync(threadUri);
            var type = result.Item1;
            var root = result.Item2;

            var items = new Tuple<ChThread, Uri>(null, null);
            switch (type)
            {
                case SiteType.Current:
                    {
                        items = await Scraping(threadUri, root);
                        break;
                    }
                case SiteType.Past:
                    {
                        items = await ScrapingPast(threadUri, root);
                        break;
                    }
                case SiteType.None:
                    {
                        break;
                    }
            }
            return items;
        }

        /// <summary>
        /// 過去スレをスクレイピング(fate系)
        /// </summary>
        /// <param name="threadUri"></param>
        /// <returns></returns>
        private async Task<Tuple<ChThread, Uri>> ScrapingPast(Uri threadUri, IHtmlDocument root)
        {
            if (root == null)
            {
                using (var webClient = new WebClient())
                {
                    var document = await webClient.DownloadStringTaskAsync(threadUri);
                    root = await parser.ParseDocumentAsync(document);
                }
            }
            var title = root.Title;
            
            var kakikomiDetail = root.GetElementsByTagName("div");  
            var kakikomiID = root.GetElementsByClassName("uid");    //ID
            var kakikomiTime = root.GetElementsByClassName("date"); //書き込み時刻
            var kakikomiSource = root.GetElementsByClassName("message"); //コメント
            Uri nextUri = null;

            var dateRegex = new Regex("[0-9]+/[0-9]+/[0-9]+");
            var timeRegex = new Regex("[0-9]+:[0-9]+:[0-9]+");            

            var kakikomies = new List<kakikomi>(kakikomiSource.Length);
            for (int index = 0; index < kakikomiSource.Length; index++)
            {
                var detail = kakikomiDetail[index].OuterHtml.Trim(' ');

                var idString = kakikomiID[index].TextContent;   //ID

                DateTime postTime = new DateTime();
                if (dateRegex.IsMatch(kakikomiTime[index].TextContent))
                {
                    var dateString = dateRegex.Match(kakikomiTime[index].TextContent);
                    var timeString = timeRegex.Match(kakikomiTime[index].TextContent);
                    var dateTimeString = dateString + " " + timeString;
                    postTime = DateTime.Parse(dateTimeString);  //書き込み時刻
                }

                var kakikomi = new kakikomi()
                {
                    Comment = kakikomiSource[index].TextContent.Trim(),    //コメント
                    Count = index,
                    ID = idString,
                    PostTime = postTime,
                    KakikomiType = KakikomiType.None
                };

                kakikomies.Add(kakikomi);

                if (nextUri != null) { continue; }

                //次スレのURLを探す     
                nextUri = SearchUri(kakikomi.Comment, threadUri);
            }

            var chThread = new ChThread()
            {
                Name = title,
                Uri = threadUri,
                Kakikomies = kakikomies
            };

            return new Tuple<ChThread, Uri>(chThread, nextUri);
        }

        /// <summary>
        /// 現行スレをスクレイピング
        /// </summary>
        /// <param name="threadUri"></param>
        /// <returns>引数のスレURLをスクレイピングし、書き込み一覧を返す。
        /// 次スレのURLも存在すれば取得する</returns>        
        private async Task<Tuple<ChThread, Uri>> Scraping(Uri threadUri, IHtmlDocument root)
        {
            if (root == null)
            {
                using (var webClient = new WebClient())
                {
                    var document = await webClient.DownloadStringTaskAsync(threadUri);
                    root = await parser.ParseDocumentAsync(document);
                }
            }
            var title = root.Title;

            var kakikomiDetail = root.GetElementsByTagName("dt");
            var kakikomiSource = root.GetElementsByTagName("dd");

            Uri nextUri = null;

            var trimRegex = new Regex("<.>|<..>|<...>");
            var trimRegex_2 = new Regex("<.*>");
            var dateRegex = new Regex("[0-9]+/[0-9]+/[0-9]+");
            var timeRegex = new Regex("[0-9]+:[0-9]+:[0-9]+");
            var idRegex = new Regex("ID:.*");            

            var kakikomies = new List<kakikomi>(kakikomiSource.Length);

            for(int index = 0; index < kakikomiSource.Length; index++)
            {                
                var detail = kakikomiDetail[index].OuterHtml.Trim(' ');
                //<>を削除                
                detail = trimRegex.Replace(detail, "");                
                detail = trimRegex_2.Replace(detail, "");
                //<時間取り出し>                               
                DateTime postTime = new DateTime();
                if (dateRegex.IsMatch(detail))
                {
                    var datetimeString = dateRegex.Match(detail).Value;
                    var timeString = timeRegex.Match(detail).Value;
                    datetimeString += " " + timeString;

                    postTime = DateTime.Parse(datetimeString);
                }

                //ID取り出し                
                var idString = idRegex.Match(detail).Value.Replace("ID:","");

                var kakikomi = new kakikomi()
                {
                    Comment = kakikomiSource[index].TextContent.Trim(),
                    Count = index,
                    ID = idString,
                    PostTime = postTime,
                    KakikomiType = KakikomiType.None
                };
                
                kakikomies.Add(kakikomi);

                if(nextUri != null) { continue; }

                //次スレのURLを探す                
                nextUri = SearchUri(kakikomi.Comment, threadUri);        
            }

            var chThread = new ChThread()
            {
                Name = title,
                Uri = threadUri,
                Kakikomies = kakikomies
            };

            return new Tuple<ChThread, Uri>(chThread, nextUri);
        }
    }
}
