using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessAffinitySherpa
{
    internal class ProcessSettings : ListViewItem
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public long Mask { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
