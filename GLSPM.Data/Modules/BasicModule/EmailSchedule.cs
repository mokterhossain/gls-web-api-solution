using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.BasicModule
{
   public class EmailSchedule
    {
        [Key]
        public long Id { get; set; }
        public long EmailHistoryId { get; set; }
        public decimal SendEmailBeforeTAT { get; set; }
        public decimal SendAfterAfterHourFromNow { get; set; }
        public DateTime? SpecificDate { get; set; }
    }
}
