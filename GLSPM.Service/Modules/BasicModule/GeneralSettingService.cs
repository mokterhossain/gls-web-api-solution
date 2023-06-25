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
    public class GeneralSettingService
    {
        #region Common Methods

        public List<GeneralSetting> All()
        {
            List<GeneralSetting> GeneralSetting = new List<GeneralSetting>();

            using (GLSPMContext db = new GLSPMContext())
            {
                GeneralSetting = db.GeneralSettings.OrderBy(f => f.Id).AsParallel().ToList();

                if (GeneralSetting == null)
                {
                    GeneralSetting = new List<GeneralSetting>();
                }
                return GeneralSetting;
            }
        }

        public GeneralSetting GetByID(int userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                GeneralSetting GeneralSetting = db.GeneralSettings.SingleOrDefault(u => u.Id == userRoleId);
                return GeneralSetting;
            }
        }
        public Response<GeneralSetting> Add(GeneralSetting GeneralSetting)
        {
            Response<GeneralSetting> addResponse = new Response<GeneralSetting>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.GeneralSettings.Add(GeneralSetting);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = GeneralSetting;
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
                    addResponse.Result = GeneralSetting;
                }
                return addResponse;
            }
        }

        public Response<GeneralSetting> Update(GeneralSetting GeneralSetting)
        {
            Response<GeneralSetting> updateResponse = new Response<GeneralSetting>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    GeneralSetting updateGeneralSetting = db.GeneralSettings.SingleOrDefault(u => u.Id == GeneralSetting.Id);
                    if (updateGeneralSetting != null)
                    {
                        //updateGeneralSetting = userRole;
                        updateGeneralSetting.Id = GeneralSetting.Id;
                        updateGeneralSetting.QcAfterPercentagePLM = GeneralSetting.QcAfterPercentagePLM;
                        updateGeneralSetting.QcAfterPercentagePCM = GeneralSetting.QcAfterPercentagePCM;
                        updateGeneralSetting.QcAfterPercentageMold = GeneralSetting.QcAfterPercentageMold;
                        updateGeneralSetting.MicroscopeFieldDiameter = GeneralSetting.MicroscopeFieldDiameter;
                        updateGeneralSetting.TraverseNumber = GeneralSetting.TraverseNumber;
                        updateGeneralSetting.Volume = GeneralSetting.Volume;
                        updateGeneralSetting.FungalCount = GeneralSetting.FungalCount;
                        updateGeneralSetting.Limitofdetection = GeneralSetting.Limitofdetection;
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = GeneralSetting;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = GeneralSetting;
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
                    updateResponse.Result = GeneralSetting;
                }
                return updateResponse;
            }
        }

        public Response<GeneralSetting> Delete(long Id)
        {
            Response<GeneralSetting> deleteResponse = new Response<GeneralSetting>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    GeneralSetting deleteGeneralSetting = db.GeneralSettings.SingleOrDefault(u => u.Id == Id);
                    if (deleteGeneralSetting != null)
                    {
                        db.GeneralSettings.Remove(deleteGeneralSetting);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteGeneralSetting;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteGeneralSetting;
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
        #region DB Bakup
        public void MakeDbBackup()
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    //var dbNameParameter = new SqlParameter { ParameterName = "DbName", Value = dbName };

                    db.Database.ExecuteSqlCommand("GLS_MakeBackup");
                }
                catch (Exception ex) { }
            }
        }
        public void RestoreDbBackup()
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    //var dbNameParameter = new SqlParameter { ParameterName = "DbName", Value = dbName };

                    db.Database.ExecuteSqlCommand("GLS_RestoreDbBackup");
                }
                catch (Exception ex) { }
            }
        }
        #endregion
    }
}
