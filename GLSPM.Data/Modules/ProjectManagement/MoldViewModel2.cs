using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    public class MoldViewModel2
    {
        public long id { get; set; }
        public long ProjectId { get; set; }
        public List<MoldSample> MoldSamples { get; set; }
    }
}
