using GLSPM.Data.Modules.ProjectManagement;
using GLSPM.Service.Modules.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GLSWebAPI.Reports_Generator.Reports.PLM
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
            project = new ProjectService().GetByProjectIdAPI(Convert.ToInt64(projectId));
            List<ProjectSampleViewModel> projectSample = project.ProjectSample;
            lvSample.DataSource = projectSample;
            lvSample.DataBind();
        }
        /*protected void lvSample_DataBound(object sender, EventArgs e)
        {
            PlaceHolder plcHeader = (PlaceHolder)(sender as ListView).FindControl("itemPlaceHolderHeader");
            StringBuilder headerHtml = new StringBuilder();
            Literal ltrlHeader = new Literal();
            headerHtml.AppendLine("<div class=\"jobnumber-bg\" style=\"width:100 %;\"><div style=\"padding:5px;\"><b>Job Name:</b> "+ project.JobNumber+"</div></div>");
            headerHtml.AppendLine("<div style=\"text-align:center; font-weight: bold; width: 100%; margin-top: 5px;\">ASBESTOS PLM REPORT:EPA-600/M4-82-020 & EPA METHOD 600/R-93-116</div>");
            headerHtml.AppendLine("<div style=\"text-align:center; width: 100%; margin-top: 5px;\"> Detection Limit: Less than 1% by area.</div>");
            ltrlHeader.Text = headerHtml.ToString();
            plcHeader.Controls.Add(ltrlHeader);


            PlaceHolder plc = (PlaceHolder)(sender as ListView).FindControl("itemPlaceHolderTest");
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
            footerHtml.AppendLine("<td style=\"width:50%; padding-left:10px; text-align:left;border: none;padding-right:30px;\">");
            footerHtml.AppendLine(string.Format("<div style=\"border-bottom:solid 2px #cccccc;height:90px;\"><img src=\"{0}\" /></div>", labAnalystSignUrl));
            footerHtml.AppendLine(string.Format("<div>{0}</div>", projectAnalystName));
            footerHtml.AppendLine(string.Format("<div>Lab Analyst {0}</div>", projectAnalystDiploma));
            footerHtml.AppendLine("</td>");
            footerHtml.AppendLine("<td style=\"width:50%; padding-left:10px; text-align:left;border: none;vertical-align:bottom;padding-left:30px;\">");
            footerHtml.AppendLine(string.Format("<div style=\"border-bottom:solid 2px #cccccc;height:60px; vertical-align:bottom;\"><img src=\"{0}\" /></div>", labrotaryManagerSignUrl));
            footerHtml.AppendLine(string.Format("<div>{0}</div>", labrotaryManagerName));
            footerHtml.AppendLine(string.Format("<div>Laboratory Manager {0}</div>", labrotaryManagerDiploma));
            footerHtml.AppendLine("</td>");
            footerHtml.AppendLine("</tr>");
            footerHtml.AppendLine("<tr>");
            footerHtml.AppendLine("<td class=\"footer-bg\" colspan=\"2\" style=\"width:100%; padding-left:10px; text-align:left;\">");
            footerHtml.AppendLine("<div style=\"padding:2px;text-align:justify;font-weight:bold;\">Test results relate to the samples, as submitted, to the Laboratory.&nbsp;&nbsp;&nbsp;•&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The report must not be used by the client to claim product clarification, approval, or authorization. by CALA, NIST, or any agency of the federal government.&nbsp;&nbsp;&nbsp;•&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;This report shall not be reproduced, except in    full, without prior approval of the laboratory. In homogeneous sample are separated into homogeneous subsamples and analyze individually •When amounts of interfering materials indicate that the sample should be analyzed by gravimetric point count, the minimum detection and reporting limit will be less than 1%, unless point counting is performed.&nbsp;&nbsp;&nbsp;•&nbsp;&nbsp;&nbsp;MMVF - Man made vitrious fiber i.e glass fiber, MMOF- Man made organic fiber, • NPAF- Natural Polymer and animal fiber i.e Hair, Mica-Micaceous Material.</div>");
            footerHtml.AppendLine("</td>");
            footerHtml.AppendLine("</tr>");
            footerHtml.AppendLine("<tr>");
            footerHtml.AppendLine("<td colspan=\"2\" style=\"width: 100%;text-align: right;border:none;\">");
            footerHtml.AppendLine("<div style=\"text-align: right;\">" + SampledBy + "</div>");
            footerHtml.AppendLine("</td>");
            footerHtml.AppendLine("</tr>");
            ltrl.Text = footerHtml.ToString();
            plc.Controls.Add(ltrl);
            //HtmlTableRow trFooter = (sender as ListView).FindControl("trFooter") as HtmlTableRow;
            //trFooter.InnerHtml = string.Format("<td>{0}</td>", project.ProjectNumber);

            //(sender as ListView).FindControl("trFooter").DataBind();
        }*/
        protected void lvGoalsInner_DataBound(object sender, EventArgs e)
        {
            (sender as ListView).FindControl("ltTitle").DataBind();
            Literal lblTest = (sender as ListView).FindControl("ltTitle") as Literal;
            string testb = lblTest.Text;
            
            (sender as ListView).FindControl("ltCompositeNonAsbestosContentsText").DataBind();
            (sender as ListView).FindControl("hdnNote").DataBind();
            HiddenField ltNote = (sender as ListView).FindControl("hdnNote") as HiddenField;
            string noteText = ltNote.Value.Trim();
            PlaceHolder plc = (PlaceHolder)(sender as ListView).FindControl("notePlaceHolder");
            Literal ltrl = new Literal();
            StringBuilder noteHtml = new StringBuilder();
            if (noteText.ToString() == "")
            {
                //HtmlTableRow tableRow = (sender as ListView).FindControl("trNote") as HtmlTableRow;
                //tableRow.Style["display"] = "none";
                //(sender as ListView).FindControl("trNote").DataBind();

                //ltrl.Text = noteText;
                //plc.Controls.Add(ltrl);
            }
            else
            {
                noteHtml.AppendLine(string.Format("<tr style=\"background-color: yellow;\"><td colspan=\"3\" style=\"text-align: left;font-style:italic; font-size:16px;'\">Note: {0}</td></tr>", noteText));
                ltrl.Text = noteHtml.ToString();
                plc.Controls.Add(ltrl);

                //HtmlTableRow tableRow = (sender as ListView).FindControl("trNote") as HtmlTableRow;
                //tableRow.Style["display"] = "inline-block";
                //(sender as ListView).FindControl("trNote").DataBind();
            }
            
        }
    }

}