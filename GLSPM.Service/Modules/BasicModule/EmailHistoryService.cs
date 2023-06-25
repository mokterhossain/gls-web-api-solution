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
    public class EmailHistoryService
    {
        #region Common Methods

        public List<EmailHistory> All()
        {
            List<EmailHistory> EmailHistory = new List<EmailHistory>();

            using (GLSPMContext db = new GLSPMContext())
            {
                EmailHistory = db.EmailHistorys.OrderBy(f => f.Id).AsParallel().ToList();

                if (EmailHistory == null)
                {
                    EmailHistory = new List<EmailHistory>();
                }
                return EmailHistory;
            }
        }

        public EmailHistory GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                EmailHistory EmailHistory = db.EmailHistorys.SingleOrDefault(u => u.Id == userRoleId);
                return EmailHistory;
            }
        }
        public Response<EmailHistory> Add(EmailHistory EmailHistory)
        {
            Response<EmailHistory> addResponse = new Response<EmailHistory>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.EmailHistorys.Add(EmailHistory);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = EmailHistory;
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
                    addResponse.Result = EmailHistory;
                }
                return addResponse;
            }
        }

        public Response<EmailHistory> Update(EmailHistory EmailHistory)
        {
            Response<EmailHistory> updateResponse = new Response<EmailHistory>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    EmailHistory updateEmailHistory = db.EmailHistorys.SingleOrDefault(u => u.Id == EmailHistory.Id);
                    if (updateEmailHistory != null)
                    {
                        //updateEmailHistory = userRole;
                        updateEmailHistory.Id = EmailHistory.Id;
                        updateEmailHistory.ProjectId = EmailHistory.ProjectId;
                        updateEmailHistory.EmailTo = EmailHistory.EmailTo;
                        updateEmailHistory.EmailCC = EmailHistory.EmailCC;
                        updateEmailHistory.EmailBCC = EmailHistory.EmailBCC;
                        updateEmailHistory.EmailBody = EmailHistory.EmailBody;
                        updateEmailHistory.IsSent = EmailHistory.IsSent;
                        updateEmailHistory.SentDate = EmailHistory.SentDate;
                        updateEmailHistory.SentBy = EmailHistory.SentBy;
                        updateEmailHistory.IsSchedule = EmailHistory.IsSchedule;
                        updateEmailHistory.CreatedOn = EmailHistory.CreatedOn;
                        updateEmailHistory.CreatedBy = EmailHistory.CreatedBy;
                        updateEmailHistory.UpdatedOn = EmailHistory.UpdatedOn;
                        updateEmailHistory.UpdatedBy = EmailHistory.UpdatedBy;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = EmailHistory;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = EmailHistory;
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
                    updateResponse.Result = EmailHistory;
                }
                return updateResponse;
            }
        }

        public Response<EmailHistory> Delete(long Id)
        {
            Response<EmailHistory> deleteResponse = new Response<EmailHistory>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    EmailHistory deleteEmailHistory = db.EmailHistorys.SingleOrDefault(u => u.Id == Id);
                    if (deleteEmailHistory != null)
                    {
                        db.EmailHistorys.Remove(deleteEmailHistory);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteEmailHistory;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteEmailHistory;
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
        public List<EmailListForAutoComplete> GetEmailListForAutoComplete()
        {
            List<EmailListForAutoComplete> emailSchedule = new List<EmailListForAutoComplete>();
            using (GLSPMContext db = new GLSPMContext())
            {
                //var projectIdParameter = new SqlParameter { ParameterName = "ProjectId", Value = projectId };
                try
                {
                    emailSchedule = db.Database.SqlQuery<EmailListForAutoComplete>("GLS_GetEmailListForAutoComplete").ToList<EmailListForAutoComplete>();
                }
                catch (Exception ex) { }
                if (emailSchedule == null)
                {
                    emailSchedule = new List<EmailListForAutoComplete>();
                }
                return emailSchedule;
            }
        }
        #endregion Common Methods
    }
}
