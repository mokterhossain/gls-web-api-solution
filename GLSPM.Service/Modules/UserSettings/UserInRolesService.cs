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
    public class UserInRolesService
    {
        public List<UserInRole> All()
        {
            List<UserInRole> UserInRole = new List<UserInRole>();

            using (GLSPMContext db = new GLSPMContext())
            {
                UserInRole = db.UserInRoles.OrderBy(f => f.UserInRoleID).AsParallel().ToList();

                if (UserInRole == null)
                {
                    UserInRole = new List<UserInRole>();
                }
                return UserInRole;
            }
        }
        public UserInRole GetByUserInRoleID(int userInRoleID)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                UserInRole UserInRole = db.UserInRoles.SingleOrDefault(u => u.UserInRoleID == userInRoleID);
                return UserInRole;
            }
        }
        public UserInRole GetByUserID(long userID)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                UserInRole UserInRole = db.UserInRoles.SingleOrDefault(u => u.UserID == userID);
                return UserInRole;
            }
        }
        public Response<UserInRole> Add(UserInRole UserInRole)
        {
            Response<UserInRole> addResponse = new Response<UserInRole>();
            using (GLSPMContext db = new GLSPMContext())
            {

                db.UserInRoles.Add(UserInRole);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add success";
                        addResponse.IsSuccess = true;
                        addResponse.Result = UserInRole;
                    }

                }
                catch (Exception ex)
                {
                    int errorNo = ((SqlException)ex.InnerException.InnerException).Number;
                    if (errorNo == 2601 || errorNo == 2627)
                    {
                        addResponse.Message = "This SchoolDB Info already exist.";
                    }
                    else
                    {
                        addResponse.Message = "There was an error while adding the SchoolDB Info: " + ex.Message;
                    }
                    addResponse.IsSuccess = false;
                    addResponse.Result = UserInRole;
                }
                return addResponse;
            }
        }

        public Response<UserInRole> Update(UserInRole UserInRole)
        {
            Response<UserInRole> updateResponse = new Response<UserInRole>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    UserInRole updateUserInRole = db.UserInRoles.SingleOrDefault(u => u.UserInRoleID == UserInRole.UserInRoleID);
                    if (updateUserInRole != null)
                    {
                        //updateUserInRole = UserInRole;
                        updateUserInRole.UserInRoleID = UserInRole.UserInRoleID;
                        updateUserInRole.UserID = UserInRole.UserID;
                        updateUserInRole.UserGuid = updateUserInRole.UserGuid;
                        updateUserInRole.UserRoleID = UserInRole.UserRoleID;
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = UserInRole;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = UserInRole;
                            updateResponse.Message = "Error on update";
                        }
                    }
                    else
                    {
                        updateResponse.Result = null;
                        updateResponse.Message = "SchoolDB Info not exist";
                    }
                }
                catch (Exception ex)
                {
                    int errorNo = ((SqlException)ex.InnerException.InnerException).Number;
                    if (errorNo == 2601 || errorNo == 2627)
                    {
                        updateResponse.Message = "This SchoolDB Info already exist.";
                    }
                    else
                    {
                        updateResponse.Message = "There was an error while adding the SchoolDB Info: " + ex.Message;
                    }
                    updateResponse.Result = UserInRole;
                }

                return updateResponse;
            }
        }

        public Response<UserInRole> Delete(long UserInRoleId)
        {
            Response<UserInRole> deleteResponse = new Response<UserInRole>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    UserInRole deleteUserInRole = db.UserInRoles.SingleOrDefault(u => u.UserInRoleID == UserInRoleId);
                    if (deleteUserInRole != null)
                    {
                        //deleteSystemManu.RecordStatus = false;
                        db.UserInRoles.Remove(deleteUserInRole);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteUserInRole;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "SchoolDB Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteUserInRole;
                            deleteResponse.Message = "Error on delete";
                        }
                    }
                    else
                    {
                        deleteResponse.Result = null;
                        deleteResponse.Message = "SchoolDB Info not exist";
                    }
                }
                catch (Exception ex)
                {
                    int errorNo = ((SqlException)ex.InnerException.InnerException).Number;

                    if (errorNo == 547)
                    {
                        deleteResponse.Message = "This SchoolDB Info currently Used.";
                    }
                    else
                    {
                        deleteResponse.Message = "There was an error while deleting SchoolDB Info: " + ex.Message;
                    }
                    deleteResponse.Result = null;
                }

                return deleteResponse;
            }
        }
    }
}
