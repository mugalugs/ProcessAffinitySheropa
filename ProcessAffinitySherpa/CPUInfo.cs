using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessAffinitySherpa
{
    internal class CPUInfo
    {
        public string DeviceID { get; set; }
        public uint MaxClockSpeed { get; set; }
        public uint L3CacheSize { get; set; }
    }
}
