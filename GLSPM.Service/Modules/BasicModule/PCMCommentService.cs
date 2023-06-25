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
    public class PCMCommentService
    {
        #region Common Methods

        public List<PCMComment> All()
        {
            List<PCMComment> PCMComment = new List<PCMComment>();

            using (GLSPMContext db = new GLSPMContext())
            {
                PCMComment = db.PCMComments.OrderBy(f => f.Id).AsParallel().ToList();

                if (PCMComment == null)
                {
                    PCMComment = new List<PCMComment>();
                }
                return PCMComment;
            }
        }

        public PCMComment GetByID(int userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                PCMComment PCMComment = db.PCMComments.SingleOrDefault(u => u.Id == userRoleId);
                return PCMComment;
            }
        }
        public Response<PCMComment> Add(PCMComment PCMComment)
        {
            Response<PCMComment> addResponse = new Response<PCMComment>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.PCMComments.Add(PCMComment);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = PCMComment;
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
                    addResponse.Result = PCMComment;
                }
                return addResponse;
            }
        }

        public Response<PCMComment> Update(PCMComment PCMComment)
        {
            Response<PCMComment> updateResponse = new Response<PCMComment>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    PCMComment updatePCMComment = db.PCMComments.SingleOrDefault(u => u.Id == PCMComment.Id);
                    if (updatePCMComment != null)
                    {
                        //updatePCMComment = userRole;
                        updatePCMComment.Id = PCMComment.Id;
                        updatePCMComment.Comment = PCMComment.Comment;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = PCMComment;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = PCMComment;
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
                    updateResponse.Result = PCMComment;
                }
                return updateResponse;
            }
        }

        public Response<PCMComment> Delete(long Id)
        {
            Response<PCMComment> deleteResponse = new Response<PCMComment>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    PCMComment deletePCMComment = db.PCMComments.SingleOrDefault(u => u.Id == Id);
                    if (deletePCMComment != null)
                    {
                        db.PCMComments.Remove(deletePCMComment);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deletePCMComment;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deletePCMComment;
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
