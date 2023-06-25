using GLSPM.Data.Modules.InvoiceModule;
using GLSPM.Data.Modules.ProjectManagement;
using GLSPM.Service.Modules.InvoiceModule;
using GLSPM.Service.Modules.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GLSWebAPI.Reports_Generator.Invoice
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        public ProjectViewModel project = new ProjectViewModel();
        public ClientInvoiceViewModel clientInvoiceData = new ClientInvoiceViewModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            long projectId = 0;
            if (Request.QueryString["projectId"] != null)
            {
                projectId = Convert.ToInt64(Request.QueryString["projectId"]);
            }
            project = new ProjectService().GetByProjectIdAPI(Convert.ToInt64(projectId));
            clientInvoiceData = new ClientInvoiceService().GetInvoiceByProjectIdNew(Convert.ToInt64(projectId));
            List<ClientInvoiceViewModel> data = new List<ClientInvoiceViewModel>();
            data.Add(clientInvoiceData);
            if (clientInvoiceData != null)
            {
                if(clientInvoiceData.Id > 0)
                {
                    lvClientInvoice.DataSource = data;
                    lvClientInvoice.DataBind();
                }
            }
        }
    }
}