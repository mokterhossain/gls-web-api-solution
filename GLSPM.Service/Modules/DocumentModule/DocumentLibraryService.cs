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
    public class DocumentLibraryService
    {
        #region Common Methods

        public List<DocumentLibrary> All()
        {
            List<DocumentLibrary> DocumentLibrary = new List<DocumentLibrary>();

            using (GLSPMContext db = new GLSPMContext())
            {
                DocumentLibrary = db.DocumentLibrarys.OrderBy(f => f.Id).AsParallel().ToList();

                if (DocumentLibrary == null)
                {
                    DocumentLibrary = new List<DocumentLibrary>();
                }
                return DocumentLibrary;
            }
        }
        public List<DocumentLibrary> GetByFolderId(int folderId)
        {
            List<DocumentLibrary> DocumentLibrary = new List<DocumentLibrary>();

            using (GLSPMContext db = new GLSPMContext())
            {
                DocumentLibrary = db.DocumentLibrarys.Where(d=> d.FolderId == folderId).OrderBy(f => f.Id).AsParallel().ToList();

                if (DocumentLibrary == null)
                {
                    DocumentLibrary = new List<DocumentLibrary>();
                }
                return DocumentLibrary;
            }
        }
        public DocumentLibrary GetByID(int userRoleId)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                DocumentLibrary DocumentLibrary = db.DocumentLibrarys.SingleOrDefault(u => u.Id == userRoleId);
                return DocumentLibrary;
            }
        }
        public DocumentLibrary GetByFileName(string fileName)
        {
            using (GLSPMContext db = new GLSPMContext())
            {
                DocumentLibrary DocumentLibrary = db.DocumentLibrarys.SingleOrDefault(u => u.Title == fileName);
                return DocumentLibrary;
            }
        }
        public Response<DocumentLibrary> Add(DocumentLibrary DocumentLibrary)
        {
            Response<DocumentLibrary> addResponse = new Response<DocumentLibrary>();
            using (GLSPMContext db = new GLSPMContext())
            {
                db.DocumentLibrarys.Add(DocumentLibrary);
                try
                {
                    int checkSuccess = 0;
                    checkSuccess = db.SaveChanges();

                    if (checkSuccess > 0)
                    {
                        addResponse.Message = "Add successfully";
                        addResponse.IsSuccess = true;
                        addResponse.Result = DocumentLibrary;
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
                    addResponse.Result = DocumentLibrary;
                }
                return addResponse;
            }
        }

        public Response<DocumentLibrary> Update(DocumentLibrary DocumentLibrary)
        {
            Response<DocumentLibrary> updateResponse = new Response<DocumentLibrary>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    DocumentLibrary updateDocumentLibrary = db.DocumentLibrarys.SingleOrDefault(u => u.Id == DocumentLibrary.Id);
                    if (updateDocumentLibrary != null)
                    {
                        //updateDocumentLibrary = userRole;
                        updateDocumentLibrary.Id = DocumentLibrary.Id;
                        updateDocumentLibrary.DocumentTypeId = DocumentLibrary.DocumentTypeId;
                        updateDocumentLibrary.DocumentType = DocumentLibrary.DocumentType;
                        updateDocumentLibrary.Title = DocumentLibrary.Title;
                        updateDocumentLibrary.DocumentURL = DocumentLibrary.DocumentURL;
                        updateDocumentLibrary.FileType = DocumentLibrary.FileType;
                        updateDocumentLibrary.IsObsolete = DocumentLibrary.IsObsolete;

                        updateDocumentLibrary.Revision = DocumentLibrary.Revision;
                        updateDocumentLibrary.EffectiveDate = DocumentLibrary.EffectiveDate;
                        updateDocumentLibrary.CreatedOn = DocumentLibrary.CreatedOn;
                        updateDocumentLibrary.CreatedBy = DocumentLibrary.CreatedBy;
                        updateDocumentLibrary.UpdatedOn = DocumentLibrary.UpdatedOn;
                        updateDocumentLibrary.UpdatedBy = DocumentLibrary.UpdatedBy;
                        updateDocumentLibrary.FolderId = DocumentLibrary.FolderId;
                        updateDocumentLibrary.DocumentID = DocumentLibrary.DocumentID;
                        updateDocumentLibrary.CanDelete = DocumentLibrary.CanDelete;
                        updateDocumentLibrary.DisplayOrder = DocumentLibrary.DisplayOrder;

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            updateResponse.Result = DocumentLibrary;
                            updateResponse.IsSuccess = true;
                            updateResponse.Message = "System menu updated successfully";
                        }
                        else
                        {
                            updateResponse.Result = DocumentLibrary;
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
                    updateResponse.Result = DocumentLibrary;
                }
                return updateResponse;
            }
        }

        public Response<DocumentLibrary> Delete(long Id)
        {
            Response<DocumentLibrary> deleteResponse = new Response<DocumentLibrary>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    DocumentLibrary deleteDocumentLibrary = db.DocumentLibrarys.SingleOrDefault(u => u.Id == Id);
                    if (deleteDocumentLibrary != null)
                    {
                        db.DocumentLibrarys.Remove(deleteDocumentLibrary);

                        int check = db.SaveChanges();

                        if (check > 0)
                        {
                            deleteResponse.Result = deleteDocumentLibrary;
                            deleteResponse.IsSuccess = true;
                            deleteResponse.Message = "Role Info deleted successfully";
                        }
                        else
                        {
                            deleteResponse.Result = deleteDocumentLibrary;
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

        #region Other Methods
        public Response<DocumentLibrary> SaveDocument(DocumentLibrary document)
        {
            Response<DocumentLibrary> addResponse = new Response<DocumentLibrary>();
            using (GLSPMContext db = new GLSPMContext())
            {
                try
                {
                    string sql = "GLS_SaveDocument";
                    List<SqlParameter> parameterList = new List<SqlParameter>();
                    var idParam = new SqlParameter { ParameterName = "Id", Value = document.Id };
                    var DocumentTypeIdParam = new SqlParameter { ParameterName = "DocumentTypeId", Value = document.DocumentTypeId };
                    var DocumentTypeParam = new SqlParameter { ParameterName = "DocumentType", Value = document.DocumentType };
                    var TitleParam = new SqlParameter { ParameterName = "Title", Value = document.Title };
                    var DocumentURLParam = new SqlParameter { ParameterName = "DocumentURL", Value = document.DocumentURL };
                    var FileTypeParam = new SqlParameter { ParameterName = "FileType", Value = document.FileType };
                    var IsObsoleteParam = new SqlParameter { ParameterName = "IsObsolete", Value = document.IsObsolete };

                    var RevisionParam = new SqlParameter { ParameterName = "Revision", Value = document.Revision };
                    var EffectiveDateParam = new SqlParameter { ParameterName = "EffectiveDate", Value = document.EffectiveDate };
                    var CreatedOnParam = new SqlParameter { ParameterName = "CreatedOn", Value = document.CreatedOn };
                    var CreatedByParam = new SqlParameter { ParameterName = "CreatedBy", Value = document.CreatedBy };
                    var UpdatedOnParam = new SqlParameter { ParameterName = "UpdatedOn", Value = document.UpdatedOn };
                    var UpdatedByParam = new SqlParameter { ParameterName = "UpdatedBy", Value = document.UpdatedBy };
                    var FolderIdParam = new SqlParameter { ParameterName = "FolderId", Value = document.FolderId };
                    var DocumentIDParam = new SqlParameter { ParameterName = "DocumentID", Value = document.DocumentID };
                    var CanDeleteIDParam = new SqlParameter { ParameterName = "CanDelete", Value = document.CanDelete };
                    var DisplayOrderParam = new SqlParameter { ParameterName = "DisplayOrder", Value = document.DisplayOrder };
                    if (document.EffectiveDate != null)
                        db.Database.SqlQuery<DocumentLibrary>("GLS_SaveDocument @Id,@DocumentTypeId,@DocumentType,@Title,@DocumentURL,@FileType,@IsObsolete,@Revision,@CreatedOn,@CreatedBy,@UpdatedOn,@UpdatedBy,@EffectiveDate,@FolderId,@DocumentID,@CanDelete,@DisplayOrder", idParam, DocumentTypeIdParam, DocumentTypeParam, TitleParam, DocumentURLParam, FileTypeParam, IsObsoleteParam, RevisionParam, CreatedOnParam,CreatedByParam,UpdatedOnParam,UpdatedByParam, EffectiveDateParam, FolderIdParam, DocumentIDParam,CanDeleteIDParam, DisplayOrderParam).ToList<DocumentLibrary>();
                    else
                        db.Database.SqlQuery<DocumentLibrary>("GLS_SaveDocument @Id,@DocumentTypeId,@DocumentType,@Title,@DocumentURL,@FileType,@IsObsolete,@Revision,@CreatedOn,@CreatedBy,@UpdatedOn,@UpdatedBy,@FolderId,@DocumentID,@CanDelete,@DisplayOrder", idParam, DocumentTypeIdParam, DocumentTypeParam, TitleParam, DocumentURLParam, FileTypeParam, IsObsoleteParam, RevisionParam, CreatedOnParam, CreatedByParam, UpdatedOnParam, UpdatedByParam, FolderIdParam, DocumentIDParam, CanDeleteIDParam, DisplayOrderParam).ToList<DocumentLibrary>();
                    addResponse.IsSuccess = true;
                }
                catch (Exception ex)
                {
                    int errorNo = ((SqlException)ex.InnerException.InnerException).Number;
                    if (errorNo == 2601 || errorNo == 2627)
                    {
                        addResponse.Message = "This Document Info already exist.";
                    }
                    else
                    {
                        addResponse.Message = "There was an error while adding the Document Info: " + ex.Message;
                    }
                    addResponse.IsSuccess = false;
                    addResponse.Result = document;
                }
            }
            return addResponse;
        }
        #endregion
    }
}
