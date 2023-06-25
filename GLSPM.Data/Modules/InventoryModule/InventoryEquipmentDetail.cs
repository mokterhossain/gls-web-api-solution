using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.InventoryModule
{
    [Table("INVENTORY_EquipmentDetails")]
    public class InventoryEquipmentDetail
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public int Id { get; set; }

        [Display(Name = "Equipment RefId")]
        public int? EquipmentRefId { get; set; }

        [Display(Name = "Date Of Purchase")]
        public DateTime? DateOfPurchase { get; set; }

        [Display(Name = "Date Of Service")]
        public DateTime? DateOfService { get; set; }

        [Display(Name = "Date Of Maintenance")]
        public DateTime? DateOfMaintenance { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Performed By")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string PerformedBy { get; set; }

        [Display(Name = "Date Checked")]
        public DateTime? DateChecked { get; set; }

        [Display(Name = "Checked By")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string CheckedBy { get; set; }

        [Display(Name = "Next Maintenance Date")]
        public DateTime? NextMaintenanceDate { get; set; }

        [Display(Name = "Validation Due")]
        public DateTime? ValidationDue { get; set; }

        [Display(Name = "Cost")]
        public decimal? Cost { get; set; }

        [Display(Name = "Location")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string Location { get; set; }

        [Display(Name = "Work Instruction")]
        [StringLength(500, ErrorMessage = "Maximum length is {1}")]
        public string WorkInstruction { get; set; }

        [Display(Name = "Disposal Date")]
        public DateTime? DisposalDate { get; set; }

        [Display(Name = "Model Number")]
        [StringLength(250, ErrorMessage = "Maximum length is {1}")]
        public string ModelNumber { get; set; }

        [Display(Name = "Serial Number")]
        [StringLength(250, ErrorMessage = "Maximum length is {1}")]
        public string SerialNumber { get; set; }

        [Display(Name = "Manufacturer")]
        [StringLength(250, ErrorMessage = "Maximum length is {1}")]
        public string Manufacturer { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Person Responsible")]
        [StringLength(250, ErrorMessage = "Maximum length is {1}")]
        public string PersonResponsible { get; set; }

        [Display(Name = "Calibaration")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Calibaration { get; set; }

        [Display(Name = "Discarded")]
        public bool? Discarded { get; set; }
    }
}
