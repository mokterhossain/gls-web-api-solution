using GLSPM.Data.Modules.BasicModule;
using GLSPM.Data.Modules.ProjectManagement;
using GLSPM.Service.Modules.BasicModule;
using GLSPM.Service.Modules.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace GLSWebAPI.Controllers
{
    public class GLSPMController : ApiController
    {
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/GLSPM/GetTest")]
        public HttpResponseMessage GetTest()
        {
            return this.Request.CreateResponse(HttpStatusCode.OK, "Test");
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/GLSPM/GetMoldReportData")]
        public HttpResponseMessage GetMoldReportData()
        {
            MoldReportDataModel moldReportDataFinal = new MoldReportDataModel();
            long ProjectId = 93861;
            ProjectViewModel project = new ProjectService().GetByProjectId(Convert.ToInt64(ProjectId));
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
            string comments = string.Empty;
            Mold mold = new MoldService().GetByProjectID(Convert.ToInt64(ProjectId));
            if (mold != null)
            {
                comments = mold.Comments;
            }
            string projectNumber = "";
            string clientName = "";
            string phone = "";
            string jobNumber = "";
            string reportTo = "";
            string dateOfSubmitted = "";
            string labAnalystSignUrl = "";
            string labrotaryManagerSignUrl = "";
            string projectAnalystName = "";
            string labrotaryManagerName = "";
            string SampledBy = "";
            if (project != null)
            {
                projectNumber = project.ProjectNumber;
                clientName = project.ClientName;
                phone = project.OfficePhone;
                jobNumber = project.JobNumber;
                reportTo = project.ReportingPerson;
                dateOfSubmitted = Convert.ToDateTime(project.ReceivedDate).ToShortDateString();
                labAnalystSignUrl = project.AnalystSignature.Replace("~/", "http://guardianlab.ca/");
                labrotaryManagerSignUrl = project.LabratoryManagerSignature.Replace("~/", "http://guardianlab.ca/");
                projectAnalystName = project.ProjectAnalystName;
                labrotaryManagerName = project.LabratoryManagerName;
                SampledBy = (!string.IsNullOrEmpty(project.SampledBy) ? "Sampled By: " + project.SampledBy : "");
            }
            List<MoldSampleViewModel> moldSample = new MoldSampleService().GetMoldSample(ProjectId);
            List<MoldSampleDetailViewModel> moldSampleDetail = new MoldSampleDetailService().GetMoldSampleDetail(ProjectId);
            List<MoldSporeType> moldSporeType = new MoldSporeTypeService().All();
            decimal numberOfTable = 1;
            decimal numberOfSample = 0;
            if (moldSample != null)
            {
                numberOfSample = moldSample.Count;
            }
            if (numberOfSample > 3)
            {
                numberOfTable = Math.Ceiling((decimal)(numberOfSample / 3));
            }
            List<MoldTableData> moldTableData = new List<MoldTableData>();
            for (int i = 0; i < numberOfTable; i++)
            {
                MoldTableData data = new MoldTableData();
                data.Id = i;
                moldTableData.Add(data);
            }
            string dateOfReported = "";
            string dateOfAnalyzed = "";
            string samplingDate = "";
            string specialNote = "<div style=\"text-align:justify;font-weight:bold;\">Background debris is an indication of the amounts of non-biological particulate matter present on the slide (dust in the air) and is graded from 1 to 4 with 4 indicating the largest amounts. All samples were received in acceptable condition unless noted in the Report Comments portion in the body of the report.Due to the nature of the analyses performed, field blank correction of results is not applied.The results relate only to the items tested. All samples were received in acceptable condition unless noted in the Report Comments portion in the body of the report.Due to the nature of the analyses performed, field blank correction of results is not applied.</div>";
            string projectTitle = string.Format("<div style=\"border-radius: 25px;border:solid 2px #cccccc;padding:10px;page-break-before: always;\"><span style=\"font-weight:bold;\">Job Name:</span> {0} </div>", jobNumber);
            moldReportDataFinal.MoldSample = moldSample;
            moldReportDataFinal.MoldSampleDetail = moldSampleDetail;
            moldReportDataFinal.MoldSporeType = moldSporeType;
            //moldReportDataFinal.NumberOfTable = numberOfTable;
            moldReportDataFinal.MoldTableData = moldTableData;
            moldReportDataFinal.ProjectNumber = projectNumber;
            moldReportDataFinal.ClientName = clientName;
            moldReportDataFinal.ReportTo = reportTo;
            moldReportDataFinal.JobNumber = jobNumber;
            moldReportDataFinal.Phone = phone;
            moldReportDataFinal.DateOfSubmitted = dateOfSubmitted;
            moldReportDataFinal.DateOfReported = dateOfReported;
            moldReportDataFinal.DateOfAnalyzed = dateOfAnalyzed;
            moldReportDataFinal.SpecialNote = specialNote;
            moldReportDataFinal.ProjectTitle = projectTitle;
            moldReportDataFinal.ProjectAnalystName = projectAnalystName;
            moldReportDataFinal.LabrotaryManagerName = labrotaryManagerName;
            moldReportDataFinal.SampledBy = SampledBy;
            moldReportDataFinal.Comments = comments;
            moldReportDataFinal.SampleVolume = sampleVolume.ToString();
            moldReportDataFinal.SamplingDate = samplingDate;
            moldReportDataFinal.AnalystSign = labAnalystSignUrl;
            moldReportDataFinal.LabratoryManagerSign = labrotaryManagerSignUrl;

            return this.Request.CreateResponse(HttpStatusCode.OK, moldReportDataFinal);
        }
        public class MoldReportDataModel
        {
            public List<MoldSampleViewModel> MoldSample { get; set; }
            public List<MoldSampleDetailViewModel> MoldSampleDetail { get; set; }
            public List<MoldSporeType> MoldSporeType { get; set; }
            public List<MoldTableData> MoldTableData { get; set; }
            public string ClientName { get; set; }
            public string Phone { get; set; }
            public string ProjectNumber { get; set; }
            public string JobNumber { get; set; }
            public string ReportTo { get; set; }
            public string DateOfReported { get; set; }
            public string DateOfSubmitted { get; set; }
            public string SamplingDate { get; set; }
            public string ProjectAnalystName { get; set; }
            public string LabrotaryManagerName { get; set; }
            public string SampledBy { get; set; }
            public string Comments { get; set; }
            public string SampleVolume { get; set; }
            public string ProjectTitle { get; set; }
            public string DateOfAnalyzed { get; set; }
            public string SpecialNote { get; set; }
            public string LabratoryManagerSign { get; set; }
            public string AnalystSign { get; set; }

        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/GLSPM/GetDataForMoldReport")]
        public HttpResponseMessage GetDataForMoldReport()
        {
            MoldReportData moldReportDataFinal = new MoldReportData();
            //long ProjectId = 95343;
            long ProjectId = 93861;
            List<MoldViewModel> moldData = new MoldService().GetDataForMoldReport(Convert.ToInt64(ProjectId));
            ProjectViewModel project = new ProjectService().GetByProjectId(Convert.ToInt64(ProjectId));
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
            string comments = string.Empty;
            Mold mold = new MoldService().GetByProjectID(Convert.ToInt64(ProjectId));
            if (mold != null)
            {
                comments = mold.Comments;
            }
            string projectNumber = "";
            string clientName = "";
            string phone = "";
            string jobNumber = "";
            string reportTo = "";
            string dateOfSubmitted = "";
            string labAnalystSignUrl = "";
            string labrotaryManagerSignUrl = "";
            string projectAnalystName = "";
            string labrotaryManagerName = "";
            string SampledBy = "";
            if (project != null)
            {
                projectNumber = project.ProjectNumber;
                clientName = project.ClientName;
                phone = project.OfficePhone;
                jobNumber = project.JobNumber;
                reportTo = project.ReportingPerson;
                dateOfSubmitted = Convert.ToDateTime(project.ReceivedDate).ToShortDateString();
                labAnalystSignUrl = project.AnalystSignature.Replace("~/", "http://guardianlab.ca/");
                labrotaryManagerSignUrl = project.LabratoryManagerSignature.Replace("~/", "http://guardianlab.ca/");
                projectAnalystName = project.ProjectAnalystName;
                labrotaryManagerName = project.LabratoryManagerName;
                SampledBy = (!string.IsNullOrEmpty(project.SampledBy) ? "Sampled By: " + project.SampledBy : "");
            }

            var groupsMoldData =
                            from sep in moldData
                            group sep by new { sep.Location, sep.LabId, sep.MoldSampleId, sep.AnalysisDate, sep.CommentsIndex };
            List<MoldReportDataSummary> moldReportDataSummary = new List<MoldReportDataSummary>();
            int count = 1;
            foreach(var item in groupsMoldData)
            {
                MoldReportDataSummary data = new MoldReportDataSummary();
                data.MoldSampleId = item.Key.MoldSampleId;
                data.Location = item.Key.Location;
                data.LabId = item.Key.LabId;
                data.AnalysisDate = item.Key.AnalysisDate;
                data.Comments = item.Key.CommentsIndex;
                data.SerialNo = count;
                moldReportDataSummary.Add(data);
                count++;
            }
            decimal numberOfTable = 1;
            decimal numberOfSample = 0;
            if(moldReportDataSummary != null)
            {
                numberOfSample = moldReportDataSummary.Count;
            }
            if(numberOfSample > 3)
            {
                numberOfTable =Math.Ceiling((decimal)(numberOfSample/3));
            }
            List<SporeCategoryData> sporeCategoryData = new List<SporeCategoryData>();
            var groupsSporeType =
                            from sep in moldData
                            group sep by new { sep.SporeTypeId, sep.SporeType };
            foreach(var item in groupsSporeType)
            {
                SporeCategoryData data = new SporeCategoryData();
                data.SporeTypeId = item.Key.SporeTypeId;
                data.SporeType = item.Key.SporeType;
                sporeCategoryData.Add(data);
            }
            List<MoldTableData> moldTableData = new List<MoldTableData>();
            for(int i=0; i< numberOfTable; i++)
            {
                MoldTableData data = new MoldTableData();
                data.Id = i;
                moldTableData.Add(data);
            }
            string dateOfReported = "";
            string dateOfAnalyzed = "";
            string samplingDate = "";
            string specialNote = "<div style=\"text-align:justify;font-weight:bold;\">Background debris is an indication of the amounts of non-biological particulate matter present on the slide (dust in the air) and is graded from 1 to 4 with 4 indicating the largest amounts. All samples were received in acceptable condition unless noted in the Report Comments portion in the body of the report.Due to the nature of the analyses performed, field blank correction of results is not applied.The results relate only to the items tested. All samples were received in acceptable condition unless noted in the Report Comments portion in the body of the report.Due to the nature of the analyses performed, field blank correction of results is not applied.</div>";
            string projectTitle = string.Format("<div style=\"border-radius: 25px;border:solid 2px #cccccc;padding:10px;page-break-before: always;\"><span style=\"font-weight:bold;\">Job Name:</span> {0} </div>", jobNumber);
            moldReportDataFinal.MoldData = moldData;
            moldReportDataFinal.MoldReportDataSummary = moldReportDataSummary;
            moldReportDataFinal.SporeCategoryData = sporeCategoryData;
            moldReportDataFinal.NumberOfTable = numberOfTable;
            moldReportDataFinal.MoldTableData = moldTableData;
            moldReportDataFinal.ProjectNumber = projectNumber;
            moldReportDataFinal.ClientName = clientName;
            moldReportDataFinal.ReportTo = reportTo;
            moldReportDataFinal.JobNumber = jobNumber;
            moldReportDataFinal.Phone = phone;
            moldReportDataFinal.DateOfSubmitted = dateOfSubmitted;
            moldReportDataFinal.DateOfReported = dateOfReported;
            moldReportDataFinal.DateOfAnalyzed = dateOfAnalyzed;
            moldReportDataFinal.SpecialNote = specialNote;
            moldReportDataFinal.ProjectTitle = projectTitle;
            moldReportDataFinal.ProjectAnalystName = projectAnalystName;
            moldReportDataFinal.LabrotaryManagerName = labrotaryManagerName;
            moldReportDataFinal.SampledBy = SampledBy;
            moldReportDataFinal.Comments = comments;
            moldReportDataFinal.SampleVolume = sampleVolume.ToString();
            moldReportDataFinal.SamplingDate = samplingDate;
            moldReportDataFinal.AnalystSign = labAnalystSignUrl;
            moldReportDataFinal.LabratoryManagerSign = labrotaryManagerSignUrl;
            return this.Request.CreateResponse(HttpStatusCode.OK, moldReportDataFinal);
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/GLSPM/GetAllProject")]
        public HttpResponseMessage GetAllProject()
        {
            int total = 0;
            List<ProjectViewModel> dataList = new ProjectService().GetAllProject(0, 10000, 0, "", 1, "", out total);
            if(dataList == null)
            {
                dataList = new List<ProjectViewModel>();
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, dataList);
        }
        public class MoldReportData
        {
            public List<MoldViewModel> MoldData { get; set; }
            public List<MoldReportDataSummary> MoldReportDataSummary { get; set; }
            public List<SporeCategoryData> SporeCategoryData { get; set; }
            public decimal NumberOfTable { get; set; }
            public List<MoldTableData> MoldTableData { get; set; }
            public string ClientName { get; set; }
            public string Phone { get; set; }
            public string ProjectNumber { get; set; }
            public string JobNumber { get; set; }
            public string ReportTo { get; set; }
            public string DateOfReported { get; set; }
            public string DateOfSubmitted { get; set; }
            public string SamplingDate { get; set; }
            public string ProjectAnalystName { get; set; }
            public string LabrotaryManagerName { get; set; }
            public string SampledBy { get; set; }
            public string Comments { get; set; }
            public string SampleVolume { get; set; }
            public string ProjectTitle { get; set; }
            public string DateOfAnalyzed { get; set; }
            public string SpecialNote { get; set; }
            public string LabratoryManagerSign { get; set; }
            public string AnalystSign { get; set; }
        }
        public class MoldReportDataSummary
        {
            public long MoldSampleId { get; set; }
            public string Location { get; set; }
            public string LabId { get; set; }
            public string AnalysisDate { get; set; }

            public string Comments { get; set; }
            public int SerialNo { get; set; }
        }
        public class SporeCategoryData
        {
            public int SporeTypeId { get; set; }
            public string SporeType { get; set; }
        }
        public class MoldTableData
        {
            public int Id { get; set; }
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/GLSPM/SendContctusEmail")]
        public HttpResponseMessage SendContctusEmail(string fromName, string subject, string fromAddress, string emailBody)
        {

            EmailAccounts emailAccount = new EmailAccounts(); //new EmailAccountsService().GetEmailAccount();
            emailAccount.Email = "rasel@renovategraphics.com";
            //emailAccount.Email = "rasel.ewu@gmail.com";
            emailAccount.DisplayName = "Contact Us";
            emailAccount.Host = "smtp.zoho.com";
            //emailAccount.Host = "smtp.gmail.com";
            emailAccount.Port = 587;
            emailAccount.Username = "rasel@renovategraphics.com";
            emailAccount.Password = "rasel@664";
            //emailAccount.Username = "rasel.ewu@gmail.com";
            //emailAccount.Password = "R@sel664664";
            emailAccount.EnableSSL = true;
            emailAccount.UseDefaultCredentials = false;
            emailAccount.IsActive = true;
            string toName = "Contact Us";
            string emailTo = "rasel.ewu@gmail.com";
            string emailBCC = "";
            string emailCC = "rasel.ewu@gmail.com";

            SendEmail(emailAccount, subject, emailBody, fromAddress, fromName, emailTo, toName, emailBCC, emailCC);

            return this.Request.CreateResponse(HttpStatusCode.OK, "");
        }
        public static void SendEmail(EmailAccounts emailAccount, string subject, string body, string fromAddress, string fromName, string toAddress, string toName, string bcc, string cc)
        {
            var message = new MailMessage();
            message.From = new MailAddress(fromAddress, fromName);
            if (!string.IsNullOrEmpty(toAddress))
            {
                string[] ToMuliId = toAddress.Split(',');
                foreach (string ToEMailId in ToMuliId)
                {
                    if (!string.IsNullOrEmpty(ToEMailId))
                    {
                        message.To.Add(new MailAddress(ToEMailId.Trim())); //adding multiple TO Email Id
                    }
                }
            }
            //message.To.Add(new MailAddress(toAddress, toName));
            if (!string.IsNullOrEmpty(bcc))
            {
                string[] bccid = bcc.Split(',');
                foreach (string bccEmailId in bccid)
                {
                    if (!string.IsNullOrEmpty(bccEmailId))
                    {
                        message.Bcc.Add(new MailAddress(bccEmailId.Trim())); //Adding Multiple BCC email Id
                    }
                }
            }
            if (!string.IsNullOrEmpty(cc))
            {
                string[] CCId = cc.Split(',');
                foreach (string CCEmail in CCId)
                {
                    if (!string.IsNullOrEmpty(CCEmail))
                    {
                        message.CC.Add(new MailAddress(CCEmail.Trim())); //Adding Multiple CC email Id
                    }
                }
            }

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            //message.Attachments.Add(new Attachment(attachmentPath));
            //Attachment attachment = new Attachment(attachmentPath);
            //message.Attachments.Add(attachment);

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.UseDefaultCredentials = emailAccount.UseDefaultCredentials;
                smtpClient.Host = emailAccount.Host;
                smtpClient.Port = emailAccount.Port;
                smtpClient.EnableSsl = emailAccount.EnableSSL;
                if (emailAccount.UseDefaultCredentials)
                    smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                else
                    smtpClient.Credentials = new NetworkCredential(emailAccount.Username, emailAccount.Password);
                smtpClient.Send(message);
            }
        }
        public class EmailAccounts
        {
            public int EmailAccountId { get; set; }
            public string Email { get; set; }
            public string DisplayName { get; set; }
            public string Host { get; set; }
            public int Port { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public bool EnableSSL { get; set; }
            public bool UseDefaultCredentials { get; set; }
            public bool IsActive { get; set; }
        }
    }
}
