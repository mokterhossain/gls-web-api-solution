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
    public class ProjectService
    {
        #region Common Methods

        public List<Project> All()
        {
            List<Project> Project = new List<Project>();

            using (GLSPMContext db = new GLSPMContext())
            {
                Project = db.Projects.OrderBy(f => f.ProjectID).AsParallel().ToList();

                if (Project == null)
                {
                    Project = new List<Project>();
                }
                return Project;
            }
        }
        public Project GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                Project Project = db.Projects.SingleOrDefault(u => u.ProjectID == userRoleId);
                return Project;
            }
        }
        public Project GetByClientID(long clientId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                Project Project = db.Projects.Where(p => p.ClientID == clientId).OrderByDescending(p => p.ProjectID).FirstOrDefault();
                return Project;
            }
        }
        public Project GetByProjectNumber(string projectNumber)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                Project Project = db.Projects.Where(p => p.ProjectNumber == projectNumber).OrderByDescending(p => p.ProjectID).FirstOrDefault();
                return Project;
            }
        }
        public Response<Project> Add(Project Project)
        {
            Response<Project> addResponse = new Response<Project>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.Projects.Add(Project);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = Project;
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
                    addResponse.Result = Project;
                }
                return addResponse;
            }
        }

        public Response<Project> Update(Project project)
        {
            Response<Project> updateResponse = new Response<Project>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    Project updateProject = db.Projects.SingleOrDefault(u => u.ProjectID == project.ProjectID);
                    if (updateProject != null)
                    {
                        //updateProject = userRole;
                        updateProject.ProjectID = project.ProjectID;
                        updateProject.ProjectNumber = project.ProjectNumber;
                        updateProject.ClientID = project.ClientID;
                        updateProject.Address = project.Address;
                        updateProject.JobNumber = project.JobNumber;
                        updateProject.ReportTo = project.ReportTo;
                        updateProject.CellNo = project.CellNo;
                        updateProject.OfficePhone = project.OfficePhone;
                        updateProject.ReceivedDate = project.ReceivedDate;
                        updateProject.DueDate = project.DueDate;
                        updateProject.ClientEmail = project.ClientEmail;
                        updateProject.Comments = project.Comments;
                        updateProject.NumberOfSample = project.NumberOfSample;
                        updateProject.DateOfReported = project.DateOfReported;
                        updateProject.DateOfAnalyzed = project.DateOfAnalyzed;
                        updateProject.ProjectStatusId = project.ProjectStatusId;
                        updateProject.AnalystId = project.AnalystId;
                        updateProject.LabratoryManagerId = project.LabratoryManagerId;
                        updateProject.ProjectType = project.ProjectType;
                        updateProject.SampledBy = project.SampledBy;
                        updateProject.CreatedOn = project.CreatedOn;
                        updateProject.CreatedBy = project.CreatedBy;
                        updateProject.UpdatedOn = project.UpdatedOn;
                        updateProject.UpdatedBy = project.UpdatedBy;
                        updateProject.ClientName = project.ClientName;
                        updateProject.ReportToName = project.ReportToName;
                        updateProject.IsAmendment = project.IsAmendment;
                        updateProject.SamplingDate = project.SamplingDate;
                        updateProject.ReportAlso = project.ReportAlso;
                        updateProject.ReportAlsoStr = project.ReportAlsoStr;
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = project;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = project;
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
                    updateResponse.Result = project;
                }
                return updateResponse;
            }
        }

        public Response<Project> Delete(long projectID)
        {
            Response<Project> deleteResponse = new Response<Project>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    Project deleteProject = db.Projects.SingleOrDefault(u => u.ProjectID == projectID);
                    if (deleteProject != null)
                    {
                        db.Projects.Remove(deleteProject);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteProject;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteProject;
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
        public List<ProjectViewModel> GetAllProject(int stratRowNumber, int endRowNumber, int ClientID, string ProjectNumber, int statusId, string batchNumber, out int totalRecordCount)
        {
            List<ProjectViewModel> projectViewModel = new List<ProjectViewModel>();
            totalRecordCount = 0;
            using (GLSPMContext db = new GLSPMContext())
            {
                var stratRowNumberParameter = new SqlParameter { ParameterName = "StratRowNumber", Value = stratRowNumber };
                var endRowNumberParameter = new SqlParameter { ParameterName = "EndRowNumber", Value = endRowNumber };
                var ClientIDParameter = new SqlParameter { ParameterName = "ClientID", Value = ClientID };
                var ProjectNumberParameter = new SqlParameter { ParameterName = "ProjectNumber", Value = ProjectNumber };
                var batchNumberParameter = new SqlParameter { ParameterName = "BatchNumber", Value = batchNumber };
                var statusIdParameter = new SqlParameter { ParameterName = "StatusId", Value = statusId };
                var totalRecordCountParameter = new SqlParameter { ParameterName = "TotalRecordCount", Value = totalRecordCount, Direction = System.Data.ParameterDirection.Output };
                try
                {
                    projectViewModel = db.Database.SqlQuery<ProjectViewModel>("GLS_GetAllProject @StratRowNumber,@EndRowNumber,@ClientID,@ProjectNumber,@StatusId,@BatchNumber, @TotalRecordCount OUTPUT", stratRowNumberParameter, endRowNumberParameter, ClientIDParameter, ProjectNumberParameter, statusIdParameter, batchNumberParameter, totalRecordCountParameter).ToList<ProjectViewModel>();
                    if(totalRecordCountParameter.Value != null)
                        totalRecordCount = Convert.ToInt16(totalRecordCountParameter.Value);
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<ProjectViewModel>();
                }
                return projectViewModel;
            }
        }
        public List<ProjectViewModel> GetAllProjectAPI(int stratRowNumber, int endRowNumber, int ClientID, string ProjectNumber, int statusId, string batchNumber, out int totalRecordCount
            , out int totalInprogressRecordCount
            , out int totalBatchGeneratedRecordCount
            , out int totalCompleteRecordCount
            , out int totalInvoicedRecordCount)
        {
            List<ProjectViewModel> projectViewModel = new List<ProjectViewModel>();
            totalRecordCount = 0;
            totalInprogressRecordCount = 0;
            totalBatchGeneratedRecordCount = 0;
            totalCompleteRecordCount = 0;
            totalInvoicedRecordCount = 0;
            using (GLSPMContext db = new GLSPMContext())
            {
                var stratRowNumberParameter = new SqlParameter { ParameterName = "StratRowNumber", Value = stratRowNumber };
                var endRowNumberParameter = new SqlParameter { ParameterName = "EndRowNumber", Value = endRowNumber };
                var ClientIDParameter = new SqlParameter { ParameterName = "ClientID", Value = ClientID };
                var ProjectNumberParameter = new SqlParameter { ParameterName = "ProjectNumber", Value = ProjectNumber };
                var batchNumberParameter = new SqlParameter { ParameterName = "BatchNumber", Value = batchNumber };
                var statusIdParameter = new SqlParameter { ParameterName = "StatusId", Value = statusId };
                var totalRecordCountParameter = new SqlParameter { ParameterName = "TotalRecordCount", Value = totalRecordCount, Direction = System.Data.ParameterDirection.Output };

                var totalInprogressRecordCountParameter = new SqlParameter { ParameterName = "TotalInProgressCount", Value = totalRecordCount, Direction = System.Data.ParameterDirection.Output };
                var totalBatchGeneratedRecordCountParameter = new SqlParameter { ParameterName = "TotalBatchGeneratedCount", Value = totalRecordCount, Direction = System.Data.ParameterDirection.Output };
                var totalCompleteRecordCountParameter = new SqlParameter { ParameterName = "TotalCompletedCount", Value = totalRecordCount, Direction = System.Data.ParameterDirection.Output };
                var totalInvoicedRecordCountParameter = new SqlParameter { ParameterName = "TotalInvoicedCount", Value = totalRecordCount, Direction = System.Data.ParameterDirection.Output };
                try
                {
                    projectViewModel = db.Database.SqlQuery<ProjectViewModel>("GLS_GetAllProject_API @StratRowNumber,@EndRowNumber,@ClientID,@ProjectNumber,@StatusId,@BatchNumber, @TotalRecordCount OUTPUT" +
                        ", @TotalInProgressCount OUTPUT, @TotalBatchGeneratedCount OUTPUT, @TotalCompletedCount OUTPUT, @TotalInvoicedCount OUTPUT"
                        , stratRowNumberParameter, endRowNumberParameter, ClientIDParameter, ProjectNumberParameter, statusIdParameter, batchNumberParameter, totalRecordCountParameter
                        , totalInprogressRecordCountParameter
                        , totalBatchGeneratedRecordCountParameter
                        , totalCompleteRecordCountParameter
                        , totalInvoicedRecordCountParameter
                        ).ToList<ProjectViewModel>();
                    if (totalRecordCountParameter.Value != null)
                        totalRecordCount = Convert.ToInt16(totalRecordCountParameter.Value);
                    if (totalInprogressRecordCountParameter.Value != null)
                        totalInprogressRecordCount = Convert.ToInt16(totalInprogressRecordCountParameter.Value);
                    if (totalBatchGeneratedRecordCountParameter.Value != null)
                        totalBatchGeneratedRecordCount = Convert.ToInt16(totalBatchGeneratedRecordCountParameter.Value);
                    if (totalCompleteRecordCountParameter.Value != null)
                        totalCompleteRecordCount = Convert.ToInt16(totalCompleteRecordCountParameter.Value);
                    if (totalInvoicedRecordCountParameter.Value != null)
                        totalInvoicedRecordCount = Convert.ToInt16(totalInvoicedRecordCountParameter.Value);
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<ProjectViewModel>();
                }
                return projectViewModel;
            }
        }
        
        public List<ProjectViewModel> GetAllProjectForReport(int stratRowNumber, int endRowNumber, int ClientID, string ProjectNumber, int statusId, string batchNumber, out int totalRecordCount)
        {
            List<ProjectViewModel> projectViewModel = new List<ProjectViewModel>();
            totalRecordCount = 0;
            using (GLSPMContext db = new GLSPMContext())
            {
                var stratRowNumberParameter = new SqlParameter { ParameterName = "StratRowNumber", Value = stratRowNumber };
                var endRowNumberParameter = new SqlParameter { ParameterName = "EndRowNumber", Value = endRowNumber };
                var ClientIDParameter = new SqlParameter { ParameterName = "ClientID", Value = ClientID };
                var ProjectNumberParameter = new SqlParameter { ParameterName = "ProjectNumber", Value = ProjectNumber };
                var batchNumberParameter = new SqlParameter { ParameterName = "BatchNumber", Value = batchNumber };
                var statusIdParameter = new SqlParameter { ParameterName = "StatusId", Value = statusId };
                var totalRecordCountParameter = new SqlParameter { ParameterName = "TotalRecordCount", Value = totalRecordCount, Direction = System.Data.ParameterDirection.Output };
                try
                {
                    projectViewModel = db.Database.SqlQuery<ProjectViewModel>("GLS_GetAllProjectForReport @StratRowNumber,@EndRowNumber,@ClientID,@ProjectNumber,@StatusId,@BatchNumber, @TotalRecordCount OUTPUT", stratRowNumberParameter, endRowNumberParameter, ClientIDParameter, ProjectNumberParameter, statusIdParameter, batchNumberParameter, totalRecordCountParameter).ToList<ProjectViewModel>();
                    if (totalRecordCountParameter.Value != null)
                        totalRecordCount = Convert.ToInt16(totalRecordCountParameter.Value);
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<ProjectViewModel>();
                }
                return projectViewModel;
            }
        }
        public List<ProjectViewModel> GetAllProjectForInvoiceReport(int stratRowNumber, int endRowNumber, int ClientID, string ProjectNumber, int statusId, string batchNumber, out int totalRecordCount)
        {
            List<ProjectViewModel> projectViewModel = new List<ProjectViewModel>();
            totalRecordCount = 0;
            using (GLSPMContext db = new GLSPMContext())
            {
                var stratRowNumberParameter = new SqlParameter { ParameterName = "StratRowNumber", Value = stratRowNumber };
                var endRowNumberParameter = new SqlParameter { ParameterName = "EndRowNumber", Value = endRowNumber };
                var ClientIDParameter = new SqlParameter { ParameterName = "ClientID", Value = ClientID };
                var ProjectNumberParameter = new SqlParameter { ParameterName = "ProjectNumber", Value = ProjectNumber };
                var batchNumberParameter = new SqlParameter { ParameterName = "BatchNumber", Value = batchNumber };
                var statusIdParameter = new SqlParameter { ParameterName = "StatusId", Value = statusId };
                var totalRecordCountParameter = new SqlParameter { ParameterName = "TotalRecordCount", Value = totalRecordCount, Direction = System.Data.ParameterDirection.Output };
                try
                {
                    projectViewModel = db.Database.SqlQuery<ProjectViewModel>("GLS_GetAllProjectForInvoiceReport @StratRowNumber,@EndRowNumber,@ClientID,@ProjectNumber,@StatusId,@BatchNumber, @TotalRecordCount OUTPUT", stratRowNumberParameter, endRowNumberParameter, ClientIDParameter, ProjectNumberParameter, statusIdParameter, batchNumberParameter, totalRecordCountParameter).ToList<ProjectViewModel>();
                    if (totalRecordCountParameter.Value != null)
                        totalRecordCount = Convert.ToInt16(totalRecordCountParameter.Value);
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<ProjectViewModel>();
                }
                return projectViewModel;
            }
        }
        public List<ProjectViewModel> GetAllProjectForAmendmentReport(int stratRowNumber, int endRowNumber, int ClientID, string ProjectNumber, int statusId, string batchNumber, out int totalRecordCount)
        {
            List<ProjectViewModel> projectViewModel = new List<ProjectViewModel>();
            totalRecordCount = 0;
            using (GLSPMContext db = new GLSPMContext())
            {
                var stratRowNumberParameter = new SqlParameter { ParameterName = "StratRowNumber", Value = stratRowNumber };
                var endRowNumberParameter = new SqlParameter { ParameterName = "EndRowNumber", Value = endRowNumber };
                var ClientIDParameter = new SqlParameter { ParameterName = "ClientID", Value = ClientID };
                var ProjectNumberParameter = new SqlParameter { ParameterName = "ProjectNumber", Value = ProjectNumber };
                var batchNumberParameter = new SqlParameter { ParameterName = "BatchNumber", Value = batchNumber };
                var statusIdParameter = new SqlParameter { ParameterName = "StatusId", Value = statusId };
                var totalRecordCountParameter = new SqlParameter { ParameterName = "TotalRecordCount", Value = totalRecordCount, Direction = System.Data.ParameterDirection.Output };
                try
                {
                    projectViewModel = db.Database.SqlQuery<ProjectViewModel>("GLS_GetAllProjectForAmendmentReport @StratRowNumber,@EndRowNumber,@ClientID,@ProjectNumber,@StatusId,@BatchNumber, @TotalRecordCount OUTPUT", stratRowNumberParameter, endRowNumberParameter, ClientIDParameter, ProjectNumberParameter, statusIdParameter, batchNumberParameter, totalRecordCountParameter).ToList<ProjectViewModel>();
                    if (totalRecordCountParameter.Value != null)
                        totalRecordCount = Convert.ToInt16(totalRecordCountParameter.Value);
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<ProjectViewModel>();
                }
                return projectViewModel;
            }
        }
        public List<ProjectViewModel> GetAllProjectForFilter()
        {
            List<ProjectViewModel> projectViewModel = new List<ProjectViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    projectViewModel = db.Database.SqlQuery<ProjectViewModel>("GLS_GetAllProjectForFilter").ToList<ProjectViewModel>();
                    
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<ProjectViewModel>();
                }
                return projectViewModel;
            }
        }
        public List<ProjectViewModel> GetAllProjectForBatchNumber(int stratRowNumber, int endRowNumber, int ClientID, string ProjectNumber, out int totalRecordCount)
        {
            List<ProjectViewModel> projectViewModel = new List<ProjectViewModel>();
            totalRecordCount = 0;
            using (GLSPMContext db = new GLSPMContext())
            {
                var stratRowNumberParameter = new SqlParameter { ParameterName = "StratRowNumber", Value = stratRowNumber };
                var endRowNumberParameter = new SqlParameter { ParameterName = "EndRowNumber", Value = endRowNumber };
                var ClientIDParameter = new SqlParameter { ParameterName = "ClientID", Value = ClientID };
                var ProjectNumberParameter = new SqlParameter { ParameterName = "ProjectNumber", Value = ProjectNumber };
                var totalRecordCountParameter = new SqlParameter { ParameterName = "TotalRecordCount", Value = totalRecordCount, Direction = System.Data.ParameterDirection.Output };
                try
                {
                    projectViewModel = db.Database.SqlQuery<ProjectViewModel>("GLS_GetAllProjectForBatchNumber @StratRowNumber,@EndRowNumber,@ClientID,@ProjectNumber, @TotalRecordCount OUTPUT", stratRowNumberParameter, endRowNumberParameter, ClientIDParameter, ProjectNumberParameter, totalRecordCountParameter).ToList<ProjectViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<ProjectViewModel>();
                }
                else
                {
                    try
                    {
                        totalRecordCount = Convert.ToInt32(totalRecordCountParameter.Value.ToString());
                    }
                    catch(Exception ex) { }
                }
                return projectViewModel;
            }
        }
        public ProjectViewModel GetByProjectId2(long projectId)
        {
            ProjectViewModel pvm = new ProjectViewModel();
            List<ProjectViewModel> projectViewModel = new List<ProjectViewModel>();
            string projectNumber = "123456789";
            using (GLSPMContext db = new GLSPMContext())
            {
                var projectIdParameter = new SqlParameter { ParameterName = "ProjectId", Value = projectId };
                SqlParameter projectNumberParameter = new SqlParameter { ParameterName = "ProjectNumber", Value = projectNumber, Direction = System.Data.ParameterDirection.Output, DbType = System.Data.DbType.String };
                projectNumberParameter.Size = 100;
                try
                {
                    //projectViewModel = db.Database.SqlQuery<ProjectViewModel>("GLS_GetByProjectId @ProjectId, @ProjectNumber OUTPUT", projectIdParameter, projectNumberParameter).ToList<ProjectViewModel>();
                    projectViewModel = db.Database.SqlQuery<ProjectViewModel>("GLS_GetByProjectId @ProjectId", projectIdParameter).ToList<ProjectViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel.Count == 0)
                {
                    try
                    {
                    }
                    catch (Exception ex) { }
                }
                else
                {
                    pvm = projectViewModel.FirstOrDefault();
                }
                return pvm;
            }
        }
        public ProjectViewModel GetByProjectId(long projectId)
        {
            ProjectViewModel pvm = new ProjectViewModel();
            List<ProjectViewModel> projectViewModel = new List<ProjectViewModel>();
            string projectNumber = "123456789";
            using (GLSPMContext db = new GLSPMContext())
            {
                var projectIdParameter = new SqlParameter { ParameterName = "ProjectId", Value = projectId };
                SqlParameter projectNumberParameter = new SqlParameter { ParameterName = "ProjectNumber", Value = projectNumber, Direction = System.Data.ParameterDirection.Output, DbType = System.Data.DbType.String };
                projectNumberParameter.Size = 100;
                try
                {
                    //projectViewModel = db.Database.SqlQuery<ProjectViewModel>("GLS_GetByProjectId @ProjectId, @ProjectNumber OUTPUT", projectIdParameter, projectNumberParameter).ToList<ProjectViewModel>();
                    projectViewModel = db.Database.SqlQuery<ProjectViewModel>("GLS_GetByProjectId @ProjectId", projectIdParameter).ToList<ProjectViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel.Count == 0)
                {
                    projectViewModel = new List<ProjectViewModel>();
                    pvm.ProjectSample = new List<ProjectSampleViewModel>();
                    try
                    {
                        //if (projectNumberParameter.Value != null)
                        //{
                        //    projectNumber = projectNumberParameter.Value.ToString();
                        //    pvm.ProjectNumber = projectNumber;
                        //}

                    }
                    catch (Exception ex) { }
                }
                else
                {
                    pvm = projectViewModel.FirstOrDefault();
                    List<ProjectSampleViewModel> projectSampleList = new ProjectSampleService().GetAllProjectSampleByProjectId(pvm.ProjectID);

                    pvm.ProjectSample = projectSampleList;
                }
                return pvm;
            }
        }
        public ProjectViewModel GetByProjectIdAPI(long projectId)
        {
            ProjectViewModel pvm = new ProjectViewModel();
            List<ProjectViewModel> projectViewModel = new List<ProjectViewModel>();
            string projectNumber = "123456789";
            using (GLSPMContext db = new GLSPMContext())
            {
                var projectIdParameter = new SqlParameter { ParameterName = "ProjectId", Value = projectId };
                SqlParameter projectNumberParameter = new SqlParameter { ParameterName = "ProjectNumber", Value = projectNumber, Direction = System.Data.ParameterDirection.Output, DbType = System.Data.DbType.String };
                projectNumberParameter.Size = 100;
                try
                {
                    projectViewModel = db.Database.SqlQuery<ProjectViewModel>("GLS_GetByProjectId_API @ProjectId, @ProjectNumber OUTPUT", projectIdParameter, projectNumberParameter).ToList<ProjectViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel.Count == 0)
                {
                    projectViewModel = new List<ProjectViewModel>();
                    pvm.ProjectSample = new List<ProjectSampleViewModel>();
                    try
                    {
                        projectNumber = projectNumberParameter.Value.ToString();
                        pvm.ProjectNumber = projectNumber;
                        pvm.ReceivedDate = DateTime.Now;
                        // pvm.SamplingDate = DateTime.Now;

                    }
                    catch (Exception ex) { }
                }
                else
                {
                    pvm = projectViewModel.FirstOrDefault();
                    List<ProjectSampleViewModel> projectSampleList = new ProjectSampleService().GetAllProjectSampleByProjectId(pvm.ProjectID);

                    pvm.ProjectSample = projectSampleList;
                }
                return pvm;
            }
        }
        public AmendmentProjectCheck CheckIsProjectAmendment(long projectId)
        {
            AmendmentProjectCheck pvm = new AmendmentProjectCheck();
            List<AmendmentProjectCheck> projectViewModel = new List<AmendmentProjectCheck>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var projectIdParameter = new SqlParameter { ParameterName = "ProjectId", Value = projectId };
                try
                {
                    projectViewModel = db.Database.SqlQuery<AmendmentProjectCheck>("GLS_CheckIsProjectAmendment @ProjectId", projectIdParameter).ToList<AmendmentProjectCheck>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<AmendmentProjectCheck>();
                }
                else
                {
                    pvm = projectViewModel.FirstOrDefault();
                }
                return pvm;
            }
        }
        
        #endregion
    }
}
