using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.BasicModule
{
    public class GeneralSetting
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public int Id { get; set; }

        [Display(Name = "Qc After Percentage PLM")]
        public decimal? QcAfterPercentagePLM { get; set; }

        [Display(Name = "Qc After Percentage PCM")]
        public decimal? QcAfterPercentagePCM { get; set; }

        public decimal? QcAfterPercentageMold { get; set; }
        public double? MicroscopeFieldDiameter { get; set; }
        public double? TraverseNumber { get; set; }
        public double? Volume { get; set; }
        public double? FungalCount { get; set; }

        public string Limitofdetection { get; set; }
    }
}
