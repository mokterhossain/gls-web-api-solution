using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    public class MoldSample
    {
        
        [Key]
        public long Id { get; set; }
        public long MoldId { get; set; }
        public long SampleId { get; set; }
        public double Volume { get; set; }
        public string Location { get; set; }
        public string LabId { get; set; }
        public string AdditionalInformation { get; set; }
        public string BackgroundDebries { get; set; }
        public DateTime? AnalysisDate { get; set; }
        public string CommentsIndex { get; set; }
        public string Overloaded { get; set; }

        public bool? IsQC { get; set; }
        public bool? IsDuplicate { get; set; }

        public double? SerialNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public List<MoldSampleDetail> MoldSampleDetails { get; set; }

        public List<MoldTapeLiftSampleDetail> MoldTapeLiftSampleDetails { get; set; }
    }
}
