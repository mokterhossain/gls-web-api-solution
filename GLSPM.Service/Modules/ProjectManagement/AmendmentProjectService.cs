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
    public class AmendmentProjectService
    {
        #region Common Methods

        public List<AmendmentProject> All()
        {
            List<AmendmentProject> AmendmentProject = new List<AmendmentProject>();

            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentProject = db.AmendmentProjects.OrderBy(f => f.AmendmentProjectID).AsParallel().ToList();

                if (AmendmentProject == null)
                {
                    AmendmentProject = new List<AmendmentProject>();
                }
                return AmendmentProject;
            }
        }
        public List<AmendmentProject> AllByProjectId(long projectId)
        {
            List<AmendmentProject> AmendmentProject = new List<AmendmentProject>();

            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentProject = db.AmendmentProjects.Where(p=> p.ProjectID == projectId).OrderBy(f => f.Amendment).AsParallel().ToList();

                if (AmendmentProject == null)
                {
                    AmendmentProject = new List<AmendmentProject>();
                }
                return AmendmentProject;
            }
        }
        public AmendmentProject GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentProject AmendmentProject = db.AmendmentProjects.SingleOrDefault(u => u.AmendmentProjectID == userRoleId);
                return AmendmentProject;
            }
        }
        public AmendmentProject GetByProjectIDAndAmendment(long projectId, string amendment)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentProject AmendmentProject = db.AmendmentProjects.SingleOrDefault(u => u.ProjectID == projectId && u.Amendment == amendment);
                return AmendmentProject;
            }
        }
        public AmendmentProject GetByClientID(long clientId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentProject AmendmentProject = db.AmendmentProjects.Where(p => p.ClientID == clientId).OrderByDescending(p => p.AmendmentProjectID).FirstOrDefault();
                return AmendmentProject;
            }
        }
        public AmendmentProject GetByProjectNumber(string ProjectNumber)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentProject AmendmentProject = db.AmendmentProjects.Where(p => p.ProjectNumber == ProjectNumber).OrderByDescending(p => p.AmendmentProjectID).FirstOrDefault();
                return AmendmentProject;
            }
        }
        public Response<AmendmentProject> Add(AmendmentProject AmendmentProject)
        {
            Response<AmendmentProject> addResponse = new Response<AmendmentProject>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.AmendmentProjects.Add(AmendmentProject);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = AmendmentProject;
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
                    addResponse.Result = AmendmentProject;
                }
                return addResponse;
            }
        }

        public Response<AmendmentProject> Update(AmendmentProject AmendmentProject)
        {
            Response<AmendmentProject> updateResponse = new Response<AmendmentProject>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    AmendmentProject updateAmendmentProject = db.AmendmentProjects.SingleOrDefault(u => u.AmendmentProjectID == AmendmentProject.AmendmentProjectID);
                    if (updateAmendmentProject != null)
                    {
                        //updateAmendmentProject = userRole;
                        updateAmendmentProject.AmendmentProjectID = AmendmentProject.AmendmentProjectID;
                        updateAmendmentProject.ProjectNumber = AmendmentProject.ProjectNumber;
                        updateAmendmentProject.ClientID = AmendmentProject.ClientID;
                        updateAmendmentProject.Address = AmendmentProject.Address;
                        updateAmendmentProject.JobNumber = AmendmentProject.JobNumber;
                        updateAmendmentProject.ReportTo = AmendmentProject.ReportTo;
                        updateAmendmentProject.CellNo = AmendmentProject.CellNo;
                        updateAmendmentProject.OfficePhone = AmendmentProject.OfficePhone;
                        updateAmendmentProject.ReceivedDate = AmendmentProject.ReceivedDate;
                        updateAmendmentProject.DueDate = AmendmentProject.DueDate;
                        updateAmendmentProject.ClientEmail = AmendmentProject.ClientEmail;
                        updateAmendmentProject.Comments = AmendmentProject.Comments;
                        updateAmendmentProject.NumberOfSample = AmendmentProject.NumberOfSample;
                        updateAmendmentProject.DateOfReported = AmendmentProject.DateOfReported;
                        updateAmendmentProject.DateOfAnalyzed = AmendmentProject.DateOfAnalyzed;
                        updateAmendmentProject.ProjectStatusId = AmendmentProject.ProjectStatusId;
                        updateAmendmentProject.AnalystId = AmendmentProject.AnalystId;
                        updateAmendmentProject.LabratoryManagerId = AmendmentProject.LabratoryManagerId;
                        updateAmendmentProject.ProjectType = AmendmentProject.ProjectType;
                        updateAmendmentProject.SampledBy = AmendmentProject.SampledBy;
                        updateAmendmentProject.CreatedOn = AmendmentProject.CreatedOn;
                        updateAmendmentProject.CreatedBy = AmendmentProject.CreatedBy;
                        updateAmendmentProject.UpdatedOn = AmendmentProject.UpdatedOn;
                        updateAmendmentProject.UpdatedBy = AmendmentProject.UpdatedBy;
                        updateAmendmentProject.ClientName = AmendmentProject.ClientName;
                        updateAmendmentProject.ReportToName = AmendmentProject.ReportToName;
                        updateAmendmentProject.Amendment = AmendmentProject.Amendment;
                        updateAmendmentProject.SamplingDate = AmendmentProject.SamplingDate;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = AmendmentProject;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = AmendmentProject;
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
                    updateResponse.Result = AmendmentProject;
                }
                return updateResponse;
            }
        }

        public Response<AmendmentProject> Delete(long AmendmentProjectID)
        {
            Response<AmendmentProject> deleteResponse = new Response<AmendmentProject>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    AmendmentProject deleteAmendmentProject = db.AmendmentProjects.SingleOrDefault(u => u.AmendmentProjectID == AmendmentProjectID);
                    if (deleteAmendmentProject != null)
                    {
                        db.AmendmentProjects.Remove(deleteAmendmentProject);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteAmendmentProject;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteAmendmentProject;
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
        public List<AmendmentProjectViewModel> GetAllAmendmentProject(int stratRowNumber, int endRowNumber, int ClientID, string ProjectNumber, int statusId, string batchNumber, out int totalRecordCount)
        {
            List<AmendmentProjectViewModel> AmendmentProjectViewModel = new List<AmendmentProjectViewModel>();
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
                    AmendmentProjectViewModel = db.Database.SqlQuery<AmendmentProjectViewModel>("GLS_GetAllAmendmentProject @StratRowNumber,@EndRowNumber,@ClientID,@ProjectNumber,@StatusId,@BatchNumber, @TotalRecordCount OUTPUT", stratRowNumberParameter, endRowNumberParameter, ClientIDParameter, ProjectNumberParameter, statusIdParameter, batchNumberParameter, totalRecordCountParameter).ToList<AmendmentProjectViewModel>();
                    if (totalRecordCountParameter.Value != null)
                        totalRecordCount = Convert.ToInt16(totalRecordCountParameter.Value);
                }
                catch (Exception ex) { }
                if (AmendmentProjectViewModel == null)
                {
                    AmendmentProjectViewModel = new List<AmendmentProjectViewModel>();
                }
                return AmendmentProjectViewModel;
            }
        }
        public List<AmendmentProjectViewModel> GetAllAmendmentProjectForFilter()
        {
            List<AmendmentProjectViewModel> AmendmentProjectViewModel = new List<AmendmentProjectViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    AmendmentProjectViewModel = db.Database.SqlQuery<AmendmentProjectViewModel>("GLS_GetAllAmendmentProjectForFilter").ToList<AmendmentProjectViewModel>();

                }
                catch (Exception ex) { }
                if (AmendmentProjectViewModel == null)
                {
                    AmendmentProjectViewModel = new List<AmendmentProjectViewModel>();
                }
                return AmendmentProjectViewModel;
            }
        }
        public List<AmendmentProjectViewModel> GetAllAmendmentProjectForBatchNumber(int stratRowNumber, int endRowNumber, int ClientID, string ProjectNumber, out int totalRecordCount)
        {
            List<AmendmentProjectViewModel> AmendmentProjectViewModel = new List<AmendmentProjectViewModel>();
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
                    AmendmentProjectViewModel = db.Database.SqlQuery<AmendmentProjectViewModel>("GLS_GetAllAmendmentProjectForBatchNumber @StratRowNumber,@EndRowNumber,@ClientID,@ProjectNumber, @TotalRecordCount OUTPUT", stratRowNumberParameter, endRowNumberParameter, ClientIDParameter, ProjectNumberParameter, totalRecordCountParameter).ToList<AmendmentProjectViewModel>();
                }
                catch (Exception ex) { }
                if (AmendmentProjectViewModel == null)
                {
                    AmendmentProjectViewModel = new List<AmendmentProjectViewModel>();
                }
                else
                {
                    totalRecordCount = Convert.ToInt32(totalRecordCountParameter.Value.ToString());
                }
                return AmendmentProjectViewModel;
            }
        }
        public AmendmentProjectViewModel GetByAmendmentProjectId(long AmendmentProjectId)
        {
            AmendmentProjectViewModel pvm = new AmendmentProjectViewModel();
            List<AmendmentProjectViewModel> AmendmentProjectViewModel = new List<AmendmentProjectViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var AmendmentProjectIdParameter = new SqlParameter { ParameterName = "AmendmentProjectId", Value = AmendmentProjectId };
                try
                {
                    AmendmentProjectViewModel = db.Database.SqlQuery<AmendmentProjectViewModel>("GLS_GetByAmendmentProjectId @AmendmentProjectId", AmendmentProjectIdParameter).ToList<AmendmentProjectViewModel>();
                }
                catch (Exception ex) { }
                if (AmendmentProjectViewModel == null)
                {
                    AmendmentProjectViewModel = new List<AmendmentProjectViewModel>();
                }
                else
                {
                    pvm = AmendmentProjectViewModel.FirstOrDefault();
                }
                return pvm;
            }
        }
        public AmendmentProjectViewModel GetByProjectId(long projectId, string amendmentNumber)
        {
            AmendmentProjectViewModel pvm = new AmendmentProjectViewModel();
            List<AmendmentProjectViewModel> projectViewModel = new List<AmendmentProjectViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var projectIdParameter = new SqlParameter { ParameterName = "ProjectId", Value = projectId };
                var amendmentNumberParameter = new SqlParameter { ParameterName = "AmendmentNumber", Value = amendmentNumber };
                try
                {
                    projectViewModel = db.Database.SqlQuery<AmendmentProjectViewModel>("GLS_GetAmendmentProjectByProjectId @ProjectId,@AmendmentNumber", projectIdParameter, amendmentNumberParameter).ToList<AmendmentProjectViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<AmendmentProjectViewModel>();
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
