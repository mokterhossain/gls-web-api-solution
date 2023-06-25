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
    public class PCMCVService
    {
        #region Common Methods

        public List<PCMCV> All()
        {
            List<PCMCV> PCMCV = new List<PCMCV>();

            using (GLSPMContext db = new GLSPMContext())
            {
                PCMCV = db.PCMCVs.OrderBy(f => f.Id).AsParallel().ToList();

                if (PCMCV == null)
                {
                    PCMCV = new List<PCMCV>();
                }
                return PCMCV;
            }
        }

        public PCMCV GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                PCMCV PCMCV = db.PCMCVs.SingleOrDefault(u => u.Id == userRoleId);
                return PCMCV;
            }
        }
        public PCMCV GetByProjectID(long projectId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                PCMCV PCMCV = db.PCMCVs.SingleOrDefault(u => u.ProjectId == projectId);
                return PCMCV;
            }
        }
        public Response<PCMCV> Add(PCMCV PCMCV)
        {
            Response<PCMCV> addResponse = new Response<PCMCV>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.PCMCVs.Add(PCMCV);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = PCMCV;
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
                    addResponse.Result = PCMCV;
                }
                return addResponse;
            }
        }

        public Response<PCMCV> Update(PCMCV PCMCV)
        {
            Response<PCMCV> updateResponse = new Response<PCMCV>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    PCMCV updatePCMCV = db.PCMCVs.SingleOrDefault(u => u.Id == PCMCV.Id);
                    if (updatePCMCV != null)
                    {
                        //updatePCMCV = userRole;
                        updatePCMCV.Id = PCMCV.Id;
                        updatePCMCV.ProjectId = PCMCV.ProjectId;
                        updatePCMCV.OriginalValue = PCMCV.OriginalValue;
                        updatePCMCV.DuplicateValue = PCMCV.DuplicateValue;
                        updatePCMCV.TVValue = PCMCV.TVValue;
                        updatePCMCV.DifValue = PCMCV.DifValue;
                        updatePCMCV.QCResult = PCMCV.QCResult;
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = PCMCV;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = PCMCV;
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
                    updateResponse.Result = PCMCV;
                }
                return updateResponse;
            }
        }

        public Response<PCMCV> Delete(long Id)
        {
            Response<PCMCV> deleteResponse = new Response<PCMCV>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    PCMCV deletePCMCV = db.PCMCVs.SingleOrDefault(u => u.Id == Id);
                    if (deletePCMCV != null)
                    {
                        db.PCMCVs.Remove(deletePCMCV);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deletePCMCV;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deletePCMCV;
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
