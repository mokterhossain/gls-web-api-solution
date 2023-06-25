using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    public class MoldSporeType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsKeySpore { get; set; }
        public bool? IsAdditional { get; set; }
        public bool? IsStringValue { get; set; }
        public bool? IsMoldTapeLift { get; set; }
        public int SerialNo { get; set; }
    }
}
