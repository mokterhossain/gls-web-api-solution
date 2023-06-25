using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.ProjectManagement
{
    public class Project
    {
        [Key]
        [Display(Name = "Project I D")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 ProjectID { get; set; }

        [Display(Name = "Project Number")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string ProjectNumber { get; set; }

        [Display(Name = "Client I D")]
        public int? ClientID { get; set; }

        [Display(Name = "Address")]
        [StringLength(250, ErrorMessage = "Maximum length is {1}")]
        public string Address { get; set; }

        [Display(Name = "Job Number")]
        public string JobNumber { get; set; }

        [Display(Name = "Report To")]
        public int? ReportTo { get; set; }

        [Display(Name = "Cell No")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string CellNo { get; set; }

        [Display(Name = "Office Phone")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string OfficePhone { get; set; }

        [Display(Name = "Received Date")]
        public DateTime? ReceivedDate { get; set; }

        [Display(Name = "Due Date")]
        public DateTime? DueDate { get; set; }

        [Display(Name = "Client Email")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string ClientEmail { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Number Of Sample")]
        public int? NumberOfSample { get; set; }
        [Display(Name = "Date Of Reported")]
        public DateTime? DateOfReported { get; set; }

        [Display(Name = "Date Of Analyzed")]
        public DateTime? DateOfAnalyzed { get; set; }

        [Display(Name = "Project Status Id")]
        public int? ProjectStatusId { get; set; }

        [Display(Name = "Analyst Id")]
        public int? AnalystId { get; set; }

        [Display(Name = "Labratory Manager Id")]
        public int? LabratoryManagerId { get; set; }

        [Display(Name = "Project Type")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string ProjectType { get; set; }

        [Display(Name = "Sampled By")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string SampledBy { get; set; }


        [Display(Name = "Created On")]
        public DateTime? CreatedOn { get; set; }

        [Display(Name = "Created By")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string CreatedBy { get; set; }

        [Display(Name = "Updated On")]
        public DateTime? UpdatedOn { get; set; }

        [Display(Name = "Updated By")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Report To Name")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string ReportToName { get; set; }

        public string ReportAlso { get; set; }
        public string ReportAlsoStr { get; set; }

        [Display(Name = "Client Name")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string ClientName { get; set; }

        [Display(Name = "Is Amendment")]
        public bool? IsAmendment { get; set; }

        public DateTime? SamplingDate { get; set; }
    }
}
