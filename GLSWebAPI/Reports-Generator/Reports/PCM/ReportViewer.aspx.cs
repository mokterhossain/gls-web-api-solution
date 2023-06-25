using GLSPM.Data.Modules.ProjectManagement;
using GLSPM.Service.Modules.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GLSWebAPI.Reports_Generator.Reports.PCM
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        public ProjectViewModel project = new ProjectViewModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            long projectId = 0;
            if (Request.QueryString["projectId"] != null)
            {
                projectId = Convert.ToInt64(Request.QueryString["projectId"]);
            }
            project = new ProjectService().GetByProjectId(Convert.ToInt64(projectId));
            List<ProjectSampleViewModel> projectSample = new ProjectSampleService().GetAllProjectSampleByProjectId(Convert.ToInt64(projectId));
            List<ProjectSamplePCMViewModel> projectSampleFinalList = new List<ProjectSamplePCMViewModel>();
            int count = 0;
            int rowNumber = 1;
            int groupNumber = 1;
            ProjectSamplePCMViewModel psdDup = new ProjectSamplePCMViewModel();
            foreach (ProjectSampleViewModel psv in projectSample)
            {
                int location = count + 1;
                try
                {
                    location = Convert.ToInt16(psv.LabId.Split('-')[1].ToString());
                }
                catch (Exception ex) { }
                ProjectSamplePCMViewModel psd2 = new ProjectSamplePCMViewModel();
                psd2.GroupNumber = groupNumber;
                psd2.LocationGroup = string.Format("Location: {0}, {1}", location, psv.Location);
                psd2.LabIdGroup = string.Format("Comment:{0}", psv.Note);
                rowNumber++;
                List<ProjectSamplePCMViewModel> projectSampleDetail = new PCMService().GetAllProjectSamplePCMData(psv.SampleId);
                string compositeNonAsbestosContentsText = string.Empty;
                int sampleCount = 0;
                foreach (ProjectSamplePCMViewModel psd in projectSampleDetail)
                {
                    if (psd.IsDuplicate == true)
                    {
                    }
                    else
                    {
                        psd.GroupNumber = groupNumber;
                        psd.LocationGroup = string.Format("Location: {0}, {1}", location, psv.Location);
                        psd.LabIdGroup = string.Format("Comment:{0}", psv.Note);
                        psd.Serial = (int)psv.SerialNo;
                        projectSampleFinalList.Add(psd);
                        rowNumber++;
                    }
                }
                if ((count + 1) % 3 == 0)
                {
                    groupNumber++;
                }
                count++;
            }
            lvPCM.DataSource = projectSampleFinalList;
            lvPCM.DataBind();
        }
        /*protected void lvPCM_DataBound(object sender, EventArgs e)
        {
            PlaceHolder plc = (PlaceHolder)(sender as ListView).FindControl("itemPlaceHolderFooter");
            Literal ltrl = new Literal();
            StringBuilder footerHtml = new StringBuilder();
            string labAnalystSignUrl = "http://192.168.23.10/gls/" + project.AnalystSignature.Replace("~/", "");
            string labrotaryManagerDiploma = "";
            string projectAnalystDiploma = "";
            string labrotaryManagerSignUrl = "http://192.168.23.10/gls/" + project.LabratoryManagerSignature.Replace("~/", "");
            string projectAnalystName = project.ProjectAnalystName;
            string labrotaryManagerName = project.LabratoryManagerName;
            if (!string.IsNullOrEmpty(project.LabratoryManagerDiploma))
                labrotaryManagerDiploma = ", " + project.LabratoryManagerDiploma;
            if (!string.IsNullOrEmpty(project.ProjectAnalystDiploma))
                projectAnalystDiploma = ", " + project.ProjectAnalystDiploma;
            string SampledBy = (!string.IsNullOrEmpty(project.SampledBy) ? "Sampled By: " + project.SampledBy : "");
            footerHtml.AppendLine("<tr>");
            footerHtml.AppendLine("<td colspan=\"4\" style=\"width:50%; padding-left:10px; text-align:left;border: none;padding-right:30px;height:90px;\">");
            footerHtml.AppendLine(string.Format("<div style=\"border-bottom:solid 2px #cccccc;height:90px;\"><img src=\"{0}\" /></div>", labAnalystSignUrl));
            footerHtml.AppendLine(string.Format("<div>{0}</div>", projectAnalystName));
            footerHtml.AppendLine(string.Format("<div>Lab Analyst {0}</div>", projectAnalystDiploma));
            footerHtml.AppendLine("</td>");
            footerHtml.AppendLine("<td colspan=\"4\" style=\"width:50%; padding-left:10px; text-align:left;border: none;vertical-align:bottom;padding-left:30px;height:90px;\">");
            footerHtml.AppendLine(string.Format("<div style=\"border-bottom:solid 2px #cccccc;height:90px; vertical-align:bottom;\"><img src=\"{0}\" /></div>", labrotaryManagerSignUrl));
            footerHtml.AppendLine(string.Format("<div>{0}</div>", labrotaryManagerName));
            footerHtml.AppendLine(string.Format("<div>Laboratory Manager {0}</div>", labrotaryManagerDiploma));
            footerHtml.AppendLine("</td>");
            footerHtml.AppendLine("</tr>");
            footerHtml.AppendLine("<tr>");
            footerHtml.AppendLine("<td class=\"footer-bg\" colspan=\"8\" style=\"width:100%; padding-left:10px; text-align:left;border-radius: 25px;border: solid 2px #cccccc;\">");
            footerHtml.AppendLine("<div style=\"padding:2px;text-align:justify;font-weight:bold;\">• Interpretation is left to the company and/or persons who conducted the field work.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;• Field blank, if submitted with the project, has been used to correct the data. • Reporting limit is calculated using a minimum detection limit of 7 fibers / mm2. • Upper 95 % Confidence Limit, calculated using a relative standard deviation value of 0.40. • A \"Version\" greater than 1 indicates amended data.</div>");
            footerHtml.AppendLine("</td>");
            footerHtml.AppendLine("</tr>");
            footerHtml.AppendLine("<tr>");
            footerHtml.AppendLine("<td colspan=\"8\" style=\"width: 100%;text-align: right;border:none;\">");
            footerHtml.AppendLine("<div style=\"text-align: right;\">" + SampledBy + "</div>");
            footerHtml.AppendLine("</td>");
            footerHtml.AppendLine("</tr>");
            ltrl.Text = footerHtml.ToString();
            plc.Controls.Add(ltrl);
        }*/
    }
}