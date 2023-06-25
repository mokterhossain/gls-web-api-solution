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
    public class UserService
    {
        #region common

        public List<User> All()
        {
            List<User> User = new List<User>();

            using (GLSPMContext db = new GLSPMContext())
            {
                User = db.Users.OrderBy(f => f.UserID).AsParallel().ToList();

                if (User == null)
                {
                    User = new List<User>();
                }
                return User;
            }
        }

        public User GetByUserID(long userId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                User User = db.Users.SingleOrDefault(u => u.UserID == userId);
                return User;
            }
        }

        public Response<User> Add(User User)
        {
            //userId = 0;
            Response<User> addResponse = new Response<User>();
            using (GLSPMContext db = new GLSPMContext())
            {

                db.Users.Add(User);
                try
                {
                    int checkSuccess = 0;

                    if (User.UserName.Trim().ToLower().Equals("bfsystemadmin") || User.UserName.Trim().ToLower().Equals("superadmin"))
                    {
                        addResponse.Message = "System is unable to create user by this user name.";
                        addResponse.IsSuccess = false;
                        addResponse.Result = User;
                        return addResponse;
                    }

                    checkSuccess = db.SaveChanges();
                    //userId = User.UserID;

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add success";
                        addResponse.IsSuccess = true;
                        addResponse.Result = User;
                    }

                }
                catch (Exception ex)
                {
                    int errorNo = ((SqlException)ex.InnerException.InnerException).Number;
                    if (errorNo == 2601 || errorNo == 2627)
                    {
                        addResponse.Message = "This User Info already exist.";
                    }
                    else
                    {
                        addResponse.Message = "There was an error while adding the User Info: " + ex.Message;
                    }
                    addResponse.IsSuccess = false;
                    addResponse.Result = User;
                }
                return addResponse;
            }
        }

        public Response<User> Update(User user)
        {
            Response<User> updateResponse = new Response<User>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    User updateUser = db.Users.SingleOrDefault(u => u.UserID == user.UserID);
                    if (updateUser != null)
                    {
                        //updateUser = User;
                        updateUser.UserID = user.UserID;
                        updateUser.UserName = user.UserName;
                        updateUser.Password = user.Password;
                        updateUser.IsActive = user.IsActive;
                        updateUser.UpdatedBy = user.UpdatedBy;
                        updateUser.UpdatedOn = DateTime.Now;
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = user;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = user;
                            updateResponse.Message = "Error on update";
                        }
                    }
                    else
                    {
                        updateResponse.Result = null;
                        updateResponse.Message = "User Info not exist";
                    }
                }
                catch (Exception ex)
                {
                    int errorNo = ((SqlException)ex.InnerException.InnerException).Number;
                    if (errorNo == 2601 || errorNo == 2627)
                    {
                        updateResponse.Message = "This User Info already exist.";
                    }
                    else
                    {
                        updateResponse.Message = "There was an error while adding the User Info: " + ex.Message;
                    }
                    updateResponse.Result = user;
                }

                return updateResponse;
            }
        }

        public Response<User> Delete(long UserId)
        {
            Response<User> deleteResponse = new Response<User>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    User deleteUser = db.Users.SingleOrDefault(u => u.UserID == UserId);
                    if (deleteUser != null)
                    {
                        //deleteSystemManu.RecordStatus = false;
                        db.Users.Remove(deleteUser);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteUser;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "User Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteUser;
                            deleteResponse.Message = "Error on delete";
                        }
                    }
                    else
                    {
                        deleteResponse.Result = null;
                        deleteResponse.Message = "User Info not exist";
                    }
                }
                catch (Exception ex)
                {
                    int errorNo = ((SqlException)ex.InnerException.InnerException).Number;

                    if (errorNo == 547)
                    {
                        deleteResponse.Message = "This User Info currently Used.";
                    }
                    else
                    {
                        deleteResponse.Message = "There was an error while deleting User Info: " + ex.Message;
                    }
                    deleteResponse.Result = null;
                }

                return deleteResponse;
            }
        }

        #endregion common

        #region Others

        public bool ValidateUser(string userName, string password)
        {
            bool result = false;
            List<User> userList = new List<User>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    userList = db.Users.Where(u => u.UserName == userName && u.Password == password).ToList();
                    if (userList == null)
                        result = false;
                    else
                    {
                        if (userList.Count > 0)
                        {
                            if (userList[0].IsActive == false)
                            {
                                result = false;
                            }
                            else
                                result = true;
                        }
                        else
                            result = false;
                    }
                }
                catch (Exception ex)
                {

                }
                return result;
            }
        }       
        public User GetByUserName(string userName)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                //User User = db.Users.Where(u => u.UserName == userName && u.UserID == userid).FirstOrDefault();
                User User = db.Users.FirstOrDefault(u => u.UserName == userName);
                return User;
            }
        }

        public User GetByUserNameUpdate(string userName, Int64 userid)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                User User = db.Users.Where(u => u.UserName == userName && u.UserID != userid).FirstOrDefault();
                //User User = db.Users.FirstOrDefault(u => u.UserName == userName && u.UserID != userid);
                return User;
            }
        }
        public UserWithRole GetUserByNameAndPassword(string userName, string password)
        {
            UserWithRole user = new UserWithRole();
            List<UserWithRole> userList = new List<UserWithRole>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    var userNameParameter = new SqlParameter { ParameterName = "UserName", Value = userName };
                    var passwordParameter = new SqlParameter { ParameterName = "Password", Value = password };

                    try
                    {
                        userList = db.Database.SqlQuery<UserWithRole>("GetUserByNameAndPassword @UserName,@Password ", userNameParameter, passwordParameter).ToList<UserWithRole>();
                    }
                    catch (Exception ex) { user = null; }
                    if (userList != null)
                    {
                        user = userList.SingleOrDefault();
                    }
                    else
                    {
                        user = null;
                    }
                }
                catch (Exception ex) { }
            }
            return user;
        }
        public List<UserCustom> GetAllUserWithDetails(int userRoleID, string userName, string email, string mobile, int stratRowNumber, int endRowNumber, out int totalRecordCount)
        {
            List<UserCustom> userList = new List<UserCustom>();
            totalRecordCount = 0;
            using (GLSPMContext db = new GLSPMContext())
            {
                var userRoleIDParameter = new SqlParameter { ParameterName = "UserRoleID", Value = userRoleID };
                var userNameParameter = new SqlParameter { ParameterName = "UserName", Value = userName };
                var emailParameter = new SqlParameter { ParameterName = "Email", Value = email };
                var mobileParameter = new SqlParameter { ParameterName = "Mobile", Value = mobile };
                var stratRowNumberParameter = new SqlParameter { ParameterName = "StratRowNumber", Value = stratRowNumber };
                var endRowNumberParameter = new SqlParameter { ParameterName = "EndRowNumber", Value = endRowNumber };
                var totalRecordCountParameter = new SqlParameter { ParameterName = "TotalRecordCount", Value = totalRecordCount, Direction = System.Data.ParameterDirection.Output };

                try
                {
                    userList = db.Database.SqlQuery<UserCustom>("GLS_GetAllUserWithDetails @UserRoleID,@UserName,@Email,@Mobile,@StratRowNumber,@EndRowNumber,@TotalRecordCount OUTPUT", userRoleIDParameter, userNameParameter, emailParameter, mobileParameter, stratRowNumberParameter, endRowNumberParameter, totalRecordCountParameter).ToList<UserCustom>();
                    totalRecordCount = (totalRecordCountParameter.Value == null) ? 0 : Convert.ToInt32(totalRecordCountParameter.Value);
                }
                catch (Exception ex) { userList = null; }
                if (userList == null)
                {
                    userList = new List<UserCustom>();
                }
                //var total = (int)totalRecordCountParameter.Value;
                //totalRecordCount = (int)total;
                return userList;
            }
        }
        #endregion Others
    }
}
