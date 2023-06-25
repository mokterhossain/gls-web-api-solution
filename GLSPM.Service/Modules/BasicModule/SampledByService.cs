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
    public class SampledByService
    {
        #region Common Methods

        public List<SampledBy> All()
        {
            List<SampledBy> SampledBy = new List<SampledBy>();

            using (GLSPMContext db = new GLSPMContext())
            {
                SampledBy = db.SampledBys.OrderBy(f => f.Id).AsParallel().ToList();

                if (SampledBy == null)
                {
                    SampledBy = new List<SampledBy>();
                }
                return SampledBy;
            }
        }

        public SampledBy GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                SampledBy SampledBy = db.SampledBys.SingleOrDefault(u => u.Id == userRoleId);
                return SampledBy;
            }
        }
        public Response<SampledBy> Add(SampledBy SampledBy)
        {
            Response<SampledBy> addResponse = new Response<SampledBy>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.SampledBys.Add(SampledBy);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = SampledBy;
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
                    addResponse.Result = SampledBy;
                }
                return addResponse;
            }
        }

        public Response<SampledBy> Update(SampledBy SampledBy)
        {
            Response<SampledBy> updateResponse = new Response<SampledBy>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    SampledBy updateSampledBy = db.SampledBys.SingleOrDefault(u => u.Id == SampledBy.Id);
                    if (updateSampledBy != null)
                    {
                        //updateSampledBy = userRole;
                        updateSampledBy.Id = SampledBy.Id;
                        updateSampledBy.Title = SampledBy.Title;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = SampledBy;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = SampledBy;
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
                    updateResponse.Result = SampledBy;
                }
                return updateResponse;
            }
        }

        public Response<SampledBy> Delete(long Id)
        {
            Response<SampledBy> deleteResponse = new Response<SampledBy>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    SampledBy deleteSampledBy = db.SampledBys.SingleOrDefault(u => u.Id == Id);
                    if (deleteSampledBy != null)
                    {
                        db.SampledBys.Remove(deleteSampledBy);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteSampledBy;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteSampledBy;
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
