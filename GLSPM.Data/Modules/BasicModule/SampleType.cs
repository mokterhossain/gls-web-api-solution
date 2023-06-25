using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.BasicModule
{
    public class SampleType
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string Name { get; set; }

        [Display(Name = "Item Code")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string ItemCode { get; set; }

        [Display(Name = "Price")]
        public decimal? Price { get; set; }

        [Display(Name = "T A T Value")]
        public decimal? TATValue { get; set; }

        public int? ClientId { get; set; }
        public string ClientName { get; set; }

        [Display(Name = "Project Type")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string ProjectType { get; set; }
    }
}
