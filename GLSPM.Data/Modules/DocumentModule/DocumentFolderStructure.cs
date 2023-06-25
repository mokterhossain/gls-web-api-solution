using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.DocumentModule
{
    public class DocumentFolderStructure
    {
        [Key]        
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string FolderName { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }

        public bool? CanDelete { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
