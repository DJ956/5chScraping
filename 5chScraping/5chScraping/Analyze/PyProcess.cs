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

        private static readonly string MECAB = "mecab_csv.exe";
        private static readonly string ANALYZER = "analyzer.exe";

        public PyProcess(IProcessObserver observer)
        {
            this.observer = observer;
        }

        private int Execute(string exeName, string argument)
        {
            using (var p = new Process())
            {
                p.StartInfo.FileName = exeName;
                p.StartInfo.Arguments = argument;
                p.StartInfo.UseShellExecute = true;
                observer.StartProcess();
                p.Start();
                p.WaitForExit();
                observer.EndProcess();
                return p.ExitCode;
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
            var outPath = Path.Combine(root, "data.txt");
            var arguments = $"{csv} {outPath}";
            if(Execute(MECAB, arguments) < 0)
            {
                throw new Exception($"{MECAB}が正常に処理されませんでした。");
            }
            
            var resultPath = Path.Combine(root, "result.txt");
            arguments = $"{outPath} {resultPath} {showCount}";
            if(Execute(ANALYZER, arguments) < 0){
                throw new Exception($"{ANALYZER}が正常に処理されませんでした。");
            }

            var wordCount = ScrapingDataUtil.LoadWordResult(resultPath);

            return wordCount;
        }
    }
}
