using GLSPM.Data.Modules.ProjectManagement;
using GLSPM.Service.Modules.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GLSWebAPI.Reports_Generator.Labels
{
    public partial class LabelViewer : System.Web.UI.Page
    {
        public List<ProjectSampleViewModel> projectSamples = new List<ProjectSampleViewModel>();
        public int totalSample = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string projectIds = "95386";
            long sampleId = 0;
            if (Request.QueryString["projectIds"] != null)
            {
                projectIds = Request.QueryString["projectIds"];
            }
            if (Request.QueryString["sampleId"] != null)
            {
                sampleId = Convert.ToInt64(Request.QueryString["sampleId"]);
            }
            projectIds = projectIds.Trim(',');
            projectSamples = new ProjectSampleService().GetAllProjectSampleByProjectIdsNew(projectIds);
            if(sampleId > 0)
            {
                projectSamples = projectSamples.Where(ps => ps.SampleId == sampleId).ToList();
                
            }
            totalSample = projectSamples.Count();
            lvLabel.DataSource = projectSamples;
            lvLabel.DataBind();
            // string test = Request.QueryString.Get("projectId");
        }
        
    }
}