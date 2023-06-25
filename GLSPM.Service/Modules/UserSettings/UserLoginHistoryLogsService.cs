using GLSPM.Data;
using GLSPM.Data.Modules.UserSettings;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Service.Modules.UserSettings
{
    public class UserLoginHistoryLogsService
    {
        public List<UserLoginHistoryLog> All()
        {
            List<UserLoginHistoryLog> userLoginHistoryLog = new List<UserLoginHistoryLog>();

            using (GLSPMContext db = new GLSPMContext())
            {
                userLoginHistoryLog = db.UserLoginHistoryLogs.OrderBy(f => f.UserLoginID).AsParallel().ToList();

                if (userLoginHistoryLog == null)
                {
                    userLoginHistoryLog = new List<UserLoginHistoryLog>();
                }
                return userLoginHistoryLog;
            }
        }

        public UserLoginHistoryLog GetByUserLoginHistoryLogID(Int64 userLoginHistoryLogId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                UserLoginHistoryLog userLoginHistoryLog = db.UserLoginHistoryLogs.SingleOrDefault(u => u.UserID == userLoginHistoryLogId);
                return userLoginHistoryLog;
            }
        }
        //public List<UserViewLoginHistory> GetUsersLoginHistory(bool isLoginHistory, bool isStudent)
        //{
        //    List<UserViewLoginHistory> userLoginHistoryLog = new List<UserViewLoginHistory>();
        //    using (GLSPMContext db = new GLSPMContext())
        //    {
        //        try
        //        {
        //            var isLoginHistoryParameter = new SqlParameter { ParameterName = "IsLoginHistory", Value = isLoginHistory };
        //            var isStudentParameter = new SqlParameter { ParameterName = "IsStudent", Value = isStudent };
        //            userLoginHistoryLog = db.Database.SqlQuery<UserViewLoginHistory>("BF_GetUsersLoginHistory @IsLoginHistory,@IsStudent", isLoginHistoryParameter, isStudentParameter).ToList();
        //        }
        //        catch (DbEntityValidationException ex) { }
        //        if (userLoginHistoryLog.Count == 0)
        //            userLoginHistoryLog = new List<UserViewLoginHistory>();
        //        return userLoginHistoryLog;
        //    }
        //}
        public Response<UserLoginHistoryLog> Add(UserLoginHistoryLog userLoginHistoryLog)
        {
            Response<UserLoginHistoryLog> addResponse = new Response<UserLoginHistoryLog>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.UserLoginHistoryLogs.Add(userLoginHistoryLog);

                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add success";
                        addResponse.IsSuccess = true;
                        addResponse.Result = userLoginHistoryLog;
                    }

                }
                catch (Exception ex)
                {
                    int errorNo = ((SqlException)ex.InnerException.InnerException).Number;
                    if (errorNo == 2601 || errorNo == 2627)
                    {
                        addResponse.Message = "This  UserLoginHistoryLog already exist.";
                    }
                    else
                    {
                        addResponse.Message = "There was an error while adding the  UserLoginHistoryLog: " + ex.Message;
                    }
                    addResponse.IsSuccess = false;
                    addResponse.Result = userLoginHistoryLog;
                }
                return addResponse;
            }
        }

        public Response<UserLoginHistoryLog> Update(UserLoginHistoryLog userLoginHistoryLog)
        {
            Response<UserLoginHistoryLog> updateResponse = new Response<UserLoginHistoryLog>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    UserLoginHistoryLog updateUserLoginHistoryLog = db.UserLoginHistoryLogs.SingleOrDefault(u => u.UserLoginID == userLoginHistoryLog.UserLoginID);
                    if (updateUserLoginHistoryLog != null)
                    {
                        updateUserLoginHistoryLog = userLoginHistoryLog;
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = userLoginHistoryLog;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = userLoginHistoryLog;
                            updateResponse.Message = "Error on update";
                        }
                    }
                    else
                    {
                        updateResponse.Result = null;
                        updateResponse.Message = " UserLoginHistoryLog not exist";
                    }
                }
                catch (Exception ex)
                {
                    int errorNo = ((SqlException)ex.InnerException.InnerException).Number;
                    if (errorNo == 2601 || errorNo == 2627)
                    {
                        updateResponse.Message = "This  UserLoginHistoryLog already exist.";
                    }
                    else
                    {
                        updateResponse.Message = "There was an error while adding the  UserLoginHistoryLog: " + ex.Message;
                    }
                    updateResponse.Result = userLoginHistoryLog;
                }

                return updateResponse;
            }
        }

        public Response<UserLoginHistoryLog> Delete(int userLoginHistoryLogID)
        {
            Response<UserLoginHistoryLog> deleteResponse = new Response<UserLoginHistoryLog>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    UserLoginHistoryLog deleteUserLoginHistoryLog = db.UserLoginHistoryLogs.SingleOrDefault(u => u.UserLoginID == userLoginHistoryLogID);
                    if (deleteUserLoginHistoryLog != null)
                    {
                        db.UserLoginHistoryLogs.Remove(deleteUserLoginHistoryLog);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteUserLoginHistoryLog;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = " UserLoginHistoryLog deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteUserLoginHistoryLog;
                            deleteResponse.Message = "Error on delete";
                        }
                    }
                    else
                    {
                        deleteResponse.Result = null;
                        deleteResponse.Message = " UserLoginHistoryLog not exist";
                    }
                }
                catch (Exception ex)
                {
                    int errorNo = ((SqlException)ex.InnerException.InnerException).Number;

                    if (errorNo == 547)
                    {
                        deleteResponse.Message = "This  UserLoginHistoryLog currently Used.";
                    }
                    else
                    {
                        deleteResponse.Message = "There was an error while deleting  UserLoginHistoryLog: " + ex.Message;
                    }
                    deleteResponse.Result = null;
                }

                return deleteResponse;
            }
        }
    }
}
