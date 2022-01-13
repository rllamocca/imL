﻿using imL.Format;
using imL.Frotcom.Hosting.Core;
using imL.Rest.Frotcom;
using imL.Utility.Hosting;

namespace SAMPLE.imL.Frotcom.Hosting.Core
{
    internal class MySettings : IHostSetting, IHostPeriodSetting
    {
        public EndpointFormat Endpoint { set; get; }
        public FrotcomFormat Frotcom { set; get; }

        public double Period { set; get; }
        public double Delay { set; get; }
    }
}
