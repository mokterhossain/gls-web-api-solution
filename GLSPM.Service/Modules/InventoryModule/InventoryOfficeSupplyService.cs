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
    public class InventoryOfficeSupplyService
    {
        #region Common Methods

        public List<InventoryOfficeSupply> All()
        {
            List<InventoryOfficeSupply> InventoryOfficeSupply = new List<InventoryOfficeSupply>();

            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryOfficeSupply = db.InventoryOfficeSupplys.OrderBy(f => f.Id).AsParallel().ToList();

                if (InventoryOfficeSupply == null)
                {
                    InventoryOfficeSupply = new List<InventoryOfficeSupply>();
                }
                return InventoryOfficeSupply;
            }
        }

        public InventoryOfficeSupply GetByID(int userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryOfficeSupply InventoryOfficeSupply = db.InventoryOfficeSupplys.SingleOrDefault(u => u.Id == userRoleId);
                return InventoryOfficeSupply;
            }
        }
        public Response<InventoryOfficeSupply> Add(InventoryOfficeSupply InventoryOfficeSupply)
        {
            Response<InventoryOfficeSupply> addResponse = new Response<InventoryOfficeSupply>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.InventoryOfficeSupplys.Add(InventoryOfficeSupply);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = InventoryOfficeSupply;
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
                    addResponse.Result = InventoryOfficeSupply;
                }
                return addResponse;
            }
        }

        public Response<InventoryOfficeSupply> Update(InventoryOfficeSupply InventoryOfficeSupply)
        {
            Response<InventoryOfficeSupply> updateResponse = new Response<InventoryOfficeSupply>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    InventoryOfficeSupply updateInventoryOfficeSupply = db.InventoryOfficeSupplys.SingleOrDefault(u => u.Id == InventoryOfficeSupply.Id);
                    if (updateInventoryOfficeSupply != null)
                    {
                        //updateInventoryOfficeSupply = userRole;
                        updateInventoryOfficeSupply.Id = InventoryOfficeSupply.Id;
                        updateInventoryOfficeSupply.Description = InventoryOfficeSupply.Description;
                        updateInventoryOfficeSupply.DateOfPurchase = InventoryOfficeSupply.DateOfPurchase;
                        updateInventoryOfficeSupply.DateOfPurchase = InventoryOfficeSupply.DateOfPurchase;
                        updateInventoryOfficeSupply.Vendor = InventoryOfficeSupply.Vendor;
                        updateInventoryOfficeSupply.Count = InventoryOfficeSupply.Count;
                        updateInventoryOfficeSupply.Cost = InventoryOfficeSupply.Cost;
                        updateInventoryOfficeSupply.Finished = InventoryOfficeSupply.Finished;
                        updateInventoryOfficeSupply.SupplyType = InventoryOfficeSupply.SupplyType;
                        updateInventoryOfficeSupply.SupplyTypeId = InventoryOfficeSupply.SupplyTypeId;
                        updateInventoryOfficeSupply.UpdatedOn = InventoryOfficeSupply.UpdatedOn;
                        updateInventoryOfficeSupply.UpdatedBy = InventoryOfficeSupply.UpdatedBy;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = InventoryOfficeSupply;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = InventoryOfficeSupply;
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
                    updateResponse.Result = InventoryOfficeSupply;
                }
                return updateResponse;
            }
        }

        public Response<InventoryOfficeSupply> Delete(long Id)
        {
            Response<InventoryOfficeSupply> deleteResponse = new Response<InventoryOfficeSupply>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    InventoryOfficeSupply deleteInventoryOfficeSupply = db.InventoryOfficeSupplys.SingleOrDefault(u => u.Id == Id);
                    if (deleteInventoryOfficeSupply != null)
                    {
                        db.InventoryOfficeSupplys.Remove(deleteInventoryOfficeSupply);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteInventoryOfficeSupply;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteInventoryOfficeSupply;
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
