﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5chScraping.Analyze
{
    public interface IProcessObserver
    {
        void StartProcess();
        void EndProcess();      
    }
}
