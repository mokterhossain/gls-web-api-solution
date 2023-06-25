using GLSPM.Data.Modules.BasicModule;
using GLSPM.Data.Modules.ProjectManagement;
using GLSPM.Service.Modules.BasicModule;
using GLSPM.Service.Modules.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GLSWebAPI.Reports_Generator.Reports.Asbestos
{
    public partial class ReportViewer : System.Web.UI.Page
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
            if(Request.QueryString["needSecondPage"] != null)
            {
                needSecondPage = Request.QueryString["needSecondPage"];
            }
            projectData = new ProjectService().GetByProjectIdAPI(projectId);
            string asbestosReport = generateAsbestosReport(projectData, needSecondPage);
            divAsbestosReport.InnerHtml = asbestosReport;
        }
        public string generateAsbestosReport(ProjectViewModel projectData, string needSecondPage)
        {
            StringBuilder asbestosReport = new StringBuilder();
            int counter = 1;
            foreach (ProjectSampleViewModel projectSample in projectData.ProjectSample)
            {
                if (projectData.ProjectSample.Count == counter)
                {
                    asbestosReport.AppendLine("<div>");
                }
                else
                {
                    asbestosReport.AppendLine("<div style=\"page-break-after: always;\">");
                }

                #region Sample Info
                asbestosReport.AppendLine("<div style=\"padding: 0 10px\">");
                asbestosReport.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"border: solid 1px #000000;\">");
                asbestosReport.AppendLine("<thead>");
                asbestosReport.AppendLine("<tr>");
                asbestosReport.AppendLine("<th> Lab Id");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>Batch");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>Date");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>Analyst");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>Temperature(&#8451;)");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>Microscope");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>QC");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("</tr>");
                asbestosReport.AppendLine("</thead>");
                asbestosReport.AppendLine("<tbody>");

                asbestosReport.AppendLine("<tr>");
                asbestosReport.AppendLine(string.Format("<td>{0}", projectSample.LabId));
                asbestosReport.AppendLine("</td>");
                asbestosReport.AppendLine(string.Format("<td>{0}", projectSample.BatchNumber));
                asbestosReport.AppendLine("</td>");
                asbestosReport.AppendLine(string.Format("<td>{0}", (projectData.DateOfAnalyzed != null ? projectData.DateOfAnalyzed.ToString().Split(' ')[0] : "")));
                asbestosReport.AppendLine("</td>");
                asbestosReport.AppendLine(string.Format("<td>{0}", projectData.ProjectAnalystName));
                asbestosReport.AppendLine("</td>");
                asbestosReport.AppendLine("<td>");
                asbestosReport.AppendLine("</td>");
                asbestosReport.AppendLine("<td>EQ0001, EQ0018");
                asbestosReport.AppendLine("</td>");
                asbestosReport.AppendLine(string.Format("<td>{0}", (projectSample.IsQC == true ? "Yes" : "No")));
                asbestosReport.AppendLine("</td>");
                asbestosReport.AppendLine("</tr>");

                asbestosReport.AppendLine("</tbody>");
                asbestosReport.AppendLine("</table>");
                asbestosReport.AppendLine("</div>");

                #endregion
                #region Layer Info

                asbestosReport.AppendLine("<div style=\"padding: 10px 10px\">");
                asbestosReport.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"border: solid 1px #000000;\">");
                asbestosReport.AppendLine("<thead>");
                asbestosReport.AppendLine("<tr>");
                asbestosReport.AppendLine("<th style=\"width:10%\"> Layer");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th style=\"width:10%\">%");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>Bulk Morphology( Fiber Type, Estimated %, Homogeneity)");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("</tr>");
                asbestosReport.AppendLine("</thead>");
                asbestosReport.AppendLine("<tbody>");
                int layerCounter = 1;
                foreach (ProjectSampleDetailViewModel projectSampleDetail in projectSample.ProjectSampleDetail)
                {
                    asbestosReport.AppendLine("<tr>");
                    asbestosReport.AppendLine(string.Format("<td>{0}", layerCounter));
                    asbestosReport.AppendLine("</td>");
                    asbestosReport.AppendLine(string.Format("<td style=\"width:10%\">{0}", "100"));
                    asbestosReport.AppendLine("</td>");
                    asbestosReport.AppendLine(string.Format("<td>{0}", projectSampleDetail.SampleType));
                    asbestosReport.AppendLine("</td>");
                    asbestosReport.AppendLine("</tr>");
                    layerCounter++;
                }


                asbestosReport.AppendLine("</tbody>");
                asbestosReport.AppendLine("</table>");
                asbestosReport.AppendLine("</div>");
                #endregion
                #region Sample Composite Homogeneity
                asbestosReport.AppendLine("<div style=\"padding: 5px 10px\">");
                asbestosReport.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" width=\"40%\" style=\"border: solid 1px #000000;\">");
                asbestosReport.AppendLine("<thead>");
                asbestosReport.AppendLine("<tr>");
                asbestosReport.AppendLine("<th style=\"text-align:right;\">Sample Composite Homogeneity:");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine(string.Format("<th>{0}", projectSample.SampleCompositeHomogeneityText));
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("</tr>");
                asbestosReport.AppendLine("</thead>");
                asbestosReport.AppendLine("</table>");
                asbestosReport.AppendLine("</div>");
                #endregion

                #region Asbestos
                asbestosReport.AppendLine("<div style=\"padding: 0px 10px\">");
                asbestosReport.AppendLine("<b>Asbestos </b>");
                asbestosReport.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"border: solid 1px #000000;\">");
                asbestosReport.AppendLine("<thead>");
                asbestosReport.AppendLine("<tr>");
                asbestosReport.AppendLine("<th style=\"width:10%\"> Layer");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th style=\"width:10%\">%");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>Fiber Morphology");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>Colour");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>Pleo");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>nD, T Corr");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>Bifring");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>Extinction");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>Elong");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>D.S Colour");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>R.I Liquid");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th style=\"width:30%\">Fiber Id");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("</tr>");
                asbestosReport.AppendLine("</thead>");
                asbestosReport.AppendLine("<tbody>");
                int layerCounter2 = 1;
                foreach (ProjectSampleDetailViewModel projectSampleDetail in projectSample.ProjectSampleDetail)
                {
                    List<AsbestosPercentageDetail> asbestosPercentageDetail = new AsbestosPercentageDetailService().GetByAsbestosPercentageID((int)projectSampleDetail.AbsestosPercentage);
                    int asbesotsCount = 1;
                    foreach (AsbestosPercentageDetail apd in asbestosPercentageDetail)
                    {
                        string riLiquid = "1.550";
                        if (!string.IsNullOrEmpty(apd.ri_liquid))
                        {
                            riLiquid = apd.ri_liquid;
                        }
                        asbestosReport.AppendLine("<tr>");
                        asbestosReport.AppendLine("<td style=\"width:6%\">" + (asbesotsCount == 1 ? layerCounter2.ToString() : ""));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:10%; text-align: center;\">{0}", apd.Value));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", apd.fiber_morphology));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", apd.color));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", apd.pleo));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", apd.nd_t_corr));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", apd.bifring));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", apd.extinction));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", apd.elong));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", apd.ds_color));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", riLiquid));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:30%\">{0}", apd.FiberId));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine("</tr>");
                        //if(asbestosPercentageDetail.Count > 1 && asbesotsCount != asbestosPercentageDetail.Count)
                        //{
                        //    asbestosReport.AppendLine("<tr>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("</tr>");
                        //}
                        asbesotsCount++;
                    }

                    layerCounter2++;
                }

                asbestosReport.AppendLine("</tbody>");
                asbestosReport.AppendLine("</table>");
                asbestosReport.AppendLine("</div>");
                #endregion
                #region Composite Non-Asbestos Contents
                asbestosReport.AppendLine("<div style=\"padding: 0px 10px\">");
                asbestosReport.AppendLine("<b>Composite Non-Asbestos Contents </b>");
                asbestosReport.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"border: solid 1px #000000;\">");
                asbestosReport.AppendLine("<thead>");
                asbestosReport.AppendLine("<tr>");
                asbestosReport.AppendLine("<th style=\"width:10%\"> Layer");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th style=\"width:10%\">%");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>Fiber Morphology");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>Colour");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>Pleo");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>nD, T Corr");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>Bifring");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>Extinction");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>Elong");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>D.S Colour");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th>R.I Liquid");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("<th style=\"width:30%\">Fiber Id");
                asbestosReport.AppendLine("</th>");
                asbestosReport.AppendLine("</tr>");
                asbestosReport.AppendLine("</thead>");
                asbestosReport.AppendLine("<tbody>");
                int layerCounter3 = 1;
                foreach (ProjectSampleDetailViewModel projectSampleDetail in projectSample.ProjectSampleDetail)
                {
                    List<CompositeNonAsbestosContentsDetail> compositeNonAsbestosContentsDetail = new CompositeNonAsbestosContentsDetailService().GetByAsbestosPercentageID((int)projectSampleDetail.CompositeNonAsbestosContents);
                    int nonAsbestosCount = 1;
                    foreach (CompositeNonAsbestosContentsDetail cnacd in compositeNonAsbestosContentsDetail)
                    {
                        string riLiquid = "1.550";
                        if (!string.IsNullOrEmpty(cnacd.ri_liquid))
                        {
                            riLiquid = cnacd.ri_liquid;
                        }
                        asbestosReport.AppendLine("<tr>");
                        asbestosReport.AppendLine("<td style=\"width:6%\">" + (nonAsbestosCount == 1 ? layerCounter3.ToString() : ""));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:10%; text-align: center;\">{0}", cnacd.Value));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", cnacd.fiber_morphology));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", cnacd.color));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", cnacd.pleo));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", cnacd.nd_t_corr));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", cnacd.bifring));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", cnacd.extinction));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", cnacd.elong));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", cnacd.ds_color));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", riLiquid));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:30%\">{0}", cnacd.FiberId));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine("</tr>");

                        //if (compositeNonAsbestosContentsDetail.Count > 1 && nonAsbestosCount != compositeNonAsbestosContentsDetail.Count)
                        //{
                        //    asbestosReport.AppendLine("<tr>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                        //    asbestosReport.AppendLine("</tr>");
                        //}
                        nonAsbestosCount++;
                    }


                    layerCounter3++;
                }

                asbestosReport.AppendLine("</tbody>");
                asbestosReport.AppendLine("</table>");
                asbestosReport.AppendLine("</div>");
                #endregion

                asbestosReport.AppendLine("</div>");
                #region  Second Page
                if (needSecondPage == "Yes")
                {
                    if (projectData.ProjectSample.Count == counter)
                    {
                        asbestosReport.AppendLine("<div style=\"page-break-before: always;margin-top:20px;\">");
                    }
                    else
                    {
                        asbestosReport.AppendLine("<div style=\"page-break-after: always;margin-top:20px;\">");
                    }

                    asbestosReport.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"border: solid 1px #000000;\">");
                    asbestosReport.AppendLine("<thead>");
                    asbestosReport.AppendLine("<tr>");
                    asbestosReport.AppendLine("<th colspan=\"3\" style=\"border-right:solid 1px #000000;\"> Ashing(Position_____Level_____)");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th colspan=\"2\" style=\"border-right:solid 1px #000000;\"> Acid");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th colspan=\"3\"> Composition");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("</thead>");
                    asbestosReport.AppendLine("<tbody>");
                    asbestosReport.AppendLine("<tr>");
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:right\">{0}", "g"));
                    asbestosReport.AppendLine("</td>");
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:right\">{0}", "g"));
                    asbestosReport.AppendLine("</td>");
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:right\">{0}", "%"));
                    asbestosReport.AppendLine("</td>");
                    asbestosReport.AppendLine(string.Format("<td>{0}", ""));
                    asbestosReport.AppendLine("</td>");
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:right\">{0}", "%"));
                    asbestosReport.AppendLine("</td>");
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center\">{0}", "Fiber ID"));
                    asbestosReport.AppendLine("</td>");
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center\">{0}", "Treated %"));
                    asbestosReport.AppendLine("</td>");
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center\">{0}", "Original"));
                    asbestosReport.AppendLine("</td>");
                    asbestosReport.AppendLine("</tr>");
                    asbestosReport.AppendLine("<tr>");
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", "Crucible (Y)"));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", "X-Y"));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", "[(Z-Y)/(X-Y)]*100,B"));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", "Sample (X)"));
                    asbestosReport.AppendLine(string.Format("<td>{0}</td>", "[(Z-Y)/X]*100, AIM"));
                    asbestosReport.AppendLine(string.Format("<td>{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td>{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td>{0}</td>", ""));
                    asbestosReport.AppendLine("</tr>");
                    asbestosReport.AppendLine("<tr>");
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:right\">{0}</td>", "g"));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:right\">{0}</td>", "g"));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", "Inorganic"));
                    asbestosReport.AppendLine(string.Format("<td>{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:right\">{0}</td>", "%"));
                    asbestosReport.AppendLine(string.Format("<td>{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td>{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td>{0}</td>", ""));
                    asbestosReport.AppendLine("</tr>");
                    asbestosReport.AppendLine("<tr>");
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", "Y+Sample(X)"));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", "Z-Y"));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:right\">{0}</td>", "%"));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", "Filter (Y)"));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", "100-AIM, ASM"));
                    asbestosReport.AppendLine(string.Format("<td>{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td>{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td>{0}</td>", ""));
                    asbestosReport.AppendLine("</tr>");
                    asbestosReport.AppendLine("<tr>");
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:right\">{0}</td>", "g"));
                    asbestosReport.AppendLine(string.Format("<td rowspan=\"2\">{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center\" rowspan=\"2\">{0}</td>", "100-B, A<br/>Organic"));
                    asbestosReport.AppendLine(string.Format("<td>{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:right\">{0}</td>", "%"));
                    asbestosReport.AppendLine(string.Format("<td>{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td>{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td>{0}</td>", ""));
                    asbestosReport.AppendLine("</tr>");
                    asbestosReport.AppendLine("<tr>");
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", "X Post Ash (Z)"));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", "Post Acid (Z)"));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", "Multiplier (Inorg X AIM)"));
                    asbestosReport.AppendLine(string.Format("<td>{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td>{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td>{0}</td>", ""));
                    asbestosReport.AppendLine("</tr>");

                    asbestosReport.AppendLine("<tr>");
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center\" rowspan=\"3\"><b>{0}</b></td>", "Vermiculite"));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", "Sample (X)"));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:right\">{0}</td>", "g"));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center;border:none;\">{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center;border:none;\">{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td>{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td>{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td>{0}</td>", ""));
                    asbestosReport.AppendLine("</tr>");
                    asbestosReport.AppendLine("<tr>");
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", "Bundle(Y)"));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:right\">{0}</td>", "g"));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center;border:none;\">{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center;border:none;\">{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td style=\"border:none;\">{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td style=\"border:none;\">{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td style=\"border:none;\">{0}</td>", ""));
                    asbestosReport.AppendLine("</tr>");
                    asbestosReport.AppendLine("<tr>");
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", "(Y/X)*100"));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:right\">{0}</td>", "%"));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center;border:none;\">{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td style=\"text-align:center;border:none;\">{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td style=\"border:none;\">{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td style=\"border:none;\">{0}</td>", ""));
                    asbestosReport.AppendLine(string.Format("<td style=\"border:none;\">{0}</td>", ""));
                    asbestosReport.AppendLine("</tr>");

                    asbestosReport.AppendLine("</tbody>");
                    asbestosReport.AppendLine("</table>");
                    asbestosReport.AppendLine("</div>");
                }
                #endregion
                #region QC
                if (projectSample.IsQC)
                {
                    if (projectData.ProjectSample.Count == counter)
                    {
                        asbestosReport.AppendLine("<div style=\"page-break-before: always;\">");
                    }
                    else
                    {
                        asbestosReport.AppendLine("<div style=\"page-break-after: always;\">");
                    }
                    #region Sample Info
                    asbestosReport.AppendLine("<div style=\"padding: 0 10px\">");
                    asbestosReport.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"border: solid 1px #000000;\">");
                    asbestosReport.AppendLine("<thead>");
                    asbestosReport.AppendLine("<tr>");
                    asbestosReport.AppendLine("<th> Lab Id");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>Batch");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>Date");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>Analyst");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>Temperature(&#8451;)");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>Microscope");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>QC");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("</tr>");
                    asbestosReport.AppendLine("</thead>");
                    asbestosReport.AppendLine("<tbody>");

                    asbestosReport.AppendLine("<tr>");
                    asbestosReport.AppendLine(string.Format("<td>{0}", projectSample.LabId));
                    asbestosReport.AppendLine("</td>");
                    asbestosReport.AppendLine(string.Format("<td>{0}", projectSample.BatchNumber));
                    asbestosReport.AppendLine("</td>");
                    asbestosReport.AppendLine(string.Format("<td>{0}", (projectData.DateOfAnalyzed != null ? projectData.DateOfAnalyzed.ToString().Split(' ')[0] : "")));
                    asbestosReport.AppendLine("</td>");
                    asbestosReport.AppendLine(string.Format("<td>{0}", projectData.ProjectAnalystName));
                    asbestosReport.AppendLine("</td>");
                    asbestosReport.AppendLine("<td>");
                    asbestosReport.AppendLine("</td>");
                    asbestosReport.AppendLine("<td>EQ0001, EQ0018");
                    asbestosReport.AppendLine("</td>");
                    asbestosReport.AppendLine(string.Format("<td>{0}", (projectSample.IsQC == true ? "Yes" : "No")));
                    asbestosReport.AppendLine("</td>");
                    asbestosReport.AppendLine("</tr>");

                    asbestosReport.AppendLine("</tbody>");
                    asbestosReport.AppendLine("</table>");
                    asbestosReport.AppendLine("</div>");

                    #endregion
                    #region Layer Info

                    asbestosReport.AppendLine("<div style=\"padding: 10px 10px\">");
                    asbestosReport.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"border: solid 1px #000000;\">");
                    asbestosReport.AppendLine("<thead>");
                    asbestosReport.AppendLine("<tr>");
                    asbestosReport.AppendLine("<th style=\"width:10%\"> Layer");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th style=\"width:10%\">%");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>Bulk Morphology( Fiber Type, Estimated %, Homogeneity)");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("</tr>");
                    asbestosReport.AppendLine("</thead>");
                    asbestosReport.AppendLine("<tbody>");
                    layerCounter = 1;
                    foreach (ProjectSampleDetailViewModel projectSampleDetail in projectSample.ProjectSampleDetail)
                    {
                        asbestosReport.AppendLine("<tr>");
                        asbestosReport.AppendLine(string.Format("<td>{0}", layerCounter));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td style=\"width:10%\">{0}", "100"));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine(string.Format("<td>{0}", projectSampleDetail.SampleType));
                        asbestosReport.AppendLine("</td>");
                        asbestosReport.AppendLine("</tr>");
                        layerCounter++;
                    }


                    asbestosReport.AppendLine("</tbody>");
                    asbestosReport.AppendLine("</table>");
                    asbestosReport.AppendLine("</div>");
                    #endregion
                    #region Sample Composite Homogeneity
                    asbestosReport.AppendLine("<div style=\"padding: 5px 10px\">");
                    asbestosReport.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" width=\"40%\" style=\"border: solid 1px #000000;\">");
                    asbestosReport.AppendLine("<thead>");
                    asbestosReport.AppendLine("<tr>");
                    asbestosReport.AppendLine("<th style=\"text-align:right;\">Sample Composite Homogeneity:");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine(string.Format("<th>{0}", projectSample.SampleCompositeHomogeneityText));
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("</tr>");
                    asbestosReport.AppendLine("</thead>");
                    asbestosReport.AppendLine("</table>");
                    asbestosReport.AppendLine("</div>");
                    #endregion

                    #region Asbestos
                    asbestosReport.AppendLine("<div style=\"padding: 0px 10px\">");
                    asbestosReport.AppendLine("<b>Asbestos </b>");
                    asbestosReport.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"border: solid 1px #000000;\">");
                    asbestosReport.AppendLine("<thead>");
                    asbestosReport.AppendLine("<tr>");
                    asbestosReport.AppendLine("<th style=\"width:10%\"> Layer");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th style=\"width:10%\">%");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>Fiber Morphology");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>Colour");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>Pleo");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>nD, T Corr");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>Bifring");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>Extinction");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>Elong");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>D.S Colour");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>R.I Liquid");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th style=\"width:30%\">Fiber Id");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("</tr>");
                    asbestosReport.AppendLine("</thead>");
                    asbestosReport.AppendLine("<tbody>");
                    layerCounter2 = 1;
                    foreach (ProjectSampleDetailViewModel projectSampleDetail in projectSample.ProjectSampleDetail)
                    {
                        List<AsbestosPercentageDetail> asbestosPercentageDetail = new AsbestosPercentageDetailService().GetByAsbestosPercentageID((int)projectSampleDetail.AbsestosPercentage);
                        int asbesotsCount = 1;
                        foreach (AsbestosPercentageDetail apd in asbestosPercentageDetail)
                        {
                            string riLiquid = "1.550";
                            if (!string.IsNullOrEmpty(apd.ri_liquid))
                            {
                                riLiquid = apd.ri_liquid;
                            }
                            asbestosReport.AppendLine("<tr>");
                            asbestosReport.AppendLine("<td style=\"width:6%\">" + (asbesotsCount == 1 ? layerCounter2.ToString() : ""));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine(string.Format("<td style=\"width:10%; text-align: center;\">{0}", apd.Value));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", apd.fiber_morphology));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", apd.color));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", apd.pleo));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", apd.nd_t_corr));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", apd.bifring));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", apd.extinction));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", apd.elong));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", apd.ds_color));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", riLiquid));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine(string.Format("<td style=\"width:30%\">{0}", apd.FiberId));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine("</tr>");
                            //if(asbestosPercentageDetail.Count > 1 && asbesotsCount != asbestosPercentageDetail.Count)
                            //{
                            //    asbestosReport.AppendLine("<tr>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("</tr>");
                            //}
                            asbesotsCount++;
                        }

                        layerCounter2++;
                    }

                    asbestosReport.AppendLine("</tbody>");
                    asbestosReport.AppendLine("</table>");
                    asbestosReport.AppendLine("</div>");
                    #endregion
                    #region Composite Non-Asbestos Contents
                    asbestosReport.AppendLine("<div style=\"padding: 0px 10px\">");
                    asbestosReport.AppendLine("<b>Composite Non-Asbestos Contents </b>");
                    asbestosReport.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"border: solid 1px #000000;\">");
                    asbestosReport.AppendLine("<thead>");
                    asbestosReport.AppendLine("<tr>");
                    asbestosReport.AppendLine("<th style=\"width:10%\"> Layer");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th style=\"width:10%\">%");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>Fiber Morphology");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>Colour");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>Pleo");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>nD, T Corr");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>Bifring");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>Extinction");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>Elong");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>D.S Colour");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th>R.I Liquid");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("<th style=\"width:30%\">Fiber Id");
                    asbestosReport.AppendLine("</th>");
                    asbestosReport.AppendLine("</tr>");
                    asbestosReport.AppendLine("</thead>");
                    asbestosReport.AppendLine("<tbody>");
                    layerCounter3 = 1;
                    foreach (ProjectSampleDetailViewModel projectSampleDetail in projectSample.ProjectSampleDetail)
                    {
                        List<CompositeNonAsbestosContentsDetail> compositeNonAsbestosContentsDetail = new CompositeNonAsbestosContentsDetailService().GetByAsbestosPercentageID((int)projectSampleDetail.CompositeNonAsbestosContents);
                        int nonAsbestosCount = 1;
                        foreach (CompositeNonAsbestosContentsDetail cnacd in compositeNonAsbestosContentsDetail)
                        {
                            string riLiquid = "1.550";
                            if (!string.IsNullOrEmpty(cnacd.ri_liquid))
                            {
                                riLiquid = cnacd.ri_liquid;
                            }
                            asbestosReport.AppendLine("<tr>");
                            asbestosReport.AppendLine("<td style=\"width:6%\">" + (nonAsbestosCount == 1 ? layerCounter3.ToString() : ""));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine(string.Format("<td style=\"width:10%; text-align: center;\">{0}", cnacd.Value));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", cnacd.fiber_morphology));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", cnacd.color));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", cnacd.pleo));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", cnacd.nd_t_corr));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", cnacd.bifring));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", cnacd.extinction));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", cnacd.elong));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", cnacd.ds_color));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine(string.Format("<td style=\"width:6%\">{0}", riLiquid));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine(string.Format("<td style=\"width:30%\">{0}", cnacd.FiberId));
                            asbestosReport.AppendLine("</td>");
                            asbestosReport.AppendLine("</tr>");

                            //if (compositeNonAsbestosContentsDetail.Count > 1 && nonAsbestosCount != compositeNonAsbestosContentsDetail.Count)
                            //{
                            //    asbestosReport.AppendLine("<tr>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("<td>&nbsp;</td>");
                            //    asbestosReport.AppendLine("</tr>");
                            //}
                            nonAsbestosCount++;
                        }


                        layerCounter3++;
                    }

                    asbestosReport.AppendLine("</tbody>");
                    asbestosReport.AppendLine("</table>");
                    asbestosReport.AppendLine("</div>");
                    #endregion
                    asbestosReport.AppendLine("</div>");

                }
                #endregion
                counter++;
            }

            return asbestosReport.ToString();
        }
        public string getAsbestosPercentageForLayer(string asbestosPercentage)
        {
            string percentage = "";

            return percentage;
        }
        public string getAsbestosPercentage(string asbestosPercentage)
        {
            string percentage = "";

            return percentage;
        }
        public string getCompositeNonAsbestosContents(string asbestosPercentage)
        {
            string percentage = "";

            return percentage;
        }
    }
}