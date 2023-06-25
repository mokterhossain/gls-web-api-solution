using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.UserSettings
{
    public class UserLoginHistoryLog
    {
        [Key]
        [Display(Name = "User Login I D")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 UserLoginID { get; set; }

        [Display(Name = "User I D")]
        public Int64? UserID { get; set; }

        [Display(Name = "User Last Login Time")]
        public DateTime? UserLastLoginTime { get; set; }

        [Display(Name = "User Last Logout Time")]
        public DateTime? UserLastLogoutTime { get; set; }

        [Display(Name = "User I P")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string UserIP { get; set; }

        [Display(Name = "User Latitude")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string UserLatitude { get; set; }

        [Display(Name = "User Longitude")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string UserLongitude { get; set; }

        [Display(Name = "User Device Name")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string UserDeviceName { get; set; }

        [Display(Name = "User O S Name")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string UserOSName { get; set; }

        [Display(Name = "User Browser Name")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string UserBrowserName { get; set; }

        [Display(Name = "User Address")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string UserAddress { get; set; }

        [Display(Name = "User City")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string UserCity { get; set; }

        [Display(Name = "User Country")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string UserCountry { get; set; }
    }
}
