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
    public class AmendmentPCMCVService
    {
        #region Common Methods

        public List<AmendmentPCMCV> All()
        {
            List<AmendmentPCMCV> AmendmentPCMCV = new List<AmendmentPCMCV>();

            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentPCMCV = db.AmendmentPCMCVs.OrderBy(f => f.Id).AsParallel().ToList();

                if (AmendmentPCMCV == null)
                {
                    AmendmentPCMCV = new List<AmendmentPCMCV>();
                }
                return AmendmentPCMCV;
            }
        }

        public AmendmentPCMCV GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentPCMCV AmendmentPCMCV = db.AmendmentPCMCVs.SingleOrDefault(u => u.Id == userRoleId);
                return AmendmentPCMCV;
            }
        }
        public AmendmentPCMCV GetByProjectID(long projectId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AmendmentPCMCV AmendmentPCMCV = db.AmendmentPCMCVs.SingleOrDefault(u => u.ProjectId == projectId);
                return AmendmentPCMCV;
            }
        }
        public Response<AmendmentPCMCV> Add(AmendmentPCMCV AmendmentPCMCV)
        {
            Response<AmendmentPCMCV> addResponse = new Response<AmendmentPCMCV>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.AmendmentPCMCVs.Add(AmendmentPCMCV);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = AmendmentPCMCV;
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
                    addResponse.Result = AmendmentPCMCV;
                }
                return addResponse;
            }
        }

        public Response<AmendmentPCMCV> Update(AmendmentPCMCV AmendmentPCMCV)
        {
            Response<AmendmentPCMCV> updateResponse = new Response<AmendmentPCMCV>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    AmendmentPCMCV updateAmendmentPCMCV = db.AmendmentPCMCVs.SingleOrDefault(u => u.Id == AmendmentPCMCV.Id);
                    if (updateAmendmentPCMCV != null)
                    {
                        //updateAmendmentPCMCV = userRole;
                        updateAmendmentPCMCV.AmendmentPCMCVId = AmendmentPCMCV.AmendmentPCMCVId;
                        updateAmendmentPCMCV.Id = AmendmentPCMCV.Id;
                        updateAmendmentPCMCV.ProjectId = AmendmentPCMCV.ProjectId;
                        updateAmendmentPCMCV.OriginalValue = AmendmentPCMCV.OriginalValue;
                        updateAmendmentPCMCV.DuplicateValue = AmendmentPCMCV.DuplicateValue;
                        updateAmendmentPCMCV.TVValue = AmendmentPCMCV.TVValue;
                        updateAmendmentPCMCV.DifValue = AmendmentPCMCV.DifValue;
                        updateAmendmentPCMCV.QCResult = AmendmentPCMCV.QCResult;
                        updateAmendmentPCMCV.Amendment = AmendmentPCMCV.Amendment;
                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = AmendmentPCMCV;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = AmendmentPCMCV;
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
                    updateResponse.Result = AmendmentPCMCV;
                }
                return updateResponse;
            }
        }

        public Response<AmendmentPCMCV> Delete(long Id)
        {
            Response<AmendmentPCMCV> deleteResponse = new Response<AmendmentPCMCV>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    AmendmentPCMCV deleteAmendmentPCMCV = db.AmendmentPCMCVs.SingleOrDefault(u => u.Id == Id);
                    if (deleteAmendmentPCMCV != null)
                    {
                        db.AmendmentPCMCVs.Remove(deleteAmendmentPCMCV);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteAmendmentPCMCV;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteAmendmentPCMCV;
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
