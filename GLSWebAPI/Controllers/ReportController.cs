using GLSPM.Data;
using GLSPM.Data.Modules.BasicModule;
using GLSPM.Data.Modules.ProjectManagement;
using GLSPM.Service.Modules.BasicModule;
using GLSPM.Service.Modules.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
// EO.Pdf;
using System.IO;
using System.Drawing;
using System.Text;
using SelectPdf;
using System.Text.RegularExpressions;

namespace GLSWebAPI.Controllers
{
    public class ReportController : ApiController
    {
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Report/GenerateLabel")]
        public HttpResponseMessage GenerateLabel(string projectIds, long sampleId)
        {
            string fileName = string.Format("{0}-{1}-{2}.pdf", "label-print", projectIds.Replace(',', '-'), sampleId); // string.Format("{0}_{1}_{2}_{3}.pdf", "GLS", "_Label_", projectIds.Replace(',', '-'), sampleId);

            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            System.Drawing.SizeF size = new System.Drawing.SizeF(216, 68.4f);

            converter.Options.PdfPageSize = PdfPageSize.Custom; //new SizeF(1, 0.5f);
            converter.Options.PdfPageCustomSize = size;

            string pdfFilePath = string.Format("C:\\GLS\\gls-pms-api\\Reports\\Labels\\{0}", fileName);

            string url = string.Format("{0}/Reports-Generator/Labels/LabelViewer.aspx?projectIds=" + projectIds + "&sampleId=" + sampleId, BaseFullUrl);

            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertUrl(url);

            // save pdf document
            doc.Save(pdfFilePath);

            // close pdf document
            doc.Close();
            return this.Request.CreateResponse(HttpStatusCode.OK, fileName);
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Report/GenerateLabel2")]
        public HttpResponseMessage GenerateLabel2(string projectIds, long sampleId)
        {
            string fileName = string.Format("{0}_{1}_{2}_{3}.pdf", "GLS", "_Label_", projectIds.Replace(',', '-'), sampleId);
            /* EO.Pdf.Runtime.AddLicense(
                        "ILfH3LVrs7P9FOKe5ff29ON3hI6xy59Zs/D6DuSn6un26bto4+30EO2s3OnP" +
                        "uIlZl6Sx5+Cl4/MI6YxDl6Sxy59Zl6TNDOOdl/gKG+R2mcng2c+d3aaxIeSr" +
                        "6u0AGbxbqLu/26FZpsKetZ9Zl6TN2uCl4/MI6YxDl6Sxy7uo6ej2Hcin3fOx" +
                        "D+Ct3MGz5K5rrrPD27BwmaQEIOF+7/T6HeSsuPjOzbhoqbvA3a9qr6axIeSr" +
                        "6u0AGbxbqLuzy653hI6xy59Zs/f6Eu2a6/kDEL2hp/sL7rV/ybgK7rWCy/Hb" +
                        "4viq6vzS6Lx1pvf6Eu2a6/kDEL1GgcDAF+ic3PIEEL1GgXXj7fQQ7azcwp61" +
                        "n1mXpM0X6Jzc8gQQyJ21uMHesmqr"); */
            EO.Pdf.HtmlToPdf.Options.PageSize = new SizeF(3f, 0.95f);
            EO.Pdf.PdfDocument theDoc = new EO.Pdf.PdfDocument();
            string pdfFilePath = string.Format("C:\\GLS\\gls-pms-api\\Reports\\Labels\\{0}", string.Format("{0}_{1}_{2}_{3}.pdf", "GLS", "_Label_", projectIds.Replace(',', '-'), sampleId));
            //string pdfFilePath = string.Format("C:\\GLS\\GLSWebAPI\\GLSWebAPI\\Reports\\Labels\\{0}", string.Format("{0}_{1}_{2}_{3}.pdf", "GLS", "_Label_", projectIds.Replace(',', '-'), sampleId)); // Server.MapPath(string.Format("~/Reports/Labels/{0}", string.Format("{0}_{1}_{2}.pdf", "CCG", "_CarInvoice_", DateTime.Now.Millisecond)));
            //string pdfFilePath = string.Format("C:\\GLS\\gls-pms-api\\Reports\\Labels\\{0}", string.Format("{0}_{1}_{2}_{3}.pdf", "GLS", "_Label_", projectIds.Replace(',', '-'), sampleId)); // Server.MapPath(string.Format("~/Reports/Labels/{0}", string.Format("{0}_{1}_{2}.pdf", "CCG", "_CarInvoice_", DateTime.Now.Millisecond)));
            EO.Pdf.HtmlToPdf.Options.AllowLocalAccess = true;

            //string url = string.Format("http://localhost:62797/Reports-Generator/Labels/LabelViewer.aspx?projectIds=" + projectIds + "&sampleId="+ sampleId);
            string url = string.Format("{0}/Reports-Generator/Labels/LabelViewer.aspx?projectIds=" + projectIds + "&sampleId=" + sampleId, BaseFullUrl);
            EO.Pdf.HtmlToPdf.ConvertUrl(url, theDoc);
            FileInfo fileInfo = new FileInfo(pdfFilePath);
            try
            {
                fileInfo = new FileInfo(pdfFilePath);
                // if (fileInfo.Exists)
                // fileInfo.Delete();
                lock (theDoc)
                {
                    theDoc.Save(pdfFilePath);
                    // DownloadPhotoReport(pdfFilePath);

                }
            }
            catch (Exception ex)
            {
                //fileInfo = new FileInfo(pdfFilePath);
                //if (fileInfo.Exists)
                //fileInfo.Delete();
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, fileName);
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Report/GeneratePRSNReport")]
        public HttpResponseMessage GeneratePRSNReport(long projectId, string projectNumber)
        {
            string reportName = "";
            Project project = new ProjectService().GetByID(projectId);
            if (project != null)
            {
                reportName = project.ProjectNumber;
            }
            string fileName = string.Format("PSRN-{0}.pdf", reportName);
            //string fileName = string.Format("{0}_{1}_{2}.pdf", "GLS", "_PRSN_", projectId);
            // get parameters
            string headerUrl = string.Format("{0}/Header-PSRN.aspx?projectId=" + projectId, BaseFullUrl);
            string footerUrl = string.Format("{0}/Footer-PSRN.aspx?projectId=" + projectId, BaseFullUrl);
            
            bool showHeaderOnFirstPage = true;
            bool showHeaderOnOddPages = true;
            bool showHeaderOnEvenPages = true;

            int headerHeight = 100;
            int footerHeight = 30;
            bool showFooterOnFirstPage = true;
            bool showFooterOnOddPages = true;
            bool showFooterOnEvenPages = true;
            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            // header settings
            converter.Options.DisplayHeader = true;
            converter.Header.DisplayOnFirstPage = showHeaderOnFirstPage;
            converter.Header.DisplayOnOddPages = showHeaderOnOddPages;
            converter.Header.DisplayOnEvenPages = showHeaderOnEvenPages;
            converter.Header.Height = headerHeight;

            PdfHtmlSection headerHtml = new PdfHtmlSection(headerUrl);
            headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            converter.Header.Add(headerHtml);

            // footer settings
            converter.Options.DisplayFooter = showFooterOnFirstPage ||
                showFooterOnOddPages || showFooterOnEvenPages;
            converter.Footer.DisplayOnFirstPage = showFooterOnFirstPage;
            converter.Footer.DisplayOnOddPages = showFooterOnOddPages;
            converter.Footer.DisplayOnEvenPages = showFooterOnEvenPages;
            converter.Footer.Height = footerHeight;

            PdfHtmlSection footerHtml = new PdfHtmlSection(footerUrl);
            footerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            converter.Footer.Add(footerHtml);

            // page numbers can be added using a PdfTextSection object
            PdfTextSection text = new PdfTextSection(5, 0,
                "Page: {page_number} of {total_pages}  ",
                new System.Drawing.Font("Arial", 8));
            text.HorizontalAlign = PdfTextHorizontalAlign.Right;
            text.VerticalAlign = PdfTextVerticalAlign.Bottom;
            converter.Footer.Add(text);

            string pdfFilePath = string.Format("C:\\GLS\\gls-pms-api\\Reports\\PRSN\\{0}", fileName);
            EO.Pdf.HtmlToPdf.Options.AllowLocalAccess = true;

            string url = "";
            url = string.Format("{0}/Reports-Generator/PRSN/PRSNViewer.aspx?projectId=" + projectId, BaseFullUrl);

            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertUrl(url);

            // save pdf document
            doc.Save(pdfFilePath);

            // close pdf document
            doc.Close();
            return this.Request.CreateResponse(HttpStatusCode.OK, fileName);
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Report/PrintBatchNumberReport")]
        public HttpResponseMessage PrintBatchNumberReport(string batchNumber)
        {
            string fileName = string.Format("BatchNumber-{0}.pdf", batchNumber);
            //string fileName = string.Format("BtachNumber-{0}.pdf", batchNumber);
            // get parameters
            string headerUrl = string.Format("{0}/Header-Print-Batch.aspx?batchNumber=" + batchNumber, BaseFullUrl);
            string footerUrl = string.Format("{0}/Footer-Print-Batch.aspx?batchNumber=" + batchNumber, BaseFullUrl);

            bool showHeaderOnFirstPage = true;
            bool showHeaderOnOddPages = true;
            bool showHeaderOnEvenPages = true;

            int headerHeight = 100;
            int footerHeight = 80;
            bool showFooterOnFirstPage = true;
            bool showFooterOnOddPages = true;
            bool showFooterOnEvenPages = true;
            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            // header settings
            converter.Options.DisplayHeader = true;
            converter.Header.DisplayOnFirstPage = showHeaderOnFirstPage;
            converter.Header.DisplayOnOddPages = showHeaderOnOddPages;
            converter.Header.DisplayOnEvenPages = showHeaderOnEvenPages;
            converter.Header.Height = headerHeight;

            PdfHtmlSection headerHtml = new PdfHtmlSection(headerUrl);
            headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            converter.Header.Add(headerHtml);

            // footer settings
            converter.Options.DisplayFooter = showFooterOnFirstPage ||
                showFooterOnOddPages || showFooterOnEvenPages;
            converter.Footer.DisplayOnFirstPage = showFooterOnFirstPage;
            converter.Footer.DisplayOnOddPages = showFooterOnOddPages;
            converter.Footer.DisplayOnEvenPages = showFooterOnEvenPages;
            converter.Footer.Height = footerHeight;

            PdfHtmlSection footerHtml = new PdfHtmlSection(footerUrl);
            footerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            converter.Footer.Add(footerHtml);

            // page numbers can be added using a PdfTextSection object
            PdfTextSection text = new PdfTextSection(5, 65,
                "Page: {page_number} of {total_pages}  ",
                new System.Drawing.Font("Arial", 8));
            text.HorizontalAlign = PdfTextHorizontalAlign.Right;
            text.VerticalAlign = PdfTextVerticalAlign.Bottom;
            converter.Footer.Add(text);

            string pdfFilePath = string.Format("C:\\GLS\\gls-pms-api\\Reports\\BatchNumber\\{0}", fileName);
            EO.Pdf.HtmlToPdf.Options.AllowLocalAccess = true;

            string url = "";
            url = string.Format("{0}/Reports-Generator/BatchNumber/BatchNumberViewer.aspx?batchNumber=" + batchNumber, BaseFullUrl);

            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertUrl(url);

            // save pdf document
            doc.Save(pdfFilePath);

            // close pdf document
            doc.Close();
            return this.Request.CreateResponse(HttpStatusCode.OK, fileName);
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Report/GeneratePRSNReport2")]
        public HttpResponseMessage GeneratePRSNReport2(long projectId, string projectNumber)
        {
            string fileName = string.Format("{0}_{1}_{2}.pdf", "GLS", "_PRSN_", projectId);
            EO.Pdf.HtmlToPdf.Options.PageSize = new SizeF(8.5f, 11f);
            //Set margins to 0.5 inch on all sides
            //EO.Pdf.HtmlToPdf.Options.OutputArea = new RectangleF(0.1f, 0.1f, 7.9f, 10.9f);
            EO.Pdf.HtmlToPdf.Options.OutputArea = new RectangleF(0.2F, 1.5F, 8.0F, 8.5F);
            EO.Pdf.PdfDocument theDoc = new EO.Pdf.PdfDocument();
            StringBuilder headerHtml = new StringBuilder();
            headerHtml.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"border-bottom: solid 2px #e5e5e5; padding: 5px;\">");
            headerHtml.AppendLine("<tr>");
            headerHtml.AppendLine("<td style=\"width:20%; padding-left:10px;\">");
            headerHtml.AppendLine("<img width=\"85\" src=\"http://localhost:62797/img/logo.png\" />");
            headerHtml.AppendLine("</td>");
            headerHtml.AppendLine("<td style=\"width:45%; line-height: 20px;\">");
            headerHtml.AppendLine("<div>GUARDIAN LAB SERVICES</div>");
            headerHtml.AppendLine("<div>11-2280 39 AVE NE</div>");
            headerHtml.AppendLine("<div>CALGARY, AB. T2E 6P7</div>");
            headerHtml.AppendLine("<div>PHONE:(403) 452-1003</div>");
            headerHtml.AppendLine("<div>e-mail: customerservice@guardianlab.ca</div>");
            headerHtml.AppendLine("</td>");
            headerHtml.AppendLine("<td style=\"width:35%\">");
            headerHtml.AppendLine(string.Format("<div style=\"font-size:20px;font-weight:bold;\">Project No: {0}</div>", projectNumber));
            headerHtml.AppendLine("</td>");
            headerHtml.AppendLine("</tr>");
            headerHtml.AppendLine("</table>");

            StringBuilder footerHtml = new StringBuilder();
            footerHtml.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"padding: 5px;\">");
            footerHtml.AppendLine("<tr>");
            footerHtml.AppendLine("<td style=\"width:50%; padding-left:10px; text-align:left;\">");
            footerHtml.AppendLine(string.Format("{0}", DateTime.Now.ToString()));
            footerHtml.AppendLine("</td>");
            footerHtml.AppendLine("<td style=\"width:50%; text-align:right;\">");
            footerHtml.AppendLine(string.Format("Page"));
            footerHtml.AppendLine("</td>");
            footerHtml.AppendLine("</tr>");
            footerHtml.AppendLine("</table>");
            EO.Pdf.HtmlToPdf.Options.HeaderHtmlFormat = headerHtml.ToString();
            EO.Pdf.HtmlToPdf.Options.FooterHtmlFormat = "<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"padding: 5px;\"><tr><td style=\"width:50%; padding-left:10px; text-align:left;\">"+ DateTime.Now.ToString() + "</td><td style=\"width:50%; text-align:right;\">Page Number: {page_number}</td></tr></table>";//footerHtml.ToString();
            string pdfFilePath = string.Format("C:\\GLS\\gls-pms-api\\Reports\\PRSN\\{0}", string.Format("{0}_{1}_{2}.pdf", "GLS", "_PRSN_", projectId));
            EO.Pdf.HtmlToPdf.Options.AllowLocalAccess = true;

            //string url = string.Format("http://localhost:62797/Reports-Generator/Labels/LabelViewer.aspx?projectIds=" + projectIds + "&sampleId="+ sampleId);
            string url = string.Format("{0}/Reports-Generator/PRSN/PRSNViewer.aspx?projectId=" + projectId , BaseFullUrl);
            EO.Pdf.HtmlToPdf.ConvertUrl(url, theDoc);
            FileInfo fileInfo = new FileInfo(pdfFilePath);
            try
            {
                fileInfo = new FileInfo(pdfFilePath);
                // if (fileInfo.Exists)
                // fileInfo.Delete();
                lock (theDoc)
                {
                    theDoc.Save(pdfFilePath);
                    // DownloadPhotoReport(pdfFilePath);

                }
            }
            catch (Exception ex)
            {
                //fileInfo = new FileInfo(pdfFilePath);
                //if (fileInfo.Exists)
                //fileInfo.Delete();
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, fileName);
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Report/GetReport2")]
        public HttpResponseMessage GetReport2(long projectId, string projectType)
        {
            string projectTypeStr = projectType.Replace(" ", "");
            string fileName = string.Format("{0}_{1}_{2}_{3}.pdf", "GLS", "_Report_",projectType, projectId);
            EO.Pdf.HtmlToPdf.Options.PageSize = new SizeF(8.5f, 11f);
            //Set margins to 0.5 inch on all sides
            //EO.Pdf.HtmlToPdf.Options.OutputArea = new RectangleF(0.1f, 0.1f, 7.9f, 10.9f);
            ProjectViewModel project = new ProjectService().GetByProjectId(projectId);
            if (projectType == "PLM")
            {
                EO.Pdf.HtmlToPdf.Options.OutputArea = new RectangleF(0.2F, 1.6F, 8.0F, 9F);
            }
            else if(projectType == "PCM")
            {
                EO.Pdf.HtmlToPdf.Options.OutputArea = new RectangleF(0.2F, 2.6F, 8.0F, 6F);
            }
            else if (projectType == "Mold")
            {
                EO.Pdf.HtmlToPdf.Options.OutputArea = new RectangleF(0.2F, 1.6F, 8.0F, 9F);
            }
            else if (projectType == "Mold Tape Lift")
            {
                EO.Pdf.HtmlToPdf.Options.OutputArea = new RectangleF(0.2F, 1.6F, 8.0F, 9F);
            }
            EO.Pdf.PdfDocument theDoc = new EO.Pdf.PdfDocument();
            StringBuilder headerHtml = new StringBuilder();
            headerHtml.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"border-bottom: solid 2px #e5e5e5; padding: 0px 0px 5px 0px;\">");
            headerHtml.AppendLine("<tr>");
            headerHtml.AppendLine("<td style=\"width:15%; padding-left:10px;\">");
            headerHtml.AppendLine("<img width=\"85\" src=\"http://localhost:62797/img/logo.png\" />");
            headerHtml.AppendLine("</td>");
            headerHtml.AppendLine("<td style=\"width:30%; line-height: 20px;\">");
            headerHtml.AppendLine("<div>GUARDIAN LAB SERVICES</div>");
            headerHtml.AppendLine("<div>11-2280 39 AVE NE</div>");
            headerHtml.AppendLine("<div>CALGARY, AB. T2E 6P7</div>");
            headerHtml.AppendLine("<div>PHONE:(403) 452-1003</div>");
            //headerHtml.AppendLine("<div>e-mail: customerservice@guardianlab.ca</div>");
            headerHtml.AppendLine("</td>");
            headerHtml.AppendLine("<td rowspan=\"2\" style=\"width:55%;border-radius: 25px;border:solid 2px #cccccc; \">");
            headerHtml.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"padding:5px 5px 5px 10px;font-size: 14px;\">");
            headerHtml.AppendLine("<tr>");
            headerHtml.AppendLine("<td>Client/PM:</td>");
            headerHtml.AppendLine(string.Format("<td colspan=\"2\">{0}</td>", project.ClientName));
            headerHtml.AppendLine("</tr>");
            headerHtml.AppendLine("<tr>");
            headerHtml.AppendLine("<td>Phone:</td>");
            headerHtml.AppendLine(string.Format("<td colspan=\"2\">{0}</td>", project.OfficePhone));
            headerHtml.AppendLine("</tr>");
            headerHtml.AppendLine("<tr>");
            headerHtml.AppendLine("<td>Project Number:</td>");
            headerHtml.AppendLine(string.Format("<td colspan=\"2\">{0}</td>", project.ProjectNumber));
            headerHtml.AppendLine("</tr>");
            headerHtml.AppendLine("<tr>");
            headerHtml.AppendLine("<td>Report To:</td>");
            headerHtml.AppendLine(string.Format("<td colspan=\"2\">{0}</td>", project.ReportToName));
            headerHtml.AppendLine("</tr>");
            headerHtml.AppendLine("<tr>");
            headerHtml.AppendLine("<td>Date of Submitted:</td>");
            headerHtml.AppendLine(string.Format("<td>{0}</td>", Convert.ToDateTime(project.ReceivedDate).ToShortDateString()));
            headerHtml.AppendLine(string.Format("<td rowspan=\"2\" style=\"padding-left:10px\"><img width=\"150\" src=\"{0}\" /></td>", "http://localhost:62797/img/A4133.jpg"));
            headerHtml.AppendLine("</tr>");
            headerHtml.AppendLine("<tr>");
            headerHtml.AppendLine("<td>Date of Submitted:</td>");
            headerHtml.AppendLine(string.Format("<td>{0}</td>", Convert.ToDateTime(project.DateOfReported).ToShortDateString()));
            headerHtml.AppendLine("</tr>");
            headerHtml.AppendLine("</table>");
            headerHtml.AppendLine("</td>");
            
            headerHtml.AppendLine("</tr>");
            headerHtml.AppendLine("<tr>");
            headerHtml.AppendLine("<td colspan=\"2\" style=\"width:40%; padding-left:10px;\"><div>e-mail: customerservice@guardianlab.ca</div></td>");
            headerHtml.AppendLine("</tr>");
            headerHtml.AppendLine("</table>");
            if (projectType == "PCM")
            {
                headerHtml.AppendLine("<div style=\"width:100 %;border-radius: 25px;border: solid 2px #cccccc;margin-top:10px;\"><div style=\"padding:5px;\"><b>Job Name:</b> " + project.JobNumber + "</div></div>");
                headerHtml.AppendLine("<div style=\"width:100%; text-align: right;\">");
                headerHtml.AppendLine("<table border=\"0\" style=\"width:100%\"><tr>");
                headerHtml.AppendLine(string.Format("<td style=\"border:none;width:86%; text-align:right;\">Sampling Date: </td><td style=\"text-align:left\">{0}</td>", (project?.SamplingDate != null ? ((DateTime)project?.SamplingDate).ToShortDateString():"")));
                headerHtml.AppendLine("</tr>");
                headerHtml.AppendLine("<tr>");
                headerHtml.AppendLine(string.Format("<td style=\"border:none;width:86%; text-align:right;\">Date Analyzed: </td><td style=\"text-align:left\">{0}</td>", (project?.DateOfAnalyzed != null ? ((DateTime)project?.DateOfAnalyzed).ToShortDateString() : "")));
                headerHtml.AppendLine("</tr></table>");
                /*if (project?.SamplingDate != null)
                    headerHtml.AppendLine(string.Format("<div style=\"padding-top:5px;padding-bottom:5px\">Sampling Date: {0}</div>", ((DateTime)project?.SamplingDate).ToShortDateString()));
                else
                    headerHtml.AppendLine(string.Format("<div style=\"padding-top:5px;padding-bottom:5px\">Sampling Date: {0}</div>", ""));
                if (project?.DateOfAnalyzed != null)
                    headerHtml.AppendLine(string.Format("<div>Date Analyzed:{0}</div>", ((DateTime)project?.DateOfAnalyzed).ToShortDateString()));
                else
                    headerHtml.AppendLine(string.Format("<div>Date Analyzed:{0}</div>", ""));*/
                headerHtml.AppendLine("</div>");
            }

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
            footerHtml.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"padding: 5px;\">");
            footerHtml.AppendLine("<tr>");
            footerHtml.AppendLine("<td style=\"width:50%; padding-left:10px; text-align:left;\">");
            footerHtml.AppendLine(string.Format("<div><img src=\"{0}\" /></div>", labAnalystSignUrl));
            footerHtml.AppendLine(string.Format("<div>{0}</div>", projectAnalystName));
            footerHtml.AppendLine(string.Format("<div>Lab Analyst {0}</div>", projectAnalystDiploma));
            footerHtml.AppendLine("</td>");
            footerHtml.AppendLine("<td style=\"width:50%; padding-left:10px; text-align:left;\">");
            footerHtml.AppendLine(string.Format("<div><img src=\"{0}\" /></div>", labrotaryManagerSignUrl));
            footerHtml.AppendLine(string.Format("<div>{0}</div>", labrotaryManagerName));
            footerHtml.AppendLine(string.Format("<div>Laboratory Manager {0}</div>", labrotaryManagerDiploma));
            footerHtml.AppendLine("</td>");
            footerHtml.AppendLine("</tr>");
            footerHtml.AppendLine("<tr>");
            footerHtml.AppendLine("<td style=\"width:50%; padding-left:10px; text-align:left;\">");
            footerHtml.AppendLine(string.Format("{0} test", DateTime.Now.ToString()));
            footerHtml.AppendLine("</td>");
            footerHtml.AppendLine("<td style=\"width:50%; text-align:right;\">");
            footerHtml.AppendLine(string.Format("Page"));
            footerHtml.AppendLine("</td>");
            footerHtml.AppendLine("</tr>");
            footerHtml.AppendLine("</table>");
            EO.Pdf.HtmlToPdf.Options.HeaderHtmlFormat = headerHtml.ToString();
            EO.Pdf.HtmlToPdf.Options.FooterHtmlFormat = @"<table cellpadding='0' 
                        cellspacing='0' width='100%' style='padding: 5px;border:solid 1px red;height: 150px;margin-top:-100px;'>"+
                        //"<tr><td style='width:50%; padding-left:10px; text-align:left;'>" + string.Format("<div><img src=\"{0}\" /></div>", labAnalystSignUrl) +
                        //string.Format("<div>{0}</div>", projectAnalystName) + string.Format("<div>Lab Analyst {0}</div>", projectAnalystDiploma) + "</td>" +
                        //"<td style='width:50%; padding-left:10px; text-align:left;'>" + string.Format("<div><img src='{0}' /></div>", labrotaryManagerSignUrl) +
                        //string.Format("<div>{0}</div>", labrotaryManagerName) + string.Format("<div>Laboratory Manager {0}</div>", labrotaryManagerDiploma) + "</td></tr>" +
                        "<tr><td style='width:50%; padding-left:10px; text-align:left;'>" + DateTime.Now.ToString() + 
                        "</td><td style='width:50%; text-align:right;'>Page Number: {page_number}</td></tr>" +                        
                        "</table>";//footerHtml.ToString();

            string pdfFilePath = string.Format("C:\\GLS\\gls-pms-api\\Reports\\"+ projectTypeStr + "\\{0}", fileName);
            EO.Pdf.HtmlToPdf.Options.AllowLocalAccess = true;

            //string url = string.Format("http://localhost:62797/Reports-Generator/Labels/LabelViewer.aspx?projectIds=" + projectIds + "&sampleId="+ sampleId);
            string url = "";
            
            url = string.Format("{0}/Reports-Generator/Reports/" + projectType + "/ReportViewer.aspx?projectId=" + projectId, BaseFullUrl);
            //Provide AfterRenderPage handler
            //EO.Pdf.HtmlToPdf.Options.AfterRenderPage = new EO.Pdf.PdfPageEventHandler(On_AfterRenderPage);

            EO.Pdf.HtmlToPdf.ConvertUrl(url, theDoc);
            /*for (int i = 0; i < theDoc.Pages.Count; i++)
            {
                EO.Pdf.HtmlToPdf.Options.OutputArea = new RectangleF(0, 8, 8.5f, 5);
                EO.Pdf.HtmlToPdf.ConvertHtml("footer content", theDoc.Pages[i]);
                EO.Pdf.HtmlToPdf.Options.OutputArea = new RectangleF(0.1f, 8.5f, 8.1f, 2.5f);

                //Render an image and a horizontal line. Note the
                //second argument is the PdfPage object
                EO.Pdf.HtmlToPdf.ConvertHtml(@"
            <div style='border:solid 1px red;height:150px;'>Footer Content Test</div> ",
                    theDoc.Pages[i]);
            }*/
            FileInfo fileInfo = new FileInfo(pdfFilePath);
            try
            {
                fileInfo = new FileInfo(pdfFilePath);
                // if (fileInfo.Exists)
                // fileInfo.Delete();
                lock (theDoc)
                {
                    theDoc.Save(pdfFilePath);
                    // DownloadPhotoReport(pdfFilePath);

                }
            }
            catch (Exception ex)
            {
                //fileInfo = new FileInfo(pdfFilePath);
                //if (fileInfo.Exists)
                //fileInfo.Delete();
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, fileName);
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Report/generatePCMRawDataReport")]
        public HttpResponseMessage generatePCMRawDataReport(long projectId)
        {
            string reportName = "";
            Project project = new ProjectService().GetByID(projectId);
            if (project != null)
            {
                reportName = project.JobNumber.Replace(",", "") + "-" + project.ProjectNumber;
                reportName = reportName.Replace("#", "");
            }
            string fileName = string.Format("{0}.pdf", reportName);
            //string fileName = string.Format("{0}_{1}_{2}.pdf", "GLS", "_PRSN_", projectId);
            // get parameters
            string headerUrl = string.Format("{0}/Header-PCM-Raw.aspx?projectId=" + projectId, BaseFullUrl);
            string footerUrl = string.Format("{0}/Footer-PCM-Raw.aspx?projectId=" + projectId, BaseFullUrl);

            bool showHeaderOnFirstPage = true;
            bool showHeaderOnOddPages = true;
            bool showHeaderOnEvenPages = true;

            int headerHeight = 90;
            int footerHeight = 130;
            bool showFooterOnFirstPage = true;
            bool showFooterOnOddPages = true;
            bool showFooterOnEvenPages = true;
            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            System.Drawing.SizeF size = new System.Drawing.SizeF(841.68f, 595.44f);

            converter.Options.PdfPageSize = PdfPageSize.Custom; //new SizeF(1, 0.5f);
            converter.Options.PdfPageCustomSize = size;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Landscape;

            // header settings
            converter.Options.DisplayHeader = true;
            converter.Header.DisplayOnFirstPage = showHeaderOnFirstPage;
            converter.Header.DisplayOnOddPages = showHeaderOnOddPages;
            converter.Header.DisplayOnEvenPages = showHeaderOnEvenPages;
            converter.Header.Height = headerHeight;

            PdfHtmlSection headerHtml = new PdfHtmlSection(headerUrl);
            headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            converter.Header.Add(headerHtml);

            // footer settings
            converter.Options.DisplayFooter = showFooterOnFirstPage ||
                showFooterOnOddPages || showFooterOnEvenPages;
            converter.Footer.DisplayOnFirstPage = showFooterOnFirstPage;
            converter.Footer.DisplayOnOddPages = showFooterOnOddPages;
            converter.Footer.DisplayOnEvenPages = showFooterOnEvenPages;
            converter.Footer.Height = footerHeight;

            PdfHtmlSection footerHtml = new PdfHtmlSection(footerUrl);
            footerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            converter.Footer.Add(footerHtml);

            // page numbers can be added using a PdfTextSection object
            PdfTextSection text = new PdfTextSection(5, 100,
                "Page: {page_number} of {total_pages}  ",
                new System.Drawing.Font("Arial", 8));
            text.HorizontalAlign = PdfTextHorizontalAlign.Right;
            text.VerticalAlign = PdfTextVerticalAlign.Bottom;
            converter.Footer.Add(text);



            string pdfFilePath = string.Format("C:\\GLS\\gls-pms-api\\Reports\\PCM\\RawData\\{0}", fileName);
            EO.Pdf.HtmlToPdf.Options.AllowLocalAccess = true;

            string url = "";
            url = string.Format("{0}/Reports-Generator/Reports/PCM/ReportViewerRawData.aspx?projectId=" + projectId, BaseFullUrl);

            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertUrl(url);

            // save pdf document
            doc.Save(pdfFilePath);

            // close pdf document
            doc.Close();
            return this.Request.CreateResponse(HttpStatusCode.OK, fileName);
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Report/generateMoldRawDataReport")]
        public HttpResponseMessage generateMoldRawDataReport(long projectId)
        {
            string projectType = "";
            
            string reportName = "";
            Project project = new ProjectService().GetByID(projectId);
            if (project != null)
            {
                projectType = project.ProjectType;
                reportName = project.JobNumber.Replace(",", "").Replace("#", "") + "-" + project.ProjectNumber +"-RawData";
            }
            string projectTypeStr = projectType.Replace(" ", "");
            string fileName = string.Format("{0}.pdf", reportName);
            // get parameters
            string headerUrl = string.Format("{0}/Header-Mold-Raw-Print.aspx?projectId=" + projectId, BaseFullUrl);
            string footerUrl = string.Format("{0}/Footer-Mold-Raw-Data.aspx?projectId=" + projectId, BaseFullUrl);
            int headerHeight = 150;
            bool showHeaderOnFirstPage = true;
            bool showHeaderOnOddPages = true;
            bool showHeaderOnEvenPages = true;
            float textY = 200;

            int footerHeight = 220;
            if (projectType == "PLM")
                headerHeight = 170;
            else if (projectType == "PCM")
            {
                footerHeight = 180;
                headerHeight = 155;
                textY = 168;
            }
            else if (projectType == "Mold")
            {
                footerHeight = 142;
                headerHeight = 138;
                textY = 132;
            }
            else if (projectType == "Mold Tape Lift")
            {
                footerHeight = 140;
                headerHeight = 140;
                textY = 128;
            }
            bool showFooterOnFirstPage = true;
            bool showFooterOnOddPages = true;
            bool showFooterOnEvenPages = true;
            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            // header settings
            converter.Options.DisplayHeader = true;
            converter.Header.DisplayOnFirstPage = showHeaderOnFirstPage;
            converter.Header.DisplayOnOddPages = showHeaderOnOddPages;
            converter.Header.DisplayOnEvenPages = showHeaderOnEvenPages;
            converter.Header.Height = headerHeight;

            PdfHtmlSection headerHtml = new PdfHtmlSection(headerUrl);
            headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            converter.Header.Add(headerHtml);

            // footer settings
            converter.Options.DisplayFooter = showFooterOnFirstPage ||
                showFooterOnOddPages || showFooterOnEvenPages;
            converter.Footer.DisplayOnFirstPage = showFooterOnFirstPage;
            converter.Footer.DisplayOnOddPages = showFooterOnOddPages;
            converter.Footer.DisplayOnEvenPages = showFooterOnEvenPages;
            converter.Footer.Height = footerHeight;

            PdfHtmlSection footerHtml = new PdfHtmlSection(footerUrl);
            footerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            converter.Footer.Add(footerHtml);

            // page numbers can be added using a PdfTextSection object
            PdfTextSection text = new PdfTextSection(5, textY,
                "Page: {page_number} of {total_pages}  ",
                new System.Drawing.Font("Arial", 8));
            text.HorizontalAlign = PdfTextHorizontalAlign.Right;
            text.VerticalAlign = PdfTextVerticalAlign.Bottom;
            converter.Footer.Add(text);

            string pdfFilePath = string.Format("C:\\GLS\\gls-pms-api\\Reports\\" + projectTypeStr + "\\RawData\\{0}", fileName);
            EO.Pdf.HtmlToPdf.Options.AllowLocalAccess = true;

            //string url = string.Format("http://localhost:62797/Reports-Generator/Labels/LabelViewer.aspx?projectIds=" + projectIds + "&sampleId="+ sampleId);
            string url = "";

            url = string.Format("{0}/Reports-Generator/Reports/" + projectTypeStr + "/ReportViewerRawData.aspx?projectId=" + projectId, BaseFullUrl);

            // create a new pdf document converting an url
            try
            {
                PdfDocument doc = converter.ConvertUrl(url);
                // custom header on page 3
                /*if (doc.Pages.Count >= 3)
                {
                    PdfPage page = doc.Pages[2];

                    PdfTemplate customHeader = doc.AddTemplate(
                        page.PageSize.Width, headerHeight);
                    page.CustomHeader = customHeader;
                }*/
                // get image path
                string imgFile = string.Format("C:\\GLS\\gls-pms-api\\img\\logo.png");
                // stamp all pages - add a template containing an image to the bottom right of 
                // the page the image should repeat on all pdf pages automatically
                PdfTemplate template = doc.AddTemplate(doc.Pages[0].ClientRectangle);
                PdfImageElement img = new PdfImageElement(
                    doc.Pages[0].ClientRectangle.Width - 500,
                    0, imgFile);
                img.Transparency = 8;
                template.Add(img);

                // save pdf document
                doc.Save(pdfFilePath);

                // close pdf document
                doc.Close();
            }
            catch (Exception ex)
            {

            }


            return this.Request.CreateResponse(HttpStatusCode.OK, fileName);
        }

        //This function is called after every page is created
        private void On_AfterRenderPage(
            object sender, EO.Pdf.PdfPageEventArgs e)
        {
            EO.Pdf.PdfDocument theDoc = e.Page.Document;
            for (int i = 0; i < theDoc.Pages.Count; i++)
            {
                //Set the output area to the top portion of the page. Note
                //this does not change the output area of the original
                //conversion from which we are called
                float y = ((float)8.5*(i+1));
                EO.Pdf.HtmlToPdf.Options.OutputArea = new RectangleF(0.1f, 8.5f, 8.1f, 2.5f);

                //Render an image and a horizontal line. Note the
                //second argument is the PdfPage object
                EO.Pdf.HtmlToPdf.ConvertHtml(@"
            <div style='border:solid 1px red;height:150px;'>Footer Content Test</div> ",
                    theDoc.Pages[i]);
            }
            
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Report/GetReport")]
        public HttpResponseMessage GetReport(long projectId, string projectType)
        {
            string projectTypeStr = projectType.Replace(" ", "");
            string reportName = "";
            Project project = new ProjectService().GetByID(projectId);
            if(project != null)
            {
                //reportName = project.JobNumber.Replace(",", "").Replace("#","") + "-" + project.ProjectNumber;
                //Regex reg = new Regex(@"[^0-9A-Za-z ,]");
                Regex reg = new Regex(@"[\\/:*?""<>|#]");
                string jobnumber = reg.Replace(project.JobNumber, string.Empty);
                reportName = jobnumber + "-" + project.ProjectNumber;
            }
            string fileName = string.Format("{0}.pdf", reportName);
            // get parameters
            string headerUrl = string.Format("{0}/Header.aspx?projectId=" + projectId, BaseFullUrl);
            string footerUrl = string.Format("{0}/Footer.aspx?projectId=" + projectId, BaseFullUrl);
            int headerHeight = 150;
            bool showHeaderOnFirstPage = true;
            bool showHeaderOnOddPages = true;
            bool showHeaderOnEvenPages = true;
            float textY = 200;

            int footerHeight = 220;
            if (projectType == "PLM")
            {
                headerHeight = 190;
                footerHeight = 210;
            }
            else if (projectType == "PCM")
            {
                footerHeight = 190;
                headerHeight = 165;
                textY = 168;
            }
            else if (projectType == "Mold")
            {
                footerHeight = 142;
                headerHeight = 138;
                textY = 132;
            }
            else if (projectType == "Mold Tape Lift")
            {
                footerHeight = 138;
                headerHeight = 135;
                textY = 128;
            }
            bool showFooterOnFirstPage = true;
            bool showFooterOnOddPages = true;
            bool showFooterOnEvenPages = true;
            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            // header settings
            converter.Options.DisplayHeader = true;
            converter.Header.DisplayOnFirstPage = showHeaderOnFirstPage;
            converter.Header.DisplayOnOddPages = showHeaderOnOddPages;
            converter.Header.DisplayOnEvenPages = showHeaderOnEvenPages;
            converter.Header.Height = headerHeight;

            PdfHtmlSection headerHtml = new PdfHtmlSection(headerUrl);
            headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            converter.Header.Add(headerHtml);

            // footer settings
            converter.Options.DisplayFooter = showFooterOnFirstPage ||
                showFooterOnOddPages || showFooterOnEvenPages;
            converter.Footer.DisplayOnFirstPage = showFooterOnFirstPage;
            converter.Footer.DisplayOnOddPages = showFooterOnOddPages;
            converter.Footer.DisplayOnEvenPages = showFooterOnEvenPages;
            converter.Footer.Height = footerHeight;

            PdfHtmlSection footerHtml = new PdfHtmlSection(footerUrl);
            footerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            converter.Footer.Add(footerHtml);            

            // page numbers can be added using a PdfTextSection object
            PdfTextSection text = new PdfTextSection(5, textY,
                "Page: {page_number} of {total_pages}  ",
                new System.Drawing.Font("Arial", 8));
            text.HorizontalAlign = PdfTextHorizontalAlign.Right;
            text.VerticalAlign = PdfTextVerticalAlign.Bottom;
            converter.Footer.Add(text);

            string pdfFilePath = string.Format("C:\\GLS\\gls-pms-api\\Reports\\" + projectTypeStr + "\\{0}", fileName);
            EO.Pdf.HtmlToPdf.Options.AllowLocalAccess = true;

            //string url = string.Format("http://localhost:62797/Reports-Generator/Labels/LabelViewer.aspx?projectIds=" + projectIds + "&sampleId="+ sampleId);
            string url = "";

            url = string.Format("{0}/Reports-Generator/Reports/" + projectTypeStr + "/ReportViewer.aspx?projectId=" + projectId, BaseFullUrl);

            // create a new pdf document converting an url
            try
            {
                //converter.Options.SecurityOptions.CanEditContent = false;
                //converter.Options.SecurityOptions.OwnerPassword = "Garybilly#1234";
                PdfDocument doc = converter.ConvertUrl(url);
                //doc.Security.CanEditContent = false;
                //doc.Security.CanEditAnnotations = false;
                //doc.Security.CanCopyContent = false;
                //doc.Security.OwnerPassword = "Garybilly#1234";
                // custom header on page 3
                /*if (doc.Pages.Count >= 3)
                {
                    PdfPage page = doc.Pages[2];

                    PdfTemplate customHeader = doc.AddTemplate(
                        page.PageSize.Width, headerHeight);
                    page.CustomHeader = customHeader;
                }*/
                // get image path
                string imgFile = string.Format("C:\\GLS\\gls-pms-api\\img\\logo.png");
                // stamp all pages - add a template containing an image to the bottom right of 
                // the page the image should repeat on all pdf pages automatically
                PdfTemplate template = doc.AddTemplate(doc.Pages[0].ClientRectangle);
                PdfImageElement img = new PdfImageElement(
                    doc.Pages[0].ClientRectangle.Width - 500,
                    0, imgFile);
                img.Transparency = 8;
                template.Add(img);
                

                // save pdf document
                doc.Save(pdfFilePath);

                // close pdf document
                doc.Close();
            }
            catch(Exception ex)
            {

            }

            
            return this.Request.CreateResponse(HttpStatusCode.OK, fileName);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Report/GetAsbestosReport")]
        public HttpResponseMessage GetAsbestosReport(long projectId, string needSecondPage)
        {
            //string projectTypeStr = projectType.Replace(" ", "");
            string reportName = "";
            Project project = new ProjectService().GetByID(projectId);
            if (project != null)
            {
                //reportName = project.JobNumber.Replace(",", "").Replace("#","") + "-" + project.ProjectNumber;
                //Regex reg = new Regex(@"[^0-9A-Za-z ,]");
                Regex reg = new Regex(@"[\\/:*?""<>|#]");
                string jobnumber = reg.Replace(project.JobNumber, string.Empty);
                reportName = jobnumber + "-" + project.ProjectNumber;
            }
            string fileName = string.Format("{0}.pdf", reportName);
            // get parameters
            string headerUrl = string.Format("{0}/AsbestosHeader.aspx?projectId=" + projectId + "&needSecondPage=" + needSecondPage, BaseFullUrl);
            string footerUrl = string.Format("{0}/AsbestosFooter.aspx?projectId=" + projectId + "&needSecondPage=" + needSecondPage, BaseFullUrl);
            int headerHeight = 102;
            bool showHeaderOnFirstPage = true;
            bool showHeaderOnOddPages = true;
            bool showHeaderOnEvenPages = true;
            float textY = 0;

            int footerHeight = 10;

            bool showFooterOnFirstPage = true;
            bool showFooterOnOddPages = true;
            bool showFooterOnEvenPages = true;
            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            System.Drawing.SizeF size = new System.Drawing.SizeF(841.68f, 595.44f);

            converter.Options.PdfPageSize = PdfPageSize.Custom; //new SizeF(1, 0.5f);
            converter.Options.PdfPageCustomSize = size;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Landscape;
            converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;

            // header settings
            converter.Options.DisplayHeader = true;
            converter.Header.DisplayOnFirstPage = showHeaderOnFirstPage;
            converter.Header.DisplayOnOddPages = showHeaderOnOddPages;
            converter.Header.DisplayOnEvenPages = showHeaderOnEvenPages;
            converter.Header.Height = headerHeight;

            PdfHtmlSection headerHtml = new PdfHtmlSection(headerUrl);
            headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            headerHtml.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
            converter.Header.Add(headerHtml);

            // footer settings
            converter.Options.DisplayFooter = showFooterOnFirstPage ||
                showFooterOnOddPages || showFooterOnEvenPages;
            converter.Footer.DisplayOnFirstPage = showFooterOnFirstPage;
            converter.Footer.DisplayOnOddPages = showFooterOnOddPages;
            converter.Footer.DisplayOnEvenPages = showFooterOnEvenPages;
            converter.Footer.Height = footerHeight;

            PdfHtmlSection footerHtml = new PdfHtmlSection(footerUrl);
            footerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            converter.Footer.Add(footerHtml);

            // page numbers can be added using a PdfTextSection object
            PdfTextSection text = new PdfTextSection(5, textY,
                "Page: {page_number} of {total_pages}  ",
                new System.Drawing.Font("Arial", 8));
            text.HorizontalAlign = PdfTextHorizontalAlign.Right;
            text.VerticalAlign = PdfTextVerticalAlign.Bottom;
            converter.Footer.Add(text);

            string pdfFilePath = string.Format("C:\\GLS\\gls-pms-api\\Reports\\Asbestos\\{0}", fileName);
            EO.Pdf.HtmlToPdf.Options.AllowLocalAccess = true;

            //string url = string.Format("http://localhost:62797/Reports-Generator/Labels/LabelViewer.aspx?projectIds=" + projectIds + "&sampleId="+ sampleId);
            string url = "";

            url = string.Format("{0}/Reports-Generator/Reports/Asbestos/ReportViewer.aspx?projectId=" + projectId + "&needSecondPage="+ needSecondPage, BaseFullUrl);

            // create a new pdf document converting an url
            try
            {
                PdfDocument doc = converter.ConvertUrl(url);
                // custom header on page 3
                /*if (doc.Pages.Count >= 3)
                {
                    PdfPage page = doc.Pages[2];

                    PdfTemplate customHeader = doc.AddTemplate(
                        page.PageSize.Width, headerHeight);
                    page.CustomHeader = customHeader;
                }*/
                // get image path
                string imgFile = string.Format("C:\\GLS\\gls-pms-api\\img\\logo.png");
                // stamp all pages - add a template containing an image to the bottom right of 
                // the page the image should repeat on all pdf pages automatically
                PdfTemplate template = doc.AddTemplate(doc.Pages[0].ClientRectangle);
                PdfImageElement img = new PdfImageElement(
                    doc.Pages[0].ClientRectangle.Width - 500,
                    0, imgFile);
                img.Transparency = 8;
                template.Add(img);

                // save pdf document
                doc.Save(pdfFilePath);

                // close pdf document
                doc.Close();
            }
            catch (Exception ex)
            {

            }


            return this.Request.CreateResponse(HttpStatusCode.OK, fileName);
        }

        public static string BaseFullUrl
        {
            get
            {
                string url = (HttpContext.Current.Request.Url.AbsoluteUri.Replace(
                   HttpContext.Current.Request.Url.PathAndQuery, "") + HttpContext.Current.Request.ApplicationPath).ToLower();
                if (url.EndsWith("/"))
                    url = url.Substring(0, url.Length - 1);
                return url;
            }
        }
        public void ActivateLicence()
        {
            SelectPdf.GlobalProperties.LicenseKey = "jqW/rry7v668v7uuvL+gvq69v6C/vKC3t7e3";
        }
    }
}