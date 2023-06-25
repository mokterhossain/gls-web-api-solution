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
    public class PCMService
    {
        #region Common Methods

        public List<PCM> All()
        {
            List<PCM> PCM = new List<PCM>();

            using (GLSPMContext db = new GLSPMContext())
            {
                PCM = db.PCMs.OrderBy(f => f.Id).AsParallel().ToList();

                if (PCM == null)
                {
                    PCM = new List<PCM>();
                }
                return PCM;
            }
        }
        public List<PCM> AllByProjectId(long projectId)
        {
            List<PCM> PCM = new List<PCM>();

            using (GLSPMContext db = new GLSPMContext())
            {
                PCM = db.PCMs.Where(p=> p.ProjectId == projectId).OrderBy(f => f.Id).AsParallel().ToList();

                if (PCM == null)
                {
                    PCM = new List<PCM>();
                }
                return PCM;
            }
        }
        public PCM GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                PCM PCM = db.PCMs.SingleOrDefault(u => u.Id == userRoleId);
                return PCM;
            }
        }
        public PCM GetBySampleID(long sampleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                PCM PCM = db.PCMs.SingleOrDefault(u => u.SampleId == sampleId && (u.IsDuplicate == false || u.IsDuplicate == null));
                return PCM;
            }
        }
        public PCM GetByDuplicateSampleID(long sampleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                PCM PCM = db.PCMs.SingleOrDefault(u => u.SampleId == sampleId && u.IsDuplicate == true);
                return PCM;
            }
        }
        public PCM GetByDuplicateSample(long projectId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                PCM PCM = db.PCMs.SingleOrDefault(u => u.ProjectId == projectId && u.IsDuplicate == true);
                return PCM;
            }
        }
        public Response<PCM> Add(PCM PCM)
        {
            Response<PCM> addResponse = new Response<PCM>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.PCMs.Add(PCM);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = PCM;
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
                    addResponse.Result = PCM;
                }
                return addResponse;
            }
        }

        public Response<PCM> Update(PCM PCM)
        {
            Response<PCM> updateResponse = new Response<PCM>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    PCM updatePCM = db.PCMs.SingleOrDefault(u => u.Id == PCM.Id);
                    if (updatePCM != null)
                    {
                        //updatePCM = userRole;
                        updatePCM.Id = PCM.Id;
                        updatePCM.ProjectId = PCM.ProjectId;
                        updatePCM.SampleId = PCM.SampleId;
                        updatePCM.FieldBlankNumber = PCM.FieldBlankNumber;
                        updatePCM.RawFiberCountHalf = PCM.RawFiberCountHalf;
                        updatePCM.RawFiberCountWhole = PCM.RawFiberCountWhole;
                        updatePCM.FieldsCounted = PCM.FieldsCounted;
                        updatePCM.VolumeL = PCM.VolumeL;
                        updatePCM.FilterAreaMM = PCM.FilterAreaMM;
                        updatePCM.CalculatedFiberCount = PCM.CalculatedFiberCount;
                        updatePCM.CalculatedFiberPermm = PCM.CalculatedFiberPermm;
                        updatePCM.CalculatedFiberPercc = PCM.CalculatedFiberPercc;
                        updatePCM.ReportedFiberPermm = PCM.ReportedFiberPermm;
                        updatePCM.ReportedFiberPercc = PCM.ReportedFiberPercc;
                        updatePCM.LOD = PCM.LOD;
                        updatePCM.SampleDate = PCM.SampleDate;
                        updatePCM.IsBlank = PCM.IsBlank;
                        updatePCM.IsDuplicate = PCM.IsDuplicate;
                        updatePCM.Comment = PCM.Comment;
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = PCM;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = PCM;
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
                    updateResponse.Result = PCM;
                }
                return updateResponse;
            }
        }

        public Response<PCM> Delete(long Id)
        {
            Response<PCM> deleteResponse = new Response<PCM>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    PCM deletePCM = db.PCMs.SingleOrDefault(u => u.Id == Id);
                    if (deletePCM != null)
                    {
                        db.PCMs.Remove(deletePCM);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deletePCM;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deletePCM;
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
        public List<ProjectSamplePCMViewModel> GetAllProjectSamplePCMData(long sampleId)
        {
            List<ProjectSamplePCMViewModel> projectViewModel = new List<ProjectSamplePCMViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var sampleIdParameter = new SqlParameter { ParameterName = "SampleId", Value = sampleId };
                try
                {
                    projectViewModel = db.Database.SqlQuery<ProjectSamplePCMViewModel>("GLS_GetAllProjectSamplePCMData @SampleId", sampleIdParameter).ToList<ProjectSamplePCMViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<ProjectSamplePCMViewModel>();
                }
                return projectViewModel;
            }
        }
        public List<ProjectSamplePCMViewModel> GetAllProjectSamplePCMDataAmendment(long sampleId)
        {
            List<ProjectSamplePCMViewModel> projectViewModel = new List<ProjectSamplePCMViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var sampleIdParameter = new SqlParameter { ParameterName = "SampleId", Value = sampleId };
                try
                {
                    projectViewModel = db.Database.SqlQuery<ProjectSamplePCMViewModel>("GLS_GetAllProjectSamplePCMDataAmendment @SampleId", sampleIdParameter).ToList<ProjectSamplePCMViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<ProjectSamplePCMViewModel>();
                }
                return projectViewModel;
            }
        }
        public List<PCM> GetPCMByProjectId(long projectId)
        {
            List<PCM> projectViewModel = new List<PCM>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var projectIdParameter = new SqlParameter { ParameterName = "@ProjectId", Value = projectId };
                try
                {
                    projectViewModel = db.Database.SqlQuery<PCM>("GLS_GetPCMByProjectId @ProjectId", projectIdParameter).ToList<PCM>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<PCM>();
                }
                return projectViewModel;
            }
        }
        #endregion
    }
}
