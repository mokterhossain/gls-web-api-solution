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
    public class AmendmentProjectSampleService
    {
        #region Common Methods

        public List<AmendmentProjectSample> All()
        {
            List<AmendmentProjectSample> AmendmentProjectSample = new List<AmendmentProjectSample>();

            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentProjectSample = db.AmendmentProjectSamples.OrderBy(f => f.SampleId).AsParallel().ToList();

                if (AmendmentProjectSample == null)
                {
                    AmendmentProjectSample = new List<AmendmentProjectSample>();
                }
                return AmendmentProjectSample;
            }
        }

        public AmendmentProjectSample GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentProjectSample AmendmentProjectSample = db.AmendmentProjectSamples.SingleOrDefault(u => u.Id == userRoleId);
                return AmendmentProjectSample;
            }
        }
        public AmendmentProjectSample GetBySampleIDAndAmendment(long SampleId, string amendment)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentProjectSample AmendmentProjectSample = db.AmendmentProjectSamples.SingleOrDefault(u => u.SampleId == SampleId && u.Amendment == amendment);
                return AmendmentProjectSample;
            }
        }
        public AmendmentProjectSample GetByLabID(string labId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentProjectSample AmendmentProjectSample = db.AmendmentProjectSamples.SingleOrDefault(u => u.LabId == labId);
                return AmendmentProjectSample;
            }
        }
        public List<AmendmentProjectSample> GetByAmendmentProjectId(long AmendmentProjectId)
        {
            List<AmendmentProjectSample> AmendmentProjectSample = new List<AmendmentProjectSample>();

            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentProjectSample = db.AmendmentProjectSamples.OrderBy(f => f.SampleId).Where(p => p.ProjectId == AmendmentProjectId).AsParallel().ToList();

                if (AmendmentProjectSample == null)
                {
                    AmendmentProjectSample = new List<AmendmentProjectSample>();
                }
                return AmendmentProjectSample;
            }
        }
        public Response<AmendmentProjectSample> Add(AmendmentProjectSample AmendmentProjectSample)
        {
            Response<AmendmentProjectSample> addResponse = new Response<AmendmentProjectSample>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.AmendmentProjectSamples.Add(AmendmentProjectSample);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = AmendmentProjectSample;
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
                    addResponse.Result = AmendmentProjectSample;
                }
                return addResponse;
            }
        }

        public Response<AmendmentProjectSample> Update(AmendmentProjectSample AmendmentProjectSample)
        {
            Response<AmendmentProjectSample> updateResponse = new Response<AmendmentProjectSample>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    AmendmentProjectSample updateAmendmentProjectSample = db.AmendmentProjectSamples.SingleOrDefault(u => u.Id == AmendmentProjectSample.Id);
                    if (updateAmendmentProjectSample != null)
                    {
                        //updateAmendmentProjectSample = userRole;
                        updateAmendmentProjectSample.Id = AmendmentProjectSample.Id;
                        updateAmendmentProjectSample.SampleId = AmendmentProjectSample.SampleId;
                        updateAmendmentProjectSample.ProjectId = AmendmentProjectSample.ProjectId;
                        updateAmendmentProjectSample.BatchNumber = AmendmentProjectSample.BatchNumber;
                        updateAmendmentProjectSample.SerialNo = AmendmentProjectSample.SerialNo;
                        updateAmendmentProjectSample.LabId = AmendmentProjectSample.LabId;
                        updateAmendmentProjectSample.SampleType = AmendmentProjectSample.SampleType;
                        updateAmendmentProjectSample.TAT = AmendmentProjectSample.TAT;
                        updateAmendmentProjectSample.Location = AmendmentProjectSample.Location;
                        updateAmendmentProjectSample.Analyst = AmendmentProjectSample.Analyst;
                        updateAmendmentProjectSample.Note = AmendmentProjectSample.Note;
                        updateAmendmentProjectSample.SampleCompositeHomogeneity = AmendmentProjectSample.SampleCompositeHomogeneity;
                        updateAmendmentProjectSample.CompositeNonAsbestosContents = AmendmentProjectSample.CompositeNonAsbestosContents;
                        updateAmendmentProjectSample.SampleCompositeHomogeneityText = AmendmentProjectSample.SampleCompositeHomogeneityText;
                        updateAmendmentProjectSample.CompositeNonAsbestosContentsText = AmendmentProjectSample.CompositeNonAsbestosContentsText;
                        updateAmendmentProjectSample.TATText = AmendmentProjectSample.TATText;
                        updateAmendmentProjectSample.AnalystName = AmendmentProjectSample.AnalystName;
                        updateAmendmentProjectSample.IsQC = AmendmentProjectSample.IsQC;
                        updateAmendmentProjectSample.Matrix = AmendmentProjectSample.Matrix;
                        updateAmendmentProjectSample.PackageCode = AmendmentProjectSample.PackageCode;
                        //updateAmendmentProjectSample.Homogeneity = AmendmentProjectSample.Homogeneity;
                        //updateAmendmentProjectSample.Content = AmendmentProjectSample.Content;
                        //updateAmendmentProjectSample.AbsestosPercentage = AmendmentProjectSample.AbsestosPercentage;
                        //updateAmendmentProjectSample.IsBilable = AmendmentProjectSample.IsBilable;
                        updateAmendmentProjectSample.CreatedOn = AmendmentProjectSample.CreatedOn;
                        updateAmendmentProjectSample.CreatedBy = AmendmentProjectSample.CreatedBy;
                        updateAmendmentProjectSample.UpdatedOn = AmendmentProjectSample.UpdatedOn;
                        updateAmendmentProjectSample.UpdatedBy = AmendmentProjectSample.UpdatedBy;
                        updateAmendmentProjectSample.Amendment = AmendmentProjectSample.Amendment;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = AmendmentProjectSample;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = AmendmentProjectSample;
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
                    updateResponse.Result = AmendmentProjectSample;
                }
                return updateResponse;
            }
        }

        public Response<AmendmentProjectSample> Delete(long SampleId)
        {
            Response<AmendmentProjectSample> deleteResponse = new Response<AmendmentProjectSample>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    AmendmentProjectSample deleteAmendmentProjectSample = db.AmendmentProjectSamples.SingleOrDefault(u => u.Id == SampleId);
                    if (deleteAmendmentProjectSample != null)
                    {
                        db.AmendmentProjectSamples.Remove(deleteAmendmentProjectSample);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteAmendmentProjectSample;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteAmendmentProjectSample;
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
        public List<AmendmentProjectSampleViewModel> GetAllAmendmentProjectSampleByAmendmentProjectId(long AmendmentProjectId)
        {
            List<AmendmentProjectSampleViewModel> projectViewModel = new List<AmendmentProjectSampleViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var AmendmentProjectIdParameter = new SqlParameter { ParameterName = "AmendmentProjectId", Value = AmendmentProjectId };
                try
                {
                    projectViewModel = db.Database.SqlQuery<AmendmentProjectSampleViewModel>("GLS_GetAllAmendmentProjectSampleByAmendmentProjectId @AmendmentProjectId", AmendmentProjectIdParameter).ToList<AmendmentProjectSampleViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<AmendmentProjectSampleViewModel>();
                }
                return projectViewModel;
            }
        }
        public List<AmendmentProjectSampleViewModel> GetAllAmendmentProjectSampleByBatchNumber(string batchNumber)
        {
            List<AmendmentProjectSampleViewModel> projectViewModel = new List<AmendmentProjectSampleViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var batchNumberParameter = new SqlParameter { ParameterName = "BatchNumber", Value = batchNumber };
                try
                {
                    projectViewModel = db.Database.SqlQuery<AmendmentProjectSampleViewModel>("GLS_GetAllAmendmentProjectSampleByBatchNumber @BatchNumber", batchNumberParameter).ToList<AmendmentProjectSampleViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<AmendmentProjectSampleViewModel>();
                }
                return projectViewModel;
            }
        }
        public List<AmendmentProjectSampleViewModel> GetAllAmendmentProjectSampleByAmendmentProjectIds(string AmendmentProjectIds)
        {
            List<AmendmentProjectSampleViewModel> projectViewModel = new List<AmendmentProjectSampleViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var AmendmentProjectIdParameter = new SqlParameter { ParameterName = "AmendmentProjectIds", Value = AmendmentProjectIds };
                try
                {
                    projectViewModel = db.Database.SqlQuery<AmendmentProjectSampleViewModel>("GLS_GetAllAmendmentProjectSampleByAmendmentProjectIds @AmendmentProjectIds", AmendmentProjectIdParameter).ToList<AmendmentProjectSampleViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<AmendmentProjectSampleViewModel>();
                }
                return projectViewModel;
            }
        }
        public Response<AmendmentProjectSampleUpdateResult> UpdateBatchNumber(string sampleIds)
        {
            AmendmentProjectSampleUpdateResult processResult = new AmendmentProjectSampleUpdateResult();

            Response<AmendmentProjectSampleUpdateResult> processResponse = new Response<AmendmentProjectSampleUpdateResult>();
            using (GLSPMContext db = new GLSPMContext())
            {
                //ClassWiseHead = db.ClassWiseHeads.OrderBy(f => f.ID).AsParallel().ToList();
                var sampleIdsParameter = new SqlParameter { ParameterName = "SampleIds", Value = sampleIds };
                try
                {
                    processResult = db.Database.SqlQuery<AmendmentProjectSampleUpdateResult>("GLS_UpdateBatchNumber @SampleIds", sampleIdsParameter).ToList<AmendmentProjectSampleUpdateResult>().FirstOrDefault();
                }
                catch (Exception ex) { }
                if (processResult == null)
                {
                    processResult = new AmendmentProjectSampleUpdateResult();
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
        public Response<AmendmentProjectSampleUpdateResult> AddSampleToBatch(string sampleIds, string batchNumber)
        {
            AmendmentProjectSampleUpdateResult processResult = new AmendmentProjectSampleUpdateResult();

            Response<AmendmentProjectSampleUpdateResult> processResponse = new Response<AmendmentProjectSampleUpdateResult>();
            using (GLSPMContext db = new GLSPMContext())
            {
                //ClassWiseHead = db.ClassWiseHeads.OrderBy(f => f.ID).AsParallel().ToList();
                var sampleIdsParameter = new SqlParameter { ParameterName = "SampleIds", Value = sampleIds };
                var batchNumberParameter = new SqlParameter { ParameterName = "BatchNumber", Value = batchNumber };
                try
                {
                    processResult = db.Database.SqlQuery<AmendmentProjectSampleUpdateResult>("GLS_AddSampleToBatch @SampleIds,@BatchNumber", sampleIdsParameter, batchNumberParameter).ToList<AmendmentProjectSampleUpdateResult>().FirstOrDefault();
                }
                catch (Exception ex) { }
                if (processResult == null)
                {
                    processResult = new AmendmentProjectSampleUpdateResult();
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
        public List<AmendmentProjectSampleViewModel> GetAllAmendmentProjectSampleForQC(int start, int limit, out int totalRecordCount)
        {
            List<AmendmentProjectSampleViewModel> projectViewModel = new List<AmendmentProjectSampleViewModel>();
            totalRecordCount = 0;
            using (GLSPMContext db = new GLSPMContext())
            {
                //var AmendmentProjectIdParameter = new SqlParameter { ParameterName = "AmendmentProjectId", Value = AmendmentProjectId };
                var stratRowNumberParameter = new SqlParameter { ParameterName = "StratRowNumber", Value = start };
                var endRowNumberParameter = new SqlParameter { ParameterName = "EndRowNumber", Value = limit };
                var totalRecordCountParameter = new SqlParameter { ParameterName = "TotalRecordCount", Value = totalRecordCount, Direction = System.Data.ParameterDirection.Output };
                try
                {
                    projectViewModel = db.Database.SqlQuery<AmendmentProjectSampleViewModel>("GLS_GetAllAmendmentProjectSampleForQC @StratRowNumber,@EndRowNumber, @TotalRecordCount OUTPUT", stratRowNumberParameter, endRowNumberParameter, totalRecordCountParameter).ToList<AmendmentProjectSampleViewModel>();
                    if (totalRecordCountParameter.Value != null)
                        totalRecordCount = Convert.ToInt16(totalRecordCountParameter.Value);
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<AmendmentProjectSampleViewModel>();
                }
                return projectViewModel;
            }
        }

        public List<AmendmentProjectSampleViewModel> GetAllAmendmentProjectSampleByProjectType(string projectType)
        {
            List<AmendmentProjectSampleViewModel> projectViewModel = new List<AmendmentProjectSampleViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var projectTypeParameter = new SqlParameter { ParameterName = "ProjectType", Value = projectType };
                try
                {
                    projectViewModel = db.Database.SqlQuery<AmendmentProjectSampleViewModel>("GLS_GetAllAmendmentProjectSampleByProjectType @ProjectType", projectTypeParameter).ToList<AmendmentProjectSampleViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<AmendmentProjectSampleViewModel>();
                }
                return projectViewModel;
            }
        }
        #endregion
    }
}
