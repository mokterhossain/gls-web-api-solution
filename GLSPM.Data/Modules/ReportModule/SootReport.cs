using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ReportModule
{
    [Table("soot_report")]
    public class SootReport
    {
        [Key]
        public long id { get; set; }

        public long? client_id { get; set; }

        public string gls_lab_id { get; set; }

        public string deberis_rating { get; set; }

        public string comments { get; set; }

        public DateTime? created_at { get; set; }

        public string created_by { get; set; }

        public DateTime? updated_at { get; set; }

        public string updated_by { get; set; }
    }
}
