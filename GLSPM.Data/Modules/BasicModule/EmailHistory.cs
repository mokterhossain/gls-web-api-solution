using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.BasicModule
{
    public class EmailHistory
    {
        [Key]
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public string EmailTo { get; set; }
        public string EmailCC { get; set; }
        public string EmailBCC { get; set; }
        public string EmailBody { get; set; }
        public bool IsSent { get; set; }
        public DateTime SentDate { get; set; }
        public string SentBy { get; set; }
        public bool IsSchedule { get; set; }

        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
