using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ReportModule
{
    [Table("soot_report_details")]
    public class SootReportDetail
    {
        [Key]
        public long id { get; set; }

        public long? soot_report_id { get; set; }

        public int? analyte_id { get; set; }

        public int? concentration_cve_percentage_id { get; set; }

        public DateTime? created_at { get; set; }

        public string created_by { get; set; }

        public DateTime? updated_at { get; set; }

        public string updated_by { get; set; }
    }
}
