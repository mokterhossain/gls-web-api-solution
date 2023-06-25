using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GLSPM.Data.Modules.BasicModule;
using GLSPM.Data.Modules.ProjectManagement;
using GLSPM.Service.Modules.BasicModule;
using GLSPM.Service.Modules.ProjectManagement;

namespace GLSWebAPI.Reports_Generator.Reports.MoldTapeLift
{
    public partial class ReportViewerRawData : System.Web.UI.Page
    {
        public string comments = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            long projectId = 0;
            if (Request.QueryString["projectId"] != null)
            {
                projectId = Convert.ToInt64(Request.QueryString["projectId"]);
            }
            GLSPM.Data.Modules.ProjectManagement.Mold mold = new MoldService().GetByProjectID(Convert.ToInt64(projectId));
            if (mold != null)
            {
                comments = mold.Comments;
            }
            StringBuilder moldReportHtml = new StringBuilder();
            double sampleVolume = 0;
            List<GeneralSetting> settings = new GeneralSettingService().All();

            if (settings != null)
            {
                GeneralSetting setting = settings.FirstOrDefault();
                if (setting != null)
                {
                    sampleVolume = (double)setting.Volume * 1000;
                }
            }
            List<MoldTapeLiftReportModel> moldData = new MoldService().GetDataForMoldTapeLiftReport(Convert.ToInt64(projectId));
            moldReportHtml.AppendLine("<table border=\"0\" style=\"width: 100%\">");
            if (moldData != null)
            {
                moldData = moldData.OrderBy(c => c.LabId).ThenBy(n => n.RowNumber).ToList();
                var moldGroups =
                            from sep in moldData
                            group sep by new { sep.LabId, sep.Location, sep.CommentsIndex, sep.AnalysisDate };
                int numberOfTable = ((moldGroups.Count() - (moldGroups.Count() % 3)) / 3) + ((moldGroups.Count() % 3) > 0 ? 1 : 0);
                var moldGroupForSporeType = moldGroups.Take(1);
                if (moldGroups.Count() > 0)
                {
                    for (int i = 0; i < numberOfTable; i++)
                    {
                        var moldGroupForTable = moldGroups.Skip(i * 3).Take(3);
                        moldReportHtml.AppendLine("<div style=\"font-weight:bold; padding-left:10px;font-size:20px;\">Lab Test Method: Analysis of Fungal Spore & Particulate by Optical Microscopy (ASTM D7391-17e1)</div>");
                        if (i == (numberOfTable - 1))
                            moldReportHtml.AppendLine("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">");
                        else
                            moldReportHtml.AppendLine("<table border=\"0\"  cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"page-break-after: always;\">");
                        moldReportHtml.Append("<tr>");
                        moldReportHtml.Append("<td style=\"font-weight:bold;padding-left:4px;width:31%\">Location:</td>");
                        foreach (var moldGroup in moldGroupForTable)
                        {
                            moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center;font-weight:bold;width:23%\">{0}</td>", moldGroup.Key.Location));
                        }
                        moldReportHtml.Append("</tr>");
                        moldReportHtml.Append("<tr>");
                        moldReportHtml.Append("<td style=\"font-weight:bold;padding-left:4px;\">Comments:</td>");
                        foreach (var moldGroup in moldGroupForTable)
                        {
                            moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center;font-weight:bold;\">{0}</td>", moldGroup.Key.CommentsIndex));
                        }
                        moldReportHtml.Append("</tr>");
                        moldReportHtml.Append("<tr>");
                        moldReportHtml.Append("<td style=\"font-weight:bold;padding-left:4px;\">Lab Id:</td>");
                        foreach (var moldGroup in moldGroupForTable)
                        {
                            moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center; font-weight:bold;\">{0}</td>", moldGroup.Key.LabId));
                        }
                        moldReportHtml.Append("</tr>");
                        moldReportHtml.Append("<tr>");
                        moldReportHtml.Append("<td style=\"font-weight:bold;padding-left:4px;\">Analysis Date:</td>");
                        foreach (var moldGroup in moldGroupForTable)
                        {
                            moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", moldGroup.Key.AnalysisDate));
                        }
                        moldReportHtml.Append("</tr>");
                        moldReportHtml.Append("<tr style=\"background-color:#D3D3D3;\">");
                        moldReportHtml.Append("<td style=\"font-weight:bold;padding-left:4px;\">Common Indoor Indicator Spore:</td>");
                        foreach (var moldGroup in moldGroupForTable)
                        {
                            moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", "Relative Mold Conc:"));
                        }
                        moldReportHtml.Append("</tr>");
                        List<MoldTapeLiftReportModel> moldDataList = new List<MoldTapeLiftReportModel>();
                        foreach (var moldGroup in moldGroupForTable)
                        {
                            List<MoldTapeLiftReportModel> moldGroupData = new List<MoldTapeLiftReportModel>(moldGroup);
                            moldDataList.AddRange(moldGroupData);
                        }
                        var moldSporeTypeGroups =
                            from sep in moldDataList
                            group sep by new { sep.SporeType };
                        foreach (var moldSporeType in moldSporeTypeGroups)
                        {
                            IList<MoldTapeLiftReportModel> moldSporeTypeData = new List<MoldTapeLiftReportModel>(moldSporeType);
                            if (moldSporeType.Key.SporeType == "Other Spore Type Detected:" || moldSporeType.Key.SporeType == "Additional Information:")
                            {
                                moldReportHtml.AppendLine("<tr style=\"background-color:#D3D3D3;\">");
                            }
                            else
                            {
                                moldReportHtml.AppendLine("<tr>");
                            }

                            int sporeTypeCount = 0;
                            foreach (MoldTapeLiftReportModel item in moldSporeTypeData)
                            {
                                if (sporeTypeCount == 0)
                                {
                                    if (moldSporeType.Key.SporeType == "Other Spore Type Detected:" || moldSporeType.Key.SporeType == "Additional Information:")
                                    {
                                        moldReportHtml.AppendLine(string.Format("<td style=\"padding-left:4px;font-weight: bold;\">{0}</td>", item.SporeType));
                                    }
                                    else
                                    {
                                        moldReportHtml.AppendLine(string.Format("<td style=\"padding-left:4px;\">{0}</td>", item.SporeType));
                                    }
                                }
                                if (moldSporeType.Key.SporeType == "Other Spore Type Detected:" || moldSporeType.Key.SporeType == "Additional Information:")
                                {
                                    if (sporeTypeCount == 0)
                                        moldReportHtml.AppendLine(string.Format("<td colspan=\"{0}\" style=\"text-align:center\"></td>", 3));

                                }
                                else
                                {
                                    moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", (item.RelativeMoldConc == "" || item.RelativeMoldConc == null ? "-" : item.RelativeMoldConc)));
                                }
                                sporeTypeCount++;
                            }
                            moldReportHtml.AppendLine("</tr>");

                        }
                        moldReportHtml.AppendLine(string.Format("<tr><td colspan=\"{0}\" style=\"border:none\"><div><span style=\"font-weight:bold;\"><i>Comments</i>: </span>{1}<div>", (moldGroupForTable.Count() + 1), comments));
                        moldReportHtml.AppendLine("</table>");
                    }
                }
                dvMold.InnerHtml = moldReportHtml.ToString();
            }
        }
    }
}