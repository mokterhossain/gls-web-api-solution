using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    [NotMapped]
    public class MoldSampleDetailViewModel : MoldSampleDetail
    {
        public long SampleId { get; set; }
        public DateTime? AnalysisDate { get; set; }
        public string CommentsIndex { get; set; }
        public string Overloaded { get; set; }
        public bool IsQc { get; set; }
    }
}
