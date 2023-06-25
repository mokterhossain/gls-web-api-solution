using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.BasicModule
{
    public class EmailAccounts
    {
        [Key]
        public int EmailAccountId { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSSL { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool IsActive { get; set; }
    }
}
