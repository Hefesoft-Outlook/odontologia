﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Util.Reportes
{
    public class PrintingEventArgs : EventArgs
    {
        public object DataContext { get; set; }
    }
}