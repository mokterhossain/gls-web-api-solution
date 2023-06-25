using GLSPM.Data.Modules.InvoiceModule;
using GLSPM.Service.Modules.InvoiceModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GLSWebAPI
{
    public partial class InvoiceFooter : System.Web.UI.Page
    {
        public ClientInvoiceViewModel clientInvoiceData = new ClientInvoiceViewModel();
        public string accountsManagerSignUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            long projectId = 0;
            if (Request.QueryString["projectId"] != null)
            {
                projectId = Convert.ToInt64(Request.QueryString["projectId"]);
            }
            clientInvoiceData = new ClientInvoiceService().GetInvoiceByProjectIdNew(Convert.ToInt64(projectId));
            if(clientInvoiceData != null)
            {
                accountsManagerSignUrl = "http://192.168.23.10/gls/" + clientInvoiceData.AccountsManagerSignature.Replace("~/", ""); 
            }
        }
    }
}