using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.InventoryModule
{
    [Table("INVENTORY_ChemicalName")]
    public class InventoryChemicalName
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
