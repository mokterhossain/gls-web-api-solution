using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    public class MoldSampleDetail
    {
        [Key]
        public long Id { get; set; }
        public long MoldSampleId { get; set; }
        public int SporeTypeId { get; set; }
        public decimal RawCt { get; set; }
        public decimal Permm { get; set; }
        public string RawCtStringValue { get; set; }
        public string PermmStringValue { get; set; }
        public double? Volume { get; set; }
        public double? MicroscopeFieldDiameter { get; set; }
        public double? TraverseNumber { get; set; }
    }
}
