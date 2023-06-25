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
    public class AmendmentPCMService
    {
        #region Common Methods

        public List<AmendmentPCM> All()
        {
            List<AmendmentPCM> AmendmentPCM = new List<AmendmentPCM>();

            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentPCM = db.AmendmentPCMs.OrderBy(f => f.Id).AsParallel().ToList();

                if (AmendmentPCM == null)
                {
                    AmendmentPCM = new List<AmendmentPCM>();
                }
                return AmendmentPCM;
            }
        }
        public List<AmendmentPCM> AllByProjectId(long projectId)
        {
            List<AmendmentPCM> AmendmentPCM = new List<AmendmentPCM>();

            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentPCM = db.AmendmentPCMs.Where(p=>p.ProjectId == projectId).OrderBy(f => f.Id).AsParallel().ToList();

                if (AmendmentPCM == null)
                {
                    AmendmentPCM = new List<AmendmentPCM>();
                }
                return AmendmentPCM;
            }
        }
        public AmendmentPCM GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentPCM AmendmentPCM = db.AmendmentPCMs.SingleOrDefault(u => u.Id == userRoleId);
                return AmendmentPCM;
            }
        }
        public AmendmentPCM GetBySampleID(long sampleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentPCM AmendmentPCM = db.AmendmentPCMs.SingleOrDefault(u => u.SampleId == sampleId && (u.IsDuplicate == false || u.IsDuplicate == null));
                return AmendmentPCM;
            }
        }
        public AmendmentPCM GetByDuplicateSampleID(long sampleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentPCM AmendmentPCM = db.AmendmentPCMs.SingleOrDefault(u => u.SampleId == sampleId && u.IsDuplicate == true);
                return AmendmentPCM;
            }
        }
        public AmendmentPCM GetBySampleIDAndAmendment(long sampleId, string amendment)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentPCM AmendmentPCM = db.AmendmentPCMs.SingleOrDefault(u => u.SampleId == sampleId && u.Amendment == amendment);
                return AmendmentPCM;
            }
        }
        public AmendmentPCM GetByDuplicateSample(long projectId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentPCM AmendmentPCM = db.AmendmentPCMs.SingleOrDefault(u => u.ProjectId == projectId && u.IsDuplicate == true);
                return AmendmentPCM;
            }
        }
        public Response<AmendmentPCM> Add(AmendmentPCM AmendmentPCM)
        {
            Response<AmendmentPCM> addResponse = new Response<AmendmentPCM>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.AmendmentPCMs.Add(AmendmentPCM);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = AmendmentPCM;
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
                    addResponse.Result = AmendmentPCM;
                }
                return addResponse;
            }
        }

        public Response<AmendmentPCM> Update(AmendmentPCM AmendmentPCM)
        {
            Response<AmendmentPCM> updateResponse = new Response<AmendmentPCM>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    AmendmentPCM updateAmendmentPCM = db.AmendmentPCMs.SingleOrDefault(u => u.AmendmentPCMId == AmendmentPCM.AmendmentPCMId);
                    if (updateAmendmentPCM != null)
                    {
                        //updateAmendmentPCM = userRole;
                        updateAmendmentPCM.Id = AmendmentPCM.Id;
                        updateAmendmentPCM.ProjectId = AmendmentPCM.ProjectId;
                        updateAmendmentPCM.SampleId = AmendmentPCM.SampleId;
                        updateAmendmentPCM.FieldBlankNumber = AmendmentPCM.FieldBlankNumber;
                        updateAmendmentPCM.RawFiberCountHalf = AmendmentPCM.RawFiberCountHalf;
                        updateAmendmentPCM.RawFiberCountWhole = AmendmentPCM.RawFiberCountWhole;
                        updateAmendmentPCM.FieldsCounted = AmendmentPCM.FieldsCounted;
                        updateAmendmentPCM.VolumeL = AmendmentPCM.VolumeL;
                        updateAmendmentPCM.FilterAreaMM = AmendmentPCM.FilterAreaMM;
                        updateAmendmentPCM.CalculatedFiberCount = AmendmentPCM.CalculatedFiberCount;
                        updateAmendmentPCM.CalculatedFiberPermm = AmendmentPCM.CalculatedFiberPermm;
                        updateAmendmentPCM.CalculatedFiberPercc = AmendmentPCM.CalculatedFiberPercc;
                        updateAmendmentPCM.ReportedFiberPermm = AmendmentPCM.ReportedFiberPermm;
                        updateAmendmentPCM.ReportedFiberPercc = AmendmentPCM.ReportedFiberPercc;
                        updateAmendmentPCM.LOD = AmendmentPCM.LOD;
                        updateAmendmentPCM.SampleDate = AmendmentPCM.SampleDate;
                        updateAmendmentPCM.IsBlank = AmendmentPCM.IsBlank;
                        updateAmendmentPCM.IsDuplicate = AmendmentPCM.IsDuplicate;
                        updateAmendmentPCM.Amendment = AmendmentPCM.Amendment;
                        updateAmendmentPCM.Comment = AmendmentPCM.Comment;
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = AmendmentPCM;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = AmendmentPCM;
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
                    updateResponse.Result = AmendmentPCM;
                }
                return updateResponse;
            }
        }

        public Response<AmendmentPCM> Delete(long Id)
        {
            Response<AmendmentPCM> deleteResponse = new Response<AmendmentPCM>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    AmendmentPCM deleteAmendmentPCM = db.AmendmentPCMs.SingleOrDefault(u => u.Id == Id);
                    if (deleteAmendmentPCM != null)
                    {
                        db.AmendmentPCMs.Remove(deleteAmendmentPCM);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteAmendmentPCM;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteAmendmentPCM;
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
        public List<ProjectSamplePCMViewModel> GetAllProjectSamplePCMData(long sampleId, string AmendmentNumber)
        {
            List<ProjectSamplePCMViewModel> projectViewModel = new List<ProjectSamplePCMViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var sampleIdParameter = new SqlParameter { ParameterName = "SampleId", Value = sampleId };
                var AmendmentNumberParameter = new SqlParameter { ParameterName = "AmendmentNumber", Value = AmendmentNumber }; 
                try
                {
                    projectViewModel = db.Database.SqlQuery<ProjectSamplePCMViewModel>("GLS_GetAllProjectSamplePCMDataAmendment @SampleId,@AmendmentNumber", sampleIdParameter, AmendmentNumberParameter).ToList<ProjectSamplePCMViewModel>();
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
        #endregion
    }
}
