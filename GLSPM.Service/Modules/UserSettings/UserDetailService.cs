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
    public class UserDetailService
    {
        public List<UserDetail> All()
        {
            List<UserDetail> UserDetail = new List<UserDetail>();

            using (GLSPMContext db = new GLSPMContext())
            {
                UserDetail = db.UserDetails.OrderBy(f => f.UserDetailID).AsParallel().ToList();

                if (UserDetail == null)
                {
                    UserDetail = new List<UserDetail>();
                }
                return UserDetail;
            }
        }
        public UserDetail GetByUserDetailID(int userDetailId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                UserDetail UserDetail = db.UserDetails.SingleOrDefault(u => u.UserDetailID == userDetailId);
                return UserDetail;
            }
        }
        public UserDetail GetByUserID(long userId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                UserDetail UserDetail = db.UserDetails.SingleOrDefault(u => u.UserID == userId);
                return UserDetail;
            }
        }
        public Response<UserDetail> Add(UserDetail UserDetail)
        {
            Response<UserDetail> addResponse = new Response<UserDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {

                db.UserDetails.Add(UserDetail);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add success";
                        addResponse.IsSuccess = true;
                        addResponse.Result = UserDetail;
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
                    addResponse.Result = UserDetail;
                }
                return addResponse;
            }
        }

        public Response<UserDetail> Update(UserDetail UserDetail)
        {
            Response<UserDetail> updateResponse = new Response<UserDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    UserDetail updateUserDetail = db.UserDetails.SingleOrDefault(u => u.UserDetailID == UserDetail.UserDetailID);
                    if (updateUserDetail != null)
                    {
                        //updateUserDetail = UserDetail;
                        updateUserDetail.UserDetailID = UserDetail.UserDetailID;
                        updateUserDetail.UserID = UserDetail.UserID;
                        updateUserDetail.UserGuid = UserDetail.UserGuid;
                        updateUserDetail.FirstName = UserDetail.FirstName;
                        updateUserDetail.LastName = UserDetail.LastName;
                        updateUserDetail.Mobile = UserDetail.Mobile;
                        updateUserDetail.Phone = UserDetail.Phone;
                        updateUserDetail.Email = UserDetail.Email;
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = UserDetail;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = UserDetail;
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
                    updateResponse.Result = UserDetail;
                }

                return updateResponse;
            }
        }

        public Response<UserDetail> Delete(long UserDetailId)
        {
            Response<UserDetail> deleteResponse = new Response<UserDetail>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    UserDetail deleteUserDetail = db.UserDetails.SingleOrDefault(u => u.UserDetailID == UserDetailId);
                    if (deleteUserDetail != null)
                    {
                        //deleteSystemManu.RecordStatus = false;
                        db.UserDetails.Remove(deleteUserDetail);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteUserDetail;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "SchoolDB Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteUserDetail;
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
