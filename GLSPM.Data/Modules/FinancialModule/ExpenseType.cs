using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.FinancialModule
{
    public class ExpenseType
    {
        [Key]
        [Display(Name = "Type Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public int TypeId { get; set; }

        [Display(Name = "Name")]
        [StringLength(250, ErrorMessage = "Maximum length is {1}")]
        public string Name { get; set; }

        [Display(Name = "Code")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Code { get; set; }
    }
}
