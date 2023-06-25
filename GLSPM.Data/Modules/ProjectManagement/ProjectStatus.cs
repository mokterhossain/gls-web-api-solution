using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    public class ProjectStatus
    {
        [Key]
        [Display(Name = "Status Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public int StatusId { get; set; }

        [Display(Name = "Status Name")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string StatusName { get; set; }

        [Display(Name = "Serial No")]
        public int? SerialNo { get; set; }

        [Display(Name = "Is Active")]
        public bool? IsActive { get; set; }
    }
}
