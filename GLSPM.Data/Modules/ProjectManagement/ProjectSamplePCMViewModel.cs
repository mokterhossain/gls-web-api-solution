using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    [NotMapped]
    public class ProjectSamplePCMViewModel : PCM
    {
        public string LocationGroup { get; set; }
        public string LabIdGroup { get; set; }

        public int GroupNumber { get; set; }
        public string LabId { get; set; }

        public int Serial { get; set; }
    }
}
