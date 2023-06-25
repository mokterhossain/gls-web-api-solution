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
    public class AttachmentLibraryService
    {
        #region Common Methods

        public List<AttachmentLibrary> All()
        {
            List<AttachmentLibrary> AttachmentLibrary = new List<AttachmentLibrary>();

            using (GLSPMContext db = new GLSPMContext())
            {
                AttachmentLibrary = db.AttachmentLibrarys.OrderBy(f => f.Id).AsParallel().ToList();

                if (AttachmentLibrary == null)
                {
                    AttachmentLibrary = new List<AttachmentLibrary>();
                }
                return AttachmentLibrary;
            }
        }
        public List<AttachmentLibrary> AllByRefId(int refId)
        {
            List<AttachmentLibrary> AttachmentLibrary = new List<AttachmentLibrary>();

            using (GLSPMContext db = new GLSPMContext())
            {
                AttachmentLibrary = db.AttachmentLibrarys.OrderBy(f => f.Id).AsParallel().ToList();

                if (AttachmentLibrary == null)
                {
                    AttachmentLibrary = new List<AttachmentLibrary>();
                }
                else
                {
                    AttachmentLibrary = AttachmentLibrary.Where(i => i.ReferenceId == refId).ToList();
                }
                return AttachmentLibrary;
            }
        }
        public List<AttachmentLibrary> AllByRefAndAttachmentType(int refId, string attachmentType)
        {
            List<AttachmentLibrary> AttachmentLibrary = new List<AttachmentLibrary>();

            using (GLSPMContext db = new GLSPMContext())
            {
                AttachmentLibrary = db.AttachmentLibrarys.OrderBy(f => f.Id).AsParallel().ToList();

                if (AttachmentLibrary == null)
                {
                    AttachmentLibrary = new List<AttachmentLibrary>();
                }
                else
                {
                    AttachmentLibrary = AttachmentLibrary.Where(i => i.ReferenceId == refId && i.AttachmentType == attachmentType).ToList();
                }
                return AttachmentLibrary;
            }
        }
        public AttachmentLibrary GetByID(long userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                AttachmentLibrary AttachmentLibrary = db.AttachmentLibrarys.SingleOrDefault(u => u.Id == userRoleId);
                return AttachmentLibrary;
            }
        }
        public Response<AttachmentLibrary> Add(AttachmentLibrary AttachmentLibrary)
        {
            Response<AttachmentLibrary> addResponse = new Response<AttachmentLibrary>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.AttachmentLibrarys.Add(AttachmentLibrary);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = AttachmentLibrary;
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
                    addResponse.Result = AttachmentLibrary;
                }
                return addResponse;
            }
        }

        public Response<AttachmentLibrary> Update(AttachmentLibrary AttachmentLibrary)
        {
            Response<AttachmentLibrary> updateResponse = new Response<AttachmentLibrary>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    AttachmentLibrary updateAttachmentLibrary = db.AttachmentLibrarys.SingleOrDefault(u => u.Id == AttachmentLibrary.Id);
                    if (updateAttachmentLibrary != null)
                    {
                        //updateAttachmentLibrary = userRole;
                        updateAttachmentLibrary.Id = AttachmentLibrary.Id;
                        updateAttachmentLibrary.ReferenceId = AttachmentLibrary.ReferenceId;
                        updateAttachmentLibrary.AttachmentType = AttachmentLibrary.AttachmentType;
                        updateAttachmentLibrary.FileName = AttachmentLibrary.FileName;
                        updateAttachmentLibrary.FileType = AttachmentLibrary.FileType;
                        updateAttachmentLibrary.FilePath = AttachmentLibrary.FilePath;
                        updateAttachmentLibrary.UpdatedOn = AttachmentLibrary.UpdatedOn;
                        updateAttachmentLibrary.UpdatedBy = AttachmentLibrary.UpdatedBy;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = AttachmentLibrary;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = AttachmentLibrary;
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
                    updateResponse.Result = AttachmentLibrary;
                }
                return updateResponse;
            }
        }

        public Response<AttachmentLibrary> Delete(long Id)
        {
            Response<AttachmentLibrary> deleteResponse = new Response<AttachmentLibrary>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    AttachmentLibrary deleteAttachmentLibrary = db.AttachmentLibrarys.SingleOrDefault(u => u.Id == Id);
                    if (deleteAttachmentLibrary != null)
                    {
                        db.AttachmentLibrarys.Remove(deleteAttachmentLibrary);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteAttachmentLibrary;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteAttachmentLibrary;
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
