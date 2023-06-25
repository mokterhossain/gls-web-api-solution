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
    public class InventoryChemicalNameService
    {
        #region Common Methods

        public List<InventoryChemicalName> All()
        {
            List<InventoryChemicalName> InventoryChemicalName = new List<InventoryChemicalName>();

            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryChemicalName = db.InventoryChemicalNames.OrderBy(f => f.Id).AsParallel().ToList();

                if (InventoryChemicalName == null)
                {
                    InventoryChemicalName = new List<InventoryChemicalName>();
                }
                return InventoryChemicalName;
            }
        }
        //public List<InventoryChemicalName> AllByCategory(string category)
        //{
        //    List<InventoryChemicalName> InventoryChemicalName = new List<InventoryChemicalName>();

        //    using (GLSPMContext db = new GLSPMContext())
        //    {
        //        InventoryChemicalName = db.InventoryChemicalNames.OrderBy(f => f.Id).Where(s => s.SupplyCategory == category).AsParallel().ToList();

        //        if (InventoryChemicalName == null)
        //        {
        //            InventoryChemicalName = new List<InventoryChemicalName>();
        //        }
        //        return InventoryChemicalName;
        //    }
        //}
        public InventoryChemicalName GetByID(int userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryChemicalName InventoryChemicalName = db.InventoryChemicalNames.SingleOrDefault(u => u.Id == userRoleId);
                return InventoryChemicalName;
            }
        }
        public Response<InventoryChemicalName> Add(InventoryChemicalName InventoryChemicalName)
        {
            Response<InventoryChemicalName> addResponse = new Response<InventoryChemicalName>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.InventoryChemicalNames.Add(InventoryChemicalName);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = InventoryChemicalName;
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
                    addResponse.Result = InventoryChemicalName;
                }
                return addResponse;
            }
        }

        public Response<InventoryChemicalName> Update(InventoryChemicalName InventoryChemicalName)
        {
            Response<InventoryChemicalName> updateResponse = new Response<InventoryChemicalName>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    InventoryChemicalName updateInventoryChemicalName = db.InventoryChemicalNames.SingleOrDefault(u => u.Id == InventoryChemicalName.Id);
                    if (updateInventoryChemicalName != null)
                    {
                        //updateInventoryChemicalName = userRole;
                        updateInventoryChemicalName.Id = InventoryChemicalName.Id;
                        updateInventoryChemicalName.Name = InventoryChemicalName.Name;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = InventoryChemicalName;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = InventoryChemicalName;
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
                    updateResponse.Result = InventoryChemicalName;
                }
                return updateResponse;
            }
        }

        public Response<InventoryChemicalName> Delete(long Id)
        {
            Response<InventoryChemicalName> deleteResponse = new Response<InventoryChemicalName>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    InventoryChemicalName deleteInventoryChemicalName = db.InventoryChemicalNames.SingleOrDefault(u => u.Id == Id);
                    if (deleteInventoryChemicalName != null)
                    {
                        db.InventoryChemicalNames.Remove(deleteInventoryChemicalName);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteInventoryChemicalName;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteInventoryChemicalName;
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
