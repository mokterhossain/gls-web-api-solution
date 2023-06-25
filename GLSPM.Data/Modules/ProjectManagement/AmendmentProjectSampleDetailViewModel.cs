using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    [NotMapped]
    public class AmendmentProjectSampleDetailViewModel : AmendmentProjectSampleDetail
    {
        public string LocationGroup { get; set; }
        public string LabIdGroup { get; set; }

        public int GroupNumber { get; set; }
        //public string SampleCompositeHomogeneityText { get; set; }
        //public string compositeNonAsbestosContentsText { get; set; }
    }
}
