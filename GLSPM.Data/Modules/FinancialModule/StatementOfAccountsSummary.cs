using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.FinancialModule
{
    [NotMapped]
    public class StatementOfAccountsSummary
    {
        public int TotalSample { get; set; }
        public decimal TotalAmount { get; set; }
        public string ProjectType { get; set; }
    }
}
