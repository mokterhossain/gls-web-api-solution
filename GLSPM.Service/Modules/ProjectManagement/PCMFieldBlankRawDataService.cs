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
    public class PCMFieldBlankRawDataService
    {
        #region Common Methods

        public List<PCMFieldBlankRawData> All()
        {
            List<PCMFieldBlankRawData> PCMFieldBlankRawData = new List<PCMFieldBlankRawData>();

            using (GLSPMContext db = new GLSPMContext())
            {
                PCMFieldBlankRawData = db.PCMFieldBlankRawDatas.OrderBy(f => f.Id).AsParallel().ToList();

                if (PCMFieldBlankRawData == null)
                {
                    PCMFieldBlankRawData = new List<PCMFieldBlankRawData>();
                }
                return PCMFieldBlankRawData;
            }
        }
        public List<PCMFieldBlankRawData> AllByProjectId(long projectId)
        {
            List<PCMFieldBlankRawData> PCMFieldBlankRawData = new List<PCMFieldBlankRawData>();

            using (GLSPMContext db = new GLSPMContext())
            {
                PCMFieldBlankRawData = db.PCMFieldBlankRawDatas.OrderBy(f => f.Id).AsParallel().ToList();
                if(PCMFieldBlankRawData != null)
                {
                    PCMFieldBlankRawData = PCMFieldBlankRawData.Where(p => p.ProjectId == projectId).ToList();
                }

                if (PCMFieldBlankRawData == null)
                {
                    PCMFieldBlankRawData = new List<PCMFieldBlankRawData>();
                }
                return PCMFieldBlankRawData;
            }
        }
        public PCMFieldBlankRawData GetByProjectIDAndSerail(long projectId, string serial)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                PCMFieldBlankRawData PCMFieldBlankRawData = db.PCMFieldBlankRawDatas.SingleOrDefault(u => u.ProjectId == projectId && u.Serial == serial);
                return PCMFieldBlankRawData;
            }
        }
        public PCMFieldBlankRawData GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                PCMFieldBlankRawData PCMFieldBlankRawData = db.PCMFieldBlankRawDatas.SingleOrDefault(u => u.Id == userRoleId);
                return PCMFieldBlankRawData;
            }
        }
        public Response<PCMFieldBlankRawData> Add(PCMFieldBlankRawData PCMFieldBlankRawData)
        {
            Response<PCMFieldBlankRawData> addResponse = new Response<PCMFieldBlankRawData>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.PCMFieldBlankRawDatas.Add(PCMFieldBlankRawData);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = PCMFieldBlankRawData;
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
                    addResponse.Result = PCMFieldBlankRawData;
                }
                return addResponse;
            }
        }

        public Response<PCMFieldBlankRawData> Update(PCMFieldBlankRawData PCMFieldBlankRawData)
        {
            Response<PCMFieldBlankRawData> updateResponse = new Response<PCMFieldBlankRawData>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    PCMFieldBlankRawData updatePCMFieldBlankRawData = db.PCMFieldBlankRawDatas.SingleOrDefault(u => u.Id == PCMFieldBlankRawData.Id);
                    if (updatePCMFieldBlankRawData != null)
                    {
                        //updatePCMFieldBlankRawData = userRole;
                        updatePCMFieldBlankRawData.Id = PCMFieldBlankRawData.Id;
                        updatePCMFieldBlankRawData.Serial = PCMFieldBlankRawData.Serial;
                        updatePCMFieldBlankRawData.ProjectId = PCMFieldBlankRawData.ProjectId;
                        updatePCMFieldBlankRawData.BCSample = PCMFieldBlankRawData.BCSample;
                        updatePCMFieldBlankRawData.RawFibersCountHalf = PCMFieldBlankRawData.RawFibersCountHalf;
                        updatePCMFieldBlankRawData.RawFibersCountWhole = PCMFieldBlankRawData.RawFibersCountWhole;
                        updatePCMFieldBlankRawData.FiledsCounted = PCMFieldBlankRawData.FiledsCounted;
                        updatePCMFieldBlankRawData.CalculatedFibersCount = PCMFieldBlankRawData.CalculatedFibersCount;
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = PCMFieldBlankRawData;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = PCMFieldBlankRawData;
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
                    updateResponse.Result = PCMFieldBlankRawData;
                }
                return updateResponse;
            }
        }

        public Response<PCMFieldBlankRawData> Delete(long Id)
        {
            Response<PCMFieldBlankRawData> deleteResponse = new Response<PCMFieldBlankRawData>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    PCMFieldBlankRawData deletePCMFieldBlankRawData = db.PCMFieldBlankRawDatas.SingleOrDefault(u => u.Id == Id);
                    if (deletePCMFieldBlankRawData != null)
                    {
                        db.PCMFieldBlankRawDatas.Remove(deletePCMFieldBlankRawData);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deletePCMFieldBlankRawData;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deletePCMFieldBlankRawData;
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
        public List<PCMFieldBlankRawData> GetPCMFieldBlankRawDataByProjectId(long projectId)
        {
            List<PCMFieldBlankRawData> data = new List<PCMFieldBlankRawData>();
            using (GLSPMContext db = new GLSPMContext())
            {
                var projectIdParameter = new SqlParameter { ParameterName = "@ProjectId", Value = projectId };
                try
                {
                    data = db.Database.SqlQuery<PCMFieldBlankRawData>("GLS_GetPCMFieldBlankRawDataByProjectId @ProjectId", projectIdParameter).ToList<PCMFieldBlankRawData>();
                }
                catch (Exception ex) { }
                if (data == null)
                {
                    data = new List<PCMFieldBlankRawData>();
                }
                return data;
            }
        }
    }
}
