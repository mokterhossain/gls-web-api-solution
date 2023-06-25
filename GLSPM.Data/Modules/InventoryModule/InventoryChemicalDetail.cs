using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.InventoryModule
{
    [Table("INVENTORY_ChemicalDetails")]
    public class InventoryChemicalDetail
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public int Id { get; set; }

        [Display(Name = "Chemical Ref Id")]
        public int? ChemicalRefId { get; set; }

        //[Display(Name = "Chemical Name")]
        //[StringLength(500, ErrorMessage = "Maximum length is {1}")]
        //public string ChemicalName { get; set; }

        [Display(Name = "Principal")]
        [StringLength(250, ErrorMessage = "Maximum length is {1}")]
        public string Principal { get; set; }

        [Display(Name = "Container Size")]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string ContainerSize { get; set; }

        [Display(Name = "Container Type")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string ContainerType { get; set; }

        [Display(Name = "C A S Number")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string CASNumber { get; set; }

        [Display(Name = "Manufacturer")]
        [StringLength(250, ErrorMessage = "Maximum length is {1}")]
        public string Manufacturer { get; set; }

        [Display(Name = "Date Of Purchase")]
        public DateTime? DateOfPurchase { get; set; }

        [Display(Name = "Expiry Date")]
        public DateTime? ExpiryDate { get; set; }

        [Display(Name = "Batch Number")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string BatchNumber { get; set; }

        [Display(Name = "Room")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Room { get; set; }

        [Display(Name = "Storage")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string Storage { get; set; }

        [Display(Name = "Lab Chemical Id")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string LabChemicalId { get; set; }

        [Display(Name = "Serial Number")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string SerialNumber { get; set; }

        [Display(Name = "Finished")]
        public bool? Finished { get; set; }

        [Display(Name = "M S D S")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        public string MSDS { get; set; }

        [Display(Name = "Prefix Serial")]
        public int? PrefixSerial { get; set; }
    }
}
