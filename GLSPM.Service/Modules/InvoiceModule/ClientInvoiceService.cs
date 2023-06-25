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
    public class ClientInvoiceService
    {
        #region Common Methods

        public List<ClientInvoice> All()
        {
            List<ClientInvoice> ClientInvoice = new List<ClientInvoice>();

            using (GLSPMContext db = new GLSPMContext())
            {
                ClientInvoice = db.ClientInvoices.OrderBy(f => f.Id).AsParallel().ToList();

                if (ClientInvoice == null)
                {
                    ClientInvoice = new List<ClientInvoice>();
                }
                return ClientInvoice;
            }
        }

        public ClientInvoice GetByID(int userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                ClientInvoice ClientInvoice = db.ClientInvoices.SingleOrDefault(u => u.Id == userRoleId);
                return ClientInvoice;
            }
        }
        public ClientInvoice GetByProjectID(long projectId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                ClientInvoice ClientInvoice = db.ClientInvoices.SingleOrDefault(u => u.ProjectId == projectId);
                return ClientInvoice;
            }
        }
        public Response<ClientInvoice> Add(ClientInvoice ClientInvoice)
        {
            Response<ClientInvoice> addResponse = new Response<ClientInvoice>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.ClientInvoices.Add(ClientInvoice);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = ClientInvoice;
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
                    addResponse.Result = ClientInvoice;
                }
                return addResponse;
            }
        }

        public Response<ClientInvoice> Update(ClientInvoice ClientInvoice)
        {
            Response<ClientInvoice> updateResponse = new Response<ClientInvoice>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    ClientInvoice updateClientInvoice = db.ClientInvoices.SingleOrDefault(u => u.Id == ClientInvoice.Id);
                    if (updateClientInvoice != null)
                    {
                        //updateClientInvoice = userRole;
                        updateClientInvoice.Id = ClientInvoice.Id;
                        updateClientInvoice.ClientId = ClientInvoice.ClientId;
                        updateClientInvoice.ProjectId = ClientInvoice.ProjectId;
                        updateClientInvoice.InvoiceNumber = ClientInvoice.InvoiceNumber;
                        updateClientInvoice.InvoiceDate = ClientInvoice.InvoiceDate;
                        updateClientInvoice.PaymentDueDate = ClientInvoice.PaymentDueDate;
                        updateClientInvoice.SubTotal = ClientInvoice.SubTotal;
                        updateClientInvoice.TaxAmount = ClientInvoice.TaxAmount;
                        updateClientInvoice.PST = ClientInvoice.PST;
                        updateClientInvoice.Shipping = ClientInvoice.Shipping;
                        updateClientInvoice.Discount = ClientInvoice.Discount;
                        updateClientInvoice.Total = ClientInvoice.Total;
                        updateClientInvoice.SampleNote = ClientInvoice.SampleNote;
                        updateClientInvoice.UpdatedOn = ClientInvoice.UpdatedOn;
                        updateClientInvoice.UpdatedBy = ClientInvoice.UpdatedBy;
                        updateClientInvoice.AccountsManagerId = ClientInvoice.AccountsManagerId;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = ClientInvoice;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = ClientInvoice;
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
                    updateResponse.Result = ClientInvoice;
                }
                return updateResponse;
            }
        }

        public Response<ClientInvoice> Delete(long Id)
        {
            Response<ClientInvoice> deleteResponse = new Response<ClientInvoice>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    ClientInvoice deleteClientInvoice = db.ClientInvoices.SingleOrDefault(u => u.Id == Id);
                    if (deleteClientInvoice != null)
                    {
                        db.ClientInvoices.Remove(deleteClientInvoice);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteClientInvoice;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteClientInvoice;
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
        #region Other Methods
        public ClientInvoiceViewModel GetInvoiceByProjectId(long projectId)
        {
            List<ClientInvoiceViewModel> data = new List<ClientInvoiceViewModel>();
            ClientInvoiceViewModel returnData = new ClientInvoiceViewModel();
            using (GLSPMContext db = new GLSPMContext())
            {
                var projectIdParameter = new SqlParameter { ParameterName = "ProjectId", Value = projectId };
                try
                {
                    data = db.Database.SqlQuery<ClientInvoiceViewModel>("GLS_GetInvoiceByProjectId @ProjectId", projectIdParameter).ToList<ClientInvoiceViewModel>();
                }
                catch (Exception ex) { }
                if (data == null)
                {
                    data = new List<ClientInvoiceViewModel>();
                }
                else
                {
                    returnData = data.FirstOrDefault();
                }
                return returnData;
            }
        }

        public ClientInvoiceViewModel GetInvoiceByProjectIdNew(long projectId)
        {
            List<ClientInvoiceViewModel> data = new List<ClientInvoiceViewModel>();
            ClientInvoiceViewModel returnData = new ClientInvoiceViewModel();
            using (GLSPMContext db = new GLSPMContext())
            {
                var projectIdParameter = new SqlParameter { ParameterName = "ProjectId", Value = projectId };
                try
                {
                    data = db.Database.SqlQuery<ClientInvoiceViewModel>("GLS_GetInvoiceByProjectId_New @ProjectId", projectIdParameter).ToList<ClientInvoiceViewModel>();
                }
                catch (Exception ex) { }
                if (data == null)
                {
                    data = new List<ClientInvoiceViewModel>();
                }
                else
                {
                    returnData = data.FirstOrDefault();
                    List<ClientInvoiceDetail> invoiceDetails = new ClientInvoiceDetailService().GetClientInvoiceForManage(projectId);
                    returnData.ClientInvoiceDetails = invoiceDetails;
                }
                return returnData;
            }
        }
        #endregion
    }
}
