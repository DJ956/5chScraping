using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace _5chScraping.Analyze
{
    /// <summary>
    /// Python プロセスを実行するクラス
    /// </summary>
    public class PyProcess
    {
        //プロセスの開始と終了をFormに通知させる。
        private IProcessObserver observer;

        public PyProcess(IProcessObserver observer)
        {
            this.observer = observer;
        }

        public void Execute(string argument)
        {
            using (var p = new Process())
            {
                p.StartInfo.FileName = "python";
                p.StartInfo.Arguments = argument;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.UseShellExecute = false;
                p.Exited += ProcessExited;

                observer.StartProcess();
                p.Start();


                p.WaitForExit();
            }
        }

        private void ProcessExited(object sender, EventArgs e)
        {
            observer.EndProcess();
        }
    }
}
