using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.UserSettings
{
    public class UserCustom
    {
        [Key]
        [Display(Name = "User I D")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 UserID { get; set; }

        [Display(Name = "User Guid")]
        [Required(ErrorMessage = "{0} is Required")]
        public Guid UserGuid { get; set; }

        [Display(Name = "User Name")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string UserName { get; set; }

        [Display(Name = "Lowered User Name")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string LoweredUserName { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Password Alt")]
        public string PasswordAlt { get; set; }

        [Display(Name = "Secrect Question")]
        public string SecrectQuestion { get; set; }

        [Display(Name = "Secrect Answer")]
        public string SecrectAnswer { get; set; }

        [Display(Name = "Password Attempt")]
        public int? PasswordAttempt { get; set; }

        [Display(Name = "Is Lock Out")]
        public bool? IsLockOut { get; set; }

        [Display(Name = "Last Login Time")]
        public DateTime? LastLoginTime { get; set; }

        [Display(Name = "Unlok Time")]
        public DateTime? UnlokTime { get; set; }

        [Display(Name = "Wrong Attempt")]
        public int? WrongAttempt { get; set; }

        [Display(Name = "Locked Time")]
        public DateTime? LockedTime { get; set; }

        public string UserFullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string RoleName { get; set; }
        public bool? IsActive { get; set; }
    }
}
