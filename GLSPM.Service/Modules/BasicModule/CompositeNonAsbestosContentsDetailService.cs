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
    public class CompositeNonAsbestosContentsDetailService
    {
        #region Common Methods

        public List<CompositeNonAsbestosContentsDetail> All()
        {
            List<CompositeNonAsbestosContentsDetail> CompositeNonAsbestosContentsDetail = new List<CompositeNonAsbestosContentsDetail>();

            using (GLSPMContext db = new GLSPMContext())
            {
                CompositeNonAsbestosContentsDetail = db.CompositeNonAsbestosContentsDetails.OrderBy(f => f.Id).AsParallel().ToList();

                if (CompositeNonAsbestosContentsDetail == null)
                {
                    CompositeNonAsbestosContentsDetail = new List<CompositeNonAsbestosContentsDetail>();
                }
                return CompositeNonAsbestosContentsDetail;
            }
        }

        public CompositeNonAsbestosContentsDetail GetByID(int Id)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                CompositeNonAsbestosContentsDetail CompositeNonAsbestosContentsDetail = db.CompositeNonAsbestosContentsDetails.SingleOrDefault(u => u.Id == Id);
                return CompositeNonAsbestosContentsDetail;
            }
        }
        public List<CompositeNonAsbestosContentsDetail> GetByAsbestosPercentageID(int percentageId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                List<CompositeNonAsbestosContentsDetail> CompositeNonAsbestosContentsDetail = db.CompositeNonAsbestosContentsDetails.Where(u => u.CompositeNonAsbestosContentsId == percentageId).ToList();
                return CompositeNonAsbestosContentsDetail;
            }
        }
        //public CompositeNonAsbestosContentsDetail GetByName(string name)
        //{
        //    using (GLSPMContext db = new GLSPMContext())
        //    {
        //        CompositeNonAsbestosContentsDetail CompositeNonAsbestosContentsDetail = db.CompositeNonAsbestosContentsDetails.SingleOrDefault(u => u.Name == name);
        //        return CompositeNonAsbestosContentsDetail;
        //    }
        //}
        public Response<CompositeNonAsbestosContentsDetail> Add(CompositeNonAsbestosContentsDetail CompositeNonAsbestosContentsDetail)
        {
            Response<CompositeNonAsbestosContentsDetail> addResponse = new Response<CompositeNonAsbestosContentsDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.CompositeNonAsbestosContentsDetails.Add(CompositeNonAsbestosContentsDetail);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = CompositeNonAsbestosContentsDetail;
                    }
                }
                catch (Exception ex)
                {
                    int errorNo = ((SqlException)ex.InnerException.InnerException).Number;
                    if (errorNo == 2601 || errorNo == 2627)
                    {
                        addResponse.Message = "This  Composite Non Asbestos Contents Detail already exist.";
                    }
                    else
                    {
                        addResponse.Message = "There was an error while adding the  Composite Non Asbestos Contents Detail: " + ex.Message;
                    }
                    addResponse.IsSuccess = false;
                    addResponse.Result = CompositeNonAsbestosContentsDetail;
                }
                return addResponse;
            }
        }

        public Response<CompositeNonAsbestosContentsDetail> Update(CompositeNonAsbestosContentsDetail CompositeNonAsbestosContentsDetail)
        {
            Response<CompositeNonAsbestosContentsDetail> updateResponse = new Response<CompositeNonAsbestosContentsDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    CompositeNonAsbestosContentsDetail updateCompositeNonAsbestosContentsDetail = db.CompositeNonAsbestosContentsDetails.SingleOrDefault(u => u.Id == CompositeNonAsbestosContentsDetail.Id);
                    if (updateCompositeNonAsbestosContentsDetail != null)
                    {
                        //updateCompositeNonAsbestosContentsDetail = userRole;
                        updateCompositeNonAsbestosContentsDetail.Id = CompositeNonAsbestosContentsDetail.Id;
                        updateCompositeNonAsbestosContentsDetail.CompositeNonAsbestosContentsId = CompositeNonAsbestosContentsDetail.CompositeNonAsbestosContentsId;
                        updateCompositeNonAsbestosContentsDetail.Value = CompositeNonAsbestosContentsDetail.Value;
                        updateCompositeNonAsbestosContentsDetail.FiberId = CompositeNonAsbestosContentsDetail.FiberId;

                        updateCompositeNonAsbestosContentsDetail.fiber_morphology = CompositeNonAsbestosContentsDetail.fiber_morphology;
                        updateCompositeNonAsbestosContentsDetail.color = CompositeNonAsbestosContentsDetail.color;
                        updateCompositeNonAsbestosContentsDetail.pleo = CompositeNonAsbestosContentsDetail.pleo;
                        updateCompositeNonAsbestosContentsDetail.nd_t_corr = CompositeNonAsbestosContentsDetail.nd_t_corr;
                        updateCompositeNonAsbestosContentsDetail.bifring = CompositeNonAsbestosContentsDetail.bifring;
                        updateCompositeNonAsbestosContentsDetail.extinction = CompositeNonAsbestosContentsDetail.extinction;
                        updateCompositeNonAsbestosContentsDetail.elong = CompositeNonAsbestosContentsDetail.elong;
                        updateCompositeNonAsbestosContentsDetail.ds_color = CompositeNonAsbestosContentsDetail.ds_color;
                        updateCompositeNonAsbestosContentsDetail.ri_liquid = CompositeNonAsbestosContentsDetail.ri_liquid;

                        updateCompositeNonAsbestosContentsDetail.UpdatedOn = DateTime.Now;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = CompositeNonAsbestosContentsDetail;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = " Composite Non Asbestos Contents Detail updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = CompositeNonAsbestosContentsDetail;
                            updateResponse.Message = "Error on update";
                        }
                    }
                    else
                    {
                        updateResponse.Result = null;
                        updateResponse.Message = " Composite Non Asbestos Contents Detail not exist";
                    }
                }
                catch (Exception ex)
                {
                    int errorNo = ((SqlException)ex.InnerException.InnerException).Number;
                    if (errorNo == 2601 || errorNo == 2627)
                    {
                        updateResponse.Message = "This  Composite Non Asbestos Contents Detail already exist.";
                    }
                    else
                    {
                        updateResponse.Message = "There was an error while adding the  Composite Non Asbestos Contents Detail: " + ex.Message;
                    }
                    updateResponse.Result = CompositeNonAsbestosContentsDetail;
                }
                return updateResponse;
            }
        }

        public Response<CompositeNonAsbestosContentsDetail> Delete(int Id)
        {
            Response<CompositeNonAsbestosContentsDetail> deleteResponse = new Response<CompositeNonAsbestosContentsDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    CompositeNonAsbestosContentsDetail deleteCompositeNonAsbestosContentsDetail = db.CompositeNonAsbestosContentsDetails.SingleOrDefault(u => u.Id == Id);
                    if (deleteCompositeNonAsbestosContentsDetail != null)
                    {
                        db.CompositeNonAsbestosContentsDetails.Remove(deleteCompositeNonAsbestosContentsDetail);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteCompositeNonAsbestosContentsDetail;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = " Composite Non Asbestos Contents Detail deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteCompositeNonAsbestosContentsDetail;
                            deleteResponse.Message = "Error on delete";
                        }
                    }
                    else
                    {
                        deleteResponse.Result = null;
                        deleteResponse.Message = " Composite Non Asbestos Contents Detail not exist";
                    }
                }
                catch (Exception ex)
                {
                    int errorNo = ((SqlException)ex.InnerException.InnerException).Number;

                    if (errorNo == 547)
                    {
                        deleteResponse.Message = "This  Composite Non Asbestos Contents Detail currently Used.";
                    }
                    else
                    {
                        deleteResponse.Message = "There was an error while deleting  Composite Non Asbestos Contents Detail: " + ex.Message;
                    }
                    deleteResponse.Result = null;
                }
                return deleteResponse;
            }
        }

        #endregion Common Methods
    }
}
