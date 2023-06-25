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
    public class AsbestosPercentageService
    {
        #region Common Methods

        public List<AsbestosPercentage> All()
        {
            List<AsbestosPercentage> AsbestosPercentage = new List<AsbestosPercentage>();

            using (GLSPMContext db = new GLSPMContext())
            {
                AsbestosPercentage = db.AsbestosPercentages.OrderBy(f => f.Id).AsParallel().ToList();

                if (AsbestosPercentage == null)
                {
                    AsbestosPercentage = new List<AsbestosPercentage>();
                }

                return AsbestosPercentage;
            }
        }

        public AsbestosPercentage GetByID(int percentageId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AsbestosPercentage AsbestosPercentage = db.AsbestosPercentages.SingleOrDefault(u => u.Id == percentageId);
                return AsbestosPercentage;
            }
        }
        public AsbestosPercentage GetByIDWithDetails(int percentageId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AsbestosPercentage AsbestosPercentage = db.AsbestosPercentages.SingleOrDefault(u => u.Id == percentageId);
                AsbestosPercentage.AsbestosPercentageDetail = new AsbestosPercentageDetailService().GetByAsbestosPercentageID(percentageId);
                return AsbestosPercentage;
            }
        }
        public AsbestosPercentage GetByName(string name)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AsbestosPercentage AsbestosPercentage = db.AsbestosPercentages.SingleOrDefault(u => u.Name == name);
                return AsbestosPercentage;
            }
        }
        public Response<AsbestosPercentage> Add(AsbestosPercentage AsbestosPercentage)
        {
            Response<AsbestosPercentage> addResponse = new Response<AsbestosPercentage>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.AsbestosPercentages.Add(AsbestosPercentage);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = AsbestosPercentage;
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
                    addResponse.Result = AsbestosPercentage;
                }
                return addResponse;
            }
        }

        public Response<AsbestosPercentage> Update(AsbestosPercentage AsbestosPercentage)
        {
            Response<AsbestosPercentage> updateResponse = new Response<AsbestosPercentage>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    AsbestosPercentage updateAsbestosPercentage = db.AsbestosPercentages.SingleOrDefault(u => u.Id == AsbestosPercentage.Id);
                    if (updateAsbestosPercentage != null)
                    {
                        //updateAsbestosPercentage = userRole;
                        updateAsbestosPercentage.Id = AsbestosPercentage.Id;
                        updateAsbestosPercentage.Name = AsbestosPercentage.Name;
                        updateAsbestosPercentage.NumericValue = AsbestosPercentage.NumericValue;
                        updateAsbestosPercentage.UpdatedOn = DateTime.Now;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = AsbestosPercentage;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = AsbestosPercentage;
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
                    updateResponse.Result = AsbestosPercentage;
                }
                return updateResponse;
            }
        }

        public Response<AsbestosPercentage> Delete(int Id)
        {
            Response<AsbestosPercentage> deleteResponse = new Response<AsbestosPercentage>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    AsbestosPercentage deleteAsbestosPercentage = db.AsbestosPercentages.SingleOrDefault(u => u.Id == Id);
                    if (deleteAsbestosPercentage != null)
                    {
                        db.AsbestosPercentages.Remove(deleteAsbestosPercentage);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteAsbestosPercentage;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteAsbestosPercentage;
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
