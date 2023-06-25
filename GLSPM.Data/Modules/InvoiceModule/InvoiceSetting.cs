using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.InvoiceModule
{
    public class InvoiceSetting
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public int Id { get; set; }

        [Display(Name = "Tax Percentage")]
        public decimal? TaxPercentage { get; set; }

        [Display(Name = "P S T")]
        public decimal? PST { get; set; }

        [Display(Name = "Shipping")]
        public decimal? Shipping { get; set; }
    }
}
