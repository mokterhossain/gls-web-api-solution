using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.UserSettings
{
    public class UserInRole
    {
        [Key]
        [Display(Name = "User In Role I D")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 UserInRoleID { get; set; }

        [Display(Name = "User Role I D")]
        public int? UserRoleID { get; set; }

        [Display(Name = "User I D")]
        public Int64? UserID { get; set; }

        [Display(Name = "User Guid")]
        public Guid? UserGuid { get; set; }
    }
}
