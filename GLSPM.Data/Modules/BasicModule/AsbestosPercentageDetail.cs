using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.BasicModule
{
    public class AsbestosPercentageDetail
    {
        [Key]
        public int Id { get; set; }

        public int AsbestosPercentageId { get; set; }

        public string Value { get; set; }

        public string FiberId { get; set; }
        public string fiber_morphology { get; set; }

        public string color { get; set; }

        public string pleo { get; set; }

        public string nd_t_corr { get; set; }

        public string bifring { get; set; }

        public string extinction { get; set; }

        public string elong { get; set; }

        public string ds_color { get; set; }

        public string ri_liquid { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
