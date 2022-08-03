using System;

namespace GlonassSoft.Models
{
    public class ReportInfo
    {
        public Guid Query { get; set; }
        public byte Percent { get; set; }
        public ReportResult Result { get; set; }
    }
}
