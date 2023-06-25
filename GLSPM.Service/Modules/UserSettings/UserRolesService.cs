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
    public class UserRolesService
    {
        #region Common Methods

        public List<UserRole> All()
        {
            List<UserRole> UserRole = new List<UserRole>();

            using (GLSPMContext db = new GLSPMContext())
            {
                UserRole = db.UserRoles.OrderBy(f => f.UserRoleID).AsParallel().ToList();

                if (UserRole == null)
                {
                    UserRole = new List<UserRole>();
                }
                return UserRole;
            }
        }

        public UserRole GetByUserRoleID(int userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                UserRole UserRole = db.UserRoles.SingleOrDefault(u => u.UserRoleID == userRoleId);
                return UserRole;
            }
        }

        public Response<UserRole> Add(UserRole UserRole)
        {
            Response<UserRole> addResponse = new Response<UserRole>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.UserRoles.Add(UserRole);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = UserRole;
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
                    addResponse.Result = UserRole;
                }
                return addResponse;
            }
        }

        public Response<UserRole> Update(UserRole userRole)
        {
            Response<UserRole> updateResponse = new Response<UserRole>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    UserRole updateUserRole = db.UserRoles.SingleOrDefault(u => u.UserRoleID == userRole.UserRoleID);
                    if (updateUserRole != null)
                    {
                        updateUserRole = userRole;
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = userRole;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = userRole;
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
                    updateResponse.Result = userRole;
                }
                return updateResponse;
            }
        }

        public Response<UserRole> Delete(int userRoleId)
        {
            Response<UserRole> deleteResponse = new Response<UserRole>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    UserRole deleteUserRole = db.UserRoles.SingleOrDefault(u => u.UserRoleID == userRoleId);
                    if (deleteUserRole != null)
                    {
                        db.UserRoles.Remove(deleteUserRole);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteUserRole;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteUserRole;
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
        public UserRole GetRoleByUserName(string userName)
        {
            List<UserRole> rolesMenu = new List<UserRole>();
            UserRole userRole = new UserRole();
            using (GLSPMContext db = new GLSPMContext())
            {
                var UserNameParameter = new SqlParameter { ParameterName = "UserName", Value = userName };
                try
                {
                    rolesMenu = db.Database.SqlQuery<UserRole>("GLS_Roles_GetRoleByUserName @UserName", UserNameParameter).ToList<UserRole>();
                }
                catch (Exception ex) { }
                if (rolesMenu != null)
                {
                    userRole = rolesMenu.FirstOrDefault();
                }
                return userRole;
            }
        }
        #endregion Common Methods
    }
}
