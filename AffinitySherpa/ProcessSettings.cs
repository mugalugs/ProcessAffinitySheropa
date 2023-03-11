using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessAffinitySherpa
{
    internal class ProcessSettings
    {
        public new string Name { get; set; }
        public string FullPath { get; set; }
        public long Mask { get; set; }

        public ListViewItem AsListItem()
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = Name;
            return lvi;
        }
    }
}
