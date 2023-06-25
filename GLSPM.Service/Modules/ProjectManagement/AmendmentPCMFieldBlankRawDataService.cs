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
    public class AmendmentPCMFieldBlankRawDataService
    {
        #region Common Methods

        public List<AmendmentPCMFieldBlankRawData> All()
        {
            List<AmendmentPCMFieldBlankRawData> AmendmentPCMFieldBlankRawData = new List<AmendmentPCMFieldBlankRawData>();

            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentPCMFieldBlankRawData = db.AmendmentPCMFieldBlankRawDatas.OrderBy(f => f.Id).AsParallel().ToList();

                if (AmendmentPCMFieldBlankRawData == null)
                {
                    AmendmentPCMFieldBlankRawData = new List<AmendmentPCMFieldBlankRawData>();
                }
                return AmendmentPCMFieldBlankRawData;
            }
        }
        public List<AmendmentPCMFieldBlankRawData> AllByProjectId(long projectId)
        {
            List<AmendmentPCMFieldBlankRawData> AmendmentPCMFieldBlankRawData = new List<AmendmentPCMFieldBlankRawData>();

            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentPCMFieldBlankRawData = db.AmendmentPCMFieldBlankRawDatas.OrderBy(f => f.Id).AsParallel().ToList();
                if (AmendmentPCMFieldBlankRawData != null)
                {
                    AmendmentPCMFieldBlankRawData = AmendmentPCMFieldBlankRawData.Where(p => p.ProjectId == projectId).ToList();
                }

                if (AmendmentPCMFieldBlankRawData == null)
                {
                    AmendmentPCMFieldBlankRawData = new List<AmendmentPCMFieldBlankRawData>();
                }
                return AmendmentPCMFieldBlankRawData;
            }
        }
        public AmendmentPCMFieldBlankRawData GetByProjectIDAndSerail(long projectId, string serial)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentPCMFieldBlankRawData AmendmentPCMFieldBlankRawData = db.AmendmentPCMFieldBlankRawDatas.SingleOrDefault(u => u.ProjectId == projectId && u.Serial == serial);
                return AmendmentPCMFieldBlankRawData;
            }
        }
        public AmendmentPCMFieldBlankRawData GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentPCMFieldBlankRawData AmendmentPCMFieldBlankRawData = db.AmendmentPCMFieldBlankRawDatas.SingleOrDefault(u => u.Id == userRoleId);
                return AmendmentPCMFieldBlankRawData;
            }
        }
        public Response<AmendmentPCMFieldBlankRawData> Add(AmendmentPCMFieldBlankRawData AmendmentPCMFieldBlankRawData)
        {
            Response<AmendmentPCMFieldBlankRawData> addResponse = new Response<AmendmentPCMFieldBlankRawData>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.AmendmentPCMFieldBlankRawDatas.Add(AmendmentPCMFieldBlankRawData);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = AmendmentPCMFieldBlankRawData;
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
                    addResponse.Result = AmendmentPCMFieldBlankRawData;
                }
                return addResponse;
            }
        }

        public Response<AmendmentPCMFieldBlankRawData> Update(AmendmentPCMFieldBlankRawData AmendmentPCMFieldBlankRawData)
        {
            Response<AmendmentPCMFieldBlankRawData> updateResponse = new Response<AmendmentPCMFieldBlankRawData>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    AmendmentPCMFieldBlankRawData updateAmendmentPCMFieldBlankRawData = db.AmendmentPCMFieldBlankRawDatas.SingleOrDefault(u => u.Id == AmendmentPCMFieldBlankRawData.Id);
                    if (updateAmendmentPCMFieldBlankRawData != null)
                    {
                        //updateAmendmentPCMFieldBlankRawData = userRole;
                        updateAmendmentPCMFieldBlankRawData.Id = AmendmentPCMFieldBlankRawData.Id;
                        updateAmendmentPCMFieldBlankRawData.Serial = AmendmentPCMFieldBlankRawData.Serial;
                        updateAmendmentPCMFieldBlankRawData.ProjectId = AmendmentPCMFieldBlankRawData.ProjectId;
                        updateAmendmentPCMFieldBlankRawData.BCSample = AmendmentPCMFieldBlankRawData.BCSample;
                        updateAmendmentPCMFieldBlankRawData.RawFibersCountHalf = AmendmentPCMFieldBlankRawData.RawFibersCountHalf;
                        updateAmendmentPCMFieldBlankRawData.RawFibersCountWhole = AmendmentPCMFieldBlankRawData.RawFibersCountWhole;
                        updateAmendmentPCMFieldBlankRawData.FiledsCounted = AmendmentPCMFieldBlankRawData.FiledsCounted;
                        updateAmendmentPCMFieldBlankRawData.CalculatedFibersCount = AmendmentPCMFieldBlankRawData.CalculatedFibersCount;
                        updateAmendmentPCMFieldBlankRawData.Amendment = AmendmentPCMFieldBlankRawData.Amendment;
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = AmendmentPCMFieldBlankRawData;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = AmendmentPCMFieldBlankRawData;
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
                    updateResponse.Result = AmendmentPCMFieldBlankRawData;
                }
                return updateResponse;
            }
        }

        public Response<AmendmentPCMFieldBlankRawData> Delete(long Id)
        {
            Response<AmendmentPCMFieldBlankRawData> deleteResponse = new Response<AmendmentPCMFieldBlankRawData>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    AmendmentPCMFieldBlankRawData deleteAmendmentPCMFieldBlankRawData = db.AmendmentPCMFieldBlankRawDatas.SingleOrDefault(u => u.Id == Id);
                    if (deleteAmendmentPCMFieldBlankRawData != null)
                    {
                        db.AmendmentPCMFieldBlankRawDatas.Remove(deleteAmendmentPCMFieldBlankRawData);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteAmendmentPCMFieldBlankRawData;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteAmendmentPCMFieldBlankRawData;
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
