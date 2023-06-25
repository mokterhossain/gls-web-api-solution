using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.BasicModule
{
    public class MenuDefinition
    {
        [Key]
        [Display(Name = "Menu Definition I D")]
        [Required(ErrorMessage = "{0} is Required")]
        public int MenuDefinitionID { get; set; }

        [Display(Name = "Title")]
        [StringLength(255, ErrorMessage = "Maximum length is {1}")]
        public string Title { get; set; }

        [Display(Name = "U R L")]
        [StringLength(512, ErrorMessage = "Maximum length is {1}")]
        public string URL { get; set; }

        [Display(Name = "Order Number")]
        public int? OrderNumber { get; set; }

        [Display(Name = "Parent I D")]
        public int? ParentID { get; set; }

        [Display(Name = "Tier")]
        public int? Tier { get; set; }

        [Display(Name = "Created By")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created On")]
        public DateTime? CreatedOn { get; set; }

        [Display(Name = "Updated By")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Updated On")]
        public DateTime? UpdatedOn { get; set; }

        [NotMapped]
        public string IconUrl { get; set; }
        public bool? IsActive { get; set; }
    }
}
