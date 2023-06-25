using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    [NotMapped]
    public class SampleCountSummaryForReportViewModel
    {
        public int TotalSample { get; set; }
        public string ProjectType { get; set; }
        public string SampleDate { get; set; }
    }
}
