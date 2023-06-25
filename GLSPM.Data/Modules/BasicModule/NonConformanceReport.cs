using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.BasicModule
{
    public class NonConformanceReport
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public int Id { get; set; }

        [Display(Name = "N C R N")]
        public int? NCRN { get; set; }

        [Display(Name = "Analyst")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Analyst { get; set; }

        [Display(Name = "Section")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Section { get; set; }

        [Display(Name = "Date")]
        public DateTime? Date { get; set; }

        [Display(Name = "Type")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Type { get; set; }

        [Display(Name = "Description")]
        [StringLength(1000, ErrorMessage = "Maximum length is {1}")]
        public string Description { get; set; }

        [Display(Name = "Other")]
        [StringLength(1000, ErrorMessage = "Maximum length is {1}")]
        public string Other { get; set; }

        [Display(Name = "Batches Affected")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string BatchesAffected { get; set; }

        [Display(Name = "Batch1")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Batch1 { get; set; }

        [Display(Name = "Batch2")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Batch2 { get; set; }

        [Display(Name = "Batch3")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Batch3 { get; set; }

        [Display(Name = "Batch4")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Batch4 { get; set; }

        [Display(Name = "Batch5")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Batch5 { get; set; }

        [Display(Name = "Equipment Affected")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string EquipmentAffected { get; set; }

        [Display(Name = "Inv1")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Inv1 { get; set; }

        [Display(Name = "Inv2")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Inv2 { get; set; }

        [Display(Name = "Inv3")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Inv3 { get; set; }

        [Display(Name = "Inv4")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Inv4 { get; set; }

        [Display(Name = "Inv5")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Inv5 { get; set; }

        [Display(Name = "N C R Rating")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string NCRRating { get; set; }

        [Display(Name = "Corrective Action Required")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string CorrectiveActionRequired { get; set; }

        [Display(Name = "Immediate Actions")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string ImmediateActions { get; set; }

        [Display(Name = "Other Actions")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string OtherActions { get; set; }

        [Display(Name = "Root Cause")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string RootCause { get; set; }

        [Display(Name = "Possible C A1")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string PossibleCA1 { get; set; }

        [Display(Name = "Possible C A2")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string PossibleCA2 { get; set; }

        [Display(Name = "Possible C A3")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string PossibleCA3 { get; set; }

        [Display(Name = "Selected C A")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string SelectedCA { get; set; }

        [Display(Name = "Monitoring Plan")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string MonitoringPlan { get; set; }

        [Display(Name = "Q A O Comments")]
        [StringLength(1000, ErrorMessage = "Maximum length is {1}")]
        public string QAOComments { get; set; }

        [Display(Name = "Q A O Signature")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string QAOSignature { get; set; }

        [Display(Name = "Reference")]
        [StringLength(1000, ErrorMessage = "Maximum length is {1}")]
        public string Reference { get; set; }
    }
}
