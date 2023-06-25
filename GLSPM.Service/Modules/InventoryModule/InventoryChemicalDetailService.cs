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
    public class InventoryChemicalDetailService
    {
        #region Common Methods

        public List<InventoryChemicalDetail> All()
        {
            List<InventoryChemicalDetail> InventoryChemicalDetail = new List<InventoryChemicalDetail>();

            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryChemicalDetail = db.InventoryChemicalDetails.OrderBy(f => f.Id).AsParallel().ToList();

                if (InventoryChemicalDetail == null)
                {
                    InventoryChemicalDetail = new List<InventoryChemicalDetail>();
                }
                return InventoryChemicalDetail;
            }
        }

        public InventoryChemicalDetail GetByID(int userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryChemicalDetail InventoryChemicalDetail = db.InventoryChemicalDetails.SingleOrDefault(u => u.Id == userRoleId);
                return InventoryChemicalDetail;
            }
        }
        public List<InventoryChemicalDetail> GetByChemical(int chemicalId)
        {
            List<InventoryChemicalDetail> InventoryChemicalDetail = new List<InventoryChemicalDetail>();

            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryChemicalDetail = db.InventoryChemicalDetails.Where(i => i.ChemicalRefId == chemicalId).OrderBy(f => f.Id).AsParallel().ToList();

                if (InventoryChemicalDetail == null)
                {
                    InventoryChemicalDetail = new List<InventoryChemicalDetail>();
                }
                return InventoryChemicalDetail;
            }
        }
        //public int GetChemicalId()
        //{
        //    int ChemicalNumber = 0;
        //    List<InventoryChemicalDetail> InventoryChemicalDetail = new List<InventoryChemicalDetail>();
        //    using (GLSPMContext db = new GLSPMContext())
        //    {
        //        InventoryChemicalDetail = db.InventoryChemicalDetails.OrderBy(f => f.Id).AsParallel().ToList();
        //        if (InventoryChemicalDetail != null)
        //        {
        //            if (InventoryChemicalDetail.Count > 0)
        //            {
        //                try
        //                {
        //                    ChemicalNumber = InventoryChemicalDetail.Max(i => i.ChemicalId).Value;
        //                    ChemicalNumber = ChemicalNumber + 1;
        //                }
        //                catch (Exception ex) { }
        //            }
        //            else
        //            {
        //                ChemicalNumber = 1;
        //            }
        //        }
        //        else
        //        {
        //            ChemicalNumber = 1;
        //        }
        //    }
        //    return ChemicalNumber;
        //}
        public Response<InventoryChemicalDetail> Add(InventoryChemicalDetail InventoryChemicalDetail)
        {
            Response<InventoryChemicalDetail> addResponse = new Response<InventoryChemicalDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.InventoryChemicalDetails.Add(InventoryChemicalDetail);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = InventoryChemicalDetail;
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
                    addResponse.Result = InventoryChemicalDetail;
                }
                return addResponse;
            }
        }

        public Response<InventoryChemicalDetail> Update(InventoryChemicalDetail InventoryChemicalDetail)
        {
            Response<InventoryChemicalDetail> updateResponse = new Response<InventoryChemicalDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    InventoryChemicalDetail updateInventoryChemicalDetail = db.InventoryChemicalDetails.SingleOrDefault(u => u.Id == InventoryChemicalDetail.Id);
                    if (updateInventoryChemicalDetail != null)
                    {
                        //updateInventoryChemicalDetail = userRole;
                        updateInventoryChemicalDetail.Id = InventoryChemicalDetail.Id;
                        updateInventoryChemicalDetail.ChemicalRefId = InventoryChemicalDetail.ChemicalRefId;
                        //updateInventoryChemicalDetail.ChemicalName = InventoryChemicalDetail.ChemicalName;
                        updateInventoryChemicalDetail.Principal = InventoryChemicalDetail.Principal;
                        updateInventoryChemicalDetail.ContainerSize = InventoryChemicalDetail.ContainerSize;
                        updateInventoryChemicalDetail.ContainerType = InventoryChemicalDetail.ContainerType;
                        updateInventoryChemicalDetail.CASNumber = InventoryChemicalDetail.CASNumber;
                        updateInventoryChemicalDetail.Manufacturer = InventoryChemicalDetail.Manufacturer;
                        updateInventoryChemicalDetail.DateOfPurchase = InventoryChemicalDetail.DateOfPurchase;
                        updateInventoryChemicalDetail.ExpiryDate = InventoryChemicalDetail.ExpiryDate;
                        updateInventoryChemicalDetail.BatchNumber = InventoryChemicalDetail.BatchNumber;
                        updateInventoryChemicalDetail.Room = InventoryChemicalDetail.Room;
                        updateInventoryChemicalDetail.Storage = InventoryChemicalDetail.Storage;
                        updateInventoryChemicalDetail.LabChemicalId = InventoryChemicalDetail.LabChemicalId;
                        updateInventoryChemicalDetail.SerialNumber = InventoryChemicalDetail.SerialNumber;
                        updateInventoryChemicalDetail.Finished = InventoryChemicalDetail.Finished;
                        updateInventoryChemicalDetail.MSDS = InventoryChemicalDetail.MSDS;
                        updateInventoryChemicalDetail.PrefixSerial = InventoryChemicalDetail.PrefixSerial;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = InventoryChemicalDetail;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = InventoryChemicalDetail;
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
                    updateResponse.Result = InventoryChemicalDetail;
                }
                return updateResponse;
            }
        }

        public Response<InventoryChemicalDetail> Delete(long Id)
        {
            Response<InventoryChemicalDetail> deleteResponse = new Response<InventoryChemicalDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    InventoryChemicalDetail deleteInventoryChemicalDetail = db.InventoryChemicalDetails.SingleOrDefault(u => u.Id == Id);
                    if (deleteInventoryChemicalDetail != null)
                    {
                        db.InventoryChemicalDetails.Remove(deleteInventoryChemicalDetail);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteInventoryChemicalDetail;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteInventoryChemicalDetail;
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
        public List<InventoryChemicalDetailViewModel> GetAllInventoryChemicalDetailByIds(string Ids)
        {
            List<InventoryChemicalDetailViewModel> projectViewModel = new List<InventoryChemicalDetailViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var IdParameter = new SqlParameter { ParameterName = "Ids", Value = Ids };
                try
                {
                    projectViewModel = db.Database.SqlQuery<InventoryChemicalDetailViewModel>("GLS_GetAllInventoryChemicalDetailByIds @Ids", IdParameter).ToList<InventoryChemicalDetailViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<InventoryChemicalDetailViewModel>();
                }
                return projectViewModel;
            }
        }
        public List<InventoryChemicalDetailViewModel> GetAllInventoryChemicalDetailByIdsForMaster(string Ids)
        {
            List<InventoryChemicalDetailViewModel> projectViewModel = new List<InventoryChemicalDetailViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var IdParameter = new SqlParameter { ParameterName = "Ids", Value = Ids };
                try
                {
                    projectViewModel = db.Database.SqlQuery<InventoryChemicalDetailViewModel>("GLS_GetAllInventoryChemicalDetailByIdsForMaster @Ids", IdParameter).ToList<InventoryChemicalDetailViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<InventoryChemicalDetailViewModel>();
                }
                return projectViewModel;
            }
        }
        
    }
}
