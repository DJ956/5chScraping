using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5chScraping
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMainForm
    {
        /// <summary>
        /// Pythonプロセスを開始したときに実行
        /// </summary>
        void StartProcess();

        /// <summary>
        /// Pythonプロセスが終了した時に実行
        /// </summary>
        void EndProcess();
    }
}
