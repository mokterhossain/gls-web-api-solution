using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    public class AmendmentProjectSample
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 Id { get; set; }

        [Display(Name = "Sample Id")]
        public Int64? SampleId { get; set; }

        [Display(Name = "Project Id")]
        public Int64? ProjectId { get; set; }

        [Display(Name = "Batch Number")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string BatchNumber { get; set; }

        [Display(Name = "Serial No")]
        public int? SerialNo { get; set; }

        [Display(Name = "Lab Id")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string LabId { get; set; }

        [Display(Name = "Sample Type")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string SampleType { get; set; }

        [Display(Name = "T A T")]
        public int? TAT { get; set; }

        [Display(Name = "T A T Text")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string TATText { get; set; }

        [Display(Name = "Location")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Location { get; set; }

        [Display(Name = "Analyst")]
        public int? Analyst { get; set; }

        [Display(Name = "Analyst Name")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string AnalystName { get; set; }

        [Display(Name = "Note")]
        public string Note { get; set; }

        [Display(Name = "Sample Composite Homogeneity")]
        public int? SampleCompositeHomogeneity { get; set; }

        [Display(Name = "Composite Non Asbestos Contents")]
        public int? CompositeNonAsbestosContents { get; set; }

        [Display(Name = "Sample Composite Homogeneity Text")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string SampleCompositeHomogeneityText { get; set; }

        [Display(Name = "Composite Non Asbestos Contents Text")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string CompositeNonAsbestosContentsText { get; set; }

        [Display(Name = "Is Q C")]
        public bool? IsQC { get; set; }

        [Display(Name = "Matrix")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Matrix { get; set; }

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
        public string Amendment { get; set; }

    }
}
