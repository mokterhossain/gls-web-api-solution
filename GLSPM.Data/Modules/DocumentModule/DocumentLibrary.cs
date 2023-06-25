using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.DocumentModule
{
    public class DocumentLibrary
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 Id { get; set; }

        [Display(Name = "Doucument Type Id")]
        public int? DocumentTypeId { get; set; }

        [Display(Name = "Document Type")]
        [StringLength(250, ErrorMessage = "Maximum length is {1}")]
        public string DocumentType { get; set; }

        [Display(Name = "Title")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string Title { get; set; }

        [Display(Name = "Document U R L")]
        [StringLength(1000, ErrorMessage = "Maximum length is {1}")]
        public string DocumentURL { get; set; }

        [Display(Name = "File Type")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string FileType { get; set; }

        public bool? IsObsolete { get; set; }

        public string Revision { get; set; }

        public DateTime? EffectiveDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }

        public int? FolderId { get; set; }
        public string DocumentID { get; set; }

        public bool? CanDelete { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
