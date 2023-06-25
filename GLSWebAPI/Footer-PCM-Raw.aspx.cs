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
    public partial class Footer_PCM_Raw : System.Web.UI.Page
    {
        public ProjectViewModel project = new ProjectViewModel();
        public string labAnalystSignUrl = "";
        public string labrotaryManagerDiploma = "";
        public string projectAnalystDiploma = "";
        public string labrotaryManagerSignUrl = "";
        public string projectAnalystName = "";
        public string labrotaryManagerName = "";
        public string SampledBy = "";
        public string footerNoteText = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            long projectId = 0;
            if (Request.QueryString["projectId"] != null)
            {
                projectId = Convert.ToInt64(Request.QueryString["projectId"]);
            }

            project = new ProjectService().GetByProjectId2(projectId);
            labAnalystSignUrl = "http://192.168.23.10/gls/" + project.AnalystSignature.Replace("~/", "");

            labrotaryManagerSignUrl = "http://192.168.23.10/gls/" + project.LabratoryManagerSignature.Replace("~/", "");
            projectAnalystName = project.ProjectAnalystName;
            labrotaryManagerName = project.LabratoryManagerName;
            if (!string.IsNullOrEmpty(project.LabratoryManagerDiploma))
                labrotaryManagerDiploma = ", " + project.LabratoryManagerDiploma;
            if (!string.IsNullOrEmpty(project.ProjectAnalystDiploma))
                projectAnalystDiploma = ", " + project.ProjectAnalystDiploma;
        }
    }
}