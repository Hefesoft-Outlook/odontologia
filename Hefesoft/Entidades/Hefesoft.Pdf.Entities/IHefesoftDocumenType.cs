using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Pdf.Entities
{
    public interface IHefesoftDocumenType
    {
        TypeElement TypeElement { get; set; }
        string Path { get; set; }
        bool scale { get; set; }
        string Text { get; set; }
    }

    public enum TypeElement
    {
        none = 0,
        Image = 1,
        Paragraph = 2
    }
}
