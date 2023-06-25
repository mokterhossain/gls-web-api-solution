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
    public class EmailAttachmentService
    {
        #region Common Methods

        public List<EmailAttachment> All()
        {
            List<EmailAttachment> EmailAttachment = new List<EmailAttachment>();

            using (GLSPMContext db = new GLSPMContext())
            {
                EmailAttachment = db.EmailAttachments.OrderBy(f => f.Id).AsParallel().ToList();

                if (EmailAttachment == null)
                {
                    EmailAttachment = new List<EmailAttachment>();
                }
                return EmailAttachment;
            }
        }
        public List<EmailAttachment> GetAllByEmailHistoryId(long historyId)
        {
            List<EmailAttachment> EmailAttachment = new List<EmailAttachment>();

            using (GLSPMContext db = new GLSPMContext())
            {
                EmailAttachment = db.EmailAttachments.Where(h=>h.EmailHistoryId == historyId).OrderBy(f => f.Id).AsParallel().ToList();

                if (EmailAttachment == null)
                {
                    EmailAttachment = new List<EmailAttachment>();
                }
                return EmailAttachment;
            }
        }

        public EmailAttachment GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                EmailAttachment EmailAttachment = db.EmailAttachments.SingleOrDefault(u => u.Id == userRoleId);
                return EmailAttachment;
            }
        }
        public Response<EmailAttachment> Add(EmailAttachment EmailAttachment)
        {
            Response<EmailAttachment> addResponse = new Response<EmailAttachment>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.EmailAttachments.Add(EmailAttachment);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = EmailAttachment;
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
                    addResponse.Result = EmailAttachment;
                }
                return addResponse;
            }
        }

        public Response<EmailAttachment> Update(EmailAttachment EmailAttachment)
        {
            Response<EmailAttachment> updateResponse = new Response<EmailAttachment>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    EmailAttachment updateEmailAttachment = db.EmailAttachments.SingleOrDefault(u => u.Id == EmailAttachment.Id);
                    if (updateEmailAttachment != null)
                    {
                        //updateEmailAttachment = userRole;
                        updateEmailAttachment.Id = EmailAttachment.Id;
                        updateEmailAttachment.EmailHistoryId = EmailAttachment.EmailHistoryId;
                        updateEmailAttachment.AttachmentPath = EmailAttachment.AttachmentPath;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = EmailAttachment;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = EmailAttachment;
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
                    updateResponse.Result = EmailAttachment;
                }
                return updateResponse;
            }
        }

        public Response<EmailAttachment> Delete(long Id)
        {
            Response<EmailAttachment> deleteResponse = new Response<EmailAttachment>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    EmailAttachment deleteEmailAttachment = db.EmailAttachments.SingleOrDefault(u => u.Id == Id);
                    if (deleteEmailAttachment != null)
                    {
                        db.EmailAttachments.Remove(deleteEmailAttachment);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteEmailAttachment;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteEmailAttachment;
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
