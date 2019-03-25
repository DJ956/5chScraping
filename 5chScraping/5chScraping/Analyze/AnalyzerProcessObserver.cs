using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5chScraping.Analyze
{
    public class AnalyzerProcessObserver : IProcessObserver
    {
        private IMainForm form;

        public AnalyzerProcessObserver(IMainForm form)
        {
            this.form = form;
        }

        public void EndProcess()
        {
            form.EndProcess();
        }

        public void StartProcess()
        {
            form.StartProcess();
        }
    }
}
