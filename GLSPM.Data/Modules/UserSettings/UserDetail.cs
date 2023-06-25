using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.UserSettings
{
    public class UserDetail
    {
        [Key]
        [Display(Name = "User Detail I D")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 UserDetailID { get; set; }

        [Display(Name = "User I D")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 UserID { get; set; }

        [Display(Name = "User Guid")]
        [Required(ErrorMessage = "{0} is Required")]
        public Guid UserGuid { get; set; }

        [Display(Name = "First Name")]
        [StringLength(255, ErrorMessage = "Maximum length is {1}")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(255, ErrorMessage = "Maximum length is {1}")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [StringLength(256, ErrorMessage = "Maximum length is {1}")]
        public string Email { get; set; }

        [Display(Name = "Mobile")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string Mobile { get; set; }

        [Display(Name = "Phone")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string Phone { get; set; }
    }
}
