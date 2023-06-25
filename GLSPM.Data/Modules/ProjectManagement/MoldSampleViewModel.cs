using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    [NotMapped]
    public class MoldSampleViewModel : MoldSample
    {
        public int SerialNo { get; set; }
        public string AnalysisDateStr { get; set; }
    }
}
