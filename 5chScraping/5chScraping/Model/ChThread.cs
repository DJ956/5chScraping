﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5chScraping.Model
{
    public class ChThread
    {
        public string Name { get; set; }        
        public Uri Uri { get; set; }
        public ICollection<kakikomi> Kakikomies { get; set; }
    }
}
