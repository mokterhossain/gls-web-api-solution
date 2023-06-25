using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.InventoryModule
{
    [Table("INVENTORY_SupplyType")]
    public class InventorySupplyType
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public int Id { get; set; }

        [Display(Name = "Supply Category")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string SupplyCategory { get; set; }

        [Display(Name = "Name")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string Name { get; set; }
    }
}
