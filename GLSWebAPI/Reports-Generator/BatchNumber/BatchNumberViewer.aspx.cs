using GLSPM.Data.Modules.ProjectManagement;
using GLSPM.Service.Modules.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GLSWebAPI.Reports_Generator.BatchNumber
{
    public partial class BatchNumberViewer : System.Web.UI.Page
    {
        public List<ProjectSampleViewModel> projectSample = new List<ProjectSampleViewModel>();
        protected void Page_Load(object sender, EventArgs e)
        {
            string batchNumber = "";
            if (Request.QueryString["batchNumber"] != null)
            {
                batchNumber = Request.QueryString["batchNumber"];
            }
            BatchNumberRecord batchNumberRecord = new BatchNumberRecordService().GetByBatchNumber(batchNumber);
            projectSample = new ProjectSampleService().GetAllProjectSampleByBatchNumber(batchNumber);
            if(projectSample != null)
            {
                lvSample.DataSource = projectSample;
                lvSample.DataBind();
            }
        }
    }
}