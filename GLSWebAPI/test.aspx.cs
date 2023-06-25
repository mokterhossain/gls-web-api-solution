using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EO.Pdf;

namespace GLSWebAPI
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EO.Pdf.Runtime.AddLicense(
                        "ILfH3LVrs7P9FOKe5ff29ON3hI6xy59Zs/D6DuSn6un26bto4+30EO2s3OnP" +
                        "uIlZl6Sx5+Cl4/MI6YxDl6Sxy59Zl6TNDOOdl/gKG+R2mcng2c+d3aaxIeSr" +
                        "6u0AGbxbqLu/26FZpsKetZ9Zl6TN2uCl4/MI6YxDl6Sxy7uo6ej2Hcin3fOx" +
                        "D+Ct3MGz5K5rrrPD27BwmaQEIOF+7/T6HeSsuPjOzbhoqbvA3a9qr6axIeSr" +
                        "6u0AGbxbqLuzy653hI6xy59Zs/f6Eu2a6/kDEL2hp/sL7rV/ybgK7rWCy/Hb" +
                        "4viq6vzS6Lx1pvf6Eu2a6/kDEL1GgcDAF+ic3PIEEL1GgXXj7fQQ7azcwp61" +
                        "n1mXpM0X6Jzc8gQQyJ21uMHesmqr");
            EO.Pdf.HtmlToPdf.Options.PageSize = new SizeF(3f, 0.95f);
            PdfDocument theDoc = new PdfDocument();
            string pdfFilePath = Server.MapPath(string.Format("~/print/{0}", string.Format("{0}_{1}_{2}.pdf", "CCG", "_CarInvoice_", DateTime.Now.Millisecond)));
            HtmlToPdf.Options.AllowLocalAccess = true;

            string url = string.Format("{0}/test.html", BaseFullUrl);
            HtmlToPdf.ConvertUrl(url, theDoc);
            FileInfo fileInfo = new FileInfo(pdfFilePath);
            try
            {
                lock (theDoc)
                {
                    theDoc.Save(pdfFilePath);
                    // DownloadPhotoReport(pdfFilePath);
                    fileInfo = new FileInfo(pdfFilePath);
                    //if (fileInfo.Exists)
                        //fileInfo.Delete();
                }
            }
            catch (Exception ex)
            {
                fileInfo = new FileInfo(pdfFilePath);
                if (fileInfo.Exists)
                    fileInfo.Delete();
            }
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
        private void DownloadPhotoReport(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            long length = fileInfo.Length;
            Response.ClearContent();
            Response.ContentType = Path.GetExtension(filePath);
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename = {0}", System.IO.Path.GetFileName(filePath)));
            Response.AddHeader("Content-Length", length.ToString("F0"));
            Response.TransmitFile(filePath);
            Response.Flush();
            Response.End();
        }
    }
}