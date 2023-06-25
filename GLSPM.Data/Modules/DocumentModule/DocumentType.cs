using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.DocumentModule
{
    public class DocumentType
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public int Id { get; set; }

        [Display(Name = "Type Name")]
        [StringLength(250, ErrorMessage = "Maximum length is {1}")]
        public string TypeName { get; set; }
    }
}
