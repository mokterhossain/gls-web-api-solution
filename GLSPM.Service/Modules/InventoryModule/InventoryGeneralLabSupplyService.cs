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
    public class InventoryGeneralLabSupplyService
    {
        #region Common Methods

        public List<InventoryGeneralLabSupply> All()
        {
            List<InventoryGeneralLabSupply> InventoryGeneralLabSupply = new List<InventoryGeneralLabSupply>();

            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryGeneralLabSupply = db.InventoryGeneralLabSupplys.OrderBy(f => f.Id).AsParallel().ToList();

                if (InventoryGeneralLabSupply == null)
                {
                    InventoryGeneralLabSupply = new List<InventoryGeneralLabSupply>();
                }
                return InventoryGeneralLabSupply;
            }
        }

        public InventoryGeneralLabSupply GetByID(int userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryGeneralLabSupply InventoryGeneralLabSupply = db.InventoryGeneralLabSupplys.SingleOrDefault(u => u.Id == userRoleId);
                return InventoryGeneralLabSupply;
            }
        }
        public Response<InventoryGeneralLabSupply> Add(InventoryGeneralLabSupply InventoryGeneralLabSupply)
        {
            Response<InventoryGeneralLabSupply> addResponse = new Response<InventoryGeneralLabSupply>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.InventoryGeneralLabSupplys.Add(InventoryGeneralLabSupply);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = InventoryGeneralLabSupply;
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
                    addResponse.Result = InventoryGeneralLabSupply;
                }
                return addResponse;
            }
        }

        public Response<InventoryGeneralLabSupply> Update(InventoryGeneralLabSupply InventoryGeneralLabSupply)
        {
            Response<InventoryGeneralLabSupply> updateResponse = new Response<InventoryGeneralLabSupply>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    InventoryGeneralLabSupply updateInventoryGeneralLabSupply = db.InventoryGeneralLabSupplys.SingleOrDefault(u => u.Id == InventoryGeneralLabSupply.Id);
                    if (updateInventoryGeneralLabSupply != null)
                    {
                        //updateInventoryGeneralLabSupply = userRole;
                        updateInventoryGeneralLabSupply.Id = InventoryGeneralLabSupply.Id;
                        updateInventoryGeneralLabSupply.Description = InventoryGeneralLabSupply.Description;
                        updateInventoryGeneralLabSupply.DateOfPurchase = InventoryGeneralLabSupply.DateOfPurchase;
                        updateInventoryGeneralLabSupply.DateOfPurchase = InventoryGeneralLabSupply.DateOfPurchase;
                        updateInventoryGeneralLabSupply.Vendor = InventoryGeneralLabSupply.Vendor;
                        updateInventoryGeneralLabSupply.Count = InventoryGeneralLabSupply.Count;
                        updateInventoryGeneralLabSupply.Cost = InventoryGeneralLabSupply.Cost;
                        updateInventoryGeneralLabSupply.Finished = InventoryGeneralLabSupply.Finished;
                        updateInventoryGeneralLabSupply.SupplyType = InventoryGeneralLabSupply.SupplyType;
                        updateInventoryGeneralLabSupply.SupplyTypeId = InventoryGeneralLabSupply.SupplyTypeId;
                        updateInventoryGeneralLabSupply.UpdatedOn = InventoryGeneralLabSupply.UpdatedOn;
                        updateInventoryGeneralLabSupply.UpdatedBy = InventoryGeneralLabSupply.UpdatedBy;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = InventoryGeneralLabSupply;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = InventoryGeneralLabSupply;
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
                    updateResponse.Result = InventoryGeneralLabSupply;
                }
                return updateResponse;
            }
        }

        public Response<InventoryGeneralLabSupply> Delete(long Id)
        {
            Response<InventoryGeneralLabSupply> deleteResponse = new Response<InventoryGeneralLabSupply>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    InventoryGeneralLabSupply deleteInventoryGeneralLabSupply = db.InventoryGeneralLabSupplys.SingleOrDefault(u => u.Id == Id);
                    if (deleteInventoryGeneralLabSupply != null)
                    {
                        db.InventoryGeneralLabSupplys.Remove(deleteInventoryGeneralLabSupply);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteInventoryGeneralLabSupply;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteInventoryGeneralLabSupply;
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
