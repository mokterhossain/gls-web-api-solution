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

namespace GLSWebAPI
{
    public partial class InvoiceHeader : System.Web.UI.Page
    {
        public ProjectViewModel project = new ProjectViewModel();
        public ClientInvoiceViewModel clientInvoiceData = new ClientInvoiceViewModel();
        public string reportAlso = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            long projectId = 0;
            if (Request.QueryString["projectId"] != null)
            {
                projectId = Convert.ToInt64(Request.QueryString["projectId"]);
            }
            clientInvoiceData = new ClientInvoiceService().GetInvoiceByProjectIdNew(Convert.ToInt64(projectId));
            project = new ProjectService().GetByProjectId(Convert.ToInt64(projectId));
           /* if (project != null)
            {
                projectNumber = project.ProjectNumber;
                companyName = project.ClientName;
                phone = project.OfficePhone;
                jobNumber = project.JobNumber.Replace(',', ' ');
                client = project.ReportingPerson;
                address = project.Address;
                email = project.ClientEmail;
                labAnalystSignUrl = project.AnalystSignature.Replace("~/", "");
                labrotaryManagerSignUrl = project.LabratoryManagerSignature.Replace("~/", "");
                projectAnalystName = project.ProjectAnalystName;
                labrotaryManagerName = project.LabratoryManagerName;
            }*/

        }
    }
}