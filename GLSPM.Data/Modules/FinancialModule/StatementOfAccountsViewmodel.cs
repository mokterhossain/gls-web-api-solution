using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.FinancialModule
{
    [NotMapped]
    public class StatementOfAccountsViewmodel
    {
        public string InvoiceNumber { get; set; }
        public string JobNumber { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal GST { get; set; }
        public decimal InvoiceTotal { get; set; }

        public string JobDate { get; set; }
    }
}
