using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.BasicModule
{
    [NotMapped]
    public class EmailHistoryViewModel : EmailHistory
    {
        public string ClientName { get; set; }
        public string ReportName { get; set; }
    }
}
