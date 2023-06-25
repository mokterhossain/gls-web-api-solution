using GLSPM.Data.Modules.ProjectManagement;
using GLSPM.Service.Modules.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GLSWebAPI
{
    public partial class AsbestosHeader : System.Web.UI.Page
    {
        public ProjectViewModel projectData = new ProjectViewModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            long projectId = 0;
            string needSecondPage = "No";
            if (Request.QueryString["projectId"] != null)
            {
                projectId = Convert.ToInt64(Request.QueryString["projectId"]);
            }
            if (Request.QueryString["needSecondPage"] != null)
            {
                needSecondPage = Request.QueryString["needSecondPage"];
            }
            //projectData = new ProjectService().GetByProjectIdAPI(projectId);
            StringBuilder asbestosReport = new StringBuilder();
            asbestosReport.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"border: solid 1px #000000; \">");

            asbestosReport.AppendLine("<tr>");
            asbestosReport.AppendLine("<td rowspan=\"4\" style=\"width: 10%; text-align:center;\">");
            asbestosReport.AppendLine("<img src=\"img/logo2.png\" style=\"width:100px\" />");
            asbestosReport.AppendLine("</td>");
            asbestosReport.AppendLine("</tr>");
            asbestosReport.AppendLine("<tr>");
            asbestosReport.AppendLine("<td colspan=\"2\" style=\"background-color:#6A5ACD; text-align: center; color: #ffffff\">");
            asbestosReport.AppendLine("METHODS");
            asbestosReport.AppendLine("</td>");
            asbestosReport.AppendLine("<td colspan=\"6\" style=\"background-color:#6A5ACD; text-align: center; color: #ffffff\">");
            asbestosReport.AppendLine("ROUTINE TEST METHOD");
            asbestosReport.AppendLine("</td>");
            asbestosReport.AppendLine("</tr>");
            asbestosReport.AppendLine("<tr>");
            asbestosReport.AppendLine("<td colspan=\"8\" style=\"text-align:center;\">");
            asbestosReport.AppendLine("BULK ASBESTOS BY POLARIZED MICROSCOPY -Appendix 1");
            asbestosReport.AppendLine("</td>");
            asbestosReport.AppendLine("</tr>");

            asbestosReport.AppendLine("<tr>");
            asbestosReport.AppendLine("<td style=\"text-align:right\">DocumentID:");
            asbestosReport.AppendLine("</td>");
            asbestosReport.AppendLine("<td>PLM-ASB-A ");
            asbestosReport.AppendLine("</td>");
            asbestosReport.AppendLine("<td style=\"text-align:right\">Revision:");
            asbestosReport.AppendLine("</td>");
            asbestosReport.AppendLine("<td>06");
            asbestosReport.AppendLine("</td>");
            asbestosReport.AppendLine("<td style=\"text-align:right\">Effective Date:");
            asbestosReport.AppendLine("</td>");
            asbestosReport.AppendLine("<td>20221128");
            asbestosReport.AppendLine("</td>");
            asbestosReport.AppendLine("<td style=\"text-align:right\">Page:");
            asbestosReport.AppendLine("</td>");
            asbestosReport.AppendLine("<td>1 of 2");
            asbestosReport.AppendLine("</td>");
            asbestosReport.AppendLine("</tr>");
            
            asbestosReport.AppendLine("</table>");
            asbestosReport.AppendLine("<div style=\"width:100%;text-align:right;\">ASBESTOS ANALYSIS BATCH RECORD</div>");
            divAsbestosHeader.InnerHtml = asbestosReport.ToString();
        }
    }
}