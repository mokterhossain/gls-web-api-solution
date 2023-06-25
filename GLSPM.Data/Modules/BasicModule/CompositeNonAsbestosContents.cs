using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.BasicModule
{
    public  class CompositeNonAsbestosContents
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string Name { get; set; }
        public int? NumericValue { get; set; }
        public DateTime? UpdatedOn { get; set; }

        [NotMapped]
        public List<CompositeNonAsbestosContentsDetail> CompositeNonAsbestosContentsDetail { get; set; }


    }
}
