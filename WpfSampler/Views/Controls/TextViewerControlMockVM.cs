using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSampler.Views.Controls
{
    public class TextViewerControlMockVM
    {
        public string Label { get; set; } = "Text Label:";

        public string Text { get; set; } = "<TIMESTAMP> DEBUG This is the first entry.\r\n"
            + "<TIMESTAMP> DEBUG This is the second entry.\r\n"
            + "<TIMESTAMP> ERROR This is an error.\r\n";
    }
}
