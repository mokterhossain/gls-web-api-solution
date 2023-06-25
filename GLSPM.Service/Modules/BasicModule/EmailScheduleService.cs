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
    public class EmailScheduleService
    {
        #region Common Methods

        public List<EmailSchedule> All()
        {
            List<EmailSchedule> EmailSchedule = new List<EmailSchedule>();

            using (GLSPMContext db = new GLSPMContext())
            {
                EmailSchedule = db.EmailSchedules.OrderBy(f => f.Id).AsParallel().ToList();

                if (EmailSchedule == null)
                {
                    EmailSchedule = new List<EmailSchedule>();
                }
                return EmailSchedule;
            }
        }

        public EmailSchedule GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                EmailSchedule EmailSchedule = db.EmailSchedules.SingleOrDefault(u => u.Id == userRoleId);
                return EmailSchedule;
            }
        }
        public Response<EmailSchedule> Add(EmailSchedule EmailSchedule)
        {
            Response<EmailSchedule> addResponse = new Response<EmailSchedule>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.EmailSchedules.Add(EmailSchedule);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = EmailSchedule;
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
                    addResponse.Result = EmailSchedule;
                }
                return addResponse;
            }
        }

        public Response<EmailSchedule> Update(EmailSchedule EmailSchedule)
        {
            Response<EmailSchedule> updateResponse = new Response<EmailSchedule>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    EmailSchedule updateEmailSchedule = db.EmailSchedules.SingleOrDefault(u => u.Id == EmailSchedule.Id);
                    if (updateEmailSchedule != null)
                    {
                        //updateEmailSchedule = userRole;
                        updateEmailSchedule.Id = EmailSchedule.Id;
                        updateEmailSchedule.EmailHistoryId = EmailSchedule.EmailHistoryId;
                        updateEmailSchedule.SendEmailBeforeTAT = EmailSchedule.SendEmailBeforeTAT;
                        updateEmailSchedule.SendAfterAfterHourFromNow = EmailSchedule.SendAfterAfterHourFromNow;
                        updateEmailSchedule.SpecificDate = EmailSchedule.SpecificDate;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = EmailSchedule;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = EmailSchedule;
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
                    updateResponse.Result = EmailSchedule;
                }
                return updateResponse;
            }
        }

        public Response<EmailSchedule> Delete(long Id)
        {
            Response<EmailSchedule> deleteResponse = new Response<EmailSchedule>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    EmailSchedule deleteEmailSchedule = db.EmailSchedules.SingleOrDefault(u => u.Id == Id);
                    if (deleteEmailSchedule != null)
                    {
                        db.EmailSchedules.Remove(deleteEmailSchedule);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteEmailSchedule;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteEmailSchedule;
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
        public List<EmailHistoryViewModel> GetScheduleEmailToSent()
        {
            List<EmailHistoryViewModel> emailSchedule = new List<EmailHistoryViewModel>();
            using (GLSPMContext db = new GLSPMContext())
            {
                //var projectIdParameter = new SqlParameter { ParameterName = "ProjectId", Value = projectId };
                try
                {
                    emailSchedule = db.Database.SqlQuery<EmailHistoryViewModel>("GLS_GetScheduleEmailToSent").ToList<EmailHistoryViewModel>();
                }
                catch (Exception ex) { }
                if (emailSchedule == null)
                {
                    emailSchedule = new List<EmailHistoryViewModel>();
                }
                return emailSchedule;
            }
        }
        #endregion Common Methods
    }
}
