using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    public class Mold
    {
        [Key]
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public string ReportName { get; set; }
        public string Comments { get; set; }
        public List<MoldSample> MoldSamples { get; set; }
    }
}
