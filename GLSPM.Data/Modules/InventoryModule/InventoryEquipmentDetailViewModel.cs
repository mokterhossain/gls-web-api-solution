using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.InventoryModule
{
    [NotMapped]
    public class InventoryEquipmentDetailViewModel : InventoryEquipmentDetail
    {
        public string EquipmentName { get; set; }
    }
}
