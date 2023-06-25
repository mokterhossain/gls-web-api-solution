using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    [NotMapped]
    public class MoldViewModel
    {
        public long MoldSampleId { get; set; }
        public int SporeTypeId { get; set; }
        public decimal Rawct { get; set; }
        public decimal Permm { get; set; }
        public string SporeType { get; set; }

        public string RawctStr { get; set; }
        public string PermmStr { get; set; }

        public int RowNumber { get; set; }
        public string Location { get; set; }
        public string LabId { get; set; }
        public string VolumeStr { get; set; }
        public string AdditionalInformation { get; set; }
        public string BackgroundDebries { get; set; }
        public string SporeCategory { get; set; }
        public string AnalysisDate { get; set; }
        public string CommentsIndex { get; set; }
        public string Overloaded { get; set; }
        public string RawCtStringValue { get; set; }
        public string PermmStringValue { get; set; }

        public double? SerialNo { get; set; }
    }
}
