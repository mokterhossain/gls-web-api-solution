using GLSPM.Data;
using GLSPM.Data.Modules.BasicModule;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Service.Modules.BasicModule
{
    public class AsbestosPercentageDetailService
    {
        #region Common Methods

        public List<AsbestosPercentageDetail> All()
        {
            List<AsbestosPercentageDetail> AsbestosPercentageDetail = new List<AsbestosPercentageDetail>();

            using (GLSPMContext db = new GLSPMContext())
            {
                AsbestosPercentageDetail = db.AsbestosPercentageDetails.OrderBy(f => f.Id).AsParallel().ToList();

                if (AsbestosPercentageDetail == null)
                {
                    AsbestosPercentageDetail = new List<AsbestosPercentageDetail>();
                }
                return AsbestosPercentageDetail;
            }
        }

        public AsbestosPercentageDetail GetByID(int Id)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AsbestosPercentageDetail AsbestosPercentageDetail = db.AsbestosPercentageDetails.SingleOrDefault(u => u.Id == Id);
                return AsbestosPercentageDetail;
            }
        }
        public List<AsbestosPercentageDetail> GetByAsbestosPercentageID(int percentageId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                List<AsbestosPercentageDetail> AsbestosPercentageDetail = db.AsbestosPercentageDetails.Where(u => u.AsbestosPercentageId == percentageId).ToList();
                return AsbestosPercentageDetail;
            }
        }
        //public AsbestosPercentageDetail GetByName(string name)
        //{
        //    using (GLSPMContext db = new GLSPMContext())
        //    {
        //        AsbestosPercentageDetail AsbestosPercentageDetail = db.AsbestosPercentageDetails.SingleOrDefault(u => u.Name == name);
        //        return AsbestosPercentageDetail;
        //    }
        //}
        public Response<AsbestosPercentageDetail> Add(AsbestosPercentageDetail AsbestosPercentageDetail)
        {
            Response<AsbestosPercentageDetail> addResponse = new Response<AsbestosPercentageDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.AsbestosPercentageDetails.Add(AsbestosPercentageDetail);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = AsbestosPercentageDetail;
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
                    addResponse.Result = AsbestosPercentageDetail;
                }
                return addResponse;
            }
        }

        public Response<AsbestosPercentageDetail> Update(AsbestosPercentageDetail AsbestosPercentageDetail)
        {
            Response<AsbestosPercentageDetail> updateResponse = new Response<AsbestosPercentageDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    AsbestosPercentageDetail updateAsbestosPercentageDetail = db.AsbestosPercentageDetails.SingleOrDefault(u => u.Id == AsbestosPercentageDetail.Id);
                    if (updateAsbestosPercentageDetail != null)
                    {
                        //updateAsbestosPercentageDetail = userRole;
                        updateAsbestosPercentageDetail.Id = AsbestosPercentageDetail.Id;
                        updateAsbestosPercentageDetail.AsbestosPercentageId = AsbestosPercentageDetail.AsbestosPercentageId;
                        updateAsbestosPercentageDetail.Value = AsbestosPercentageDetail.Value;
                        updateAsbestosPercentageDetail.FiberId = AsbestosPercentageDetail.FiberId;

                        updateAsbestosPercentageDetail.fiber_morphology = AsbestosPercentageDetail.fiber_morphology;
                        updateAsbestosPercentageDetail.color = AsbestosPercentageDetail.color;
                        updateAsbestosPercentageDetail.pleo = AsbestosPercentageDetail.pleo;
                        updateAsbestosPercentageDetail.nd_t_corr = AsbestosPercentageDetail.nd_t_corr;
                        updateAsbestosPercentageDetail.bifring = AsbestosPercentageDetail.bifring;
                        updateAsbestosPercentageDetail.extinction = AsbestosPercentageDetail.extinction;
                        updateAsbestosPercentageDetail.elong = AsbestosPercentageDetail.elong;
                        updateAsbestosPercentageDetail.ds_color = AsbestosPercentageDetail.ds_color;
                        updateAsbestosPercentageDetail.ri_liquid = AsbestosPercentageDetail.ri_liquid;

                        updateAsbestosPercentageDetail.UpdatedOn = DateTime.Now;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = AsbestosPercentageDetail;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = AsbestosPercentageDetail;
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
                    updateResponse.Result = AsbestosPercentageDetail;
                }
                return updateResponse;
            }
        }

        public Response<AsbestosPercentageDetail> Delete(int Id)
        {
            Response<AsbestosPercentageDetail> deleteResponse = new Response<AsbestosPercentageDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    AsbestosPercentageDetail deleteAsbestosPercentageDetail = db.AsbestosPercentageDetails.SingleOrDefault(u => u.Id == Id);
                    if (deleteAsbestosPercentageDetail != null)
                    {
                        db.AsbestosPercentageDetails.Remove(deleteAsbestosPercentageDetail);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteAsbestosPercentageDetail;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteAsbestosPercentageDetail;
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
