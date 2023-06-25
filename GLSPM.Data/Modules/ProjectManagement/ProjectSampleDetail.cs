using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    public class ProjectSampleDetail
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 Id { get; set; }

        [Display(Name = "Project Sample Id")]
        public Int64? ProjectSampleId { get; set; }

        [Display(Name = "Sample Type Id")]
        public int? SampleTypeId { get; set; }

        [Display(Name = "Sample Type")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string SampleType { get; set; }

        [Display(Name = "Homogeneity")]
        public int? Homogeneity { get; set; }

        [Display(Name = "Sample Content")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string SampleContent { get; set; }

        [Display(Name = "Absestos Percentage")]
        public int? AbsestosPercentage { get; set; }

        [Display(Name = "Display Order")]
        public int? DisplayOrder { get; set; }

        [Display(Name = "Is Bilable")]
        public bool? IsBilable { get; set; }

        [Display(Name = "Composite Non Asbestos Contents")]
        public int? CompositeNonAsbestosContents { get; set; }
        public string CompositeNonAsbestosContentsText { get; set; }

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

        public string AbsestosPercentageText { get; set; }

        public bool? IsAmendment { get; set; }
    }
}
