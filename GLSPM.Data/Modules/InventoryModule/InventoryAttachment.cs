using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.InventoryModule
{
    [Table("INVENTORY_Attachment")]
    public class InventoryAttachment
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 Id { get; set; }

        [Display(Name = "Reference Id")]
        public int? ReferenceId { get; set; }

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
    }
}
