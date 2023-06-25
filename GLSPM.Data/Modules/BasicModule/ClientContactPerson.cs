using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.BasicModule
{
    public class ClientContactPerson
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public int Id { get; set; }

        [Display(Name = "Client Id")]
        public int? ClientId { get; set; }

        [Display(Name = "Contact Person")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string ContactPerson { get; set; }

        [Display(Name = "Cell No")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string CellNo { get; set; }

        [Display(Name = "Office Phone")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string OfficePhone { get; set; }

        [Display(Name = "Email")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Email { get; set; }

    }
}
