using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.FinancialModule
{
    [NotMapped]
    public class IncomeStatementViewModel
    {
        public string StatementGroup { get; set; }
        public string HeadTitle { get; set; }
        public decimal TotalAmount { get; set; }
        public int Position { get; set; }

        public decimal GroupTotalAmount { get; set; }
        public int RowNumber { get; set; }

        public int TotalRow { get; set; }
    }
}
