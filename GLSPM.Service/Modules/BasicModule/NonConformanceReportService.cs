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
    public class NonConformanceReportService
    {
        #region Common Methods

        public List<NonConformanceReport> All()
        {
            List<NonConformanceReport> NonConformanceReport = new List<NonConformanceReport>();

            using (GLSPMContext db = new GLSPMContext())
            {
                NonConformanceReport = db.NonConformanceReports.OrderBy(f => f.Id).AsParallel().ToList();

                if (NonConformanceReport == null)
                {
                    NonConformanceReport = new List<NonConformanceReport>();
                }
                return NonConformanceReport;
            }
        }

        public NonConformanceReport GetByID(int userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                NonConformanceReport NonConformanceReport = db.NonConformanceReports.SingleOrDefault(u => u.Id == userRoleId);
                return NonConformanceReport;
            }
        }
        public int GetNCRN()
        {
            int equipmentNumber = 0;
            List<NonConformanceReport> InventoryEquipment = new List<NonConformanceReport>();
            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryEquipment = db.NonConformanceReports.OrderBy(f => f.Id).AsParallel().ToList();
                if (InventoryEquipment != null)
                {
                    if (InventoryEquipment.Count > 0)
                    {
                        try
                        {
                            equipmentNumber = InventoryEquipment.Max(i => i.NCRN).Value;
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
        public Response<NonConformanceReport> Add(NonConformanceReport NonConformanceReport)
        {
            Response<NonConformanceReport> addResponse = new Response<NonConformanceReport>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.NonConformanceReports.Add(NonConformanceReport);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = NonConformanceReport;
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
                    addResponse.Result = NonConformanceReport;
                }
                return addResponse;
            }
        }

        public Response<NonConformanceReport> Update(NonConformanceReport NonConformanceReport)
        {
            Response<NonConformanceReport> updateResponse = new Response<NonConformanceReport>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    NonConformanceReport updateNonConformanceReport = db.NonConformanceReports.SingleOrDefault(u => u.Id == NonConformanceReport.Id);
                    if (updateNonConformanceReport != null)
                    {
                        //updateNonConformanceReport = userRole;
                        updateNonConformanceReport.Id = NonConformanceReport.Id;
                        updateNonConformanceReport.NCRN = NonConformanceReport.NCRN;
                        updateNonConformanceReport.Analyst = NonConformanceReport.Analyst;
                        updateNonConformanceReport.Section = NonConformanceReport.Section;
                        updateNonConformanceReport.Date = NonConformanceReport.Date;

                        updateNonConformanceReport.Type = NonConformanceReport.Type;
                        updateNonConformanceReport.Description = NonConformanceReport.Description;
                        updateNonConformanceReport.Other = NonConformanceReport.Other;
                        updateNonConformanceReport.BatchesAffected = NonConformanceReport.BatchesAffected;
                        updateNonConformanceReport.Batch1 = NonConformanceReport.Batch1;
                        updateNonConformanceReport.Batch2 = NonConformanceReport.Batch2;
                        updateNonConformanceReport.Batch3 = NonConformanceReport.Batch3;
                        updateNonConformanceReport.Batch4 = NonConformanceReport.Batch4;
                        updateNonConformanceReport.Batch5 = NonConformanceReport.Batch5;
                        updateNonConformanceReport.EquipmentAffected = NonConformanceReport.EquipmentAffected;
                        updateNonConformanceReport.Inv1 = NonConformanceReport.Inv1;
                        updateNonConformanceReport.Inv2 = NonConformanceReport.Inv2;
                        updateNonConformanceReport.Inv3 = NonConformanceReport.Inv3;
                        updateNonConformanceReport.Inv4 = NonConformanceReport.Inv4;
                        updateNonConformanceReport.Inv5 = NonConformanceReport.Inv5;
                        updateNonConformanceReport.NCRRating = NonConformanceReport.NCRRating;
                        updateNonConformanceReport.CorrectiveActionRequired = NonConformanceReport.CorrectiveActionRequired;
                        updateNonConformanceReport.ImmediateActions = NonConformanceReport.ImmediateActions;
                        updateNonConformanceReport.OtherActions = NonConformanceReport.OtherActions;
                        updateNonConformanceReport.RootCause = NonConformanceReport.RootCause;
                        updateNonConformanceReport.PossibleCA1 = NonConformanceReport.PossibleCA1;
                        updateNonConformanceReport.PossibleCA2 = NonConformanceReport.PossibleCA2;
                        updateNonConformanceReport.PossibleCA3 = NonConformanceReport.PossibleCA3;
                        updateNonConformanceReport.SelectedCA = NonConformanceReport.SelectedCA;
                        updateNonConformanceReport.MonitoringPlan = NonConformanceReport.MonitoringPlan;
                        updateNonConformanceReport.QAOComments = NonConformanceReport.QAOComments;
                        updateNonConformanceReport.QAOSignature = NonConformanceReport.QAOSignature;
                        updateNonConformanceReport.Reference = NonConformanceReport.Reference;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = NonConformanceReport;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = NonConformanceReport;
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
                    updateResponse.Result = NonConformanceReport;
                }
                return updateResponse;
            }
        }

        public Response<NonConformanceReport> Delete(long Id)
        {
            Response<NonConformanceReport> deleteResponse = new Response<NonConformanceReport>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    NonConformanceReport deleteNonConformanceReport = db.NonConformanceReports.SingleOrDefault(u => u.Id == Id);
                    if (deleteNonConformanceReport != null)
                    {
                        db.NonConformanceReports.Remove(deleteNonConformanceReport);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteNonConformanceReport;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteNonConformanceReport;
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
