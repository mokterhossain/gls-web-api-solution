using GLSPM.Data;
using GLSPM.Data.Modules.InvoiceModule;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Service.Modules.InvoiceModule
{
    public class InvoiceSettingService
    {
        #region Common Methods

        public List<InvoiceSetting> All()
        {
            List<InvoiceSetting> InvoiceSetting = new List<InvoiceSetting>();

            using (GLSPMContext db = new GLSPMContext())
            {
                InvoiceSetting = db.InvoiceSettings.OrderBy(f => f.Id).AsParallel().ToList();

                if (InvoiceSetting == null)
                {
                    InvoiceSetting = new List<InvoiceSetting>();
                }
                return InvoiceSetting;
            }
        }

        public InvoiceSetting GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                InvoiceSetting InvoiceSetting = db.InvoiceSettings.SingleOrDefault(u => u.Id == userRoleId);
                return InvoiceSetting;
            }
        }
        public Response<InvoiceSetting> Add(InvoiceSetting InvoiceSetting)
        {
            Response<InvoiceSetting> addResponse = new Response<InvoiceSetting>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.InvoiceSettings.Add(InvoiceSetting);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = InvoiceSetting;
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
                    addResponse.Result = InvoiceSetting;
                }
                return addResponse;
            }
        }

        public Response<InvoiceSetting> Update(InvoiceSetting InvoiceSetting)
        {
            Response<InvoiceSetting> updateResponse = new Response<InvoiceSetting>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    InvoiceSetting updateInvoiceSetting = db.InvoiceSettings.SingleOrDefault(u => u.Id == InvoiceSetting.Id);
                    if (updateInvoiceSetting != null)
                    {
                        //updateInvoiceSetting = userRole;
                        updateInvoiceSetting.Id = InvoiceSetting.Id;
                        updateInvoiceSetting.TaxPercentage = InvoiceSetting.TaxPercentage;
                        updateInvoiceSetting.PST = InvoiceSetting.PST;
                        updateInvoiceSetting.Shipping = InvoiceSetting.Shipping;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = InvoiceSetting;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = InvoiceSetting;
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
                    updateResponse.Result = InvoiceSetting;
                }
                return updateResponse;
            }
        }

        public Response<InvoiceSetting> Delete(long Id)
        {
            Response<InvoiceSetting> deleteResponse = new Response<InvoiceSetting>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    InvoiceSetting deleteInvoiceSetting = db.InvoiceSettings.SingleOrDefault(u => u.Id == Id);
                    if (deleteInvoiceSetting != null)
                    {
                        db.InvoiceSettings.Remove(deleteInvoiceSetting);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteInvoiceSetting;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteInvoiceSetting;
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
