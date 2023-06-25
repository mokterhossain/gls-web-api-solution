using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    [NotMapped]
    public class AmendmentProjectSampleViewModel : AmendmentProjectSample
    {
        public string TurnAroundTime { get; set; }
        //public string AnalystName { get; set; }
        public string ProjectNumber { get; set; }

        public int TotalType { get; set; }

        public DateTime? DueDate { get; set; }

        public string QCStr { get; set; }

        public string QCStrTag { get; set; }

        public long Attribute1Id { get; set; }
        public string Attribute1Name { get; set; }
        public long Attribute2Id { get; set; }
        public string Attribute2Name { get; set; }
        public long Attribute3Id { get; set; }
        public string Attribute3Name { get; set; }
        public long Attribute4Id { get; set; }
        public string Attribute4Name { get; set; }
        public long Attribute5Id { get; set; }
        public string Attribute5Name { get; set; }
        public long Attribute6Id { get; set; }
        public string Attribute6Name { get; set; }
        public long Attribute7Id { get; set; }
        public string Attribute7Name { get; set; }
        public long Attribute8Id { get; set; }
        public string Attribute8Name { get; set; }
        public long Attribute9Id { get; set; }
        public string Attribute9Name { get; set; }
        public long Attribute10Id { get; set; }
        public string Attribute10Name { get; set; }

        public string QCStatus { get; set; }
    }
}
