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
    public class ClientService
    {
        #region Common Methods

        public List<Client> All()
        {
            List<Client> Client = new List<Client>();

            using (GLSPMContext db = new GLSPMContext())
            {
                Client = db.Clients.OrderBy(f => f.ClientId).AsParallel().ToList();

                if (Client == null)
                {
                    Client = new List<Client>();
                }
                return Client;
            }
        }

        public Client GetByID(int userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                Client Client = db.Clients.SingleOrDefault(u => u.ClientId == userRoleId);
                return Client;
            }
        }
        public Client GetByClientId(long ClientId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                Client Client = db.Clients.Where(p => p.ClientId == ClientId).OrderByDescending(p => p.ClientId).FirstOrDefault();
                return Client;
            }
        }
        public Response<Client> Add(Client Client)
        {
            Response<Client> addResponse = new Response<Client>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.Clients.Add(Client);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = Client;
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
                    addResponse.Result = Client;
                }
                return addResponse;
            }
        }

        public Response<Client> Update(Client Client)
        {
            Response<Client> updateResponse = new Response<Client>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    Client updateClient = db.Clients.SingleOrDefault(u => u.ClientId == Client.ClientId);
                    if (updateClient != null)
                    {
                        //updateClient = userRole;
                        updateClient.ClientId = Client.ClientId;
                        updateClient.ClientName = Client.ClientName;
                        updateClient.CellNo = Client.CellNo;
                        updateClient.OfficePhone = Client.OfficePhone;
                        updateClient.Email = Client.Email;
                        updateClient.Address = Client.Address;
                        updateClient.CreatedOn = Client.CreatedOn;
                        updateClient.CreatedBy = Client.CreatedBy;
                        updateClient.UpdatedOn = Client.UpdatedOn;
                        updateClient.UpdatedBy = Client.UpdatedBy;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = Client;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = Client;
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
                    updateResponse.Result = Client;
                }
                return updateResponse;
            }
        }

        public Response<Client> Delete(long ClientId)
        {
            Response<Client> deleteResponse = new Response<Client>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    Client deleteClient = db.Clients.SingleOrDefault(u => u.ClientId == ClientId);
                    if (deleteClient != null)
                    {
                        db.Clients.Remove(deleteClient);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteClient;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteClient;
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
        public List<ClientViewModel> GetAllClient()
        {
            List<ClientViewModel> projectViewModel = new List<ClientViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    projectViewModel = db.Database.SqlQuery<ClientViewModel>("GLS_GetAllClient").ToList<ClientViewModel>();
                }
                catch (Exception ex) { }
                if (projectViewModel == null)
                {
                    projectViewModel = new List<ClientViewModel>();
                }
                return projectViewModel;
            }
        }
    }
}
