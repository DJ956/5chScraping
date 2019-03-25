using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using _5chScraping.Model;
using System.IO;
using _5chScraping.Scraping;

namespace _5chScraping.Analyze
{
    /// <summary>
    /// Python プロセスを実行するクラス
    /// </summary>
    public class PyProcess
    {
        private IProcessObserver observer;

        private static readonly string MECAB = "mecab_csv.py";
        private static readonly string ANALYZER = "analyzer.py";

        public PyProcess(IProcessObserver observer)
        {
            this.observer = observer;
        }

        private void Execute(string argument)
        {
            using (var p = new Process())
            {
                p.StartInfo.FileName = "python";
                p.StartInfo.Arguments = argument;
                p.StartInfo.UseShellExecute = true;
                observer.StartProcess();
                p.Start();
                p.WaitForExit();
                observer.EndProcess();
            }
        }

        /// <summary>
        /// CSVファイルから頻出単語リストを出力する
        /// </summary>
        /// <param name="csv"></param>
        /// <param name="showCount"></param>
        /// <returns></returns>
        public WordCount CallWordCount(string csv, int showCount)
        {
            var root = Path.GetDirectoryName(csv);

            var mecab_path = Path.Combine(root, MECAB);
            var outPath = Path.Combine(root, "data.txt");
            var arguments = $"{mecab_path} {csv} {outPath}";
            Execute(arguments);

            var analyzer_Path = Path.Combine(root, ANALYZER);
            var resultPath = Path.Combine(root, "result.txt");
            arguments = $"{analyzer_Path} {outPath} {resultPath} {showCount}";
            Execute(arguments);

            var wordCount = ScrapingDataUtil.LoadWordResult(resultPath);

            return wordCount;
        }
    }
}
