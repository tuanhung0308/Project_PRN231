using System;
using System.Collections.Generic;

namespace Project_PRN231_API.Models
{
    public partial class Report
    {
        public int ReportId { get; set; }
        public string ReportName { get; set; } = null!;
        public DateTime GeneratedDate { get; set; }
        public string Data { get; set; } = null!;
    }
}
