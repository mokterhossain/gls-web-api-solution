using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.InvoiceModule
{
    public class ClientInvoiceDetail
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 Id { get; set; }

        [Display(Name = "Invoice Id")]
        public Int64? InvoiceId { get; set; }

        [Display(Name = "Item Code")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string ItemCode { get; set; }

        [Display(Name = "Sample Type")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string SampleType { get; set; }

        [Display(Name = "Matrix")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Matrix { get; set; }

        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [Display(Name = "Unit Price")]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "Total Amount")]
        public decimal? TotalAmount { get; set; }
    }
}
