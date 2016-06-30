using System;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace CopyingCutingClipboard.Common
{
    [XmlRoot("copied-text"), XmlType("copied-text")]
    public class CopiedText
    {
        public int Order { get; set; }
        public string Text { get; set; }
        public string RichText { get; set; }
        public DateTime Date { get; set; }
        public string DataFormat { get; set; }
    }
}
