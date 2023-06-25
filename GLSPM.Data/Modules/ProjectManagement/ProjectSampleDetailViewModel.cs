using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    [NotMapped]
    public class ProjectSampleDetailViewModel : ProjectSampleDetail
    {
        public string LocationGroup { get; set; }
        public string LabIdGroup { get; set; }

        public int GroupNumber { get; set; }

        public string Layer { get; set; }
        public string SampleCompositeHomogeneityText { get; set; }
        //public string CompositeNonAsbestosContentsText { get; set; }
    }
}
