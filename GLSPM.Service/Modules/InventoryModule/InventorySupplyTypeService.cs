using GLSPM.Data;
using GLSPM.Data.Modules.InventoryModule;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Service.Modules.InventoryModule
{
    public class InventorySupplyTypeService
    {
        #region Common Methods

        public List<InventorySupplyType> All()
        {
            List<InventorySupplyType> InventorySupplyType = new List<InventorySupplyType>();

            using (GLSPMContext db = new GLSPMContext())
            {
                InventorySupplyType = db.InventorySupplyTypes.OrderBy(f => f.Id).AsParallel().ToList();

                if (InventorySupplyType == null)
                {
                    InventorySupplyType = new List<InventorySupplyType>();
                }
                return InventorySupplyType;
            }
        }
        public List<InventorySupplyType> AllByCategory(string category)
        {
            List<InventorySupplyType> InventorySupplyType = new List<InventorySupplyType>();

            using (GLSPMContext db = new GLSPMContext())
            {
                InventorySupplyType = db.InventorySupplyTypes.OrderBy(f => f.Id).Where(s=>s.SupplyCategory == category).AsParallel().ToList();

                if (InventorySupplyType == null)
                {
                    InventorySupplyType = new List<InventorySupplyType>();
                }
                return InventorySupplyType;
            }
        }
        public InventorySupplyType GetByID(int userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                InventorySupplyType InventorySupplyType = db.InventorySupplyTypes.SingleOrDefault(u => u.Id == userRoleId);
                return InventorySupplyType;
            }
        }
        public Response<InventorySupplyType> Add(InventorySupplyType InventorySupplyType)
        {
            Response<InventorySupplyType> addResponse = new Response<InventorySupplyType>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.InventorySupplyTypes.Add(InventorySupplyType);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = InventorySupplyType;
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
                    addResponse.Result = InventorySupplyType;
                }
                return addResponse;
            }
        }

        public Response<InventorySupplyType> Update(InventorySupplyType InventorySupplyType)
        {
            Response<InventorySupplyType> updateResponse = new Response<InventorySupplyType>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    InventorySupplyType updateInventorySupplyType = db.InventorySupplyTypes.SingleOrDefault(u => u.Id == InventorySupplyType.Id);
                    if (updateInventorySupplyType != null)
                    {
                        //updateInventorySupplyType = userRole;
                        updateInventorySupplyType.Id = InventorySupplyType.Id;
                        updateInventorySupplyType.SupplyCategory = InventorySupplyType.SupplyCategory;
                        updateInventorySupplyType.Name = InventorySupplyType.Name;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = InventorySupplyType;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = InventorySupplyType;
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
                    updateResponse.Result = InventorySupplyType;
                }
                return updateResponse;
            }
        }

        public Response<InventorySupplyType> Delete(long Id)
        {
            Response<InventorySupplyType> deleteResponse = new Response<InventorySupplyType>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    InventorySupplyType deleteInventorySupplyType = db.InventorySupplyTypes.SingleOrDefault(u => u.Id == Id);
                    if (deleteInventorySupplyType != null)
                    {
                        db.InventorySupplyTypes.Remove(deleteInventorySupplyType);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteInventorySupplyType;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteInventorySupplyType;
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
