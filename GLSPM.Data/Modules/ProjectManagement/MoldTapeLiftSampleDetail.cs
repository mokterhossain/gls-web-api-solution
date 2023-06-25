using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    public class MoldTapeLiftSampleDetail
    {
        [Key]
        public long Id { get; set; }
        public long MoldSampleId { get; set; }
        public int SporeTypeId { get; set; }
        public string RelativeMoldConc { get; set; }
    }
}
