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
    public class EmailAccountsService
    {
        #region Common Methods

        public List<EmailAccounts> All()
        {
            List<EmailAccounts> EmailAccounts = new List<EmailAccounts>();

            using (GLSPMContext db = new GLSPMContext())
            {
                EmailAccounts = db.EmailAccountss.OrderBy(f => f.EmailAccountId).AsParallel().ToList();

                if (EmailAccounts == null)
                {
                    EmailAccounts = new List<EmailAccounts>();
                }
                return EmailAccounts;
            }
        }
        public EmailAccounts GetEmailAccount()
        {
            List<EmailAccounts> EmailAccounts = new List<EmailAccounts>();
            EmailAccounts EmailAccount = new EmailAccounts();
            using (GLSPMContext db = new GLSPMContext())
            {
                EmailAccounts = db.EmailAccountss.OrderBy(f => f.EmailAccountId).AsParallel().ToList();

                if (EmailAccounts != null)
                {
                    EmailAccount = EmailAccounts.FirstOrDefault();
                }
                return EmailAccount;
            }
        }
        public EmailAccounts GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                EmailAccounts EmailAccounts = db.EmailAccountss.SingleOrDefault(u => u.EmailAccountId == userRoleId);
                return EmailAccounts;
            }
        }
        public Response<EmailAccounts> Add(EmailAccounts EmailAccounts)
        {
            Response<EmailAccounts> addResponse = new Response<EmailAccounts>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.EmailAccountss.Add(EmailAccounts);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = EmailAccounts;
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
                    addResponse.Result = EmailAccounts;
                }
                return addResponse;
            }
        }

        public Response<EmailAccounts> Update(EmailAccounts EmailAccounts)
        {
            Response<EmailAccounts> updateResponse = new Response<EmailAccounts>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    EmailAccounts updateEmailAccounts = db.EmailAccountss.SingleOrDefault(u => u.EmailAccountId == EmailAccounts.EmailAccountId);
                    if (updateEmailAccounts != null)
                    {
                        //updateEmailAccounts = userRole;
                        updateEmailAccounts.EmailAccountId = EmailAccounts.EmailAccountId;
                        updateEmailAccounts.Email = EmailAccounts.Email;
                        updateEmailAccounts.DisplayName = EmailAccounts.DisplayName;
                        updateEmailAccounts.Host = EmailAccounts.Host;
                        updateEmailAccounts.Port = EmailAccounts.Port;
                        updateEmailAccounts.Username = EmailAccounts.Username;
                        updateEmailAccounts.Password = EmailAccounts.Password;
                        updateEmailAccounts.EnableSSL = EmailAccounts.EnableSSL;
                        updateEmailAccounts.UseDefaultCredentials = EmailAccounts.UseDefaultCredentials;
                        updateEmailAccounts.IsActive = EmailAccounts.IsActive;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = EmailAccounts;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = EmailAccounts;
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
                    updateResponse.Result = EmailAccounts;
                }
                return updateResponse;
            }
        }

        public Response<EmailAccounts> Delete(long Id)
        {
            Response<EmailAccounts> deleteResponse = new Response<EmailAccounts>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    EmailAccounts deleteEmailAccounts = db.EmailAccountss.SingleOrDefault(u => u.EmailAccountId == Id);
                    if (deleteEmailAccounts != null)
                    {
                        db.EmailAccountss.Remove(deleteEmailAccounts);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteEmailAccounts;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteEmailAccounts;
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
