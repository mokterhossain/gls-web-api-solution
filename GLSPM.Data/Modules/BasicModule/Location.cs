using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.BasicModule
{
    [Table("tblLocation")]
    public class Location
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 Id { get; set; }

        [Display(Name = "Location Name")]
        [StringLength(1000, ErrorMessage = "Maximum length is {1}")]
        public string LocationName { get; set; }
    }
}
