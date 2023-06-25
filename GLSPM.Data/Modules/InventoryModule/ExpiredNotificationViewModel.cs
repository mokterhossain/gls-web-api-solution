using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.InventoryModule
{
    [NotMapped]
    public class ExpiredNotificationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int TotalDayLeft { get; set; }
        public string InventoryType { get; set; }

        public DateTime? ExpiredDate { get; set; }
    }
}
