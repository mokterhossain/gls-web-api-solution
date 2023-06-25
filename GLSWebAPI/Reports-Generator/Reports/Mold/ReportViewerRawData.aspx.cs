using GLSPM.Data.Modules.ProjectManagement;
using GLSPM.Service.Modules.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GLSWebAPI.Reports_Generator.Reports.Mold
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
            List<MoldViewModel> moldData = new MoldService().GetDataForMoldReportPrint(Convert.ToInt64(projectId));
            moldReportHtml.AppendLine("<table border=\"0\" style=\"width: 100%\">");
            var moldGroups =
                            from sep in moldData
                            group sep by new { sep.SerialNo, sep.LabId, sep.Location, sep.CommentsIndex, sep.AnalysisDate };
            int numberOfTable = ((moldGroups.Count() - (moldGroups.Count() % 3)) / 3) + ((moldGroups.Count() % 3) > 0 ? 1 : 0);
            var moldGroupForSporeType = moldGroups.Take(1);
            if (moldGroups.Count() > 0)
            {
                for (int i = 0; i < numberOfTable; i++)
                {
                    var moldGroupForTable = moldGroups.Skip(i * 3).Take(3);
                    moldReportHtml.AppendLine("<div style=\"font-weight:bold; padding-left:10px;\">Lab Test Method: Analysis of Fungal Spore & Particulate by Optical Microscopy (ASTM D7391-17e1)</div>");
                    if (i == (numberOfTable - 1))
                        moldReportHtml.AppendLine("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">");
                    else
                        moldReportHtml.AppendLine("<table border=\"0\"  cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"page-break-after: always;\">");
                    moldReportHtml.Append("<tr>");
                    moldReportHtml.Append("<td style=\"font-weight:bold;padding-left:4px;width:28%\">Location:</td>");
                    foreach (var moldGroup in moldGroupForTable)
                    {
                        moldReportHtml.AppendLine(string.Format("<td colspan=\"2\" style=\"text-align:center;font-weight:bold;width:24%; line-height: 16px;\">{0}</td>", moldGroup.Key.Location));
                    }
                    moldReportHtml.Append("</tr>");
                    moldReportHtml.Append("<tr>");
                    moldReportHtml.Append("<td style=\"font-weight:bold;padding-left:4px;\">Comments:</td>");
                    foreach (var moldGroup in moldGroupForTable)
                    {
                        moldReportHtml.AppendLine(string.Format("<td colspan=\"2\" style=\"text-align:center;font-weight:bold;\">{0}</td>", moldGroup.Key.CommentsIndex));
                    }
                    moldReportHtml.Append("</tr>");
                    moldReportHtml.Append("<tr>");
                    moldReportHtml.Append("<td style=\"font-weight:bold;padding-left:4px;\">Lab Id:</td>");
                    foreach (var moldGroup in moldGroupForTable)
                    {
                        moldReportHtml.AppendLine(string.Format("<td colspan=\"2\" style=\"text-align:center; font-weight:bold;\">{0}</td>", moldGroup.Key.LabId));
                    }
                    moldReportHtml.Append("</tr>");
                    moldReportHtml.Append("<tr>");
                    moldReportHtml.Append("<td style=\"font-weight:bold;padding-left:4px;\">Analysis Date:</td>");
                    foreach (var moldGroup in moldGroupForTable)
                    {
                        moldReportHtml.AppendLine(string.Format("<td colspan=\"2\" style=\"text-align:center\">{0}</td>", moldGroup.Key.AnalysisDate));
                    }
                    moldReportHtml.Append("</tr>");
                    moldReportHtml.Append("<tr style=\"background-color:#D3D3D3;\">");
                    moldReportHtml.Append("<td style=\"font-weight:bold;padding-left:4px;\">Common Indoor Indicator Spore:</td>");
                    foreach (var moldGroup in moldGroupForTable)
                    {
                        moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", "raw ct."));
                        moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", "per m3"));
                    }
                    moldReportHtml.Append("</tr>");
                    /*StringBuilder firstSampleColumn = new StringBuilder();
                    StringBuilder secondSampleColumn = new StringBuilder();
                    StringBuilder thirdSampleColumn = new StringBuilder();
                    int sampleCount = 1;
                    foreach (var moldSporeTypeGroup in moldGroupForSporeType)
                    {
                        IList<MoldViewModel> moldGroupData = new List<MoldViewModel>(moldSporeTypeGroup);
                        foreach (MoldViewModel item in moldGroupData)
                        {

                        }
                    }*/
                    List<MoldViewModel> moldDataList = new List<MoldViewModel>();
                    foreach (var moldGroup in moldGroupForTable)
                    {
                        List<MoldViewModel> moldGroupData = new List<MoldViewModel>(moldGroup);
                        moldDataList.AddRange(moldGroupData);
                    }
                    var moldSporeTypeGroups =
                            from sep in moldDataList
                            group sep by new { sep.SporeType };

                    foreach (var moldSporeType in moldSporeTypeGroups)
                    {
                        int counter = 0;
                        IList<MoldViewModel> moldSporeTypeData = new List<MoldViewModel>(moldSporeType);
                        if (moldSporeType.Key.SporeType == "Other Spore Type Detected:" || moldSporeType.Key.SporeType == "Additional Information:")
                        {
                            moldReportHtml.AppendLine("<tr style=\"background-color:#D3D3D3;\">");
                        }
                        else
                        {
                            moldReportHtml.AppendLine("<tr>");
                        }

                        int sporeTypeCount = 0;

                        foreach (MoldViewModel item in moldSporeTypeData)
                        {
                            counter++;
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
                                    moldReportHtml.AppendLine(string.Format("<td colspan=\"{0}\" style=\"text-align:center\"></td>", 6));

                            }
                            else
                            {
                                if (item.Overloaded == "Overloaded")
                                {
                                    if (item.SporeCategory != "Additional")
                                    {
                                        moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center; border: none;\"></td>"));
                                        if (item.SporeTypeId == 14)
                                        {
                                            moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center; border-top: none;border-bottom: none;border-left: none;   text-align: left;font-weight: bold;\">Overloaded</td>"));
                                        }
                                        else
                                        {
                                            moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center;  border-top: none;border-bottom: none;border-left: none;\">{0}</td>", ""));
                                        }

                                    }
                                    else
                                    {
                                        if (item.SporeType == "Total:")
                                        {
                                            moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center;border-bottom: none;border-left: none;\">{0}</td>", ""));
                                            moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center;border-bottom: none;border-left: none;\">{0}</td>", ""));
                                        }
                                        else if (item.SporeType == "Sample volume (liters)")
                                        {
                                            moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", (item.RawctStr == "0" ? "-" : item.RawctStr)));
                                            moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", (item.PermmStr == "0" ? "-" : item.PermmStr)));
                                        }
                                        else
                                        {
                                            moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", (item.Rawct == 0 ? "-" : Math.Round(item.Rawct, 0).ToString())));
                                            moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", (item.Permm == 0 ? "-" : Math.Round(item.Permm, 0).ToString())));
                                        }
                                    }
                                    // moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center; border: none;\">{0}</td>", (item.PermmStr == "0" ? "-" : item.PermmStr)));
                                }
                                else
                                {
                                    if (item.SporeType == "Total:")
                                    {

                                        if (item.RawctStr == "")
                                        {
                                            moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center;border-right: none;\">{0}</td>", (item.Rawct == 0 ? "" : Math.Round(item.Rawct, 0).ToString())));
                                        }
                                        else
                                        {
                                            moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center;border-right: none; text-align: left;\">{0}</td>", (item.Rawct == 0 ? "-" : Math.Round(item.Rawct, 0).ToString())));
                                        }
                                        if (item.PermmStr == "")
                                        {
                                            moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center;border-left: none;\">{0}</td>", (item.Permm == 0 ? "-" : Math.Round(item.Permm, 0).ToString())));
                                        }
                                        else
                                        {
                                            moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center;border-left: none; text-align: left;\">{0}</td>", (item.Permm == 0 ? "-" : Math.Round(item.Permm, 0).ToString())));
                                        }
                                    }
                                    else if (item.SporeType == "Sample volume (liters)")
                                    {
                                        moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center; border-right: none; \">{0}</td>", ""));
                                        moldReportHtml.AppendLine(string.Format("<td style=\"text-align:left;border-left: none; \">{0}</td>", (item.PermmStr == "0" ? "-" : item.PermmStr)));
                                    }
                                    else if (item.SporeType == "Background debris")
                                    {
                                        moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center; border-right: none; \">{0}</td>", ""));
                                        moldReportHtml.AppendLine(string.Format("<td style=\"text-align:left;border-left: none; \">{0}</td>", (item.Permm == 0 ? "-" : Math.Round(item.Permm, 0).ToString())));
                                    }
                                    else if (item.SporeType == "Limit of detection")
                                    {
                                        moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center; border-right: none; \">{0}</td>", ""));
                                        moldReportHtml.AppendLine(string.Format("<td style=\"text-align:left;border-left: none; \">{0}</td>", (item.Permm == 0 ? "-" : Math.Round(item.Permm, 0).ToString())));
                                    }
                                    else if (item.SporeType == "Hyphal fragments")
                                    {
                                        moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center; border-right: none; \">{0}</td>", ""));
                                        moldReportHtml.AppendLine(string.Format("<td style=\"text-align:left;border-left: none; \">{0}</td>", (item.Permm == 0 ? "-" : Math.Round(item.Permm, 0).ToString())));
                                    }
                                    else if (item.SporeType == "Skin cells")
                                    {
                                        moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center; border-right: none; \">{0}</td>", ""));
                                        moldReportHtml.AppendLine(string.Format("<td style=\"text-align:left;border-left: none; \">{0}</td>", (item.Permm == 0 ? "-" : Math.Round(item.Permm, 0).ToString())));
                                    }
                                    else if (item.SporeType == "Pollen")
                                    {
                                        moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center; border-right: none; \">{0}</td>", ""));
                                        moldReportHtml.AppendLine(string.Format("<td style=\"text-align:left;border-left: none; \">{0}</td>", (item.Permm == 0 ? "-" : Math.Round(item.Permm, 0).ToString())));
                                    }
                                    else
                                    {
                                        moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", (item.Rawct == 0 ? "-" : Math.Round(item.Rawct, 0).ToString())));
                                        moldReportHtml.AppendLine(string.Format("<td style=\"text-align:center\">{0}</td>", (item.Permm == 0 ? "-" : Math.Round(item.Permm, 0).ToString())));
                                    }

                                }

                            }
                            sporeTypeCount++;
                        }
                        moldReportHtml.AppendLine("</tr>");

                    }
                    moldReportHtml.AppendLine(string.Format("<tr><td colspan=\"{0}\" style=\"border:none\"><div><span style=\"font-weight:bold;\"><i>Comments</i>: </span>{1}<div>", (moldGroupForTable.Count() * 2 + 1), comments));
                    /*foreach (var moldGroup in moldGroupForTable)
                    {
                        IList<MoldViewModel> moldGroupData = new List<MoldViewModel>(moldGroup);
                        foreach (MoldViewModel item in moldGroupData)
                        {
                            if(sampleCount == 1)
                            {
                                firstSampleColumn.AppendLine("<tr>");
                                firstSampleColumn.AppendLine(string.Format("<td>{0}</td>", item.SporeType));
                                firstSampleColumn.AppendLine(string.Format("<td>{0}</td>", item.RawctStr));
                                firstSampleColumn.AppendLine(string.Format("<td>{0}</td>", item.PermmStr));
                            }
                            else if (sampleCount == 2)
                            {
                                secondSampleColumn.AppendLine(string.Format("<td>{0}</td>", item.RawctStr));
                                secondSampleColumn.AppendLine(string.Format("<td>{0}</td>", item.PermmStr));
                            }
                            else if (sampleCount == 3)
                            {
                                thirdSampleColumn.AppendLine(string.Format("<td>{0}</td>", item.RawctStr));
                                thirdSampleColumn.AppendLine(string.Format("<td>{0}</td>", item.PermmStr));
                                thirdSampleColumn.AppendLine("</tr>");
                            }

                        }
                        sampleCount++;
                    }*/

                    moldReportHtml.AppendLine("</table>");
                    //moldReportHtml.AppendLine(string.Format("<div><span style=\"font-weight:bold;\"><i>Comments</i>: </span>{0}<div>", comments));
                }
            }
            /*moldReportHtml.AppendLine("<tr>");
            foreach (var moldGroup in moldGroups)
            {
                moldReportHtml.AppendLine("<td>Location</td>");
                moldReportHtml.AppendLine(string.Format("<td>{0}</td>", moldGroup.Key.Location));
                IList <MoldViewModel> moldGroupData = new List<MoldViewModel>(moldGroup);
                foreach(MoldViewModel item in moldGroupData)
                {

                }
            }
            moldReportHtml.AppendLine("</tr>");
            moldReportHtml.AppendLine("</table>");*/
            dvMold.InnerHtml = moldReportHtml.ToString();
        }
    }
}