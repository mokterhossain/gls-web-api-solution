using GLSPM.Data;
using GLSPM.Data.Modules.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Service.Modules.ProjectManagement
{
    public class MoldSporeTypeService
    {
        #region Common Methods

        public List<MoldSporeType> All()
        {
            List<MoldSporeType> MoldSporeType = new List<MoldSporeType>();

            using (GLSPMContext db = new GLSPMContext())
            {
                MoldSporeType = db.MoldSporeTypes.OrderBy(f => f.SerialNo).AsParallel().ToList();

                if (MoldSporeType == null)
                {
                    MoldSporeType = new List<MoldSporeType>();
                }
                return MoldSporeType;
            }
        }

        public MoldSporeType GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                MoldSporeType MoldSporeType = db.MoldSporeTypes.SingleOrDefault(u => u.Id == userRoleId);
                return MoldSporeType;
            }
        }
        public Response<MoldSporeType> Add(MoldSporeType MoldSporeType)
        {
            Response<MoldSporeType> addResponse = new Response<MoldSporeType>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.MoldSporeTypes.Add(MoldSporeType);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = MoldSporeType;
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
                    addResponse.Result = MoldSporeType;
                }
                return addResponse;
            }
        }

        public Response<MoldSporeType> Update(MoldSporeType MoldSporeType)
        {
            Response<MoldSporeType> updateResponse = new Response<MoldSporeType>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    MoldSporeType updateMoldSporeType = db.MoldSporeTypes.SingleOrDefault(u => u.Id == MoldSporeType.Id);
                    if (updateMoldSporeType != null)
                    {
                        //updateMoldSporeType = userRole;
                        updateMoldSporeType.Id = MoldSporeType.Id;
                        updateMoldSporeType.Name = MoldSporeType.Name;
                        updateMoldSporeType.IsKeySpore = MoldSporeType.IsKeySpore;
                        updateMoldSporeType.IsAdditional = MoldSporeType.IsAdditional;
                        updateMoldSporeType.IsStringValue = MoldSporeType.IsStringValue;
                        updateMoldSporeType.IsMoldTapeLift = MoldSporeType.IsMoldTapeLift;
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = MoldSporeType;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = MoldSporeType;
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
                    updateResponse.Result = MoldSporeType;
                }
                return updateResponse;
            }
        }

        public Response<MoldSporeType> Delete(long Id)
        {
            Response<MoldSporeType> deleteResponse = new Response<MoldSporeType>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    MoldSporeType deleteMoldSporeType = db.MoldSporeTypes.SingleOrDefault(u => u.Id == Id);
                    if (deleteMoldSporeType != null)
                    {
                        db.MoldSporeTypes.Remove(deleteMoldSporeType);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteMoldSporeType;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteMoldSporeType;
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
