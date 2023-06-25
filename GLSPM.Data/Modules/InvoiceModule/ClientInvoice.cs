using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.InvoiceModule
{
    public class ClientInvoice
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 Id { get; set; }

        [Display(Name = "Client Id")]
        public int? ClientId { get; set; }

        [Display(Name = "Project Id")]
        public Int64? ProjectId { get; set; }

        [Display(Name = "Invoice Number")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Invoice Date")]
        public DateTime? InvoiceDate { get; set; }

        [Display(Name = "Payment Due Date")]
        public DateTime? PaymentDueDate { get; set; }

        [Display(Name = "Sub Total")]
        public decimal? SubTotal { get; set; }

        [Display(Name = "Tax Amount")]
        public decimal? TaxAmount { get; set; }

        [Display(Name = "P S T")]
        public decimal? PST { get; set; }

        [Display(Name = "Shipping")]
        public decimal? Shipping { get; set; }

        [Display(Name = "Discount")]
        public decimal? Discount { get; set; }

        [Display(Name = "Total")]
        public decimal? Total { get; set; }
        [Display(Name = "Sample Note")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string SampleNote { get; set; }

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

        public int? AccountsManagerId { get; set; }

        public bool? IsDiscountPercentage { get; set; }
        public decimal? DiscountPercentage { get; set; }
    }
}
