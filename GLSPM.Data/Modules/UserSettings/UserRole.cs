using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.UserSettings
{
    public class UserRole
    {
        [Key]
        [Display(Name = "User Role I D")]
        [Required(ErrorMessage = "{0} is Required")]
        public int UserRoleID { get; set; }

        [Display(Name = "Role Name")]
        [StringLength(255, ErrorMessage = "Maximum length is {1}")]
        public string RoleName { get; set; }

        [Display(Name = "Created By")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created On")]
        public DateTime? CreatedOn { get; set; }

        [Display(Name = "Updated By")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Updated On")]
        public DateTime? UpdatedOn { get; set; }

        [Display(Name = "Key")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string Key { get; set; }
        public bool? IsActive { get; set; }
    }
}
