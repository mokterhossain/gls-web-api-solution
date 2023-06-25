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
    public class ClientContactPersonService
    {
        #region Common Methods

        public List<ClientContactPerson> All()
        {
            List<ClientContactPerson> ClientContactPerson = new List<ClientContactPerson>();

            using (GLSPMContext db = new GLSPMContext())
            {
                ClientContactPerson = db.ClientContactPersons.OrderBy(f => f.Id).AsParallel().ToList();

                if (ClientContactPerson == null)
                {
                    ClientContactPerson = new List<ClientContactPerson>();
                }
                return ClientContactPerson;
            }
        }
        public List<ClientContactPerson> GetByClientId(int clientId)
        {
            List<ClientContactPerson> ClientContactPerson = new List<ClientContactPerson>();

            using (GLSPMContext db = new GLSPMContext())
            {
                ClientContactPerson = db.ClientContactPersons.Where(c=> c.ClientId == clientId).OrderBy(f => f.Id).AsParallel().ToList();

                if (ClientContactPerson == null)
                {
                    ClientContactPerson = new List<ClientContactPerson>();
                }
                return ClientContactPerson;
            }
        }
        public ClientContactPerson GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                ClientContactPerson ClientContactPerson = db.ClientContactPersons.SingleOrDefault(u => u.Id == userRoleId);
                return ClientContactPerson;
            }
        }
        public ClientContactPerson GetByName(string name)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                ClientContactPerson ClientContactPerson = db.ClientContactPersons.SingleOrDefault(u => u.ContactPerson == name);
                return ClientContactPerson;
            }
        }
        public Response<ClientContactPerson> Add(ClientContactPerson ClientContactPerson)
        {
            Response<ClientContactPerson> addResponse = new Response<ClientContactPerson>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.ClientContactPersons.Add(ClientContactPerson);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = ClientContactPerson;
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
                    addResponse.Result = ClientContactPerson;
                }
                return addResponse;
            }
        }

        public Response<ClientContactPerson> Update(ClientContactPerson ClientContactPerson)
        {
            Response<ClientContactPerson> updateResponse = new Response<ClientContactPerson>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    ClientContactPerson updateClientContactPerson = db.ClientContactPersons.SingleOrDefault(u => u.Id == ClientContactPerson.Id);
                    if (updateClientContactPerson != null)
                    {
                        //updateClientContactPerson = userRole;
                        updateClientContactPerson.Id = ClientContactPerson.Id;
                        updateClientContactPerson.ContactPerson = ClientContactPerson.ContactPerson;
                        updateClientContactPerson.CellNo = ClientContactPerson.CellNo;
                        updateClientContactPerson.Email = ClientContactPerson.Email;
                        updateClientContactPerson.OfficePhone = ClientContactPerson.OfficePhone;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = ClientContactPerson;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = ClientContactPerson;
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
                    updateResponse.Result = ClientContactPerson;
                }
                return updateResponse;
            }
        }

        public Response<ClientContactPerson> Delete(long Id)
        {
            Response<ClientContactPerson> deleteResponse = new Response<ClientContactPerson>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    ClientContactPerson deleteClientContactPerson = db.ClientContactPersons.SingleOrDefault(u => u.Id == Id);
                    if (deleteClientContactPerson != null)
                    {
                        db.ClientContactPersons.Remove(deleteClientContactPerson);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteClientContactPerson;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteClientContactPerson;
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
