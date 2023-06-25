using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    public class BatchNumberRecord
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 Id { get; set; }

        [Display(Name = "Batch Number")]
        [StringLength(250, ErrorMessage = "Maximum length is {1}")]
        public string BatchNumber { get; set; }

        [Display(Name = "Date Of Batch")]
        public DateTime? DateOfBatch { get; set; }
    }
}
