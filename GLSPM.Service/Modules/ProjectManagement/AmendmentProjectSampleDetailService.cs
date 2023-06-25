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
    public class AmendmentProjectSampleDetailService
    {
        #region Common Methods

        public List<AmendmentProjectSampleDetail> All()
        {
            List<AmendmentProjectSampleDetail> AmendmentProjectSampleDetail = new List<AmendmentProjectSampleDetail>();

            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentProjectSampleDetail = db.AmendmentProjectSampleDetails.OrderBy(f => f.Id).AsParallel().ToList();

                if (AmendmentProjectSampleDetail == null)
                {
                    AmendmentProjectSampleDetail = new List<AmendmentProjectSampleDetail>();
                }
                return AmendmentProjectSampleDetail;
            }
        }

        public AmendmentProjectSampleDetail GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentProjectSampleDetail AmendmentProjectSampleDetail = db.AmendmentProjectSampleDetails.SingleOrDefault(u => u.SampleDetailId == userRoleId);
                return AmendmentProjectSampleDetail;
            }
        }
        public AmendmentProjectSampleDetail GetBySampleIDAndAmendmentNumber(long sampleId, string amendmentNumber)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentProjectSampleDetail AmendmentProjectSampleDetail = db.AmendmentProjectSampleDetails.SingleOrDefault(u => u.Id == sampleId && u.Amendment == amendmentNumber);
                return AmendmentProjectSampleDetail;
            }
        }
        public List<AmendmentProjectSampleDetail> GetByAmendmentProjectSampleId(long AmendmentProjectSampleId)
        {
            List<AmendmentProjectSampleDetail> AmendmentProjectSampleDetail = new List<AmendmentProjectSampleDetail>();

            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentProjectSampleDetail = db.AmendmentProjectSampleDetails.OrderBy(f => f.Id).Where(p => p.ProjectSampleId == AmendmentProjectSampleId).AsParallel().ToList();

                if (AmendmentProjectSampleDetail == null)
                {
                    AmendmentProjectSampleDetail = new List<AmendmentProjectSampleDetail>();
                }
                return AmendmentProjectSampleDetail;
            }
        }
        public Response<AmendmentProjectSampleDetail> Add(AmendmentProjectSampleDetail AmendmentProjectSampleDetail)
        {
            Response<AmendmentProjectSampleDetail> addResponse = new Response<AmendmentProjectSampleDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.AmendmentProjectSampleDetails.Add(AmendmentProjectSampleDetail);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = AmendmentProjectSampleDetail;
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
                    addResponse.Result = AmendmentProjectSampleDetail;
                }
                return addResponse;
            }
        }

        public Response<AmendmentProjectSampleDetail> Update(AmendmentProjectSampleDetail AmendmentProjectSampleDetail)
        {
            Response<AmendmentProjectSampleDetail> updateResponse = new Response<AmendmentProjectSampleDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    AmendmentProjectSampleDetail updateProjectSample = db.AmendmentProjectSampleDetails.SingleOrDefault(u => u.SampleDetailId == AmendmentProjectSampleDetail.SampleDetailId);
                    if (updateProjectSample != null)
                    {
                        //updateProjectSample = userRole;
                        updateProjectSample.SampleDetailId = AmendmentProjectSampleDetail.SampleDetailId;
                        updateProjectSample.Id = AmendmentProjectSampleDetail.Id;
                        updateProjectSample.ProjectSampleId = AmendmentProjectSampleDetail.ProjectSampleId;
                        updateProjectSample.SampleTypeId = AmendmentProjectSampleDetail.SampleTypeId;
                        updateProjectSample.SampleType = AmendmentProjectSampleDetail.SampleType;
                        updateProjectSample.Homogeneity = AmendmentProjectSampleDetail.Homogeneity;
                        updateProjectSample.SampleContent = AmendmentProjectSampleDetail.SampleContent;
                        updateProjectSample.AbsestosPercentage = AmendmentProjectSampleDetail.AbsestosPercentage;
                        updateProjectSample.AbsestosPercentageText = AmendmentProjectSampleDetail.AbsestosPercentageText;
                        updateProjectSample.CompositeNonAsbestosContents = AmendmentProjectSampleDetail.CompositeNonAsbestosContents;
                        updateProjectSample.CompositeNonAsbestosContentsText = AmendmentProjectSampleDetail.CompositeNonAsbestosContentsText;
                        updateProjectSample.DisplayOrder = AmendmentProjectSampleDetail.DisplayOrder;
                        updateProjectSample.IsBilable = AmendmentProjectSampleDetail.IsBilable;
                        updateProjectSample.CreatedOn = AmendmentProjectSampleDetail.CreatedOn;
                        updateProjectSample.CreatedBy = AmendmentProjectSampleDetail.CreatedBy;
                        updateProjectSample.UpdatedOn = AmendmentProjectSampleDetail.UpdatedOn;
                        updateProjectSample.UpdatedBy = AmendmentProjectSampleDetail.UpdatedBy;
                        updateProjectSample.Amendment = AmendmentProjectSampleDetail.Amendment;
                        updateProjectSample.IsAmendment = AmendmentProjectSampleDetail.IsAmendment;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = AmendmentProjectSampleDetail;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = AmendmentProjectSampleDetail;
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
                    updateResponse.Result = AmendmentProjectSampleDetail;
                }
                return updateResponse;
            }
        }

        public Response<AmendmentProjectSampleDetail> Delete(long Id)
        {
            Response<AmendmentProjectSampleDetail> deleteResponse = new Response<AmendmentProjectSampleDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    AmendmentProjectSampleDetail deleteProjectSample = db.AmendmentProjectSampleDetails.SingleOrDefault(u => u.Id == Id);
                    if (deleteProjectSample != null)
                    {
                        db.AmendmentProjectSampleDetails.Remove(deleteProjectSample);

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
        public List<AmendmentProjectSampleDetailViewModel> GetAllAmendmentProjectSampleDetailsBySampleId(long sampleId, string amendmentNumber)
        {
            List<AmendmentProjectSampleDetailViewModel> projectViewModel = new List<AmendmentProjectSampleDetailViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var sampleIdParameter = new SqlParameter { ParameterName = "SampleId", Value = sampleId };
                var amendmentNumberParameter = new SqlParameter { ParameterName = "AmendmentNumber", Value = amendmentNumber };
                try
                {
                    projectViewModel = db.Database.SqlQuery<AmendmentProjectSampleDetailViewModel>("GLS_GetAllAmendmentProjectSampleDetailsBySampleId @SampleId,@AmendmentNumber", sampleIdParameter, amendmentNumberParameter).ToList<AmendmentProjectSampleDetailViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<AmendmentProjectSampleDetailViewModel>();
                }
                return projectViewModel;
            }
        }

        #endregion
    }
}
