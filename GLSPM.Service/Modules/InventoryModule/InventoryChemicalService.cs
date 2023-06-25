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
    public class InventoryChemicalService
    {
        #region Common Methods

        public List<InventoryChemical> All()
        {
            List<InventoryChemical> InventoryChemical = new List<InventoryChemical>();

            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryChemical = db.InventoryChemicals.OrderBy(f => f.Id).AsParallel().ToList();

                if (InventoryChemical == null)
                {
                    InventoryChemical = new List<InventoryChemical>();
                }
                return InventoryChemical;
            }
        }

        public InventoryChemical GetByID(int userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryChemical InventoryChemical = db.InventoryChemicals.SingleOrDefault(u => u.Id == userRoleId);
                return InventoryChemical;
            }
        }
        public List<InventoryChemical> GetByName(string name)
        {
            List<InventoryChemical> InventoryChemical = new List<InventoryChemical>();

            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryChemical = db.InventoryChemicals.Where(i=>i.ChemicalName == name).OrderBy(f => f.Id).AsParallel().ToList();

                if (InventoryChemical == null)
                {
                    InventoryChemical = new List<InventoryChemical>();
                }
                return InventoryChemical;
            }
        }
        public int GetChemicalId()
        {
            int ChemicalNumber = 0;
            List<InventoryChemical> InventoryChemical = new List<InventoryChemical>();
            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryChemical = db.InventoryChemicals.OrderBy(f => f.Id).AsParallel().ToList();
                if (InventoryChemical != null)
                {
                    if (InventoryChemical.Count > 0)
                    {
                        try
                        {
                            ChemicalNumber = InventoryChemical.Max(i => i.ChemicalId).Value;
                            ChemicalNumber = ChemicalNumber + 1;
                        }
                        catch (Exception ex) { }
                    }
                    else
                    {
                        ChemicalNumber = 1;
                    }
                }
                else
                {
                    ChemicalNumber = 1;
                }
            }
            return ChemicalNumber;
        }
        public Response<InventoryChemical> Add(InventoryChemical InventoryChemical)
        {
            Response<InventoryChemical> addResponse = new Response<InventoryChemical>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.InventoryChemicals.Add(InventoryChemical);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = InventoryChemical;
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
                    addResponse.Result = InventoryChemical;
                }
                return addResponse;
            }
        }

        public Response<InventoryChemical> Update(InventoryChemical InventoryChemical)
        {
            Response<InventoryChemical> updateResponse = new Response<InventoryChemical>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    InventoryChemical updateInventoryChemical = db.InventoryChemicals.SingleOrDefault(u => u.Id == InventoryChemical.Id);
                    if (updateInventoryChemical != null)
                    {
                        //updateInventoryChemical = userRole;
                        updateInventoryChemical.Id = InventoryChemical.Id;
                        updateInventoryChemical.ChemicalId = InventoryChemical.ChemicalId;
                        updateInventoryChemical.ChemicalName = InventoryChemical.ChemicalName;
                        updateInventoryChemical.Principal = InventoryChemical.Principal;
                        updateInventoryChemical.ContainerSize = InventoryChemical.ContainerSize;
                        updateInventoryChemical.ContainerType = InventoryChemical.ContainerType;
                        updateInventoryChemical.CASNumber = InventoryChemical.CASNumber;
                        updateInventoryChemical.Manufacturer = InventoryChemical.Manufacturer;
                        updateInventoryChemical.DateOfPurchase = InventoryChemical.DateOfPurchase;
                        updateInventoryChemical.ExpiryDate = InventoryChemical.ExpiryDate;
                        updateInventoryChemical.BatchNumber = InventoryChemical.BatchNumber;
                        updateInventoryChemical.Room = InventoryChemical.Room;
                        updateInventoryChemical.Storage = InventoryChemical.Storage;
                        updateInventoryChemical.LabChemicalId = InventoryChemical.LabChemicalId;
                        updateInventoryChemical.SerialNumber = InventoryChemical.SerialNumber;
                        updateInventoryChemical.Finished = InventoryChemical.Finished;
                        updateInventoryChemical.MSDS = InventoryChemical.MSDS;
                        updateInventoryChemical.PrefixSerial = InventoryChemical.PrefixSerial;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = InventoryChemical;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = InventoryChemical;
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
                    updateResponse.Result = InventoryChemical;
                }
                return updateResponse;
            }
        }

        public Response<InventoryChemical> Delete(long Id)
        {
            Response<InventoryChemical> deleteResponse = new Response<InventoryChemical>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    InventoryChemical deleteInventoryChemical = db.InventoryChemicals.SingleOrDefault(u => u.Id == Id);
                    if (deleteInventoryChemical != null)
                    {
                        db.InventoryChemicals.Remove(deleteInventoryChemical);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteInventoryChemical;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteInventoryChemical;
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
        public List<InventoryChemical> GetAllInventoryChemicalByIds(string Ids)
        {
            List<InventoryChemical> projectViewModel = new List<InventoryChemical>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var IdParameter = new SqlParameter { ParameterName = "Ids", Value = Ids };
                try
                {
                    projectViewModel = db.Database.SqlQuery<InventoryChemical>("GLS_GetAllInventoryChemicalByIds @Ids", IdParameter).ToList<InventoryChemical>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<InventoryChemical>();
                }
                return projectViewModel;
            }
        }
    }
}
