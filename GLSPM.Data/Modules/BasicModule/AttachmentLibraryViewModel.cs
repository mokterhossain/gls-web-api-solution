using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data.Modules.BasicModule
{
    [NotMapped]
    public class AttachmentLibraryViewModel : AttachmentLibrary
    {
        public string FilePathCustom
        {
            get
            {
                return "file:" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.FilePath.Replace("~/", "")) ;
            }
        }
        public string TransactionTitle { get; set; }
    }
}
