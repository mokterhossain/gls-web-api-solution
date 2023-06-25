using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    [NotMapped]
    public class ProjectViewModel : Project
    {
        public string BatchNumber { get; set; }
        public int SerialNo { get; set; }
        public string LabId { get; set; }
        public string SampleType { get; set; }
        public string TAT { get; set; }
        public string Location { get; set; }
        public string ClientName { get; set; }
        public string ReportingPerson { get; set; }

        public long SampleId { get; set; }

        public string ProjectStatus { get; set; }

        public string AnalystSignature { get; set; }
        public string LabratoryManagerSignature { get; set; }
        public string ProjectAnalystName { get; set; }
        public string LabratoryManagerName { get; set; }
        public string TATColor { get; set; }
        public string ProjectAnalystDiploma { get; set; }
        public string LabratoryManagerDiploma { get; set; }

        public List<ProjectSampleViewModel> ProjectSample { get; set; }
    }
}
