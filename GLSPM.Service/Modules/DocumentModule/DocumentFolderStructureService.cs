using GLSPM.Data;
using GLSPM.Data.Modules.DocumentModule;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Service.Modules.DocumentModule
{
    public class DocumentFolderStructureService
    {
        #region Common Methods

        public List<DocumentFolderStructure> All()
        {
            List<DocumentFolderStructure> DocumentFolderStructure = new List<DocumentFolderStructure>();

            using (GLSPMContext db = new GLSPMContext())
            {
                DocumentFolderStructure = db.DocumentFolderStructures.OrderBy(f => f.DisplayOrder).AsParallel().ToList();

                if (DocumentFolderStructure == null)
                {
                    DocumentFolderStructure = new List<DocumentFolderStructure>();
                }
                return DocumentFolderStructure;
            }
        }

        public DocumentFolderStructure GetByID(int userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                DocumentFolderStructure DocumentFolderStructure = db.DocumentFolderStructures.SingleOrDefault(u => u.Id == userRoleId);
                return DocumentFolderStructure;
            }
        }
        public Response<DocumentFolderStructure> Add(DocumentFolderStructure DocumentFolderStructure)
        {
            Response<DocumentFolderStructure> addResponse = new Response<DocumentFolderStructure>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.DocumentFolderStructures.Add(DocumentFolderStructure);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = DocumentFolderStructure;
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
                    addResponse.Result = DocumentFolderStructure;
                }
                return addResponse;
            }
        }

        public Response<DocumentFolderStructure> Update(DocumentFolderStructure DocumentFolderStructure)
        {
            Response<DocumentFolderStructure> updateResponse = new Response<DocumentFolderStructure>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    DocumentFolderStructure updateDocumentFolderStructure = db.DocumentFolderStructures.SingleOrDefault(u => u.Id == DocumentFolderStructure.Id);
                    if (updateDocumentFolderStructure != null)
                    {
                        //updateDocumentFolderStructure = userRole;
                        updateDocumentFolderStructure.Id = DocumentFolderStructure.Id;
                        updateDocumentFolderStructure.ParentId = DocumentFolderStructure.ParentId;
                        updateDocumentFolderStructure.FolderName = DocumentFolderStructure.FolderName;
                        updateDocumentFolderStructure.IsActive = DocumentFolderStructure.IsActive;
                        updateDocumentFolderStructure.DisplayOrder = DocumentFolderStructure.DisplayOrder;
                        updateDocumentFolderStructure.CanDelete = DocumentFolderStructure.CanDelete;
                        updateDocumentFolderStructure.CreatedOn = DocumentFolderStructure.CreatedOn;
                        updateDocumentFolderStructure.CreatedBy = DocumentFolderStructure.CreatedBy;
                        updateDocumentFolderStructure.UpdatedOn = DocumentFolderStructure.UpdatedOn;
                        updateDocumentFolderStructure.UpdatedBy = DocumentFolderStructure.UpdatedBy;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = DocumentFolderStructure;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = DocumentFolderStructure;
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
                    updateResponse.Result = DocumentFolderStructure;
                }
                return updateResponse;
            }
        }

        public Response<DocumentFolderStructure> Delete(int Id)
        {
            Response<DocumentFolderStructure> deleteResponse = new Response<DocumentFolderStructure>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    DocumentFolderStructure deleteDocumentFolderStructure = db.DocumentFolderStructures.SingleOrDefault(u => u.Id == Id);
                    if (deleteDocumentFolderStructure != null)
                    {
                        db.DocumentFolderStructures.Remove(deleteDocumentFolderStructure);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteDocumentFolderStructure;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteDocumentFolderStructure;
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
