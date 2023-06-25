using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.InvoiceModule
{
    [NotMapped]
    public class ClientInvoiceViewModel : ClientInvoice
    {
        public string ClientName { get; set; }
        public string ClientCompanyName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string JobNumber { get; set; }
        public int CustomerID { get; set; }
        public string ProjectNumber { get; set; }
        public string AccountsManagerSignature { get; set; }
        public string AccountsManagerName { get; set; }
        public string AccountsManagerDiploma { get; set; }

        public List<ClientInvoiceDetail> ClientInvoiceDetails { get; set; }
    }
}
