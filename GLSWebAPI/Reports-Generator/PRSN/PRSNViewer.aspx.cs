using GLSPM.Data.Modules.ProjectManagement;
using GLSPM.Service.Modules.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GLSWebAPI.Reports_Generator.PRSN
{
    public partial class PRSNViewer : System.Web.UI.Page
    {
        public ProjectViewModel project = new ProjectViewModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            long projectId = 0;
            long sampleId = 0;
            if (Request.QueryString["projectId"] != null)
            {
                projectId = Convert.ToInt64(Request.QueryString["projectId"]);
            }
            project = new ProjectService().GetByProjectId(Convert.ToInt64(projectId));
            List<ProjectSampleViewModel> projectSample = new ProjectSampleService().GetAllProjectSampleByProjectId(Convert.ToInt64(projectId));
            if(project != null)
            {
                //tdClientName.InnerHtml = project.ClientName;
            }
            lvSample.DataSource = projectSample;
            lvSample.DataBind();
        }
        public class PrsnViewModel
        {
            ProjectViewModel Project { get; set; }
            List<ProjectSampleViewModel> ProjectSample { get; set; }
        }
    }
}