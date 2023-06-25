using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.BasicModule
{
    public class InvoiceDetails
    {
        public string Description { get; set; }
        public int TotalHour { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
    }
}
