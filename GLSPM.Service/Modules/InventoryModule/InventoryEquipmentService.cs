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
    public class InventoryEquipmentService
    {
        #region Common Methods

        public List<InventoryEquipment> All()
        {
            List<InventoryEquipment> InventoryEquipment = new List<InventoryEquipment>();

            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryEquipment = db.InventoryEquipments.OrderBy(f => f.Id).AsParallel().ToList();

                if (InventoryEquipment == null)
                {
                    InventoryEquipment = new List<InventoryEquipment>();
                }
                return InventoryEquipment;
            }
        }

        public InventoryEquipment GetByID(int userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryEquipment InventoryEquipment = db.InventoryEquipments.SingleOrDefault(u => u.Id == userRoleId);
                return InventoryEquipment;
            }
        }
        public int GetEquipmentNumber()
        {
            int equipmentNumber = 0;
            List<InventoryEquipment> InventoryEquipment = new List<InventoryEquipment>();
            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryEquipment = db.InventoryEquipments.OrderBy(f => f.Id).AsParallel().ToList();
                if (InventoryEquipment != null)
                {
                    if (InventoryEquipment.Count > 0)
                    {
                        try
                        {
                            equipmentNumber = InventoryEquipment.Max(i => i.EquipmentNumber).Value;
                            equipmentNumber = equipmentNumber + 1;
                        }
                        catch (Exception ex) { }
                    }
                    else
                    {
                        equipmentNumber = 1;
                    }
                }
                else
                {
                    equipmentNumber = 1;
                }
            }
            return equipmentNumber;
        }
        public Response<InventoryEquipment> Add(InventoryEquipment InventoryEquipment)
        {
            Response<InventoryEquipment> addResponse = new Response<InventoryEquipment>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.InventoryEquipments.Add(InventoryEquipment);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = InventoryEquipment;
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
                    addResponse.Result = InventoryEquipment;
                }
                return addResponse;
            }
        }

        public Response<InventoryEquipment> Update(InventoryEquipment InventoryEquipment)
        {
            Response<InventoryEquipment> updateResponse = new Response<InventoryEquipment>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    InventoryEquipment updateInventoryEquipment = db.InventoryEquipments.SingleOrDefault(u => u.Id == InventoryEquipment.Id);
                    if (updateInventoryEquipment != null)
                    {
                        //updateInventoryEquipment = userRole;
                        updateInventoryEquipment.Id = InventoryEquipment.Id;
                        updateInventoryEquipment.EquipmentNumber = InventoryEquipment.EquipmentNumber;
                        updateInventoryEquipment.EquipmentName = InventoryEquipment.EquipmentName;
                        updateInventoryEquipment.DateOfPurchase = InventoryEquipment.DateOfPurchase;
                        updateInventoryEquipment.DateOfService = InventoryEquipment.DateOfService;
                        updateInventoryEquipment.DateOfMaintenance = InventoryEquipment.DateOfMaintenance;
                        updateInventoryEquipment.Description = InventoryEquipment.Description;
                        updateInventoryEquipment.PerformedBy = InventoryEquipment.PerformedBy;
                        updateInventoryEquipment.DateChecked = InventoryEquipment.DateChecked;
                        updateInventoryEquipment.CheckedBy = InventoryEquipment.CheckedBy;
                        updateInventoryEquipment.NextMaintenanceDate = InventoryEquipment.NextMaintenanceDate;
                        updateInventoryEquipment.ValidationDue = InventoryEquipment.ValidationDue;
                        updateInventoryEquipment.Cost = InventoryEquipment.Cost;
                        updateInventoryEquipment.Location = InventoryEquipment.Location;
                        updateInventoryEquipment.WorkInstruction = InventoryEquipment.WorkInstruction;
                        updateInventoryEquipment.DisposalDate = InventoryEquipment.DisposalDate;
                        updateInventoryEquipment.ModelNumber = InventoryEquipment.ModelNumber;
                        updateInventoryEquipment.SerialNumber = InventoryEquipment.SerialNumber;
                        updateInventoryEquipment.Manufacturer = InventoryEquipment.Manufacturer;
                        updateInventoryEquipment.Comments = InventoryEquipment.Comments;
                        updateInventoryEquipment.PersonResponsible = InventoryEquipment.PersonResponsible;
                        updateInventoryEquipment.Calibaration = InventoryEquipment.Calibaration;
                        updateInventoryEquipment.Discarded = InventoryEquipment.Discarded;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = InventoryEquipment;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = InventoryEquipment;
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
                    updateResponse.Result = InventoryEquipment;
                }
                return updateResponse;
            }
        }

        public Response<InventoryEquipment> Delete(long Id)
        {
            Response<InventoryEquipment> deleteResponse = new Response<InventoryEquipment>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    InventoryEquipment deleteInventoryEquipment = db.InventoryEquipments.SingleOrDefault(u => u.Id == Id);
                    if (deleteInventoryEquipment != null)
                    {
                        db.InventoryEquipments.Remove(deleteInventoryEquipment);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteInventoryEquipment;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteInventoryEquipment;
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

        public List<InventoryEquipmentViewModel> GetAllProjectSampleByProjectIds(string Ids)
        {
            List<InventoryEquipmentViewModel> projectViewModel = new List<InventoryEquipmentViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var IdParameter = new SqlParameter { ParameterName = "Ids", Value = Ids };
                try
                {
                    projectViewModel = db.Database.SqlQuery<InventoryEquipmentViewModel>("GLS_GetAllInventoryEquipmentByIds @Ids", IdParameter).ToList<InventoryEquipmentViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<InventoryEquipmentViewModel>();
                }
                return projectViewModel;
            }
        }
        public List<InventoryEquipmentViewModel> GetAllInventoryEquipmentDetailByIdsForMaster(string Ids)
        {
            List<InventoryEquipmentViewModel> projectViewModel = new List<InventoryEquipmentViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var IdParameter = new SqlParameter { ParameterName = "Ids", Value = Ids };
                try
                {
                    projectViewModel = db.Database.SqlQuery<InventoryEquipmentViewModel>("GLS_GetAllInventoryEquipmentDetailByIdsForMaster @Ids", IdParameter).ToList<InventoryEquipmentViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<InventoryEquipmentViewModel>();
                }
                return projectViewModel;
            }
        }
        public List<ExpiredNotificationViewModel> GetAllExpiredNotificationViewModel()
        {
            List<ExpiredNotificationViewModel> projectViewModel = new List<ExpiredNotificationViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    projectViewModel = db.Database.SqlQuery<ExpiredNotificationViewModel>("GLS_GetAllExpiredNotificationViewModel").ToList<ExpiredNotificationViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<ExpiredNotificationViewModel>();
                }
                return projectViewModel;
            }
        }
    }
}
