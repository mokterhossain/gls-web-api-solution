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
using System.Web.Http;

namespace GLSWebAPI.Controllers
{
    public class ProjectController : ApiController
    {
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Project/GetProjectList")]
        public HttpResponseMessage GetProjectList(int start, int limit, int clientId, string projectNumber, int statusId, string batchNumber)
        {
            int totalData = 0;
            int totalInprogressRecordCount = 0;
            int totalBatchGeneratedRecordCount = 0;
            int totalCompleteRecordCount = 0;
            int totalInvoicedRecordCount = 0;
            if (projectNumber == null)
                projectNumber = "";
            if (batchNumber == null)
                batchNumber = "";
            List<ProjectViewModel> dataList = new ProjectService().GetAllProjectAPI(start, limit, clientId, projectNumber, statusId, batchNumber, out totalData, out totalInprogressRecordCount
            , out totalBatchGeneratedRecordCount
            , out totalCompleteRecordCount
            , out totalInvoicedRecordCount);
            int total = totalData;
            if (dataList != null)
            {
                // dataList = dataList.Skip(start).Take(limit).ToList();
            }
            ResponseProject<ProjectViewModel> response = new ResponseProject<ProjectViewModel>();
            if (dataList == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed to save.";
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success.";
                response.Count = total;
                response.InprogressCount = totalInprogressRecordCount;
                response.BatchGeneratedCount = totalBatchGeneratedRecordCount;
                response.CompletedCount = totalCompleteRecordCount;
                response.InvoicedCount = totalInvoicedRecordCount;
                response.ResultList = dataList;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Project/GetAllProjectForFilter")]
        public HttpResponseMessage GetAllProjectForFilter()
        {
            List<ProjectViewModel> dataList = new ProjectService().GetAllProjectForFilter();
            int total = 0;
            if (dataList != null)
            {
                // dataList = dataList.Skip(start).Take(limit).ToList();
                total = dataList.Count();
            }
            ResponseProject<ProjectViewModel> response = new ResponseProject<ProjectViewModel>();
            if (dataList == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed to save.";
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success.";
                response.Count = total;
                response.ResultList = dataList;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Project/GetAllBatchNumber")]
        public HttpResponseMessage GetAllBatchNumber(int start, int limit, string batchNumber)
        {
            int total = 0;
            if (string.IsNullOrEmpty(batchNumber))
            {
                batchNumber = "";
            }
            List<BatchNumberRecord> dataList = new BatchNumberRecordService().GetAllBatchNumber(start, limit, batchNumber, out total);
            ResponseProject<BatchNumberRecord> response = new ResponseProject<BatchNumberRecord>();
            if (dataList == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed to save.";
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success.";
                response.Count = total;
                response.ResultList = dataList;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/Project/GetAllProjectForBatchNumber")]
        public HttpResponseMessage GetAllProjectForBatchNumber(int start, int limit, int clientId, string projectNumber, int statusId, string batchNumber)
        {
            int totalData = 0;
            // List<ProjectViewModel> dataList = new ProjectService().GetAllProjectForFilter();
            List<ProjectViewModel> dataList = new ProjectService().GetAllProjectForBatchNumber(start, limit, clientId, "", out totalData);
            int total = totalData;
            if (dataList != null)
            {
                // dataList = dataList.Skip(start).Take(limit).ToList();
                // total = dataList.Count();
            }
            ResponseProject<ProjectViewModel> response = new ResponseProject<ProjectViewModel>();
            if (dataList == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed to save.";
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success.";
                response.Count = total;
                response.ResultList = dataList;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Project/GetProjectById")]
        public HttpResponseMessage GetProjectById(long projectId)
        {
            ProjectViewModel data = new ProjectService().GetByProjectIdAPI(projectId);
            Response<ProjectViewModel> response = new Response<ProjectViewModel>();
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
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Project/SaveProject")]
        public HttpResponseMessage SaveProject([FromBody] ProjectViewModel formData)
        {
            string projectNumber = formData.ProjectNumber;
            // Project projectExisting = new Project();
            string batchNumber = "";
            if (formData != null)
            {
                if (formData.ProjectID > 0)
                {
                    projectNumber = formData.ProjectNumber;
                    // projectExisting = new ProjectService().GetByID(formData.ProjectID);
                }
                else
                {
                    Project projForCheckProjectNumber = new ProjectService().GetByProjectNumber(formData.ProjectNumber);
                    if (projForCheckProjectNumber != null)
                    {
                        projectNumber = GenerateProjectNumber();
                    }
                }
                Project project = new Project();
                project.ProjectNumber = projectNumber;
                project.ClientID = formData.ClientID;
                project.Address = formData.Address;
                project.JobNumber = formData.JobNumber;
                project.ReportTo = formData.ReportTo;
                project.CellNo = formData.CellNo;
                project.OfficePhone = formData.OfficePhone;
                project.ReceivedDate = formData.ReceivedDate; //dfReceivedDate.SelectedDate;
                                                              //project.DueDate = dfDueDate.SelectedDate;
                project.ClientEmail = formData.ClientEmail;
                project.Comments = formData.Comments;
                project.NumberOfSample = formData.NumberOfSample;
                project.CreatedOn = DateTime.Now;
                project.AnalystId = formData.AnalystId;
                project.LabratoryManagerId = formData.LabratoryManagerId;
                project.ProjectType = formData.ProjectType;
                project.SampledBy = formData.SampledBy;
                project.ClientName = formData.ClientName;
                project.ReportToName = formData.ReportToName;
                project.SamplingDate = formData.SamplingDate;
                project.ReportAlso = formData.ReportAlso;
                project.ReportAlsoStr = formData.ReportAlso;
                project.ProjectStatusId = formData.ProjectStatusId;
                project.ProjectID = formData.ProjectID;

                IList<ProjectSampleViewModel> projectSampleList = formData.ProjectSample;
                project.NumberOfSample = projectSampleList.Count();
                if (formData.ProjectID > 0)
                {
                    project.DateOfAnalyzed = formData.DateOfAnalyzed;
                    project.DateOfReported = formData.DateOfReported;
                    var editProjectResponse = new ProjectService().Update(project);
                    if (editProjectResponse.IsSuccess)
                    {
                        foreach (ProjectSampleViewModel projectSampleData in projectSampleList)
                        {
                            ProjectSample ps = new ProjectSample();
                            ps.ProjectId = projectSampleData.ProjectId;
                            ps.SampleType = projectSampleData.SampleType;
                            ps.LabId = projectSampleData.LabId;
                            ////string labId = projectSampleData.LabId;
                            ////string projNum = string.Empty;
                            ////if (!string.IsNullOrEmpty(labId))
                            ////{
                            ////    string[] labIdTemp = labId.Split('-');
                            ////    ps.LabId = (projectNumber.Length < 4 ? "GLS0" : "GLS") + projectNumber + "-" + labIdTemp[1];
                            ////}
                            ps.TAT = projectSampleData.TAT;
                            ps.TATText = projectSampleData.TATText;
                            ps.Location = projectSampleData.Location;
                            ps.IsQC = projectSampleData.IsQC;
                            ps.Matrix = projectSampleData.Matrix;
                            ps.PackageCode = projectSampleData.PackageCode;
                            ps.Analyst = projectSampleData.Analyst;
                            ps.Note = projectSampleData.Note;
                            ps.SampleCompositeHomogeneity = projectSampleData.SampleCompositeHomogeneity;
                            ps.SampleCompositeHomogeneityText = projectSampleData.SampleCompositeHomogeneityText;
                            ps.SerialNo = projectSampleData.SerialNo;
                            ps.Volume = projectSampleData.Volume;
                            if (projectSampleData.SampleId > 0)
                            {
                                ProjectSample existingSample = new ProjectSampleService().GetByID(projectSampleData.SampleId);

                                ps.SampleId = projectSampleData.SampleId;
                                ps.UpdatedOn = DateTime.Now;
                                ps.BatchNumber = projectSampleData.BatchNumber;
                                var editSampleResponse = new ProjectSampleService().Update(ps);
                                if (editProjectResponse.IsSuccess)
                                {
                                    if(existingSample != null)
                                    {
                                        if(formData.ProjectType == "Mold")
                                        {
                                            if(projectSampleData.IsQC == true)
                                            {
                                                if(projectSampleData.IsQC != existingSample.IsQC)
                                                {
                                                    Mold moldData = new MoldService().GetByProjectID(formData.ProjectID);
                                                    if (moldData != null)
                                                    {
                                                        MoldSample moldSample = new MoldSample();
                                                        //moldSample.Id = (-1) * sampleCount;
                                                        moldSample.MoldId = moldData.Id;
                                                        moldSample.SampleId = projectSampleData.SampleId;
                                                        moldSample.Location = projectSampleData.Location + "(Dup)";
                                                        moldSample.LabId = projectSampleData.LabId;
                                                        moldSample.IsQC = projectSampleData.IsQC;
                                                        moldSample.IsDuplicate = true;
                                                        moldSample.SerialNo = (int)projectSampleData.SerialNo + 0.5;
                                                        var moldSampleAddResponse = new MoldSampleService().Add(moldSample);

                                                        List<MoldSporeType> moldSporeType = new MoldSporeTypeService().All().OrderBy(m => m.SerialNo).ToList();
                                                        List<MoldSampleDetail> moldSampleDetailList = new List<MoldSampleDetail>();
                                                        if (moldSampleAddResponse.IsSuccess)
                                                        {
                                                            moldSporeType.ForEach(sporeType =>
                                                            {
                                                                MoldSampleDetail moldSampleDetail = new MoldSampleDetail();
                                                                moldSampleDetail.SporeTypeId = sporeType.Id;
                                                                moldSampleDetail.MoldSampleId = moldSampleAddResponse.Result.Id;
                                                                moldSampleDetail.RawCt = 0;
                                                                moldSampleDetail.Permm = 0;
                                                                var moldSampleDetailsResponse = new MoldSampleDetailService().Add(moldSampleDetail);
                                                            });
                                                        }

                                                        MoldSample moldSampleExst = new MoldSampleService().GetBySampleIDAndSerial(projectSampleData.SampleId, (double)projectSampleData.SerialNo);
                                                        if (moldSampleExst != null)
                                                        {
                                                            moldSampleExst.IsQC = true;
                                                            var moldSampleUpdateResponse = new MoldSampleService().Update(moldSampleExst);
                                                        }
                                                        

                                                    }
                                                }
                                                else
                                                {
                                                    MoldSample moldSampleExst = new MoldSampleService().GetBySampleIDAndSerial(projectSampleData.SampleId, (double)projectSampleData.SerialNo);
                                                    if (moldSampleExst != null)
                                                    {
                                                        moldSampleExst.Location = projectSampleData.Location;
                                                        var moldSampleUpdateResponse = new MoldSampleService().Update(moldSampleExst);
                                                    }
                                                    MoldSample moldSampleExst2 = new MoldSampleService().GetBySampleIDAndSerial2(projectSampleData.SampleId, ((double)projectSampleData.SerialNo + 0.5));
                                                    if (moldSampleExst2 != null)
                                                    {
                                                        moldSampleExst2.IsQC = true;
                                                        if ((bool)moldSampleExst2.IsDuplicate)
                                                        {
                                                            moldSampleExst2.Location = projectSampleData.Location + "(Dup)";
                                                        }
                                                        var moldSampleUpdateResponse = new MoldSampleService().Update(moldSampleExst2);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (projectSampleData.IsQC != existingSample.IsQC)
                                                {
                                                    MoldSample moldSampleExst = new MoldSampleService().GetBySampleIDAndSerial(projectSampleData.SampleId, (double)projectSampleData.SerialNo);
                                                    if (moldSampleExst != null)
                                                    {
                                                        moldSampleExst.IsQC = false;
                                                        moldSampleExst.Location = projectSampleData.Location;
                                                        var moldSampleUpdateResponse = new MoldSampleService().Update(moldSampleExst);
                                                    }
                                                    MoldSample moldSampleExst2 = new MoldSampleService().GetBySampleIDAndSerial(projectSampleData.SampleId, (double)(projectSampleData.SerialNo+ 0.5));
                                                    if (moldSampleExst2 != null)
                                                    {
                                                        var moldSampledeleteResponse = new MoldSampleService().Delete(moldSampleExst2.Id);
                                                    }
                                                }
                                                else
                                                {
                                                    MoldSample moldSampleExst = new MoldSampleService().GetBySampleIDAndSerial(projectSampleData.SampleId, (double)projectSampleData.SerialNo);
                                                    if (moldSampleExst != null)
                                                    {
                                                        moldSampleExst.Location = projectSampleData.Location;
                                                        var moldSampleUpdateResponse = new MoldSampleService().Update(moldSampleExst);
                                                    }
                                                }

                                            }
                                        }
                                    }
                                    List<ProjectSampleDetailViewModel> projectSampleDetailsData = projectSampleData.ProjectSampleDetail;
                                    foreach (ProjectSampleDetailViewModel psdData in projectSampleDetailsData)
                                    {
                                        ProjectSampleDetail psd = new ProjectSampleDetail();
                                        psd.ProjectSampleId = psdData.ProjectSampleId;
                                        psd.SampleType = psdData.SampleType;
                                        psd.SampleTypeId = psdData.SampleTypeId;
                                        psd.AbsestosPercentage = psdData.AbsestosPercentage;
                                        psd.AbsestosPercentageText = psdData.AbsestosPercentageText;
                                        psd.CompositeNonAsbestosContents = psdData.CompositeNonAsbestosContents;
                                        psd.CompositeNonAsbestosContentsText = psdData.CompositeNonAsbestosContentsText;
                                        psd.DisplayOrder = psdData.DisplayOrder;
                                        psd.IsBilable = psdData.IsBilable;
                                        if (psdData.Id > 0)
                                        {
                                            psd.Id = psdData.Id;
                                            psd.UpdatedOn = DateTime.Now;
                                            var addProjectSampleDetailResponse = new ProjectSampleDetailService().Update(psd);
                                        }
                                        else
                                        {
                                            psd.CreatedOn = DateTime.Now;
                                            if (project.ProjectType == "PLM")
                                            {
                                                var addProjectSampleDetailResponse = new ProjectSampleDetailService().Add(psd);
                                            }
                                            else
                                            {
                                                SampleLayer sampleLayer = new SampleLayerService().GetByName("Cassette");
                                                psd.SampleType = sampleLayer.Name;
                                                psd.SampleTypeId = sampleLayer.Id;
                                                psd.IsBilable = true;
                                                psd.CreatedOn = DateTime.Now;
                                                var addProjectSampleDetailResponse = new ProjectSampleDetailService().Add(psd);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ps.CreatedOn = DateTime.Now;
                                var addSampleResponse = new ProjectSampleService().Add(ps);
                                if (addSampleResponse.IsSuccess)
                                {
                                    List<ProjectSampleDetailViewModel> projectSampleDetailsData = projectSampleData.ProjectSampleDetail;
                                    foreach (ProjectSampleDetailViewModel psdData in projectSampleDetailsData)
                                    {
                                        ProjectSampleDetail psd = new ProjectSampleDetail();
                                        psd.ProjectSampleId = addSampleResponse.Result.SampleId;
                                        psd.SampleType = psdData.SampleType;
                                        psd.SampleTypeId = psdData.SampleTypeId;
                                        psd.AbsestosPercentage = psdData.AbsestosPercentage;
                                        psd.CompositeNonAsbestosContents = psdData.CompositeNonAsbestosContents;
                                        psd.CompositeNonAsbestosContentsText = psdData.CompositeNonAsbestosContentsText;
                                        psd.DisplayOrder = psdData.DisplayOrder;
                                        psd.IsBilable = psdData.IsBilable;
                                        if (psdData.Id > 0)
                                        {
                                            psd.Id = psdData.Id;
                                            psd.UpdatedOn = DateTime.Now;
                                            var addProjectSampleDetailResponse = new ProjectSampleDetailService().Update(psd);
                                        }
                                        else
                                        {
                                            psd.CreatedOn = DateTime.Now;
                                            if (project.ProjectType == "PLM")
                                            {
                                                var addProjectSampleDetailResponse = new ProjectSampleDetailService().Add(psd);
                                            }
                                            else
                                            {
                                                SampleLayer sampleLayer = new SampleLayerService().GetByName("Cassette");
                                                psd.SampleType = sampleLayer.Name;
                                                psd.SampleTypeId = sampleLayer.Id;
                                                psd.IsBilable = true;
                                                psd.CreatedOn = DateTime.Now;
                                                var addProjectSampleDetailResponse = new ProjectSampleDetailService().Add(psd);
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
                else
                {
                    project.ProjectStatusId = 1;
                    project.CreatedOn = DateTime.Now;
                    var addProjectResponse = new ProjectService().Add(project);
                    if (addProjectResponse.IsSuccess)
                    {
                        foreach (ProjectSampleViewModel projectSampleData in projectSampleList)
                        {
                            ProjectSample ps = new ProjectSample();
                            ps.ProjectId = addProjectResponse.Result.ProjectID;
                            ps.SampleType = projectSampleData.SampleType;
                            string labId = projectSampleData.LabId;
                            string projNum = string.Empty;
                            if (!string.IsNullOrEmpty(labId))
                            {
                                string[] labIdTemp = labId.Split('-');
                                ps.LabId = (projectNumber.Length < 4 ? "GLS0" : "GLS") + projectNumber + "-" + labIdTemp[1];
                            }
                            ps.TAT = projectSampleData.TAT;
                            ps.TATText = projectSampleData.TATText;
                            ps.Location = projectSampleData.Location;
                            ps.IsQC = projectSampleData.IsQC;
                            ps.Matrix = projectSampleData.Matrix;
                            ps.PackageCode = projectSampleData.PackageCode;
                            ps.Analyst = projectSampleData.Analyst;
                            ps.Note = projectSampleData.Note;
                            ps.SampleCompositeHomogeneity = projectSampleData.SampleCompositeHomogeneity;
                            ps.SampleCompositeHomogeneityText = projectSampleData.SampleCompositeHomogeneityText;
                            ps.CreatedOn = DateTime.Now;
                            ps.SerialNo = projectSampleData.SerialNo;
                            ps.Volume = projectSampleData.Volume;
                            var addSampleResponse = new ProjectSampleService().Add(ps);
                            if (addSampleResponse.IsSuccess)
                            {
                                List<ProjectSampleDetailViewModel> projectSampleDetailsData = projectSampleData.ProjectSampleDetail;
                                if (project.ProjectType == "PLM")
                                {
                                    foreach (ProjectSampleDetailViewModel psdData in projectSampleDetailsData)
                                    {
                                        ProjectSampleDetail psd = new ProjectSampleDetail();
                                        psd.ProjectSampleId = addSampleResponse.Result.SampleId;
                                        psd.SampleType = psdData.SampleType;
                                        psd.SampleTypeId = psdData.SampleTypeId;
                                        psd.AbsestosPercentage = psdData.AbsestosPercentage;
                                        psd.CompositeNonAsbestosContents = psdData.CompositeNonAsbestosContents;
                                        psd.CompositeNonAsbestosContentsText = psdData.CompositeNonAsbestosContentsText;
                                        psd.DisplayOrder = psdData.DisplayOrder;
                                        psd.IsBilable = psdData.IsBilable;
                                        psd.CreatedOn = DateTime.Now;
                                        var addProjectSampleDetailResponse = new ProjectSampleDetailService().Add(psd);
                                    }
                                }
                                else
                                {
                                    ProjectSampleDetail psd = new ProjectSampleDetail();
                                    SampleLayer sampleLayer = new SampleLayerService().GetByName("Cassette");
                                    psd.ProjectSampleId = addSampleResponse.Result.SampleId;
                                    psd.SampleType = sampleLayer.Name;
                                    psd.SampleTypeId = sampleLayer.Id;
                                    psd.IsBilable = true;
                                    psd.CreatedOn = DateTime.Now;
                                    var addProjectSampleDetailResponse = new ProjectSampleDetailService().Add(psd);
                                }
                            }
                        }
                    }
                }
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, "");
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Project/GetProjectSample")]
        public HttpResponseMessage GetProjectSample(long projectId)
        {
            List<ProjectSampleViewModel> data = new ProjectSampleService().GetAllProjectSampleByProjectId(projectId);
            // ProjectViewModel data = new ProjectService().GetByProjectId(projectId);
            Response<ProjectSampleViewModel> response = new Response<ProjectSampleViewModel>();
            if (data == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed.";
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success.";
                response.ResultList = data;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);

        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Project/GetPCMFieldBlankRawData")]
        public HttpResponseMessage GetPCMFieldBlankRawData(long projectId)
        {
            // List<ProjectSampleViewModel> data = new ProjectSampleService().GetAllProjectSampleByProjectId(projectId);
            List<PCMFieldBlankRawData> data = new PCMFieldBlankRawDataService().AllByProjectId(projectId);
            // ProjectViewModel data = new ProjectService().GetByProjectId(projectId);
            Response<PCMFieldBlankRawData> response = new Response<PCMFieldBlankRawData>();
            if (data == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed.";
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success.";
                response.ResultList = data;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);

        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Project/GetPCMData")]
        public HttpResponseMessage GetPCMData(long projectId)
        {
            // List<ProjectSampleViewModel> data = new ProjectSampleService().GetAllProjectSampleByProjectId(projectId);
            List<PCM> pcmData = new PCMService().GetPCMByProjectId(projectId);
            List<PCMFieldBlankRawData> pcmRawBlankdata = new PCMFieldBlankRawDataService().GetPCMFieldBlankRawDataByProjectId(projectId);
            List<ProjectSample> projectSampleList = new ProjectSampleService().GetByProjectID(projectId);
            PCMCV pcmCv = new PCMCVService().GetByProjectID(projectId);
            ProjectSample ProjectSampleQC = new ProjectSample();
            int totalQcCount = 0;
            if (projectSampleList != null)
            {
                projectSampleList = projectSampleList.Where(s => s.IsQC == true).ToList();                
                if(projectSampleList != null)
                {
                    ProjectSampleQC = projectSampleList.FirstOrDefault();
                    totalQcCount = projectSampleList.Count;
                }
                
            }
            // ProjectViewModel data = new ProjectService().GetByProjectId(projectId);
            PCMViewModel data = new PCMViewModel();
            data.ProjectPCM = pcmData;
            data.ProjectPCMRawBlankData = pcmRawBlankdata;
            data.ProjectSampleQC = ProjectSampleQC;
            data.pcmCv = pcmCv;
            Response<PCMViewModel> response = new Response<PCMViewModel>();
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
                response.Count = totalQcCount;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);

        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Project/saveProjectPCM")]
        public HttpResponseMessage saveProjectPCM([FromBody] PCMViewModel formData)
        {
            string projectNumber = "";

            if (formData != null)
            {
                List<PCM> ProjectPCM = formData.ProjectPCM;
                List<PCMFieldBlankRawData> ProjectPCMRawBlankData = formData.ProjectPCMRawBlankData;
                PCMCV PCMCV = formData.pcmCv;
                ProjectPCM.ForEach(item =>
                {
                    if((bool)item.IsBlank)
                    {
                        //if(item.CalculatedFiberCount == null)
                            item.CalculatedFiberCount = 0;
                        //if (item.CalculatedFiberPermm == null)
                            item.CalculatedFiberPermm = 0;
                        //if (item.CalculatedFiberPercc == null)
                            item.CalculatedFiberPercc = 0;
                        ///if (item.ReportedFiberPermm == null)
                            item.ReportedFiberPermm = "0";
                        //if (item.ReportedFiberPercc == null)
                            item.ReportedFiberPercc = "0";
                        item.LOD = "";
                    }
                    if (item.Id == -1)
                    {
                        var addResponse = new PCMService().Add(item);
                    }
                    else
                    {
                        var editResponse = new PCMService().Update(item);
                    }
                });
                ProjectPCMRawBlankData.ForEach(item =>
                {
                    if (item.Id == -1)
                    {
                        var addResponse = new PCMFieldBlankRawDataService().Add(item);
                    }
                    else
                    {
                        var editResponse = new PCMFieldBlankRawDataService().Update(item);
                    }
                });
                if (PCMCV != null)
                {
                    if (PCMCV.Id > 0)
                    {
                        var editResponse = new PCMCVService().Update(PCMCV);
                    }
                    else
                    {
                        var addResponse = new PCMCVService().Add(PCMCV);

                    }
                }
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, "");
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Project/GetPCMComment")]
        public HttpResponseMessage GetPCMComment()
        {
            List<PCMComment> data = new PCMCommentService().All();
            Response<PCMComment> response = new Response<PCMComment>();
            if (data == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed.";
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success.";
                response.ResultList = data;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);

        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Project/GetMoldData")]
        public HttpResponseMessage GetMoldData(long projectId)
        {
            Mold data = new MoldService().GetByProjectID(projectId);
            Response<Mold> response = new Response<Mold>();
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
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Project/saveProjectMold")]
        public HttpResponseMessage saveProjectMold([FromBody] Mold formData)
        {
            Mold moldData = new Mold();
            moldData.ProjectId = formData.ProjectId;
            moldData.Comments = formData.Comments;
            moldData.ReportName = formData.ReportName;
            moldData.MoldSamples = formData.MoldSamples;

            if (formData.Id > 0)
            {
                moldData.Id = formData.Id;
                var response = new MoldService().Update(moldData);
                List<MoldSample> moldsampleList = formData.MoldSamples;
                moldsampleList.ForEach(moldSample =>
                {
                    ProjectSample projectSample = new ProjectSampleService().GetByID(moldSample.SampleId);
                    if(projectSample != null)
                    {
                        if (moldSample.IsDuplicate != null)
                        {
                            if ((bool)moldSample.IsDuplicate)
                            {
                                moldSample.SerialNo = (int)projectSample.SerialNo + 0.5;
                                moldSample.Location = projectSample.Location + "(Dup)";
                            }
                            else
                            {
                                moldSample.SerialNo = (int)projectSample.SerialNo;
                                moldSample.Location = projectSample.Location;

                            }
                        }
                        else
                        {
                            moldSample.SerialNo = (int)projectSample.SerialNo;
                            moldSample.Location = projectSample.Location;

                        }
                    }
                    var moldSampleResponse = new MoldSampleService().Update(moldSample);
                    //if (moldSampleResponse.IsSuccess)
                    //{
                        List<MoldSampleDetail> moldsampleDetailsList = moldSample.MoldSampleDetails; ;
                        moldsampleDetailsList.ForEach(moldSampleDetail =>
                        {
                            var moldSampleDetailResponse = new MoldSampleDetailService().Update(moldSampleDetail);

                        });
                    //}
                });
            }
            else 
            {
                var response = new MoldService().Add(moldData);
            }
            //if (formData.Id > 0)
            //{
            //    moldData.Id = formData.Id;
            //    var editResponse = new MoldService().Update(moldData);
            //    if (editResponse.IsSuccess)
            //    {

            //    }
            //}
            //else
            //{
            //    var response = new MoldService().Add(moldData);
            //    if (response.IsSuccess)
            //    {
            //        long moldId = response.Result.Id;
            //        List<MoldSample> moldsampleList = formData.MoldSamples;
            //        moldsampleList.ForEach(moldSample =>
            //        {

            //        });
            //    }
            //}
            
            return this.Request.CreateResponse(HttpStatusCode.OK, "");
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Project/GetMoldDataTapeLift")]
        public HttpResponseMessage GetMoldDataTapeLift(long projectId)
        {
            Mold data = new MoldService().GetByProjectIDForTapeLift(projectId);
            Response<Mold> response = new Response<Mold>();
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

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Project/saveProjectMoldTapeLift")]
        public HttpResponseMessage saveProjectMoldTapeLift([FromBody] Mold formData)
        {
            Mold moldData = new Mold();
            moldData.ProjectId = formData.ProjectId;
            moldData.Comments = formData.Comments;
            moldData.ReportName = formData.ReportName;
            moldData.MoldSamples = formData.MoldSamples;
            if (formData.Id > 0)
            {
                moldData.Id = formData.Id;
                var response = new MoldService().Update(moldData);
                List<MoldSample> moldsampleList = formData.MoldSamples;
                moldsampleList.ForEach(moldSample =>
                {
                    var moldSampleResponse = new MoldSampleService().Update(moldSample);
                    if (moldSampleResponse.IsSuccess)
                    {
                        List<MoldTapeLiftSampleDetail> moldsampleDetailsList = moldSample.MoldTapeLiftSampleDetails; ;
                        moldsampleDetailsList.ForEach(moldSampleDetail =>
                        {
                            var moldSampleDetailResponse = new MoldTapeLiftSampleDetailService().Update(moldSampleDetail);

                        });
                    }
                });
            }
            else
            {
                var response = new MoldService().Add(moldData);
            }
            //if (formData.Id > 0)
            //{
            //    moldData.Id = formData.Id;
            //    var editResponse = new MoldService().Update(moldData);
            //    if (editResponse.IsSuccess)
            //    {

            //    }
            //}
            //else
            //{
            //    var response = new MoldService().Add(moldData);
            //    if (response.IsSuccess)
            //    {
            //        long moldId = response.Result.Id;
            //        List<MoldSample> moldsampleList = formData.MoldSamples;
            //        moldsampleList.ForEach(moldSample =>
            //        {

            //        });
            //    }
            //}

            return this.Request.CreateResponse(HttpStatusCode.OK, "");
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Project/GetMoldSporeType")]
        public HttpResponseMessage GetMoldSporeType(string moldType)
        {
            List<MoldSporeType> data = new MoldSporeTypeService().All();
            if(moldType == "Mold Tape Lift")
            {
                data = data.Where(m => m.IsMoldTapeLift == true).OrderBy(m => m.SerialNo).ToList();
            }
            else
            {
                data = data.OrderBy(m => m.SerialNo).ToList();
            }
            Response<MoldSporeType> response = new Response<MoldSporeType>();
            if (data == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed.";
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success.";
                response.ResultList = data;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);

        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Project/GetGeneralSetting")]
        public HttpResponseMessage GetGeneralSetting()
        {
            GeneralSetting data = new GeneralSettingService().All().FirstOrDefault();
            Response<GeneralSetting> response = new Response<GeneralSetting>();
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
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Project/GetAllProjectSampleByProjectType")]
        public HttpResponseMessage GetAllProjectSampleByProjectType(string projectType)
        {
            List<ProjectSampleViewModel> data = new ProjectSampleService().GetAllProjectSampleByProjectType(projectType);
            Response<ProjectSampleViewModel> response = new Response<ProjectSampleViewModel>();
            if (data == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed.";
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Success.";
                response.ResultList = data;
                response.Count = data.Count;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);

        }
        [System.Web.Http.AcceptVerbs("POST")]
        [HttpGet]
        [Route("api/Project/saveLocation")]
        public HttpResponseMessage saveLocation([FromBody] Location formData)
        {
            string projectNumber = "";

            if (formData != null)
            {
                if (formData.Id > 0)
                {
                    var editResponse = new LocationService().Update(formData);
                }
                else
                {
                    var addResponse = new LocationService().Add(formData);
                }
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, "");
        }
        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/Project/getLocation")]
        public HttpResponseMessage getLocation()
        {
            string projectNumber = "";
            List<Location> locationList = new LocationService().GetSomeLocation();

            return this.Request.CreateResponse(HttpStatusCode.OK, locationList);
        }
        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/Project/getAllLocation")]
        public HttpResponseMessage getAllLocation()
        {
            string projectNumber = "";
            List<Location> locationList = new LocationService().All();

            return this.Request.CreateResponse(HttpStatusCode.OK, locationList);
        }
        [System.Web.Http.AcceptVerbs("POST")]
        [HttpGet]
        [Route("api/Project/GenerateBatchNumber")]
        public HttpResponseMessage GenerateBatchNumber([FromBody] batchGenerateObject batchObj)
        {
            string sampleIds = batchObj.sampleIds.Trim(',');
            var updateBatchNumberResponse = new ProjectSampleService().UpdateBatchNumber(sampleIds);

            return this.Request.CreateResponse(HttpStatusCode.OK, "");
        }
        [System.Web.Http.AcceptVerbs("POST")]
        [HttpGet]
        [Route("api/Project/DisconnectFromBatch")]
        public HttpResponseMessage DisconnectFromBatch([FromBody] batchGenerateObject batchObj)
        {
            long sampleId = Convert.ToInt32(batchObj.sampleIds);
            ProjectSample ps = new ProjectSampleService().GetByID(sampleId);
            if (ps != null)
            {
                ps.BatchNumber = "";
                var editResponse = new ProjectSampleService().Update(ps);
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, "");
        }
        
        
        [System.Web.Http.AcceptVerbs("POST")]
        [HttpGet]
        [Route("api/Project/GetBatchNumber")]
        public HttpResponseMessage GetBatchNumber()
        {
            List<BatchNumberRecord> data = new BatchNumberRecordService().All();
            Response<BatchNumberRecord> response = new Response<BatchNumberRecord>();
            if (data == null)
            {
                response.IsSuccess = false;
                response.Message = "Failed.";
            }
            else
            {
                data = data.OrderByDescending(item => item.BatchNumber).ToList();
                response.IsSuccess = true;
                response.Message = "Success.";
                response.ResultList = data;
                response.Count = data.Count;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [System.Web.Http.AcceptVerbs("POST")]
        [HttpGet]
        [Route("api/Project/AssignSamplesToBatch")]
        public HttpResponseMessage AssignSamplesToBatch([FromBody] AssignSamplesToBatchObj assignSamplesToBatchObj)
        {
            string sampleIds = assignSamplesToBatchObj.sampleIds.Trim(',');
            var updateBatchNumberResponse = new ProjectSampleService().AddSampleToBatch(sampleIds, assignSamplesToBatchObj.batchNumber);
            return this.Request.CreateResponse(HttpStatusCode.OK, "");
        }
        [System.Web.Http.AcceptVerbs("POST")]
        [HttpGet]
        [Route("api/Project/GetProjectStatus")]
        public HttpResponseMessage GetProjectStatus()
        {
            List<ProjectStatus> projectStatus = new ProjectStatusService().All();
            return this.Request.CreateResponse(HttpStatusCode.OK, projectStatus);
        }
        [System.Web.Http.AcceptVerbs("POST")]
        [HttpGet]
        [Route("api/Project/SaveProjectStatus")]
        public HttpResponseMessage SaveProjectStatus([FromBody] projectStatusObject statusObj)
        {
            Project project = new ProjectService().GetByID(statusObj.projectId);
            project.ProjectStatusId = statusObj.statusId;
            var editResponse = new ProjectService().Update(project);
            return this.Request.CreateResponse(HttpStatusCode.OK, "");
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Project/getReportDateStatus")]
        public HttpResponseMessage getReportDateStatus(long projectId)
        {
            bool result = false;
            string message = "";
            ProjectViewModel data = new ProjectService().GetByProjectIdAPI(projectId);
            Response<ProjectViewModel> response = new Response<ProjectViewModel>();
            if(data.DateOfAnalyzed == null)
            {
                result = false;
            }
            if(data.DateOfAnalyzed == null)
            {
                result = false;
            }
            if(data.DateOfAnalyzed != null && data.DateOfAnalyzed != null)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            response.IsSuccess = result;
            response.Message = "Success.";
            response.Result = data;
            return this.Request.CreateResponse(HttpStatusCode.OK, response);

        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/Project/saveProjectReportDate")]
        public HttpResponseMessage saveProjectReportDate([FromBody] Project formData)
        {
            string projectNumber = formData.ProjectNumber;
            if (formData != null)
            {
                
                Project project = new ProjectService().GetByID(formData.ProjectID);
                if(project != null)
                {
                    project.DateOfReported = formData.DateOfReported;
                    project.DateOfAnalyzed = formData.DateOfAnalyzed;
                    var response = new ProjectService().Update(project);
                }
                
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, "");
        }

        public class projectStatusObject
        {
            public long projectId { get; set; }
            public int statusId { get; set; }
        }
        public class batchGenerateObject
        {
            public string sampleIds { get; set; }
        }
        public class AssignSamplesToBatchObj
        {
            public string sampleIds { get; set; }
            public string batchNumber { get; set; }
        }

        public class PCMViewModel
        {
            public List<PCM> ProjectPCM { get; set; }
            public List<PCMFieldBlankRawData> ProjectPCMRawBlankData { get; set; }
            public ProjectSample ProjectSampleQC { get; set; }
            public PCMCV pcmCv { get; set; }
        }

        public string GenerateProjectNumber()
        {
            string projectNumber = string.Empty;
            List<Project> projectList = new ProjectService().All();
            if (projectList.Count == 0)
                projectNumber = "205";
            else
            {
                //projectNumber = projectList.Max(x => x.ProjectNumber);
                if (projectList.OrderByDescending(o => o.ProjectID).FirstOrDefault() != null)
                    projectNumber = projectList.OrderByDescending(o => o.ProjectID).FirstOrDefault().ProjectNumber;
                long projectNo = Convert.ToInt32(projectNumber);
                projectNo = projectNo + 1;
                projectNumber = projectNo.ToString();
            }
            return projectNumber;
        }
        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/Project/getLocation2")]
        public HttpResponseMessage getLocation2()
        {
            string projectNumber = "";
            List<TestLocation> locationList = new LocationService().All2();

            return this.Request.CreateResponse(HttpStatusCode.OK, locationList);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/Project/DeleteSampleDetail")]
        public HttpResponseMessage DeleteSampleDetail(long detailId)
        {
            var editRespose = new ProjectSampleDetailService().Delete(detailId);

            return this.Request.CreateResponse(HttpStatusCode.OK, editRespose);
        }
        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        [Route("api/Project/DeleteSample")]
        public HttpResponseMessage DeleteSample(long sampleId)
        {
            var editRespose = new ProjectSampleService().Delete(sampleId);

            return this.Request.CreateResponse(HttpStatusCode.OK, editRespose);
        }
    }
}
