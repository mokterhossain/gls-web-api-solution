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
    public class MoldTapeLiftSampleDetailService
    {
        #region Common Methods

        public List<MoldTapeLiftSampleDetail> All()
        {
            List<MoldTapeLiftSampleDetail> MoldTapeLiftSampleDetail = new List<MoldTapeLiftSampleDetail>();

            using (GLSPMContext db = new GLSPMContext())
            {
                MoldTapeLiftSampleDetail = db.MoldTapeLiftSampleDetails.OrderBy(f => f.Id).AsParallel().ToList();

                if (MoldTapeLiftSampleDetail == null)
                {
                    MoldTapeLiftSampleDetail = new List<MoldTapeLiftSampleDetail>();
                }
                return MoldTapeLiftSampleDetail;
            }
        }

        public MoldTapeLiftSampleDetail GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                MoldTapeLiftSampleDetail MoldTapeLiftSampleDetail = db.MoldTapeLiftSampleDetails.SingleOrDefault(u => u.Id == userRoleId);
                return MoldTapeLiftSampleDetail;
            }
        }
        public MoldTapeLiftSampleDetail GetByMoldSampleIDAndSporeType(long moldSampleId, int sporeTypeId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                MoldTapeLiftSampleDetail MoldTapeLiftSampleDetail = db.MoldTapeLiftSampleDetails.SingleOrDefault(u => u.MoldSampleId == moldSampleId && u.SporeTypeId == sporeTypeId);
                return MoldTapeLiftSampleDetail;
            }
        }
        public Response<MoldTapeLiftSampleDetail> Add(MoldTapeLiftSampleDetail MoldTapeLiftSampleDetail)
        {
            Response<MoldTapeLiftSampleDetail> addResponse = new Response<MoldTapeLiftSampleDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.MoldTapeLiftSampleDetails.Add(MoldTapeLiftSampleDetail);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = MoldTapeLiftSampleDetail;
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
                    addResponse.Result = MoldTapeLiftSampleDetail;
                }
                return addResponse;
            }
        }

        public Response<MoldTapeLiftSampleDetail> Update(MoldTapeLiftSampleDetail MoldTapeLiftSampleDetail)
        {
            Response<MoldTapeLiftSampleDetail> updateResponse = new Response<MoldTapeLiftSampleDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    MoldTapeLiftSampleDetail updateMoldTapeLiftSampleDetail = db.MoldTapeLiftSampleDetails.SingleOrDefault(u => u.Id == MoldTapeLiftSampleDetail.Id);
                    if (updateMoldTapeLiftSampleDetail != null)
                    {
                        //updateMoldTapeLiftSampleDetail = userRole;
                        updateMoldTapeLiftSampleDetail.Id = MoldTapeLiftSampleDetail.Id;
                        updateMoldTapeLiftSampleDetail.MoldSampleId = MoldTapeLiftSampleDetail.MoldSampleId;
                        updateMoldTapeLiftSampleDetail.SporeTypeId = MoldTapeLiftSampleDetail.SporeTypeId;
                        updateMoldTapeLiftSampleDetail.RelativeMoldConc = MoldTapeLiftSampleDetail.RelativeMoldConc;
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = MoldTapeLiftSampleDetail;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = MoldTapeLiftSampleDetail;
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
                    updateResponse.Result = MoldTapeLiftSampleDetail;
                }
                return updateResponse;
            }
        }

        public Response<MoldTapeLiftSampleDetail> Delete(long Id)
        {
            Response<MoldTapeLiftSampleDetail> deleteResponse = new Response<MoldTapeLiftSampleDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    MoldTapeLiftSampleDetail deleteMoldTapeLiftSampleDetail = db.MoldTapeLiftSampleDetails.SingleOrDefault(u => u.Id == Id);
                    if (deleteMoldTapeLiftSampleDetail != null)
                    {
                        db.MoldTapeLiftSampleDetails.Remove(deleteMoldTapeLiftSampleDetail);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteMoldTapeLiftSampleDetail;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteMoldTapeLiftSampleDetail;
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
    }
}
