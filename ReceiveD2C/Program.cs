﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiveD2C
{
    class Program
    {
        static void Main(string[] args)
        {
            var cloudReceiver = new CloudReceiver();
            cloudReceiver.ReceiveWeatherData();
        }
    }
}
