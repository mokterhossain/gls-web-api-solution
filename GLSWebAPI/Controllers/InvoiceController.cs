using GLSPM.Data;
using GLSPM.Data.Modules.BasicModule;
using GLSPM.Data.Modules.InvoiceModule;
using GLSPM.Data.Modules.ProjectManagement;
using GLSPM.Service.Modules.BasicModule;
using GLSPM.Service.Modules.InvoiceModule;
using GLSPM.Service.Modules.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Drawing;
using System.Text;
using SelectPdf;
using System.Text.RegularExpressions;
using System.Web;

namespace GLSWebAPI.Controllers
{
    public class InvoiceController : ApiController
    {
        // GET: Invoice
        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/Invoice/GetInvoiceByProject")]
        public HttpResponseMessage GetInvoiceByProject(long projectId)
        {
            ClientInvoiceViewModel data = new ClientInvoiceService().GetInvoiceByProjectIdNew(Convert.ToInt64(projectId));
            Response<ClientInvoiceViewModel> response = new Response<ClientInvoiceViewModel>();
            if (data == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed.";
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success.";
                response.Result = data;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }
        // GET: Invoice
        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/Invoice/GetInvoiceSettings")]
        public HttpResponseMessage GetInvoiceSettings()
        {
            List<InvoiceSetting> invoiceSetting = new InvoiceSettingService().All();
            
            Response<InvoiceSetting> response = new Response<InvoiceSetting>();
            if (invoiceSetting == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed.";
            }
            else
            {
                InvoiceSetting data = invoiceSetting.FirstOrDefault();
                response.IsSuccess = true;
                response.Message = "Success.";
                response.Result = data;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [System.Web.Http.AcceptVerbs("POST")]
        [HttpGet]
        [Route("api/Invoice/SaveClientInvoice")]
        public HttpResponseMessage SaveClientInvoice([FromBody] ClientInvoiceViewModel clientInvoiceData)
        {
            //string projectTypeStr = projectType.Replace(" ", "");
            string reportName = "";

            ProjectViewModel project = new ProjectService().GetByProjectId(Convert.ToInt64(clientInvoiceData.ProjectId));
            int? clientId = 0;
            string jobNumber = "";
            string invoiceNumber = "";
            if (project != null)
            {
                clientId = project.ClientID;
                jobNumber = project.ProjectNumber;
            }

            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            year = year.Substring(2, 2);
            if (month.Length == 1)
                month = "0" + month;
            if (day.Length == 1)
                day = "0" + day;
            invoiceNumber = year + month + day + jobNumber;
            string fileName = string.Format("{0}.pdf", invoiceNumber);

            SaveInvoice(clientInvoiceData, project, invoiceNumber);

            ClientInvoiceViewModel data = new ClientInvoiceService().GetInvoiceByProjectIdNew(Convert.ToInt64(clientInvoiceData.ProjectId));
            Response<ClientInvoiceViewModel> response = new Response<ClientInvoiceViewModel>();
            if (data == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed.";
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success.";
                response.Result = data;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }

        #region INVOICE GENERATION
        [System.Web.Http.AcceptVerbs("POST")]
        [HttpGet]
        [Route("api/Invoice/GetInvoice")]
        public HttpResponseMessage GetInvoice([FromBody] ClientInvoiceViewModel clientInvoiceData)
        {
            //string projectTypeStr = projectType.Replace(" ", "");
            string reportName = "";
            
            ProjectViewModel project = new ProjectService().GetByProjectId(Convert.ToInt64(clientInvoiceData.ProjectId));
            int? clientId = 0;
            string jobNumber = "";
            string invoiceNumber = "";
            if (project != null)
            {
                clientId = project.ClientID;
                jobNumber = project.ProjectNumber;
            }

            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            year = year.Substring(2, 2);
            if (month.Length == 1)
                month = "0" + month;
            if (day.Length == 1)
                day = "0" + day;
            invoiceNumber = year + month + day + jobNumber;
            string fileName = string.Format("{0}.pdf", invoiceNumber);

            SaveInvoice(clientInvoiceData, project, invoiceNumber);
            GenerateInvoice(clientInvoiceData, project, invoiceNumber, fileName);

            return this.Request.CreateResponse(HttpStatusCode.OK, fileName);
        }
        public void SaveInvoice(ClientInvoiceViewModel clientInvoiceData, Project project, string invoiceNumber)
        {
            
            ClientInvoice clientInvoiceExt = new ClientInvoiceService().GetByProjectID(project.ProjectID);
            if (clientInvoiceExt != null)
            {
                ClientInvoice clientInvoice = new ClientInvoice();
                clientInvoice.Id = clientInvoiceExt.Id;
                clientInvoice.ProjectId = clientInvoiceExt.ProjectId;
                clientInvoice.ClientId = clientInvoiceExt.ClientId;
                clientInvoice.InvoiceNumber = clientInvoiceExt.InvoiceNumber;
                clientInvoice.InvoiceDate = clientInvoiceExt.InvoiceDate;
                clientInvoice.PaymentDueDate = clientInvoiceExt.PaymentDueDate;
                clientInvoice.SubTotal = clientInvoiceData.SubTotal;
                clientInvoice.TaxAmount = clientInvoiceData.TaxAmount;
                clientInvoice.PST = clientInvoiceData.PST;
                clientInvoice.Shipping = clientInvoiceData.Shipping;
                clientInvoice.Discount = clientInvoiceData.Discount;
                clientInvoice.Total = clientInvoiceData.Total;
                clientInvoice.UpdatedOn = DateTime.Now;
                clientInvoice.SampleNote = clientInvoiceData.SampleNote;
                clientInvoice.IsDiscountPercentage = clientInvoiceData.IsDiscountPercentage;
                clientInvoice.DiscountPercentage = clientInvoiceData.DiscountPercentage;
                clientInvoice.AccountsManagerId = clientInvoiceData.AccountsManagerId;
                var editResponse = new ClientInvoiceService().Update(clientInvoice);
                if (editResponse.IsSuccess)
                {
                    List<ClientInvoiceDetail> clientInvoiceDetailExt = new ClientInvoiceDetailService().GetByInvoiceId(editResponse.Result.Id);
                    /*foreach (ClientInvoiceDetail cid in clientInvoiceDetailExt)
                    {
                        var deleteResponse = new ClientInvoiceDetailService().Delete(cid.Id);
                    }*/
                    IList<ClientInvoiceDetail> invoiceDetails = clientInvoiceData.ClientInvoiceDetails;
                    foreach (ClientInvoiceDetail cid in invoiceDetails)
                    {
                        //cid.TotalAmount = cid.Quantity * cid.UnitPrice;
                        if (cid.Id > 0)
                        {
                            var editResponse2 = new ClientInvoiceDetailService().Update(cid);
                        }
                        else
                        {
                            cid.InvoiceId = editResponse.Result.Id;
                            var addResponse2 = new ClientInvoiceDetailService().Add(cid);
                        }
                    }
                }
            }
            else
            {
                ClientInvoice clientInvoice = new ClientInvoice();
                //clientInvoice = new ClientInvoiceViewModel();
                clientInvoice.SampleNote = clientInvoiceData.SampleNote;
                clientInvoice.ProjectId = clientInvoiceData.ProjectId;
                clientInvoice.ClientId = clientInvoiceData.ClientId;
                clientInvoice.InvoiceNumber = invoiceNumber;
                clientInvoice.InvoiceDate = DateTime.Now;
                clientInvoice.PaymentDueDate = DateTime.Now.AddDays(30);
                clientInvoice.SubTotal = clientInvoiceData.SubTotal;
                clientInvoice.TaxAmount = clientInvoiceData.TaxAmount;
                clientInvoice.PST = clientInvoiceData.PST;
                clientInvoice.Shipping = clientInvoiceData.Shipping;
                clientInvoice.Discount = clientInvoiceData.Discount;
                clientInvoice.Total = clientInvoiceData.Total;
                clientInvoice.CreatedOn = DateTime.Now;
                clientInvoice.IsDiscountPercentage = clientInvoiceData.IsDiscountPercentage;
                clientInvoice.DiscountPercentage = clientInvoiceData.DiscountPercentage;
                clientInvoice.AccountsManagerId = clientInvoiceData.AccountsManagerId;
                var addResponse = new ClientInvoiceService().Add(clientInvoice);
                if (addResponse.IsSuccess)
                {
                    List<ClientInvoiceDetail> clientInvoiceDetailExt = new ClientInvoiceDetailService().GetByInvoiceId(addResponse.Result.Id);
                    foreach (ClientInvoiceDetail cid in clientInvoiceDetailExt)
                    {
                        var deleteResponse = new ClientInvoiceDetailService().Delete(cid.Id);
                    }
                    IList<ClientInvoiceDetail> invoiceDetails = clientInvoiceData.ClientInvoiceDetails;
                    foreach (ClientInvoiceDetail cid in invoiceDetails)
                    {
                        //cid.TotalAmount = cid.Quantity * cid.UnitPrice;
                        cid.InvoiceId = addResponse.Result.Id;
                        var addResponse2 = new ClientInvoiceDetailService().Add(cid);
                    }
                }
            }
        }
        public void GenerateInvoice(ClientInvoiceViewModel clientInvoiceData, Project project, string invoiceNumber, string fileName)
        {
            // get parameters
            string headerUrl = string.Format("{0}/InvoiceHeader.aspx?projectId=" + project.ProjectID, BaseFullUrl);
            string footerUrl = string.Format("{0}/InvoiceFooter.aspx?projectId=" + project.ProjectID, BaseFullUrl);
            int headerHeight = 105;
            bool showHeaderOnFirstPage = true;
            bool showHeaderOnOddPages = true;
            bool showHeaderOnEvenPages = true;
            float textY = 170;

            int footerHeight = 180;

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

            string pdfFilePath = string.Format("C:\\GLS\\gls-pms-api\\Reports\\Invoice\\{0}", fileName);
            EO.Pdf.HtmlToPdf.Options.AllowLocalAccess = true;

            //string url = string.Format("http://localhost:62797/Reports-Generator/Labels/LabelViewer.aspx?projectIds=" + projectIds + "&sampleId="+ sampleId);
            string url = "";

            url = string.Format("{0}/Reports-Generator/Invoice/ReportViewer.aspx?projectId=" + project.ProjectID, BaseFullUrl);

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
                    80, imgFile);
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
        }
        #endregion
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