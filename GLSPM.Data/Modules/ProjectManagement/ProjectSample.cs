using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    public class ProjectSample
    {
        [Key]
        [Display(Name = "Sample Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 SampleId { get; set; }

        [Display(Name = "Project Id")]
        public Int64? ProjectId { get; set; }

        [Display(Name = "Batch Number")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string BatchNumber { get; set; }

        [Display(Name = "Serial No")]
        public int? SerialNo { get; set; }

        //[Display(Name = "Lab Id")]
        //public int? LabId { get; set; }
        [Display(Name = "Lab Id")]
        public string LabId { get; set; }

        [Display(Name = "Sample Type")]
        public string SampleType { get; set; }

        [Display(Name = "T A T")]
        public int? TAT { get; set; }

        [Display(Name = "Location")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string Location { get; set; }

        [Display(Name = "Analyst")]
        public int? Analyst { get; set; }

        [Display(Name = "Note")]
        public string Note { get; set; }
        [Display(Name = "Sample Composite Homogeneity")]
        public int? SampleCompositeHomogeneity { get; set; }

        [Display(Name = "Composite Non Asbestos Contents")]
        public int? CompositeNonAsbestosContents { get; set; }

        [Display(Name = "Package Code")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string PackageCode { get; set; }

        [Display(Name = "Created On")]
        public DateTime? CreatedOn { get; set; }

        [Display(Name = "Created By")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string CreatedBy { get; set; }

        [Display(Name = "Updated On")]
        public DateTime? UpdatedOn { get; set; }

        [Display(Name = "Updated By")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string UpdatedBy { get; set; }

        public string SampleCompositeHomogeneityText { get; set; }
        public string CompositeNonAsbestosContentsText { get; set; }

        public string TATText { get; set; }
        public string AnalystName { get; set; }

        public bool IsQC { get; set; }
        public string Matrix { get; set; }

        [Display(Name = "Volume")]
        public double? Volume { get; set; }
    }
}
