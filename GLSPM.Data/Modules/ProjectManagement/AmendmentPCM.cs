using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    public class AmendmentPCM
    {
        [Key]
        [Display(Name = "Amendment P C M Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 AmendmentPCMId { get; set; }

        [Display(Name = "Id")]
        public Int64? Id { get; set; }

        [Display(Name = "Project Id")]
        public Int64? ProjectId { get; set; }

        [Display(Name = "Sample Id")]
        public Int64? SampleId { get; set; }

        [Display(Name = "B C Sample")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string BCSample { get; set; }

        [Display(Name = "Field Blank Number")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string FieldBlankNumber { get; set; }

        [Display(Name = "Raw Fiber Count Half")]
        public int? RawFiberCountHalf { get; set; }

        [Display(Name = "Raw Fiber Count Whole")]
        public int? RawFiberCountWhole { get; set; }

        [Display(Name = "Fields Counted")]
        public int? FieldsCounted { get; set; }

        [Display(Name = "Volume L")]
        public double? VolumeL { get; set; }

        [Display(Name = "Filter Area M M")]
        public double? FilterAreaMM { get; set; }

        [Display(Name = "Calculated Fiber Count")]
        public double? CalculatedFiberCount { get; set; }

        [Display(Name = "Calculated Fiber Permm")]
        public double? CalculatedFiberPermm { get; set; }

        [Display(Name = "Calculated Fiber Percc")]
        public double? CalculatedFiberPercc { get; set; }

        [Display(Name = "Reported Fiber Permm")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string ReportedFiberPermm { get; set; }

        [Display(Name = "Reported Fiber Percc")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string ReportedFiberPercc { get; set; }

        [Display(Name = "L O D")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string LOD { get; set; }

        [Display(Name = "Sample Date")]
        public DateTime? SampleDate { get; set; }

        [Display(Name = "Is Blank")]
        public bool? IsBlank { get; set; }

        [Display(Name = "Is Duplicate")]
        public bool? IsDuplicate { get; set; }

        public string Amendment { get; set; }
        public string Comment { get; set; }

    }
}
