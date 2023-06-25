using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    public class QCAnalyticData
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 Id { get; set; }

        [Display(Name = "Sample Id")]
        public Int64? SampleId { get; set; }

        [Display(Name = "Data Point1 Value")]
        public double? DataPoint1Value { get; set; }

        [Display(Name = "Data Point2 Value")]
        public double? DataPoint2Value { get; set; }

        [Display(Name = "Q C Percentage")]
        public double? QCPercentage { get; set; }

        [Display(Name = "Q C Status")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string QCStatus { get; set; }

    }
}
