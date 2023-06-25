using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.BasicModule
{
    public class AttachmentLibrary
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 Id { get; set; }

        [Display(Name = "Reference Id")]
        public Int64? ReferenceId { get; set; }

        [Display(Name = "Attachment Type")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string AttachmentType { get; set; }

        [Display(Name = "File Name")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string FileName { get; set; }

        [Display(Name = "File Path")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string FilePath { get; set; }

        [Display(Name = "File Type")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string FileType { get; set; }

        [Display(Name = "Created On")]
        public DateTime? CreatedOn { get; set; }

        [Display(Name = "Created By")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string CreatedBy { get; set; }

        [Display(Name = "Updated On")]
        public DateTime? UpdatedOn { get; set; }

        [Display(Name = "Updated By")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string UpdatedBy { get; set; }
    }
}
