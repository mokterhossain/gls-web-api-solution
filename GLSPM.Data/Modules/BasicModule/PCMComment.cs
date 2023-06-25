using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.BasicModule
{
    public class PCMComment
    {
        [Key]
        public int Id { get; set; }
        public string Comment { get; set; }
    }
}
