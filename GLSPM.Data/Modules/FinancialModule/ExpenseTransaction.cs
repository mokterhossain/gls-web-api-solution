using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.FinancialModule
{
    public class ExpenseTransaction
    {
        [Key]
        [Display(Name = "Expense Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 ExpenseId { get; set; }

        [Display(Name = "Expense Type Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public int ExpenseTypeId { get; set; }

        [Display(Name = "Date Of Transaction")]
        public DateTime? DateOfTransaction { get; set; }

        [Display(Name = "Description")]
        [StringLength(1000, ErrorMessage = "Maximum length is {1}")]
        public string Description { get; set; }

        [Display(Name = "Expense Amount")]
        public decimal? ExpenseAmount { get; set; }

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

    }
}
