﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GateWay.Core.IDAL
{
    public interface ICrawler
    {
        void Crawler(string url);
    }
}
