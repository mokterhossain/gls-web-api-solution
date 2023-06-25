using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.InventoryModule
{
    [Table("INVENTORY_GeneralLabSupply")]
    public class InventoryGeneralLabSupply
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public int Id { get; set; }

        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string Description { get; set; }

        [Display(Name = "Date Of Purchase")]
        public DateTime? DateOfPurchase { get; set; }

        [Display(Name = "Vendor")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string Vendor { get; set; }

        [Display(Name = "Count")]
        public int? Count { get; set; }

        [Display(Name = "Cost")]
        public decimal? Cost { get; set; }

        [Display(Name = "Finished")]
        public bool? Finished { get; set; }
        [Display(Name = "Supply Type")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string SupplyType { get; set; }

        [Display(Name = "Supply Type Id")]
        public int? SupplyTypeId { get; set; }

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
