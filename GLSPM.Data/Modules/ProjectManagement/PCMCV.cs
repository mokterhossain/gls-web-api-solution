using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    public class PCMCV
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 Id { get; set; }

        [Display(Name = "Project Id")]
        public Int64? ProjectId { get; set; }

        [Display(Name = "Original Value")]
        public double? OriginalValue { get; set; }

        [Display(Name = "Duplicate Value")]
        public double? DuplicateValue { get; set; }

        [Display(Name = "T V Value")]
        public double? TVValue { get; set; }

        [Display(Name = "Dif Value")]
        public double? DifValue { get; set; }

        [Display(Name = "Q C Result")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string QCResult { get; set; }
    }
}
