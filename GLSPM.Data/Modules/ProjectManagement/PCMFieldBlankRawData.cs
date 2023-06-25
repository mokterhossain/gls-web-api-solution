using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    public class PCMFieldBlankRawData
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 Id { get; set; }

        [Display(Name = "Serial")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Serial { get; set; }

        [Display(Name = "Project Id")]
        public Int64? ProjectId { get; set; }

        [Display(Name = "B C Sample")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string BCSample { get; set; }

        [Display(Name = "Raw Fibers Count Half")]
        public int? RawFibersCountHalf { get; set; }

        [Display(Name = "Raw Fibers Count Whole")]
        public int? RawFibersCountWhole { get; set; }

        [Display(Name = "Fileds Counted")]
        public int? FiledsCounted { get; set; }

        [Display(Name = "Calculated Fibers Count")]
        public double? CalculatedFibersCount { get; set; }
    }
}
