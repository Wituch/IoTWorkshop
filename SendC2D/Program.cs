﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendC2D
{
    class Program
    {
        static void Main(string[] args)
        {
            var deviceSender = new DeviceSender();
            deviceSender.SendMessage();
        }
    }
}
