﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ReportModule
{
    [Table("soot_concentration_cve_percentage")]
    public class SootConcentrationCvePercentage
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }
    }
}
