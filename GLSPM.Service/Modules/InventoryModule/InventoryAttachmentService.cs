using GLSPM.Data;
using GLSPM.Data.Modules.InventoryModule;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Service.Modules.InventoryModule
{
    public class InventoryAttachmentService
    {
        #region Common Methods

        public List<InventoryAttachment> All()
        {
            List<InventoryAttachment> InventoryAttachment = new List<InventoryAttachment>();

            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryAttachment = db.InventoryAttachments.OrderBy(f => f.Id).AsParallel().ToList();

                if (InventoryAttachment == null)
                {
                    InventoryAttachment = new List<InventoryAttachment>();
                }
                return InventoryAttachment;
            }
        }
        public List<InventoryAttachment> AllByRefId(int refId)
        {
            List<InventoryAttachment> InventoryAttachment = new List<InventoryAttachment>();

            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryAttachment = db.InventoryAttachments.OrderBy(f => f.Id).AsParallel().ToList();

                if (InventoryAttachment == null)
                {
                    InventoryAttachment = new List<InventoryAttachment>();
                }
                else
                {
                    InventoryAttachment = InventoryAttachment.Where(i => i.ReferenceId == refId).ToList();
                }
                return InventoryAttachment;
            }
        }
        public List<InventoryAttachment> AllByRefAndAttachmentType(int refId, string attachmentType)
        {
            List<InventoryAttachment> InventoryAttachment = new List<InventoryAttachment>();

            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryAttachment = db.InventoryAttachments.OrderBy(f => f.Id).AsParallel().ToList();

                if (InventoryAttachment == null)
                {
                    InventoryAttachment = new List<InventoryAttachment>();
                }
                else
                {
                    InventoryAttachment = InventoryAttachment.Where(i => i.ReferenceId == refId && i.AttachmentType == attachmentType).ToList();
                }
                return InventoryAttachment;
            }
        }
        public InventoryAttachment GetByID(int userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                InventoryAttachment InventoryAttachment = db.InventoryAttachments.SingleOrDefault(u => u.Id == userRoleId);
                return InventoryAttachment;
            }
        }        
        public Response<InventoryAttachment> Add(InventoryAttachment InventoryAttachment)
        {
            Response<InventoryAttachment> addResponse = new Response<InventoryAttachment>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.InventoryAttachments.Add(InventoryAttachment);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = InventoryAttachment;
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
                    addResponse.Result = InventoryAttachment;
                }
                return addResponse;
            }
        }

        public Response<InventoryAttachment> Update(InventoryAttachment InventoryAttachment)
        {
            Response<InventoryAttachment> updateResponse = new Response<InventoryAttachment>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    InventoryAttachment updateInventoryAttachment = db.InventoryAttachments.SingleOrDefault(u => u.Id == InventoryAttachment.Id);
                    if (updateInventoryAttachment != null)
                    {
                        //updateInventoryAttachment = userRole;
                        updateInventoryAttachment.Id = InventoryAttachment.Id;
                        updateInventoryAttachment.ReferenceId = InventoryAttachment.ReferenceId;
                        updateInventoryAttachment.AttachmentType = InventoryAttachment.AttachmentType;
                        updateInventoryAttachment.FileName = InventoryAttachment.FileName;
                        updateInventoryAttachment.FileType = InventoryAttachment.FileType;
                        updateInventoryAttachment.FilePath = InventoryAttachment.FilePath;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = InventoryAttachment;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = InventoryAttachment;
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
                    updateResponse.Result = InventoryAttachment;
                }
                return updateResponse;
            }
        }

        public Response<InventoryAttachment> Delete(long Id)
        {
            Response<InventoryAttachment> deleteResponse = new Response<InventoryAttachment>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    InventoryAttachment deleteInventoryAttachment = db.InventoryAttachments.SingleOrDefault(u => u.Id == Id);
                    if (deleteInventoryAttachment != null)
                    {
                        db.InventoryAttachments.Remove(deleteInventoryAttachment);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteInventoryAttachment;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteInventoryAttachment;
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
