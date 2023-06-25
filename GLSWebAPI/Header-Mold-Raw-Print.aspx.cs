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
    public partial class Header_Mold_Raw_Print : System.Web.UI.Page
    {
        public ProjectViewModel project = new ProjectViewModel();
        public string reportAlso = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            long projectId = 0;
            if (Request.QueryString["projectId"] != null)
            {
                projectId = Convert.ToInt64(Request.QueryString["projectId"]);
            }

            project = new ProjectService().GetByProjectId2(projectId);
            if (!string.IsNullOrEmpty(project.ReportAlsoStr))
                reportAlso = "," + project.ReportAlsoStr;
            if (project.ProjectType == "PLM")
            {
                //dvPlmTitle1.Style.Add("display", "block");
                //dvPlmTitle2.Style.Add("display", "block");
                dvPlmTitle1.Visible = true;
                dvPlmTitle2.Visible = true;
            }
            else if (project.ProjectType == "PCM")
            {
                dvPlmTitle1.Visible = false;
                dvPlmTitle2.Visible = false;
                //dvPlmTitle1.Style.Add("display", "none");
                //dvPlmTitle2.Style.Add("display", "none");
            }
            else if (project.ProjectType == "Mold")
            {
                dvPlmTitle1.Visible = false;
                dvPlmTitle2.Visible = false;
                trDateAnalyzed.Visible = false;
                //dvPlmTitle1.Style.Add("display", "none");
                //dvPlmTitle2.Style.Add("display", "none");
            }
            else if (project.ProjectType == "Mold Tape Lift")
            {
                dvPlmTitle1.Visible = false;
                dvPlmTitle2.Visible = false;
                trDateAnalyzed.Visible = false;
                //dvPlmTitle1.Style.Add("display", "none");
                //dvPlmTitle2.Style.Add("display", "none");
            }
        }
    }
}