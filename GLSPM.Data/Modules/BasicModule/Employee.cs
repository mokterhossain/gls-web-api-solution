using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.BasicModule
{
    public class Employee
    {
        [Key]
        [Display(Name = "Employee Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public int EmployeeId { get; set; }

        [Display(Name = "Name")]
        [StringLength(250, ErrorMessage = "Maximum length is {1}")]
        public string Name { get; set; }

        [Display(Name = "Designation")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Designation { get; set; }

        [Display(Name = "Cell No")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string CellNo { get; set; }

        [Display(Name = "Email")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Email { get; set; }

        [Display(Name = "Office Phone")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string OfficePhone { get; set; }

        [Display(Name = "Company")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Company { get; set; }

        [Display(Name = "Is Analyst")]
        public bool? IsAnalyst { get; set; }

        [Display(Name = "Is Labrotary Manager")]

        public bool? IsLabrotaryManager { get; set; }

        public bool? CanSignOnInvoice { get; set; }

        [Display(Name = "Is Qao")]
        public bool? IsQao { get; set; }
        

        [Display(Name = "Signature Url")]
        [StringLength(1000, ErrorMessage = "Maximum length is {1}")]
        public string SignatureUrl { get; set; }

        [Display(Name = "Is Active")]
        public bool? IsActive { get; set; }

        public string Diploma { get; set; }

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
