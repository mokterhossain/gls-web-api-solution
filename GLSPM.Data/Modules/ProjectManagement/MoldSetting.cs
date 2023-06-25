using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    public class MoldSetting
    {
        [Key]
        public int Id { get; set; }
        public double MicroscopeFieldDiameter { get; set; }
        public double TraverseNumber { get; set; }
        public double Volume { get; set; }
        public double FungalCount { get; set; }
    }
}
