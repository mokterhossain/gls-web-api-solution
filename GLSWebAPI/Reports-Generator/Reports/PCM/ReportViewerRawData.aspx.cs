using GLSPM.Data.Modules.ProjectManagement;
using GLSPM.Service.Modules.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GLSWebAPI.Reports_Generator.Reports.PCM
{
    public partial class ReportViewerRawData : System.Web.UI.Page
    {
        public string projectNumber = "";
        public string clientName = "";
        public string phone = "";
        public string jobNumber = "";
        public string reportTo = "";
        public string dateOfSubmitted = "";
        public string labAnalystSignUrl = "";
        public string labrotaryManagerSignUrl = "";
        public string projectAnalystName = "";
        public string labrotaryManagerName = "";
        public string dateOfAnalyzed = "";
        public double originalValue = 0;
        public double duplicateValue = 0;
        public double tvValue = 0;
        public double difValue = 0;
        public string qcResult = "";
        public ProjectViewModel project = new ProjectViewModel();
        public List<ProjectSampleViewModel> projectSample = new List<ProjectSampleViewModel>();
        public List<ProjectSamplePCMViewModel> projectSampleFinalList = new List<ProjectSamplePCMViewModel>();
        public List<PCMFieldBlankRawData> PCMFieldBlankRawData = new List<PCMFieldBlankRawData>();
        protected void Page_Load(object sender, EventArgs e)
        {
            long ProjectId = 0;
            if (Request.QueryString["projectId"] != null)
            {
                ProjectId = Convert.ToInt64(Request.QueryString["projectId"]);
            }
            project = new ProjectService().GetByProjectId(Convert.ToInt64(ProjectId));
            projectSample = new ProjectSampleService().GetAllProjectSampleByProjectId(Convert.ToInt64(ProjectId));
           
            int count = 0;
            int rowNumber = 1;
            int groupNumber = 1;
            ProjectSamplePCMViewModel psdDup = new ProjectSamplePCMViewModel();
            foreach (ProjectSampleViewModel psv in projectSample)
            {
                ProjectSamplePCMViewModel psd2 = new ProjectSamplePCMViewModel();
                psd2.GroupNumber = groupNumber;
                //psd2.DisplayOrder = rowNumber;
                psd2.LocationGroup = string.Format("Location: {0}, {1}", count + 1, psv.Location);
                psd2.LabIdGroup = string.Format("Comment:{0}", psv.Note);
                //psd2.SampleType = "<div style=\"text-align:center;border:solid 1px #cccccc;\"><b>Sample Layers</b></div>";
                //psd2.AbsestosPercentageText = "<div style=\"text-align:center;border:solid 1px #cccccc;\"><b>Asbestos Content</b></div>";
                //projectSampleFinalList.Add(psd2);
                rowNumber++;
                List<ProjectSamplePCMViewModel> projectSampleDetail = new PCMService().GetAllProjectSamplePCMData(psv.SampleId);
                string compositeNonAsbestosContentsText = string.Empty;
                int sampleCount = 0;
                foreach (ProjectSamplePCMViewModel psd in projectSampleDetail)
                {
                    if (psd.IsDuplicate == true)
                    {
                        psdDup = psd;
                        psdDup.GroupNumber = 100;
                        //psd.DisplayOrder = rowNumber;
                        psdDup.LocationGroup = string.Format("Dup - Location: {0}, {1}", count + 1, psv.Location);
                        psdDup.LabIdGroup = string.Format("Dup - Comment:{0}", psv.Note);
                        //sampleCount++;
                        //projectSampleFinalList.Add(psd);
                        rowNumber++;
                    }
                    else
                    {
                        psd.GroupNumber = groupNumber;
                        //psd.DisplayOrder = rowNumber;
                        psd.LocationGroup = string.Format("Location: {0}, {1}", count + 1, psv.Location);
                        psd.LabIdGroup = string.Format("Comment:{0}", psv.Note);
                        //sampleCount++;
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
            if (psdDup != null)
                projectSampleFinalList.Add(psdDup);
            
            if (project != null)
            {
                projectNumber = project.ProjectNumber;
                clientName = project.ClientName;
                phone = project.OfficePhone;
                jobNumber = project.JobNumber;
                reportTo = project.ReportingPerson;
                dateOfSubmitted = Convert.ToDateTime(project.ReceivedDate).ToShortDateString();//project.NumberOfSample.ToString();
                                                                                               //dateOfReported = DateTime.Now.ToShortDateString();// string.Format("{0} {1}", project.CellNo, project.OfficePhone);
                                                                                               //ReceivedDate = project.ReceivedDate.ToString();
                labAnalystSignUrl = project.AnalystSignature.Replace("~/", "");
                labrotaryManagerSignUrl = project.LabratoryManagerSignature.Replace("~/", "");
                projectAnalystName = project.ProjectAnalystName;
                labrotaryManagerName = project.LabratoryManagerName;
                dateOfAnalyzed = Convert.ToDateTime(project.DateOfAnalyzed).ToShortDateString();
            }
            
            PCMCV pcmCv = new PCMCVService().GetByProjectID(Convert.ToInt64(ProjectId));
            if (pcmCv != null)
            {
                if(pcmCv?.OriginalValue != null)
                    originalValue = (double)pcmCv?.OriginalValue;
                if (pcmCv?.DuplicateValue != null)
                    duplicateValue = (double)pcmCv?.DuplicateValue;
                if (pcmCv?.TVValue != null)
                    tvValue = (double)pcmCv?.TVValue;
                if (pcmCv?.DifValue != null)
                    difValue = (double)pcmCv?.DifValue;
                if (pcmCv?.QCResult != null)
                    qcResult = pcmCv?.QCResult;
            }
            PCMFieldBlankRawData = new PCMFieldBlankRawDataService().AllByProjectId(Convert.ToInt64(ProjectId));
            lvRawBlankData.DataSource = PCMFieldBlankRawData;
            lvRawBlankData.DataBind();

            lvPCM.DataSource = projectSampleFinalList;
            lvPCM.DataBind();
        }
    }
}