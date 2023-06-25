using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.BasicModule
{
    public class Client
    {
        [Key]
        [Display(Name = "Client Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public int ClientId { get; set; }

        [Display(Name = "Client Name")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string ClientName { get; set; }

        [Display(Name = "Cell No")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string CellNo { get; set; }

        [Display(Name = "Office Phone")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string OfficePhone { get; set; }

        [Display(Name = "Email")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Email { get; set; }

        [Display(Name = "Address")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string Address { get; set; }

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
