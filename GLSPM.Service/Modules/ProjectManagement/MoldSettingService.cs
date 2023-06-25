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
    public class MoldSettingService
    {
        #region Common Methods

        public List<MoldSetting> All()
        {
            List<MoldSetting> MoldSetting = new List<MoldSetting>();

            using (GLSPMContext db = new GLSPMContext())
            {
                MoldSetting = db.MoldSettings.OrderBy(f => f.Id).AsParallel().ToList();

                if (MoldSetting == null)
                {
                    MoldSetting = new List<MoldSetting>();
                }
                return MoldSetting;
            }
        }

        public MoldSetting GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                MoldSetting MoldSetting = db.MoldSettings.SingleOrDefault(u => u.Id == userRoleId);
                return MoldSetting;
            }
        }
        public Response<MoldSetting> Add(MoldSetting MoldSetting)
        {
            Response<MoldSetting> addResponse = new Response<MoldSetting>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.MoldSettings.Add(MoldSetting);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = MoldSetting;
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
                    addResponse.Result = MoldSetting;
                }
                return addResponse;
            }
        }

        public Response<MoldSetting> Update(MoldSetting MoldSetting)
        {
            Response<MoldSetting> updateResponse = new Response<MoldSetting>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    MoldSetting updateMoldSetting = db.MoldSettings.SingleOrDefault(u => u.Id == MoldSetting.Id);
                    if (updateMoldSetting != null)
                    {
                        //updateMoldSetting = userRole;
                        updateMoldSetting.Id = MoldSetting.Id;
                        updateMoldSetting.MicroscopeFieldDiameter = MoldSetting.MicroscopeFieldDiameter;
                        updateMoldSetting.TraverseNumber = MoldSetting.TraverseNumber;
                        updateMoldSetting.Volume = MoldSetting.Volume;
                        updateMoldSetting.FungalCount = MoldSetting.FungalCount;
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = MoldSetting;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = MoldSetting;
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
                    updateResponse.Result = MoldSetting;
                }
                return updateResponse;
            }
        }

        public Response<MoldSetting> Delete(long Id)
        {
            Response<MoldSetting> deleteResponse = new Response<MoldSetting>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    MoldSetting deleteMoldSetting = db.MoldSettings.SingleOrDefault(u => u.Id == Id);
                    if (deleteMoldSetting != null)
                    {
                        db.MoldSettings.Remove(deleteMoldSetting);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteMoldSetting;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteMoldSetting;
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
