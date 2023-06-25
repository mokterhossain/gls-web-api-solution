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
    public class InventoryEquipmentDetailService
    {
        #region Common Methods

        public List<InventoryEquipmentDetail> All()
        {
            List<InventoryEquipmentDetail> InventoryEquipmentDetail = new List<InventoryEquipmentDetail>();

            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryEquipmentDetail = db.InventoryEquipmentDetails.OrderBy(f => f.Id).AsParallel().ToList();

                if (InventoryEquipmentDetail == null)
                {
                    InventoryEquipmentDetail = new List<InventoryEquipmentDetail>();
                }
                return InventoryEquipmentDetail;
            }
        }
        public List<InventoryEquipmentDetail> GetByEquipment(int eqiupmentId)
        {
            List<InventoryEquipmentDetail> InventoryEquipmentDetail = new List<InventoryEquipmentDetail>();

            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryEquipmentDetail = db.InventoryEquipmentDetails.Where(e=>e.EquipmentRefId == eqiupmentId).OrderBy(f => f.Id).AsParallel().ToList();

                if (InventoryEquipmentDetail == null)
                {
                    InventoryEquipmentDetail = new List<InventoryEquipmentDetail>();
                }
                return InventoryEquipmentDetail;
            }
        }
        public InventoryEquipmentDetail GetByID(int userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryEquipmentDetail InventoryEquipmentDetail = db.InventoryEquipmentDetails.SingleOrDefault(u => u.Id == userRoleId);
                return InventoryEquipmentDetail;
            }
        }
        //public int GetEquipmentNumber()
        //{
        //    int equipmentNumber = 0;
        //    List<InventoryEquipmentDetail> InventoryEquipmentDetail = new List<InventoryEquipmentDetail>();
        //    using (GLSPMContext db = new GLSPMContext())
        //    {
        //        InventoryEquipmentDetail = db.InventoryEquipmentDetails.OrderBy(f => f.Id).AsParallel().ToList();
        //        if (InventoryEquipmentDetail != null)
        //        {
        //            if (InventoryEquipmentDetail.Count > 0)
        //            {
        //                try
        //                {
        //                    equipmentNumber = InventoryEquipmentDetail.Max(i => i.EquipmentNumber).Value;
        //                    equipmentNumber = equipmentNumber + 1;
        //                }
        //                catch (Exception ex) { }
        //            }
        //            else
        //            {
        //                equipmentNumber = 1;
        //            }
        //        }
        //        else
        //        {
        //            equipmentNumber = 1;
        //        }
        //    }
        //    return equipmentNumber;
        //}
        public Response<InventoryEquipmentDetail> Add(InventoryEquipmentDetail InventoryEquipmentDetail)
        {
            Response<InventoryEquipmentDetail> addResponse = new Response<InventoryEquipmentDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.InventoryEquipmentDetails.Add(InventoryEquipmentDetail);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = InventoryEquipmentDetail;
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
                    addResponse.Result = InventoryEquipmentDetail;
                }
                return addResponse;
            }
        }

        public Response<InventoryEquipmentDetail> Update(InventoryEquipmentDetail InventoryEquipmentDetail)
        {
            Response<InventoryEquipmentDetail> updateResponse = new Response<InventoryEquipmentDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    InventoryEquipmentDetail updateInventoryEquipmentDetail = db.InventoryEquipmentDetails.SingleOrDefault(u => u.Id == InventoryEquipmentDetail.Id);
                    if (updateInventoryEquipmentDetail != null)
                    {
                        //updateInventoryEquipmentDetail = userRole;
                        updateInventoryEquipmentDetail.Id = InventoryEquipmentDetail.Id;
                        updateInventoryEquipmentDetail.EquipmentRefId = InventoryEquipmentDetail.EquipmentRefId;
                        updateInventoryEquipmentDetail.DateOfPurchase = InventoryEquipmentDetail.DateOfPurchase;
                        updateInventoryEquipmentDetail.DateOfService = InventoryEquipmentDetail.DateOfService;
                        updateInventoryEquipmentDetail.DateOfMaintenance = InventoryEquipmentDetail.DateOfMaintenance;
                        updateInventoryEquipmentDetail.Description = InventoryEquipmentDetail.Description;
                        updateInventoryEquipmentDetail.PerformedBy = InventoryEquipmentDetail.PerformedBy;
                        updateInventoryEquipmentDetail.DateChecked = InventoryEquipmentDetail.DateChecked;
                        updateInventoryEquipmentDetail.CheckedBy = InventoryEquipmentDetail.CheckedBy;
                        updateInventoryEquipmentDetail.NextMaintenanceDate = InventoryEquipmentDetail.NextMaintenanceDate;
                        updateInventoryEquipmentDetail.ValidationDue = InventoryEquipmentDetail.ValidationDue;
                        updateInventoryEquipmentDetail.Cost = InventoryEquipmentDetail.Cost;
                        updateInventoryEquipmentDetail.Location = InventoryEquipmentDetail.Location;
                        updateInventoryEquipmentDetail.WorkInstruction = InventoryEquipmentDetail.WorkInstruction;
                        updateInventoryEquipmentDetail.DisposalDate = InventoryEquipmentDetail.DisposalDate;
                        updateInventoryEquipmentDetail.ModelNumber = InventoryEquipmentDetail.ModelNumber;
                        updateInventoryEquipmentDetail.SerialNumber = InventoryEquipmentDetail.SerialNumber;
                        updateInventoryEquipmentDetail.Manufacturer = InventoryEquipmentDetail.Manufacturer;
                        updateInventoryEquipmentDetail.Comments = InventoryEquipmentDetail.Comments;
                        updateInventoryEquipmentDetail.PersonResponsible = InventoryEquipmentDetail.PersonResponsible;
                        updateInventoryEquipmentDetail.Calibaration = InventoryEquipmentDetail.Calibaration;
                        updateInventoryEquipmentDetail.Discarded = InventoryEquipmentDetail.Discarded;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = InventoryEquipmentDetail;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = InventoryEquipmentDetail;
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
                    updateResponse.Result = InventoryEquipmentDetail;
                }
                return updateResponse;
            }
        }

        public Response<InventoryEquipmentDetail> Delete(long Id)
        {
            Response<InventoryEquipmentDetail> deleteResponse = new Response<InventoryEquipmentDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    InventoryEquipmentDetail deleteInventoryEquipmentDetail = db.InventoryEquipmentDetails.SingleOrDefault(u => u.Id == Id);
                    if (deleteInventoryEquipmentDetail != null)
                    {
                        db.InventoryEquipmentDetails.Remove(deleteInventoryEquipmentDetail);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteInventoryEquipmentDetail;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteInventoryEquipmentDetail;
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

        public List<InventoryEquipmentDetailViewModel> GetAllProjectSampleByProjectIds(string Ids)
        {
            List<InventoryEquipmentDetailViewModel> projectViewModel = new List<InventoryEquipmentDetailViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var IdParameter = new SqlParameter { ParameterName = "Ids", Value = Ids };
                try
                {
                    projectViewModel = db.Database.SqlQuery<InventoryEquipmentDetailViewModel>("GLS_GetAllInventoryEquipmentDetailByIds @Ids", IdParameter).ToList<InventoryEquipmentDetailViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<InventoryEquipmentDetailViewModel>();
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
