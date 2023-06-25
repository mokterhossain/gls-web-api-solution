using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.FinancialModule
{
    public class SampleSubmitSummary
    {
        public string ContactPerson { get; set; }
        public int TotalSample { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalPLMSample { get; set; }
        public int TotalPCMSample { get; set; }
        public string OfficePhone { get; set; }
    }
}
