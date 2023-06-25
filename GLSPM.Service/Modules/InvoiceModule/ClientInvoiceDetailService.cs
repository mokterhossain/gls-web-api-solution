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
    public class ClientInvoiceDetailService
    {
        #region Common Methods

        public List<ClientInvoiceDetail> All()
        {
            List<ClientInvoiceDetail> ClientInvoiceDetail = new List<ClientInvoiceDetail>();

            using (GLSPMContext db = new GLSPMContext())
            {
                ClientInvoiceDetail = db.ClientInvoiceDetails.OrderBy(f => f.Id).AsParallel().ToList();

                if (ClientInvoiceDetail == null)
                {
                    ClientInvoiceDetail = new List<ClientInvoiceDetail>();
                }
                return ClientInvoiceDetail;
            }
        }
        public List<ClientInvoiceDetail> GetByInvoiceId(long invoiceId)
        {
            List<ClientInvoiceDetail> ClientInvoiceDetail = new List<ClientInvoiceDetail>();

            using (GLSPMContext db = new GLSPMContext())
            {
                ClientInvoiceDetail = db.ClientInvoiceDetails.Where(c=>c.InvoiceId == invoiceId).OrderBy(f => f.Id).AsParallel().ToList();

                if (ClientInvoiceDetail == null)
                {
                    ClientInvoiceDetail = new List<ClientInvoiceDetail>();
                }
                return ClientInvoiceDetail;
            }
        }
        public ClientInvoiceDetail GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                ClientInvoiceDetail ClientInvoiceDetail = db.ClientInvoiceDetails.SingleOrDefault(u => u.Id == userRoleId);
                return ClientInvoiceDetail;
            }
        }
        public Response<ClientInvoiceDetail> Add(ClientInvoiceDetail ClientInvoiceDetail)
        {
            Response<ClientInvoiceDetail> addResponse = new Response<ClientInvoiceDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.ClientInvoiceDetails.Add(ClientInvoiceDetail);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = ClientInvoiceDetail;
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
                    addResponse.Result = ClientInvoiceDetail;
                }
                return addResponse;
            }
        }

        public Response<ClientInvoiceDetail> Update(ClientInvoiceDetail ClientInvoiceDetail)
        {
            Response<ClientInvoiceDetail> updateResponse = new Response<ClientInvoiceDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    ClientInvoiceDetail updateClientInvoiceDetail = db.ClientInvoiceDetails.SingleOrDefault(u => u.Id == ClientInvoiceDetail.Id);
                    if (updateClientInvoiceDetail != null)
                    {
                        //updateClientInvoiceDetail = userRole;
                        updateClientInvoiceDetail.Id = ClientInvoiceDetail.Id;
                        updateClientInvoiceDetail.InvoiceId = ClientInvoiceDetail.InvoiceId;
                        updateClientInvoiceDetail.ItemCode = ClientInvoiceDetail.ItemCode;
                        updateClientInvoiceDetail.SampleType = ClientInvoiceDetail.SampleType;
                        updateClientInvoiceDetail.Quantity = ClientInvoiceDetail.Quantity;
                        updateClientInvoiceDetail.UnitPrice = ClientInvoiceDetail.UnitPrice;
                        updateClientInvoiceDetail.TotalAmount = ClientInvoiceDetail.TotalAmount;
                        updateClientInvoiceDetail.Matrix = ClientInvoiceDetail.Matrix;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = ClientInvoiceDetail;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = ClientInvoiceDetail;
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
                    updateResponse.Result = ClientInvoiceDetail;
                }
                return updateResponse;
            }
        }

        public Response<ClientInvoiceDetail> Delete(long Id)
        {
            Response<ClientInvoiceDetail> deleteResponse = new Response<ClientInvoiceDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    ClientInvoiceDetail deleteClientInvoiceDetail = db.ClientInvoiceDetails.SingleOrDefault(u => u.Id == Id);
                    if (deleteClientInvoiceDetail != null)
                    {
                        db.ClientInvoiceDetails.Remove(deleteClientInvoiceDetail);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteClientInvoiceDetail;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteClientInvoiceDetail;
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
        public List<ClientInvoiceDetail> GetClientInvoiceForManage(long projectId)
        {
            List<ClientInvoiceDetail> projectViewModel = new List<ClientInvoiceDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var ProjectIdParameter = new SqlParameter { ParameterName = "ProjectId", Value = projectId };
                try
                {
                    projectViewModel = db.Database.SqlQuery<ClientInvoiceDetail>("GLS_GetClientInvoiceForManage @ProjectId", ProjectIdParameter).ToList<ClientInvoiceDetail>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<ClientInvoiceDetail>();
                }
                return projectViewModel;
            }
        }
        #endregion
    }
}
