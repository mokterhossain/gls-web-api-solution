using GLSPM.Data.Modules.FinancialModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Service.Modules.FinancialModule
{
    [NotMapped]
    public class ExpenseTransactionViewModel : ExpenseTransaction
    {
        public string ExpenseType { get; set; }
    }
}
