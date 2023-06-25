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
    public class MoldSampleDetailService
    {
        #region Common Methods

        public List<MoldSampleDetail> All()
        {
            List<MoldSampleDetail> MoldSampleDetail = new List<MoldSampleDetail>();

            using (GLSPMContext db = new GLSPMContext())
            {
                MoldSampleDetail = db.MoldSampleDetails.OrderBy(f => f.Id).AsParallel().ToList();

                if (MoldSampleDetail == null)
                {
                    MoldSampleDetail = new List<MoldSampleDetail>();
                }
                return MoldSampleDetail;
            }
        }

        public MoldSampleDetail GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                MoldSampleDetail MoldSampleDetail = db.MoldSampleDetails.SingleOrDefault(u => u.Id == userRoleId);
                return MoldSampleDetail;
            }
        }
        public MoldSampleDetail GetByMoldSampleIDAndSporeType(long moldSampleId, int sporeTypeId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                MoldSampleDetail MoldSampleDetail = db.MoldSampleDetails.SingleOrDefault(u => u.MoldSampleId == moldSampleId && u.SporeTypeId == sporeTypeId);
                return MoldSampleDetail;
            }
        }
        public Response<MoldSampleDetail> Add(MoldSampleDetail MoldSampleDetail)
        {
            Response<MoldSampleDetail> addResponse = new Response<MoldSampleDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.MoldSampleDetails.Add(MoldSampleDetail);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = MoldSampleDetail;
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
                    addResponse.Result = MoldSampleDetail;
                }
                return addResponse;
            }
        }

        public Response<MoldSampleDetail> Update(MoldSampleDetail MoldSampleDetail)
        {
            Response<MoldSampleDetail> updateResponse = new Response<MoldSampleDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    MoldSampleDetail updateMoldSampleDetail = db.MoldSampleDetails.SingleOrDefault(u => u.Id == MoldSampleDetail.Id);
                    if (updateMoldSampleDetail != null)
                    {
                        //updateMoldSampleDetail = userRole;
                        updateMoldSampleDetail.Id = MoldSampleDetail.Id;
                        updateMoldSampleDetail.MoldSampleId = MoldSampleDetail.MoldSampleId;
                        updateMoldSampleDetail.SporeTypeId = MoldSampleDetail.SporeTypeId;
                        updateMoldSampleDetail.RawCt = MoldSampleDetail.RawCt;
                        updateMoldSampleDetail.Permm = MoldSampleDetail.Permm;
                        updateMoldSampleDetail.RawCtStringValue = MoldSampleDetail.RawCtStringValue;
                        updateMoldSampleDetail.PermmStringValue = MoldSampleDetail.PermmStringValue;

                        updateMoldSampleDetail.Volume = MoldSampleDetail.Volume;
                        updateMoldSampleDetail.MicroscopeFieldDiameter = MoldSampleDetail.MicroscopeFieldDiameter;
                        updateMoldSampleDetail.TraverseNumber = MoldSampleDetail.TraverseNumber;
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = MoldSampleDetail;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = MoldSampleDetail;
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
                    updateResponse.Result = MoldSampleDetail;
                }
                return updateResponse;
            }
        }

        public Response<MoldSampleDetail> Delete(long Id)
        {
            Response<MoldSampleDetail> deleteResponse = new Response<MoldSampleDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    MoldSampleDetail deleteMoldSampleDetail = db.MoldSampleDetails.SingleOrDefault(u => u.Id == Id);
                    if (deleteMoldSampleDetail != null)
                    {
                        db.MoldSampleDetails.Remove(deleteMoldSampleDetail);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteMoldSampleDetail;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteMoldSampleDetail;
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
        public List<MoldSampleDetailViewModel> GetMoldSampleDetail(long projectId)
        {
            List<MoldSampleDetailViewModel> projectViewModel = new List<MoldSampleDetailViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var projectIdParameter = new SqlParameter { ParameterName = "ProjectId", Value = projectId };
                try
                {
                    projectViewModel = db.Database.SqlQuery<MoldSampleDetailViewModel>("GLS_GetMoldSampleDetail @ProjectId", projectIdParameter).ToList<MoldSampleDetailViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<MoldSampleDetailViewModel>();
                }
                return projectViewModel;
            }
        }
        public List<MoldSampleDetail> GetMoldSampleDetailBySample(long sampleId)
        {
            List<MoldSampleDetail> projectViewModel = new List<MoldSampleDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var sampleIdParameter = new SqlParameter { ParameterName = "SampleId", Value = sampleId };
                try
                {
                    projectViewModel = db.Database.SqlQuery<MoldSampleDetail>("GLS_GetMoldSampleDetailBySample @SampleId", sampleIdParameter).ToList<MoldSampleDetail>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<MoldSampleDetail>();
                }
                return projectViewModel;
            }
        }
        #endregion
    }
}
