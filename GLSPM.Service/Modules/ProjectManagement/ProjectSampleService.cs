using GLSPM.Data;
using GLSPM.Data.Modules.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Service.Modules.ProjectManagement
{
    public class ProjectSampleService
    {
        #region Common Methods

        public List<ProjectSample> All()
        {
            List<ProjectSample> ProjectSample = new List<ProjectSample>();

            using (GLSPMContext db = new GLSPMContext())
            {
                ProjectSample = db.ProjectSamples.OrderBy(f => f.SampleId).AsParallel().ToList();

                if (ProjectSample == null)
                {
                    ProjectSample = new List<ProjectSample>();
                }
                return ProjectSample;
            }
        }

        public ProjectSample GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                ProjectSample ProjectSample = db.ProjectSamples.SingleOrDefault(u => u.SampleId == userRoleId);
                return ProjectSample;
            }
        }
        public ProjectSample GetByLabID(string labId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                ProjectSample ProjectSample = db.ProjectSamples.SingleOrDefault(u => u.LabId == labId);
                return ProjectSample;
            }
        }
        public List<ProjectSample> GetByProjectID(long projectId)
        {
            List<ProjectSample> ProjectSample = new List<ProjectSample>();

            using (GLSPMContext db = new GLSPMContext())
            {
                ProjectSample = db.ProjectSamples.OrderBy(f => f.SerialNo).Where(p=> p.ProjectId == projectId).AsParallel().ToList();

                if (ProjectSample == null)
                {
                    ProjectSample = new List<ProjectSample>();
                }
                return ProjectSample;
            }
        }
        public Response<ProjectSample> Add(ProjectSample ProjectSample)
        {
            Response<ProjectSample> addResponse = new Response<ProjectSample>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.ProjectSamples.Add(ProjectSample);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = ProjectSample;
                    }
                }
                catch (Exception ex)
                {
                    int errorNo = ((SqlException)ex.InnerException.InnerException).Number;
                    if (errorNo == 2601 || errorNo == 2627)
                    {
                        addResponse.Message = "This role Info already exist.";
                    }
                    else
                    {
                        addResponse.Message = "There was an error while adding the role Info: " + ex.Message;
                    }
                    addResponse.IsSuccess = false;
                    addResponse.Result = ProjectSample;
                }
                return addResponse;
            }
        }

        public Response<ProjectSample> Update(ProjectSample ProjectSample)
        {
            Response<ProjectSample> updateResponse = new Response<ProjectSample>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    ProjectSample updateProjectSample = db.ProjectSamples.SingleOrDefault(u => u.SampleId == ProjectSample.SampleId);
                    if (updateProjectSample != null)
                    {
                        //updateProjectSample = userRole;
                        updateProjectSample.SampleId = ProjectSample.SampleId;
                        updateProjectSample.ProjectId = ProjectSample.ProjectId;
                        updateProjectSample.BatchNumber = ProjectSample.BatchNumber;
                        updateProjectSample.SerialNo = ProjectSample.SerialNo;
                        updateProjectSample.LabId = ProjectSample.LabId;
                        updateProjectSample.SampleType = ProjectSample.SampleType;
                        updateProjectSample.TAT = ProjectSample.TAT;
                        updateProjectSample.Location = ProjectSample.Location;
                        updateProjectSample.Analyst = ProjectSample.Analyst;
                        updateProjectSample.Note = ProjectSample.Note;
                        updateProjectSample.SampleCompositeHomogeneity = ProjectSample.SampleCompositeHomogeneity;
                        updateProjectSample.CompositeNonAsbestosContents = ProjectSample.CompositeNonAsbestosContents;
                        updateProjectSample.SampleCompositeHomogeneityText = ProjectSample.SampleCompositeHomogeneityText;
                        updateProjectSample.CompositeNonAsbestosContentsText = ProjectSample.CompositeNonAsbestosContentsText;
                        updateProjectSample.TATText = ProjectSample.TATText;
                        updateProjectSample.AnalystName = ProjectSample.AnalystName;
                        updateProjectSample.IsQC = ProjectSample.IsQC;
                        updateProjectSample.Matrix = ProjectSample.Matrix;
                        updateProjectSample.PackageCode = ProjectSample.PackageCode;
                        updateProjectSample.Volume = ProjectSample.Volume;
                        //updateProjectSample.Homogeneity = ProjectSample.Homogeneity;
                        //updateProjectSample.Content = ProjectSample.Content;
                        //updateProjectSample.AbsestosPercentage = ProjectSample.AbsestosPercentage;
                        //updateProjectSample.IsBilable = ProjectSample.IsBilable;
                        updateProjectSample.CreatedOn = ProjectSample.CreatedOn;
                        updateProjectSample.CreatedBy = ProjectSample.CreatedBy;
                        updateProjectSample.UpdatedOn = ProjectSample.UpdatedOn;
                        updateProjectSample.UpdatedBy = ProjectSample.UpdatedBy;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = ProjectSample;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = ProjectSample;
                            updateResponse.Message = "Error on update";
                        }
                    }
                    else
                    {
                        updateResponse.Result = null;
                        updateResponse.Message = "Role Info not exist";
                    }
                }
                catch (Exception ex)
                {
                    int errorNo = ((SqlException)ex.InnerException.InnerException).Number;
                    if (errorNo == 2601 || errorNo == 2627)
                    {
                        updateResponse.Message = "This role Info already exist.";
                    }
                    else
                    {
                        updateResponse.Message = "There was an error while adding the role Info: " + ex.Message;
                    }
                    updateResponse.Result = ProjectSample;
                }
                return updateResponse;
            }
        }

        public Response<ProjectSample> Delete(long SampleId)
        {
            Response<ProjectSample> deleteResponse = new Response<ProjectSample>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    ProjectSample deleteProjectSample = db.ProjectSamples.SingleOrDefault(u => u.SampleId == SampleId);
                    if (deleteProjectSample != null)
                    {
                        db.ProjectSamples.Remove(deleteProjectSample);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteProjectSample;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteProjectSample;
                            deleteResponse.Message = "Error on delete";
                        }
                    }
                    else
                    {
                        deleteResponse.Result = null;
                        deleteResponse.Message = "Role Info not exist";
                    }
                }
                catch (Exception ex)
                {
                    int errorNo = ((SqlException)ex.InnerException.InnerException).Number;

                    if (errorNo == 547)
                    {
                        deleteResponse.Message = "This role Info currently Used.";
                    }
                    else
                    {
                        deleteResponse.Message = "There was an error while deleting role Info: " + ex.Message;
                    }
                    deleteResponse.Result = null;
                }
                return deleteResponse;
            }
        }

        #endregion Common Methods
        #region Other Methods
        public List<ProjectSampleViewModel> GetAllProjectSampleByProjectId(long projectId)
        {
            List<ProjectSampleViewModel> projectSampleFinal = new List<ProjectSampleViewModel>();
            List<ProjectSampleViewModel> projectViewModel = new List<ProjectSampleViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var projectIdParameter = new SqlParameter { ParameterName = "ProjectId", Value = projectId };
                try
                {
                    projectViewModel = db.Database.SqlQuery<ProjectSampleViewModel>("GLS_GetAllProjectSampleByProjectId @ProjectId", projectIdParameter).ToList<ProjectSampleViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<ProjectSampleViewModel>();
                    projectSampleFinal = new List<ProjectSampleViewModel>();
                }
                else
                {
                    foreach (ProjectSampleViewModel ps in projectViewModel)
                    {
                        List<ProjectSampleDetailViewModel> projectSampleDetail = new ProjectSampleDetailService().GetAllProjectSampleDetailsBySampleId(ps.SampleId);
                        string compositeNonAsbestosContentsText = string.Empty;
                        foreach (ProjectSampleDetailViewModel psd in projectSampleDetail)
                        {
                            if (projectSampleDetail.Count > 1)
                                compositeNonAsbestosContentsText += string.Format("<div>{0} ({1})</div>", psd.CompositeNonAsbestosContentsText, psd.SampleType);
                            else
                                compositeNonAsbestosContentsText += string.Format("<div>{0}</div>", psd.CompositeNonAsbestosContentsText, psd.SampleType);
                        }
                        ps.ProjectSampleDetail = projectSampleDetail;
                        ps.CompositeNonAsbestosContentsText = compositeNonAsbestosContentsText;
                        projectSampleFinal.Add(ps);
                    }
                }
                return projectSampleFinal;
            }
        }
        public List<AmendmentProjectSampleViewModel> GetAllAmendmentProjectSampleByProjectId(long projectId, string amendmentNumber)
        {
            List<AmendmentProjectSampleViewModel> projectViewModel = new List<AmendmentProjectSampleViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var projectIdParameter = new SqlParameter { ParameterName = "ProjectId", Value = projectId };
                var amendmentNumberParameter = new SqlParameter { ParameterName = "amendmentNumber", Value = amendmentNumber };
                try
                {
                    projectViewModel = db.Database.SqlQuery<AmendmentProjectSampleViewModel>("GLS_GetAllAmendmentProjectSampleByProjectId @ProjectId,@AmendmentNumber", projectIdParameter, amendmentNumberParameter).ToList<AmendmentProjectSampleViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<AmendmentProjectSampleViewModel>();
                }
                return projectViewModel;
            }
        }
        public List<ProjectSampleViewModel> GetAllProjectSampleByBatchNumber(string batchNumber)
        {
            List<ProjectSampleViewModel> projectViewModel = new List<ProjectSampleViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var batchNumberParameter = new SqlParameter { ParameterName = "BatchNumber", Value = batchNumber };
                try
                {
                    projectViewModel = db.Database.SqlQuery<ProjectSampleViewModel>("GLS_GetAllProjectSampleByBatchNumber @BatchNumber", batchNumberParameter).ToList<ProjectSampleViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<ProjectSampleViewModel>();
                }
                return projectViewModel;
            }
        }
        public List<ProjectSampleViewModel> GetAllProjectSampleByProjectIds(string projectIds)
        {
            List<ProjectSampleViewModel> projectViewModel = new List<ProjectSampleViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var projectIdParameter = new SqlParameter { ParameterName = "projectIds", Value = projectIds };
                try
                {
                    projectViewModel = db.Database.SqlQuery<ProjectSampleViewModel>("GLS_GetAllProjectSampleByProjectIds @ProjectIds", projectIdParameter).ToList<ProjectSampleViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<ProjectSampleViewModel>();
                }
                return projectViewModel;
            }
        }
        public List<ProjectSampleViewModel> GetAllProjectSampleByProjectIdsNew(string projectIds)
        {
            List<ProjectSampleViewModel> projectViewModel = new List<ProjectSampleViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var projectIdParameter = new SqlParameter { ParameterName = "projectIds", Value = projectIds };
                try
                {
                    projectViewModel = db.Database.SqlQuery<ProjectSampleViewModel>("GLS_GetAllProjectSampleByProjectIds_New @ProjectIds", projectIdParameter).ToList<ProjectSampleViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<ProjectSampleViewModel>();
                }
                return projectViewModel;
            }
        }
        public Response<ProjectSampleUpdateResult> UpdateBatchNumber(string sampleIds)
        {
            ProjectSampleUpdateResult processResult = new ProjectSampleUpdateResult();

            Response<ProjectSampleUpdateResult> processResponse = new Response<ProjectSampleUpdateResult>();
            using (GLSPMContext db = new GLSPMContext())
            {
                //ClassWiseHead = db.ClassWiseHeads.OrderBy(f => f.ID).AsParallel().ToList();
                var sampleIdsParameter = new SqlParameter { ParameterName = "SampleIds", Value = sampleIds };
                try
                {
                    processResult = db.Database.SqlQuery<ProjectSampleUpdateResult>("GLS_UpdateBatchNumber @SampleIds", sampleIdsParameter).ToList<ProjectSampleUpdateResult>().FirstOrDefault();
                }
                catch (Exception ex) { }
                if (processResult == null)
                {
                    processResult = new ProjectSampleUpdateResult();
                    processResponse.Message = "Failed To Process Result";
                    processResponse.IsSuccess = false;
                    processResponse.Result = processResult;
                }
                else
                {
                    processResponse.Message = "Processed Result successfully";
                    processResponse.IsSuccess = true;
                    processResponse.Result = processResult;
                }
                return processResponse;
            }
        }
        public Response<ProjectSampleUpdateResult> AddSampleToBatch(string sampleIds, string batchNumber)
        {
            ProjectSampleUpdateResult processResult = new ProjectSampleUpdateResult();

            Response<ProjectSampleUpdateResult> processResponse = new Response<ProjectSampleUpdateResult>();
            using (GLSPMContext db = new GLSPMContext())
            {
                //ClassWiseHead = db.ClassWiseHeads.OrderBy(f => f.ID).AsParallel().ToList();
                var sampleIdsParameter = new SqlParameter { ParameterName = "SampleIds", Value = sampleIds };
                var batchNumberParameter = new SqlParameter { ParameterName = "BatchNumber", Value = batchNumber };
                try
                {
                    processResult = db.Database.SqlQuery<ProjectSampleUpdateResult>("GLS_AddSampleToBatch @SampleIds,@BatchNumber", sampleIdsParameter, batchNumberParameter).ToList<ProjectSampleUpdateResult>().FirstOrDefault();
                }
                catch (Exception ex) { }
                if (processResult == null)
                {
                    processResult = new ProjectSampleUpdateResult();
                    processResponse.Message = "Failed To Process Result";
                    processResponse.IsSuccess = false;
                    processResponse.Result = processResult;
                }
                else
                {
                    processResponse.Message = "Processed Result successfully";
                    processResponse.IsSuccess = true;
                    processResponse.Result = processResult;
                }
                return processResponse;
            }
        }
        public List<ProjectSampleViewModel> GetAllProjectSampleForQC(int start, int limit, out int totalRecordCount)
        {
            List<ProjectSampleViewModel> projectViewModel = new List<ProjectSampleViewModel>();
            totalRecordCount = 0;
            using (GLSPMContext db = new GLSPMContext())
            {
                //var projectIdParameter = new SqlParameter { ParameterName = "ProjectId", Value = projectId };
                var stratRowNumberParameter = new SqlParameter { ParameterName = "StratRowNumber", Value = start };
                var endRowNumberParameter = new SqlParameter { ParameterName = "EndRowNumber", Value = limit };
                var totalRecordCountParameter = new SqlParameter { ParameterName = "TotalRecordCount", Value = totalRecordCount, Direction = System.Data.ParameterDirection.Output };
                try
                {
                    projectViewModel = db.Database.SqlQuery<ProjectSampleViewModel>("GLS_GetAllProjectSampleForQC @StratRowNumber,@EndRowNumber, @TotalRecordCount OUTPUT", stratRowNumberParameter, endRowNumberParameter, totalRecordCountParameter).ToList<ProjectSampleViewModel>();
                    if (totalRecordCountParameter.Value != null)
                        totalRecordCount = Convert.ToInt16(totalRecordCountParameter.Value);
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<ProjectSampleViewModel>();
                }
                return projectViewModel;
            }
        }

        public List<ProjectSampleViewModel> GetAllProjectSampleByProjectType(string projectType)
        {
            List<ProjectSampleViewModel> projectViewModel = new List<ProjectSampleViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var projectTypeParameter = new SqlParameter { ParameterName = "ProjectType", Value = projectType };
                try
                {
                    projectViewModel = db.Database.SqlQuery<ProjectSampleViewModel>("GLS_GetAllProjectSampleByProjectType @ProjectType", projectTypeParameter).ToList<ProjectSampleViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<ProjectSampleViewModel>();
                }
                return projectViewModel;
            }
        }
        #endregion
    }
}
