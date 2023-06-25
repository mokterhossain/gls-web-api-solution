using GLSPM.Data.Modules.ProjectManagement;
using GLSPM.Service.Modules.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GLSWebAPI
{
    public partial class Header_Print_Batch : System.Web.UI.Page
    {
        public BatchNumberRecord batchNumberRecord = new BatchNumberRecord();
        public string dateOfBatch = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string batchNumber = "";
            if (Request.QueryString["batchNumber"] != null)
            {
                batchNumber = Request.QueryString["batchNumber"];
            }
            batchNumberRecord = new BatchNumberRecordService().GetByBatchNumber(batchNumber);
            if (batchNumberRecord != null)
            {
                if (batchNumberRecord.DateOfBatch != null)
                    dateOfBatch = Convert.ToDateTime(batchNumberRecord.DateOfBatch).ToShortDateString();
            }
        }
    }
}